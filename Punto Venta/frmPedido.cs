using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;
using LibPrintTicket;

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
            public string Letra { get; set; }
        }
        List<ProductoInventario> productos;
        int idMesero = 0;
        string idCliente;
        public String Usuario;
        double total = 0;
        bool mesaNueva = false;
        int idMesa = 0;
        int categoriaSeleccionada = 0;
        public frmPedido()
        {
            InitializeComponent();
            this.MinimumSize = new Size(810, 600);

        }
        private void frmPedido_Load(object sender, EventArgs e)
        {
            DgvPedidoprevio.Columns["Pre"].DefaultCellStyle.Format = "N2";
            DgvPedidoprevio.Columns["Tot"].DefaultCellStyle.Format = "N2";
            productos = ObtenerProductosDesdeBD();

            cargarMesas();
            cargarCategoriasAutomatico();
            cargarCombo();
            cargarBotonTodos();

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
        public void cargarCombo()
        {

            Button butC = new Button();
            butC.FlatStyle = FlatStyle.Flat;
            butC.FlatAppearance.BorderSize = 0;
            butC.Font = new Font(new FontFamily("Calibri"), 11, FontStyle.Bold);
            butC.BackColor = ColorTranslator.FromHtml("#654321");
            butC.ForeColor = Color.FromName("White");
            butC.Size = new Size(104, 56);
            butC.Text = "COMBOS";
            butC.Click += new EventHandler(this.Combos);
            flpCategorias.Controls.Add(butC);
        }
        public void cargarBotonTodos()
        {

            Button butC = new Button();
            butC.FlatStyle = FlatStyle.Flat;
            butC.FlatAppearance.BorderSize = 0;
            butC.Font = new Font(new FontFamily("Calibri"), 11, FontStyle.Bold);
            butC.BackColor = ColorTranslator.FromHtml("#FFFFFF");
            butC.ForeColor = Color.FromName("Black");
            butC.Size = new Size(104, 56);
            butC.Text = "TODOS";
            butC.Tag = new
            {
                Id = "0",
            };
            butC.Click += new EventHandler(this.CambiarCategoria);
            flpCategorias.Controls.Add(butC);
        }
        public void cargarPizzas()
        {
            Button butP = new Button();
            butP.FlatStyle = FlatStyle.Flat;
            butP.FlatAppearance.BorderSize = 0;
            butP.Font = new Font(new FontFamily("Calibri"), 11, FontStyle.Bold);
            butP.BackColor = ColorTranslator.FromHtml("#ff8000");
            butP.ForeColor = Color.FromName("White");
            butP.Size = new Size(104, 56);
            butP.Text = "PIZZA";
            butP.Click += new EventHandler(this.Pizzas);
            flpCategorias.Controls.Add(butP);
        }
        private void cargarCategoriasAutomatico()
        {
            flpCategorias.Controls.Clear();
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
                        but.Font = new Font(new FontFamily("Calibri"), 12, FontStyle.Bold);
                        but.BackColor = ColorTranslator.FromHtml($"#{reader["Color"].ToString()}");
                        but.ForeColor = Color.FromName(reader["Letra"].ToString());
                        but.Size = new Size(104, 56);
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
                but.Font = new Font(new FontFamily("Calibri"), 12, FontStyle.Bold);
                but.BackColor = ColorTranslator.FromHtml($"#{producto.Color}");
                but.ForeColor = Color.FromName(producto.Letra);
                but.Font = new Font(new FontFamily("Calibri"), 12, FontStyle.Bold);
                but.Size = new Size(135, 70);
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
        public List<ProductoInventario> ObtenerProductosDesdeBD()
        {
            List<ProductoInventario> productos = new List<ProductoInventario>();


            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();

                string query = @"SELECT A.IdInventario, A.IdCategoria, A.Nombre, A.Precio, A.Comanda, B.Color, B.Letra
                                FROM INVENTARIO A
                                INNER JOIN CATEGORIAS B ON A.IdCategoria = B.IdCategoria WHERE A.Estatus = 1;";

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
                            Letra = reader["Letra"].ToString(),
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
            if (categoriaSeleccionada == idCategoria)
                return;
            categoriaSeleccionada = idCategoria;
            var productosFiltrados = productos
                   .Where(p => p.IdCategoria == idCategoria)
                   .ToList();
            // Filtrar los productos por IdCategoria usando LINQ
            if (idCategoria == 0)
            {
                productosFiltrados = productos
                    .ToList();
            }
            flpInventario.Controls.Clear();
            foreach (var producto in productosFiltrados)
            {
                Button but = new Button();
                but.FlatStyle = FlatStyle.Flat;
                but.FlatAppearance.BorderSize = 0;
                but.Font = new Font(new FontFamily("Calibri"), 12, FontStyle.Bold);
                but.BackColor = ColorTranslator.FromHtml($"#{producto.Color}");
                but.ForeColor = Color.FromName(producto.Letra);
                but.Size = new Size(135, 70);
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
                    DgvPedidoprevio.Rows.Add(pi.id, pi.cantidad, pi.nombre, pi.precio, pi.total, "X", pi.comentario, "1", ides);
                }
                LblTotal.Text = $"{RecalcularTotal:C}";
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
        public void TicketComanda(List<(string id,string Cantidad, string Descripcion, string Comentario, string ides)> itemsComanda)
        {
            string RESULT = "";
            Ticket ticket = new Ticket();
            ticket.FontSize = 10;
            ticket.MaxCharDescription = 26;
            //Llevar
            if (tabControl1.SelectedIndex == 2)
            {
                ticket.AddHeaderLine("****PARA LLEVAR****");
                ticket.AddHeaderLine(" ");
                ticket.AddHeaderLine("FECHA: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                //llevar();
            }
            //mesa
            else if (tabControl1.SelectedIndex == 0)
            {
                ticket.AddHeaderLine("****MESA " + CmbMesa.Text + "****");
                ticket.AddHeaderLine(" ");
                //enMesa();
            }
            //domicilio
            else if (tabControl1.SelectedIndex == 1)
            {
                ticket.AddHeaderLine("****ENTREGA A DOMICILIO****");
                ticket.AddHeaderLine("Cliente: " + LblNombre.Text);
                //domicilio();
            }
            ticket.AddHeaderLine("MESERO:" + lblMesero.Text);
            ticket.AddHeaderLine("FECHA: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
            foreach (var (id,cantidad, descripcion, comentario, ide) in itemsComanda)
            {
                ticket.AddItem(cantidad, descripcion +": "+ comentario, "");
                if (id.Substring(0, 1) == "C")
                {
                    string[] ids = ide.Split(';');
                    
                    foreach (var word in ids)
                    {
                        string[] ids2 = word.Split(',');
                        for (int i2 = 0; i2 < ids2.Length - 1; i2 = i2 + 2)
                        {
                            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
                            {
                                conectar.Open();

                                string query = @"SELECT Nombre FROM Inventario WHERE IdInventario = @IdInventario";

                                using (SqlCommand cmd = new SqlCommand(query, conectar))
                                {
                                    cmd.Parameters.AddWithValue("@IdInventario", ids2[1]);
                                    using (SqlDataReader reader = cmd.ExecuteReader())
                                    {
                                        while (reader.Read())
                                        {
                                            ticket.AddItem(ids2[0], reader[0].ToString() + "", "");
                                            RESULT += ids2[0] + " : " + reader[0].ToString() + "\n";
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            //MessageBox.Show(RESULT);
            ticket.PrintTicket(Conexion.impresora2);
        }
        private void BtnEntregar_Click(object sender, EventArgs e)
        {
            if (!ordenVacia())
            {
                BtnEntregar.Visible = false;
                var listado = new List<(string,string, string, string, string)>();
                using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
                {
                    conectar.Open();
                    if (tabControl1.SelectedIndex == 1)
                    {
                        //domicilio
                        string query = "INSERT INTO Mesas (Nombre, IdMesero,Impresion,Estatus, IdCliente) " +
                                           "VALUES ('Domicilio " + (LblNombre.Text.Length > 20 ? LblNombre.Text.Substring(0, 20) : LblNombre.Text) + "', @IdMesero, 0, @Estatus, @IdCliente);" +
                                             "SELECT SCOPE_IDENTITY();"; // Obtener el último ID insertado
                        using (SqlCommand cmd = new SqlCommand(query, conectar))
                        {
                            cmd.Parameters.AddWithValue("@IdMesero", idMesero);
                            cmd.Parameters.AddWithValue("@Estatus", "COCINA");
                            cmd.Parameters.AddWithValue("@IdCliente", idCliente);
                            idMesa = Convert.ToInt32(cmd.ExecuteScalar());
                        }
                    }
                    else if(tabControl1.SelectedIndex == 2)
                    {
                        //llevar
                        string query = "INSERT INTO Mesas (Nombre, IdMesero,Impresion,Estatus) " +
                                          "VALUES ('Para llevar', @IdMesero, 0, @Estatus);" +
                                            "SELECT SCOPE_IDENTITY();"; // Obtener el último ID insertado
                        using (SqlCommand cmd = new SqlCommand(query, conectar))
                        {
                            cmd.Parameters.AddWithValue("@IdMesero", idMesero);
                            cmd.Parameters.AddWithValue("@Estatus", "COCINA");
                            idMesa = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                    }
                    else if (checkBox3.Checked ==false && CmbMesa.SelectedValue != null)
                        idMesa = (int)CmbMesa.SelectedValue;
                    else if(CmbMesa.SelectedValue == null && checkBox3.Checked == false)
                    {
                        BtnEntregar.Visible = true;
                        MessageBox.Show("NO SE HA SELECCIONADO MESA", "Alto!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    
                    
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
                        using (SqlCommand cmd = new SqlCommand("DELETE AM FROM ArticulosMesa AM INNER JOIN MESAS M ON AM.IdMesa = M.IdMesa WHERE M.Estatus = 'NUEVA';", conectar))
                        {
                            cmd.ExecuteNonQuery();
                        }

                    }
                }
                for (int i = 0; i < DgvPedidoprevio.RowCount; i++)
                {
                    string id = DgvPedidoprevio.Rows[i].Cells["Aidi"].Value.ToString();
                    string cantidad = DgvPedidoprevio.Rows[i].Cells["Cantidad"].Value.ToString();
                    string descripcion = DgvPedidoprevio.Rows[i].Cells["Prod"].Value.ToString();
                    string comentario = DgvPedidoprevio.Rows[i].Cells["Comentario"].Value.ToString();
                    string idInventario = DgvPedidoprevio.Rows[i].Cells["Aidi"].Value.ToString();
                    string totalArticulo = DgvPedidoprevio.Rows[i].Cells["Tot"].Value.ToString();
                    string ides = DgvPedidoprevio.Rows[i].Cells["idExtra"].Value.ToString();
                    listado.Add((id, cantidad, descripcion, comentario, ides));
                    using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
                    {
                        conectar.Open();

                        string insertFolioQuery = "INSERT INTO ArticulosMesa (IdInventario, Cantidad, Total,Comentario,IdMesa, IdMesero, FechaHora, Estatus,Ids, IdPromo) " +
                                                  "VALUES (@IdInventario, @Cantidad, @Total, @Comentario, @IdMesa, @IdMesero, GETDATE(),@Estatus, @Ids, @IdPromo); ";

                        using (SqlCommand cmd = new SqlCommand(insertFolioQuery, conectar))
                        {
                            if (idInventario.Substring(0, 1) == "C")
                            {
                                cmd.Parameters.AddWithValue("@IdInventario", 0);
                                cmd.Parameters.AddWithValue("@Ids", ides);
                                cmd.Parameters.AddWithValue("@IdPromo", idInventario.Substring(1, idInventario.Length-1));
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IdInventario", idInventario);
                                cmd.Parameters.AddWithValue("@Ids", (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@IdPromo", (object)DBNull.Value);
                            }
                            cmd.Parameters.AddWithValue("@Cantidad", cantidad);
                            cmd.Parameters.AddWithValue("@Total", totalArticulo);
                            cmd.Parameters.AddWithValue("@Comentario", comentario);
                            cmd.Parameters.AddWithValue("@IdMesa", idMesa);
                            cmd.Parameters.AddWithValue("@IdMesero", idMesero);
                            cmd.Parameters.AddWithValue("@Estatus", "COCINA");
                           

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                
                TicketComanda(listado);
               
                MessageBox.Show("SE HA REALIZADO LA ORDEN CON EXITO!", "ORDEN REALIZADA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (tabControl1.SelectedIndex == 2)
                {
                    frmCobros cobrar = new frmCobros();
                    cobrar.lblID.Text = idMesa.ToString();
                    cobrar.lblMesa.Text = "Para llevar";
                    cobrar.lblMesero.Text = lblMesero.Text;
                    cobrar.idMesero = idMesero;
                    cobrar.print = "0";
                    cobrar.lblPersonas.Text = "N/A";
                    cobrar.FormBorderStyle = FormBorderStyle.None;
                    cobrar.ShowDialog();
                    ReiniciarForm();
                }
                else
                {
                    ReiniciarForm();
                }
                
            }
        }
        private void ReiniciarForm()
        {
            #region domicilio
            lblColonia.Text = "";
            LblTelefono.Text = "";
            LblDomicilio.Text = "";
            LblReferencia.Text = "";
            LblNombre.Text = "";
            idCliente = "0";
            #endregion
            BtnEntregar.Visible = true;
            tabControl1.SelectedIndex = 0;
            checkBox3.Checked = false;
            lblMesa.Text = "0";
            idCliente = null;
            DgvPedidoprevio.Rows.Clear();
            LblTotal.Text = $"{RecalcularTotal:C}";
            mesaNueva = false;
            idMesa = 0;
            cargarMesas(); 
            Button botonFicticio = new Button();
            var tag = new { Id = "0" }; 
            botonFicticio.Tag = tag; 
            CambiarCategoria(botonFicticio, EventArgs.Empty);
            
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
            if (DgvPedidoprevio.CurrentRow == null)
            {
                return;
            }
            if (DgvPedidoprevio.RowCount > 0)
            {
                DgvPedidoprevio.Rows.RemoveAt(DgvPedidoprevio.CurrentRow.Index);
                LblTotal.Text = $"{RecalcularTotal:C}";
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

        private void lblMesero_Click(object sender, EventArgs e)
        {
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
    }
}
