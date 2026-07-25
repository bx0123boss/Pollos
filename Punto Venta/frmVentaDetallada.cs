using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace Punto_Venta
{
    public partial class frmVentaDetallada : Form
    {
        public string idMesero;
        public double total, utilidad;
        public string usuario = "";
        public string IdMesa;
        public string idCliente = "0";

        public frmVentaDetallada()
        {
            InitializeComponent();
            this.MinimumSize = new Size(750, 650);
        }

        private void frmVentaDetallada_Load(object sender, EventArgs e)
        {
            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();
                DataSet ds = new DataSet();
                string query = @"SELECT A.IdInventario, A.IdArticulosFolio, A.Cantidad,
                                CASE 
                                    WHEN A.IdInventario = 0 THEN P.Nombre 
                                    ELSE B.Nombre 
                                END AS Nombre,
                                CASE 
                                    WHEN A.IdInventario = 0 THEN P.Precio 
                                    ELSE B.Precio 
                                END AS Precio, A.Total, A.Comentario , A.IdExtra as Ids, A.IdPromo
                                FROM ArticulosFolio A
                                INNER JOIN INVENTARIO B ON A.IdInventario = B.IdInventario
                                LEFT JOIN Promos P ON A.IdPromo = P.IdPromo
                                WHERE IdFolio = @Folio;";

                using (SqlDataAdapter da = new SqlDataAdapter(query, conectar))
                {
                    da.SelectCommand.Parameters.AddWithValue("@Folio", lblFolio.Text);
                    da.Fill(ds, "IdFolio");
                    dataGridView1.DataSource = ds.Tables["IdFolio"];
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns["IdArticulosFolio"].Visible = false;
                    dataGridView1.Columns["Ids"].Visible = false;
                    dataGridView1.Columns["IdPromo"].Visible = false;
                }
                if (idCliente != "0")
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Clientes WHERE IdCliente= @IdCliente;", conectar))
                    {
                        cmd.Parameters.AddWithValue("@IdCliente", idCliente);

                        using (SqlDataReader sqlDataReader = cmd.ExecuteReader())
                        {
                            if (sqlDataReader.Read())
                            {
                                lblNombre.Text = sqlDataReader["Nombre"].ToString();
                                string telefono = sqlDataReader["Telefono"].ToString();
                                string telefonoFormateado = $"({telefono.Substring(0, 3)}) {telefono.Substring(3, 3)}-{telefono.Substring(6, 4)}";
                                lblTelefono.Text = telefonoFormateado;
                                lblDirección.Text = sqlDataReader["Direccion"].ToString();
                                lblColonia.Text = sqlDataReader["Colonia"].ToString();
                                gbClientes.Visible = true;
                            }
                        }

                    }
                }
            }
            dataGridView1.Columns[0].Visible = false;
            lblMonto.Text = $"{total:C}";
            lblUtilidad.Text = $"{utilidad:C}";

        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Producto> productos = new List<Producto>();

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                productos.Add(new Producto
                {
                    Nombre = dataGridView1[3, i].Value.ToString(),
                    Cantidad = Convert.ToDouble(dataGridView1[2, i].Value.ToString()),
                    PrecioUnitario = Convert.ToDouble(dataGridView1[5, i].Value.ToString()) / Convert.ToDouble(dataGridView1[2, i].Value.ToString()),
                    Total = Convert.ToDouble(dataGridView1[5, i].Value.ToString()),
                });
            }

            string GetNumericValue(string input)
            {
                return Regex.Replace(input, @"[^\d.-]", "");
            }

            double total = Convert.ToDouble(GetNumericValue(lblMonto.Text));
            Dictionary<string, double> totales = new Dictionary<string, double>();
            totales.Add("Subtotal", total / 1.16);
            totales.Add("IVA", (total / 1.16) * 0.16);
            totales.Add("Total", total);

            TicketPrinter ticketPrinter = new TicketPrinter(
                   Conexion.datosTicket,
                   Conexion.pieDeTicket,
                   Conexion.logoPath,
                   productos,
                   lblFolio.Text,
                   "",       // Datos Extra
                   "",
                   Convert.ToDouble(lblMonto.Text),
                   false,
                   totales,
                   "");

            ticketPrinter.ImprimirTicket();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult confirmacion = MessageBox.Show(
                $"¿Está seguro de que desea CANCELAR el Folio {lblFolio.Text}?\nEsta acción revertirá los movimientos en caja y repondrá el inventario.",
                "Confirmar Cancelación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmacion != DialogResult.Yes) return;

            double ventas = 0;
            int mesas = 0;

            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();
                SqlTransaction transaccion = conectar.BeginTransaction();

                try
                {
                    // 1. Consultar estadísticas actuales del mesero
                    using (SqlCommand cmd = new SqlCommand("SELECT Ventas, Mesas FROM Usuarios WHERE IdUsuario = @IdMesero;", conectar, transaccion))
                    {
                        cmd.Parameters.AddWithValue("@IdMesero", idMesero);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                ventas = Convert.ToDouble(reader["Ventas"]);
                                mesas = Convert.ToInt32(reader["Mesas"]);
                            }
                        }
                    }

                    ventas -= total;
                    mesas = Math.Max(0, mesas - 1);

                    // 2. REVERTIR PAGOS EN LA TABLA CORTE (Soporte Pago Mixto)
                    // Consultamos los desgloces guardados en VentasPagos para este folio
                    List<Tuple<string, double>> desgloses = new List<Tuple<string, double>>();
                    string queryGetPagos = "SELECT MetodoPago, Monto FROM VentasPagos WHERE IdFolio = @IdFolio";

                    using (SqlCommand cmdPagos = new SqlCommand(queryGetPagos, conectar, transaccion))
                    {
                        cmdPagos.Parameters.AddWithValue("@IdFolio", lblFolio.Text);
                        using (SqlDataReader reader = cmdPagos.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string metodo = reader["MetodoPago"].ToString();
                                double monto = Convert.ToDouble(reader["Monto"]);
                                desgloses.Add(new Tuple<string, double>(metodo, monto));
                            }
                        }
                    }

                    // Si se encontraron desglices mixtos/específicos, revertimos cada uno en CORTE
                    if (desgloses.Count > 0)
                    {
                        string queryCorteNegativo = @"INSERT INTO CORTE (Concepto, Total, FechaHora, FormaPago) 
                                              VALUES (@Concepto, @Total, GETDATE(), @FormaPago)";

                        foreach (var pago in desgloses)
                        {
                            using (SqlCommand cmdReversa = new SqlCommand(queryCorteNegativo, conectar, transaccion))
                            {
                                cmdReversa.Parameters.AddWithValue("@Concepto", $"CANCELACION DE FOLIO: {lblFolio.Text} por {usuario} [{pago.Item1}]");
                                cmdReversa.Parameters.AddWithValue("@Total", pago.Item2 * -1); // Valor negativo para contrarrestar
                                cmdReversa.Parameters.AddWithValue("@FormaPago", pago.Item1);
                                cmdReversa.ExecuteNonQuery();
                            }
                        }
                    }
                    else
                    {
                        // Resguardo en caso de que sea una venta antigua sin registro en VentasPagos
                        string queryCorteGenerico = @"INSERT INTO CORTE (Concepto, Total, FechaHora, FormaPago) 
                                              VALUES (@Concepto, @Total, GETDATE(), 'CANCELADO')";

                        using (SqlCommand cmdReversa = new SqlCommand(queryCorteGenerico, conectar, transaccion))
                        {
                            cmdReversa.Parameters.AddWithValue("@Concepto", $"CANCELACION DE FOLIO: {lblFolio.Text} por {usuario}");
                            cmdReversa.Parameters.AddWithValue("@Total", total * -1);
                            cmdReversa.ExecuteNonQuery();
                        }
                    }

                    // 3. Marcar Folio como CANCELADO
                    using (SqlCommand cmd = new SqlCommand("UPDATE Folios SET Estatus='CANCELADO', Utilidad = 0 WHERE IdFolio = @IdFolio;", conectar, transaccion))
                    {
                        cmd.Parameters.AddWithValue("@IdFolio", lblFolio.Text);
                        cmd.ExecuteNonQuery();
                    }

                    // 4. Actualizar ventas acumuladas del mesero
                    using (SqlCommand cmd = new SqlCommand("UPDATE Usuarios SET Ventas = @Ventas, Mesas = @Mesas WHERE IdUsuario = @IdMesero;", conectar, transaccion))
                    {
                        cmd.Parameters.AddWithValue("@Ventas", ventas);
                        cmd.Parameters.AddWithValue("@Mesas", mesas);
                        cmd.Parameters.AddWithValue("@IdMesero", idMesero);
                        cmd.ExecuteNonQuery();
                    }

                    // 5. Liberar/Cancelar estado de la Mesa y sus Artículos
                    if (!string.IsNullOrEmpty(IdMesa) && IdMesa != "0")
                    {
                        using (SqlCommand cmd = new SqlCommand("UPDATE MESAS SET Estatus = 'CANCELADO' WHERE IdMesa = @IdMesa;", conectar, transaccion))
                        {
                            cmd.Parameters.AddWithValue("@IdMesa", IdMesa);
                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = new SqlCommand("UPDATE ArticulosMesa SET Estatus = 'CANCELADO' WHERE IdMesa = @IdMesa;", conectar, transaccion))
                        {
                            cmd.Parameters.AddWithValue("@IdMesa", IdMesa);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    // 6. Recomponer el Inventario mediante tempInventario
                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                        string ide = null;
                        if (dataGridView1.Rows[i].Cells["Ids"].Value != null && dataGridView1.Rows[i].Cells["Ids"].Value.ToString().Length > 0)
                        {
                            ide = dataGridView1.Rows[i].Cells["Ids"].Value.ToString();
                        }

                        string insertTempInvQuery = @"INSERT INTO tempInventario(id, cantidad, ide) VALUES (@id, @cantidad, @ide);";
                        using (SqlCommand cmdTemp = new SqlCommand(insertTempInvQuery, conectar, transaccion))
                        {
                            cmdTemp.Parameters.AddWithValue("@id", dataGridView1.Rows[i].Cells["IdInventario"].Value?.ToString() == "0"
                                                                    ? dataGridView1.Rows[i].Cells["IdPromo"].Value
                                                                    : dataGridView1.Rows[i].Cells["IdInventario"].Value);
                            // Cantidad negativa para indicar devolución al stock
                            cmdTemp.Parameters.AddWithValue("@cantidad", Convert.ToDecimal(dataGridView1.Rows[i].Cells["Cantidad"].Value) * -1);
                            cmdTemp.Parameters.AddWithValue("@ide", (object)ide ?? DBNull.Value);

                            cmdTemp.ExecuteNonQuery();
                        }
                    }

                    // Confirmar transacción
                    transaccion.Commit();

                    MessageBox.Show("¡ORDEN CANCELADA CON ÉXITO!", "Comanda General", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    transaccion.Rollback();
                    MessageBox.Show($"Error al intentar cancelar el folio: {ex.Message}", "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
