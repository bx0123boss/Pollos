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
    public partial class frmCategorias : Form
    {
        private DataSet ds;
        public string tipo;
        public frmCategorias()
        {
            InitializeComponent();
        }

        private void frmCategorias_Load(object sender, EventArgs e)
        {
            this.Text = tipo;
            ds = new DataSet();

            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            using (SqlDataAdapter da = new SqlDataAdapter($"SELECT * FROM {tipo};", conectar))
            {
                conectar.Open();
                da.Fill(ds, "Id");
                dataGridView1.DataSource = ds.Tables["Id"];
                dataGridView1.Columns[0].Visible = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmAgregarCategorias CAT = new frmAgregarCategorias();
            CAT.tipo = tipo;
            CAT.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("¿Estás seguro de eliminar la Categoría?", "Alto!", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
                {
                    conectar.Open();
                    using (SqlCommand cmd = new SqlCommand($"DELETE FROM {tipo} WHERE Id{tipo.Substring(0, tipo.Length - 1)} = @IdCategoria;", conectar))
                    {
                        cmd.Parameters.AddWithValue("@IdCategoria", dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString());
                        cmd.ExecuteNonQuery();
                    }

                    using (SqlCommand cmd = new SqlCommand($"UPDATE Inventario SET Id{tipo.Substring(0, tipo.Length - 1)} = 0 WHERE Id{tipo.Substring(0, tipo.Length - 1)} = @IdCategoria;", conectar))
                    {
                        cmd.Parameters.AddWithValue("@IdCategoria", dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString());
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show($"Se ha eliminado la {tipo} con éxito", "ELIMINADO", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    using (SqlDataAdapter da = new SqlDataAdapter($"SELECT * FROM {tipo};", conectar))
                    {
                        DataSet ds = new DataSet();
                        da.Fill(ds, "Id");
                        dataGridView1.DataSource = ds.Tables["Id"];
                        dataGridView1.Columns[0].Visible = false;
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmAgregarCategorias cat = new frmAgregarCategorias();
            cat.tipo = tipo;
            cat.id = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
            //cat.nombre = dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString();
            cat.txtNombre.Text = dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString();
            cat.button2.BackColor = System.Drawing.ColorTranslator.FromHtml("#" + dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString());
            cat.button2.ForeColor = Color.FromName(dataGridView1[3, dataGridView1.CurrentRow.Index].Value.ToString());
            if (dataGridView1[3, dataGridView1.CurrentRow.Index].Value.ToString()=="Black")            
                cat.radioButton1.Checked = true;            
            else
                cat.radioButton2.Checked = true; 
            cat.button1.Text = "Editar";
            cat.Show();
            this.Close();
        }
    }
}
