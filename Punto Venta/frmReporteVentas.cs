using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Punto_Venta
{
    public partial class frmReporteVentas : Form
    {
        private DataSet ds;
        OleDbConnection conectar = new OleDbConnection(Conexion.CadCon); 
        OleDbDataAdapter da;
        public string usuario = "";
        public frmReporteVentas()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                frmVentaDetallada detalles = new frmVentaDetallada();
                //detalles.idMesero = Convert.ToInt32(dataGridView1[4, dataGridView1.CurrentRow.Index].Value.ToString());
                //detalles.lblMesero.Text = dataGridView1[5, dataGridView1.CurrentRow.Index].Value.ToString();
                detalles.lblFolio.Text = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
                detalles.lblFecha.Text = dataGridView1[8, dataGridView1.CurrentRow.Index].Value.ToString();
                detalles.lblModalidad.Text = dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString();
                detalles.usuario = usuario;
                if (dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString() == "CANCELADO")
                {
                    detalles.button2.Hide();
                }
                //detalles.lblMonto.Text = dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString();
                detalles.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiene que seleccionar un folio antes", "Reporte de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmReporteVentas_Load(object sender, EventArgs e)
        {            
            conectar.Open();
            ds = new DataSet();
            da = new OleDbDataAdapter("Select * from folios where Fecha >=#" + dateTimePicker1.Value.Month.ToString() + "/" + dateTimePicker1.Value.Day.ToString() + "/" + dateTimePicker1.Value.Year.ToString() + " 00:00:00# and Fecha <=#" + dateTimePicker1.Value.Month.ToString() + "/" + dateTimePicker1.Value.Day.ToString() + "/" + dateTimePicker1.Value.Year.ToString() + " 23:59:59# ORDER BY Fecha DESC;", conectar);
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

        private void dateTimePicker1_CloseUp(object sender, EventArgs e)
        {

            ds = new DataSet();
            da = new OleDbDataAdapter("Select * from folios where Fecha >=#" + dateTimePicker1.Value.Month.ToString() + "/" + dateTimePicker1.Value.Day.ToString() + "/" + dateTimePicker1.Value.Year.ToString() + " 00:00:00# and Fecha <=#" + dateTimePicker1.Value.Month.ToString() + "/" + dateTimePicker1.Value.Day.ToString() + "/" + dateTimePicker1.Value.Year.ToString() + " 23:59:59#;", conectar);
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

        private void button2_Click(object sender, EventArgs e)
        {
            frmArticulosCancelados art = new frmArticulosCancelados();
            art.Show();
            this.Close();
        }
    }
}
