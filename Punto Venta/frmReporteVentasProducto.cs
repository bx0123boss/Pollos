using LibPrintTicket;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
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
        private DataSet ds2;
        OleDbDataAdapter da2;
        string anoSQL = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + " " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();            
        public frmReporteVentasProducto()
        {
            InitializeComponent();
        }

        private void frmReporteVentasProducto_Load(object sender, EventArgs e)
        {
            conectar.Open();            
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_CloseUp(object sender, EventArgs e)
        {
            ds = new DataSet();
            da = new OleDbDataAdapter("SELECT TOP 100 Sum(ventas.Cantidad) AS CantidadVendidos, ventas.Producto, Sum(ventas.Total) AS SumaDeTotal from ventas where Fecha >=#" + dateTimePicker1.Value.Month.ToString() + "/" + dateTimePicker1.Value.Day.ToString() + "/" + dateTimePicker1.Value.Year.ToString() + " 00:00:00# and Fecha <=#" + dateTimePicker1.Value.Month.ToString() + "/" + dateTimePicker1.Value.Day.ToString() + "/" + dateTimePicker1.Value.Year.ToString() + " 23:59:59# AND Estatus = 'COBRADO' GROUP BY Ventas.Producto ORDER BY 2 ASC;", conectar);
            da.Fill(ds, "Id");
            dataGridView1.DataSource = ds.Tables["Id"];

            ds = new DataSet();
            da = new OleDbDataAdapter("select DISTINCT producto from ventas where Fecha >=#" + dateTimePicker1.Value.Month.ToString() + "/" + dateTimePicker1.Value.Day.ToString() + "/" + dateTimePicker1.Value.Year.ToString() + " 00:00:00# and Fecha <=#" + dateTimePicker1.Value.Month.ToString() + "/" + dateTimePicker1.Value.Day.ToString() + "/" + dateTimePicker1.Value.Year.ToString() + " 23:59:59#  AND Estatus = 'COBRADO';", conectar);
            da.Fill(ds, "Id");
            dataGridView2.DataSource = ds.Tables["Id"];                                   
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Microsoft.Office.Interop.Excel.Application xla = new Microsoft.Office.Interop.Excel.Application();
            //Workbook wb = xla.Workbooks.Add(XlSheetType.xlWorksheet);
            //Worksheet ws = (Worksheet)xla.ActiveSheet;

            //xla.Visible = true;

            //ws.Cells[1, 1] = "PRODUCTO";
            //ws.Cells[1, 2] = "CANTIDAD";
            //ws.Cells[1, 3] = "FECHA: " + (DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
            //for (int i = 0; i < dataGridView2.RowCount; i++)
            //{
            //    double suma=0;
            //    ws.Cells[(i + 2), 1] = dataGridView2[0, i].Value.ToString();
            //    for (int n = 0; n < dataGridView1.RowCount; n++)
            //    {
            //        if (dataGridView1[3, n].Value.ToString() == dataGridView2[0, i].Value.ToString())
            //        {
            //            suma += Convert.ToDouble(dataGridView1[2, n].Value.ToString());
            //        }
            //        ws.Cells[(i + 2), 2] = suma;
            //    }
            //}

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
