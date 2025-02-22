using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Punto_Venta
{
    public partial class frmClientes : Form
    {
        public frmClientes()
        {
            InitializeComponent();
        }


        private void frmClientes_Load(object sender, EventArgs e)
        {
            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();
                DataSet ds = new DataSet();
                using (SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM CLIENTES ORDER BY NOMBRE;", conectar))
                {
                    da.Fill(ds, "Productos");
                    dataGridView1.DataSource = ds.Tables["Productos"];
                }
                dataGridView1.Columns[0].Visible = false;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();
                if (textBox1.Text == "")
                {
                    DataSet ds = new DataSet();
                    using (SqlDataAdapter da = new SqlDataAdapter(
                     "SELECT * FROM CLIENTES ORDER BY NOMBRE;",
                     conectar))
                    {
                        da.Fill(ds, "Productos");
                    }
                    dataGridView1.DataSource = ds.Tables["Productos"];
                    if (dataGridView1.Columns.Count > 0)
                    {
                        dataGridView1.Columns[0].Visible = false;
                    }
                }
                else
                {
                    DataSet ds = new DataSet();
                    using (SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM CLIENTES WHERE Nombre LIKE @Filtro " +
                        "OR Telefono LIKE @Filtro " +
                        "OR Direccion LIKE @Filtro " +
                        "OR Referencia LIKE @Filtro " +
                        "OR Colonia LIKE @Filtro " +
                        "ORDER BY Nombre;", conectar))
                    {
                        da.SelectCommand.Parameters.AddWithValue("@Filtro", "%" + textBox1.Text + "%");
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                return;
            }
            DialogResult dialogResult = MessageBox.Show("¿Estás seguro de eliminar el Cliente?", "Alto!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
                {
                    conectar.Open();
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM CLIENTES WHERE IdCliente = @Id;", conectar))
                    {
                        cmd.Parameters.AddWithValue("@Id", dataGridView1[0, dataGridView1.CurrentRow.Index].Value);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Se ha eliminado el Cliente con éxito", "Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    DataSet ds = new DataSet();
                    using (SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM CLIENTES ORDER BY NOMBRE;", conectar))
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
        private void button2_Click(object sender, EventArgs e)
        {
            using (frmAgregarCliente ori = new frmAgregarCliente())
            {
                if (ori.ShowDialog() == DialogResult.OK)
                {
                    using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
                    {
                        conectar.Open();
                        DataSet ds = new DataSet();
                        using (SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM CLIENTES ORDER BY NOMBRE;", conectar))
                        {
                            da.Fill(ds, "Productos");
                            dataGridView1.DataSource = ds.Tables["Productos"];
                        }
                        dataGridView1.Columns[0].Visible = false;
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (frmAgregarCliente add = new frmAgregarCliente())
            {
                add.Text = "Editar";
                add.id = Convert.ToInt32(dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString());
                add.txtNombre.Text = dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString();
                add.txtTelefono.Text = dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString();
                add.txtDireccion.Text = dataGridView1[3, dataGridView1.CurrentRow.Index].Value.ToString();
                add.txtReferencia.Text = dataGridView1[4, dataGridView1.CurrentRow.Index].Value.ToString();
                if (add.ShowDialog() == DialogResult.OK)
                {
                    using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
                    {
                        conectar.Open();
                        DataSet ds = new DataSet();
                        using (SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM CLIENTES ORDER BY NOMBRE;", conectar))
                        {
                            da.Fill(ds, "Productos");
                            dataGridView1.DataSource = ds.Tables["Productos"];
                        }
                        dataGridView1.Columns[0].Visible = false;
                    }
                }
            }
        }
    }
}
