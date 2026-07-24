using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Punto_Venta
{
    public partial class frmIngreso : frmBase
    {
        public string usuario;
        public frmIngreso()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();
                string query = @"INSERT INTO CORTE (Concepto, Total,FechaHora,FormaPago) VALUES
                                    (@Concepto, @Total, GETDATE(), 'EFECTIVO')";
                using (SqlCommand cmd2 = new SqlCommand(query, conectar))
                {
                    cmd2.Parameters.AddWithValue("@Concepto", $"ENTRADA DE EFECTIVO: {txtConcepto.Text}");
                    cmd2.Parameters.AddWithValue("@Total", txtIngreso.Text);
                    cmd2.ExecuteNonQuery();
                }
            }
            MessageBox.Show("Se ha ingresado a caja correctamente", "Listo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void txtIngreso_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void frmIngreso_Load(object sender, EventArgs e)
        {
            EstilizarBotonPrimario(button1);
        }
    }
}

