using MigraDoc.DocumentObjectModel.Internals;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Punto_Venta
{
    public partial class frmPedido : Form
    {
        private TextBox txtBuscarProducto;

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
        public string Usuario;
        double total = 0;
        bool mesaNueva = false;
        public int idMesa = 0;
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

            this.KeyPreview = true;
            this.KeyDown += FrmPedido_KeyDown;

            InicializarBuscador();

            flpInventario.Resize += (s, ev) =>
            {
                // Re-filtrar para recalcular posiciones y tamaños dinámicos
                FiltrarProductos(txtBuscarProducto != null ? txtBuscarProducto.Text.Trim() : "");
            };
            flpCategorias.Resize += (s, ev) =>
            {
                // Re-filtrar para recalcular posiciones y tamaños dinámicos
                FiltrarProductos(txtBuscarProducto != null ? txtBuscarProducto.Text.Trim() : "");
            };  
            if (idMesa > 0)
            {
                CargarMesa(idMesa);
                mesaNueva = false;
            }
            else
            {
                mesaNueva = true;
                cargarMesas();
            }
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
        private void InicializarBuscador()
        {
            // 1. Guardamos las dimensiones y posición original de flpInventario desde el Designer
            Point posicionOriginal = flpInventario.Location;
            Size tamanoOriginal = flpInventario.Size;
            Control padreOriginal = flpInventario.Parent;

            int altoBuscador = 30;
            int separacion = 5; // Espacio entre el TextBox y los botones

            // 2. Creación del TextBox de Búsqueda sobre el área de productos (flpInventario)
            txtBuscarProducto = new TextBox
            {
                Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point),
                Location = new Point(posicionOriginal.X, posicionOriginal.Y),
                Width = tamanoOriginal.Width,
                Height = altoBuscador,
                // Anchor dinámico: crecerá junto con el panel de botones
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            txtBuscarProducto.TextChanged += TxtBuscarProducto_TextChanged;
            txtBuscarProducto.Enter += TxtBuscarProducto_AbrirTeclado;
            txtBuscarProducto.Click += TxtBuscarProducto_AbrirTeclado;
            flpInventario.Location = new Point(
                posicionOriginal.X,
                posicionOriginal.Y + altoBuscador + separacion
            );

            // 4. Reducimos la altura del flpInventario para que no se desborde abajo
            flpInventario.Size = new Size(
                tamanoOriginal.Width,
                tamanoOriginal.Height - (altoBuscador + separacion)
            );

            // 5. Agregamos el TextBox al formulario
            padreOriginal.Controls.Add(txtBuscarProducto);
            txtBuscarProducto.BringToFront();
        }
        private void FrmPedido_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2 && txtBuscarProducto != null)
            {
                txtBuscarProducto.Focus();
                txtBuscarProducto.SelectAll();
                e.Handled = true;
            }
        }

        private void TxtBuscarProducto_TextChanged(object sender, EventArgs e)
        {
            FiltrarProductos(txtBuscarProducto.Text.Trim());
        }

        private void FiltrarProductos(string busqueda)
        {
            if (productos == null) return;

            flpInventario.SuspendLayout();
            flpInventario.Controls.Clear();

            var listaFiltrada = productos.Where(p =>
                (categoriaSeleccionada == 0 || p.IdCategoria == categoriaSeleccionada) &&
                (string.IsNullOrEmpty(busqueda) || p.Nombre.IndexOf(busqueda, StringComparison.OrdinalIgnoreCase) >= 0)
            ).ToList();
            Size tamanoResponsivo = CalcularTamanoBotonProducto();
            float tamañoFuente = tamanoResponsivo.Width > 140 ? 14F : 12.5F;
            Font fuenteDinamica = new Font("Segoe UI", tamañoFuente, FontStyle.Bold);
            foreach (var producto in listaFiltrada)
            {
                Button but = new Button
                {
                    FlatStyle = FlatStyle.Flat,
                    Size = tamanoResponsivo,
                    Text = producto.Nombre,
                    BackColor = ColorTranslator.FromHtml($"#{producto.Color}"),
                    ForeColor = Color.FromName(producto.Letra),
                    Font = fuenteDinamica,
                    Tag = producto // Guardamos la entidad completa directamente
                };

                but.FlatAppearance.BorderSize = 0;
                but.FlatAppearance.MouseOverBackColor = ControlPaint.Light(ColorTranslator.FromHtml($"#{producto.Color}"), 0.15f);
                but.FlatAppearance.MouseDownBackColor = ControlPaint.Dark(Color.FromName(producto.Letra), 0.15f);
                but.Click += AgregarProducto;

                flpInventario.Controls.Add(but);
            }

            flpInventario.ResumeLayout();
        }

        private void CambiarCategoria(object sender, EventArgs e)
        {
            txtBuscarProducto.Clear();
            if (!(sender is Button boton)) return;

            int idCat = 0;

            // Extraer ID de manera segura probando los dos formatos (entero directo u objeto anónimo)
            if (boton.Tag is int idInt)
            {
                idCat = idInt;
            }
            else if (boton.Tag != null)
            {
                var tagDynamic = boton.Tag as dynamic;
                try { idCat = int.Parse(tagDynamic.Id.ToString()); } catch { idCat = 0; }
            }

            categoriaSeleccionada = idCat;
            FiltrarProductos(txtBuscarProducto != null ? txtBuscarProducto.Text.Trim() : "");
        }

        // --- MÉTODOS DE DATOS Y LÓGICA EXISTENTES ---

        public void cargarCombo()
        {
            Size tamanoResponsivo = CalcularTamanoBotonCategoria();
            float tamañoFuente = tamanoResponsivo.Width > 110 ? 11.5F : 10F;
            Font fuenteDinamica = new Font("Segoe UI", tamañoFuente, FontStyle.Bold);

            Button butC = new Button();
            butC.FlatStyle = FlatStyle.Flat;
            butC.FlatAppearance.BorderSize = 0;
            butC.Font = fuenteDinamica;
            butC.BackColor = ColorTranslator.FromHtml("#654321");
            butC.ForeColor = Color.FromName("White");
            butC.Size = tamanoResponsivo;
            butC.Text = "COMBOS";
            butC.Click += new EventHandler(this.Combos);
            flpCategorias.Controls.Add(butC);
        }

        public void cargarBotonTodos()
        {
            Size tamanoResponsivo = CalcularTamanoBotonCategoria();
            float tamañoFuente = tamanoResponsivo.Width > 110 ? 11.5F : 10F;
            Font fuenteDinamica = new Font("Segoe UI", tamañoFuente, FontStyle.Bold);

            Button butC = new Button();
            butC.FlatStyle = FlatStyle.Flat;
            butC.FlatAppearance.BorderSize = 0;
            butC.Font = fuenteDinamica;
            butC.BackColor = ColorTranslator.FromHtml("#FFFFFF");
            butC.ForeColor = Color.FromName("Black");
            butC.Size = tamanoResponsivo;
            butC.Text = "TODOS";
            butC.Tag = 0; // Guardamos directamente el entero 0
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
                Size tamanoResponsivo = CalcularTamanoBotonCategoria();

                // Fuente un poco más moderada para no saturar la pestaña (11pt a 12pt)
                float tamañoFuente = tamanoResponsivo.Width > 110 ? 11.5F : 10F;
                Font fuenteDinamica = new Font("Segoe UI", tamañoFuente, FontStyle.Bold);
                using (SqlCommand cmd = new SqlCommand(query, conectar))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Button but = new Button();
                        but.FlatStyle = FlatStyle.Flat;
                        but.FlatAppearance.BorderSize = 0;
                        but.Font = fuenteDinamica;
                        but.BackColor = ColorTranslator.FromHtml($"#{reader["Color"]}");
                        but.ForeColor = Color.FromName(reader["Letra"].ToString());
                        but.Size = tamanoResponsivo;
                        but.Text = reader["Nombre"].ToString();
                        but.Click += new EventHandler(this.CambiarCategoria);

                        // Guardamos el ID parseado como int
                        but.Tag = Convert.ToInt32(reader["IdCategoria"]);

                        flpCategorias.Controls.Add(but);
                    }
                }
            }

            // Muestra todos los productos inicialmente en la cuadrícula
            FiltrarProductos("");
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
                    DataRow fila = dt.NewRow();
                    fila["IdMesa"] = 0;
                    fila["Nombre"] = "-- Seleccione una mesa --";
                    dt.Rows.InsertAt(fila, 0);
                    CmbMesa.DisplayMember = "Nombre";
                    CmbMesa.ValueMember = "IdMesa";
                    CmbMesa.DataSource = dt;
                }
            }
        }
        private void CargarMesa(int idMesa)
        {
            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                DataTable dt = new DataTable();

                using (SqlCommand cmd = new SqlCommand(
                    "SELECT IdMesa, Nombre FROM MESAS WHERE IdMesa = @IdMesa;", conectar))
                {
                    cmd.Parameters.AddWithValue("@IdMesa", idMesa);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);

                        CmbMesa.DisplayMember = "Nombre";
                        CmbMesa.ValueMember = "IdMesa";
                        CmbMesa.DataSource = dt;
                    }
                }
            }
        }
        public List<ProductoInventario> ObtenerProductosDesdeBD()
        {
            List<ProductoInventario> productos = new List<ProductoInventario>();

            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();
                string query = @"SELECT A.IdInventario, A.IdCategoria, A.Nombre, A.Precio, A.Comanda, B.Color, B.Letra, A.Comanda
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

        private void AgregarProducto(object sender, EventArgs e)
        {
            using (frmCantidad ori = new frmCantidad())
            {
                if (ori.ShowDialog() == DialogResult.OK)
                {
                    Button boton = sender as Button;

                    // Manejo robusto del objeto Tag (ProductoInventario directo o dynamic)
                    int id;
                    string nombre;
                    double precio;
                    bool comanda;

                    if (boton.Tag is ProductoInventario p)
                    {
                        id = p.IdInventario;
                        nombre = p.Nombre;
                        precio = p.Precio;
                        comanda = p.Comanda;
                    }
                    else
                    {
                        var tag = boton.Tag as dynamic;
                        id = tag.IdInventario;
                        nombre = tag.Nombre;
                        precio = tag.Precio;
                        comanda = tag.Comanda;
                    }

                    double cantidad = ori.cantidad;
                    string comentario = ori.comentario;
                    string idExtra = "";

                    DgvPedidoprevio.Rows.Add(id, cantidad, nombre, precio, (cantidad * precio), "X", comentario, comanda, idExtra);
                    total += cantidad * precio;
                    LblTotal.Text = $"{total:C}";
                    txtBuscarProducto.Clear();
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
                        string ides = "1," + pi.id1 + ";1," + pi.id2 + ";1," + pi.idMasa + ";";
                        DgvPedidoprevio.Rows.Add("P" + pi.id1, "1", pi.nombre, pi.precio, pi.precio, pi.comentarios, "1", ides);
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
            txtBuscarProducto.Clear();
        }

        private void Combos(object sender, EventArgs e)
        {
            using (frmSeleccionarCombo pi = new frmSeleccionarCombo())
            {
                if (pi.ShowDialog() == DialogResult.OK)
                {
                    total += Convert.ToDouble(Convert.ToString(pi.total));
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

        public void TicketComanda(List<(string id, string Cantidad, string Descripcion, string Comentario, string ides, bool comanda)> itemsComanda)
        {
            ImprimirComanda(itemsComanda.Where(x => x.comanda).ToList());
            ImprimirComanda(itemsComanda.Where(x => !x.comanda).ToList());
        }

        private void ImprimirComanda(List<(string id, string Cantidad, string Descripcion, string Comentario, string ides, bool comanda)> items)
        {
            // Si no hay artículos de este tipo, no imprimir
            if (items == null || items.Count == 0)
                return;

            string modalidad = "";

            if (tabControl1.SelectedIndex == 2)
            {
                modalidad = "****PARA LLEVAR****";
            }
            else if (tabControl1.SelectedIndex == 0)
            {
                modalidad = "****MESA " + CmbMesa.Text + "****";
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                modalidad = "****ENTREGA A DOMICILIO****";
                modalidad += "\nCliente: " + LblNombre.Text;
            }

            List<Producto> productosParaImprimir = new List<Producto>();

            foreach (var (id, cantidad, descripcion, comentario, ide, comanda) in items)
            {
                productosParaImprimir.Add(new Producto
                {
                    Nombre = descripcion,
                    Cantidad = Convert.ToDouble(cantidad),
                    Comentario = comentario,
                    PrecioUnitario = 0,
                    Total = 0
                });

                if ((id.StartsWith("C") || id.StartsWith("P") || !string.IsNullOrEmpty(ide)) && !string.IsNullOrWhiteSpace(ide))
                {
                    string[] idsExtras = ide.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var extra in idsExtras)
                    {
                        string[] detallesId = extra.Split(',');

                        if (detallesId.Length >= 2)
                        {
                            double subCantidad = Convert.ToDouble(detallesId[0]);
                            string idInventario = detallesId[1];
                            string nombreExtra = ObtenerNombreProducto(idInventario);

                            if (!string.IsNullOrEmpty(nombreExtra))
                            {
                                productosParaImprimir.Add(new Producto
                                {
                                    Nombre = "  -> " + nombreExtra,
                                    Cantidad = subCantidad,
                                    Comentario = "",
                                    PrecioUnitario = 0,
                                    Total = 0
                                });
                            }
                        }
                    }
                }
            }

            if (productosParaImprimir.Count == 0)
                return;

            TicketPrinter ticket = new TicketPrinter(productosParaImprimir, lblMesa.Text, lblMesero.Text);
            ticket.ImprimirComanda(Conexion.impresora2);
        }
        private string ObtenerNombreProducto(string idInventario)
        {
            string nombre = "";
            try
            {
                using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
                {
                    conectar.Open();
                    string query = "SELECT Nombre FROM Inventario WHERE IdInventario = @Id";
                    using (SqlCommand cmd = new SqlCommand(query, conectar))
                    {
                        cmd.Parameters.AddWithValue("@Id", idInventario);
                        var result = cmd.ExecuteScalar();
                        if (result != null) nombre = result.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al consultar BD para impresión: " + ex.Message);
            }
            return nombre;
        }

        private void BtnEntregar_Click(object sender, EventArgs e)
        {
            if (!ordenVacia())
            {
                BtnEntregar.Visible = false;
                var listado = new List<(string, string, string, string, string, bool)>();
                using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
                {
                    conectar.Open();
                    if (tabControl1.SelectedIndex == 1)
                    {
                        string query = "INSERT INTO Mesas (Nombre, IdMesero,Impresion,Estatus, IdCliente) " +
                                       "VALUES ('Domicilio " + (LblNombre.Text.Length > 20 ? LblNombre.Text.Substring(0, 20) : LblNombre.Text) + "', @IdMesero, 0, @Estatus, @IdCliente);" +
                                       "SELECT SCOPE_IDENTITY();";
                        using (SqlCommand cmd = new SqlCommand(query, conectar))
                        {
                            cmd.Parameters.AddWithValue("@IdMesero", idMesero);
                            cmd.Parameters.AddWithValue("@Estatus", "COCINA");
                            cmd.Parameters.AddWithValue("@IdCliente", idCliente);
                            idMesa = Convert.ToInt32(cmd.ExecuteScalar());
                        }
                    }
                    else if (tabControl1.SelectedIndex == 2)
                    {
                        string query = "INSERT INTO Mesas (Nombre, IdMesero,Impresion,Estatus) " +
                                       "VALUES ('Para llevar', @IdMesero, 0, @Estatus);" +
                                       "SELECT SCOPE_IDENTITY();";
                        using (SqlCommand cmd = new SqlCommand(query, conectar))
                        {
                            cmd.Parameters.AddWithValue("@IdMesero", idMesero);
                            cmd.Parameters.AddWithValue("@Estatus", "COCINA");
                            idMesa = Convert.ToInt32(cmd.ExecuteScalar());
                        }
                    }
                    else if (checkBox3.Checked == false && CmbMesa.SelectedValue != null)
                        idMesa = (int)CmbMesa.SelectedValue;
                    if(CmbMesa.SelectedValue == null || CmbMesa.SelectedValue.ToString() == "0")
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
                    bool comanda = Convert.ToBoolean(DgvPedidoprevio.Rows[i].Cells["Comanda"].Value);
                    listado.Add((id, cantidad, descripcion, comentario, ides,comanda));

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
                                cmd.Parameters.AddWithValue("@IdPromo", idInventario.Substring(1, idInventario.Length - 1));
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

            txtBuscarProducto.Clear();
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

            if (txtBuscarProducto != null)
                txtBuscarProducto.Clear();

            Button botonFicticio = new Button { Tag = 0 };
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
            if (DgvPedidoprevio.CurrentRow == null) return;

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
        private void TxtBuscarProducto_AbrirTeclado(object sender, EventArgs e)
        {
            if(Conexion.AbrirTeclado)
                AbrirTeclado();
        }

        private void AbrirTeclado()
        {
            try
            {
                string rutaTeclado = @"C:\Jaeger Soft\FreeVK.exe";

                if (System.IO.File.Exists(rutaTeclado))
                {
                    // Solo lo inicia si no existe una instancia activa en ejecución
                    if (Process.GetProcessesByName("FreeVK").Length == 0)
                    {
                        Process.Start(rutaTeclado);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al intentar abrir el teclado virtual: " + ex.Message);
            }
        }
        private Size CalcularTamanoBotonProducto()
        {
            int anchoDisponible = flpInventario.ClientSize.Width - SystemInformation.VerticalScrollBarWidth;

            if (anchoDisponible <= 0) anchoDisponible = 500; // Valor fallback de seguridad

            // Determinamos número de columnas según el tamaño del área
            int columnas = 3; // Por defecto 3 columnas
            if (anchoDisponible > 700) columnas = 5;
            else if (anchoDisponible > 450) columnas = 4;

            int margenHorizontal = 8; // Espacio entre botones
            int anchoBoton = (anchoDisponible / columnas) - margenHorizontal;

            // Proporción táctil: la altura es aproximadamente el 55% del ancho
            int altoBoton = Math.Max((int)(anchoBoton * 0.35), 65);

            return new Size(anchoBoton, altoBoton);
        }
        private Size CalcularTamanoBotonCategoria()
        {
            int anchoDisponible = flpCategorias.ClientSize.Width - SystemInformation.VerticalScrollBarWidth;
            if (anchoDisponible <= 0) anchoDisponible = 500;

            // Queremos que quepan más categorías por fila (ej. 5 a 7 según la pantalla)
            int columnas = 5;
            if (anchoDisponible > 1000) columnas = 7;
            else if (anchoDisponible > 700) columnas = 6;

            int margenHorizontal = 6;
            int anchoBoton = (anchoDisponible / columnas) - margenHorizontal;

            // Altura compacta y fija para categorías (entre 45px y 50px es lo ideal para POS)
            int altoBoton = 48;

            return new Size(anchoBoton, altoBoton);
        }
    }
}