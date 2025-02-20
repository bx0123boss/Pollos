using System;
using System.Data;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace Punto_Venta
{
    public partial class frmAbrirCaja : Form
    {
        public string usuario, nombre;
        public int id = 0;
        public frmAbrirCaja()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            aceptar();

        }

        private void txtIngreso_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                aceptar();
            }
            else if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo se permiten numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }
        public void aceptar()
        {
            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();
                if ((txtIngreso.Text == "0") || (txtIngreso.Text == ""))
                {

                }
                else
                {

                    conectar.Open();
                    string query = @"INSERT INTO CORTE (Concepto, Total,FechaHora,FormaPago) VALUES
                                    ('APERTURA DE CAJA', @Total, GETDATE(), 'EFECTIVO')";
                    using (SqlCommand cmd2 = new SqlCommand(query, conectar))
                    {
                        cmd2.Parameters.AddWithValue("@Total", txtIngreso.Text);
                        cmd2.ExecuteNonQuery();
                    }

                }

                using (SqlCommand cmd2 = new SqlCommand("UPDATE inicio set inicio='1' Where id=1;", conectar))
                {
                    cmd2.ExecuteNonQuery();
                }
            }
            frmPrincipal principal = new frmPrincipal();
            principal.lblUser.Text = usuario;
            principal.usuario = nombre;
            principal.id = id;
            principal.Show();
            this.Close();
        }
    }
}
