using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Globalization;


namespace Punto_Venta
{
    public partial class frmAgregarInventario : Form
    {
        public string lista = "";
        OleDbConnection conectar = new OleDbConnection(Conexion.CadCon); 
        OleDbCommand cmd;
        OleDbDataAdapter da;
        public frmAgregarInventario()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cmd = new OleDbCommand("INSERT INTO articulos (Nombre, Cantidad,Medida,origen,precio,limite) VALUES ('" + txtProducto.Text + "','" + txtCantidad.Text + "','" + comboBox1.Text + "','" + comboBox2.Text + "','"+txtPrecio.Text+"','"+txtLimite.Text+"');", conectar);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Se ha agregado el producto correctamente","Correcto",MessageBoxButtons.OK,MessageBoxIcon.Information);
            frmInventario invent = new frmInventario();
            if (lista != "")
            {
                invent.checkBox1.Checked = true;
                invent.textBox1.Enabled = false;
                invent.cmbOrigen.Text = lista;
            }
            invent.Show();
            this.Close();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtProducto_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void frmAgregarInventario_Load(object sender, EventArgs e)
        {
            conectar.Open();
            DataTable dt = new DataTable();
            cmd = new OleDbCommand("SELECT * from Origen;", conectar);
            da = new OleDbDataAdapter(cmd);
            da.Fill(dt);
            comboBox2.DisplayMember = "Nombre";
            comboBox2.ValueMember = "Nombre";
            comboBox2.DataSource = dt;
            comboBox2.Text = "";
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar))//Si es número
            {
                e.Handled = false;
            }
            else if (e.KeyChar == (char)Keys.Back)//si es tecla borrar
            {
                e.Handled = false;
            }
            else //Si es otra tecla cancelamos
            {
                e.Handled = true;
            }
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (Char.IsNumber(e.KeyChar))//Si es número
            {
                e.Handled = false;
            }
            else if (e.KeyChar == (char)Keys.Back)//si es tecla borrar
            {
                e.Handled = false;
            }
            else //Si es otra tecla cancelamos
            {
                e.Handled = true;
            }
        }

        private void txtLimite_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (Char.IsNumber(e.KeyChar))//Si es número
            {
                e.Handled = false;
            }
            else if (e.KeyChar == (char)Keys.Back)//si es tecla borrar
            {
                e.Handled = false;
            }
            else //Si es otra tecla cancelamos
            {
                e.Handled = true;
            }
        }
    }
}
