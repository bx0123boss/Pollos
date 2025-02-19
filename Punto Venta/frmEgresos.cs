using System;
using System.Windows.Forms;
using LibPrintTicket;
using System.Data.SqlClient;

namespace Punto_Venta
{
    public partial class frmEgresos : Form
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
                Ticket ticket2 = new Ticket();
                ticket2.MaxChar = 35;
                ticket2.MaxCharDescription = 22;
                ticket2.FontSize = 8;
                ticket2.AddHeaderLine("****** SALIDA DE EFECTIVO  *****");
                ticket2.AddSubHeaderLine("FECHA Y HORA:" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                ticket2.AddSubHeaderLine("Usuario:" + usuario);
                ticket2.AddItem("1", txtConcepto.Text, "$" + txtIngreso.Text);
                ticket2.PrintTicket(Conexion.impresora);
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
    }
}
