using LibPrintTicket;
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
    public partial class frmCortesMesero : Form
    {
        private DataSet ds;
        OleDbConnection conectar = new OleDbConnection(Conexion.CadCon);
        OleDbDataAdapter da;
        public string idMesero = "";
        public string nombre = "";
        public frmCortesMesero()
        {
            InitializeComponent();
        }

        private void frmCortesMesero_Load(object sender, EventArgs e)
        {
            conectar.Open(); 
            ds = new DataSet();
            da = new OleDbDataAdapter("Select * from folios where Estatus='COBRADO' and Vehiculo='" + idMesero + "' and Fecha >=#" + dateTimePicker1.Value.Month.ToString() + "/" + dateTimePicker1.Value.Day.ToString() + "/" + dateTimePicker1.Value.Year.ToString() + " 00:00:00# and Fecha <=#" + dateTimePicker1.Value.Month.ToString() + "/" + dateTimePicker1.Value.Day.ToString() + "/" + dateTimePicker1.Value.Year.ToString() + " 23:59:59#;", conectar);
            da.Fill(ds, "Id");
            dataGridView1.DataSource = ds.Tables["Id"];
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[10].Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                frmVentaDetallada detalles = new frmVentaDetallada();
                detalles.idMesero = Convert.ToInt32(dataGridView1[4, dataGridView1.CurrentRow.Index].Value.ToString());
                detalles.lblMesero.Text = dataGridView1[5, dataGridView1.CurrentRow.Index].Value.ToString();
                detalles.lblFolio.Text = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
                detalles.lblFecha.Text = dataGridView1[8, dataGridView1.CurrentRow.Index].Value.ToString();
                detalles.lblModalidad.Text = dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString();
                if (dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString() == "CANCELADO")
                {
                    detalles.button2.Hide();
                }
                //detalles.lblMonto.Text = dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString();
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
