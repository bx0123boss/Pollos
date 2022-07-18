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
    public partial class frmCambio : Form
    {
        OleDbDataAdapter da;
        OleDbCommand cmd;
        private DataSet ds;
        //OleDbConnection conectar = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=\\192.168.0.15\Jaeger Soft\Restaurante.accdb");
        OleDbConnection conectar = new OleDbConnection(Conexion.CadCon); 
        string meseroO;
        public frmCambio()
        {
            InitializeComponent();
        }

        private void frmCambio_Load(object sender, EventArgs e)
        {


            ds = new DataSet();
            conectar.Open();
            da = new OleDbDataAdapter("select * from mesa" + lblID.Text + ";", conectar);
            da.Fill(ds, "Id");
            dgvCambio.DataSource = ds.Tables["Id"];

            ds = new DataSet();
            da = new OleDbDataAdapter("select * from mesa" + lblID2.Text + ";", conectar);
            da.Fill(ds, "Id");
            dgvDestino.DataSource = ds.Tables["Id"];

            int num = 0;
            int origen = 0;
            for (int i = 0; i < dgvDestino.RowCount; i++)
            {
                num++;
            }
            for (int i = 0; i < dgvCambio.RowCount; i++)
            {
                origen++;
            }
            if (num == 0)
            {
                if (origen != 0)
                {
                    for (int i = 0; i < dgvCambio.RowCount; i++)
                    {
                        cmd = new OleDbCommand("insert into mesa" + lblID2.Text + " (id,cantidad, producto, precio, total) values ('" + dgvCambio[0, i].Value.ToString() + "','" + dgvCambio[1, i].Value.ToString() + "','" + dgvCambio[2, i].Value.ToString() + "','" + dgvCambio[3, i].Value.ToString() + "','" + dgvCambio[4, i].Value.ToString() + "');", conectar);
                        cmd.ExecuteNonQuery();
                    }

                    cmd = new OleDbCommand("delete from mesa" + lblID.Text + " where 1;", conectar);
                    cmd.ExecuteNonQuery();
                    cmd = new OleDbCommand("update mesas set mesa" + lblIndex1.Text + "=0 where id=1;", conectar);
                    cmd.ExecuteNonQuery();
                    cmd = new OleDbCommand("update mesas set mesa" + lblIndex2.Text + "=1 where id=1;", conectar);
                    cmd.ExecuteNonQuery();
                    cmd = new OleDbCommand("select mesa" + lblID.Text + " from mesas where Id=2;", conectar);
                    OleDbDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        meseroO= Convert.ToString(reader[0].ToString());
                    }
                    cmd = new OleDbCommand("update mesas set mesa" + lblIndex1.Text + "=0 where id=2;", conectar);
                    cmd.ExecuteNonQuery();
                    cmd = new OleDbCommand("update mesas set mesa" + lblIndex2.Text + "='"+meseroO+"' where id=2;", conectar);
                    cmd.ExecuteNonQuery();
                   
                    MessageBox.Show("Cambio Realizado con Exito!", "Cambio hecho", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                    MessageBox.Show("No existen productos en la mesa de origen, por favor, revise que la mesa de origen sea la correcta", "Cuidado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            else
            {
                MessageBox.Show("Existen productos en la mesa de destino, por favor, libere la mesa de destino", "Cuidado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
