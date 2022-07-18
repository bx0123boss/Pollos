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
using MySql.Data.MySqlClient;

namespace Punto_Venta
{
    public partial class frmPedidoRealizado : Form
    {

        OleDbConnection conectar = new OleDbConnection(Conexion.CadCon); 
        OleDbCommand cmd;
        MySqlCommand cmd2;

        public frmPedidoRealizado()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double total = Convert.ToDouble(lblCantidad.Text) * Convert.ToDouble(lblPrecio.Text);
            cmd = new OleDbCommand("select count(*) from mesa" + lblMesa.Text + ";", conectar);
            int valor = int.Parse(cmd.ExecuteScalar().ToString());
            if (valor == 0)
            {
                cmd = new OleDbCommand("update mesas set mesa" + lblMesa.Text + "=1 where id=1;", conectar);
                cmd.ExecuteNonQuery();
                cmd = new OleDbCommand("update mesas set mesa" + lblMesa.Text + "='" + lblMesero.Text + "' where id=2;", conectar);
                cmd.ExecuteNonQuery();
            }

            try
            {


                //cmd2 = new MySqlCommand("delete from pedido where id=" + Convert.ToInt32(lblID.Text) + ";", Conexion.obtenerConexion());
                //cmd2.ExecuteNonQuery();
                try
                {
                    cmd = new OleDbCommand("insert into mesa" + lblMesa.Text + " (id,cantidad, producto, precio, total) values ('" + lblIdProducto.Text + "','" + lblCantidad.Text + "','" + lblProducto.Text + "'," + lblPrecio.Text + ",'" + total + "');", conectar);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Se ha entregado la orden!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al conectar" + ex.ToString());
                }
                
                this.Close();
               

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar" + ex.ToString());
            }
        }

        private void frmPedidoRealizado_Load(object sender, EventArgs e)
        {
            conectar.Open();
            cmd = new OleDbCommand("select * from Inventario where Id=" + lblIdProducto.Text + ";", conectar);
                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    lblPrecio.Text = Convert.ToString(reader[2].ToString());
                }
        }

        private void frmPedidoRealizado_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmComanda comanda = new frmComanda();
            comanda.Show();
        }
    }
}
