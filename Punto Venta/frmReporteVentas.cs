using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Security.Cryptography;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Punto_Venta
{
    public partial class frmReporteVentas : Form
    {
        public string usuario = "Administrador";
        public frmReporteVentas()
        {
            InitializeComponent();
            this.MinimumSize = new Size(750, 475);
        }
        private void frmReporteVentas_Load(object sender, EventArgs e)
        {
            if(Conexion.lugar == "PLAYA")
            {
                CargarFoliosPorDia(dateTimePicker1.Value.Date);
                return;
            }
            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();
                DataSet ds = new DataSet();
                string query = @"SELECT a.IdFolio, A.ModalidadVenta, A.Estatus, ISNULL(A.IdCliente,0) AS IdCliente, A.FechaHora, A.Total, A.Descuento, A.Utilidad, B.Nombre AS Mesa, ISNULL(CAST(B.CantidadPersonas AS VARCHAR), 'N/A') AS CantidadPersonas, C.Usuario AS Mesero, A.IdMesa, B.IdMesero
                                   FROM Folios A
                                   INNER JOIN MESAS B ON A.IdMesa = B.IdMesa
                                   INNER JOIN USUARIOS C ON B.IdMesero = C.IdUsuario
                                   WHERE FechaHora >= @StartDate AND FechaHora <= @EndDate ORDER BY FechaHora DESC;";

                using (SqlDataAdapter da = new SqlDataAdapter(query, conectar))
                {
                    da.SelectCommand.Parameters.AddWithValue("@StartDate", dateTimePicker1.Value.Date);
                    da.SelectCommand.Parameters.AddWithValue("@EndDate", dateTimePicker1.Value.Date.AddDays(1).AddSeconds(-1));

                    da.Fill(ds, "IdFolio");
                    dataGridView1.DataSource = ds.Tables["IdFolio"];
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns["IdCliente"].Visible = false;
                    dataGridView1.Columns["IdMesero"].Visible = false;
                    dataGridView1.Columns["IdMesa"].Visible = false;
                    dataGridView1.Columns["Total"].DefaultCellStyle.Format = "N2";
                    dataGridView1.Columns["Descuento"].DefaultCellStyle.Format = "N2";
                    dataGridView1.Columns["Utilidad"].DefaultCellStyle.Format = "N2";
                }
            }

        }
        private void CargarFoliosPorDia(DateTime dia)
        {
            var inicio = dia.Date;       // 00:00 del día elegido
            var siguiente = inicio.AddDays(1);

            const string sql = @"
                SELECT 
                    A.folio, A.numcheque, A.fecha, A.cierre, A.mesa, A.nopersonas, B.nombre AS nombremesero, A.orden, A.total, A.usuariopago
                FROM cheques A
                INNER JOIN meseros B ON A.idmesero = B.idmesero 

                WHERE A.Fecha >= @StartDate
                  AND A.Fecha <  @NextDate
                    and A.pagado = 1
                ORDER BY A.Fecha DESC;";

            using (var cn = new SqlConnection(Conexion.CadConRestaurantSoft))
            using (var cmd = new SqlCommand(sql, cn))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = inicio;
                cmd.Parameters.Add("@NextDate", SqlDbType.DateTime).Value = siguiente;

                var ds = new DataSet();
                da.Fill(ds, "folio");

                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "folio";
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(Conexion.empresa == "PLAYA")
            {
                frmVentaDetalladaPlaya detalles = new frmVentaDetalladaPlaya();
                detalles.lblFolio.Text = dataGridView1.CurrentRow.Cells["folio"].Value.ToString();
                detalles.fechaApertura = dataGridView1.CurrentRow.Cells["fecha"].Value.ToString();
                detalles.lblFecha.Text = dataGridView1.CurrentRow.Cells["cierre"].Value.ToString();
                detalles.lblMesero.Text = dataGridView1.CurrentRow.Cells["nombremesero"].Value.ToString();
                detalles.orden = dataGridView1.CurrentRow.Cells["orden"].Value.ToString();
                detalles.nopersonas = Convert.ToInt32(dataGridView1.CurrentRow.Cells["nopersonas"].Value.ToString());
                detalles.lblMesa.Text = dataGridView1.CurrentRow.Cells["mesa"].Value.ToString();
                detalles.total = Convert.ToDouble(dataGridView1.CurrentRow.Cells["total"].Value.ToString());
                detalles.usuario = usuario;
                detalles.ShowDialog();
                return;
            }
            try
            {
                frmVentaDetallada detalles = new frmVentaDetallada();
                detalles.lblFolio.Text = dataGridView1.CurrentRow.Cells["IdFolio"].Value.ToString();
                detalles.lblFecha.Text = dataGridView1.CurrentRow.Cells["FechaHora"].Value.ToString();
                detalles.lblModalidad.Text = dataGridView1.CurrentRow.Cells["ModalidadVenta"].Value.ToString();
                detalles.lblMesero.Text = dataGridView1.CurrentRow.Cells["Mesero"].Value.ToString();
                detalles.IdMesa = dataGridView1.CurrentRow.Cells["IdMesa"].Value.ToString();
                detalles.idMesero = dataGridView1.CurrentRow.Cells["IdMesero"].Value.ToString();
                detalles.lblMesa.Text = dataGridView1.CurrentRow.Cells["Mesa"].Value.ToString();
                detalles.total = Convert.ToDouble(dataGridView1.CurrentRow.Cells["Total"].Value.ToString());
                detalles.usuario = usuario;
                detalles.utilidad = Convert.ToDouble(dataGridView1.CurrentRow.Cells["Utilidad"].Value.ToString());
                detalles.idCliente = dataGridView1.CurrentRow.Cells["IdCliente"].Value.ToString();
                detalles.lblEstatus.Text = dataGridView1.CurrentRow.Cells["Estatus"].Value.ToString();
                if (dataGridView1.CurrentRow.Cells["Estatus"].Value.ToString() == "CANCELADO")
                {
                    detalles.button2.Hide();
                }
                detalles.ShowDialog();
                this.Close();
            }
            catch 
            {
                MessageBox.Show("Tiene que seleccionar un folio antes", "Reporte de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       
        private void dateTimePicker1_CloseUp(object sender, EventArgs e)
        {
            if(Conexion.empresa == "PLAYA")
            {
                CargarFoliosPorDia(dateTimePicker1.Value.Date);
                return;
            }
            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();
                DataSet ds = new DataSet();
                string query = @"SELECT a.IdFolio, A.ModalidadVenta, A.Estatus, ISNULL(A.IdCliente,0) AS IdCliente, A.FechaHora, A.Total, A.Descuento, A.Utilidad, B.Nombre AS Mesa, ISNULL(CAST(B.CantidadPersonas AS VARCHAR), 'N/A') AS CantidadPersonas, C.Usuario AS Mesero,  A.IdMesa,  B.IdMesero
                                   FROM Folios A
                                   INNER JOIN MESAS B ON A.IdMesa = B.IdMesa
                                   INNER JOIN USUARIOS C ON B.IdMesero = C.IdUsuario
                                   WHERE FechaHora >= @StartDate AND FechaHora <= @EndDate ORDER BY FechaHora DESC;";

                using (SqlDataAdapter da = new SqlDataAdapter(query, conectar))
                {
                    da.SelectCommand.Parameters.AddWithValue("@StartDate", dateTimePicker1.Value.Date);
                    da.SelectCommand.Parameters.AddWithValue("@EndDate", dateTimePicker1.Value.Date.AddDays(1).AddSeconds(-1));

                    da.Fill(ds, "IdFolio");
                    dataGridView1.DataSource = ds.Tables["IdFolio"];
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns["IdCliente"].Visible = false;
                    dataGridView1.Columns["IdMesero"].Visible = false;
                    dataGridView1.Columns["IdMesa"].Visible = false;
                    dataGridView1.Columns["Total"].DefaultCellStyle.Format = "N2";
                    dataGridView1.Columns["Descuento"].DefaultCellStyle.Format = "N2";
                    dataGridView1.Columns["Utilidad"].DefaultCellStyle.Format = "N2";
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmArticulosCancelados art = new frmArticulosCancelados();
            art.Show();
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            if(Conexion.empresa == "PLAYA")
            {
                frmAjustarFolios ajustar = new frmAjustarFolios();
                ajustar.ShowDialog();
            }
        }

        private void txtFolio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                filtraroFolio();
            }
        }

        private void filtraroFolio()
        {

            const string sql = @"
                SELECT 
                    A.folio, A.numcheque, A.fecha, A.cierre, A.mesa, A.nopersonas, B.nombre AS nombremesero, A.orden, A.total, A.usuariopago
                FROM cheques A
                INNER JOIN meseros B ON A.idmesero = B.idmesero 

                WHERE A.folio = @Folio
                    AND A.pagado = 1
                ORDER BY A.Fecha DESC;";

            using (var cn = new SqlConnection(Conexion.CadConRestaurantSoft))
            using (var cmd = new SqlCommand(sql, cn))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@Folio", txtFolio.Text);

                var ds = new DataSet();
                da.Fill(ds, "folio");

                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "folio";
            }
        }
    }
}
