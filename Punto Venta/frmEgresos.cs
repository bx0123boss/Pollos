using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Punto_Venta
{
    public partial class frmEgresos : frmBase
    {
       
        public string usuario;
        public frmEgresos()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
                {
                    conectar.Open();
                    string query = @"INSERT INTO CORTE (Concepto, Total,FechaHora,FormaPago) VALUES
                                    (@Concepto, @Total, GETDATE(), 'EFECTIVO')";
                    using (SqlCommand cmd2 = new SqlCommand(query, conectar))
                    {
                        cmd2.Parameters.AddWithValue("@Concepto", $"SALIDA DE EFECTIVO: {txtConcepto.Text}");
                        cmd2.Parameters.AddWithValue("@Total", "-"+txtIngreso.Text);
                        cmd2.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Se ha retirado de caja correctamente", "Listo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch 
            {

            }
            this.Close();
        }

        private void txtIngreso_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar))//Si es número
            {
                e.Handled = false;
            }
            else if (e.KeyChar == (char)Keys.Back)//si es tecla borrar
            {
                e.Handled = false;
            }
            else //Si es otra tecla cancelamos
            {
                e.Handled = true;
            }
        }

        private void frmEgresos_Load(object sender, EventArgs e)
        {
            EstilizarBotonPrimario(button1);
        }
    }
}

