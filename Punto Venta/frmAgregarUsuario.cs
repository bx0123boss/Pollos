using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Punto_Venta
{
    public partial class frmAgregarUsuario : Form
    {
        OleDbConnection conectar = new OleDbConnection(Conexion.CadCon);
        OleDbCommand cmd; 

        public frmAgregarUsuario()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtPass.Text == txtPass2.Text)
            {
                bool existe = false;

                using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
                {
                    conectar.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT Usuario FROM Usuarios WHERE Usuario = @Usuario;", conectar))
                    {
                        cmd.Parameters.AddWithValue("@Usuario", txtNombre.Text);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                existe = true;
                            }
                        }
                    }
                    using (SqlCommand cmd = new SqlCommand("SELECT Usuario FROM Usuarios WHERE Contraseña = @Contraseña;", conectar))
                    {
                        cmd.Parameters.AddWithValue("@Contraseña", txtPass.Text);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                existe = true;
                            }
                        }
                    }

                    if (existe)
                    {
                        MessageBox.Show("Existe un usuario similar, favor de verificar", "Agregar Usuario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        using (SqlCommand cmd = new SqlCommand("INSERT INTO Usuarios (Usuario, Contraseña, TipoUsuario, Ventas, Mesas) VALUES (@Usuario, @Contraseña, @TipoUsuario, '0', '0');", conectar))
                        {
                            cmd.Parameters.AddWithValue("@Usuario", txtNombre.Text);
                            cmd.Parameters.AddWithValue("@Contraseña", txtPass.Text);
                            cmd.Parameters.AddWithValue("@TipoUsuario", txtTipo.Text);
                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("¡Se ha agregado al usuario con éxito!", "Agregar Usuario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Las contraseñas no son idénticas", "Agregar Usuario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPass.Clear();
                txtPass2.Clear();
                txtPass.Focus();
            }
        }

        private void frmAgregarUsuario_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmUsuarios users = new frmUsuarios();
            users.Show();
        }

        private void frmAgregarUsuario_Load(object sender, EventArgs e)
        {
            conectar.Open();
        }
    }
}
