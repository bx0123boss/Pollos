using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Punto_Venta
{
    public partial class frmBotonesComentario : frmBase
    {
        public frmBotonesComentario()
        {
            InitializeComponent();
        }

        private void frmBotonesComentario_Load(object sender, EventArgs e)
        {

            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();
                DataSet ds = new DataSet();
                using (SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM BotonesComentario;", conectar))
                {
                    da.Fill(ds, "BotonesComentario"); 
                }
                dataGridView1.DataSource = ds.Tables["BotonesComentario"];
                if (dataGridView1.Columns.Count > 0)
                {
                    dataGridView1.Columns[0].Visible = false;
                }
            }
            EstilizarDataGridView(dataGridView1);
            EstilizarBotonPeligro(button1);
            EstilizarBotonPrimario(button3);
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmAgregarBotonesComentario CAT = new frmAgregarBotonesComentario();
            CAT.ShowDialog();
            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();
                DataSet ds = new DataSet();
                using (SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM BotonesComentario;", conectar))
                {
                    da.Fill(ds, "BotonesComentario");
                }

                dataGridView1.DataSource = ds.Tables["BotonesComentario"];
                if (dataGridView1.Columns.Count > 0)
                {
                    dataGridView1.Columns[0].Visible = false;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                return;
            }
            DialogResult dialogResult = MessageBox.Show("¿Estás seguro de eliminar el botón?", "Alto!", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
                {
                    conectar.Open();
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM BotonesComentario WHERE IdBotonesComentario = @Id;", conectar))
                    {
                        cmd.Parameters.AddWithValue("@Id", dataGridView1[0, dataGridView1.CurrentRow.Index].Value);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Se ha eliminado el botón con éxito", "ELIMINADO", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    DataSet ds = new DataSet();
                    using (SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM BotonesComentario;", conectar))
                    {
                        da.Fill(ds, "BotonesComentario");
                    }

                    dataGridView1.DataSource = ds.Tables["BotonesComentario"];
                    if (dataGridView1.Columns.Count > 0)
                    {
                        dataGridView1.Columns[0].Visible = false;
                    }
                }
            }
        }
    }
}

