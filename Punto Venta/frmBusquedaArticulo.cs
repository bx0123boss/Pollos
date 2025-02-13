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
    public partial class frmBusquedaArticulo : Form
    {      

        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Medida { get; set; }
        public string Precio { get; set; }

        public frmBusquedaArticulo()
        {
            InitializeComponent();
        }

        private void frmBusquedaArticulo_Load(object sender, EventArgs e)
        {
            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            using (SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Productos;", conectar))
            {
                conectar.Open();
                DataSet ds = new DataSet();
                da.Fill(ds, "Id");
                dataGridView1.DataSource = ds.Tables["Id"];
                dataGridView1.Columns[0].Visible = false;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Id = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
            Nombre = dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString();
            Medida = dataGridView1[3, dataGridView1.CurrentRow.Index].Value.ToString();
            Precio = dataGridView1[5, dataGridView1.CurrentRow.Index].Value.ToString();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;    
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();
                DataSet ds = new DataSet();

                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Productos ORDER BY Nombre;", conectar))
                    {
                        da.Fill(ds, "Id");
                    }
                }
                else
                {
                    using (SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Productos WHERE Nombre LIKE @Nombre;", conectar))
                    {
                        da.SelectCommand.Parameters.AddWithValue("@Nombre", $"%{textBox1.Text}%");
                        da.Fill(ds, "Id");
                    }
                }

                dataGridView1.DataSource = ds.Tables["Id"];
                dataGridView1.Columns[0].Visible = false;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
