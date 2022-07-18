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
    public partial class frmAgregarMesas : Form
    {
        OleDbConnection conectar = new OleDbConnection(Conexion.CadCon);
        OleDbCommand cmd; 
        public frmAgregarMesas()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conectar.Open();
            bool existe = false;
            cmd = new OleDbCommand("select Nombre from Mesas where Nombre='" + txtNombre.Text + "';", conectar);
            OleDbDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                existe = true;
            }
            if (existe)
            {
                MessageBox.Show("Existe una mesa similar, favor de verificar", "Agregar Mesas", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                cmd = new OleDbCommand("insert into Mesas(Nombre,Ubicacion) Values('" + txtNombre.Text + "','" + txtUbicacion.Text + "');", conectar);
                cmd.ExecuteNonQuery();
                MessageBox.Show("¡Se ha agregado la mesa con exito!", "Agregar Mesas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                conectar.Close();
                frmMesas mesa = new frmMesas();
                mesa.Show();
                this.Close();
            }
        }
    }
}
