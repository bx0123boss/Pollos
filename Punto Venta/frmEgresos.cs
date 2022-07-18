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
    public partial class frmEgresos : Form
    {
        OleDbConnection conectar = new OleDbConnection(Conexion.CadCon); 
        OleDbCommand cmd;
        public string usuario;
        public frmEgresos()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conectar.Open();
            cmd = new OleDbCommand("INSERT INTO corte (concepto, total,fecha,FormaPago) VALUES ('SALIDA POR CONCEPTO: " + txtConcepto.Text + "',-" + txtIngreso.Text + ",'" + (DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString()) + "','Efectivo');", conectar);
            cmd.ExecuteNonQuery();
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
            conectar.Close();
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
