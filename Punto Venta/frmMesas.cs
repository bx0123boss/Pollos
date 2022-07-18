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
    public partial class frmMesas : Form
    {
        private DataSet ds;
        OleDbConnection conectar = new OleDbConnection(Conexion.CadCon);
        OleDbDataAdapter da;
        OleDbCommand cmd;
        public frmMesas()
        {
            InitializeComponent();
        }

        private void frmMesas_Load(object sender, EventArgs e)
        {
            ds = new DataSet();
            conectar.Open();
            da = new OleDbDataAdapter("select * from Mesas order by Nombre;", conectar);
            da.Fill(ds, "Id");
            dataGridView1.DataSource = ds.Tables["Id"];
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[5].Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmAgregarMesas mes = new frmAgregarMesas();
            mes.Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            frmCambiarMesa cambio = new frmCambiarMesa();
            cambio.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmMesasOcupadas me = new frmMesasOcupadas();
            me.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cmd = new OleDbCommand("DELETE FROM Mesas where Id=" + dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString() + ";", conectar);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Se ha eliminado la mesa", "Mesas", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ds = new DataSet();
            da = new OleDbDataAdapter("select * from Mesas order by Nombre;", conectar);
            da.Fill(ds, "Id");
            dataGridView1.DataSource = ds.Tables["Id"];
            dataGridView1.Columns[0].Visible = false;
        }
    }
}
