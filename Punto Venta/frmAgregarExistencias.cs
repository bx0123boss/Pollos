using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data.OleDb;

namespace Punto_Venta
{
    public partial class frmAgregarExistencias : Form
    {

        OleDbConnection conectar = new OleDbConnection(Conexion.CadCon); 
        OleDbCommand cmd;
        public string lista = "";
        public frmAgregarExistencias()
        {
            InitializeComponent();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            txtTotales.Text = "" + (Convert.ToDouble(txtActuales.Text) + Convert.ToDouble(textBox1.Text));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cmd = new OleDbCommand("select * from invent where idArticulo=" + lblID.Text + ";", conectar);
            OleDbDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                string salida = "" + (Convert.ToDouble(Convert.ToString(reader[3].ToString())) + Convert.ToDouble(textBox1.Text));
                cmd = new OleDbCommand("UPDATE invent set entrada='" + salida + "' Where idArticulo=" + lblID.Text + ";", conectar);
                cmd.ExecuteNonQuery();
            }
            cmd = new OleDbCommand("UPDATE articulos set cantidad='" + txtTotales.Text + "' Where id=" + lblID.Text + ";", conectar);            
            cmd.ExecuteNonQuery();
            MessageBox.Show("Se ha actualizado el producto correctamente", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            if (lista == "FISICO")
            {
                frmActInventario ACTU = new frmActInventario();
                ACTU.ShowDialog();
            }
            else if (lista != "")
            {
                frmInventario invent = new frmInventario();
                invent.checkBox1.Checked = true;
                invent.textBox1.Enabled = false;
                invent.cmbOrigen.Text = lista;
                invent.ShowDialog();
            }
            this.Close();

        }

        private void frmAgregarExistencias_Load(object sender, EventArgs e)
        {
            conectar.Open();
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
