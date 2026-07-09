using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Punto_Venta
{
    public partial class frmAgregarProveedor : Form
    {
        public bool buscar { get; set; } = false;
        public string Nombre { get; set; }

        public frmAgregarProveedor()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Por favor, ingrese el nombre del proveedor.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return;
            }

            try
            {
                using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
                {
                    conectar.Open();

                    if (this.Text == "Agregar")
                    {
                        string queryInsert = "INSERT INTO Proveedores (Nombre, RFC, Direccion, Telefono, Correo, Referencia, Clave, Adeudo) " +
                                             "VALUES (@Nombre, @RFC, @Direccion, @Telefono, @Correo, @Referencia, @Clave, 0);";

                        using (SqlCommand cmd = new SqlCommand(queryInsert, conectar))
                        {
                            cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 150).Value = txtNombre.Text.Trim();
                            cmd.Parameters.Add("@RFC", SqlDbType.VarChar, 20).Value = txtRFC.Text.Trim();
                            cmd.Parameters.Add("@Direccion", SqlDbType.VarChar, 255).Value = txtDireccion.Text.Trim();
                            cmd.Parameters.Add("@Telefono", SqlDbType.VarChar, 20).Value = txtTelefono.Text.Trim();
                            cmd.Parameters.Add("@Correo", SqlDbType.VarChar, 100).Value = txtCorreo.Text.Trim();
                            cmd.Parameters.Add("@Referencia", SqlDbType.VarChar, 150).Value = txtReferencia.Text.Trim();
                            cmd.Parameters.Add("@Clave", SqlDbType.VarChar, 50).Value = textBox1.Text.Trim();

                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("Se ha agregado el proveedor con éxito.", "AGREGADO", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (buscar)
                        {
                            Nombre = txtNombre.Text.Trim();
                            this.DialogResult = DialogResult.OK;
                        }
                        else
                        {
                            this.Close();
                            frmProveedores cliente = new frmProveedores();
                            cliente.Show();
                        }
                    }
                    else
                    {
                        if (!int.TryParse(lblID.Text, out int idProveedor))
                        {
                            MessageBox.Show("Identificador de proveedor no válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        string queryUpdate = "UPDATE Proveedores SET Nombre = @Nombre, Telefono = @Telefono, Direccion = @Direccion, " +
                                             "Referencia = @Referencia, RFC = @RFC, Correo = @Correo, Clave = @Clave " +
                                             "WHERE Id = @Id;";

                        using (SqlCommand cmd = new SqlCommand(queryUpdate, conectar))
                        {
                            cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 150).Value = txtNombre.Text.Trim();
                            cmd.Parameters.Add("@Telefono", SqlDbType.VarChar, 20).Value = txtTelefono.Text.Trim();
                            cmd.Parameters.Add("@Direccion", SqlDbType.VarChar, 255).Value = txtDireccion.Text.Trim();
                            cmd.Parameters.Add("@Referencia", SqlDbType.VarChar, 150).Value = txtReferencia.Text.Trim();
                            cmd.Parameters.Add("@RFC", SqlDbType.VarChar, 20).Value = txtRFC.Text.Trim();
                            cmd.Parameters.Add("@Correo", SqlDbType.VarChar, 100).Value = txtCorreo.Text.Trim();
                            cmd.Parameters.Add("@Clave", SqlDbType.VarChar, 50).Value = textBox1.Text.Trim();
                            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = idProveedor;

                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("Se ha actualizado el proveedor con éxito.", "ACTUALIZADO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al guardar los datos: " + ex.Message, "Error de Base de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }
    }
}