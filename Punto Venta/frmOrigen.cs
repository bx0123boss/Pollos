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
    public partial class frmOrigen : Form
    {
        private DataSet ds;
        OleDbConnection conectar = new OleDbConnection(Conexion.CadCon);
        OleDbDataAdapter da;
        OleDbCommand cmd;

        public frmOrigen()
        {
            InitializeComponent();
        }

        private void frmOrigen_Load(object sender, EventArgs e)
        {

            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();
                DataSet ds = new DataSet();
                using (SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Origen;", conectar))
                {
                    // Llenar el DataSet con los resultados de la consulta
                    da.Fill(ds, "Origen"); // Cambia "Id" por "Origen" para mayor claridad
                }

                // Asignar el DataTable al DataGridView
                dataGridView1.DataSource = ds.Tables["Origen"];

                // Ocultar la primera columna (si es necesario)
                if (dataGridView1.Columns.Count > 0)
                {
                    dataGridView1.Columns[0].Visible = false;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmAgregarOrigen CAT = new frmAgregarOrigen();
            CAT.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("¿Estás seguro de eliminar el Origen?", "Alto!", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
                {
                    conectar.Open();
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Origen WHERE IdOrigen = @Id;", conectar))
                    {
                        cmd.Parameters.AddWithValue("@Id", dataGridView1[0, dataGridView1.CurrentRow.Index].Value);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Se ha eliminado el Origen con éxito", "ELIMINADO", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    DataSet ds = new DataSet();
                    using (SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Origen;", conectar))
                    {
                        da.Fill(ds, "Origen");
                    }

                    dataGridView1.DataSource = ds.Tables["Origen"];
                    if (dataGridView1.Columns.Count > 0)
                    {
                        dataGridView1.Columns[0].Visible = false;
                    }
                }
            }
        }
    }
}
