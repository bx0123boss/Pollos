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
using System.Drawing.Printing;

namespace Punto_Venta
{
    public partial class frmCorte : Form
    {
        double mas = 0;
        double menos = 0;
        double credito = 0;
        private DataSet ds;
        OleDbConnection conectar = new OleDbConnection(Conexion.CadCon); 
        OleDbDataAdapter da;
        OleDbCommand cmd;
        public string usuario="";
        string anoSQL = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + " " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();            
        public frmCorte()
        {
            InitializeComponent();
        }

        private void frmCorte_Load(object sender, EventArgs e)
        {

            ds = new DataSet();
            conectar.Open();
            da = new OleDbDataAdapter("select * from corte ORDER BY Id Desc;", conectar);
            da.Fill(ds, "Id");
            dgvCorte.DataSource = ds.Tables["Id"];
            dgvCorte.Columns[0].Visible = false;

            ds = new DataSet();
            da = new OleDbDataAdapter("SELECT TOP 100 Sum(ventas.Cantidad) AS CantidadVendidos, ventas.Subcategoria, Sum(ventas.Total) AS SumaDeTotal from ventas where Fecha >=#" + DateTime.Now.ToString("dd/MM/yyyy") + " 00:00:00# and Fecha <=#" + DateTime.Now.ToString("dd/MM/yyyy") + " 23:59:59# AND Estatus = 'COBRADO' GROUP BY Ventas.Subcategoria ORDER BY 2 DESC;", conectar); 
            da.Fill(ds, "Id");
            dataGridView1.DataSource = ds.Tables["Id"];

            ds = new DataSet();
            da = new OleDbDataAdapter("SELECT invent.idArticulo, invent.articulo AS Nombre, invent.entrada, invent.salida, Articulos.cantidad as Actual FROM invent, Articulos WHERE invent.idArticulo = Articulos.id;", conectar);
            da.Fill(ds, "Id");
            dataGridView2.DataSource = ds.Tables["Id"];

            ds = new DataSet();
            da = new OleDbDataAdapter("select Id,Usuario,('$' + Ventas) as Ventas, Mesas as Mesas_Atendidas from Usuarios;", conectar);
            da.Fill(ds, "Id");
            dataGridView4.DataSource = ds.Tables["Id"];
            dataGridView4.Columns[0].Visible = false;

            for (int i = 0; i < dgvCorte.RowCount; i++)
            {
                if (dgvCorte[4, i].Value.ToString() == "Efectivo" )
                {
                    if (Convert.ToDouble(dgvCorte[2, i].Value.ToString()) > 0)
                    {                        
                        mas += Convert.ToDouble(dgvCorte[2, i].Value.ToString());
                    }
                    else if (Convert.ToDouble(dgvCorte[2, i].Value.ToString()) < 0)
                        menos += Convert.ToDouble(dgvCorte[2, i].Value.ToString());
                }
                else if (dgvCorte[4, i].Value.ToString() == "Tarjeta")
                {
                    credito += Convert.ToDouble(dgvCorte[2, i].Value.ToString());
                }
                else if (Convert.ToDouble(dgvCorte[2, i].Value.ToString()) > 0)
                    {
                        mas += Convert.ToDouble(dgvCorte[2, i].Value.ToString());
                    }
                    else if (Convert.ToDouble(dgvCorte[2, i].Value.ToString()) < 0)
                        menos += Convert.ToDouble(dgvCorte[2, i].Value.ToString());

            }
            //if (usuario=="VENTAS")
            //{
            //    corte();
            //}
            //for (int i = 0; i < dataGridView3.RowCount; i++)
            //{
            //tarjeta += Convert.ToSingle(dataGridView3[2, i].Value.ToString(), CultureInfo.CreateSpecificCulture("es-ES"));
            //} 
            string mas2 = String.Format(CultureInfo.InvariantCulture, "{0:0,0.00}", mas);
            string menos2 = String.Format(CultureInfo.InvariantCulture, "{0:0,0.00}", (menos * -1));
            string masmenos = String.Format(CultureInfo.InvariantCulture, "{0:0,0.00}", (mas + menos + credito));
            lblEntrada.Text = "$" + mas2;
            lblSalida.Text = "$" + menos2;
            lblCorte.Text = "$" + masmenos;
            lblCredito.Text = "$" + credito;

        }

