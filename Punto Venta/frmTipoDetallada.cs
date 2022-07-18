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
    public partial class frmTipoDetallada : Form
    {
        public string usuario = "";
        public frmTipoDetallada()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmReporteVentas ventas = new frmReporteVentas();
            ventas.usuario = usuario;
            ventas.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmReporteVentasProducto ventas = new frmReporteVentasProducto();
            ventas.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmArticulosCancelados art = new frmArticulosCancelados();
            art.Show();
            this.Close();

        }

        private void frmTipoDetallada_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            frmProductoMas mas = new frmProductoMas();
            mas.Show();
            this.Close();
        }
    }
}
