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
    public partial class frmEditarGasto : Form
    {
        OleDbConnection conectar = new OleDbConnection(Conexion.CadCon); 
        OleDbCommand cmd;
        public string id;

        public frmEditarGasto()
        {
            InitializeComponent();
        }

        private void frmEditarGasto_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            conectar.Open();
            cmd = new OleDbCommand("UPDATE Gastos set Nombre='" + txtProducto.Text + "', Total='" + txtCantidad.Text + "' Where id=" + id + ";", conectar);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Se ha actualizado el gasto correctamente", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
            frmInventario invent = new frmInventario();            
            invent.Show();
            this.Close();
        }

        private void frmEditarGasto_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmGastos gas = new frmGastos();
            gas.Show();
        }
    }
}
