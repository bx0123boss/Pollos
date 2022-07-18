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
    public partial class frmMesa : Form
    {
        OleDbConnection conectar = new OleDbConnection(Conexion.CadCon);
        OleDbDataAdapter da;
        OleDbCommand cmd;
        public frmMesa()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmCobros cobrar = new frmCobros();
            cobrar.lblID.Text = comboBox1.SelectedValue.ToString();;
            cobrar.lblMesa.Text = comboBox1.Text;
            cobrar.Show();
            this.Close();                      
        }

        private void frmMesa_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            cmd = new OleDbCommand("SELECT * from Mesas;", conectar);
            da = new OleDbDataAdapter(cmd);
            da.Fill(dt);
            comboBox1.DisplayMember = "Nombre";
            comboBox1.ValueMember = "Id";
            comboBox1.DataSource = dt;
            comboBox1.Text = "";
        }
    }
}
