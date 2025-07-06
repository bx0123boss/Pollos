using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Linq;

namespace Punto_Venta
{
    public partial class frmAgregarPlatillo : Form
    {
        Double total=0;
        public string cat1, cat2;
        public string id = "0";
        public string idArticulo1 = "0", idArticulo2 = "0", idArticulo3 = "0", idArticulo4 = "0", idArticulo5 = "0", idArticulo6 = "0", idArticulo7 = "0", idArticulo8 = "0", idArticulo9 = "0", idArticulo10 = "0";
        public string Nombre1 = "0", Nombre2 = "0", Nombre3 = "0", Nombre4 = "0", Nombre5 = "0", Nombre6 = "0", Nombre7 = "0", Nombre8 = "0", Nombre9 = "0", Nombre10 = "0";
        public string Medida1 = "0", Medida2 = "0", Medida3 = "0", Medida4 = "0", Medida5 = "0", Medida6 = "0", Medida7 = "0", Medida8 = "0", Medida9 = "0", Medida10 = "0";
        public string Precio1 = "0", Precio2 = "0", Precio3 = "0", Precio4 = "0", Precio5 = "0", Precio6 = "0", Precio7 = "0", Precio8 = "0", Precio9 = "0", Precio10 = "0";
        public frmAgregarPlatillo()
        {
            InitializeComponent();
        }
        private void frmAgregarPlatillo_Load(object sender, EventArgs e)
        {
           
                using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Categorias;", conectar))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comboBox1.DisplayMember = "Nombre";
                    comboBox1.ValueMember = "IdCategoria";
                    comboBox1.DataSource = dt;
                }

