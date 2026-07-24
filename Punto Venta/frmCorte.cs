using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Punto_Venta
{
    public partial class frmCorte : Form
    {
        private double entradasEfectivo = 0;
        private double salidasEfectivo = 0;
        private double ventasTarjeta = 0;
        public string usuario = "";

        public frmCorte()
        {
            InitializeComponent();
            this.MinimumSize = new Size(1024, 720);
        }

        private void frmCorte_Load(object sender, EventArgs e)
        {
            CargarDatos();
            CalcularTotales();
            AplicarEstilosGrillas();
        }

        private void CargarDatos()
        {
            try
            {
                using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
                {
                    conectar.Open();

                    // Carga de movimientos de caja
                    using (SqlDataAdapter da = new SqlDataAdapter("SELECT Concepto, Total, FormaPago, FechaHora FROM CORTE ORDER BY FechaHora DESC;", conectar))
                    {
                        DataTable dtCorte = new DataTable();
                        da.Fill(dtCorte);
                        dgvCorte.DataSource = dtCorte;
                    }

                    // Carga de desempeño por mesero
                    using (SqlDataAdapter da = new SqlDataAdapter("SELECT IdUsuario, Usuario AS Mesero, Ventas, Mesas AS MesasAtendidas FROM Usuarios WHERE Ventas > 0 OR Mesas > 0;", conectar))
                    {
                        DataTable dtMeseros = new DataTable();
                        da.Fill(dtMeseros);
                        dgvMeseros.DataSource = dtMeseros;
                        if (dgvMeseros.Columns.Contains("IdUsuario"))
                            dgvMeseros.Columns["IdUsuario"].Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los datos del corte: {ex.Message}", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalcularTotales()
        {
            entradasEfectivo = 0;
            salidasEfectivo = 0;
            ventasTarjeta = 0;

            if (dgvCorte.DataSource is DataTable dt)
            {
                foreach (DataRow row in dt.Rows)
                {
                    double monto = Convert.ToDouble(row["Total"]);
                    string formaPago = row["FormaPago"]?.ToString() ?? "";

                    if (formaPago.Equals("Tarjeta", StringComparison.OrdinalIgnoreCase))
                    {
                        ventasTarjeta += monto;
                    }
                    else
                    {
                        if (monto >= 0)
                            entradasEfectivo += monto;
                        else
                            salidasEfectivo += monto; // acumulado negativo
                    }
                }
            }

            double corteNeto = entradasEfectivo + salidasEfectivo;

            lblCredito.Text = $"{ventasTarjeta:C2}";
            lblEntrada.Text = $"{entradasEfectivo:C2}";
            lblSalida.Text = $"{Math.Abs(salidasEfectivo):C2}";
            lblCorte.Text = $"{corteNeto:C2}";
        }

        private void AplicarEstilosGrillas()
        {
            EstilarGrillaPOS(dgvCorte);
            EstilarGrillaPOS(dgvMeseros);

            if (dgvCorte.Columns.Contains("Total"))
                dgvCorte.Columns["Total"].DefaultCellStyle.Format = "C2";

            if (dgvMeseros.Columns.Contains("Ventas"))
                dgvMeseros.Columns["Ventas"].DefaultCellStyle.Format = "C2";
        }

        private void EstilarGrillaPOS(DataGridView dgv)
        {
            dgv.EnableHeadersVisualStyles = false;
            dgv.BorderStyle = BorderStyle.None;
            dgv.BackgroundColor = Color.White;
            dgv.GridColor = Color.FromArgb(230, 233, 239);

            // Encabezados
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(40, 45, 54);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            dgv.ColumnHeadersHeight = 38;

            // Filas
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10.5F);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(226, 238, 255);
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            dgv.RowTemplate.Height = 32;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void btnCorte_Click(object sender, EventArgs e)
        {
            if (dgvCorte.Rows.Count == 0)
            {
                MessageBox.Show("No existen movimientos registrados para realizar el corte.", "Caja Vacía", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirmacion = MessageBox.Show(
                "¿Está seguro que desea CERRAR LA CAJA?\nEsta acción procesará el historial y reiniciará las ventas del día.",
                "Confirmación de Cierre de Caja",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                ProcesarCierreCaja();
            }
        }

        private void ProcesarCierreCaja()
        {
            List<Producto> productosTicket = new List<Producto>();
            double corteNeto = entradasEfectivo + salidasEfectivo;

            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();
                using (SqlTransaction transaccion = conectar.BeginTransaction())
                {
                    try
                    {
                        // 1. Insertar Historial del Corte
                        int idCorteInsertado;
                        string queryHistorial = @"INSERT INTO HistorialCortes (Monto, FechaHora) VALUES (@Monto, GETDATE());
                                                 SELECT SCOPE_IDENTITY();";

                        using (SqlCommand cmd = new SqlCommand(queryHistorial, conectar, transaccion))
                        {
                            cmd.Parameters.AddWithValue("@Monto", corteNeto);
                            idCorteInsertado = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        // 2. Insertar Detalle de Meseros
                        if (dgvMeseros.DataSource is DataTable dtMeseros)
                        {
                            string queryMeseros = @"INSERT INTO CortesMeseros(IdHistorialCortes, Mesero, Ventas, Mesas) 
                                                   VALUES (@IdCorte, @Mesero, @Ventas, @Mesas);";

                            foreach (DataRow row in dtMeseros.Rows)
                            {
                                using (SqlCommand cmd = new SqlCommand(queryMeseros, conectar, transaccion))
                                {
                                    cmd.Parameters.AddWithValue("@IdCorte", idCorteInsertado);
                                    cmd.Parameters.AddWithValue("@Mesero", row["Mesero"]?.ToString() ?? "");
                                    cmd.Parameters.AddWithValue("@Ventas", Convert.ToDouble(row["Ventas"]));
                                    cmd.Parameters.AddWithValue("@Mesas", Convert.ToInt32(row["MesasAtendidas"]));
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }

                        // 3. Insertar Detalle de Movimientos y preparar ticket
                        if (dgvCorte.DataSource is DataTable dtCorte)
                        {
                            string queryCorte = @"INSERT INTO CORTES(Concepto, Total, FormaPago, FechaHora, IdHistorialCortes) 
                                                 VALUES (@Concepto, @Total, @FormaPago, @FechaHora, @IdHistorialCortes);";

                            foreach (DataRow row in dtCorte.Rows)
                            {
                                using (SqlCommand cmd = new SqlCommand(queryCorte, conectar, transaccion))
                                {
                                    cmd.Parameters.AddWithValue("@Concepto", row["Concepto"]?.ToString() ?? "");
                                    cmd.Parameters.AddWithValue("@Total", Convert.ToDouble(row["Total"]));
                                    cmd.Parameters.AddWithValue("@FormaPago", row["FormaPago"]?.ToString() ?? "");
                                    cmd.Parameters.AddWithValue("@FechaHora", Convert.ToDateTime(row["FechaHora"]));
                                    cmd.Parameters.AddWithValue("@IdHistorialCortes", idCorteInsertado);
                                    cmd.ExecuteNonQuery();
                                }

                                productosTicket.Add(new Producto
                                {
                                    Cantidad = 1,
                                    Nombre = row["Concepto"]?.ToString() ?? "",
                                    PrecioUnitario = Convert.ToDouble(row["Total"]),
                                    Total = Convert.ToDouble(row["Total"])
                                });
                            }
                        }

                        // 4. Limpieza de tablas de operación diaria
                        using (SqlCommand cmd = new SqlCommand("UPDATE Usuarios SET Ventas = 0, Mesas = 0;", conectar, transaccion))
                            cmd.ExecuteNonQuery();

                        using (SqlCommand cmd = new SqlCommand("DELETE FROM CORTE;", conectar, transaccion))
                            cmd.ExecuteNonQuery();

                        using (SqlCommand cmd = new SqlCommand("UPDATE inicio SET inicio = '0' WHERE id = 1;", conectar, transaccion))
                            cmd.ExecuteNonQuery();

                        transaccion.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaccion.Rollback();
                        MessageBox.Show($"Error al guardar el corte de caja: {ex.Message}", "Error de Transacción", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            // Impresión Opcional de Ticket
            DialogResult dialogPrint = MessageBox.Show("Corte realizado con éxito.\n¿Desea imprimir el comprobante de caja?", "Impresión de Ticket", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogPrint == DialogResult.Yes)
            {
                string[] encabezados = new string[] {
                    "********** CORTE DE CAJA **********",
                    "Fecha: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")
                };

                Dictionary<string, double> totales = new Dictionary<string, double>
                {
                    { "Entradas", entradasEfectivo },
                    { "Salidas", salidasEfectivo },
                    { "Total Neto", corteNeto }
                };

                TicketPrinter ticketPrinter = new TicketPrinter(encabezados, Conexion.pieDeTicket, Conexion.logoPath, productosTicket, "", "", "", 0, true, totales);
                ticketPrinter.ImprimirTicket();
            }

            this.Close();
        }

        private void btnDetalleMesero_Click(object sender, EventArgs e)
        {
            if (dgvMeseros.CurrentRow == null)
            {
                MessageBox.Show("Por favor, seleccione un mesero de la lista para consultar su detalle.", "Selección requerida", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string idUsuario = dgvMeseros.CurrentRow.Cells["IdUsuario"].Value?.ToString();
            string nombreMesero = dgvMeseros.CurrentRow.Cells["Mesero"].Value?.ToString();
            string ventas = dgvMeseros.CurrentRow.Cells["Ventas"].Value?.ToString();
            string mesas = dgvMeseros.CurrentRow.Cells["MesasAtendidas"].Value?.ToString();

            frmCortesMesero cor = new frmCortesMesero
            {
                idMesero = idUsuario,
                lblMonto = { Text = ventas },
                lblMesas = { Text = mesas },
                Text = "Corte individual: " + nombreMesero,
                nombre = nombreMesero
            };
            cor.ShowDialog();
            CargarDatos(); // Refrescar vista
        }
    }
}