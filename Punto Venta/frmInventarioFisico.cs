using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data.OleDb;
using Microsoft.Office.Interop.Excel;

namespace Punto_Venta
{
    public partial class frmInventarioFisico : Form
    {
        public bool invitado;
        private DataSet ds;
        OleDbConnection conectar = new OleDbConnection(Conexion.CadCon); 
        OleDbDataAdapter da;
        OleDbCommand cmd;

        public frmInventarioFisico()
        {
            InitializeComponent();
            conectar.Open();
        }

        private void frmInventarioFisico_Load(object sender, EventArgs e)
        {
            if (invitado)
            {
               
            }
            else
            {
                button4.Visible = false;
                button6.Visible = false;
                dgvInventario.Visible = false;
                dataGridView1.Location = new System.Drawing.Point(12, 12);
                dataGridView1.Size = new Size(1213, 726);
            }
            ds = new DataSet();
            da = new OleDbDataAdapter("select * from articulos ORDER BY Nombre;", conectar);
            da.Fill(ds, "Id");
            dgvInventario.DataSource = ds.Tables["Id"];

            ds = new DataSet();
            da = new OleDbDataAdapter("select * from InventFisico;", conectar);
            da.Fill(ds, "Id");
            dataGridView1.DataSource = ds.Tables["Id"];
           
        }

        private void button1_Click(object sender, EventArgs e)
        {

            cmd = new OleDbCommand("delete from InventFisico where 1;", conectar);
            cmd.ExecuteNonQuery();
            for (int i = 0; i < dgvInventario.RowCount; i++)
            {
                cmd = new OleDbCommand("insert into InventFisico(Nombre,Medida) values('" + dgvInventario[1, i].Value.ToString() + "','" + dgvInventario[3, i].Value.ToString() + "');", conectar);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("ELIMINADO");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                cmd = new OleDbCommand("UPDATE InventFisico set Cantidad='" + dataGridView1[2, i].Value.ToString() + "' where Id="+dataGridView1[0, i].Value.ToString()+";", conectar);
               //MessageBox.Show("UPDATE InventFisico set Cantidad='" + dataGridView1[2, i].Value.ToString() + "' where Id=" + dataGridView1[0, i].Value.ToString() + ";");
                cmd.ExecuteNonQuery();
            }
            MessageBox.Show("ACTUALIZADO");
            ds = new DataSet();
            
            da = new OleDbDataAdapter("select * from InventFisico;", conectar);
            da.Fill(ds, "Id");
            dataGridView1.DataSource = ds.Tables["Id"];
        }

        private void button4_Click(object sender, EventArgs e)
        {
            frmAgregarExistencias agregar = new frmAgregarExistencias();
            agregar.lista = "FISICO";
            agregar.lblProducto.Text = dgvInventario[1, dgvInventario.CurrentRow.Index].Value.ToString();
            agregar.txtActuales.Text = dgvInventario[2, dgvInventario.CurrentRow.Index].Value.ToString();
            agregar.lblID.Text = dgvInventario[0, dgvInventario.CurrentRow.Index].Value.ToString();
            agregar.Show();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            frmDescontarExistencias agregar = new frmDescontarExistencias();
            agregar.lista = "FISICO";
            agregar.lblProducto.Text = dgvInventario[1, dgvInventario.CurrentRow.Index].Value.ToString();
            agregar.txtActuales.Text = dgvInventario[2, dgvInventario.CurrentRow.Index].Value.ToString();
            agregar.lblID.Text = dgvInventario[0, dgvInventario.CurrentRow.Index].Value.ToString();
            agregar.Show();
            this.Close();
        }
    }
}
