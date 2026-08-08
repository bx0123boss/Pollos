using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Punto_Venta
{
    public partial class frmPago : Form
    {
        public double cambio { get; set; }
        public double efectivo { get; set; }
        public double total { get; set; }

        public Dictionary<string, double> PagosRealizados { get; set; }

        public frmPago()
        {
            InitializeComponent();
            PagosRealizados = new Dictionary<string, double>();
        }

        private void frmPago_Load(object sender, EventArgs e)
        {
            txtTotal.Text = $"{total:C}";
            txtRestante.Text = $"{total:C}";
            textBox3.Text = "$0.00";

            // Vincular el objetivo de escritura de UC_Numpad a textBox2
            ucNumpad1.TargetTextBox = textBox2;
            ucNumpad1.EnterPressed += UcNumpad1_EnterPressed;

            string[] opcionesPago = {
                "01=EFECTIVO",
                "03=TRANSFERENCIA ELECTRONICA DE FONDOS",
                "04=TARJETA BANCARIA",
                "PROPINA"
            };
            cmbPago.Items.AddRange(opcionesPago);
            cmbPago.SelectedIndex = 0;

            chkMixto.Checked = false;
            ConfigurarDiseno();
        }

        // Evento disparado al pulsar 'OK' en el UC_Numpad
        private void UcNumpad1_EnterPressed(object sender, EventArgs e)
        {
            if (chkMixto.Checked)
            {
                AgregarMonto();
            }
            else
            {
                if (double.TryParse(textBox2.Text.Replace("$", ""), out double capturado))
                {
                    ProcesarCobroSimple(capturado);
                }
                else
                {
                    MessageBox.Show("Ingresa una cantidad válida", "ALTO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void chkMixto_CheckedChanged(object sender, EventArgs e)
        {
            ConfigurarDiseno();
        }

        private void ConfigurarDiseno()
        {
            bool mixto = chkMixto.Checked;

            lblMetodo.Visible = mixto;
            cmbPago.Visible = mixto;
            btnAgregar.Visible = mixto;
            dgvPagos.Visible = mixto;
            btnLimpiar.Visible = mixto;
            lblRestante.Visible = mixto;
            txtRestante.Visible = mixto;

            if (mixto)
            {
                label2.Top = 115;
                textBox2.Top = 109;
                label2.Text = "MONTO:";

                label3.Top = 365;
                textBox3.Top = 361;

                PagosRealizados.Clear();
                ActualizarPagos();
                cmbPago.SelectedIndex = 0;
                textBox2.Clear();
            }
            else
            {
                label2.Top = 65;
                textBox2.Top = 59;
                label2.Text = "EFECTIVO:";

                label3.Top = 115;
                textBox3.Top = 109;

                PagosRealizados.Clear();
                textBox2.Clear();
                textBox3.Text = "$0.00";
                efectivo = 0;
                cambio = 0;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != (char)Keys.Enter && e.KeyChar != '.')
            {
                e.Handled = true;
                return;
            }

            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
                return;
            }

            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                UcNumpad1_EnterPressed(sender, e);
            }
        }

        #region --- BOTONERA RÁPIDA DE BILLETES (TOUCH) ---

        private void btnBillete_Click(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                string valorLimpio = new string(btn.Text.Where(char.IsDigit).ToArray());
                if (double.TryParse(valorLimpio, out double billete))
                {
                    double.TryParse(textBox2.Text.Replace("$", ""), out double actual);

                    double nuevoMonto = actual + billete;
                    textBox2.Text = nuevoMonto.ToString("0.00");

                    if (!chkMixto.Checked && nuevoMonto >= total)
                    {
                        ProcesarCobroSimple(nuevoMonto);
                    }
                }
            }
        }

        private void btnExacto_Click(object sender, EventArgs e)
        {
            double sumaActual = PagosRealizados.Where(p => !p.Key.Contains("PROPINA")).Sum(p => p.Value);
            double restante = total - sumaActual;

            textBox2.Text = restante > 0 ? restante.ToString("0.00") : total.ToString("0.00");

            if (!chkMixto.Checked)
            {
                ProcesarCobroSimple(total);
            }
        }

        #endregion

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            AgregarMonto();
        }

        private void AgregarMonto()
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text)) return;
            if (!double.TryParse(textBox2.Text.Replace("$", ""), out double montoAgregado)) return;

            string metodo = cmbPago.Text;

            double sumaActual = PagosRealizados.Where(p => !p.Key.Contains("PROPINA")).Sum(p => p.Value);
            double restante = total - sumaActual;

            if (montoAgregado > 0)
            {
                if (!metodo.Contains("EFECTIVO") && !metodo.Contains("PROPINA") && montoAgregado > restante)
                {
                    MessageBox.Show($"El pago con {metodo} no puede ser mayor al saldo restante ({restante:C}).\n\nSolo el Efectivo puede generar cambio.",
                                    "Monto Excedido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox2.SelectAll();
                    return;
                }

                if (PagosRealizados.ContainsKey(metodo))
                {
                    PagosRealizados[metodo] += montoAgregado;
                }
                else
                {
                    PagosRealizados.Add(metodo, montoAgregado);
                }
            }

            ActualizarPagos();
            textBox2.Clear();
        }

        private void ActualizarPagos()
        {
            dgvPagos.Rows.Clear();
            double sumaVenta = 0;
            efectivo = 0;

            foreach (var pago in PagosRealizados)
            {
                dgvPagos.Rows.Add(pago.Key, $"{pago.Value:C}");

                if (!pago.Key.Contains("PROPINA"))
                {
                    sumaVenta += pago.Value;
                }

                if (pago.Key.Contains("EFECTIVO"))
                {
                    efectivo += pago.Value;
                }
            }

            double restante = total - sumaVenta;

            if (restante <= 0)
            {
                txtRestante.Text = "$0.00";
                cambio = Math.Abs(restante);
                textBox3.Text = $"{cambio:C}";
                btnAceptar.Focus();
            }
            else
            {
                txtRestante.Text = $"{restante:C}";
                cambio = 0;
                textBox3.Text = "$0.00";
            }
        }

        private void ProcesarCobroSimple(double capturado)
        {
            if (capturado < total)
            {
                textBox2.Clear();
                MessageBox.Show("Ingresa una cantidad válida o mayor al total.", "Monto Insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                efectivo = capturado;
                cambio = capturado - total;
                textBox3.Text = $"{cambio:C}";
                textBox2.Text = $"{capturado:C}";
                btnAceptar.Focus();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            PagosRealizados.Clear();
            ActualizarPagos();
            textBox2.Clear();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (!btnAceptar.Enabled) return;

            if (chkMixto.Checked)
            {
                double sumaVenta = PagosRealizados.Where(p => !p.Key.Contains("PROPINA")).Sum(p => p.Value);

                if (sumaVenta >= total)
                {
                    btnAceptar.Enabled = false;

                    if (cambio > 0)
                    {
                        string llaveEfectivo = PagosRealizados.Keys.FirstOrDefault(k => k.Contains("EFECTIVO"));
                        if (llaveEfectivo != null)
                        {
                            PagosRealizados[llaveEfectivo] -= cambio;
                            cambio = 0;
                        }
                    }
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("Falta cubrir el total de la venta.", "Alto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                if (double.TryParse(textBox2.Text.Replace("$", ""), out double capturado) && capturado >= total)
                {
                    efectivo = capturado;
                    cambio = capturado - total;
                }

                if (efectivo >= total)
                {
                    btnAceptar.Enabled = false;
                    PagosRealizados.Clear();
                    PagosRealizados.Add("01=EFECTIVO", total);

                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("Aún no ha ingresado el efectivo correcto.", "Alto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}