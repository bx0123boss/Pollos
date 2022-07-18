using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Punto_Venta
{
    public partial class frmEntregarRuta : Form
    {
        private DataSet ds;
        OleDbConnection conectar = new OleDbConnection(Conexion.CadCon); 
        OleDbDataAdapter da;
        OleDbCommand cmd;
        float total = 0;
        public string idCliente = "";
        public double cambio;
        
        public frmEntregarRuta()
        {
            InitializeComponent();
        }

        private void frmEntregarRuta_Load(object sender, EventArgs e)
        {
            conectar.Open();

            ds = new DataSet();
            da = new OleDbDataAdapter("select * from ventas where Folio='" + lblFolio.Text + "';", conectar);
            da.Fill(ds, "Id");
            dgvTotal2.DataSource = ds.Tables["Id"];
            dgvTotal2.Columns[0].Visible = false;
            dgvTotal2.Columns[1].Visible = false;
            dgvTotal2.Columns[7].Visible = false;
            dgvTotal2.Columns[10].Visible = false;
            panel1.Visible = true;
            cmd = new OleDbCommand("select Nombre,Telefono,Direccion,Referencia from Clientes where Id=" + idCliente + ";", conectar);
            OleDbDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                LblNombre.Text = Convert.ToString(reader[0].ToString());
                LblTelefono.Text = Convert.ToString(reader[1].ToString());
                LblDomicilio.Text = Convert.ToString(reader[2].ToString());
                LblReferencia.Text = Convert.ToString(reader[3].ToString());
            }
            for (int i = 0; i < dgvTotal2.RowCount; i++)
            {
                total += Convert.ToSingle(dgvTotal2[6, i].Value.ToString(), CultureInfo.CreateSpecificCulture("es-ES"));
            }
            lblTotal.Text = "$" + total;
            DateTime fecha, ruta;
            fecha = DateTime.Now;
            ruta = Convert.ToDateTime(lblFechaRuta.Text);
            string transcurrido = "" + (fecha - ruta).Hours + ":" + (fecha - ruta).Minutes;
            lblTiempo.Text = transcurrido;
            txtDineroMoto.Text = "" + (cambio + total);

        }

        private void BtnEntregarComanda_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvTotal2.RowCount; i++)
            {
                cmd = new OleDbCommand("insert into temp(id,cantidad, producto, precio, total,ide) values ('" + dgvTotal2[1, i].Value.ToString() + "','" + dgvTotal2[2, i].Value.ToString() + "','" + dgvTotal2[3, i].Value.ToString() + "'," + dgvTotal2[5, i].Value.ToString() + ",'" + dgvTotal2[6, i].Value.ToString() + "','" + dgvTotal2[10, i].Value.ToString() + "');", conectar);
                cmd.ExecuteNonQuery();
            }
            cmd = new OleDbCommand("UPDATE folios set Estatus='COBRADO', DepositoEnvio='" + txtDineroMoto.Text + "' where Folio='" + lblFolio.Text + "';", conectar);
            cmd.ExecuteNonQuery();
            double cambio = Convert.ToDouble(txtDineroMoto.Text) - total;
            cmd = new OleDbCommand("INSERT INTO corte (concepto, total,fecha,FormaPago) VALUES ('DEPOSITO DE CAMBIO FOLIO:" + lblFolio.Text + " A DOMICILIO'," + cambio + ",'" + (DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString()) + "','Efectivo');", conectar);
            //MessageBox.Show("INSERT INTO corte (concepto, total,fecha) VALUES ('DEPOSITO DE CAMBIO FOLIO:" + lblFolio.Text + " A DOMICILIO'," + txtDineroMoto + ",'" + (DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString()) + "');");
            cmd.ExecuteNonQuery();

            cmd = new OleDbCommand("INSERT INTO corte (concepto, total,fecha,FormaPago) VALUES ('COBRO FOLIO:" + lblFolio.Text + " A DOMICILIO'," + total + ",'" + (DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString()) + "','Efectivo');", conectar);
            cmd.ExecuteNonQuery();

            MessageBox.Show("COBRADO CON EXITO", "ENTREGADOO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void frmEntregarRuta_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void lblFechaRuta_Click(object sender, EventArgs e)
        {
            
            
        }

        private void txtDineroMoto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
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
