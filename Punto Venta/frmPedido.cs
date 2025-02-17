using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Data.OleDb;
using LibPrintTicket;
using System.Globalization;
using System.Drawing.Printing;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Button = System.Windows.Forms.Button;

namespace Punto_Venta
{
    public partial class frmPedido : Form
    {
        public class ProductoInventario
        {
            public int IdInventario { get; set; }
            public int IdCategoria { get; set; }
            public string Nombre { get; set; }
            public double Precio { get; set; }
            public bool Comanda { get; set; }
            public string Color { get; set; }
        }
        List<ProductoInventario> productos;
        private DataSet ds;
        int idMesero = 0;
        string idCliente;
        string modalidad;
        OleDbConnection conectar = new OleDbConnection(Conexion.CadCon);
        OleDbDataAdapter da;
        OleDbCommand cmd;
        public String Usuario;
        double total = 0;
        int suma = 0;
        string categoria = "";
        bool mesaNueva = false;
        int idMesa = 0;
        private double utilidadTotal;

        public frmPedido()
        {
            InitializeComponent();
            this.MinimumSize = new Size(810, 600);

        }
        private void frmPedido_Load(object sender, EventArgs e)
        {
            conectar.Open();
            DgvPedidoprevio.Columns["Pre"].DefaultCellStyle.Format = "N2";
            DgvPedidoprevio.Columns["Tot"].DefaultCellStyle.Format = "N2";
            productos = ObtenerProductosDesdeBD();

            cargarMesas();
            cargarCategoriasAutomatico();


            using (frmClaveVendendor ori = new frmClaveVendendor())
            {
                if (ori.ShowDialog() == DialogResult.OK)
                {
                    idMesero = ori.Id;
                    lblMesero.Text = ori.Mesero;
                }
                else
                    this.Close();
            }
            
        }

