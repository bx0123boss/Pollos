using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient; // Asegúrate de tener esta referencia
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Punto_Venta
{
    public partial class frmCantidad : Form
    {
        public string comentario { get; set; }
        public double cantidad { get; set; }

        public frmCantidad()
        {
            InitializeComponent();
        }
        private void frmCantidad_Load(object sender, EventArgs e)
        {
            CargarBotonesDinamicos();
        }

        private void CargarBotonesDinamicos()
        {
            flowLayoutPanelBotones.Controls.Clear();

            string query = "SELECT Nombre FROM BotonesComentario";

            try
            {
                using (SqlConnection connection = new SqlConnection(Conexion.CadConSql))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string nombreBoton = reader["Nombre"].ToString();

                                Button btn = new Button();
                                btn.Text = nombreBoton;
                                btn.Width = 80;
                                btn.Height = 49;
                                btn.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Regular);
                                btn.UseVisualStyleBackColor = true;

                                // Asignar el manejador de eventos existente
                                btn.Click += new EventHandler(button_Click);

                                // Agregar al contenedor dinámico
                                flowLayoutPanelBotones.Controls.Add(btn);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los botones de comentarios: " + ex.Message, "Error BD", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = (Convert.ToDouble(textBox1.Text) + 1).ToString();
            txtComentario.Focus();
        }

        public void Cantidad(double cant)
        {
            comentario = txtComentario.Text;
            cantidad = cant;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = (Convert.ToDouble(textBox1.Text) + 2).ToString();
            txtComentario.Focus();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = (Convert.ToDouble(textBox1.Text) + 3).ToString();
            txtComentario.Focus();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = (Convert.ToDouble(textBox1.Text) + 4).ToString();
            txtComentario.Focus();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = (Convert.ToDouble(textBox1.Text) + 5).ToString();
            txtComentario.Focus();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text = (Convert.ToDouble(textBox1.Text) + 6).ToString();
            txtComentario.Focus();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Text = (Convert.ToDouble(textBox1.Text) + 7).ToString();
            txtComentario.Focus();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox1.Text = (Convert.ToDouble(textBox1.Text) + 8).ToString();
            txtComentario.Focus();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox1.Text = (Convert.ToDouble(textBox1.Text) + 9).ToString();
            txtComentario.Focus();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            textBox1.AppendText(".");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            textBox1.Text = (Convert.ToDouble(textBox1.Text) + 0).ToString();
            txtComentario.Focus();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "0")
            {
                MessageBox.Show("La cantidad no es un numero valido, verifique", "Product error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    double price = Convert.ToDouble(textBox1.Text);
                    Cantidad(price);
                }
                catch (FormatException)
                {
                    MessageBox.Show("La cantidad no es un numero valido, verifique", "Product error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Text = "0";
                }
            }
            txtComentario.Focus();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            textBox1.Text = (Convert.ToDouble(textBox1.Text) + 0.25).ToString();
            txtComentario.Focus();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            textBox1.Text = (Convert.ToDouble(textBox1.Text) + 0.5).ToString();
            txtComentario.Focus();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            textBox1.Text = (Convert.ToDouble(textBox1.Text) + 0.75).ToString();
            txtComentario.Focus();
        }

        private void txtComentario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && e.KeyChar != '\b' && !char.IsNumber(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            // Mantiene el comportamiento original de redimensionado
            if (button16.Text == "◄")
            {
                this.Size = new Size(372, 460);
                button16.Text = "►";
            }
            else
            {
                this.Size = new Size(650, 460); // Aumentado ligeramente a 650 para dar espacio cómodo al panel dinámico
                button16.Text = "◄";
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                txtComentario.AppendText(" " + button.Text);
            }
        }

        private void button31_Click(object sender, EventArgs e)
        {
            txtComentario.Clear();
        }

        private void button32_Click(object sender, EventArgs e)
        {
            textBox1.Text = "0";
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
    }
}