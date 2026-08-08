using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Punto_Venta
{
    public partial class frmDetalleCorte : Form
    {
        public int ID; // ID de la tabla HistorialCortes

        // Acumuladores de Métricas
        private double ventasEfectivo = 0;
        private double ventasTarjeta = 0;
        private double ventasTransferencia = 0;

        private double cancelacionesEfectivo = 0;
        private double cancelacionesTarjeta = 0;
        private double cancelacionesTransferencia = 0;
        private double totalCancelaciones = 0;

        private double retirosEfectivo = 0;

        private double aperturaCaja = 0;
        private double ingresosEfectivo = 0;
        private double totalOtrosIngresos = 0;

        private double totalEfectivoEnCaja = 0;
        private double granTotal = 0;
        private double totalPropinas = 0;

        public frmDetalleCorte()
        {
            InitializeComponent();
            this.MinimumSize = new Size(1024, 720);
        }

        private void frmDetalleCorte_Load(object sender, EventArgs e)
        {
            CargarDatos();
            CalcularTotalesYGrids();
            AplicarEstilosGrillas();
        }

        private void CargarDatos()
        {
            try
            {
                using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
                {
                    conectar.Open();

                    // Cargar fecha del corte desde HistorialCortes
                    using (SqlCommand cmd = new SqlCommand("SELECT FechaHora FROM HistorialCortes WHERE IdHistorialCortes = @IdCortes;", conectar))
                    {
                        cmd.Parameters.AddWithValue("@IdCortes", ID);
                        object resFecha = cmd.ExecuteScalar();
                        if (resFecha != null && resFecha != DBNull.Value)
                            lblFecha.Text = Convert.ToDateTime(resFecha).ToString("dd/MM/yyyy HH:mm:ss");
                    }

                    // Carga general de movimientos históricos asociados a este ID de corte
                    using (SqlDataAdapter da = new SqlDataAdapter("SELECT Concepto, Total, FormaPago, FechaHora FROM CORTES WHERE IdHistorialCortes = @IdCortes ORDER BY FechaHora DESC;", conectar))
                    {
                        da.SelectCommand.Parameters.AddWithValue("@IdCortes", ID);
                        DataTable dtCorte = new DataTable();
                        da.Fill(dtCorte);
                        dgvGeneral.DataSource = dtCorte;
                    }

                    // Carga por meseros histórica asociada a este ID de corte
                    using (SqlDataAdapter da = new SqlDataAdapter("SELECT Mesero, Ventas, Mesas AS MesasAtendidas FROM CortesMeseros WHERE IdHistorialCortes = @IdCorte;", conectar))
                    {
                        da.SelectCommand.Parameters.AddWithValue("@IdCorte", ID);
                        DataTable dtMeseros = new DataTable();
                        da.Fill(dtMeseros);
                        dgvMeseros.DataSource = dtMeseros;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el detalle del corte: {ex.Message}", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalcularTotalesYGrids()
        {
            // Reset de contadores
            ventasEfectivo = 0; ventasTarjeta = 0; ventasTransferencia = 0;
            cancelacionesEfectivo = 0; cancelacionesTarjeta = 0; cancelacionesTransferencia = 0; totalCancelaciones = 0;
            retirosEfectivo = 0;
            aperturaCaja = 0; ingresosEfectivo = 0; totalOtrosIngresos = 0;
            totalEfectivoEnCaja = 0; granTotal = 0; totalPropinas = 0;

            DataTable dtVentas = CrearEstructuraGrid();
            DataTable dtCancelaciones = CrearEstructuraGrid();
            DataTable dtRetiros = CrearEstructuraGrid();
            DataTable dtOtrosIngresos = CrearEstructuraGrid();

            if (dgvGeneral.DataSource is DataTable dt)
            {
                foreach (DataRow row in dt.Rows)
                {
                    double monto = Convert.ToDouble(row["Total"]);
                    string formaPago = (row["FormaPago"]?.ToString() ?? "").ToUpper();
                    string concepto = (row["Concepto"]?.ToString() ?? "").ToUpper();
                    DateTime fecha = Convert.ToDateTime(row["FechaHora"]);

                    if (formaPago.Contains("PROPINA") || concepto.Contains("PROPINA"))
                    {
                        totalPropinas += monto;
                        continue;
                    }

                    if (concepto.Contains("CANCELAC"))
                    {
                        double montoAbs = Math.Abs(monto);
                        if (formaPago.Contains("TARJETA") || formaPago.Contains("CREDITO") || formaPago.Contains("DEBITO"))
                            cancelacionesTarjeta += montoAbs;
                        else if (formaPago.Contains("TRANSFER") || formaPago.Contains("TRANFER"))
                            cancelacionesTransferencia += montoAbs;
                        else
                            cancelacionesEfectivo += montoAbs;

                        dtCancelaciones.Rows.Add(concepto, montoAbs, formaPago, fecha);
                    }
                    else if (concepto.Contains("RETIRO") || concepto.Contains("SALIDA") || monto < 0)
                    {
                        double montoAbs = Math.Abs(monto);
                        retirosEfectivo += montoAbs;
                        dtRetiros.Rows.Add(concepto, montoAbs, "EFECTIVO", fecha);
                    }
                    else if (concepto.Contains("APERTURA") || concepto.Contains("ENTRADA") || concepto.Contains("INGRESO"))
                    {
                        if (concepto.Contains("APERTURA") || concepto.Contains("INICIO"))
                            aperturaCaja += monto;
                        else
                            ingresosEfectivo += monto;

                        dtOtrosIngresos.Rows.Add(concepto, monto, "EFECTIVO", fecha);
                    }
                    else
                    {
                        if (formaPago.Contains("TARJETA") || formaPago.Contains("CREDITO") || formaPago.Contains("DEBITO"))
                            ventasTarjeta += monto;
                        else if (formaPago.Contains("TRANSFER") || formaPago.Contains("TRANFER"))
                            ventasTransferencia += monto;
                        else
                            ventasEfectivo += monto;

                        dtVentas.Rows.Add(concepto, monto, formaPago, fecha);
                    }
                }
            }

            dgvVentas.DataSource = dtVentas;
            dgvCancelaciones.DataSource = dtCancelaciones;
            dgvRetiros.DataSource = dtRetiros;
            dgvOtrosIngresos.DataSource = dtOtrosIngresos;

            totalCancelaciones = cancelacionesEfectivo + cancelacionesTarjeta + cancelacionesTransferencia;
            totalOtrosIngresos = ingresosEfectivo + aperturaCaja;

            // Formulación del Corte de Caja
            totalEfectivoEnCaja = ventasEfectivo + totalOtrosIngresos - cancelacionesEfectivo - retirosEfectivo;
            granTotal = (ventasEfectivo + ventasTarjeta + ventasTransferencia) - totalCancelaciones;

            // Asignación con etiquetas de contexto completas en las Cards
            lblVentasDetalle.Text = $"Efec: {ventasEfectivo:C2} | Tarj: {ventasTarjeta:C2} | Transf: {ventasTransferencia:C2}";
            lblCancDetalle.Text = $"Efec: {cancelacionesEfectivo:C2} | Tarj: {cancelacionesTarjeta:C2} | Transf: {cancelacionesTransferencia:C2}";
            lblTotalCancelaciones.Text = $"Total Cancelaciones: {totalCancelaciones:C2}";

            lblRetiros.Text = $"Total Retirado: {retirosEfectivo:C2}";
            lblOtrosIngresosDetalle.Text = $"Apertura: {aperturaCaja:C2} | Ingresos: {ingresosEfectivo:C2}";
            lblTotalOtrosIngresos.Text = $"Total Otros Ingresos: {totalOtrosIngresos:C2}";

            lblEfectivoEnCaja.Text = $"Efectivo Neto en Caja: {totalEfectivoEnCaja:C2}";
            lblGranTotal.Text = $"Total Venta General: {granTotal:C2}";
            lblPropina.Text = $"Propinas: {totalPropinas:C2}";
        }

        private DataTable CrearEstructuraGrid()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Concepto", typeof(string));
            dt.Columns.Add("Total", typeof(double));
            dt.Columns.Add("FormaPago", typeof(string));
            dt.Columns.Add("FechaHora", typeof(DateTime));
            return dt;
        }

        private void AplicarEstilosGrillas()
        {
            DataGridView[] grillas = { dgvGeneral, dgvVentas, dgvCancelaciones, dgvRetiros, dgvOtrosIngresos, dgvMeseros };

            foreach (var dgv in grillas)
            {
                if (dgv == null) continue;
                EstilarGrillaPOS(dgv);

                if (dgv.Columns.Contains("Total"))
                    dgv.Columns["Total"].DefaultCellStyle.Format = "C2";

                if (dgv.Columns.Contains("Ventas"))
                    dgv.Columns["Ventas"].DefaultCellStyle.Format = "C2";
            }
        }

        private void EstilarGrillaPOS(DataGridView dgv)
        {
            dgv.EnableHeadersVisualStyles = false;
            dgv.BorderStyle = BorderStyle.None;
            dgv.BackgroundColor = Color.White;
            dgv.GridColor = Color.FromArgb(230, 233, 239);

            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(40, 45, 54);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgv.ColumnHeadersHeight = 35;

            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(226, 238, 255);
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            dgv.RowTemplate.Height = 28;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (dgvGeneral.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos de movimientos para generar la impresión.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            List<Producto> productosTicket = new List<Producto>();

            if (dgvGeneral.DataSource is DataTable dtCorte)
            {
                foreach (DataRow row in dtCorte.Rows)
                {
                    productosTicket.Add(new Producto
                    {
                        Cantidad = 1,
                        Nombre = row["Concepto"]?.ToString() ?? "",
                        PrecioUnitario = Convert.ToDouble(row["Total"]),
                        Total = Convert.ToDouble(row["Total"])
                    });
                }
            }

            string[] encabezados = new string[] {
                "********** REIMPRESIÓN CORTE **********",
                "Fecha Corte: " + lblFecha.Text,
                "Impreso el: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")
            };

            Dictionary<string, double> totalesTicket = new Dictionary<string, double>
            {
                { "Ventas Efectivo", ventasEfectivo },
                { "Ventas Tarjeta", ventasTarjeta },
                { "Ventas Transferencia", ventasTransferencia },
                { "Total Cancelaciones", totalCancelaciones },
                { "Retiros Efectivo", retirosEfectivo },
                { "**VENTA TOTAL**", granTotal },
                { "Apertura de Caja", aperturaCaja },
                { "Otros Ingresos", (totalOtrosIngresos - aperturaCaja)},
                { "Propinas", totalPropinas },
                { "**EFECTIVO EN CAJA**", totalEfectivoEnCaja },
            };

            TicketPrinter ticketPrinter = new TicketPrinter(
                encabezados,
                Conexion.pieDeTicket,
                Conexion.logoPath,
                productosTicket,
                "", "", "", 0, true,
                totalesTicket
            );

            ticketPrinter.ImprimirTicket();
        }
    }
}