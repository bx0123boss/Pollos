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
    public partial class frmComandaGeneral : Form
    {
        private DataSet ds;
        OleDbConnection conectar = new OleDbConnection(Conexion.CadCon); 
        OleDbDataAdapter da;
        OleDbCommand cmd;
        public frmComandaGeneral()
        {
            InitializeComponent();
            conectar.Open();
        }

        private void frmComandaGeneral_Load(object sender, EventArgs e)
        {
            ds = new DataSet();
            da = new OleDbDataAdapter("select Folio,ModalidadVenta,Estatus,idCliente,Fecha,Colonia from folios where Estatus='COCINA';", conectar);
            da.Fill(ds, "Id");
            dgvCocina.DataSource = ds.Tables["Id"];
            dgvCocina.Columns[3].Visible = false;

            ds = new DataSet();
            da = new OleDbDataAdapter("select * from Comanda;", conectar);
            da.Fill(ds, "Id");
            dgvComanda.DataSource = ds.Tables["Id"];
            dgvComanda.Columns[0].Visible = false;
            dgvComanda.Columns[1].Visible = false;
            dgvComanda.Columns[4].Visible = false;
            dgvComanda.Columns[5].Visible = false;


            ds = new DataSet();
            da = new OleDbDataAdapter("select * from folios where Estatus='RUTA';", conectar);
            da.Fill(ds, "Id");
            dgvRuta.DataSource = ds.Tables["Id"];
        }

        private void BtnEntregarComanda_Click(object sender, EventArgs e)
        {
            frmEntregarRuta entrega = new frmEntregarRuta();
            entrega.idCliente = dgvRuta[3, dgvRuta.CurrentRow.Index].Value.ToString();
            entrega.lblFolio.Text = dgvRuta[0, dgvRuta.CurrentRow.Index].Value.ToString();
            entrega.lblVehiculo.Text = dgvRuta[4, dgvRuta.CurrentRow.Index].Value.ToString();
            entrega.lblChofer.Text = dgvRuta[5, dgvRuta.CurrentRow.Index].Value.ToString();
            entrega.lblCambio.Text = "$" + dgvRuta[6, dgvRuta.CurrentRow.Index].Value.ToString();
            entrega.cambio = Convert.ToDouble(dgvRuta[6, dgvRuta.CurrentRow.Index].Value.ToString());
            entrega.lblFecha.Text = dgvRuta[8, dgvRuta.CurrentRow.Index].Value.ToString();
            entrega.lblFechaRuta.Text = dgvRuta[9, dgvRuta.CurrentRow.Index].Value.ToString();
            entrega.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            frmEntregarCocina entregar = new frmEntregarCocina();
            entregar.lblFolio2.Text = dgvCocina[0, dgvCocina.CurrentRow.Index].Value.ToString();
            entregar.lblFolio.Text = dgvCocina[0, dgvCocina.CurrentRow.Index].Value.ToString();
            entregar.idCliente = dgvCocina[3, dgvCocina.CurrentRow.Index].Value.ToString();
            entregar.Show();
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void frmComandaGeneral_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void frmComandaGeneral_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cmd = new OleDbCommand("insert into mesa" + dgvComanda[7, dgvComanda.CurrentRow.Index].Value.ToString() + "(id,Cantidad,Producto,Precio,Total) values('" + dgvComanda[1, dgvComanda.CurrentRow.Index].Value.ToString() + "','" + dgvComanda[2, dgvComanda.CurrentRow.Index].Value.ToString() + "','" + dgvComanda[3, dgvComanda.CurrentRow.Index].Value.ToString() + "','" + dgvComanda[4, dgvComanda.CurrentRow.Index].Value.ToString() + "','" + dgvComanda[5, dgvComanda.CurrentRow.Index].Value.ToString() + "');", conectar);
            cmd.ExecuteNonQuery();
            cmd = new OleDbCommand("delete from Comanda where Id=" + dgvComanda[0, dgvComanda.CurrentRow.Index].Value.ToString() + ";", conectar);
            cmd.ExecuteNonQuery();
            cmd = new OleDbCommand("update mesas set mesa" + dgvComanda[7, dgvComanda.CurrentRow.Index].Value.ToString() + "=1 where id=1;", conectar);
            cmd.ExecuteNonQuery();

            MessageBox.Show("PRODUCTO ENTREGADO!", "ENTREGADO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            ds = new DataSet();
            da = new OleDbDataAdapter("select Folio,ModalidadVenta,Estatus,idCliente,Fecha from folios where Estatus='COCINA';", conectar);
            da.Fill(ds, "Id");
            dgvCocina.DataSource = ds.Tables["Id"];

            ds = new DataSet();
            da = new OleDbDataAdapter("select * from Comanda;", conectar);
            da.Fill(ds, "Id");
            dgvComanda.DataSource = ds.Tables["Id"];
            dgvComanda.Columns[0].Visible = false;
            dgvComanda.Columns[1].Visible = false;
            dgvComanda.Columns[4].Visible = false;
            dgvComanda.Columns[5].Visible = false;


            ds = new DataSet();
            da = new OleDbDataAdapter("select * from folios where Estatus='RUTA';", conectar);
            da.Fill(ds, "Id");
            dgvRuta.DataSource = ds.Tables["Id"];
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cmd = new OleDbCommand("update folios set Estatus='CANCELADO' Where Folio='" + dgvCocina[0, dgvCocina.CurrentRow.Index].Value.ToString() + "'", conectar);
            cmd.ExecuteNonQuery();
            MessageBox.Show("ORDEN CANCELADA CON EXITO", "Comanda General", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            ds = new DataSet();
            da = new OleDbDataAdapter("select Folio,ModalidadVenta,Estatus,idCliente,Fecha from folios where Estatus='COCINA';", conectar);
            da.Fill(ds, "Id");
            dgvCocina.DataSource = ds.Tables["Id"];
        }

        private void BtnCancelarComanda_Click(object sender, EventArgs e)
        {
            cmd = new OleDbCommand("update folios set Estatus='CANCELADO' Where Folio='" + dgvRuta[0, dgvRuta.CurrentRow.Index].Value.ToString() + "'", conectar);
            cmd.ExecuteNonQuery();
            MessageBox.Show("ORDEN CANCELADA CON EXITO", "Comanda General", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            ds = new DataSet();
            da = new OleDbDataAdapter("select * from folios where Estatus='RUTA';", conectar);
            da.Fill(ds, "Id");
            dgvRuta.DataSource = ds.Tables["Id"];
        }
    }
}
