using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Punto_Venta
{
    public partial class frmMesasOcupadas : frmBase
    {
        private ContextMenuStrip menuClicDerecho;

        public frmMesasOcupadas()
        {
            InitializeComponent();
            InicializarContextMenu();
        }

        // Crear el menú contextual para el clic derecho
        private void InicializarContextMenu()
        {
            menuClicDerecho = new ContextMenuStrip();
            ToolStripMenuItem itemEditar = new ToolStripMenuItem("Editar Mesa");
            itemEditar.Click += new EventHandler(EditarMesa_Click);
            menuClicDerecho.Items.Add(itemEditar);
        }

        private void frmMesasOcupadas_Load(object sender, EventArgs e)
        {
            CargarMesas();
        }

        public void CargarMesas()
        {
            flowBotones.Controls.Clear();

            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();

                // 1. Mesas estándar (Sin cliente asignado)
                string query = @"SELECT A.IdMesa, A.Nombre, B.Usuario AS Mesero, A.CantidadPersonas, A.Impresion, A.IdMesero
                                 FROM MESAS A INNER JOIN USUARIOS B ON A.IdMesero = B.IdUsuario
                                 WHERE A.Estatus = 'COCINA' AND IdCliente IS NULL
                                 ORDER BY Nombre";

                using (SqlCommand cmd = new SqlCommand(query, conectar))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Button but = CrearBotonMesa(reader, Color.SkyBlue);

                        but.Tag = new
                        {
                            Id = reader["IdMesa"].ToString(),
                            Nombre = reader["Nombre"].ToString(),
                            Impresion = reader["Impresion"].ToString(),
                            IdMesero = reader["IdMesero"].ToString(),
                            Mesero = reader["Mesero"].ToString(),
                            CantPersonas = reader["CantidadPersonas"] != DBNull.Value ? reader["CantidadPersonas"].ToString() : "1"
                        };

                        flowBotones.Controls.Add(but);
                    }
                }

                // 2. Mesas con Cliente asignado
                query = @"SELECT A.IdMesa, A.Nombre, B.Usuario AS Mesero, A.CantidadPersonas, A.Impresion, A.IdMesero, A.IdCliente
                          FROM MESAS A INNER JOIN USUARIOS B ON A.IdMesero = B.IdUsuario
                          WHERE A.Estatus = 'COCINA' AND IdCliente IS NOT NULL
                          ORDER BY Nombre";

                using (SqlCommand cmd = new SqlCommand(query, conectar))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Button but = CrearBotonMesa(reader, Color.YellowGreen);

                        but.Tag = new
                        {
                            Id = reader["IdMesa"].ToString(),
                            Nombre = reader["Nombre"].ToString(),
                            Impresion = reader["Impresion"].ToString(),
                            IdMesero = reader["IdMesero"].ToString(),
                            Mesero = reader["Mesero"].ToString(),
                            CantPersonas = reader["CantidadPersonas"] != DBNull.Value ? reader["CantidadPersonas"].ToString() : "1",
                            IdCliente = int.Parse(reader["IdCliente"].ToString())
                        };

                        flowBotones.Controls.Add(but);
                    }
                }
            }
        }

        // Método auxiliar para no repetir la configuración básica de cada botón
        private Button CrearBotonMesa(SqlDataReader reader, Color colorFondo)
        {
            Button but = new Button();
            but.FlatStyle = FlatStyle.Flat;
            but.FlatAppearance.BorderSize = 0;
            but.Font = new Font(new FontFamily("Calibri"), 16, FontStyle.Bold);
            but.Size = new Size(135, 80);
            but.Text = reader["Nombre"].ToString();
            but.BackColor = colorFondo;

            // Evento Click unificado (maneja clic izquierdo y derecho)
            but.MouseClick += new MouseEventHandler(BotonMesa_MouseClick);
            but.MouseHover += new EventHandler(this.Myevent2);

            // Asignar el menú contextual al botón
            but.ContextMenuStrip = menuClicDerecho;

            return but;
        }

        // Maneja el clic sobre el botón de la mesa
        private void BotonMesa_MouseClick(object sender, MouseEventArgs e)
        {
            Button boton = sender as Button;
            var data = (dynamic)boton.Tag;

            // Clic Izquierdo -> Abrir cobros/pedidos (Flujo normal)
            if (e.Button == MouseButtons.Left)
            {
                bool abierto = false;
                foreach (Form frm in Application.OpenForms)
                {
                    if (frm.GetType() == typeof(frmCobros))
                    {
                        abierto = true;
                        frm.BringToFront();
                    }
                }

                if (!abierto)
                {
                    using (frmCobros cobrar = new frmCobros())
                    {
                        cobrar.lblID.Text = data.Id;
                        cobrar.lblMesa.Text = boton.Text;
                        cobrar.lblMesero.Text = data.Mesero;
                        cobrar.idMesero = int.Parse(data.IdMesero);
                        cobrar.print = data.Impresion == "True" ? "1" : "0";

                        if (boton.BackColor == Color.SkyBlue)
                            cobrar.lblPersonas.Text = data.CantPersonas;
                        else
                            cobrar.idCliente = data.IdCliente;

                        if (cobrar.ShowDialog() == DialogResult.OK)
                        {
                            CargarMesas();
                        }
                    }
                }
            }
        }

        // Evento que se ejecuta al seleccionar "Editar Mesa" en el menú contextual
        private void EditarMesa_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            ContextMenuStrip strip = item.Owner as ContextMenuStrip;
            Button botonTarget = strip.SourceControl as Button; // Obtiene el botón al que se le dio clic derecho

            if (botonTarget != null)
            {
                var data = (dynamic)botonTarget.Tag;

                using (frmAgregarMesas frmEditar = new frmAgregarMesas())
                {
                    frmEditar.EsEdicion = true;
                    frmEditar.IdMesa = int.Parse(data.Id);
                    frmEditar.Mesa = data.Nombre;
                    frmEditar.CantidadPersonas = int.Parse(data.CantPersonas);

                    if (frmEditar.ShowDialog() == DialogResult.OK)
                    {
                        CargarMesas(); // Recarga la cuadrícula con el nuevo nombre/personas
                    }
                }
            }
        }

        private void Myevent2(object sender, EventArgs e)
        {
            Button boton = sender as Button;
            var data = (dynamic)boton.Tag;
            lblMesero.Text = data.Mesero;
        }
    }
}