using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
            textBox1.Text = (Convert.ToDouble(textBox1.Text) + 0.25 ).ToString();
            txtComentario.Focus();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            textBox1.Text = (Convert.ToDouble(textBox1.Text) + 0.5 ).ToString();
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
            if (button16.Text == "◄")
            {
                this.Size = new Size(372, 460);
                button16.Text = "►";
            }
            else
            {
                this.Size = new Size(550, 460);
                button16.Text = "◄";
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            txtComentario.AppendText(" Sin cebolla ");
        }

        private void button18_Click(object sender, EventArgs e)
        {
            txtComentario.AppendText(" Sin jitomate ");
        }

        private void button19_Click(object sender, EventArgs e)
        {
            txtComentario.AppendText(" Sin picante ");
        }

        private void button20_Click(object sender, EventArgs e)
        {
            txtComentario.AppendText(" Sin aderezo ");
        }

        private void button21_Click(object sender, EventArgs e)
        {
            txtComentario.AppendText(" Sin catsup ");
        }

        private void button22_Click(object sender, EventArgs e)
        {
            txtComentario.AppendText(" FRESA ");
        }

        private void button23_Click(object sender, EventArgs e)
        {
            txtComentario.AppendText(" PIÑA ");
        }

        private void button24_Click(object sender, EventArgs e)
        {
            txtComentario.AppendText(" BBQ ");
        }

        private void button25_Click(object sender, EventArgs e)
        {
            txtComentario.AppendText(" Bufalo ");
        }

        private void button26_Click(object sender, EventArgs e)
        {
            txtComentario.AppendText(" Chiltepin ");
        }

        private void button27_Click(object sender, EventArgs e)
        {
            txtComentario.AppendText(" Tamarindo ");
        }

        private void button28_Click(object sender, EventArgs e)
        {
            txtComentario.AppendText(" Mango Habanero ");
        }

        private void button29_Click(object sender, EventArgs e)
        {
            txtComentario.AppendText(" Piña Habanero ");
        }

        private void button30_Click(object sender, EventArgs e)
        {
            txtComentario.AppendText(" Para llevar ");
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

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
    }
}
