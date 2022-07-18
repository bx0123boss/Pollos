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
    public partial class frmAgregarOrigen : Form
    {
        OleDbConnection conectar = new OleDbConnection(Conexion.CadCon);
        OleDbCommand cmd;
        public frmAgregarOrigen()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conectar.Open();
            cmd = new OleDbCommand("insert into Origen(Nombre) Values('" + txtNombre.Text + "');", conectar);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Se ha creado el origen con exito", "EXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            frmOrigen apart = new frmOrigen();
            apart.Show();
            this.Close();
        }
    }
}
