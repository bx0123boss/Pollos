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
    public partial class frmDetalleCorte : Form
    {
        public int ID;
        private DataSet ds;
        OleDbConnection conectar = new OleDbConnection(Conexion.CadCon);
        OleDbDataAdapter da;

        public frmDetalleCorte()
        {
            InitializeComponent();
        }

        private void frmDetalleCorte_Load(object sender, EventArgs e)
        {
            conectar.Open();
            ds = new DataSet();
            da = new OleDbDataAdapter("select * from Cortes where idCorte='" + ID + "';", conectar);
            da.Fill(ds, "Id");
            dataGridView1.DataSource = ds.Tables["Id"];
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[3].Visible = false;


            ds = new DataSet();
            da = new OleDbDataAdapter("select Mesero,('$' + Ventas) as Ventas, Mesas as Mesas_Atendidas from MesaDet where idCorte='" + ID + "';", conectar);
            da.Fill(ds, "Id");
            dataGridView2.DataSource = ds.Tables["Id"];
        }
    }
}
