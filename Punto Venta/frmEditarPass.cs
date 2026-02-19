using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Punto_Venta
{
    public partial class frmEditarPass : Form
    {
        public int id;

        public frmEditarPass()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtPass.Text == txtPass2.Text)
            {
                using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
                {
                    conectar.Open();
                    string Query = @"SELECT Usuario from Usuarios where Contraseña WHERE IdUsuario = @Id;";
                    using (SqlCommand cmd = new SqlCommand(Query, conectar))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        using (SqlDataReader sqlReader = cmd.ExecuteReader())
                        {
                            if (sqlReader.Read())
                            {
                                MessageBox.Show("Error en la contraseña, favor de verificar", "Editar Usuario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtPass.Clear();
                                txtPass2.Clear();
                                txtPass.Focus();
                            }
                            else
                            {
                                using (SqlCommand cmd2 = new SqlCommand("UPDATE Usuarios set Contraseña = @Pass WHERE IdUsuario = @Id;", conectar))
                                {
                                    cmd2.Parameters.AddWithValue("@Pass", txtPass.Text);
                                    cmd2.ExecuteNonQuery();

                                    MessageBox.Show("¡Se ha editado la contraseña con exito!", "Editar Contraseña", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    this.Close();
                                }
                            }
                        }
                    }
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
