using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Punto_Venta
{
    public partial class frmAgregarOrigen : Form
    {
        public frmAgregarOrigen()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open(); // Abrir la conexión

                // Validar si el origen ya existe
                using (SqlCommand cmdVerificar = new SqlCommand("SELECT COUNT(*) FROM Origen WHERE Nombre = @Nombre;", conectar))
                {
                    cmdVerificar.Parameters.AddWithValue("@Nombre", txtNombre.Text);

                    // Ejecutar la consulta de validación
                    int count = Convert.ToInt32(cmdVerificar.ExecuteScalar());

                    if (count > 0)
                    {
                        MessageBox.Show("El origen ya existe en la base de datos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return; // Salir si el origen ya existe
                    }
                }
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Origen (Nombre) VALUES (@Nombre);", conectar))
                {
                    cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Se ha creado el Origen con éxito", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmOrigen apart = new frmOrigen();
                    apart.Show();
                    this.Close();
                }
            } // La conexión se cierra automáticamente aquí
        }
    }
}
