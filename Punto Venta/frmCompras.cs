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
    public partial class frmCompras : Form
    {
        private DataSet ds;
        OleDbConnection conectar = new OleDbConnection(Conexion.CadCon);
        OleDbDataAdapter da;
        OleDbCommand cmd;
        public string usuario = "";
        public frmCompras()
        {
            InitializeComponent();
        }

        private void frmCompras_Load(object sender, EventArgs e)
        {
            ds = new DataSet();
            conectar.Open();
            da = new OleDbDataAdapter("select * from Compras;", conectar);
            da.Fill(ds, "Id");
            dataGridView1.DataSource = ds.Tables["Id"];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmAgregarCompras com = new frmAgregarCompras();
            com.usuario = usuario; 
            com.Show();
            this.Close();
        }

        private void dateTimePicker1_CloseUp(object sender, EventArgs e)
        {
            ds = new DataSet();
            da = new OleDbDataAdapter("Select * from Compras where Fecha >=#" + dateTimePicker1.Value.Month.ToString() + "/" + dateTimePicker1.Value.Day.ToString() + "/" + dateTimePicker1.Value.Year.ToString() + " 00:00:00# and Fecha <=#" + dateTimePicker1.Value.Month.ToString() + "/" + dateTimePicker1.Value.Day.ToString() + "/" + dateTimePicker1.Value.Year.ToString() + " 23:59:59#;", conectar);
            da.Fill(ds, "Id");
            dataGridView1.DataSource = ds.Tables["Id"];
        }

        private void button4_Click(object sender, EventArgs e)
        {
            frmVerCompra detalles = new frmVerCompra();
            detalles.lblUser.Text = dataGridView1[3, dataGridView1.CurrentRow.Index].Value.ToString();
            detalles.id = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
            detalles.lblDocRel.Text = dataGridView1[4, dataGridView1.CurrentRow.Index].Value.ToString();
            detalles.lblMovimiento.Text = dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString();
            detalles.lblFecha.Text = dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString();
            detalles.Show();
            this.Close();
        }
    }
}
