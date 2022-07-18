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
    public partial class frmAgregarCliente : Form
    {
        OleDbConnection conectar = new OleDbConnection(Conexion.CadCon); 
        OleDbCommand cmd;
        public int id;
        public string Nombre { get; set; }

        public frmAgregarCliente()
        {
            InitializeComponent();
            conectar.Open();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.Text == "Editar")
            {
                cmd = new OleDbCommand("update Clientes set Nombre='"+txtNombre.Text+"',Telefono='"+txtTelefono.Text+"',Direccion='"+txtDireccion.Text+"',Referencia='"+txtReferencia.Text+"', Colonia='"+txtColonia.Text+"' where Id="+id+";", conectar);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Se ha editado el cliente correctamente", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmClientes clientes = new frmClientes();
                clientes.Show();
                this.Close();
            }
            else
            {
                if (txtNombre.Text == "" || txtTelefono.Text == "" || txtDireccion.Text == "" || txtColonia.Text == "")
                {
                    MessageBox.Show("Faltan campos por llenar", "Alto!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    cmd = new OleDbCommand("INSERT INTO Clientes(Nombre, Telefono,Direccion,Referencia,Colonia) VALUES ('" + txtNombre.Text + "','" + txtTelefono.Text + "','" + txtDireccion.Text + "','" + txtReferencia.Text + "','" + txtColonia.Text + "');", conectar);
                    cmd.ExecuteNonQuery();
                    Nombre = txtTelefono.Text;
                    MessageBox.Show("Se ha agregado el cliente correctamente", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void frmAgregarCliente_Load(object sender, EventArgs e)
        {

        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
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
