using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Punto_Venta
{
    public partial class frmUsuarios : Form
    {
        public frmUsuarios()
        {
            InitializeComponent();
        }

        private void frmUsuarios_Load(object sender, EventArgs e)
        {
            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            using (SqlDataAdapter da = new SqlDataAdapter("SELECT IdUsuario, Usuario, TipoUsuario FROM Usuarios;", conectar))
            {
                DataSet ds = new DataSet();
                da.Fill(ds, "Id");
                dataGridView1.DataSource = ds.Tables["Id"];
                dataGridView1.Columns[0].Visible = false;
            }
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
            }
            else if (MessageBox.Show("¿Estás seguro de eliminar el Usuario?", "Alto!",MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
                {
                    conectar.Open();
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Usuarios WHERE IdUsuario = @IdUsuario;", conectar))
                    {
                        cmd.Parameters.AddWithValue("@IdUsuario", dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString());
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Se ha eliminado el usuario con éxito", "ELIMINADO", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    using (SqlDataAdapter da = new SqlDataAdapter("SELECT IdUsuario, Usuario, TipoUsuario FROM Usuarios;", conectar))
                    {
                        DataSet ds = new DataSet();
                        da.Fill(ds, "Id");
                        dataGridView1.DataSource = ds.Tables["Id"];
                    }
                }
            }
        }
    }
}
