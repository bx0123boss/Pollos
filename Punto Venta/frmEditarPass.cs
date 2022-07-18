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
    public partial class frmEditarPass : Form
    {
        OleDbConnection conectar = new OleDbConnection(Conexion.CadCon);
        OleDbCommand cmd;
        public int id;

        public frmEditarPass()
        {
            InitializeComponent();
            conectar.Open();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtPass.Text == txtPass2.Text)
            {
                cmd = new OleDbCommand("select Usuario from Usuarios where Contraseña='" + txtPass.Text + "';", conectar);
                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    MessageBox.Show("Error en la contraseña, favor de verificar", "Editar Usuario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPass.Clear();
                    txtPass2.Clear();
                    txtPass.Focus();
                }
                else
                {
                    cmd = new OleDbCommand("UPDATE Usuarios set Contraseña='" + txtPass.Text + "' where Id=" + id + ";", conectar);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("¡Se ha editado la contraseña con exito!", "Editar Contraseña", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    conectar.Close();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Las contraseñas no son identicas", "Agregar Usuario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPass.Clear();
                txtPass2.Clear();
                txtPass.Focus();
            }
        }

        private void frmEditarPass_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmUsuarios user = new frmUsuarios();
            user.Show();
        }
    }
}
