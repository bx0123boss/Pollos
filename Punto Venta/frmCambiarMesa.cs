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
    public partial class frmCambiarMesa : Form
    {
        OleDbConnection conectar = new OleDbConnection(Conexion.CadCon);
        OleDbDataAdapter da;
        OleDbDataAdapter da2;
        OleDbCommand cmd;
        OleDbCommand cmd2;
        private DataSet ds;
        public frmCambiarMesa()
        {
            InitializeComponent();
            conectar.Open();
        }

        private void button1_Click(object sender, EventArgs e)
        {
                cmd = new OleDbCommand("UPDATE ArticulosMesa set Mesa='" + cmbDestino.SelectedValue.ToString() + "' Where Mesa='" + cmbOrigen.SelectedValue.ToString() + "';", conectar);
                cmd.ExecuteNonQuery();
                cmd = new OleDbCommand("select IdMesero,Mesero,Print from Mesas where Id=" + cmbOrigen.SelectedValue.ToString()+";", conectar);
                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    frmCobros cobrar = new frmCobros();
                    cobrar.lblID.Text = reader[0].ToString();
                    cmd = new OleDbCommand("UPDATE Mesas set IdMesero='" + reader[0] + "', Mesero='" + reader[1] + "', Print='" + reader[2] + "' Where Id=" + cmbDestino.SelectedValue.ToString() + ";", conectar);
                    cmd.ExecuteNonQuery();
                    cmd = new OleDbCommand("UPDATE Mesas set IdMesero='', Mesero='', Print='1' Where Id=" + cmbOrigen.SelectedValue.ToString() + ";", conectar);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Se ha cambiado la mesa con exito", "Cambiar Mesa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                                
        }

        private void frmCambiarMesa_Load(object sender, EventArgs e)
        {
            Dictionary<string, string> ocupadas = new Dictionary<string, string>();
            Dictionary<string, string> libres = new Dictionary<string, string>();
            ds = new DataSet();
            da = new OleDbDataAdapter("select * from mesas order by Id;", conectar);
            da.Fill(ds, "Id");
            dataGridView1.DataSource = ds.Tables["Id"];
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                string id = dataGridView1[0, i].Value.ToString();
                cmd = new OleDbCommand("SELECT * FROM ArticulosMesa where Mesa='" + id + "';", conectar);
                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    //OCUPADO
                    ocupadas.Add(dataGridView1[0, i].Value.ToString(), dataGridView1[1, i].Value.ToString());
                }
                else
                {
                    //LIBRE
                    libres.Add(dataGridView1[0, i].Value.ToString(), dataGridView1[1, i].Value.ToString());
                }
            }
            cmbOrigen.DataSource = new BindingSource(ocupadas, null);
            cmbOrigen.DisplayMember = "Value";
            cmbOrigen.ValueMember = "Key";

            cmbDestino.DataSource = new BindingSource(libres, null);
            cmbDestino.DisplayMember = "Value";
            cmbDestino.ValueMember = "Key";

            //DataTable dt = new DataTable();
            ////cmd = new OleDbCommand("SELECT DISTINCT Categoria from Inventario;", conectar);
            //cmd = new OleDbCommand("SELECT * from Mesas;", conectar);
            //da = new OleDbDataAdapter(cmd);
            //da.Fill(dt);
            //cmbOrigen.DisplayMember = "Nombre";
            //cmbOrigen.ValueMember = "Id";
            //cmbOrigen.DataSource = dt;
            //cmbOrigen.Text = "";


            //DataTable dt2 = new DataTable();
            ////cmd = new OleDbCommand("SELECT DISTINCT Categoria from Inventario;", conectar);
            //cmd2 = new OleDbCommand("SELECT * from Mesas;", conectar);
            //da2 = new OleDbDataAdapter(cmd2);
            //da2.Fill(dt2);
            //cmbDestino.DisplayMember = "Nombre";
            //cmbDestino.ValueMember = "Id";
            //cmbDestino.DataSource = dt2;
            //cmbDestino.Text = "";
            
            
        }

        private void label1_Click(object sender, EventArgs e)
        {


        }
    }
}
