using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Punto_Venta
{
    public partial class frmAgregarMesas : frmBase
    {
        public int IdMesa { get; set; } = 0; // Si es 0 es Nueva, si es > 0 es Edición
        public int IdMesero { get; set; }
        public string Mesa { get; set; }
        public int CantidadPersonas { get; set; }
        public bool EsEdicion { get; set; } = false; // Flag para controlar la acción

        public frmAgregarMesas()
        {
            InitializeComponent();
        }

        private void frmAgregarMesas_Load(object sender, EventArgs e)
        {
            EstilizarBotonPrimario(button1);

            // Si viene en modo edición, cargar los datos actuales en las cajas de texto
            if (EsEdicion || IdMesa > 0)
            {
                txtNombre.Text = Mesa;
                txtUbicacion.Text = CantidadPersonas.ToString(); // txtUbicacion guarda la CantidadPersonas
                button1.Text = "Guardar Cambios";
                this.Text = "Editar Mesa";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtUbicacion.Text))
            {
                MessageBox.Show("Favor de llenar todos los campos.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();

                // 1. Validar que no exista otra mesa activa con el mismo nombre
                // Excluimos la mesa actual si estamos en modo Edición
                string checkQuery = "SELECT Nombre FROM Mesas WHERE Nombre = @Nombre AND Estatus = 'COCINA'";
                if (EsEdicion || IdMesa > 0)
                {
                    checkQuery += " AND IdMesa <> @IdMesa";
                }

                bool existe = false;
                using (SqlCommand cmdCheck = new SqlCommand(checkQuery, conectar))
                {
                    cmdCheck.Parameters.AddWithValue("@Nombre", txtNombre.Text.Trim());
                    if (EsEdicion || IdMesa > 0)
                    {
                        cmdCheck.Parameters.AddWithValue("@IdMesa", IdMesa);
                    }

                    using (SqlDataReader reader = cmdCheck.ExecuteReader())
                    {
                        if (reader.Read()) existe = true;
                    }
                }

                if (existe)
                {
                    MessageBox.Show("Existe una mesa similar activa, favor de verificar.", "Agregar / Editar Mesas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 2. Ejecutar UPDATE o INSERT según sea el caso
                if (EsEdicion || IdMesa > 0)
                {
                    string updateQuery = @"UPDATE Mesas 
                                           SET Nombre = @Nombre, 
                                               CantidadPersonas = @CantidadPersonas 
                                           WHERE IdMesa = @IdMesa";

                    using (SqlCommand cmdUpdate = new SqlCommand(updateQuery, conectar))
                    {
                        cmdUpdate.Parameters.AddWithValue("@Nombre", txtNombre.Text.Trim());
                        cmdUpdate.Parameters.AddWithValue("@CantidadPersonas", int.Parse(txtUbicacion.Text));
                        cmdUpdate.Parameters.AddWithValue("@IdMesa", IdMesa);

                        cmdUpdate.ExecuteNonQuery();
                    }
                }
                else
                {
                    string insertQuery = @"INSERT INTO Mesas (Nombre, IdMesero, CantidadPersonas, Impresion, Estatus) 
                                           VALUES (@Nombre, @IdMesero, @CantidadPersonas, @Impresion, @Estatus); 
                                           SELECT SCOPE_IDENTITY();";

                    using (SqlCommand cmdInsert = new SqlCommand(insertQuery, conectar))
                    {
                        cmdInsert.Parameters.AddWithValue("@Nombre", txtNombre.Text.Trim());
                        cmdInsert.Parameters.AddWithValue("@IdMesero", IdMesero);
                        cmdInsert.Parameters.AddWithValue("@CantidadPersonas", int.Parse(txtUbicacion.Text));
                        cmdInsert.Parameters.AddWithValue("@Impresion", 0);
                        cmdInsert.Parameters.AddWithValue("@Estatus", "NUEVA");

                        IdMesa = Convert.ToInt32(cmdInsert.ExecuteScalar());
                    }
                }

                // Asignar los valores actualizados/nuevos a las propiedades públicas
                Mesa = txtNombre.Text.Trim();
                CantidadPersonas = int.Parse(txtUbicacion.Text);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void txtUbicacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}