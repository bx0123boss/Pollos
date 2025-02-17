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
    public partial class frmClaveVendendor : Form
    {
        OleDbConnection conectar = new OleDbConnection(Conexion.CadCon);
        OleDbCommand cmd;
        public int Id { get; set; }
        public string Mesero { get; set; }
        public string Tipo { get; set; }
        public frmClaveVendendor()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clave();
        }

        private void frmClaveVendendor_Load(object sender, EventArgs e)
        {
            conectar.Open();
            // Obtener el tamaño del formulario
            int formWidth = this.ClientSize.Width;
            int formHeight = this.ClientSize.Height;

            // Obtener el tamaño del control de usuario
            int userControlWidth = label3.Width;
            int userControlHeight = label3.Height;

            // Calcular las coordenadas X e Y para centrar el control de usuario
            int userControlX = (formWidth - userControlWidth) / 2;
            int userControlY = (formHeight - userControlHeight) / 2;

            // Establecer la propiedad Location del control de usuario
            label3.Location = new Point(userControlX-220, userControlY);
            txtPass.Location = new Point(userControlX, userControlY);
            button1.Location = new Point(userControlX-50, userControlY+50);
        }

        private void txtPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                Clave();
            }
        }

        public void Clave()
        {
            if (txtPass.Text == "")
            {

            }
            else
            {
                using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
                {
                    conectar.Open();

                    string query = @"SELECT IdUsuario,Usuario,TipoUsuario FROM USUARIOS WHERE Contraseña = @Contraseña";

                    using (SqlCommand cmd = new SqlCommand(query, conectar))
                    {
                        cmd.Parameters.AddWithValue("@Contraseña", txtPass.Text);
                        using (SqlDataReader readerSQL = cmd.ExecuteReader())
                        {
                            if (readerSQL.Read()) // Si hay datos, entra aquí
                            {
                                Id = int.Parse(readerSQL["IdUsuario"].ToString());
                                Mesero = readerSQL["Usuario"].ToString();
                                Tipo = readerSQL["TipoUsuario"].ToString();
                                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                            }
                            else
                            {
                                MessageBox.Show("No se encuentra el usuario, favor de verificar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtPass.Clear();
                            }
                        }
                    }
                }
               
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
