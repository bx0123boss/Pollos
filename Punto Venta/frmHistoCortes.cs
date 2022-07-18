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
    public partial class frmHistoCortes : Form
    {
        private DataSet ds;
        OleDbConnection conectar = new OleDbConnection(Conexion.CadCon); 
        OleDbDataAdapter da; 
        public frmHistoCortes()
        {
            InitializeComponent();
        }

        private void frmHistoCortes_Load(object sender, EventArgs e)
        {            
            conectar.Open();            
        }

        private void dateTimePicker1_CloseUp(object sender, EventArgs e)
        {

            ds = new DataSet();
            da = new OleDbDataAdapter("Select * from histocortes where Fecha >=#" + dateTimePicker1.Value.Month.ToString() + "/" + dateTimePicker1.Value.Day.ToString() + "/" + dateTimePicker1.Value.Year.ToString() + " 00:00:00# and Fecha <=#" + dateTimePicker1.Value.Month.ToString() + "/" + dateTimePicker1.Value.Day.ToString() + "/" + dateTimePicker1.Value.Year.ToString() + " 23:59:59#;", conectar);
            da.Fill(ds, "Id");
            dataGridView1.DataSource = ds.Tables["Id"];
            dataGridView1.Columns[0].Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                frmDetalleCorte detail = new frmDetalleCorte();
                detail.ID = Convert.ToInt32(dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString());
                detail.lblMonto.Text = dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString();
                detail.lblFecha.Text = dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString();
                detail.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiene que seleccionar un folio antes", "Reporte de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