        private void label1_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < dgvCorte.RowCount; i++)
            {
                if (Convert.ToDouble(dgvCorte[2, i].Value.ToString()) < 0)
                {
                    menos += Convert.ToSingle(dgvCorte[2, i].Value.ToString(), CultureInfo.CreateSpecificCulture("es-ES"));

                }
                else if (Convert.ToDouble(dgvCorte[2, i].Value.ToString()) > 0)
                    mas += Convert.ToSingle(dgvCorte[2, i].Value.ToString(), CultureInfo.CreateSpecificCulture("es-ES"));
            }
            lblEntrada.Text = "$" + mas;
            lblSalida.Text = "$" + (menos * -1);
            lblCorte.Text = "$" + (mas + menos);
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            corteTicket();
        }

        public void corteTicket()
        {

            button1.Hide();
            Ticket ticket = new Ticket();
            ticket.MaxChar = 35;
            ticket.MaxCharDescription = 22;
            ticket.FontSize = 8;
            ticket.AddHeaderLine("********  CORTE DE CAJA  *******");
            ticket.AddSubHeaderLine("FECHA Y HORA:" + anoSQL);
            cmd = new OleDbCommand("INSERT INTO histocortes(Monto,Fecha) VALUES ('" + lblCorte.Text + "','" + (DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString()) + "');", conectar);
            cmd.ExecuteNonQuery();
            string idInsertado = "";
            cmd = new OleDbCommand("select @@IDENTITY;", conectar);
            OleDbDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                idInsertado = reader[0].ToString();
            }
            ticket.AddSubHeaderLine("FOLIO: " + idInsertado);
            for (int i = 0; i < dataGridView4.RowCount; i++)
            {
                string cadena = dataGridView4[2, i].Value.ToString().Substring(1);
                cmd = new OleDbCommand("INSERT INTO MesaDet(IdCorte,Mesero,Ventas,Mesas) VALUES ('" + idInsertado + "','" + dataGridView4[1, i].Value.ToString() + "','" + cadena + "','" + dataGridView4[3, i].Value.ToString() + "');", conectar);
                cmd.ExecuteNonQuery();
            }
            cmd = new OleDbCommand("delete from corte where 1;", conectar);
            cmd.ExecuteNonQuery();
            cmd = new OleDbCommand("delete from Credito where 1;", conectar);
            //cmd.ExecuteNonQuery();
            MessageBox.Show("Corte relizado con exito", "Corte", MessageBoxButtons.OK, MessageBoxIcon.Information);
            cmd = new OleDbCommand("UPDATE inicio set inicio='0' Where id=1;", conectar);
            cmd.ExecuteNonQuery();
            cmd = new OleDbCommand("UPDATE Usuarios set Ventas='0',Mesas='0';", conectar);
            cmd.ExecuteNonQuery();
            cmd = new OleDbCommand("delete from invent where 1;", conectar);
            cmd.ExecuteNonQuery();
            for (int i = 0; i < dgvCorte.RowCount; i++)
            {
                cmd = new OleDbCommand("insert into Cortes(Concepto,Monto,idCorte) Values('" + dgvCorte[1, i].Value.ToString() + "','" + dgvCorte[2, i].Value.ToString() + "','" + idInsertado + "');", conectar);
                cmd.ExecuteNonQuery();
                ticket.AddItem("1", dgvCorte[1, i].Value.ToString(), "   $" + dgvCorte[2, i].Value.ToString());
            }

            ticket.AddTotal("Entradas", lblEntrada.Text);
            ticket.AddTotal("Salidas", lblSalida.Text);
            ticket.AddTotal("Total", lblCorte.Text);
            ticket.PrintTicket(Conexion.impresora);

            //Ticket ticket2 = new Ticket();
            //ticket2.MaxChar = 35;
            //ticket2.MaxCharDescription = 22;
            //ticket2.FontSize = 8;
            //ticket2.AddHeaderLine("********  PRODUCTOS VENDIDOS  *******");
            //ticket2.AddSubHeaderLine("FECHA Y HORA:"+ anoSQL);
            //ticket2.AddSubHeaderLine("FOLIO: " + idInsertado);
            //for (int i = 0; i < dataGridView1.RowCount; i++)
            //{
            //    ticket2.AddItem(dataGridView1[0, i].Value.ToString(), dataGridView1[1, i].Value.ToString(), "   $" + dataGridView1[2, i].Value.ToString());
            //}
            //ticket2.PrintTicket(Conexion.impresora);
            this.Close();
        }

        public void corte()
        {

            button1.Hide();
            
            string anoSQL = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + " " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
            cmd = new OleDbCommand("INSERT INTO histocortes(Monto,Fecha) VALUES ('" + lblCorte.Text + "','" + (DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString()) + "');", conectar);
            cmd.ExecuteNonQuery();
            string idInsertado = "";
            cmd = new OleDbCommand("select @@IDENTITY;", conectar);
            OleDbDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                idInsertado = reader[0].ToString();
            }
            for (int i = 0; i < dataGridView4.RowCount; i++)
            {
                string cadena = dataGridView4[2, i].Value.ToString().Substring(1);
                cmd = new OleDbCommand("INSERT INTO MesaDet(IdCorte,Mesero,Ventas,Mesas) VALUES ('" + idInsertado + "','" + dataGridView4[1, i].Value.ToString() + "','" + cadena + "','" + dataGridView4[3, i].Value.ToString() + "');", conectar);
                cmd.ExecuteNonQuery();
            }
            cmd = new OleDbCommand("delete from corte where 1;", conectar);
            cmd.ExecuteNonQuery();
            cmd = new OleDbCommand("delete from Credito where 1;", conectar);
            //cmd.ExecuteNonQuery();
            cmd = new OleDbCommand("UPDATE inicio set inicio='0' Where id=1;", conectar);
            cmd.ExecuteNonQuery();
            cmd = new OleDbCommand("UPDATE Usuarios set Ventas='0',Mesas='0';", conectar);
            cmd.ExecuteNonQuery();
            cmd = new OleDbCommand("delete from invent where 1;", conectar);
            cmd.ExecuteNonQuery();
            for (int i = 0; i < dgvCorte.RowCount; i++)
            {
                cmd = new OleDbCommand("insert into Cortes(Concepto,Monto,idCorte) Values('" + dgvCorte[1, i].Value.ToString() + "','" + dgvCorte[2, i].Value.ToString() + "','" + idInsertado + "');", conectar);
                cmd.ExecuteNonQuery();
            }
            this.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Ticket ticket2 = new Ticket();
            ticket2.MaxChar = 35;
            ticket2.MaxCharDescription = 22;
            ticket2.FontSize = 8;
            ticket2.AddHeaderLine("*******  CATEGORIAS ******");
            ticket2.AddSubHeaderLine("FECHA Y HORA:" + anoSQL);
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                ticket2.AddItem(dataGridView1[0, i].Value.ToString(), dataGridView1[1, i].Value.ToString(), "   $" + dataGridView1[2, i].Value.ToString());
            }
            ticket2.PrintTicket(Conexion.impresora);
            int width = 420;
            int height = 540;
            printDocument1.PrinterSettings.DefaultPageSettings.PaperSize = new PaperSize("", width, height);
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(this.printDocument1_PrintPage_1);
            PrintDialog printdlg = new PrintDialog();
            PrintPreviewDialog printPrvDlg = new PrintPreviewDialog();
            // preview the assigned document or you can create a different previewButton for it
            printPrvDlg.Document = pd;
            printdlg.Document = pd;
            pd.Print();
           
        }

        private void printDocument1_PrintPage_1(object sender, PrintPageEventArgs e)
        {
            int posicion = 10;
            //RESIZE
            Image logo = Image.FromFile("C:\\Jaeger Soft\\logo.jpg");
            e.Graphics.DrawImage(logo, new PointF(1, 10));
            //LOGO
            posicion += 200;
            e.Graphics.DrawString("*****  INVENTARIO  *****", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(1, posicion));
            posicion += 20;            
            e.Graphics.DrawString("FECHA: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString(), new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(1, posicion));
            posicion += 30;
            //Titulo Columna
            e.Graphics.DrawString("Producto                     E    S    A", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(1, posicion));
            posicion += 20;
            e.Graphics.DrawLine(new Pen(Color.Black), 1, posicion, 420, posicion);
            posicion += 5;
            //Lista de Productos
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Far;
            int posiColumn = posicion;
            for (int i = 0; i < dataGridView2.RowCount; i++)
            {

                string producto = dataGridView2[1, i].Value.ToString();
                string en= dataGridView2[2, i].Value.ToString();
                string s = dataGridView2[3, i].Value.ToString();
                string a = dataGridView2[4, i].Value.ToString();
                if (producto.Length > 28)
                {
                    producto = producto.Substring(0, 27);
                }

                e.Graphics.DrawString(producto+"", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(1, posicion));
                e.Graphics.DrawString(en, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(180, posicion));
                e.Graphics.DrawString(s, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(215, posicion), sf);
                e.Graphics.DrawString(a, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(245, posicion), sf);
                posicion += 20;
                //ticket.AddItem(item, producto, uni + "|" + String.Format(CultureInfo.InvariantCulture, "{0:0,0.00}", precio));
            }
            e.Graphics.DrawLine(new Pen(Color.Black), 0, posicion + 10, 420, posicion + 10);
            //e.Graphics.DrawLine(new Pen(Color.Red), 260, posiColumn - 5, 260, posicion + 10);
            //e.Graphics.DrawLine(new Pen(Color.Red), 220, posiColumn - 5, 220, posicion + 10);
            //e.Graphics.DrawLine(new Pen(Color.Red), 190, posiColumn - 5, 190, posicion + 10);
            posicion += 15;                       
        }

        private void label6_Click(object sender, EventArgs e)
        {
           
        }

        private void label4_Click(object sender, EventArgs e)
        {
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                frmCortesMesero cor = new frmCortesMesero();
                cor.idMesero = dataGridView4[0, dataGridView4.CurrentRow.Index].Value.ToString();
                cor.lblMonto.Text = dataGridView4[2, dataGridView4.CurrentRow.Index].Value.ToString();
                cor.lblMesas.Text = dataGridView4[3, dataGridView4.CurrentRow.Index].Value.ToString();
                cor.Text = "Corte de: " + dataGridView4[1, dataGridView4.CurrentRow.Index].Value.ToString();
                cor.nombre = dataGridView4[1, dataGridView4.CurrentRow.Index].Value.ToString();
                cor.Show();
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Tiene que seleccionar un MESERO antes", "Corte de caja", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblCredito_Click(object sender, EventArgs e)
        {
            

        }
    }
}
