using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Punto_Venta;

namespace FastFoodWeb.Services
{
    public class ServicioImpresion
    {
        public ServicioImpresion()
        {
        }

        // ==========================================
        // 1. MÉTODO PARA IMPRIMIR COMANDA (COCINA / BARRA)
        // ==========================================
        public void ImprimirComandaCocina(
            List<(string id, string Cantidad, string Descripcion, string Comentario, string ides)> itemsComanda,
            string mesa,
            string mesero,
            string impresoraDestino = null) // 👈 Ahora acepta la impresora dinámicamente
        {
            if (itemsComanda == null || itemsComanda.Count == 0) return;

            List<Producto> productosParaImprimir = new List<Producto>();

            foreach (var (id, cantidadStr, descripcion, comentario, ide) in itemsComanda)
            {
                // Manejo seguro de la cantidad
                double.TryParse(cantidadStr, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double cantidad);
                if (cantidad <= 0) cantidad = 1;

                // 1.1 Agregar el producto principal
                productosParaImprimir.Add(new Producto
                {
                    Nombre = descripcion,
                    Cantidad = cantidad,
                    Comentario = comentario,
                    PrecioUnitario = 0,
                    Total = 0
                });

                // 1.2 Desglosar los extras/mitades si los hay
                if (!string.IsNullOrWhiteSpace(ide) && (id.StartsWith("C") || id.StartsWith("P") || !string.IsNullOrEmpty(ide)))
                {
                    string[] idsExtras = ide.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var extra in idsExtras)
                    {
                        string[] detallesId = extra.Split(',');
                        if (detallesId.Length >= 2)
                        {
                            double.TryParse(detallesId[0], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double subCantidad);
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

            // 👈 Si no se especifica impresora, usa la de cocina por defecto (impresora2)
            string targetPrinter = string.IsNullOrEmpty(impresoraDestino) ? Conexion.impresora2 : impresoraDestino;

            ticket.ImprimirComanda(targetPrinter);
        }

        // ==========================================
        // 2. MÉTODO PARA IMPRIMIR TICKET DE CLIENTE
        // ==========================================
        public void ImprimirTicketVenta(string folio, string mesa, string mesero, double total, Dictionary<string, double> totales, string formaPago, List<Producto> productosParaImprimir)
        {
            string[] encabezados = Conexion.datosTicket;
            string[] pieDePagina = Conexion.pieDeTicket;
            string logoPath = @"C:\Jaeger Soft\LOGO.png";

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