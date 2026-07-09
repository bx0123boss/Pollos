using MigraDoc.DocumentObjectModel.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Punto_Venta
{
    public partial class frmCorte : frmBase
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
            List<Producto> productos = new List<Producto>();
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
                    productos.Add(new Producto
                    {
                        Cantidad = 0,
                        Nombre = dgvCorte.Rows[i].Cells["Concepto"].Value?.ToString(),
                        PrecioUnitario = Convert.ToDouble(dgvCorte.Rows[i].Cells["Total"].Value?.ToString()),
                        Total = Convert.ToDouble(dgvCorte.Rows[i].Cells["Total"].Value?.ToString()),
                    });
                    

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
            DialogResult dialogResult = MessageBox.Show("¿Desea imprimir el corte de caja?", "Alto!", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                string[] encabezados = new string[] { "********** CORTE DE CAJA  ********", "               Corte de caja:", DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() };
                Dictionary<string, double> totales = new Dictionary<string, double>();
                totales.Add("Entradas", Convert.ToDouble(GetNumericValue(lblEntrada.Text)));
                totales.Add("Salidas", Convert.ToDouble(GetNumericValue(lblSalida.Text)));
                totales.Add("Total", Convert.ToDouble(GetNumericValue(lblCorte.Text)));

                string[] pieDePagina = new string[] { "" };
                TicketPrinter ticketPrinter = new TicketPrinter(encabezados, Conexion.pieDeTicket, Conexion.logoPath, productos, "", "", "", 0, true, totales);
                ticketPrinter.ImprimirTicket();
            }
            MessageBox.Show("Corte relizado con exito", "Corte", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Close();
        }
        string GetNumericValue(string input)
        {
            return Regex.Replace(input, @"[^\d.-]", "");
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }


        private void printDocument1_PrintPage_1(object sender, PrintPageEventArgs e)
        {
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

