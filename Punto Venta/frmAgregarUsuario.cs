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
    public partial class frmAgregarUsuario : Form
    {
        OleDbConnection conectar = new OleDbConnection(Conexion.CadCon);
        OleDbCommand cmd; 

        public frmAgregarUsuario()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtPass.Text == txtPass2.Text)
            {
                
                bool existe = false;
                cmd = new OleDbCommand("select Usuario from Usuarios where Usuario='" + txtNombre.Text + "';", conectar);
                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    existe = true;
                }

                cmd = new OleDbCommand("select Usuario from Usuarios where Contraseña='" + txtPass.Text + "';", conectar);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    existe = true;
                }
                if (existe)
                {
                    MessageBox.Show("Existe un usuario similar, favor de verificar", "Agregar Usuario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    cmd = new OleDbCommand("insert into Usuarios(Usuario,Contraseña,TipoUsuario,Ventas,Mesas) Values('" + txtNombre.Text + "','" + txtPass.Text + "','" + txtTipo.Text + "','0','0');", conectar);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("¡Se ha agregado al usuario con exito!", "Agregar Usuario", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void frmAgregarUsuario_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmUsuarios users = new frmUsuarios();
            users.Show();
        }

        private void frmAgregarUsuario_Load(object sender, EventArgs e)
        {
            conectar.Open();
        }
    }
}
