using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.OleDb;
using LibPrintTicket;
using System.Drawing.Printing;
using System.Data.SqlClient;

namespace Punto_Venta
{
    public partial class frmCorte : Form
    {
        double mas = 0;
        double menos = 0;
        double credito = 0;
        public string usuario = "";
        string anoSQL = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + " " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
        public frmCorte()
        {
            InitializeComponent();
            this.MinimumSize = new Size(800, 775);
        }

        private void frmCorte_Load(object sender, EventArgs e)
        {
            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                DataSet ds = new DataSet();
                using (SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM CORTE", conectar))
                {
                    da.Fill(ds, "Id");
                    dgvCorte.DataSource = ds.Tables["Id"];
                    dgvCorte.Columns[0].Visible = false;
                }
                ds = new DataSet();
                using (SqlDataAdapter da = new SqlDataAdapter("select IdUsuario,Usuario As Mesero,Ventas, Mesas as MesasAtentidas from Usuarios;", conectar))
                {
                    da.Fill(ds, "Id");
                    dataGridView4.DataSource = ds.Tables["Id"];
                    dataGridView4.Columns[0].Visible = false;
                }
            }


            dgvCorte.Columns["Total"].DefaultCellStyle.Format = "N2";
            dataGridView4.Columns["Ventas"].DefaultCellStyle.Format = "N2";

            for (int i = 0; i < dgvCorte.RowCount; i++)
            {
                if (dgvCorte[4, i].Value.ToString() == "Efectivo")
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

            lblEntrada.Text = $"{mas:C}";
            lblSalida.Text = $"{menos:C}";
            lblCorte.Text = $"{(mas + menos):C}";
            lblCredito.Text = $"{credito:C}";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            corteTicket();
        }

        public void corteTicket()
        {
            int idInsertado;
            Ticket ticket = new Ticket();
            ticket.MaxChar = 35;
            ticket.MaxCharDescription = 22;
            ticket.FontSize = 8;
            ticket.AddHeaderLine("********  CORTE DE CAJA  *******");
            ticket.AddSubHeaderLine("FECHA Y HORA:" + anoSQL);
            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();
                string query = @"INSERT INTO HistorialCortes (Monto,FechaHora) VALUES (@Monto, GETDATE());
                                SELECT SCOPE_IDENTITY();"; // Obtener el último ID insertado
                using (SqlCommand cmd2 = new SqlCommand(query, conectar))
                {
                    cmd2.Parameters.AddWithValue("@Monto", $"{(mas + menos)}");
                    idInsertado = Convert.ToInt32(cmd2.ExecuteScalar());
                }
                ticket.AddSubHeaderLine("FOLIO: " + idInsertado);
                for (int i = 0; i < dataGridView4.RowCount; i++)
                {
                    query = @"INSERT INTO CortesMeseros(IdHistorialCortes,Mesero,Ventas,Mesas) VALUES (@IdCorte,@Mesero,@Ventas,@Mesas);";
                    using (SqlCommand cmd2 = new SqlCommand(query, conectar))
                    {
                        cmd2.Parameters.AddWithValue("@IdCorte", idInsertado);
                        cmd2.Parameters.AddWithValue("@Mesero", dataGridView4.Rows[i].Cells["Mesero"].Value?.ToString());
                        cmd2.Parameters.AddWithValue("@Ventas", dataGridView4.Rows[i].Cells["Ventas"].Value?.ToString());
                        cmd2.Parameters.AddWithValue("@Mesas", dataGridView4.Rows[i].Cells["MesasAtentidas"].Value?.ToString());
                        cmd2.ExecuteNonQuery();
                    }
                }
                for (int i = 0; i < dgvCorte.RowCount; i++)
                {
                    query = @"INSERT INTO CORTES(Concepto,Total, FormaPago,FechaHora, IdHistorialCortes) VALUES (@Concepto,@Total,@FormaPago,@FechaHora, @IdHistorialCortes);";
                    using (SqlCommand cmd2 = new SqlCommand(query, conectar))
                    {

                        cmd2.Parameters.AddWithValue("@Concepto", dgvCorte.Rows[i].Cells["Concepto"].Value?.ToString());
                        cmd2.Parameters.AddWithValue("@Total", dgvCorte.Rows[i].Cells["Total"].Value?.ToString());
                        cmd2.Parameters.AddWithValue("@FormaPago", dgvCorte.Rows[i].Cells["FormaPago"].Value?.ToString());
                        DateTime fechaHora = (DateTime)dgvCorte.Rows[i].Cells["FechaHora"].Value;
                        cmd2.Parameters.AddWithValue("@FechaHora", fechaHora);
                        cmd2.Parameters.AddWithValue("@IdHistorialCortes", idInsertado);
                        cmd2.ExecuteNonQuery();
                    }
                    ticket.AddItem("1", dgvCorte[1, i].Value.ToString(), "   $" + dgvCorte[2, i].Value.ToString());

                }
                using (SqlCommand cmd2 = new SqlCommand("UPDATE Usuarios set Ventas=0,Mesas=0;", conectar))
                {
                    cmd2.ExecuteNonQuery();
                }
                using (SqlCommand cmd2 = new SqlCommand("DELETE FROM CORTE;", conectar))
                {
                    cmd2.ExecuteNonQuery();
                }
                using (SqlCommand cmd2 = new SqlCommand("UPDATE inicio set inicio='0' Where id=1;", conectar))
                {
                    cmd2.ExecuteNonQuery();
                }
                using (SqlCommand cmd2 = new SqlCommand("UPDATE Usuarios set Ventas=0,Mesas=0;", conectar))
                {
                    cmd2.ExecuteNonQuery();
                }



            }
            ticket.AddTotal("Entradas", lblEntrada.Text);
            ticket.AddTotal("Salidas", lblSalida.Text);
            ticket.AddTotal("Total", lblCorte.Text);
            ticket.PrintTicket(Conexion.impresora);
            MessageBox.Show("Corte relizado con exito", "Corte", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                string en = dataGridView2[2, i].Value.ToString();
                string s = dataGridView2[3, i].Value.ToString();
                string a = dataGridView2[4, i].Value.ToString();
                if (producto.Length > 28)
                {
                    producto = producto.Substring(0, 27);
                }

                e.Graphics.DrawString(producto + "", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(1, posicion));
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

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Tiene que seleccionar un MESERO antes", "Corte de caja", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                frmCortesMesero cor = new frmCortesMesero();
                cor.idMesero = dataGridView4.CurrentRow.Cells["IdUsuario"].Value.ToString();
                cor.lblMonto.Text = dataGridView4.CurrentRow.Cells["Ventas"].Value.ToString();
                cor.lblMesas.Text = dataGridView4.CurrentRow.Cells["MesasAtentidas"].Value.ToString();
                cor.Text = "Corte de: " + dataGridView4.CurrentRow.Cells["Mesero"].Value.ToString();
                cor.nombre = dataGridView4.CurrentRow.Cells["Mesero"].Value.ToString();
                cor.Show();
                this.Close();
            }

        }
    }
}
