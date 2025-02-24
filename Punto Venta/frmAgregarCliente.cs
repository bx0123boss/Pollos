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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.SqlClient;

namespace Punto_Venta
{
    public partial class frmAgregarCliente : Form
    {
        public int id;
        public string Nombre { get; set; }

        public frmAgregarCliente()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();

                if (this.Text == "Editar")
                {

                    // Usar parámetros para evitar la inyección SQL
                    string query = "UPDATE Clientes SET Nombre=@Nombre, Telefono=@Telefono, Direccion=@Direccion, Referencia=@Referencia, Colonia=@Colonia WHERE IdCliente=@id;";

                    using (SqlCommand cmd = new SqlCommand(query, conectar))
                    {
                        cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                        cmd.Parameters.AddWithValue("@Telefono", txtTelefono.Text);
                        cmd.Parameters.AddWithValue("@Direccion", txtDireccion.Text);
                        cmd.Parameters.AddWithValue("@Referencia", txtReferencia.Text);
                        cmd.Parameters.AddWithValue("@Colonia", txtColonia.Text);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Se ha editado el cliente correctamente", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Nombre = txtTelefono.Text;
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    }

                }
                else
                {
                    if (txtNombre.Text == "" || txtTelefono.Text == "" || txtDireccion.Text == "" || txtColonia.Text == "")
                    {
                        MessageBox.Show("Faltan campos por llenar", "Alto!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        using (SqlCommand cmdInsertar = new SqlCommand("INSERT INTO Clientes (Nombre, Telefono,Direccion,Referencia,Colonia) " +
                                                                        "VALUES (@Nombre, @Telefono, @Direccion, @Referencia, @Colonia);", conectar))
                        {
                            cmdInsertar.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                            cmdInsertar.Parameters.AddWithValue("@Telefono", txtTelefono.Text);
                            cmdInsertar.Parameters.AddWithValue("@Direccion", txtDireccion.Text);
                            cmdInsertar.Parameters.AddWithValue("@Referencia", txtReferencia.Text);
                            cmdInsertar.Parameters.AddWithValue("@Colonia", txtColonia.Text);

                            cmdInsertar.ExecuteNonQuery();

                            MessageBox.Show("Se ha agregado el cliente correctamente", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Nombre = txtTelefono.Text;

                            this.DialogResult = System.Windows.Forms.DialogResult.OK;
                        }
                       
                    }
                }
            }
        
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
