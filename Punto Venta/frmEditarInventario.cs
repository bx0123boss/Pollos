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

namespace Punto_Venta
{
    public partial class frmEditarInventario : Form
    {
        OleDbConnection conectar = new OleDbConnection(Conexion.CadCon); 
        OleDbCommand cmd;
        public string lista = "";
        OleDbDataAdapter da;
        public string origen;
        public frmEditarInventario()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conectar.Open();
            cmd = new OleDbCommand("UPDATE articulos set Nombre='" + txtProducto.Text + "', Cantidad='" + txtCantidad.Text + "', Medida='" + comboBox1.Text + "', Origen='" + comboBox2.Text + "', precio='"+txtPrecio.Text+"', limite='"+txtLimite.Text+"' Where id=" + txtID.Text + ";", conectar);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Se ha actualizado el producto correctamente", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
            frmInventario invent = new frmInventario();            
            invent.Show();
            this.Close();
        }

        private void frmEditarInventario_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            cmd = new OleDbCommand("SELECT * from Origen;", conectar);
            da = new OleDbDataAdapter(cmd);
            da.Fill(dt);
            comboBox2.DisplayMember = "Nombre";
            comboBox2.ValueMember = "Nombre";
            comboBox2.DataSource = dt;
            comboBox2.Text = origen;
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtLimite_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
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
