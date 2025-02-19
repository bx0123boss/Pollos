using LibPrintTicket;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Punto_Venta
{
    public partial class frmReporteVentasProducto : Form
    {
        OleDbConnection conectar = new OleDbConnection(Conexion.CadCon);
        private DataSet ds;
        OleDbDataAdapter da;
        string anoSQL = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + " " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();            
        public frmReporteVentasProducto()
        {
            InitializeComponent();
            this.MinimumSize = new Size(757, 500);
        }

        private void frmReporteVentasProducto_Load(object sender, EventArgs e)
        {
            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();
                DataSet ds = new DataSet();
                string query = @"SELECT TOP 100 
                                    Sum(A.Cantidad) AS CantidadVendidos, 
                                    C.Nombre, Sum(A.Total) AS SumaDeTotal 
                                    from ArticulosFolio A
                                    INNER JOIN Folios B ON A.IdFolio = B.IdFolio
                                    INNER JOIN INVENTARIO C ON C.IdInventario = A.IdInventario
                                    WHERE 
                                    B.Estatus = 'COBRADO' 
                                   AND FechaHora >= @StartDate AND FechaHora <= @EndDate GROUP BY A.IdInventario, C.Nombre;";

                using (SqlDataAdapter da = new SqlDataAdapter(query, conectar))
                {
                    da.SelectCommand.Parameters.AddWithValue("@StartDate", dateTimePicker1.Value.Date);
                    da.SelectCommand.Parameters.AddWithValue("@EndDate", dateTimePicker1.Value.Date.AddDays(1).AddSeconds(-1));

                    da.Fill(ds, "IdFolio");
                    dataGridView1.DataSource = ds.Tables["IdFolio"];
                }
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();
                DataSet ds = new DataSet();
                string query = @"SELECT TOP 100 
                                    Sum(A.Cantidad) AS CantidadVendidos, 
                                    C.Nombre, Sum(A.Total) AS SumaDeTotal 
                                    from ArticulosFolio A
                                    INNER JOIN Folios B ON A.IdFolio = B.IdFolio
                                    INNER JOIN INVENTARIO C ON C.IdInventario = A.IdInventario
                                    WHERE 
                                    B.Estatus = 'COBRADO' 
                                   AND FechaHora >= @StartDate AND FechaHora <= @EndDate GROUP BY A.IdInventario, C.Nombre;";

                using (SqlDataAdapter da = new SqlDataAdapter(query, conectar))
                {
                    da.SelectCommand.Parameters.AddWithValue("@StartDate", dateTimePicker1.Value.Date);
                    da.SelectCommand.Parameters.AddWithValue("@EndDate", dateTimePicker1.Value.Date.AddDays(1).AddSeconds(-1));

                    da.Fill(ds, "IdFolio");
                    dataGridView1.DataSource = ds.Tables["IdFolio"];
                }
            }
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Ticket ticket2 = new Ticket();
            ticket2.MaxChar = 35;
            ticket2.MaxCharDescription = 22;
            ticket2.FontSize = 8;
            ticket2.AddHeaderLine("******  PRODUCTOS VENDIDOS  *****");
            ticket2.AddSubHeaderLine("FECHA Y HORA:" + anoSQL);
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                ticket2.AddItem(dataGridView1[0, i].Value.ToString(), dataGridView1[1, i].Value.ToString(), "   $" + dataGridView1[2, i].Value.ToString());
            }
            ticket2.PrintTicket(Conexion.impresora);
        }
    }
}
