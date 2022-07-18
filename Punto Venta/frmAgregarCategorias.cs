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
    public partial class frmAgregarCategorias : Form
    {
        OleDbConnection conectar = new OleDbConnection(Conexion.CadCon);
        OleDbCommand cmd;
        public string id="";
        string color="bab8b8";
        public string tipo;
        public frmAgregarCategorias()
        {
            InitializeComponent();
            conectar.Open();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text == "")
            {
                MessageBox.Show("Nombre invalido", "Agregar "+tipo, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                bool existe = false;
                cmd = new OleDbCommand("select Nombre from "+ tipo +" where Nombre='" + txtNombre.Text + "';", conectar);
                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    existe = true;
                }
                if (id!="")
                {
                    string letra;
                    if (radioButton1.Checked)
                        letra = "Black";
                    else
                        letra = "White";
                    cmd = new OleDbCommand("update "+tipo+" set Nombre='" + txtNombre.Text + "', Color='" + color + "', Letra='" + letra + "' where Id=" + id + ";", conectar);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Se ha editado la "+tipo+" con exito", "EXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmCategorias apart = new frmCategorias();
                    apart.tipo = tipo;
                    apart.Show();
                    this.Close();
                }
                else if (existe)
                {
                    MessageBox.Show("Existe una "+tipo+" similar, favor de verificar", "Agregar Categoria", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string letra;
                    if (radioButton1.Checked)
                        letra = "Black";
                    else
                        letra = "White";
                    cmd = new OleDbCommand("insert into " + tipo + "(Nombre,Color,Letra) Values('" + txtNombre.Text + "','" + color + "','" + letra + "');", conectar);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Se ha creado la " + tipo + " con exito", "EXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmCategorias apart = new frmCategorias();
                    apart.tipo = tipo;
                    apart.Show();
                    this.Close();
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                color = (colorDialog1.Color.ToArgb() & 0x00FFFFFF).ToString("X6");
                button2.BackColor = System.Drawing.ColorTranslator.FromHtml("#" + color);
            }  
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                button2.ForeColor = Color.FromName("Black");
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                button2.ForeColor = Color.FromName("White");
            }
        }

        private void frmAgregarCategorias_Load(object sender, EventArgs e)
        {
            this.Text = tipo;
        }
    }
}
