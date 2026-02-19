
using System;
using System.Data;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;

namespace Punto_Venta
{
    public partial class frmActInventario : Form
    {

        public frmActInventario()
        {
            InitializeComponent();
        }
        private void frmActInventario_Load(object sender, EventArgs e)
        {
            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();
                DataSet ds = new DataSet();
                string query = @"SELECT * FROM tempInventario";

                using (SqlDataAdapter da = new SqlDataAdapter(query, conectar))
                {
                    da.Fill(ds, "IdFolio");
                    dgvMesa.DataSource = ds.Tables["IdFolio"];
                    dgvMesa.Columns[0].Visible = false;
                    if(dgvMesa.RowCount == 0)
                    {
                        MessageBox.Show("No hay articulos que actualizar por el momento", "INVENTARIO ACTUALIZADO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Close();
                    }
                }

            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DescontarInventario();          
        }
        public void DescontarInventario()
        {
            using (SqlConnection connection = new SqlConnection(Conexion.CadConSql))
            {
                connection.Open();

                List<TempInventario> tempInventarioList = GetTempInventario(connection);

                foreach (var tempInventario in tempInventarioList)
                {
                    if (tempInventario.Ide == null)
                    {
                        // Cuando ide es NULL, descuento directo desde INVENTARIO
                        DescontarDesdeInventario(connection, tempInventario.Id, tempInventario.Cantidad);
                    }
                    else
                    {
                        // Cuando ide no es NULL, separar los valores y descontar
                        DescontarDesdePromo(connection, tempInventario.Ide, tempInventario.Cantidad);
                    }
                }
                string query = "DELETE FROM tempInventario";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
                MessageBox.Show("INVENTARIO ACTUALIZADO CORRECTAMENTE", "INVENTARIO ACTUALIZADO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private List<TempInventario> GetTempInventario(SqlConnection connection)
        {
            List<TempInventario> tempInventarioList = new List<TempInventario>();

            string query = "SELECT id, cantidad, ide FROM tempInventario";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tempInventarioList.Add(new TempInventario
                        {
                            Id = reader.GetString(0),
                            Cantidad = reader.GetDecimal(1),
                            Ide = reader.IsDBNull(2) ? null : reader.GetString(2)
                        });
                    }
                }
            }

            return tempInventarioList;
        }

        private void DescontarDesdeInventario(SqlConnection connection, string idInventario, decimal cantidad)
        {
            // Obtener los productos asociados al IdInventario
            string query = @"
            SELECT IdProducto1, CantidadProducto1 FROM INVENTARIO WHERE IdInventario = @IdInventario AND IdProducto1 IS NOT NULL
            UNION ALL
            SELECT IdProducto2, CantidadProducto2 FROM INVENTARIO WHERE IdInventario = @IdInventario AND IdProducto2 IS NOT NULL
            UNION ALL
            SELECT IdProducto3, CantidadProducto3 FROM INVENTARIO WHERE IdInventario = @IdInventario AND IdProducto3 IS NOT NULL
            UNION ALL
            SELECT IdProducto4, CantidadProducto4 FROM INVENTARIO WHERE IdInventario = @IdInventario AND IdProducto4 IS NOT NULL
            UNION ALL
            SELECT IdProducto5, CantidadProducto5 FROM INVENTARIO WHERE IdInventario = @IdInventario AND IdProducto5 IS NOT NULL
            UNION ALL
            SELECT IdProducto6, CantidadProducto6 FROM INVENTARIO WHERE IdInventario = @IdInventario AND IdProducto6 IS NOT NULL
            UNION ALL
            SELECT IdProducto7, CantidadProducto7 FROM INVENTARIO WHERE IdInventario = @IdInventario AND IdProducto7 IS NOT NULL
            UNION ALL
            SELECT IdProducto8, CantidadProducto8 FROM INVENTARIO WHERE IdInventario = @IdInventario AND IdProducto8 IS NOT NULL
            UNION ALL
            SELECT IdProducto9, CantidadProducto9 FROM INVENTARIO WHERE IdInventario = @IdInventario AND IdProducto9 IS NOT NULL
            UNION ALL
            SELECT IdProducto10, CantidadProducto10 FROM INVENTARIO WHERE IdInventario = @IdInventario AND IdProducto10 IS NOT NULL";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@IdInventario", idInventario);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int idProducto = reader.GetInt32(0);
                        decimal cantidadProducto = reader.GetDecimal(1);

                        // Calcular la cantidad a descontar
                        decimal cantidadDescontar = cantidadProducto * cantidad;
                       
                        // Actualizar PRODUCTOS
                        ActualizarProducto(connection, idProducto, cantidadDescontar);
                    }
                }
            }
        }

        private void DescontarDesdePromo(SqlConnection connection, string ide, decimal cantidad)
        {
            // Separar los pares Cantidad,IdInventario
            var pares = ide.Split(';')
                           .Select(p => p.Split(','))
                           .Where(p => p.Length == 2)
                           .Select(p => new
                           {
                               Cantidad = decimal.Parse(p[0]),
                               IdInventario = (p[1]).ToString()
                           });

            foreach (var par in pares)
            {
                DescontarDesdeInventario(connection, par.IdInventario, par.Cantidad * cantidad);
            }
        }

        private void ActualizarProducto(SqlConnection connection, int idProducto, decimal cantidadDescontar)
        {
            string query = "UPDATE PRODUCTOS SET Cantidad = Cantidad - @CantidadDescontar WHERE IdProducto = @IdProducto";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@CantidadDescontar", cantidadDescontar);
                command.Parameters.AddWithValue("@IdProducto", idProducto);
                command.ExecuteNonQuery();
            }
        }
    }

    public class TempInventario
    {
        public string Id { get; set; }
        public decimal Cantidad { get; set; }
        public string Ide { get; set; }
    }
}
       