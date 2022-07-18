using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Punto_Venta
{
    public partial class frmClientes : Form
    {
        private DataSet ds;
        OleDbConnection conectar = new OleDbConnection(Conexion.CadCon); 
        OleDbDataAdapter da;
        OleDbCommand cmd;
        public frmClientes()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (frmAgregarCliente ori = new frmAgregarCliente())
            {
                if (ori.ShowDialog() == DialogResult.OK)
                {
                    conectar.Close();
                    conectar.Open();
                    ds = new DataSet();
                    da = new OleDbDataAdapter("select * from Clientes;", conectar);
                    da.Fill(ds, "Id");
                    dataGridView1.DataSource = ds.Tables["Id"];
                    dataGridView1.Columns[0].Visible = false;

                }
            }
        }

        private void frmClientes_Load(object sender, EventArgs e)
        {
            ds = new DataSet();
            conectar.Open();
            da = new OleDbDataAdapter("select * from Clientes order by Nombre;", conectar);
            da.Fill(ds, "Id");
            dataGridView1.DataSource = ds.Tables["Id"];
            dataGridView1.Columns[0].Visible = false;
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

        private void button1_Click(object sender, EventArgs e)
        {
            cmd = new OleDbCommand("DELETE FROM Clientes where Id=" + dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString() + ";", conectar);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Se ha eliminado el cliente", "Clientes", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ds = new DataSet();
            da = new OleDbDataAdapter("select * from Clientes order by Nombre;", conectar);
            da.Fill(ds, "Id");
            dataGridView1.DataSource = ds.Tables["Id"];
            dataGridView1.Columns[0].Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmAgregarCliente add = new frmAgregarCliente();
            add.Text = "Editar";
            add.id = Convert.ToInt32(dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString());
            add.txtNombre.Text = dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString();
            add.txtTelefono.Text = dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString();
            add.txtDireccion.Text=dataGridView1[3, dataGridView1.CurrentRow.Index].Value.ToString();
            add.txtReferencia.Text = dataGridView1[4, dataGridView1.CurrentRow.Index].Value.ToString();
            add.Show();
            this.Close();
        }
    }
}
