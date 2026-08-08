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
                        cmd2.Parameters.AddWithValue("@Concepto", $"ENTRADA DE EFECTIVO: {txtConcepto.Text}");
                        cmd2.Parameters.AddWithValue("@Total", txtIngreso.Text);
                        cmd2.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Se ha ingresado a caja correctamente", "Listo", MessageBoxButtons.OK, MessageBoxIcon.Information);


                // =========================================================
                // IMPRESIÓN DEL COMPROBANTE DE RETIRO CON LA CLASE GENÉRICA
                // =========================================================
                DialogResult printResult = MessageBox.Show("¿Desea imprimir el comprobante de ingreso?", "Imprimir", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (printResult == DialogResult.Yes)
                {
                    GenericTicketPrinter ticket = new GenericTicketPrinter();

                    ticket.AddTitle("COMPROBANTE DE INGRESO")
                          .AddText("ENTRADA A CAJA CHICA", true, System.Drawing.StringAlignment.Center)
                          .AddLine()
                          .AddRow("FECHA:", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"))
                          .AddRow("USUARIO:", !string.IsNullOrEmpty(usuario) ? usuario : "CAJERO")
                          .AddLine()
                          .AddText("CONCEPTO:", true)
                          .AddText(txtConcepto.Text.Trim())
                          .AddSpace(5)
                          .AddRow("MONTO INGRESADO:", $"${monto:N2}", true)
                          .AddLine()
                          .AddSpace(10)
                          // CAMPOS PARA LLENAR A LAPICERO
                          .AddFillField("ENTREGADO POR (NOMBRE):")
                          .AddSpace(15)
                          .AddFillField("FIRMA DE CONFORMIDAD:")
                          .AddSpace(10)
                          .AddText("Conserve este comprobante para su arqueo.", false, System.Drawing.StringAlignment.Center);

                    // Reemplaza por tu variable global de impresora si aplica
                    ticket.Print();
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el movimiento: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

