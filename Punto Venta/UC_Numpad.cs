using System;
using System.Drawing;
using System.Windows.Forms;

namespace Punto_Venta
{
    public partial class UC_Numpad : UserControl
    {
        // Evento que se dispara cuando se presiona cualquier botón numérico o de control
        public event EventHandler<string> KeyPressed;

        // Evento opcional para cuando se presiona la tecla ENTER/Aceptar del pad
        public event EventHandler EnterPressed;

        // Propiedad opcional para asociar directamente un TextBox objetivo
        public TextBox TargetTextBox { get; set; }

        public UC_Numpad()
        {
            InitializeComponent();
        }

        // Método genérico para vincular a los eventos Click de los botones 0-9 y punto
        private void Button_Click(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                string key = btn.Text;

                if (TargetTextBox != null && TargetTextBox.Enabled)
                {
                    if (key == "." && TargetTextBox.Text.Contains("."))
                        return; // Evita puntos dobles

                    TargetTextBox.Text += key;
                    TargetTextBox.SelectionStart = TargetTextBox.Text.Length;
                }

                KeyPressed?.Invoke(this, key);
            }
        }

        private void btnBackspace_Click(object sender, EventArgs e)
        {
            if (TargetTextBox != null && TargetTextBox.Text.Length > 0)
            {
                TargetTextBox.Text = TargetTextBox.Text.Substring(0, TargetTextBox.Text.Length - 1);
            }
            KeyPressed?.Invoke(this, "BACK");
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (TargetTextBox != null)
            {
                TargetTextBox.Clear();
            }
            KeyPressed?.Invoke(this, "CLEAR");
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            EnterPressed?.Invoke(this, EventArgs.Empty);
        }
    }
}