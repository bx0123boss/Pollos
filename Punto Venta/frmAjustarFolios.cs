using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Punto_Venta
{
    public partial class frmAjustarFolios : Form
    {
        decimal total;
        int numFolios;
        public class Folio
        {
            public int Id { get; set; }
            public List<Producto> Productos { get; set; } = new List<Producto>();
            public decimal Total => Productos.Sum(p => p.Precio);
        }
        public class Producto
        {
            public int IdProducto { get; set; }
            public string Nombre { get; set; }
            public decimal Precio { get; set; }
        }
        public frmAjustarFolios()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection conectar = new SqlConnection(Conexion.CadConRestaurantSoft))
            {
                conectar.Open();
                string query = @"SELECT 
                                    SUM(total) AS suma_total,
                                    COUNT(folio) AS cantidad_folios
                                FROM 
                                    cheques
                                WHERE pagado = 1 AND fecha >= @StartDate AND fecha <= @EndDate";
                using (SqlCommand cmd = new SqlCommand(query, conectar))
                {
                    cmd.Parameters.AddWithValue("@StartDate", dateTimePicker1.Value.Date);
                    cmd.Parameters.AddWithValue("@EndDate", dateTimePicker1.Value.Date.AddDays(1).AddSeconds(-1));
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            numFolios = int.Parse(reader["cantidad_folios"].ToString());
                            total = decimal.Parse(reader["suma_total"].ToString());
                        }

                    }
                    lblTotal.Text = $"{total:C}";
                    lblFolios.Text = numFolios.ToString();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Obtener la lista de productos desde la base de datos
            List<Producto> productos = ObtenerProductosDesdeBaseDeDatos();

            
            List<Folio> folios = new List<Folio>();

            Random random = new Random();
            decimal montoPorFolio = total / numFolios;

            for (int i = 0; i < numFolios; i++)
            {
                Folio folio = new Folio { Id = i + 1 };
                decimal montoActual = 0;

                while (montoActual < montoPorFolio)
                {
                    // Filtrar productos que no excedan el monto restante
                    var productosValidos = productos.Where(p => p.Precio <= (montoPorFolio - montoActual)).ToList();

                    if (productosValidos.Count == 0)
                    {
                        // Si no hay productos válidos, salir del bucle
                        break;
                    }

                    // Seleccionar un producto aleatorio de los válidos
                    var producto = productosValidos[random.Next(productosValidos.Count)];
                    folio.Productos.Add(producto);
                    montoActual += producto.Precio;
                }

                folios.Add(folio);
            }

            // Ajustar el último folio para que la suma total sea exacta
            decimal sumaTotal = folios.Sum(f => f.Total);
            if (sumaTotal != total)
            {
                var diferencia = total - sumaTotal;
                folios.Last().Productos.Add(new Producto { Nombre = "Ajuste", Precio = diferencia });
            }

            // Mostrar los folios
            foreach (var folio in folios)
            {
                Console.WriteLine($"Folio {folio.Id}:");
                foreach (var producto in folio.Productos)
                {
                    Console.WriteLine($"  {producto.Nombre} - {producto.Precio:C}");
                }
                Console.WriteLine($"Total: {folio.Total:C}");
                Console.WriteLine();
            }
        }
        public static List<Producto> ObtenerProductosDesdeBaseDeDatos()
        {
            List<Producto> productos = new List<Producto>();

            using (SqlConnection connection = new SqlConnection(Conexion.CadConRestaurantSoft))
            {
                string query = @"
                SELECT 
                    A.idproducto,
                    A.descripcion,
                    B.precio
                FROM productos A
                INNER JOIN productosdetalle B ON A.idproducto = B.idproducto WHERE idgrupo NOT IN(51,23)"; // Consulta SQL proporcionada

                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Producto producto = new Producto
                        {
                            IdProducto = Convert.ToInt32(reader["idproducto"]),
                            Nombre = reader["descripcion"].ToString(),
                            Precio = Convert.ToDecimal(reader["precio"])
                        };
                        productos.Add(producto);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al consultar la base de datos: " + ex.Message);
                }
            }

            return productos;
        }
    }
}
