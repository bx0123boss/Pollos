using LibPrintTicket;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Tickets80mm;

namespace Punto_Venta
{
    public partial class frmVentaDetalladaPlaya : Form
    {
        public string idMesero;
        public double total, utilidad;
        public string usuario = "";
        public string IdMesa;
        public string idCliente = "0";
        public string fechaApertura;
        public int nopersonas;
        public string orden;
        public string cajero;
        private void frmVentaDetalladaPlaya_Load(object sender, EventArgs e)
        {

            using (SqlConnection conectar = new SqlConnection(Conexion.CadConRestaurantSoft))
            {
                conectar.Open();
                DataSet ds = new DataSet();
                string query = @" SELECT 
                                      C.cantidad,
                                      A.descripcion,
                                      B.precio
                                  FROM productos A
                                  INNER JOIN productosdetalle B ON A.idproducto = B.idproducto
                                  INNER JOIN cheqdet C ON A.idproducto = C.idproducto
                                  
                                WHERE C.foliodet = @Folio;";

                using (SqlDataAdapter da = new SqlDataAdapter(query, conectar))
                {
                    da.SelectCommand.Parameters.AddWithValue("@Folio", lblFolio.Text);
                    da.Fill(ds, "IdFolio");
                    dataGridView1.DataSource = ds.Tables["IdFolio"];
                }
               
            }
           // dataGridView1.Columns[0].Visible = false;
            lblMonto.Text = $"{total:C}";

        }

        private void button1_Click(object sender, EventArgs e)
        {
           

            string[] encabezado =
            {
                "[B]PLAYA HERMOSA",
                "[B]DANIEL BAEZ TEMIX",
                "RFC: BATD931010K37",
                "SIMON BOLIVAR 10372 VERACRUZ VERACRUZ MEXICO CP",
                "91918",
                "LUGAR DE EXPEDICION",
                "TEL:"
            };

                    string[] pie =
                    {
                "[B]ESTE NO ES UN COMPROBANTE FISCAL",
                "[B]PROPINA NO INCLUIDA",
                "*** SOFT RESTAURANT V10 ***"
            };


            //            ticket.AddSubHeaderLine("FECHA REIMPRESION: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
            List<Tickets80mm.Producto> productos = new List<Tickets80mm.Producto>();
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                string nombre = dataGridView1[1, i].Value.ToString();
                decimal cant = Convert.ToDecimal(dataGridView1[0, i].Value.ToString());
                decimal total = Convert.ToDecimal(dataGridView1[2, i].Value.ToString());
                productos.Add(new Tickets80mm.Producto { Nombre = nombre, Cantidad = cant, Total = total });
            }
            var ticket = new TicketPlaya(
               productos,
               lblMesa.Text,            // mesa
               lblMesero.Text,       // mesero
               nopersonas,               // personas (int)
               orden,             // orden
               lblFolio.Text,         // folio
               cajero,        // cajero
               encabezado,
               pie,
               DateTime.Parse(fechaApertura),    // apertura
               DateTime.Parse(lblFecha.Text),    // cierre
               "Courier New",   // fuente monoespaciada
               10f              // tamaño en puntos (float)
           );

            // Imprimir (pasa el nombre exacto si quieres fijar la impresora)
            ticket.ImprimirComanda("print");
        }

        public frmVentaDetalladaPlaya()
        {
            InitializeComponent();
            this.MinimumSize = new Size(750, 650);
        }
    }
}
