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
            if (string.IsNullOrWhiteSpace(txtIngreso.Text) || string.IsNullOrWhiteSpace(txtConcepto.Text))
            {
                MessageBox.Show("Por favor complete todos los campos.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            double monto = 0;
            double.TryParse(txtIngreso.Text, out monto);
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
                        cmd2.Parameters.AddWithValue("@Total", "-" + txtIngreso.Text);
                        cmd2.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Se ha retirado de caja correctamente", "Listo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // =========================================================
                // IMPRESIÓN DEL COMPROBANTE DE RETIRO CON LA CLASE GENÉRICA
                // =========================================================
                DialogResult printResult = MessageBox.Show("¿Desea imprimir el comprobante de retiro?", "Imprimir", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (printResult == DialogResult.Yes)
                {
                    GenericTicketPrinter ticket = new GenericTicketPrinter("", 280);

                    ticket.AddTitle("COMPROBANTE DE EGRESO")
                          .AddText("RETIRO DE CAJA CHICA", true, System.Drawing.StringAlignment.Center)
                          .AddLine()
                          .AddRow("FECHA:", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"))
                          .AddRow("USUARIO:", !string.IsNullOrEmpty(usuario) ? usuario : "CAJERO")
                          .AddLine()
                          .AddText("CONCEPTO:", true)
                          .AddText(txtConcepto.Text.Trim())
                          .AddSpace(5)
                          .AddRow("MONTO RETIRADO:", $"${monto:N2}", true)
                          .AddLine()
                          .AddSpace(10)
                          // CAMPOS PARA LLENAR A LAPICERO
                          .AddFillField("RECIBI CONFORME (NOMBRE):")
                          .AddSpace(15)
                          .AddFillField("FIRMA DE CONFORMIDAD:")
                          .AddSpace(10)
                          .AddText("Conserve este comprobante para su arqueo.", false, System.Drawing.StringAlignment.Center);

                    ticket.Print(Conexion.impresora);
                }
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

