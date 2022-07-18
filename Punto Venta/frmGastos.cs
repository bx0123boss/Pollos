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
    public partial class frmGastos : Form
    {
        private DataSet ds;
        OleDbConnection conectar = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Jaeger Soft\FastFood.accdb");
        OleDbDataAdapter da;
        OleDbCommand cmd;
        double costos = 0, gastos = 0;
        public frmGastos()
        {
            InitializeComponent();
        }

        private void frmGastos_Load(object sender, EventArgs e)
        {
            ds = new DataSet();
            conectar.Open();
            da = new OleDbDataAdapter("SELECT Id,Nombre,CostoTotal,CostoFinal from Inventario;", conectar);
            da.Fill(ds, "Id");
            dataGridView1.DataSource = ds.Tables["Id"];

            ds = new DataSet();
            da = new OleDbDataAdapter("select * from Gastos order by Nombre;", conectar);
            da.Fill(ds, "Id");
            dataGridView2.DataSource = ds.Tables["Id"];
            dataGridView2.Columns[0].Visible = false;

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                costos += Convert.ToDouble(dataGridView1[2, i].Value.ToString());
            }

            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                gastos += Convert.ToDouble(dataGridView2[2, i].Value.ToString());
            }
            lblCosto.Text = "$" + costos;
            lblGastos.Text = "$" + gastos;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cmd = new OleDbCommand("INSERT INTO Gastos (Nombre, Total) VALUES ('" + txtNombre.Text + "','" + txtTotal.Text + "');", conectar);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Se ha agregado el producto correctamente", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            ds = new DataSet();
            da = new OleDbDataAdapter("select * from Gastos order by Nombre;", conectar);
            da.Fill(ds, "Id");
            dataGridView2.DataSource = ds.Tables["Id"];
            dataGridView2.Columns[0].Visible = false;
            gastos = 0;
            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                gastos += Convert.ToDouble(dataGridView2[2, i].Value.ToString());
            }
            lblGastos.Text = "$" + gastos;
            txtNombre.Clear();
            txtTotal.Clear();
            txtNombre.Focus();
        }

        private void frmGastos_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmEditarGasto edita = new frmEditarGasto();
            edita.id = dataGridView2[0, dataGridView2.CurrentRow.Index].Value.ToString();
            edita.txtProducto.Text = dataGridView2[1, dataGridView2.CurrentRow.Index].Value.ToString();
            edita.txtCantidad.Text = dataGridView2[2, dataGridView2.CurrentRow.Index].Value.ToString();
            edita.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cmd = new OleDbCommand("delete from Gastos where id=" + dataGridView2[0, dataGridView2.CurrentRow.Index].Value.ToString() + ";", conectar);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Se ha eliminado con exito el gasto", "Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ds = new DataSet();
            da = new OleDbDataAdapter("select * from gastos order by Nombre;", conectar);
            da.Fill(ds, "Id");
            dataGridView2.DataSource = ds.Tables["Id"];
            dataGridView2.Columns[0].Visible = false;
            gastos = 0;
            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                gastos += Convert.ToDouble(dataGridView2[2, i].Value.ToString());
            }
            lblGastos.Text = "$" + gastos;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                double porcentaje = ((100 * Convert.ToDouble(dataGridView1[2, i].Value.ToString())) / costos)/100;
                double CostoFinal=(Convert.ToDouble(dataGridView1[2, i].Value.ToString()))+(gastos*porcentaje);
                //MessageBox.Show("UPDATE Gastos set CostoFinal='" + CostoFinal + "' Where id=" + dataGridView1[0, i].Value.ToString() + ";");
                cmd = new OleDbCommand("UPDATE Inventario set CostoFinal='" + CostoFinal + "' Where id=" + dataGridView1[0, i].Value.ToString() + ";", conectar);
                cmd.ExecuteNonQuery();
                
            }
            MessageBox.Show("Se han calculado los gastos fijos con exito", "Gastos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ds = new DataSet();
            conectar.Open();
            da = new OleDbDataAdapter("SELECT Id,Nombre,CostoTotal,CostoFinal from Inventario;", conectar);
            da.Fill(ds, "Id");
            dataGridView1.DataSource = ds.Tables["Id"];
        }
    }
}
