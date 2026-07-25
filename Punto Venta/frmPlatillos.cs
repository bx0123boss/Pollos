using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace Punto_Venta
{
    public partial class frmPlatillos : frmBase
    {
        // DTO Modelo para mapear los platillos del inventario
        public class Platillo
        {
            public string IdInventario { get; set; }
            public string Nombre { get; set; }
            public decimal Precio { get; set; }
            public string Categoria { get; set; }
            public bool Comanda { get; set; }
            public string Subcategoria { get; set; }
            public string IdCategoria { get; set; }
            public string IdSubcategoria { get; set; }
            public decimal CostoTotal { get; set; }
        }

        // Variables de paginación
        private List<Platillo> listaCompletaPlatillos;
        private List<Platillo> listaFiltradaPlatillos;
        private int tamanoPagina = 26;
        private int paginaActual = 1;
        private int totalPaginas = 0;

        public frmPlatillos()
        {
            InitializeComponent();
        }

        private void frmPlatillos_Load(object sender, EventArgs e)
        {
            // Estilizar controles
            EstilizarDataGridView(dgvInventario);
            EstilizarBotonPrimario(button1);
            EstilizarBotonAdvertencia(button2);
            EstilizarBotonPeligro(button3);
            EstilizarComboBox(comboBox2);
            EstilizarTextBox(textBox1);

            EstilizarBotonPaginador(btnPrimero);
            EstilizarBotonPaginador(btnAnterior);
            EstilizarBotonPaginador(btnSiguiente);
            EstilizarBotonPaginador(btnUltimo);

            this.dgvInventario.ReadOnly = true;
            this.dgvInventario.AllowUserToAddRows = false;
            this.dgvInventario.AllowUserToDeleteRows = false;

            CargarCategorias();
            CargarDatosDesdeBD();
        }

        private void CargarCategorias()
        {
            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                DataTable dt = new DataTable();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Categorias;", conectar))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                    comboBox2.SelectedIndexChanged -= comboBox2_SelectedIndexChanged; // Prevenir disparo prematuro
                    comboBox2.DisplayMember = "Nombre";
                    comboBox2.ValueMember = "IdCategoria";
                    comboBox2.DataSource = dt;
                    comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
                }
            }
        }

        private void CargarDatosDesdeBD()
        {
            listaCompletaPlatillos = new List<Platillo>();

            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                string query = @"SELECT A.IdInventario, A.Nombre, A.Precio, B.Nombre AS Categoria, 
                                        A.Comanda, C.Nombre AS Subcategoria, A.IdCategoria, 
                                        A.IdSubcategoria, ISNULL(A.CostoTotal, 0) AS CostoTotal
                                 FROM Inventario A
                                 INNER JOIN CATEGORIAS B ON A.IdCategoria = B.IdCategoria
                                 LEFT JOIN SUBCATEGORIAS C ON A.IdSubcategoria = C.IdSubcategoria
                                 WHERE A.Estatus = 1 ORDER BY A.Nombre;";

                using (SqlCommand cmd = new SqlCommand(query, conectar))
                {
                    conectar.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Platillo item = new Platillo
                            {
                                IdInventario = reader["IdInventario"].ToString(),
                                Nombre = reader["Nombre"].ToString(),
                                Precio = reader["Precio"] == DBNull.Value ? 0m : Convert.ToDecimal(reader["Precio"]),
                                Categoria = reader["Categoria"].ToString(),
                                Comanda = reader["Comanda"] != DBNull.Value && Convert.ToBoolean(reader["Comanda"]),
                                Subcategoria = reader["Subcategoria"] == DBNull.Value ? "" : reader["Subcategoria"].ToString(),
                                IdCategoria = reader["IdCategoria"].ToString(),
                                IdSubcategoria = reader["IdSubcategoria"] == DBNull.Value ? "" : reader["IdSubcategoria"].ToString(),
                                CostoTotal = Convert.ToDecimal(reader["CostoTotal"])
                            };
                            listaCompletaPlatillos.Add(item);
                        }
                    }
                }
            }

            AplicarFiltros();
        }

        #region MOTOR DE FILTRADO Y PAGINACIÓN

        private void AplicarFiltros()
        {
            if (listaCompletaPlatillos == null) return;

            var filtro = listaCompletaPlatillos.AsEnumerable();

            // Filtro por Búsqueda de Texto
            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {
                filtro = filtro.Where(p => p.Nombre != null &&
                                           p.Nombre.IndexOf(textBox1.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            // Filtro por Categoría CheckBox
            if (checkBox1.Checked && comboBox2.SelectedValue != null)
            {
                string idCatSeleccionada = comboBox2.SelectedValue.ToString();
                filtro = filtro.Where(p => p.IdCategoria == idCatSeleccionada);
            }

            listaFiltradaPlatillos = filtro.ToList();
            paginaActual = 1;
            CargarDatosPaginados();
        }

        private void CargarDatosPaginados()
        {
            if (listaFiltradaPlatillos == null || listaFiltradaPlatillos.Count == 0)
            {
                dgvInventario.DataSource = null;
                totalPaginas = 1;
                paginaActual = 1;
                ActualizarControlesNavegacion();
                return;
            }

            totalPaginas = (int)Math.Ceiling((double)listaFiltradaPlatillos.Count / tamanoPagina);

            if (paginaActual > totalPaginas) paginaActual = totalPaginas;
            if (paginaActual < 1) paginaActual = 1;

            var datosPaginados = listaFiltradaPlatillos
                .Skip((paginaActual - 1) * tamanoPagina)
                .Take(tamanoPagina)
                .ToList();

            dgvInventario.DataSource = datosPaginados;
            OcultarColumnasClave();
            ActualizarControlesNavegacion();
        }

        private void OcultarColumnasClave()
        {
            if (dgvInventario.Columns.Count > 0)
            {
                if (dgvInventario.Columns.Contains("IdInventario")) dgvInventario.Columns["IdInventario"].Visible = false;
                if (dgvInventario.Columns.Contains("IdCategoria")) dgvInventario.Columns["IdCategoria"].Visible = false;
                if (dgvInventario.Columns.Contains("IdSubcategoria")) dgvInventario.Columns["IdSubcategoria"].Visible = false;
            }
        }

        private void ActualizarControlesNavegacion()
        {
            lblEstado.Text = $"Página {paginaActual} de {totalPaginas}";

            btnPrimero.Enabled = (paginaActual > 1);
            btnAnterior.Enabled = (paginaActual > 1);

            btnSiguiente.Enabled = (paginaActual < totalPaginas);
            btnUltimo.Enabled = (paginaActual < totalPaginas);
        }

        private void EstilizarBotonPaginador(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            btn.FlatAppearance.BorderSize = 1;
            btn.Cursor = Cursors.Hand;

            btn.EnabledChanged += (s, e) =>
            {
                if (btn.Enabled)
                {
                    btn.BackColor = System.Drawing.Color.FromArgb(45, 45, 45);
                    btn.ForeColor = System.Drawing.Color.White;
                    btn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(70, 70, 70);
                }
                else
                {
                    btn.BackColor = System.Drawing.Color.FromArgb(25, 25, 25);
                    btn.ForeColor = System.Drawing.Color.FromArgb(80, 80, 80);
                    btn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(40, 40, 40);
                }
            };
        }

        #endregion

        #region EVENTOS DEL PAGINADOR

        private void btnPrimero_Click(object sender, EventArgs e)
        {
            if (paginaActual > 1)
            {
                paginaActual = 1;
                CargarDatosPaginados();
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (paginaActual > 1)
            {
                paginaActual--;
                CargarDatosPaginados();
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (paginaActual < totalPaginas)
            {
                paginaActual++;
                CargarDatosPaginados();
            }
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            if (paginaActual < totalPaginas)
            {
                paginaActual = totalPaginas;
                CargarDatosPaginados();
            }
        }

        #endregion

        #region EVENTOS DE FILTROS Y ACCIONES

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            AplicarFiltros();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            AplicarFiltros();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                AplicarFiltros();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmAgregarPlatillo platillo = new frmAgregarPlatillo();
            platillo.Text = "Agregar Platillo";
            platillo.ShowDialog();

            // Refrescar lista completa desde base de datos
            CargarDatosDesdeBD();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dgvInventario.CurrentRow == null) return;

            Platillo seleccionado = (Platillo)dgvInventario.CurrentRow.DataBoundItem;

            frmAgregarPlatillo platillo = new frmAgregarPlatillo();
            platillo.Text = "Editar Platillo";
            platillo.id = seleccionado.IdInventario;
            platillo.txtNombre.Text = seleccionado.Nombre;
            platillo.txtPrecio.Text = seleccionado.Precio.ToString();
            platillo.cat1 = seleccionado.IdCategoria;
            platillo.cat2 = seleccionado.IdSubcategoria;
            platillo.checkBox1.Checked = seleccionado.Comanda;

            platillo.ShowDialog();
            CargarDatosDesdeBD();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dgvInventario.CurrentRow == null) return;

            Platillo seleccionado = (Platillo)dgvInventario.CurrentRow.DataBoundItem;

            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();
                using (SqlCommand cmd = new SqlCommand("UPDATE INVENTARIO SET Estatus = 0 WHERE IdInventario = @Id;", conectar))
                {
                    cmd.Parameters.AddWithValue("@Id", seleccionado.IdInventario);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Se ha eliminado con éxito", "Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarDatosDesdeBD();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            frmGastos gas = new frmGastos();
            gas.Show();
            this.Close();
        }

        #endregion
    }
}