using LibPrintTicket;
using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Punto_Venta
{
    public partial class frmCortesMesero : Form
    {
        OleDbConnection conectar = new OleDbConnection(Conexion.CadCon);
        public string idMesero = "";
        public string nombre = "";
        public frmCortesMesero()
        {
            InitializeComponent();
        }

        private void frmCortesMesero_Load(object sender, EventArgs e)
        {
            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();
                DataSet ds = new DataSet();
                string query = @"SELECT a.IdFolio, A.ModalidadVenta, A.Estatus, ISNULL(A.IdCliente,0) AS IdCliente, A.FechaHora, A.Total, A.Descuento, A.Utilidad, B.Nombre AS Mesa, B.CantidadPersonas, C.Usuario AS Mesero, A.IdMesa, B.IdMesero
                                   FROM Folios A
                                   INNER JOIN MESAS B ON A.IdMesa = B.IdMesa
                                   INNER JOIN USUARIOS C ON B.IdMesero = C.IdUsuario
                                   WHERE FechaHora >= @StartDate AND FechaHora <= @EndDate
                                    AND A.Estatus='COBRADO' AND B.IdMesero = @IdMesero ORDER BY FechaHora DESC;";

                using (SqlDataAdapter da = new SqlDataAdapter(query, conectar))
                {
                    da.SelectCommand.Parameters.AddWithValue("@StartDate", dateTimePicker1.Value.Date);
                    da.SelectCommand.Parameters.AddWithValue("@EndDate", dateTimePicker1.Value.Date.AddDays(1).AddSeconds(-1));
                    da.SelectCommand.Parameters.AddWithValue("@IdMesero", idMesero);

                    da.Fill(ds, "IdFolio");
                    dataGridView1.DataSource = ds.Tables["IdFolio"];
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns["Total"].DefaultCellStyle.Format = "N2";
                    dataGridView1.Columns["Descuento"].DefaultCellStyle.Format = "N2";
                    dataGridView1.Columns["Utilidad"].DefaultCellStyle.Format = "N2";
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
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
                detalles.utilidad = Convert.ToDouble(dataGridView1.CurrentRow.Cells["Utilidad"].Value.ToString());
                detalles.lblEstatus.Text = dataGridView1.CurrentRow.Cells["Estatus"].Value.ToString();
                if (dataGridView1.CurrentRow.Cells["Estatus"].Value.ToString() == "CANCELADO")
                {
                    detalles.button2.Hide();
                }
                detalles.Show();
                this.Close();
            }
            catch
            { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Ticket ticket = new Ticket();
            ticket.MaxChar = 35;
            ticket.MaxCharDescription = 22;
            ticket.FontSize = 8;
            ticket.AddHeaderLine("********  CORTE DE CAJA  *******");
            ticket.AddHeaderLine("MESERO: " + nombre);
            ticket.AddSubHeaderLine("FECHA Y HORA:");
            ticket.AddSubHeaderLine(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                ticket.AddItem("1", "Folio: " +dataGridView1[0, i].Value.ToString(), "   $" + dataGridView1[11, i].Value.ToString());
            }
            ticket.AddTotal("Total: ", lblMonto.Text);
            ticket.AddTotal("Mesas Atendidas:", lblMesas.Text);
            ticket.PrintTicket(Conexion.impresora);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmCorte corte = new frmCorte();
            corte.usuario = "VENTAS";
            corte.Show();
            this.Close();
        }
    }
}
