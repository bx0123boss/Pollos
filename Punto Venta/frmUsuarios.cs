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
    public partial class frmUsuarios : Form
    {
        OleDbConnection conectar = new OleDbConnection(Conexion.CadCon);
        private DataSet ds;
        OleDbDataAdapter da;
        OleDbCommand cmd;
        public frmUsuarios()
        {
            InitializeComponent();
        }

        private void frmUsuarios_Load(object sender, EventArgs e)
        {
            ds = new DataSet();
            conectar.Open();
            da = new OleDbDataAdapter("select Id,Usuario,TipoUsuario from Usuarios;", conectar);
            da.Fill(ds, "Id");
            dataGridView1.DataSource = ds.Tables["Id"];
            dataGridView1.Columns[0].Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmAgregarUsuario add = new frmAgregarUsuario();
            add.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmEditarPass edit = new frmEditarPass();
            edit.id = Convert.ToInt32(dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString());
            edit.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString() == "Administrador")
            {
                MessageBox.Show("No se puede eliminar al Administrador del sistema", "Alto!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ds = new DataSet();
            }
            else
            {
                cmd = new OleDbCommand("delete from Usuarios where Id=" + dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString() + ";", conectar);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Se ha eliminado el usuario con exito", "ELIMINADO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ds = new DataSet();
                da = new OleDbDataAdapter("select Id,Usuario,TipoUsuario from Usuarios;", conectar);
                da.Fill(ds, "Id");
                dataGridView1.DataSource = ds.Tables["Id"];
            }
        }
    }
}