        private void cargarCategoriasAutomatico()
        {
            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();

                string query = @"SELECT * FROM CATEGORIAS ORDER BY Nombre";

                using (SqlCommand cmd = new SqlCommand(query, conectar))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Button but = new Button();
                        but.FlatStyle = FlatStyle.Flat;
                        but.FlatAppearance.BorderSize = 0;
                        but.Font = new System.Drawing.Font(new FontFamily("Calibri"), 12, FontStyle.Bold);
                        but.BackColor = System.Drawing.ColorTranslator.FromHtml($"#{reader["Color"].ToString()}");
                        but.ForeColor = Color.FromName(reader["Letra"].ToString());
                        but.Size = new System.Drawing.Size(135, 70);
                        but.Text = reader[1].ToString();
                        but.Click += new EventHandler(this.CambiarCategoria);
                        but.Tag = new
                        {
                            Id = reader["IdCategoria"].ToString(),
                            Nombre = reader["Nombre"].ToString(),
                        };

                        flpCategorias.Controls.Add(but);
                    }
                }
            }
            flpInventario.Controls.Clear();
            foreach (var producto in productos)
            {
                Button but = new Button();
                but.FlatStyle = FlatStyle.Flat;
                but.FlatAppearance.BorderSize = 0;
                but.Font = new System.Drawing.Font(new FontFamily("Calibri"), 12, FontStyle.Bold);
                but.BackColor = System.Drawing.ColorTranslator.FromHtml($"#{producto.Color}");
                but.Font = new System.Drawing.Font(new FontFamily("Calibri"), 12, FontStyle.Bold);
                but.Size = new System.Drawing.Size(135, 70);
                but.Text = producto.Nombre;
                but.Tag = new
                {
                    IdInventario = producto.IdInventario,
                    Nombre = producto.Nombre,
                    Precio = producto.Precio,
                    Comanda = producto.Comanda,
                };
                but.Click += new EventHandler(this.AgregarProducto);
                flpInventario.Controls.Add(but);
            }
        }

        private void cargarMesas()
        {
            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                DataTable dt = new DataTable();
                using (SqlCommand cmd = new SqlCommand("SELECT IdMesa, Nombre FROM MESAS Where Estatus = 'COCINA';", conectar))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                    CmbMesa.DisplayMember = "Nombre";
                    CmbMesa.ValueMember = "IdMesa";
                    CmbMesa.DataSource = dt;
                }
            }
        }
        private void rbRapido_CheckedChanged(object sender, EventArgs e)
        {
            CmbMesa.Visible = false;
            idCliente = "";
            modalidad = "RAPIDO";
        }
        private void rbDomicilo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDomicilo.Checked)
            {
                CmbMesa.Visible = false;
                using (frmBuscarClientes ori = new frmBuscarClientes())
                {
                    if (ori.ShowDialog() == DialogResult.OK)
                    {
                        LblNombre.Text = ori.Nombre;
                        LblDomicilio.Text = ori.Direccion;
                        LblTelefono.Text = ori.Telefono;
                        LblReferencia.Text = ori.Referencia;
                        idCliente = ori.Id;
                        lblColonia.Text = ori.Colonia;
                        modalidad = "DOMICILIO";
                    }
                }
            }

        }
        private void rbMesa_CheckedChanged(object sender, EventArgs e)
        {
            idCliente = "";
            CmbMesa.Visible = true;
            modalidad = "MESA";
        }
        public List<ProductoInventario> ObtenerProductosDesdeBD()
        {
            List<ProductoInventario> productos = new List<ProductoInventario>();


            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();

                string query = @"SELECT A.IdInventario, A.IdCategoria, A.Nombre, A.Precio, A.Comanda, B.Color
                                FROM INVENTARIO A
                                INNER JOIN CATEGORIAS B ON A.IdCategoria = B.IdCategoria;";

                using (SqlCommand cmd = new SqlCommand(query, conectar))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        productos.Add(new ProductoInventario
                        {
                            IdInventario = Convert.ToInt32(reader["IdInventario"]),
                            IdCategoria = Convert.ToInt32(reader["IdCategoria"]),
                            Nombre = reader["Nombre"].ToString(),
                            Precio = Convert.ToDouble(reader["Precio"]),
                            Comanda = Convert.ToBoolean(reader["Comanda"]),
                            Color = reader["Color"].ToString(),
                        });
                    }
                }
            }

            return productos;
        }
        private void CambiarCategoria(object sender, EventArgs e)
        {
            Button boton = sender as Button;
            var tag = boton.Tag as dynamic;
            int idCategoria = int.Parse(tag.Id);
            // Filtrar los productos por IdCategoria usando LINQ
            var productosFiltrados = productos
                .Where(p => p.IdCategoria == idCategoria)
                .ToList();
            flpInventario.Controls.Clear();
            foreach (var producto in productosFiltrados)
            {
                Button but = new Button();
                but.FlatStyle = FlatStyle.Flat;
                but.FlatAppearance.BorderSize = 0;
                but.Font = new System.Drawing.Font(new FontFamily("Calibri"), 12, FontStyle.Bold);
                but.BackColor = System.Drawing.ColorTranslator.FromHtml($"#{producto.Color}");
                but.Font = new System.Drawing.Font(new FontFamily("Calibri"), 12, FontStyle.Bold);
                but.Size = new System.Drawing.Size(135, 70);
                but.Text = producto.Nombre;
                but.Tag = new
                {
                    IdInventario = producto.IdInventario,
                    Nombre = producto.Nombre,
                    Precio = producto.Precio,
                    Comanda = producto.Comanda,
                };
                but.Click += new EventHandler(this.AgregarProducto);
                flpInventario.Controls.Add(but);
            }
        }
        private void AgregarProducto(object sender, EventArgs e)
        {
            using (frmCantidad ori = new frmCantidad())
            {
                if (ori.ShowDialog() == DialogResult.OK)
                {
                    Button boton = sender as Button;
                    var tag = boton.Tag as dynamic;
                    int id = tag.IdInventario;
                    double cantidad = ori.cantidad;
                    string nombre = tag.Nombre;
                    double precio = tag.Precio;
                    string comentario = ori.comentario;
                    bool comanda = tag.Comanda;
                    string idExtra = "";
                    DgvPedidoprevio.Rows.Add(id, cantidad, nombre, precio, (cantidad * precio), "X", comentario, comanda, idExtra);
                    total += cantidad * precio;
                    LblTotal.Text = $"{total:C}";
                }
            }
        }
        private void Pizzas(object sender, EventArgs e)
        {
            using (frmPizzas pi = new frmPizzas())
            {
                if (pi.ShowDialog() == DialogResult.OK)
                {
                    if (pi.tipo == "MITAD")
                    {
                        //ESTRUCTURA DE IDES EXTRAS: CANTIDAD1,ID1;CANTIDAD2,ID2;...
                        string ides = "1," + pi.id1 + ";1," + pi.id2 + ";1," + pi.idMasa + ";";
                        DgvPedidoprevio.Rows.Add("P" + pi.id1, "1", pi.nombre, pi.precio, pi.precio, pi.comentarios, "1", ides);
                        //SEPARAR IDES
                        //string[] ids = ides.Split(';');
                        //foreach (var word in ids)
                        //{
                        //    MessageBox.Show(ids[0] + " " + ids[1]);
                        //}
                        total += Convert.ToDouble(Convert.ToString(pi.precio));
                        for (int i = 0; i < pi.dataGridView1.RowCount; i++)
                        {
                            DgvPedidoprevio.Rows.Add(pi.dataGridView1[0, i].Value.ToString(), pi.dataGridView1[1, i].Value.ToString(), pi.dataGridView1[2, i].Value.ToString(), pi.dataGridView1[3, i].Value.ToString(), pi.dataGridView1[4, i].Value.ToString(), pi.dataGridView1[5, i].Value.ToString(), "1", "");
                            total += Convert.ToDouble(pi.dataGridView1[4, i].Value.ToString());
                        }
                    }
                    else
                    {
                        for (int i = 0; i < pi.DgvPedidoprevio.RowCount; i++)
                        {
                            DgvPedidoprevio.Rows.Add(pi.DgvPedidoprevio[0, i].Value.ToString(), pi.DgvPedidoprevio[1, i].Value.ToString(), pi.DgvPedidoprevio[2, i].Value.ToString(), pi.DgvPedidoprevio[3, i].Value.ToString(), pi.DgvPedidoprevio[3, i].Value.ToString(), pi.DgvPedidoprevio[4, i].Value.ToString(), "1", "1," + pi.idMasa + ";");
                            total += Convert.ToDouble(pi.DgvPedidoprevio[3, i].Value.ToString());
                        }
                        for (int i = 0; i < pi.dataGridView1.RowCount; i++)
                        {
                            DgvPedidoprevio.Rows.Add(pi.dataGridView1[0, i].Value.ToString(), pi.dataGridView1[1, i].Value.ToString(), pi.dataGridView1[2, i].Value.ToString(), pi.dataGridView1[3, i].Value.ToString(), pi.dataGridView1[4, i].Value.ToString(), pi.dataGridView1[5, i].Value.ToString(), "1", "");
                            total += Convert.ToDouble(pi.dataGridView1[4, i].Value.ToString());
                        }
                    }
                    LblTotal.Text = String.Format("{0:0.00}", total);

                }
            }
        }
        private void Combos(object sender, EventArgs e)
        {
            using (frmSeleccionarCombo pi = new frmSeleccionarCombo())
            {
                if (pi.ShowDialog() == DialogResult.OK)
                {
                    total += Convert.ToDouble(Convert.ToString(pi.total));
                    //ESTRUCTURA DE IDES EXTRAS: CANTIDAD1,ID1;CANTIDAD2,ID2;CANTIDADn,IDn...
                    string ides = "";
                    for (int i = 0; i < pi.DgvPedidoprevio.RowCount; i++)
                    {
                        string idCombo = pi.DgvPedidoprevio[0, i].Value.ToString();
                        double cantCombo = Convert.ToDouble(pi.DgvPedidoprevio[1, i].Value.ToString()) * Convert.ToDouble(pi.cantidad);
                        ides += cantCombo + "," + idCombo + ";";
                    }
                    DgvPedidoprevio.Rows.Add(pi.id, pi.cantidad, pi.nombre, pi.precio, pi.total, pi.comentario, "1", ides);
                }
                LblTotal.Text = String.Format("{0:0.00}", total);
            }
        }
        private bool ordenVacia()
        {
            if (1 > DgvPedidoprevio.RowCount)
            {
                MessageBox.Show("Orden Vacia, favor de verificar", "Alto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
            else if (String.IsNullOrEmpty(idCliente) && tabControl1.SelectedIndex == 1)
            {
                MessageBox.Show("NO SE HA SELECCIONADO CLIENTE", "Alto!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
            else
                return false;
        }
        private void llevar()
        {
            utilidadTotal = 0;
            total = 0;
            for (int i = 0; i < DgvPedidoprevio.RowCount; i++)
            {
                double compra = Convert.ToDouble(DgvPedidoprevio[8, i].Value.ToString());
                double venta = Convert.ToDouble(DgvPedidoprevio[3, i].Value.ToString());
                double piezas = Convert.ToDouble(DgvPedidoprevio[1, i].Value.ToString());
                double utilidad = (venta - compra) * piezas;
                utilidadTotal += utilidad;

                //cmd = new OleDbCommand("insert into ventas(idProducto,Cantidad,Producto,Comentarios,Precio,Total,Folio,Fecha,Estatus,Subcategoria,Utilidad) values('" + DgvPedidoprevio[0, i].Value.ToString() + "','" + DgvPedidoprevio[1, i].Value.ToString() + "','" + DgvPedidoprevio[2, i].Value.ToString() + "','" + DgvPedidoprevio[5, i].Value.ToString() + "','" + DgvPedidoprevio[3, i].Value.ToString() + "','" + DgvPedidoprevio[4, i].Value.ToString() + "','" + lblFolio.Text + "','" + (DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString()) + "','COBRADO','" + subcategoria + "','"+utilidad+"');", conectar);
                cmd.ExecuteNonQuery();
                string ide = "";
                if (DgvPedidoprevio[7, i].Value.ToString().Length > 0)
                    ide = DgvPedidoprevio[7, i].Value.ToString();
                cmd = new OleDbCommand("insert into temp(id,cantidad, producto, precio, total,ide) values ('" + DgvPedidoprevio[0, i].Value.ToString() + "','" + DgvPedidoprevio[1, i].Value.ToString() + "','" + DgvPedidoprevio[2, i].Value.ToString() + "'," + DgvPedidoprevio[3, i].Value.ToString() + ",'" + DgvPedidoprevio[4, i].Value.ToString() + "','" + ide + "');", conectar);
                cmd.ExecuteNonQuery();
                total += Convert.ToDouble(DgvPedidoprevio[4, i].Value.ToString());
            }
            cmd = new OleDbCommand("insert into folios(Folio,ModalidadVenta,Estatus,idCliente,Fecha,Colonia,Monto,FormaPago, Utilidad) values ('" + lblFolio.Text + "','P/LLEVAR','COBRADO','" + idCliente + "','" + (DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString()) + "','" + lblColonia.Text + "','" + total + "','Efectivo','" + utilidadTotal + "');", conectar);
            cmd.ExecuteNonQuery();
            cmd = new OleDbCommand("INSERT INTO corte (concepto, total,fecha,FormaPago) VALUES ('VENTA FOLIO: " + lblFolio.Text + "'," + total + ",'" + (DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString()) + "','Efectivo');", conectar);
            cmd.ExecuteNonQuery();
            int width = 420;
            int height = 540;
            printDocument1.PrinterSettings.DefaultPageSettings.PaperSize = new PaperSize("", width, height);
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(this.printDocument1_PrintPage);
            PrintDialog printdlg = new PrintDialog();
            PrintPreviewDialog printPrvDlg = new PrintPreviewDialog();
            printPrvDlg.Document = pd;
            printdlg.Document = pd;
            pd.Print();
            DialogResult dialogResult = MessageBox.Show("¿Imprimir otro ticket?", "Alto!", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                pd.Print();
            }
        }
        private void enMesa()
        {
            for (int i = 0; i < DgvPedidoprevio.RowCount; i++)
            {
                double compra = Convert.ToDouble(DgvPedidoprevio[8, i].Value.ToString());
                double venta = Convert.ToDouble(DgvPedidoprevio[3, i].Value.ToString());
                double piezas = Convert.ToDouble(DgvPedidoprevio[1, i].Value.ToString());
                double utilidad = (venta - compra) * piezas;
                string ide = "";
                if (DgvPedidoprevio[7, i].Value.ToString().Length > 0)
                    ide = DgvPedidoprevio[7, i].Value.ToString();
                cmd = new OleDbCommand("insert into ArticulosMesa(id,cantidad,producto,precio,total,Comentario,Mesa,Estatus,ids,Utilidad) values('" + DgvPedidoprevio[0, i].Value.ToString() + "','" + DgvPedidoprevio[1, i].Value.ToString() + "','" + DgvPedidoprevio[2, i].Value.ToString() + "','" + DgvPedidoprevio[3, i].Value.ToString() + "','" + DgvPedidoprevio[4, i].Value.ToString() + "','" + DgvPedidoprevio[5, i].Value.ToString() + "','" + CmbMesa.SelectedValue + "','COCINA','" + ide + "','" + utilidad + "');", conectar);
                cmd.ExecuteNonQuery();
            }
        }
        private void domicilio()
        {
            utilidadTotal = 0;
            total = 0;
            for (int i = 0; i < DgvPedidoprevio.RowCount; i++)
            {
                double compra = Convert.ToDouble(DgvPedidoprevio[8, i].Value.ToString());
                double venta = Convert.ToDouble(DgvPedidoprevio[3, i].Value.ToString());
                double piezas = Convert.ToDouble(DgvPedidoprevio[1, i].Value.ToString());
                double utilidad = (venta - compra) * piezas;
                utilidadTotal += utilidad;
                //cmd = new OleDbCommand("insert into ventas(idProducto,Cantidad,Producto,Comentarios,Precio,Total,Folio,Fecha,Estatus,ide,Subcategoria,Utilidad) values('" + DgvPedidoprevio[0, i].Value.ToString() + "','" + DgvPedidoprevio[1, i].Value.ToString() + "','" + DgvPedidoprevio[2, i].Value.ToString() + "','" + DgvPedidoprevio[5, i].Value.ToString() + "','" + DgvPedidoprevio[3, i].Value.ToString() + "','" + DgvPedidoprevio[4, i].Value.ToString() + "','" + lblFolio.Text + "','" + (DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString()) + "','COCINA','" + DgvPedidoprevio[7, i].Value.ToString() + "','" + subcategoria + "','"+utilidad+"');", conectar);
                cmd.ExecuteNonQuery();
                total += Convert.ToDouble(DgvPedidoprevio[4, i].Value.ToString());
            }
            cmd = new OleDbCommand("insert into folios(Folio,ModalidadVenta,Estatus,idCliente,Fecha,Colonia,Monto,FormaPago,Utilidad) values ('" + lblFolio.Text + "','REPARTO','COCINA','" + idCliente + "','" + (DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString()) + "','" + lblColonia.Text + "','" + total + "','Efectivo','" + utilidadTotal + "');", conectar);
            cmd.ExecuteNonQuery();
        }
        public void TicketComanda(List<(string Cantidad, string Descripcion, string Comentario)> itemsComanda)
        {

            Ticket ticket = new Ticket();
            ticket.FontSize = 10;
            ticket.MaxCharDescription = 26;
            //Llevar
            if (tabControl1.SelectedIndex == 2)
            {
                ticket.AddHeaderLine("Folio: " + lblFolio.Text);
                ticket.AddHeaderLine(" ");
                ticket.AddHeaderLine("****PARA LLEVAR****");
                ticket.AddHeaderLine(" ");
                ticket.AddHeaderLine("FECHA: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                //llevar();
            }
            //mesa
            else if (tabControl1.SelectedIndex == 0)
            {
                ticket.AddHeaderLine("FECHA: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                ticket.AddHeaderLine(" ");
                ticket.AddHeaderLine("****MESA " + CmbMesa.Text + "****");
                ticket.AddHeaderLine(" ");
                //enMesa();
            }
            //domicilio
            else if (tabControl1.SelectedIndex == 1)
            {
                ticket.AddHeaderLine("Folio: " + lblFolio.Text);
                ticket.AddHeaderLine(" ");
                ticket.AddHeaderLine("****ENTREGA A DOMICILIO****");
                ticket.AddHeaderLine("Folio: " + lblFolio.Text);
                ticket.AddHeaderLine("Cliente: " + LblNombre.Text);
                //domicilio();
            }
            ticket.AddHeaderLine("MESERO:" + lblMesero.Text);
            ticket.AddHeaderLine("FECHA: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
            foreach (var (cantidad, descripcion, comentario) in itemsComanda)
            {
                Console.WriteLine($"Cantidad: {cantidad}, Descripción: {descripcion}, Comentario: {comentario}");
                ticket.AddItem(cantidad, descripcion + comentario, "");
            }

            ticket.PrintTicket(Conexion.impresora2);
        }
        private void BtnEntregar_Click(object sender, EventArgs e)
        {
            if (!ordenVacia())
            {
                BtnEntregar.Visible = false;
                var listado = new List<(string, string, string)>();
                using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
                {
                    conectar.Open();
                    if (checkBox3.Checked)
                    {
                        using (SqlCommand cmd2 = new SqlCommand("UPDATE MESAS SET Estatus = 'COCINA' WHERE IdMesa = @IdMesa;", conectar))
                        {
                            cmd2.Parameters.AddWithValue("@IdMesa", idMesa);

                            cmd2.ExecuteNonQuery();
                        }


                    }
                    else if (idMesa != 0)
                    {
                        using (SqlCommand cmd2 = new SqlCommand("DELETE FROM MESAS WHERE Estatus = 'NUEVA';", conectar))
                        {
                            cmd2.ExecuteNonQuery();
                        }

                    }
                }
                if (!checkBox3.Checked)
                    idMesa = (int)CmbMesa.SelectedValue;
                for (int i = 0; i < DgvPedidoprevio.RowCount; i++)
                {
                    string cantidad = DgvPedidoprevio.Rows[i].Cells["Cantidad"].Value.ToString();
                    string descripcion = DgvPedidoprevio.Rows[i].Cells["Prod"].Value.ToString();
                    string comentario = DgvPedidoprevio.Rows[i].Cells["Comentario"].Value.ToString();
                    string idInventario = DgvPedidoprevio.Rows[i].Cells["Aidi"].Value.ToString();
                    listado.Add((cantidad, descripcion, comentario));
                    using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
                    {
                        conectar.Open();

                        string insertFolioQuery = "INSERT INTO ArticulosMesa (IdInventario, Cantidad, Total,Comentario,IdMesa, IdMesero, FechaHora, Estatus) " +
                                                  "VALUES (@IdInventario, @Cantidad, @Total, @Comentario, @IdMesa, @IdMesero, GETDATE(),@Estatus); ";

                        using (SqlCommand cmd = new SqlCommand(insertFolioQuery, conectar))
                        {
                            cmd.Parameters.AddWithValue("@IdInventario", idInventario);
                            cmd.Parameters.AddWithValue("@Cantidad", cantidad);
                            cmd.Parameters.AddWithValue("@Total", total);
                            cmd.Parameters.AddWithValue("@Comentario", comentario);
                            cmd.Parameters.AddWithValue("@IdMesa", idMesa);
                            cmd.Parameters.AddWithValue("@IdMesero", idMesero);
                            cmd.Parameters.AddWithValue("@Estatus", "COCINA");
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                
                TicketComanda(listado);
                this.Close();
                MessageBox.Show("SE HA REALIZADO LA ORDEN CON EXITO!", "ORDEN REALIZADA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmPedido pedo = new frmPedido();
                pedo.Show();
            }
        }
        private double RecalcularTotal
        {
            get
            {
                total = 0;
                for (int i = 0; i < DgvPedidoprevio.RowCount; i++)
                {
                    total += Convert.ToDouble(DgvPedidoprevio.Rows[i].Cells["Tot"].Value);
                }
                return total;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (DgvPedidoprevio.RowCount > 0)
            {
                DgvPedidoprevio.Rows.RemoveAt(DgvPedidoprevio.CurrentRow.Index);
                LblTotal.Text = $"{RecalcularTotal:C}";
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                lblFolio.Visible = false;
                groupBox1.Visible = false;
            }
            else
            {
                lblFolio.Visible = true;
                groupBox1.Visible = true;
            }
        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                int posicion = 10;
                //RESIZE
                Image logo = Image.FromFile("C:\\Jaeger Soft\\logo.jpg");
                e.Graphics.DrawImage(logo, new PointF(1, 10));
                //LOGO
                posicion += 180;
                e.Graphics.DrawString("********  NOTA DE CONSUMO  ********", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(1, posicion));
                posicion += 20;
                e.Graphics.DrawString("Cliente: " + LblNombre.Text, new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(1, posicion));
                posicion += 20;
                e.Graphics.DrawString("Telefono: " + LblTelefono.Text, new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(1, posicion));
                posicion += 20;
                e.Graphics.DrawString("Domicilio: " + LblDomicilio.Text, new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(1, posicion));
                posicion += 20;
                e.Graphics.DrawString("Referencia: " + LblReferencia.Text, new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(1, posicion));
                posicion += 20;
                e.Graphics.DrawString("FOLIO DE VENTA: " + lblFolio.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(1, posicion));
                posicion += 20;
                e.Graphics.DrawString("FECHA: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString(), new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(1, posicion));
                posicion += 50;
                //Titulo Columna
                e.Graphics.DrawString("Cant   Producto        P.Unit  Importe", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(1, posicion));
                posicion += 20;
                e.Graphics.DrawLine(new Pen(Color.Black), 1, posicion, 420, posicion);
                posicion += 10;
                //Lista de Productos
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Far;
                for (int i = 0; i < DgvPedidoprevio.RowCount; i++)
                {

                    double precio = Convert.ToDouble(DgvPedidoprevio[4, i].Value.ToString());
                    string producto = DgvPedidoprevio[2, i].Value.ToString();
                    double cant = Convert.ToDouble(DgvPedidoprevio[1, i].Value.ToString());
                    string item = cant.ToString("0.00", CultureInfo.InvariantCulture);
                    string pre = precio.ToString("00.00", CultureInfo.InvariantCulture);
                    double precioUni = Convert.ToDouble(DgvPedidoprevio[3, i].Value.ToString());
                    string uni = precioUni.ToString("00.00", CultureInfo.InvariantCulture);
                    if (producto.Length > 15)
                    {
                        producto = producto.Substring(0, 15);
                    }

                    e.Graphics.DrawString(item, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(1, posicion));
                    e.Graphics.DrawString(producto, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(40, posicion));
                    e.Graphics.DrawString(uni, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(210, posicion), sf);
                    e.Graphics.DrawString(String.Format(CultureInfo.InvariantCulture, "{0:0,0.00}", precio), new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(280, posicion), sf);
                    posicion += 20;
                }
                double to = Convert.ToDouble(LblTotal.Text);
                string toty = String.Format(CultureInfo.InvariantCulture, "{0:0,0.00}", to);
                e.Graphics.DrawLine(new Pen(Color.Black), 210, posicion + 10, 420, posicion + 10);

                posicion += 15;
                e.Graphics.DrawString("TOTAL: $" + toty, new Font("Arial", 10, FontStyle.Bold), Brushes.Black, new Point(280, posicion), sf);
                posicion += 50;
                e.Graphics.DrawLine(new Pen(Color.Black), 1, posicion, 2, posicion);
            }
            if (tabControl1.SelectedIndex == 2)
            {
                int posicion = 10;
                //RESIZE
                Image logo = Image.FromFile("C:\\Jaeger Soft\\logo.jpg");
                e.Graphics.DrawImage(logo, new PointF(1, 10));
                //LOGO
                posicion += 200;
                e.Graphics.DrawString("********  NOTA DE CONSUMO  ********", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(1, posicion));
                posicion += 20;
                e.Graphics.DrawString("FOLIO DE VENTA: " + lblFolio.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(1, posicion));
                posicion += 20;
                e.Graphics.DrawString("FECHA: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString(), new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(1, posicion));
                posicion += 50;
                //Titulo Columna
                e.Graphics.DrawString("Cant   Producto        P.Unit  Importe", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(1, posicion));
                posicion += 20;
                e.Graphics.DrawLine(new Pen(Color.Black), 1, posicion, 420, posicion);
                posicion += 10;
                //Lista de Productos
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Far;
                for (int i = 0; i < DgvPedidoprevio.RowCount; i++)
                {

                    double precio = Convert.ToDouble(DgvPedidoprevio[4, i].Value.ToString());
                    string producto = DgvPedidoprevio[2, i].Value.ToString();
                    double cant = Convert.ToDouble(DgvPedidoprevio[1, i].Value.ToString());
                    string item = cant.ToString("0.00", CultureInfo.InvariantCulture);
                    string pre = precio.ToString("00.00", CultureInfo.InvariantCulture);
                    double precioUni = Convert.ToDouble(DgvPedidoprevio[3, i].Value.ToString());
                    string uni = precioUni.ToString("00.00", CultureInfo.InvariantCulture);
                    if (producto.Length > 15)
                    {
                        producto = producto.Substring(0, 15);
                    }

                    e.Graphics.DrawString(item, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(1, posicion));
                    e.Graphics.DrawString(producto, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(40, posicion));
                    e.Graphics.DrawString(uni, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(210, posicion), sf);
                    e.Graphics.DrawString(String.Format(CultureInfo.InvariantCulture, "{0:0,0.00}", precio), new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(280, posicion), sf);
                    posicion += 20;
                }
                double to = Convert.ToDouble(LblTotal.Text);
                string toty = String.Format(CultureInfo.InvariantCulture, "{0:0,0.00}", to);
                e.Graphics.DrawLine(new Pen(Color.Black), 210, posicion + 10, 420, posicion + 10);

                posicion += 15;
                e.Graphics.DrawString("TOTAL: $" + toty, new Font("Arial", 10, FontStyle.Bold), Brushes.Black, new Point(280, posicion), sf);
                posicion += 50;
                e.Graphics.DrawLine(new Pen(Color.Black), 1, posicion, 2, posicion);
            }
        }
        private void DgvPedidoprevio_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == DgvPedidoprevio.Columns["btnEliminar"].Index && e.RowIndex >= 0)
            {
                DgvPedidoprevio.Rows.RemoveAt(e.RowIndex);
                LblTotal.Text = $"{RecalcularTotal:C}";
            }
        }
        private void tabPage2_MouseClick(object sender, MouseEventArgs e)
        {
            using (frmBuscarClientes ori = new frmBuscarClientes())
            {
                if (ori.ShowDialog() == DialogResult.OK)
                {
                    LblNombre.Text = ori.Nombre;
                    LblNombre.Visible = true;
                    LblDomicilio.Text = ori.Direccion;
                    LblDomicilio.Visible = true;
                    LblTelefono.Text = ori.Telefono;
                    LblTelefono.Visible = true;
                    LblReferencia.Text = ori.Referencia;
                    LblReferencia.Visible = true;
                    idCliente = ori.Id;
                    lblColonia.Text = ori.Colonia;
                    lblColonia.Visible = true;
                }

            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                if(!mesaNueva)
                {
                    using (frmAgregarMesas ori = new frmAgregarMesas())
                    {
                        ori.IdMesero = idMesero;
                        if (ori.ShowDialog() == DialogResult.OK)
                        {
                            idMesa = ori.IdMesa;
                            lblMesa.Text = ori.Mesa;
                            lblMesa.Visible = true;
                            CmbMesa.Visible = false;
                            mesaNueva = true;
                        }
                        else
                        {
                            checkBox3.Checked = false;
                        }
                    }
                }
                else
                {
                    lblMesa.Visible = true;
                    CmbMesa.Visible = false;
                }
            }
            else
            {
                lblMesa.Visible = false;
                CmbMesa.Visible = true;
            }
        }
    }
}
