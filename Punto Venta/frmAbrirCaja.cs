using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data.OleDb;
using System.Text.RegularExpressions;

namespace Punto_Venta
{
    public partial class frmAbrirCaja : Form
    {
        private DataSet ds;
        
        OleDbDataAdapter da;
        OleDbConnection conectar = new OleDbConnection(Conexion.CadCon);  
        OleDbCommand cmd;
        public string usuario,nombre;
        public int id = 0;
        public frmAbrirCaja()
        {
            InitializeComponent();
        }

        private void frmAbrirCaja_Load(object sender, EventArgs e)
        {
            ds = new DataSet();
            conectar.Open();
            da = new OleDbDataAdapter("select * from articulos order by Origen;", conectar);
            da.Fill(ds, "Id");
            dataGridView1.DataSource = ds.Tables["Id"];
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
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                cmd = new OleDbCommand("INSERT INTO invent (idArticulo, articulo, entrada, salida, antiguas) VALUES ('" + dataGridView1[0, i].Value.ToString() + "','" + dataGridView1[1, i].Value.ToString() + "',0,0,'" + dataGridView1[2, i].Value.ToString() + "');", conectar);
                //MessageBox.Show("INSERT INTO invent (idArticulo, articulo, entrada, salida) VALUES ('" + dataGridView1[0, i].Value.ToString() + "','" + dataGridView1[1, i].Value.ToString() + "',0,0);");
                cmd.ExecuteNonQuery();
            }
            if ((txtIngreso.Text == "0") || (txtIngreso.Text == ""))
            {

            }
            else
            {
                cmd = new OleDbCommand("INSERT INTO corte (concepto, total,fecha,FormaPago) VALUES ('APERTURA DE CAJA'," + txtIngreso.Text + ",'" + (DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString()) + "','Efectivo');", conectar);
                cmd.ExecuteNonQuery();
            }
            cmd = new OleDbCommand("UPDATE inicio set inicio='1' Where id=1;", conectar);
            cmd.ExecuteNonQuery();
            frmPrincipal principal = new frmPrincipal();
            principal.lblUser.Text = usuario;
            principal.usuario = nombre;
            principal.id = id;
            principal.Show();
            this.Close();
        }
    }
}
