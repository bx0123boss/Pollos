using JaegerSoft;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net.Mail;
using System.Windows.Forms;
using Tickets80mm;
using static ComandasReportPdf;

namespace Punto_Venta
{
    public partial class Form1 : Form
    {
        int idMesero = 0;
        string usuario = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            entrar();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.facebook.com/jaegersoft/");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                this.BackgroundImage = Image.FromFile("C:\\Jaeger Soft\\w1.jpg");
                using (StreamWriter sw = new StreamWriter(@"MODIFICACION.txt"))
                {
                    sw.WriteLine(DateTime.Now.ToShortDateString() + " ");
                    sw.WriteLine(DateTime.Now.ToShortTimeString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }

            // --- CONFIGURACIÓN SEGÚN MODO TOUCH / TRADICIONAL ---
            if (Conexion.AbrirTeclado)
            {
                // Modo Touch: Solo captura PIN/Contraseña
                txtUser.Visible = false;
                label1.Visible = false; // Oculta el label "Usuario"

                // Acomodar la caja de contraseña y el botón si es necesario
                txtContraseña.Focus();
            }
            else
            {
                // Modo Tradicional: Cargar la lista de usuarios en el ComboBox
                txtUser.Visible = true;
                label1.Visible = true;

                using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
                {
                    conectar.Open();
                    DataTable dt = new DataTable();
                    using (SqlCommand cmd = new SqlCommand("SELECT IdUsuario, Usuario FROM Usuarios;", conectar))
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                        txtUser.DisplayMember = "Usuario";
                        txtUser.ValueMember = "IdUsuario";
                        txtUser.DataSource = dt;
                        txtUser.Text = "";
                    }
                }
            }
        }

        private void txtContraseña_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                entrar();
            }
        }

        public void entrar()
        {
            try
            {
                using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
                {
                    conectar.Open();

                    // Obtener datos del usuario autenticado
                    string aut = Autentica();
                    if (aut == "ERROR")
                    {
                        MessageBox.Show("La contraseña o el usuario introducido no son válidos.\nFavor de verificar.",
                                      "Error de Autenticación", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        if (!Conexion.AbrirTeclado)
                            txtUser.Text = "";

                        txtContraseña.Clear();
                        txtContraseña.Focus();
                        return;
                    }

                    // Consultar el estado de inicio del sistema (caja abierta o cerrada)
                    using (SqlCommand cmd = new SqlCommand("SELECT inicio FROM inicio WHERE id = 1", conectar))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                CargarPermisosUsuario(Sesion.IdUsuario);

                                string estadoInicio = reader["inicio"].ToString();

                                if (estadoInicio == "0")
                                {
                                    // Abrir caja
                                    frmAbrirCaja caja = new frmAbrirCaja();
                                    caja.usuario = aut;
                                    caja.id = idMesero;
                                    caja.nombre = usuario;
                                    caja.ShowDialog();
                                    this.Hide();
                                }
                                else
                                {
                                    // Ir al Form principal
                                    frmPrincipal principal = new frmPrincipal();
                                    principal.id = idMesero;
                                    principal.lblUser.Text = aut;
                                    principal.usuario = usuario;
                                    principal.Show();
                                    this.Hide();
                                }
                            }
                            else
                            {
                                MessageBox.Show("No se encontró información del estado del sistema.",
                                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (SqlException exSql)
            {
                MessageBox.Show("Error de base de datos: " + exSql.Message,
                               "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message,
                               "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (Conexion.AbrirTeclado)
                CerrarTeclado();
        }

        private void CargarPermisosUsuario(string idUsuario)
        {
            try
            {
                Sesion.PermisosActuales.Clear();

                using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
                {
                    conectar.Open();

                    string queryPermisos = @"SELECT Permiso FROM PermisosUsuario WHERE IdUsuario = @IdUsuario";

                    using (SqlCommand cmdPermisos = new SqlCommand(queryPermisos, conectar))
                    {
                        cmdPermisos.Parameters.AddWithValue("@IdUsuario", idUsuario);

                        using (SqlDataReader rdrPermisos = cmdPermisos.ExecuteReader())
                        {
                            while (rdrPermisos.Read())
                            {
                                string permiso = rdrPermisos["Permiso"].ToString().ToUpper();
                                Sesion.PermisosActuales.Add(permiso);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar permisos: " + ex.Message,
                               "Error de Permisos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Autentica al usuario por Contraseña (en modo Touch) o por Combo + Contraseña (Modo Tradicional).
        /// </summary>
        private string Autentica()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtContraseña.Text))
                    return "ERROR";

                using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
                {
                    conectar.Open();
                    string query = "";

                    if (Conexion.AbrirTeclado)
                    {
                        // En pantalla touch, busca directamente qué usuario coincide con la contraseña/PIN
                        query = @"SELECT IdUsuario, usuario 
                                  FROM Usuarios 
                                  WHERE contrasena = @Contrasena";
                    }
                    else
                    {
                        // En modo tradicional, valida el nombre de usuario seleccionado y la contraseña
                        query = @"SELECT IdUsuario, usuario 
                                  FROM Usuarios 
                                  WHERE usuario = @Usuario AND contrasena = @Contrasena";
                    }

                    using (SqlCommand cmd = new SqlCommand(query, conectar))
                    {
                        cmd.Parameters.AddWithValue("@Contrasena", txtContraseña.Text.Trim());

                        if (!Conexion.AbrirTeclado)
                        {
                            cmd.Parameters.AddWithValue("@Usuario", txtUser.Text.Trim());
                        }

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                idMesero = Convert.ToInt32(reader["IdUsuario"]);
                                usuario = reader["usuario"].ToString();
                                Sesion.IdUsuario = reader["IdUsuario"].ToString();
                                Sesion.NombreUsuario = reader["usuario"].ToString();
                                return reader["usuario"].ToString();
                            }
                            else
                            {
                                return "ERROR";
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                return "ERROR";
            }
        }

        private void AbrirTeclado()
        {
            string RutaTeclado = @"C:\Jaeger Soft\FreeVK.exe";
            try
            {
                if (File.Exists(RutaTeclado))
                {
                    if (Process.GetProcessesByName("FreeVK").Length == 0)
                    {
                        Process.Start(RutaTeclado);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al abrir el teclado: " + ex.Message);
            }
        }

        private void txtContraseña_Enter(object sender, EventArgs e)
        {
            if (Conexion.AbrirTeclado)
                AbrirTeclado();
        }

        private void CerrarTeclado()
        {
            try
            {
                foreach (var proceso in Process.GetProcessesByName("FreeVK"))
                {
                    proceso.Kill();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cerrar el teclado: " + ex.Message);
            }
        }

        private void label1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
    }
}