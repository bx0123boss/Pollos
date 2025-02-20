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
    public partial class frmCombos : Form
    {
        public frmCombos()
        {
            InitializeComponent();
            this.MinimumSize = new Size(890, 697);
            this.MaximumSize= new Size(890, 697);
        }

        private void frmCombos_Load(object sender, EventArgs e)
        {
            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();
                DataSet ds = new DataSet();
                using (SqlDataAdapter da = new SqlDataAdapter("SELECT * from Promos ORDER BY NOMBRE;", conectar))
                {
                    da.Fill(ds, "Productos");
                }
                dataGridView1.DataSource = ds.Tables["Productos"];
                if (dataGridView1.Columns.Count > 0)
                {
                    dataGridView1.Columns[0].Visible = false;
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            frmAgregarPromo prom = new frmAgregarPromo();
            prom.ShowDialog();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                frmAgregarPromo agg = new frmAgregarPromo();
                agg.id = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
                agg.txtNombre.Text = dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString();
                agg.txtPrecio.Text = dataGridView1[3, dataGridView1.CurrentRow.Index].Value.ToString();
                agg.cbLunes.Checked = bool.Parse(dataGridView1[4, dataGridView1.CurrentRow.Index].Value.ToString());
                agg.cbMartes.Checked = bool.Parse(dataGridView1[5, dataGridView1.CurrentRow.Index].Value.ToString());
                agg.cbMiercoles.Checked = bool.Parse(dataGridView1[6, dataGridView1.CurrentRow.Index].Value.ToString());
                agg.cbJueves.Checked = bool.Parse(dataGridView1[7, dataGridView1.CurrentRow.Index].Value.ToString());
                agg.cbViernes.Checked = bool.Parse(dataGridView1[8, dataGridView1.CurrentRow.Index].Value.ToString());
                agg.cbSabado.Checked = bool.Parse(dataGridView1[9, dataGridView1.CurrentRow.Index].Value.ToString());
                agg.cbDomingo.Checked = bool.Parse(dataGridView1[10, dataGridView1.CurrentRow.Index].Value.ToString());
                agg.ShowDialog();
                this.Close();
            }catch(Exception ex)
            {
                MessageBox.Show("Tiene que seleccionar un combo antes", "Reporte de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("¿Estás seguro de eliminar el Producto?", "Alto!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
                {
                    conectar.Open();
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Promos WHERE IdPromo = @Id;", conectar))
                    {
                        cmd.Parameters.AddWithValue("@Id", dataGridView1[0, dataGridView1.CurrentRow.Index].Value);
                        cmd.ExecuteNonQuery();
                    }

                    using (SqlCommand cmd = new SqlCommand("DELETE FROM ArticulosPromo WHERE IdPromo = @Id;", conectar))
                    {
                        cmd.Parameters.AddWithValue("@Id", dataGridView1[0, dataGridView1.CurrentRow.Index].Value);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Se ha eliminado la promoción con éxito", "Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    DataSet ds = new DataSet();
                    using (SqlDataAdapter da = new SqlDataAdapter("SELECT * from Promos ORDER BY NOMBRE;", conectar))
                    {
                        da.Fill(ds, "Productos");
                    }
                    dataGridView1.DataSource = ds.Tables["Productos"];
                    if (dataGridView1.Columns.Count > 0)
                    {
                        dataGridView1.Columns[0].Visible = false;
                    }
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                DataSet ds = new DataSet();
                if (textBox1.Text != "")
                {   
                    using (SqlDataAdapter da = new SqlDataAdapter(
                        "SELECT * FROM Promos WHERE Nombre LIKE @Nombre ORDER BY NOMBRE;",
                    conectar))
                    {
                        da.SelectCommand.Parameters.AddWithValue("@Nombre", "%" + textBox1.Text + "%");
                        da.Fill(ds, "Productos");
                    }
                    dataGridView1.DataSource = ds.Tables["Productos"];
                    dataGridView1.Columns[0].Visible = false;
                    

                }
                else
                {
                    using (SqlDataAdapter da = new SqlDataAdapter("SELECT * from Promos ORDER BY NOMBRE;", conectar))
                    {
                        da.Fill(ds, "Productos");
                    }
                    dataGridView1.DataSource = ds.Tables["Productos"];
                    if (dataGridView1.Columns.Count > 0)
                    {
                        dataGridView1.Columns[0].Visible = false;
                    }
                }
            }
        }
    }
}
