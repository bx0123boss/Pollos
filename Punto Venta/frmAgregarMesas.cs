using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Punto_Venta
{
    public partial class frmAgregarMesas : Form
    {
        public int IdMesa { get; set; }
        public int IdMesero { get; set; }
        public string Mesa { get; set; }
        public int CantidadPersonas { get; set; }
       
        public frmAgregarMesas()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtNombre.Text) && !String.IsNullOrEmpty(txtUbicacion.Text))
            {
                bool existe = false;

                using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
                {
                    conectar.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT Nombre FROM Mesas WHERE Nombre = @Nombre AND Estatus = 'COCINA';", conectar))
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
                    if (existe)
                    {
                        MessageBox.Show("Existe una mesa similar, favor de verificar", "Agregar Mesas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        string query = "INSERT INTO Mesas (Nombre, IdMesero, CantidadPersonas,Impresion,Estatus) " +
                                             "VALUES (@Nombre, @IdMesero, @CantidadPersonas,@Impresion,@Estatus); " +
                                             "SELECT SCOPE_IDENTITY();"; // Obtener el último ID insertado

                        using (SqlCommand cmd = new SqlCommand(query, conectar))
                        {
                            // Usar parámetros para evitar SQL Injection
                            cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                            cmd.Parameters.AddWithValue("@IdMesero", IdMesero);
                            cmd.Parameters.AddWithValue("@CantidadPersonas", txtUbicacion.Text);
                            cmd.Parameters.AddWithValue("@Impresion", 0);
                            cmd.Parameters.AddWithValue("@Estatus", "NUEVA");
                            int lastIdFolio = Convert.ToInt32(cmd.ExecuteScalar());
                            //MessageBox.Show("¡Se ha agregado la mesa con éxito!", "Agregar Mesas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            IdMesa = lastIdFolio;
                            Mesa = txtNombre.Text;
                            CantidadPersonas = int.Parse(txtUbicacion.Text);
                            this.DialogResult = System.Windows.Forms.DialogResult.OK;
                        }
                    }
                } 
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
