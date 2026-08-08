using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using static ComandasReportPdf;

namespace Punto_Venta
{
    public partial class frmReporteVentasProducto : frmBase
    {
        List<ComandasReportPdf.ComandaRow> datos = new List<ComandasReportPdf.ComandaRow>();
        string anoSQL = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + " " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();            
        public frmReporteVentasProducto()
        {
            InitializeComponent();
            this.MinimumSize = new Size(757, 500);
        }

        private void frmReporteVentasProducto_Load(object sender, EventArgs e)
        {
            if (Conexion.empresa == "PLAYA")
            {
                CargarGrid();
                return;
            }
            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();
                DataSet ds = new DataSet();
                string query = @"SELECT TOP 100 
                                   Sum(A.Cantidad) AS CantidadVendidos, 
									CASE
                                     WHEN A.IdInventario = 0 THEN P.Nombre 
									 ELSE C.Nombre 
									 END AS Nombre,
									 CASE 
										 WHEN A.IdInventario = 0 THEN P.Precio 
										 ELSE C.Precio 
									 END AS Precio,
									Sum(A.Total) AS Total 
                                    from ArticulosFolio A
                                    INNER JOIN Folios B ON A.IdFolio = B.IdFolio
                                    INNER JOIN INVENTARIO C ON C.IdInventario = A.IdInventario
									LEFT JOIN Promos P ON A.IdPromo = P.IdPromo
                                    WHERE 
                                    B.Estatus = 'COBRADO' 
									
                                   AND FechaHora >= @StartDate AND FechaHora <= @EndDate 
                                   GROUP BY A.IdInventario, C.Nombre,P.Nombre, P.Precio, C.Precio;";

                using (SqlDataAdapter da = new SqlDataAdapter(query, conectar))
                {
                    da.SelectCommand.Parameters.AddWithValue("@StartDate", dateTimePicker1.Value.Date);
                    da.SelectCommand.Parameters.AddWithValue("@EndDate", dateTimePicker1.Value.Date.AddDays(1).AddSeconds(-1));

                    da.Fill(ds, "IdFolio");
                    dataGridView1.DataSource = ds.Tables["IdFolio"];
                }
            }
            EstilizarDataGridView(dataGridView1);
            EstilizarBotonPrimario(button2);
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var hdr = new ReportHeader
            {
                Empresa = Conexion.datosTicket[0],
                Rfc = "",
                Direccion = "",
                CiudadCpTel = "",
                Desde = new System.DateTime(2025, 7, 31, 6, 0, 0),
                Hasta = new System.DateTime(2025, 8, 1, 5, 59, 59),
                MeseroFiltro = "(TODOS)",
                FooterNote = "Jaeger Soft"
            };
            using (var sfd = new SaveFileDialog { Filter = "PDF (*.pdf)|*.pdf", FileName = "ReporteComandas.pdf" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    ComandasReportPdf.Generate(hdr, datos, sfd.FileName);
                    MessageBox.Show("PDF generado.");
                }
            }
        }
        private void CargarGrid()
        {
            var dia = dateTimePicker1.Value.Date;
            datos = ConsultarComandas(dia, dia); // reusa tu método

            // Si no vas a editar, List<T> basta; si quieres edición/refresh, usa BindingList<T>
            var bs = new BindingSource { DataSource = new BindingList<ComandasReportPdf.ComandaRow>(datos) };

            dataGridView1.AutoGenerateColumns = true;   // o false si defines columnas a mano
            dataGridView1.DataSource = bs;

            // Formatos/estilos (ajusta nombres si cambiaste propiedades)
            FormatearColumnas();
        }
        private void FormatearColumnas()
        {
            // Evita excepción si la columna no existe (por cambios de nombres)
            void fmt(string col, string format = null, DataGridViewContentAlignment? align = null)
            {
                if (!dataGridView1.Columns.Contains(col)) return;
                var c = dataGridView1.Columns[col];
                if (format != null) c.DefaultCellStyle.Format = format;
                if (align.HasValue) c.DefaultCellStyle.Alignment = align.Value;
            }

            // Fechas en una línea (24h)
            fmt(nameof(ComandasReportPdf.ComandaRow.FechaApertura), "dd/MM/yyyy HH:mm:ss");
            fmt(nameof(ComandasReportPdf.ComandaRow.FechaCierre), "dd/MM/yyyy HH:mm:ss");
            fmt(nameof(ComandasReportPdf.ComandaRow.FechaCaptura), "dd/MM/yyyy HH:mm:ss");

            // Cantidad sin ceros de más
            fmt(nameof(ComandasReportPdf.ComandaRow.Cantidad), "0.##", DataGridViewContentAlignment.MiddleRight);

            // Moneda a la derecha
            fmt(nameof(ComandasReportPdf.ComandaRow.Importe), "c2", DataGridViewContentAlignment.MiddleRight);
            fmt(nameof(ComandasReportPdf.ComandaRow.Descuento), "c2", DataGridViewContentAlignment.MiddleRight);

            // (Opcional) ancho rápido
            if (dataGridView1.Columns.Contains(nameof(ComandasReportPdf.ComandaRow.Producto)))
                dataGridView1.Columns[nameof(ComandasReportPdf.ComandaRow.Producto)].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AllowUserToAddRows = false;
        }
        public static List<ComandasReportPdf.ComandaRow> ConsultarComandas(DateTime desde, DateTime hasta)
        {
            var lista = new List<ComandasReportPdf.ComandaRow>();

            const string sql = @"
                        SELECT
                            F.IdFolio                                    AS FolioCuenta,
                            AF.Orden                                     AS Orden,
                            F.FechaHora                                  AS FechaApertura,
                            F.FechaHoraCobro                             AS FechaCierre,
                            U.Nombre                                     AS MeseroCuenta,
                            U.Nombre                                     AS MeseroProd,
                            AF.Cantidad                                  AS Cantidad,
                            AF.FechaHora                                 AS FechaCaptura,
                            CASE
                                WHEN AF.IdInventario = 0 THEN P.Nombre
                                ELSE I.Nombre
                            END                                          AS Producto,
                            CASE
                                WHEN AF.IdInventario = 0 THEN P.Precio
                                ELSE I.Precio
                            END                                          AS Importe,
                            ISNULL(AF.Descuento,0)                       AS Descuento
                        FROM ArticulosFolio AF
                            INNER JOIN Folios F
                                ON AF.IdFolio = F.IdFolio
                            LEFT JOIN INVENTARIO I
                                ON AF.IdInventario = I.IdInventario
                            LEFT JOIN Promos P
                                ON AF.IdPromo = P.IdPromo
                            LEFT JOIN USUARIOS U
                                ON F.IdUsuario = U.IdUsuario
                        WHERE
                            F.Estatus = 'COBRADO'
                            AND F.FechaHora >= @Desde
                            AND F.FechaHora < @Hasta

                        ORDER BY F.IdFolio, AF.Orden;";

            using (var cn = new SqlConnection(Conexion.CadConRestaurantSoft))
            using (var cmd = new SqlCommand(sql, cn))
            {
                var start = desde.Date;
                var next = hasta.Date.AddDays(1);

                cmd.Parameters.Add("@Desde", SqlDbType.DateTime).Value = start;
                cmd.Parameters.Add("@Hasta", SqlDbType.DateTime).Value = next;

                cn.Open();
                using (var rd = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    // Construir mapa nombre→ordinal (case-insensitive)
                    var ord = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                    for (int i = 0; i < rd.FieldCount; i++)
                        ord[rd.GetName(i)] = i;

                    // Función para obtener ordinal validando nombre y dando error claro
                    int Need(string name)
                    {
                        if (!ord.TryGetValue(name, out var idx))
                        {
                            // Mensaje útil con columnas disponibles
                            var cols = string.Join(", ", ord.Keys);
                            throw new IndexOutOfRangeException(
                                $"La columna '{name}' no existe en el lector. Columnas disponibles: {cols}");
                        }
                        return idx;
                    }

                    // Resolver una vez todos los índices
                    int iFolioComanda = Need("FolioComanda");
                    int iOrden = Need("Orden");
                    int iFechaApertura = Need("FechaApertura");
                    int iFechaCierre = Need("FechaCierre");
                    int iMeseroCuenta = Need("MeseroCuenta");
                    int iMeseroProd = Need("MeseroProd");
                    int iCantidad = Need("Cantidad");
                    int iFechaCaptura = Need("FechaCaptura");
                    int iProducto = Need("Producto");
                    int iImporte = Need("Importe");
                    int iDescuento = Need("Descuento");

                    while (rd.Read())
                    {
                        var row = new ComandasReportPdf.ComandaRow
                        {
                            FolioComanda = 0l,
                            FolioCuenta = rd.IsDBNull(iFolioComanda) ? 0L : Convert.ToInt64(rd.GetValue(iFolioComanda)),
                            Orden = rd.IsDBNull(iOrden) ? 0 : Convert.ToInt32(rd.GetValue(iOrden)),
                            FechaApertura = rd.IsDBNull(iFechaApertura) ? DateTime.MinValue : rd.GetDateTime(iFechaApertura),
                            FechaCierre = rd.IsDBNull(iFechaCierre) ? DateTime.MinValue : rd.GetDateTime(iFechaCierre),
                            MeseroCuenta = rd.IsDBNull(iMeseroCuenta) ? "" : rd.GetString(iMeseroCuenta),
                            MeseroProd = rd.IsDBNull(iMeseroProd) ? "" : rd.GetString(iMeseroProd),
                            Cantidad = rd.IsDBNull(iCantidad) ? 0m : Convert.ToDecimal(rd.GetValue(iCantidad), CultureInfo.InvariantCulture),
                            FechaCaptura = rd.IsDBNull(iFechaCaptura) ? DateTime.MinValue : rd.GetDateTime(iFechaCaptura),
                            Producto = rd.IsDBNull(iProducto) ? "" : rd.GetString(iProducto),
                            Importe = rd.IsDBNull(iImporte) ? 0m : Convert.ToDecimal(rd.GetValue(iImporte), CultureInfo.InvariantCulture),
                            Descuento = rd.IsDBNull(iDescuento) ? 0m : Convert.ToDecimal(rd.GetValue(iDescuento), CultureInfo.InvariantCulture)
                        };

                        lista.Add(row);
                    }
                }
            }

            return lista;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(Conexion.empresa == "PLAYA")
            {
                CargarGrid();
                return;
            }
            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();
                DataSet ds = new DataSet();
                string query = @"SELECT
                       Sum(A.Cantidad) AS CantidadVendidos, 
                       CASE
                         WHEN A.IdInventario = 0 THEN P.Nombre 
                         ELSE C.Nombre 
                       END AS Nombre,
                       CASE 
                         WHEN A.IdInventario = 0 THEN P.Precio 
                         ELSE C.Precio 
                       END AS Precio,
                       Sum(A.Total) AS Total 
                     from ArticulosFolio A
                     INNER JOIN Folios B ON A.IdFolio = B.IdFolio
                     INNER JOIN INVENTARIO C ON C.IdInventario = A.IdInventario
                     LEFT JOIN Promos P ON A.IdPromo = P.IdPromo
                     WHERE 
                     B.Estatus = 'COBRADO' 
                     AND FechaHora >= @StartDate AND FechaHora <= @EndDate 
                     GROUP BY A.IdInventario, C.Nombre, P.Nombre, P.Precio, C.Precio;";

                using (SqlDataAdapter da = new SqlDataAdapter(query, conectar))
                {
                    da.SelectCommand.Parameters.AddWithValue("@StartDate", dateTimePicker1.Value.Date);
                    da.SelectCommand.Parameters.AddWithValue("@EndDate", dateTimePicker2.Value.Date.AddDays(1).AddSeconds(-1));
                    da.Fill(ds, "IdFolio");
                    dataGridView1.DataSource = ds.Tables["IdFolio"];
                }
            }
        }
    }
}
