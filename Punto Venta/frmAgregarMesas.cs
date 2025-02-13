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
    public partial class frmAgregarMesas : Form
    {
        OleDbConnection conectar = new OleDbConnection(Conexion.CadCon);
        OleDbCommand cmd; 
        public frmAgregarMesas()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            bool existe = false;

            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();

                // Primera operación: Verificar si la mesa ya existe
                using (SqlCommand cmd = new SqlCommand("SELECT Nombre FROM Mesas WHERE Nombre = @Nombre;", conectar))
                {
                    // Usar parámetros para evitar SQL Injection
                    cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            existe = true;
                        }
                    } // El DataReader se cierra automáticamente aquí
                }

                if (existe)
                {
                    MessageBox.Show("Existe una mesa similar, favor de verificar", "Agregar Mesas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    // Segunda operación: Insertar la nueva mesa
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Mesas (Nombre, IdMesero, CantidadPersonas,Impresion) VALUES (@Nombre, @IdMesero, @CantidadPersonas,@Impresion);", conectar))
                    {
                        // Usar parámetros para evitar SQL Injection
                        cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                        cmd.Parameters.AddWithValue("@IdMesero", 1);
                        cmd.Parameters.AddWithValue("@CantidadPersonas", txtUbicacion.Text);
                        cmd.Parameters.AddWithValue("@Impresion", 0);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("¡Se ha agregado la mesa con éxito!", "Agregar Mesas", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Close();
                    }
                }
            } // La conexión se cierra automáticamente aquí
        }
    }
}
