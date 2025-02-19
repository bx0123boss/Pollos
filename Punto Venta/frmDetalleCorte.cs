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
    public partial class frmDetalleCorte : Form
    {
        public int ID;
        
        public frmDetalleCorte()
        {
            InitializeComponent();
        }

        private void frmDetalleCorte_Load(object sender, EventArgs e)
        {
            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();
                DataSet ds = new DataSet();
                string query = @"SELECT * FROM CORTES
                                   WHERE IdHistorialCortes = @IdCortes";

                using (SqlDataAdapter da = new SqlDataAdapter(query, conectar))
                {
                    da.SelectCommand.Parameters.AddWithValue("@IdCortes", ID);

                    da.Fill(ds, "IdFolio");
                    dataGridView1.DataSource = ds.Tables["IdFolio"];
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[5].Visible = false;

                }
                DataSet ds2 = new DataSet();
                query = @"SELECT * FROM CortesMeseros
                                   WHERE IdHistorialCortes = @IdCorte";

                using (SqlDataAdapter da = new SqlDataAdapter(query, conectar))
                {
                    da.SelectCommand.Parameters.AddWithValue("@IdCorte", ID);

                    da.Fill(ds2, "IdFolio");
                    dataGridView2.DataSource = ds2.Tables["IdFolio"];
                    dataGridView2.Columns[0].Visible = false;
                    dataGridView2.Columns[1].Visible = false;

                }
            }
        }
    }
}
