using System;
using System.Data;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace Punto_Venta
{
    public partial class frmBuscarClientes : Form
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Referencia { get; set; }
        public string Colonia { get; set; }

        public frmBuscarClientes()
        {
            InitializeComponent();
            this.MinimumSize = new System.Drawing.Size(883, 548);
            this.MaximumSize = new System.Drawing.Size(883, 548);
        }

        private void frmBuscarClientes_Load(object sender, EventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            Id = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
            Nombre = dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString();
            Telefono = dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString();
            Direccion = dataGridView1[3, dataGridView1.CurrentRow.Index].Value.ToString();
            Referencia = dataGridView1[4, dataGridView1.CurrentRow.Index].Value.ToString();
            Colonia = dataGridView1[5, dataGridView1.CurrentRow.Index].Value.ToString();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;    
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();
                if (textBox2.Text == "")
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
                        da.SelectCommand.Parameters.AddWithValue("@Filtro", "%" + textBox2.Text + "%");
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
                        using (SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM CLIENTES WHERE Telefono = @Filtro ORDER BY NOMBRE;", conectar))
                        {
                            da.SelectCommand.Parameters.AddWithValue("@Filtro", ori.Nombre);
                            da.Fill(ds, "Productos");
                            dataGridView1.DataSource = ds.Tables["Productos"];
                        }
                        dataGridView1.Columns[0].Visible = false;
                        Id = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
                        Nombre = dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString();
                        Telefono = dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString();
                        Direccion = dataGridView1[3, dataGridView1.CurrentRow.Index].Value.ToString();
                        Referencia = dataGridView1[4, dataGridView1.CurrentRow.Index].Value.ToString();
                        Colonia = dataGridView1[5, dataGridView1.CurrentRow.Index].Value.ToString();
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    }
                }
            }
        }
    }
}
