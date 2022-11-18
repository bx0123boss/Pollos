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
using LibPrintTicket;

namespace Punto_Venta
{
    public partial class frmComanda : Form
    {
        //int n = 0;
        //MySqlCommand cmd;
        //MySqlDataAdapter ad;


        public frmComanda()
        {
            InitializeComponent();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {

        }

        private void frmComanda_Load(object sender, EventArgs e)
        {
            //timer1.Enabled = true;
            //DataTable dtDatos = new DataTable();
            //ad = new MySqlDataAdapter("select * from pedido order by id;", Conexion.obtenerConexion());
            //ad.Fill(dtDatos);
            //dgvComanda.DataSource = dtDatos;
            //dgvComanda.Columns[0].Visible = false;
            //dgvComanda.Columns[5].Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmPedidoRealizado entregar = new frmPedidoRealizado();
            entregar.lblCantidad.Text = dgvComanda[2, dgvComanda.CurrentRow.Index].Value.ToString();
            entregar.lblID.Text=dgvComanda[0, dgvComanda.CurrentRow.Index].Value.ToString();
            entregar.lblMesero.Text = dgvComanda[6, dgvComanda.CurrentRow.Index].Value.ToString();
            entregar.lblMesa.Text = dgvComanda[1, dgvComanda.CurrentRow.Index].Value.ToString();
            entregar.lblProducto.Text = dgvComanda[3, dgvComanda.CurrentRow.Index].Value.ToString();
            entregar.lblComent.Text = dgvComanda[4, dgvComanda.CurrentRow.Index].Value.ToString();
            entregar.lblIdProducto.Text = dgvComanda[5, dgvComanda.CurrentRow.Index].Value.ToString();
            entregar.Show();
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //n++;
            //if (n == 10)
            //{
            //    DataTable dtDatos = new DataTable();
            //    ad = new MySqlDataAdapter("select * from pedido order by id;", Conexion.obtenerConexion());
            //    ad.Fill(dtDatos);
            //    dgvComanda.DataSource = dtDatos;
            //    dgvComanda.Columns[0].Visible = false;
            //    dgvComanda.Columns[5].Visible = false;
            //    n = 0;
            //}
            //lblTiempo.Text = "" + n;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //cmd = new MySqlCommand("delete from pedido where id=" +dgvComanda[0, dgvComanda.CurrentRow.Index].Value.ToString() + ";", Conexion.obtenerConexion());
            //cmd.ExecuteNonQuery();
            //MessageBox.Show("Se ha eliminado la orden con exito!");
            //DataTable dtDatos = new DataTable();
            //ad = new MySqlDataAdapter("select * from pedido;", Conexion.obtenerConexion());
            //ad.Fill(dtDatos);
            //dgvComanda.DataSource = dtDatos;
            //dgvComanda.Columns[0].Visible = false;
            //dgvComanda.Columns[5].Visible = false;
            //n = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Ticket ticket = new Ticket();
            ticket.MaxChar = 34;
            ticket.FontSize = 9;
            ticket.AddItem("1", dgvComanda[3, dgvComanda.CurrentRow.Index].Value.ToString(), "");
            ticket.AddFooterLine(dgvComanda[4, dgvComanda.CurrentRow.Index].Value.ToString());
            ticket.PrintTicket("print");
            frmPedidoRealizado entregar = new frmPedidoRealizado();
            entregar.lblCantidad.Text = dgvComanda[2, dgvComanda.CurrentRow.Index].Value.ToString();
            entregar.lblID.Text = dgvComanda[0, dgvComanda.CurrentRow.Index].Value.ToString();
            entregar.lblMesa.Text = dgvComanda[1, dgvComanda.CurrentRow.Index].Value.ToString();
            entregar.lblProducto.Text = dgvComanda[3, dgvComanda.CurrentRow.Index].Value.ToString();
            entregar.lblComent.Text = dgvComanda[4, dgvComanda.CurrentRow.Index].Value.ToString();
            entregar.lblIdProducto.Text = dgvComanda[5, dgvComanda.CurrentRow.Index].Value.ToString();
            entregar.Show();
            this.Close();
        }
    }
}
