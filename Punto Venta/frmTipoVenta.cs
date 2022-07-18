using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Punto_Venta
{
    public partial class frmTipoVenta : Form
    {
        public string Tipo { get; set; }

        public frmTipoVenta()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Tipo = "RAPIDO";
            this.DialogResult = System.Windows.Forms.DialogResult.OK;    
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Tipo = "DOMICILIO";
            this.DialogResult = System.Windows.Forms.DialogResult.OK;    
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Tipo = "MESA";
            this.DialogResult = System.Windows.Forms.DialogResult.OK;    
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Tipo = "X";
            this.DialogResult = System.Windows.Forms.DialogResult.OK;    
        }
    }
}
