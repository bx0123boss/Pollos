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
using System.Globalization;


namespace Punto_Venta
{
    public partial class frmVentaDetallada : Form
    {
        private DataSet ds;
        OleDbConnection conectar = new OleDbConnection(Conexion.CadCon); 
        OleDbDataAdapter da;
        OleDbCommand cmd;
        public int idMesero = 0;
        double total;
        public string usuario = "";
        public frmVentaDetallada()
        {
            InitializeComponent();
        }

        private void frmVentaDetallada_Load(object sender, EventArgs e)
        {
            ds = new DataSet();
            conectar.Open();
            da = new OleDbDataAdapter("select * from ventas where folio='" + lblFolio.Text + "';", conectar);
            da.Fill(ds, "Id");
            dataGridView1.DataSource = ds.Tables["Id"];

            dataGridView1.Columns[0].Visible = false;

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                total += Convert.ToSingle(dataGridView1[6, i].Value.ToString(), CultureInfo.CreateSpecificCulture("es-ES"));
            }
            lblMonto.Text = "" + total;
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Ticket ticket = new Ticket();
            ticket.MaxChar = 34;
            ticket.FontSize = 9;
            ticket.HeaderImage = Image.FromFile("C:\\Jaeger Soft\\logo.jpg");
            ticket.AddHeaderLine("*******  NOTA DE CONSUMO  *******");
            ticket.AddHeaderLine("FOLIO DE VENTA: " + lblFolio.Text);
            ticket.AddSubHeaderLine("FECHA: " + lblFecha.Text);
            ticket.AddSubHeaderLine("FECHA REIMPRESION: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {

                double lol = Convert.ToDouble(dataGridView1[5, i].Value.ToString());
                string producto;

                producto = dataGridView1[3, i].Value.ToString();


                string item = dataGridView1[2, i].Value.ToString();

                ticket.AddItem(String.Format(CultureInfo.InvariantCulture, "{0:0,0.00}", item), producto, "$" + String.Format(CultureInfo.InvariantCulture, "{0:0,0.00}", lol));
                    
                //ticket.AddItem(dataGridView1[2, i].Value.ToString(), producto, "$" + lol);

            }

               ticket.AddTotal("TOTAL", String.Format(CultureInfo.InvariantCulture, "{0:0,0.00}",lblMonto.Text));
               ticket.AddFooterLine("  ¡GRACIAS POR SU PREFERENCIA!");
               ticket.PrintTicket("print");
            //ticket.PrintTicket("print");
            //ticket.PrintTicket("EPSON TM-T20II Receipt");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cmd = new OleDbCommand("update folios set Estatus='CANCELADO' Where Folio='" + lblFolio.Text + "'", conectar);
            cmd.ExecuteNonQuery();
            cmd = new OleDbCommand("INSERT INTO corte (concepto, total,fecha,FormaPago) VALUES ('CANCELACION DE FOLIO: " + lblFolio.Text + ", por: "+usuario+"',-" +lblMonto.Text + ",'" + (DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString()) + "','CANCELADO');", conectar);
            cmd.ExecuteNonQuery();
            cmd = new OleDbCommand("update ventas set Estatus='CANCELADO' Where Folio='" + lblFolio.Text + "'", conectar);
            cmd.ExecuteNonQuery();  
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {

                string id="";
                cmd = new OleDbCommand("select ID1 from temp where Cantidad='" + dataGridView1[2, i].Value.ToString() + "' and producto='"+dataGridView1[3, i].Value.ToString()+"';", conectar);
                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    id= reader[0].ToString();
                }
                cmd = new OleDbCommand("delete from temp where ID1=" + id + ";", conectar);
                cmd.ExecuteNonQuery();
                cmd = new OleDbCommand("INSERT INTO ArticulosCancelados(Cantidad, Producto, Comentario, Mesa, Fecha, Mesero, Cancelo) VALUES ('" + dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString() + "','" + dataGridView1[3, dataGridView1.CurrentRow.Index].Value.ToString() + "','','"+lblFolio.Text+"','" + (DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString()) + "','" + lblMesero.Text + "','" + usuario + "');", conectar);
                cmd.ExecuteNonQuery();
            }
            double ventas = 0;
            int mesas = 0;
            cmd = new OleDbCommand("select * from Usuarios where Id=" + idMesero + ";", conectar);
            OleDbDataReader reader2 = cmd.ExecuteReader();
            if (reader2.Read())
            {
                ventas = Convert.ToDouble(reader2[4].ToString());
                mesas = Convert.ToInt32(reader2[5].ToString());
            }
            ventas -= total;
            mesas--;
            cmd = new OleDbCommand("UPDATE Usuarios SET Ventas='" + ventas + "',Mesas='"+mesas+"' where Id=" + idMesero + ";", conectar);
            cmd.ExecuteNonQuery();
            MessageBox.Show("ORDEN CANCELADA CON EXITO", "Comanda General", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            this.Close();
        }
    }
}
