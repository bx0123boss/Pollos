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
    public partial class frmAgregarCategorias : Form
    {
        public string id="";
        string color="bab8b8";
        public string tipo;
        public frmAgregarCategorias()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                MessageBox.Show("Nombre inválido", $"Agregar {tipo}", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                bool existe = false;

                using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
                {
                    conectar.Open();

                    using (SqlCommand cmd = new SqlCommand($"SELECT Nombre FROM {tipo} WHERE Nombre = @Nombre;", conectar))
                    {
                        cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                existe = true;
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(id))
                    {
                        string letra = radioButton1.Checked ? "Black" : "White";

                        using (SqlCommand cmd = new SqlCommand($"UPDATE {tipo} SET Nombre = @Nombre, Color = @Color, Letra = @Letra WHERE Id{tipo.Substring(0, tipo.Length-1)} = @Id;", conectar))
                        {
                            cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                            cmd.Parameters.AddWithValue("@Color", color);
                            cmd.Parameters.AddWithValue("@Letra", letra);
                            cmd.Parameters.AddWithValue("@Id", id);
                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show($"Se ha editado la {tipo} con éxito", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        frmCategorias apart = new frmCategorias();
                        apart.tipo = tipo;
                        apart.Show();
                        this.Close();
                    }
                    else if (existe)
                    {
                        MessageBox.Show($"Existe una {tipo} similar, favor de verificar", $"Agregar {tipo}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        string letra = radioButton1.Checked ? "Black" : "White";

                        using (SqlCommand cmd = new SqlCommand($"INSERT INTO {tipo} (Nombre, Color, Letra) VALUES (@Nombre, @Color, @Letra);", conectar))
                        {
                            cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                            cmd.Parameters.AddWithValue("@Color", color);
                            cmd.Parameters.AddWithValue("@Letra", letra);
                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show($"Se ha creado la {tipo} con éxito", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        frmCategorias apart = new frmCategorias();
                        apart.tipo = tipo;
                        apart.Show();
                        this.Close();
                    }
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
