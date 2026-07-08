using System;
using Microsoft.Data.SqlClient;
using Punto_Venta;
namespace FastFoodWeb.Services
{
    public class ServicioImpresion
    {
        // Ya no necesitamos inyectar configuración local, todo viene de Conexion.cs
        public ServicioImpresion()
        {
        }

        // ==========================================
        // 1. MÉTODO PARA IMPRIMIR COMANDA (COCINA)
        // ==========================================
        public void ImprimirComandaCocina(List<(string id, string Cantidad, string Descripcion, string Comentario, string ides)> itemsComanda, string mesa, string mesero)
        {
            List<Producto> productosParaImprimir = new List<Producto>();

            foreach (var (id, cantidad, descripcion, comentario, ide) in itemsComanda)
            {
                // 1.1 Agregar el producto principal
                productosParaImprimir.Add(new Producto
                {
                    Nombre = descripcion,
                    Cantidad = Convert.ToDouble(cantidad),
                    Comentario = comentario,
                    PrecioUnitario = 0,
                    Total = 0
                });

                // 1.2 Desglosar los extras/mitades si los hay
                if ((id.StartsWith("C") || id.StartsWith("P") || !string.IsNullOrEmpty(ide)) && !string.IsNullOrWhiteSpace(ide))
                {
                    string[] idsExtras = ide.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var extra in idsExtras)
                    {
                        string[] detallesId = extra.Split(',');
                        if (detallesId.Length >= 2)
                        {
                            double subCantidad = Convert.ToDouble(detallesId[0]);
                            string idInventario = detallesId[1];
                            string nombreExtra = ObtenerNombreProducto(idInventario);

                            if (!string.IsNullOrEmpty(nombreExtra))
                            {
                                productosParaImprimir.Add(new Producto
                                {
                                    Nombre = "  -> " + nombreExtra,
                                    Cantidad = subCantidad,
                                    Comentario = "",
                                    PrecioUnitario = 0,
                                    Total = 0
                                });
                            }
                        }
                    }
                }
            }

            TicketPrinter ticket = new TicketPrinter(productosParaImprimir, mesa, mesero);

            ticket.ImprimirComanda(Conexion.impresora2);
        }

        // ==========================================
        // 2. MÉTODO PARA IMPRIMIR TICKET DE CLIENTE
        // ==========================================
        public void ImprimirTicketVenta(string folio, string mesa, string mesero, double total, Dictionary<string, double> totales, string formaPago, List<Producto> productosParaImprimir)
        {
            // Tomamos los datos compartidos de WinForms!
            string[] encabezados = Conexion.datosTicket;
            string[] pieDePagina = Conexion.pieDeTicket;

            // Puedes agregar el "logoPath" a Conexion.cs en un futuro, por ahora usamos ruta física:
            string logoPath = @"C:\Jaeger Soft\LOGO.png"; // Ajusta a la ruta de tu logo en la PC Servidor

            TicketPrinter ticket = new TicketPrinter(
                encabezados,
                pieDePagina,
                logoPath,
                productosParaImprimir,
                folio,
                mesa,
                mesero,
                total,
                false,
                totales,
                formaPago
            );

            // Usamos impresora1 de tu archivo Conexion.cs (la de mostrador/caja)
            ticket.ImprimirTicket(Conexion.impresora);
        }

        // ==========================================
        // MÉTODO AUXILIAR DE BASE DE DATOS
        // ==========================================
        private string ObtenerNombreProducto(string idInventario)
        {
            string nombre = "";
            try
            {
                // Usamos la cadena de conexión de SQL de tu clase compartida
                using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
                {
                    conectar.Open();
                    string query = "SELECT Nombre FROM Inventario WHERE IdInventario = @Id";
                    using (SqlCommand cmd = new SqlCommand(query, conectar))
                    {
                        cmd.Parameters.AddWithValue("@Id", idInventario);
                        var result = cmd.ExecuteScalar();
                        if (result != null) nombre = result.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al consultar BD para impresión: " + ex.Message);
            }
            return nombre;
        }
    }
}