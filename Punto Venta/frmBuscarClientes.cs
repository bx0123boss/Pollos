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
    public partial class frmBuscarClientes : Form
    {
        private DataSet ds;
        OleDbConnection conectar = new OleDbConnection(Conexion.CadCon); 
        OleDbDataAdapter da;
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Referencia { get; set; }
        public string Colonia { get; set; }

        public frmBuscarClientes()
        {
            InitializeComponent();
        }

        private void frmBuscarClientes_Load(object sender, EventArgs e)
        {
            ds = new DataSet();
            conectar.Open();
            da = new OleDbDataAdapter("select * from Clientes;", conectar);
            da.Fill(ds, "Id");
            dataGridView1.DataSource = ds.Tables["Id"];
            dataGridView1.Columns[0].Visible = false;
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
            if (textBox1.Text == "")
            {

                ds = new DataSet();
                da = new OleDbDataAdapter("select * from Clientes order by Nombre;", conectar);
                da.Fill(ds, "Id");
                dataGridView1.DataSource = ds.Tables["Id"];
                dataGridView1.Columns[0].Visible = false;
            }
            else
            {
                ds = new DataSet();
                da = new OleDbDataAdapter("select * from Clientes where Nombre LIKE '%" + textBox1.Text + "%';", conectar);
                da.Fill(ds, "Id");
                dataGridView1.DataSource = ds.Tables["Id"];
                dataGridView1.Columns[0].Visible = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (frmAgregarCliente ori = new frmAgregarCliente())
            {
                ori.txtTelefono.Text = textBox2.Text;
                if (ori.ShowDialog() == DialogResult.OK)
                {
                    conectar.Close();
                    conectar.Open();
                    ds = new DataSet();
                    da = new OleDbDataAdapter("select * from Clientes where Telefono LIKE '%" + ori.Nombre + "%';", conectar);
                    da.Fill(ds, "Id");
                    dataGridView1.DataSource = ds.Tables["Id"];
                    dataGridView1.Columns[0].Visible = false;
                    
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {

                ds = new DataSet();
                da = new OleDbDataAdapter("select * from Clientes order by Nombre;", conectar);
                da.Fill(ds, "Id");
                dataGridView1.DataSource = ds.Tables["Id"];
                dataGridView1.Columns[0].Visible = false;
            }
            else
            {
                ds = new DataSet();
                da = new OleDbDataAdapter("select * from Clientes where Telefono LIKE '%" + textBox2.Text + "%';", conectar);
                da.Fill(ds, "Id");
                dataGridView1.DataSource = ds.Tables["Id"];
                dataGridView1.Columns[0].Visible = false;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo se permiten numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }
    }
}
