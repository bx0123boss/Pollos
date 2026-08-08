using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Punto_Venta
{
    public partial class frmAgregarBotonesComentario: frmBase
    {
        public frmAgregarBotonesComentario()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open(); 
                using (SqlCommand cmdVerificar = new SqlCommand("SELECT COUNT(*) FROM BotonesComentario WHERE Nombre = @Nombre;", conectar))
                {
                    cmdVerificar.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                    int count = Convert.ToInt32(cmdVerificar.ExecuteScalar());

                    if (count > 0)
                    {
                        MessageBox.Show("El botón ya existe en la base de datos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return; 
                    }
                }
                using (SqlCommand cmd = new SqlCommand("INSERT INTO BotonesComentario (Nombre) VALUES (@Nombre);", conectar))
                {
                    cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Se ha creado el botón con éxito", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }

        private void frmAgregarBotonesComentario_Load(object sender, EventArgs e)
        {
            EstilizarTextBox(txtNombre);
            EstilizarBotonPrimario(button1);
        }
    }
}

