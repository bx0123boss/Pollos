using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using LibPrintTicket;

namespace Punto_Venta
{
    public partial class frmIngreso : Form
    {
        OleDbConnection conectar = new OleDbConnection(Conexion.CadCon); 
        OleDbCommand cmd;
        public string usuario;
        public frmIngreso()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conectar.Open();
            cmd = new OleDbCommand("INSERT INTO corte (concepto, total,fecha,FormaPago) VALUES ('ENTRADA POR CONCEPTO: " + txtConcepto.Text + "'," + txtIngreso.Text + ",'" + (DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString()) + "','Efectivo');", conectar);
            cmd.ExecuteNonQuery();
            Ticket ticket2 = new Ticket();
            ticket2.MaxChar = 35;
            ticket2.MaxCharDescription = 22;
            ticket2.FontSize = 8;
            ticket2.AddHeaderLine("****** ENTRADA DE EFECTIVO  *****");
            ticket2.AddSubHeaderLine("FECHA Y HORA:" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
            ticket2.AddSubHeaderLine("Usuario:" + usuario);
            ticket2.AddItem("1", txtConcepto.Text,"$"+txtIngreso.Text);
            ticket2.PrintTicket(Conexion.impresora);
            MessageBox.Show("Se ha ingresado a caja correctamente", "Listo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            conectar.Close();
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
    }
}
