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
using System.Data.SqlClient;

namespace Punto_Venta
{
    public partial class frmHistoCortes : Form
    {
       
        public frmHistoCortes()
        {
            InitializeComponent();
        }

        private void frmHistoCortes_Load(object sender, EventArgs e)
        {
            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();
                DataSet ds = new DataSet();
                string query = @"SELECT *  FROM HistorialCortes
                                   WHERE FechaHora >= @StartDate AND FechaHora <= @EndDate ORDER BY FechaHora DESC;";

                using (SqlDataAdapter da = new SqlDataAdapter(query, conectar))
                {
                    da.SelectCommand.Parameters.AddWithValue("@StartDate", dateTimePicker1.Value.Date);
                    da.SelectCommand.Parameters.AddWithValue("@EndDate", dateTimePicker1.Value.Date.AddDays(1).AddSeconds(-1));

                    da.Fill(ds, "IdFolio");
                    dataGridView1.DataSource = ds.Tables["IdFolio"];
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns["Monto"].DefaultCellStyle.Format = "N2";
                }
            }
        }

        private void dateTimePicker1_CloseUp(object sender, EventArgs e)
        {
            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();
                DataSet ds = new DataSet();
                string query = @"SELECT *  FROM HistorialCortes
                                   WHERE FechaHora >= @StartDate AND FechaHora <= @EndDate ORDER BY FechaHora DESC;";

                using (SqlDataAdapter da = new SqlDataAdapter(query, conectar))
                {
                    da.SelectCommand.Parameters.AddWithValue("@StartDate", dateTimePicker1.Value.Date);
                    da.SelectCommand.Parameters.AddWithValue("@EndDate", dateTimePicker1.Value.Date.AddDays(1).AddSeconds(-1));

                    da.Fill(ds, "IdFolio");
                    dataGridView1.DataSource = ds.Tables["IdFolio"];
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns["Monto"].DefaultCellStyle.Format = "N2";
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                frmDetalleCorte detail = new frmDetalleCorte();
                detail.ID = Convert.ToInt32(dataGridView1.CurrentRow.Cells["IdHistorialCortes"].Value.ToString());
                detail.lblMonto.Text = $"{Convert.ToDouble(dataGridView1.CurrentRow.Cells["Monto"].Value.ToString()):C}";
                detail.lblFecha.Text = dataGridView1.CurrentRow.Cells["FechaHora"].Value.ToString();
                detail.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiene que seleccionar un folio antes \n" + ex.ToString(), "Reporte de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
