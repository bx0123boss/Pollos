using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
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
                cmd = new OleDbCommand("select Id,Usuario,TipoUsuario from Usuarios where Contraseña='" + txtPass.Text + "';", conectar);
                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Id = Convert.ToInt32(reader[0].ToString());
                    Mesero = reader[1].ToString();
                    Tipo = reader[2].ToString();
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("No se encuentra el usuario, favor de verificar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPass.Clear();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