                using (SqlCommand cmd = new SqlCommand("SELECT * FROM SubCategorias;", conectar))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comboBox2.DisplayMember = "Nombre";
                    comboBox2.ValueMember = "IdSubcategoria";
                    comboBox2.DataSource = dt;
                }
                if (this.Text == "Editar Platillo")
                {
                    if (!(id is null))
                    {
                        comboBox1.SelectedValue = cat1;
                        comboBox2.SelectedValue = cat2;
                        string query = @"SELECT 
                            ISNULL(i.IdProducto1, 0)AS IdProducto1,
                            ISNULL(p1.Nombre, '0') AS Producto1, 
                            ISNULL(p1.Medida, '0') AS Medida1, 
                            ISNULL(p1.Precio, 0) AS Precio1, 
                            ISNULL(i.CantidadProducto1, 0) AS CantidadProducto1,

                            ISNULL(i.IdProducto2, 0)AS IdProducto2,
                            ISNULL(p2.Nombre, '0') AS Producto2, 
                            ISNULL(p2.Medida, '0') AS Medida2, 
                            ISNULL(p2.Precio, 0) AS Precio2, 
                            ISNULL(i.CantidadProducto2, 0) AS CantidadProducto2,

                            ISNULL(i.IdProducto3, 0)AS IdProducto3,
                            ISNULL(p3.Nombre, '0') AS Producto3, 
                            ISNULL(p3.Medida, '0') AS Medida3, 
                            ISNULL(p3.Precio, 0) AS Precio3, 
                            ISNULL(i.CantidadProducto3, 0) AS CantidadProducto3,

                            ISNULL(i.IdProducto4, 0)AS IdProducto4,
                            ISNULL(p4.Nombre, '0') AS Producto4, 
                            ISNULL(p4.Medida, '0') AS Medida4, 
                            ISNULL(p4.Precio, 0) AS Precio4, 
                            ISNULL(i.CantidadProducto4, 0) AS CantidadProducto4,

                            ISNULL(i.IdProducto5, 0)AS IdProducto5,
                            ISNULL(p5.Nombre, '0') AS Producto5, 
                            ISNULL(p5.Medida, '0') AS Medida5, 
                            ISNULL(p5.Precio, 0) AS Precio5, 
                            ISNULL(i.CantidadProducto5, 0) AS CantidadProducto5,

                            ISNULL(i.IdProducto6, 0)AS IdProducto6,
                            ISNULL(p6.Nombre, '0') AS Producto6, 
                            ISNULL(p6.Medida, '0') AS Medida6, 
                            ISNULL(p6.Precio, 0) AS Precio6, 
                            ISNULL(i.CantidadProducto6, 0) AS CantidadProducto6,

                            ISNULL(i.IdProducto7, 0)AS IdProducto7,
                            ISNULL(p7.Nombre, '0') AS Producto7, 
                            ISNULL(p7.Medida, '0') AS Medida7, 
                            ISNULL(p7.Precio, 0) AS Precio7, 
                            ISNULL(i.CantidadProducto7, 0) AS CantidadProducto7,

                            ISNULL(i.IdProducto8, 0)AS IdProducto8,
                            ISNULL(p8.Nombre, '0') AS Producto8, 
                            ISNULL(p8.Medida, '0') AS Medida8, 
                            ISNULL(p8.Precio, 0) AS Precio8, 
                            ISNULL(i.CantidadProducto8, 0) AS CantidadProducto8,

                            ISNULL(i.IdProducto9, 0)AS IdProducto9,
                            ISNULL(p9.Nombre, '0') AS Producto9, 
                            ISNULL(p9.Medida, '0') AS Medida9, 
                            ISNULL(p9.Precio, 0) AS Precio9, 
                            ISNULL(i.CantidadProducto9, 0) AS CantidadProducto9,

                            ISNULL(i.IdProducto10, 0)AS IdProducto10,
                            ISNULL(p10.Nombre, '0') AS Producto10, 
                            ISNULL(p10.Medida, '0') AS Medida10, 
                            ISNULL(p10.Precio, 0) AS Precio10, 
                            ISNULL(i.CantidadProducto10, 0) AS CantidadProducto10
                        FROM INVENTARIO i
                        LEFT JOIN PRODUCTOS p1 ON i.IdProducto1 = p1.IdProducto
                        LEFT JOIN PRODUCTOS p2 ON i.IdProducto2 = p2.IdProducto
                        LEFT JOIN PRODUCTOS p3 ON i.IdProducto3 = p3.IdProducto
                        LEFT JOIN PRODUCTOS p4 ON i.IdProducto4 = p4.IdProducto
                        LEFT JOIN PRODUCTOS p5 ON i.IdProducto5 = p5.IdProducto
                        LEFT JOIN PRODUCTOS p6 ON i.IdProducto6 = p6.IdProducto
                        LEFT JOIN PRODUCTOS p7 ON i.IdProducto7 = p7.IdProducto
                        LEFT JOIN PRODUCTOS p8 ON i.IdProducto8 = p8.IdProducto
                        LEFT JOIN PRODUCTOS p9 ON i.IdProducto9 = p9.IdProducto
                        LEFT JOIN PRODUCTOS p10 ON i.IdProducto10 = p10.IdProducto
                        WHERE i.IdInventario = @IdInventario;
";
                        using (SqlCommand cmd = new SqlCommand(query, conectar))
                        {
                            cmd.Parameters.AddWithValue("@IdInventario", id);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    for (int i = 1; i <= 10; i++)
                                    {
                                        string idP = reader[$"IdProducto{i}"].ToString();
                                        string producto = reader[$"Producto{i}"].ToString();
                                        string medida = reader[$"Medida{i}"].ToString();
                                        string precio = reader[$"Precio{i}"].ToString();
                                        string cantidad = reader[$"CantidadProducto{i}"].ToString();

                                        // Asignar los valores a las variables globales usando Reflection
                                        GetType().GetField($"idArticulo{i}").SetValue(this, idP);
                                        GetType().GetField($"Nombre{i}").SetValue(this, producto);
                                        GetType().GetField($"Medida{i}").SetValue(this, medida);
                                        GetType().GetField($"Precio{i}").SetValue(this, precio);

                                        var lblNombre = this.Controls.Find($"lblNombre{i}", true).FirstOrDefault() as Label;
                                        lblNombre.Text = producto == "0" ? "No definido": producto;
                                        var lblMedida = this.Controls.Find($"lblMedida{i}", true).FirstOrDefault() as Label;
                                        lblMedida.Text = medida;
                                        var txtCantidad = this.Controls.Find($"txtCantidad{i}", true).FirstOrDefault() as TextBox;
                                        txtCantidad.Text = cantidad;
                                        var lblCosto = this.Controls.Find($"lblPrecio{i}", true).FirstOrDefault() as Label;
                                        lblCosto.Text = (Convert.ToDouble(cantidad) * Convert.ToDouble(precio)) + "";
                                       
                                    }
                                    lblTotal.Text = $"{GetTotal():C}";
                                }
                            }
                        }
                    }
                }
            }
        }
        private void btnArticulo1_Click(object sender, EventArgs e)
        {
            using (frmBusquedaArticulo ori = new frmBusquedaArticulo())
            {
                if (ori.ShowDialog() == DialogResult.OK)
                {
                    idArticulo1 = ori.Id;
                    Nombre1 = ori.Nombre;
                    Medida1 = ori.Medida;
                    Precio1 = ori.Precio;
                    lblNombre1.Text = Nombre1;
                    lblMedida1.Text = Medida1;
                    txtCantidad1.Focus();
                }
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (frmBusquedaArticulo ori = new frmBusquedaArticulo())
            {
                if (ori.ShowDialog() == DialogResult.OK)
                {
                    idArticulo2 = ori.Id;
                    Nombre2 = ori.Nombre;
                    Medida2 = ori.Medida;
                    Precio2 = ori.Precio;
                    lblNombre2.Text = Nombre2;
                    lblMedida2.Text = Medida2;
                    txtCantidad2.Focus();
                }
                
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (frmBusquedaArticulo ori = new frmBusquedaArticulo())
            {
                if (ori.ShowDialog() == DialogResult.OK)
                {
                    idArticulo3 = ori.Id;
                    Nombre3 = ori.Nombre;
                    Medida3 = ori.Medida;
                    Precio3 = ori.Precio;
                    lblNombre3.Text = Nombre3;
                    lblMedida3.Text = Medida3;
                    txtCantidad3.Focus();
                }
               
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            using (frmBusquedaArticulo ori = new frmBusquedaArticulo())
            {
                if (ori.ShowDialog() == DialogResult.OK)
                {
                    idArticulo6 = ori.Id;
                    Nombre6 = ori.Nombre;
                    Medida6 = ori.Medida;
                    Precio6 = ori.Precio;
                    lblNombre6.Text = Nombre6;
                    lblMedida6.Text = Medida6;
                    txtCantidad6.Focus();
                }
                
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            using (frmBusquedaArticulo ori = new frmBusquedaArticulo())
            {
                if (ori.ShowDialog() == DialogResult.OK)
                {
                    idArticulo7 = ori.Id;
                    Nombre7 = ori.Nombre;
                    Medida7 = ori.Medida;
                    Precio7 = ori.Precio;
                    lblNombre7.Text = Nombre7;
                    lblMedida7.Text = Medida7;
                    txtCantidad7.Focus();
                }
               
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            using (frmBusquedaArticulo ori = new frmBusquedaArticulo())
            {
                if (ori.ShowDialog() == DialogResult.OK)
                {
                    idArticulo8 = ori.Id;
                    Nombre8 = ori.Nombre;
                    Medida8 = ori.Medida;
                    Precio8 = ori.Precio;
                    lblNombre8.Text = Nombre8;
                    lblMedida8.Text = Medida8;
                    txtCantidad8.Focus();
                }
                
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (frmBusquedaArticulo ori = new frmBusquedaArticulo())
            {
                if (ori.ShowDialog() == DialogResult.OK)
                {
                    idArticulo4 = ori.Id;
                    Nombre4 = ori.Nombre;
                    Medida4 = ori.Medida;
                    Precio4 = ori.Precio;
                    lblNombre4.Text = Nombre4;
                    lblMedida4.Text = Medida4;
                    txtCantidad4.Focus();
                }
                
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            using (frmBusquedaArticulo ori = new frmBusquedaArticulo())
            {
                if (ori.ShowDialog() == DialogResult.OK)
                {
                    idArticulo9 = ori.Id;
                    Nombre9 = ori.Nombre;
                    Medida9 = ori.Medida;
                    Precio9 = ori.Precio;
                    lblNombre9.Text = Nombre9;
                    lblMedida9.Text = Medida9;
                    txtCantidad9.Focus();
                }
               
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (frmBusquedaArticulo ori = new frmBusquedaArticulo())
            {
                if (ori.ShowDialog() == DialogResult.OK)
                {
                    idArticulo5 = ori.Id;
                    Nombre5 = ori.Nombre;
                    Medida5 = ori.Medida;
                    Precio5 = ori.Precio;
                    lblNombre5.Text = Nombre5;
                    lblMedida5.Text = Medida5;
                    txtCantidad5.Focus();
                }
               
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            using (frmBusquedaArticulo ori = new frmBusquedaArticulo())
            {
                if (ori.ShowDialog() == DialogResult.OK)
                {
                    idArticulo10 = ori.Id;
                    Nombre10 = ori.Nombre;
                    Medida10 = ori.Medida;
                    Precio10 = ori.Precio;
                    lblNombre10.Text = Nombre10;
                    lblMedida10.Text = Medida10;
                    txtCantidad10.Focus();
                }
               
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPrecio.Text) || string.IsNullOrEmpty(txtNombre.Text))
                return;
            string comanda = checkBox1.Checked ? "1" : "0";

            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();

                if (this.Text == "Agregar Platillo")
                {
                    string query = @"
            INSERT INTO Inventario (
                Nombre, Precio, idProducto1, CantidadProducto1, idProducto2, CantidadProducto2, 
                idProducto3, CantidadProducto3, idProducto4, CantidadProducto4, idProducto5, 
                CantidadProducto5, idProducto6, CantidadProducto6, idProducto7, CantidadProducto7, 
                idProducto8, CantidadProducto8, idProducto9, CantidadProducto9, idProducto10, 
                CantidadProducto10, IdCategoria, CostoTotal, Comanda, IdSubCategoria, Estatus
            ) VALUES (
                @Nombre, @Precio, @idProducto1, @CantidadProducto1, @idProducto2, @CantidadProducto2, 
                @idProducto3, @CantidadProducto3, @idProducto4, @CantidadProducto4, @idProducto5, 
                @CantidadProducto5, @idProducto6, @CantidadProducto6, @idProducto7, @CantidadProducto7, 
                @idProducto8, @CantidadProducto8, @idProducto9, @CantidadProducto9, @idProducto10, 
                @CantidadProducto10, @Categoria, @CostoTotal, @Comanda, @SubCategoria, 1
            );";
                   

                    using (SqlCommand cmd2 = new SqlCommand(query, conectar))
                    {
                        cmd2.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                        cmd2.Parameters.AddWithValue("@Precio", txtPrecio.Text);
                        cmd2.Parameters.AddWithValue("@idProducto1", idArticulo1 == "0" ? (object)DBNull.Value : int.Parse(idArticulo1));
                        cmd2.Parameters.AddWithValue("@CantidadProducto1", idArticulo1 == "0" ? (object)DBNull.Value : txtCantidad1.Text);
                        cmd2.Parameters.AddWithValue("@idProducto2", idArticulo2 == "0" ? (object)DBNull.Value : int.Parse(idArticulo2));
                        cmd2.Parameters.AddWithValue("@CantidadProducto2", idArticulo2 == "0" ? (object)DBNull.Value : txtCantidad2.Text);
                        cmd2.Parameters.AddWithValue("@idProducto3", idArticulo3 == "0" ? (object)DBNull.Value : int.Parse(idArticulo3));
                        cmd2.Parameters.AddWithValue("@CantidadProducto3", idArticulo3 == "0" ? (object)DBNull.Value : txtCantidad3.Text);
                        cmd2.Parameters.AddWithValue("@idProducto4", idArticulo4 == "0" ? (object)DBNull.Value : int.Parse(idArticulo4));
                        cmd2.Parameters.AddWithValue("@CantidadProducto4", idArticulo4 == "0" ? (object)DBNull.Value : txtCantidad4.Text);
                        cmd2.Parameters.AddWithValue("@idProducto5", idArticulo5 == "0" ? (object)DBNull.Value : int.Parse(idArticulo5));
                        cmd2.Parameters.AddWithValue("@CantidadProducto5", idArticulo5 == "0" ? (object)DBNull.Value : txtCantidad5.Text);
                        cmd2.Parameters.AddWithValue("@idProducto6", idArticulo6 == "0" ? (object)DBNull.Value : int.Parse(idArticulo6));
                        cmd2.Parameters.AddWithValue("@CantidadProducto6", idArticulo6 == "0" ? (object)DBNull.Value : txtCantidad6.Text);
                        cmd2.Parameters.AddWithValue("@idProducto7", idArticulo7 == "0" ? (object)DBNull.Value : int.Parse(idArticulo7));
                        cmd2.Parameters.AddWithValue("@CantidadProducto7", idArticulo7 == "0" ? (object)DBNull.Value : txtCantidad7.Text);
                        cmd2.Parameters.AddWithValue("@idProducto8", idArticulo8 == "0" ? (object)DBNull.Value : int.Parse(idArticulo8));
                        cmd2.Parameters.AddWithValue("@CantidadProducto8", idArticulo8 == "0" ? (object)DBNull.Value : txtCantidad8.Text);
                        cmd2.Parameters.AddWithValue("@idProducto9", idArticulo9 == "0" ? (object)DBNull.Value : int.Parse(idArticulo9));
                        cmd2.Parameters.AddWithValue("@CantidadProducto9", idArticulo9 == "0" ? (object)DBNull.Value : txtCantidad9.Text);
                        cmd2.Parameters.AddWithValue("@idProducto10", idArticulo10 == "0" ? (object)DBNull.Value : int.Parse(idArticulo10));
                        cmd2.Parameters.AddWithValue("@CantidadProducto10", idArticulo10 == "0" ? (object)DBNull.Value : txtCantidad10.Text);
                        cmd2.Parameters.AddWithValue("@Categoria", comboBox1.SelectedValue);
                        cmd2.Parameters.AddWithValue("@CostoTotal", GetTotal());
                        cmd2.Parameters.AddWithValue("@Comanda", comanda);
                        cmd2.Parameters.AddWithValue("@SubCategoria", comboBox2.SelectedValue);
                        cmd2.ExecuteNonQuery();
                    }

                    MessageBox.Show("Se ha agregado el platillo con éxito", "AGREGADO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else if (this.Text == "Editar Platillo")
                {
                    string query = @"
            UPDATE Inventario SET 
                Nombre = @Nombre, Precio = @Precio, idProducto1 = @idProducto1, 
                CantidadProducto1 = @CantidadProducto1, idProducto2 = @idProducto2, 
                CantidadProducto2 = @CantidadProducto2, idProducto3 = @idProducto3, 
                CantidadProducto3 = @CantidadProducto3, idProducto4 = @idProducto4, 
                CantidadProducto4 = @CantidadProducto4, idProducto5 = @idProducto5, 
                CantidadProducto5 = @CantidadProducto5, idProducto6 = @idProducto6, 
                CantidadProducto6 = @CantidadProducto6, idProducto7 = @idProducto7, 
                CantidadProducto7 = @CantidadProducto7, idProducto8 = @idProducto8, 
                CantidadProducto8 = @CantidadProducto8, idProducto9 = @idProducto9, 
                CantidadProducto9 = @CantidadProducto9, idProducto10 = @idProducto10, 
                CantidadProducto10 = @CantidadProducto10, IdCategoria = @Categoria, 
                CostoTotal = @CostoTotal, Comanda = @Comanda, IdSubCategoria = @SubCategoria 
            WHERE IdInventario = @Id;";

                    using (SqlCommand cmd2 = new SqlCommand(query, conectar))
                    {
                        cmd2.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                        cmd2.Parameters.AddWithValue("@Precio", txtPrecio.Text);
                        cmd2.Parameters.AddWithValue("@idProducto1", idArticulo1 == "0" ? (object)DBNull.Value : int.Parse(idArticulo1));
                        cmd2.Parameters.AddWithValue("@CantidadProducto1", idArticulo1 == "0" ? (object)DBNull.Value : txtCantidad1.Text);
                        cmd2.Parameters.AddWithValue("@idProducto2", idArticulo2 == "0" ? (object)DBNull.Value : int.Parse(idArticulo2));
                        cmd2.Parameters.AddWithValue("@CantidadProducto2", idArticulo2 == "0" ? (object)DBNull.Value : txtCantidad2.Text);
                        cmd2.Parameters.AddWithValue("@idProducto3", idArticulo3 == "0" ? (object)DBNull.Value : int.Parse(idArticulo3));
                        cmd2.Parameters.AddWithValue("@CantidadProducto3", idArticulo3 == "0" ? (object)DBNull.Value : txtCantidad3.Text);
                        cmd2.Parameters.AddWithValue("@idProducto4", idArticulo4 == "0" ? (object)DBNull.Value : int.Parse(idArticulo4));
                        cmd2.Parameters.AddWithValue("@CantidadProducto4", idArticulo4 == "0" ? (object)DBNull.Value : txtCantidad4.Text);
                        cmd2.Parameters.AddWithValue("@idProducto5", idArticulo5 == "0" ? (object)DBNull.Value : int.Parse(idArticulo5));
                        cmd2.Parameters.AddWithValue("@CantidadProducto5", idArticulo5 == "0" ? (object)DBNull.Value : txtCantidad5.Text);
                        cmd2.Parameters.AddWithValue("@idProducto6", idArticulo6 == "0" ? (object)DBNull.Value : int.Parse(idArticulo6));
                        cmd2.Parameters.AddWithValue("@CantidadProducto6", idArticulo6 == "0" ? (object)DBNull.Value : txtCantidad6.Text);
                        cmd2.Parameters.AddWithValue("@idProducto7", idArticulo7 == "0" ? (object)DBNull.Value : int.Parse(idArticulo7));
                        cmd2.Parameters.AddWithValue("@CantidadProducto7", idArticulo7 == "0" ? (object)DBNull.Value : txtCantidad7.Text);
                        cmd2.Parameters.AddWithValue("@idProducto8", idArticulo8 == "0" ? (object)DBNull.Value : int.Parse(idArticulo8));
                        cmd2.Parameters.AddWithValue("@CantidadProducto8", idArticulo8 == "0" ? (object)DBNull.Value : txtCantidad8.Text);
                        cmd2.Parameters.AddWithValue("@idProducto9", idArticulo9 == "0" ? (object)DBNull.Value : int.Parse(idArticulo9));
                        cmd2.Parameters.AddWithValue("@CantidadProducto9", idArticulo9 == "0" ? (object)DBNull.Value : txtCantidad9.Text);
                        cmd2.Parameters.AddWithValue("@idProducto10", idArticulo10 == "0" ? (object)DBNull.Value : int.Parse(idArticulo10));
                        cmd2.Parameters.AddWithValue("@CantidadProducto10", idArticulo10 == "0" ? (object)DBNull.Value : txtCantidad10.Text);
                        cmd2.Parameters.AddWithValue("@Categoria", comboBox1.SelectedValue);
                        cmd2.Parameters.AddWithValue("@CostoTotal", GetTotal());
                        cmd2.Parameters.AddWithValue("@Comanda", comanda);
                        cmd2.Parameters.AddWithValue("@SubCategoria", comboBox2.SelectedValue);
                        cmd2.Parameters.AddWithValue("@Id", int.Parse(id));

                        cmd2.ExecuteNonQuery();
                        MessageBox.Show("Se ha editado el platillo con éxito", "EDITADO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }

                
            }
        }
       
        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox txtCantidad = sender as TextBox;
                int index = int.Parse(txtCantidad.Name.Replace("txtCantidad", ""));
                Label lblPrecio = this.Controls.Find($"lblPrecio{index}", true).FirstOrDefault() as Label;
                // Obtener el precio directamente usando reflexión
                var property = GetType().GetField($"Precio{index}");
                double precioBase = property != null ? Convert.ToDouble(property.GetValue(this)) : 0;
            
                if (string.IsNullOrEmpty(txtCantidad.Text))
                {
                    lblPrecio.Text = "0"; // Si no hay cantidad, el precio es 0
                }
                else
                {
                    double cantidad = Convert.ToDouble(txtCantidad.Text);
                    lblPrecio.Text = (precioBase * cantidad).ToString(); // Calcular precio
                }

                total = GetTotal() ;
                

                lblTotal.Text = $"{total:C}";
            }
            catch
            {
                // Manejar errores (por ejemplo, si el usuario ingresa un valor no numérico)
            }
        }
        private double GetTotal()
        {
            double total = 0;
            for (int i = 1; i <= 10; i++)
            {
                Label lblPrecioActual = this.Controls.Find($"lblPrecio{i}", true).FirstOrDefault() as Label;
                if (lblPrecioActual != null && !string.IsNullOrEmpty(lblPrecioActual.Text))
                {
                    total += Convert.ToDouble(lblPrecioActual.Text);
                }
            }
            return total;
        }

        private void TxtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // Solo permitir un punto decimal
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
        private void TxtCantidad_Leave(object sender, EventArgs e)
        {
            TextBox txtCantidad = sender as TextBox;
            if (string.IsNullOrEmpty(txtCantidad.Text))
            {
                txtCantidad.Text = "0";
            }
        }

    }
}
