using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Punto_Venta
{
    public partial class frmAjustarFolios : Form
    {
        decimal total;
        int numFolios;
        DateTime fechaInicio, fechaFin;
        public class Folio
        {
            public int Id { get; set; }
            public List<Producto> Productos { get; set; } = new List<Producto>();
            public DateTime fechaHora { get; set; }
            public DateTime FechaHora { get; set; }
            public int NoPersonas { get; set; }
            public string Mesa { get; set; }
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
                DataTable dt = new DataTable();

                conectar.Open();
                string query = @"SELECT * FROM turnos
                                WHERE cierre >= @StartDate AND cierre <= @EndDate";
                using (SqlCommand cmd = new SqlCommand(query, conectar))
                {
                    cmd.Parameters.AddWithValue("@StartDate", dateTimePicker1.Value.Date);
                    cmd.Parameters.AddWithValue("@EndDate", dateTimePicker2.Value.Date.AddDays(1).AddSeconds(-1));

                    // Construir la consulta SQL con los valores de los parámetros
                    string debugQuery = query;
                    foreach (SqlParameter p in cmd.Parameters)
                    {
                        debugQuery = debugQuery.Replace(p.ParameterName, $"'{p.Value.ToString()}'");
                    }

                    // Imprimir la consulta en la consola
                    Console.WriteLine("Consulta SQL:");
                    Console.WriteLine(debugQuery);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    comboBox1.DisplayMember = "idturno";
                    comboBox1.ValueMember = "idturnointerno";
                    comboBox1.DataSource = dt;

                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            decimal cantidad;

            // Intentar convertir el texto del TextBox a decimal
            bool esValido = decimal.TryParse(txtTotal.Text, out cantidad);

            // Verificar si la conversión fue exitosa
            if (!esValido)
                return;
            // Obtener la lista de productos desde la base de datos
            List<Producto> productos = ObtenerProductosDesdeBaseDeDatos();


            List<Folio> folios = new List<Folio>();

            Random random = new Random();
            decimal montoPorFolio = cantidad / numFolios;

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
            if (sumaTotal != cantidad)
            {
                var diferencia = cantidad - sumaTotal;
                folios.Last().Productos.Add(new Producto { Nombre = "Ajuste", Precio = diferencia });
            }

            // Mostrar los folios
            InsertarEnBaseJaegersoft(folios);
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
        private List<Folio> ObtenerFoliosDesdeBaseDeDatos()
        {
            List<Folio> folios = new List<Folio>();

            // Consulta SQL para obtener los folios
            string query = "SELECT folio, cierre, nopersonas, mesa FROM cheques " +
                "WHERE pagado = 1 and fecha>= @StartDate and fecha<= @EndDate";

            using (SqlConnection conectar = new SqlConnection(Conexion.CadConRestaurantSoft))
            {
                conectar.Open();
                using (SqlCommand cmd = new SqlCommand(query, conectar))
                {
                    cmd.Parameters.AddWithValue("@StartDate", fechaInicio);
                    cmd.Parameters.AddWithValue("@EndDate", fechaFin);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Folio folio = new Folio
                            {
                                Id = reader.GetInt32(0),
                                FechaHora = reader.GetDateTime(1),
                                NoPersonas = reader.GetInt32(2),
                                Mesa = reader.GetString(3)
                            };
                            folios.Add(folio);
                        }
                    }
                }

                return folios;
            }
        }
        private void InsertarEnBaseJaegersoft(List<Folio> folios)
        {
            // Asumo que ya tienes una conexión configurada
            using (SqlConnection connection = new SqlConnection(Conexion.CadConSql))
            {
                connection.Open();

                foreach (var folio in folios)
                {
                    // Insertar el folio en la tabla Folios
                    string insertFolioQuery = @"
                INSERT INTO Folios (ModalidadVenta, Estatus, FechaHora, Total, Descuento, Utilidad, IdMesa)
                VALUES (@ModalidadVenta, @Estatus, @FechaHora, @Total, @Descuento, @Utilidad, @IdMesa);
                SELECT SCOPE_IDENTITY();"; // Obtener el IdFolio generado
                    /*
                    using (SqlCommand folioCommand = new SqlCommand(insertFolioQuery, connection))
                    {
                        folioCommand.Parameters.AddWithValue("@ModalidadVenta", "MESA");
                        folioCommand.Parameters.AddWithValue("@Estatus", "COBRADO");
                        folioCommand.Parameters.AddWithValue("@FechaHora", folio.fechaHora);
                        folioCommand.Parameters.AddWithValue("@Total", folio.Total);
                        folioCommand.Parameters.AddWithValue("@Descuento", folio.Descuento);
                        folioCommand.Parameters.AddWithValue("@Utilidad", folio.Utilidad);
                        folioCommand.Parameters.AddWithValue("@IdMesa", folio.IdMesa);

                        // Ejecutar la consulta y obtener el IdFolio generado
                        int idFolio = Convert.ToInt32(folioCommand.ExecuteScalar());

                        // Insertar los productos asociados al folio en la tabla ArticulosFolio
                        foreach (var producto in folio.Productos)
                        {
                            string insertProductoQuery = @"
                        INSERT INTO ArticulosFolio (IdInventario, IdFolio, Cantidad, Comentario, Total, IdExtra, IdPromo)
                        VALUES (@IdInventario, @IdFolio, @Cantidad, @Comentario, @Total, @IdExtra, @IdPromo);";

                            using (SqlCommand productoCommand = new SqlCommand(insertProductoQuery, connection))
                            {
                                productoCommand.Parameters.AddWithValue("@IdInventario", producto.IdInventario);
                                productoCommand.Parameters.AddWithValue("@IdFolio", idFolio); // IdFolio generado
                                productoCommand.Parameters.AddWithValue("@Cantidad", producto.Cantidad);
                                productoCommand.Parameters.AddWithValue("@Comentario", producto.Comentario ?? (object)DBNull.Value); // Manejo de nulos
                                productoCommand.Parameters.AddWithValue("@Total", producto.Total);
                                productoCommand.Parameters.AddWithValue("@IdExtra", producto.IdExtra ?? (object)DBNull.Value); // Manejo de nulos
                                productoCommand.Parameters.AddWithValue("@IdPromo", producto.IdPromo ?? (object)DBNull.Value); // Manejo de nulos

                                productoCommand.ExecuteNonQuery();
                            }
                        }
                    }
                    */
                }
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

        private void txtTotal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // Solo permitir un punto decimal
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SqlConnection conectar = new SqlConnection(Conexion.CadConRestaurantSoft))
            {
                DataTable dt = new DataTable();

                conectar.Open();
                string query = @"SELECT * FROM turnos
                                WHERE idturnointerno= @turno";
                using (SqlCommand cmd = new SqlCommand(query, conectar))
                {
                    cmd.Parameters.AddWithValue("@turno", comboBox1.SelectedValue);
                    // Construir la consulta SQL con los valores de los parámetros
                    string debugQuery = query;
                    foreach (SqlParameter p in cmd.Parameters)
                    {
                        debugQuery = debugQuery.Replace(p.ParameterName, $"'{p.Value.ToString()}'");
                    }

                    // Imprimir la consulta en la consola
                    Console.WriteLine("Consulta SQL:");
                    Console.WriteLine(debugQuery);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            fechaInicio = (DateTime)reader["apertura"];
                            fechaFin = (DateTime)reader["cierre"];
                        }

                    }
                }
                query = @"SELECT 
                            SUM(total) AS suma_total,
                            COUNT(folio) AS cantidad_folios  
                            FROM cheques
                            WHERE pagado = 1 and fecha>=@StartDate and fecha<=@EndDate";
                using (SqlCommand cmd = new SqlCommand(query, conectar))
                {
                    cmd.Parameters.AddWithValue("@StartDate", fechaInicio);
                    cmd.Parameters.AddWithValue("@EndDate", fechaFin);
                    // Construir la consulta SQL con los valores de los parámetros
                    string debugQuery = query;
                    foreach (SqlParameter p in cmd.Parameters)
                    {
                        debugQuery = debugQuery.Replace(p.ParameterName, $"'{p.Value.ToString()}'");
                    }

                    // Imprimir la consulta en la consola
                    Console.WriteLine("Consulta SQL:");
                    Console.WriteLine(debugQuery);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            numFolios = int.Parse(reader["cantidad_folios"].ToString());
                            total = decimal.Parse(reader["suma_total"].ToString());
                        }
                        txtTotal.Text = total.ToString();
                        lblTotal.Text = $"{total:C}";
                        lblFolios.Text = numFolios.ToString();

                    }
                }
            }
        }
    }
}