using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Punto_Venta
{
    public partial class frmCorte : Form
    {
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

        public string usuario = "";

        public frmCorte()
        {
            InitializeComponent();
            this.MinimumSize = new Size(1024, 720);
        }

        private void frmCorte_Load(object sender, EventArgs e)
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

                    // Carga general de movimientos
                    using (SqlDataAdapter da = new SqlDataAdapter("SELECT Concepto, Total, FormaPago, FechaHora FROM CORTE ORDER BY FechaHora DESC;", conectar))
                    {
                        DataTable dtCorte = new DataTable();
                        da.Fill(dtCorte);
                        dgvGeneral.DataSource = dtCorte;
                    }

                    // Carga por meseros
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

        private void btnCorte_Click(object sender, EventArgs e)
        {
            if (dgvGeneral.Rows.Count == 0)
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

            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();
                using (SqlTransaction transaccion = conectar.BeginTransaction())
                {
                    try
                    {
                        int idCorteInsertado;
                        string queryHistorial = @"INSERT INTO HistorialCortes (Monto, FechaHora) VALUES (@Monto, GETDATE());
                                                 SELECT SCOPE_IDENTITY();";

                        using (SqlCommand cmd = new SqlCommand(queryHistorial, conectar, transaccion))
                        {
                            cmd.Parameters.AddWithValue("@Monto", granTotal);
                            idCorteInsertado = Convert.ToInt32(cmd.ExecuteScalar());
                        }

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

                        if (dgvGeneral.DataSource is DataTable dtCorte)
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

                        using (SqlCommand cmd = new SqlCommand("UPDATE Usuarios SET Ventas = 0, Mesas = 0;", conectar, transaccion))
                            cmd.ExecuteNonQuery();

                        /*using (SqlCommand cmd = new SqlCommand("DELETE FROM CORTE;", conectar, transaccion))
                            cmd.ExecuteNonQuery();
                        
                        using (SqlCommand cmd = new SqlCommand("UPDATE inicio SET inicio = '0' WHERE id = 1;", conectar, transaccion))
                            cmd.ExecuteNonQuery();

                        */transaccion.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaccion.Rollback();
                        MessageBox.Show($"Error al guardar el corte de caja: {ex.Message}", "Error de Transacción", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            DialogResult dialogPrint = MessageBox.Show("Corte realizado con éxito.\n¿Desea imprimir el comprobante de caja?", "Impresión de Ticket", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogPrint == DialogResult.Yes)
            {
                string[] encabezados = new string[] {
                    "********** CORTE DE CAJA **********",
                    "Fecha: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")
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

                TicketPrinter ticketPrinter = new TicketPrinter(encabezados, Conexion.pieDeTicket, Conexion.logoPath, productosTicket, "", "", "", 0, true, totalesTicket);
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
            CargarDatos();
        }
    }
}