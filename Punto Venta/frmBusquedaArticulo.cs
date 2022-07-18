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
    public partial class frmBusquedaArticulo : Form
    {
        private DataSet ds;
        OleDbConnection conectar = new OleDbConnection(Conexion.CadCon); 
        OleDbDataAdapter da;

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
            ds = new DataSet();
            conectar.Open();
            da = new OleDbDataAdapter("select * from articulos;", conectar);
            da.Fill(ds, "Id");
            dataGridView1.DataSource = ds.Tables["Id"];
            dataGridView1.Columns[0].Visible = false;
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
            if (textBox1.Text == "")
            {

                ds = new DataSet();
                da = new OleDbDataAdapter("select * from articulos order by Nombre;", conectar);
                da.Fill(ds, "Id");
                dataGridView1.DataSource = ds.Tables["Id"];
                dataGridView1.Columns[0].Visible = false;
            }
            else
            {
                ds = new DataSet();
                da = new OleDbDataAdapter("select * from articulos where Nombre LIKE '%" + textBox1.Text + "%';", conectar);
                da.Fill(ds, "Id");
                dataGridView1.DataSource = ds.Tables["Id"];
                dataGridView1.Columns[0].Visible = false;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
