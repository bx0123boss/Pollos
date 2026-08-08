using JaegerSoft;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace Punto_Venta
{
    public partial class frmCobros : frmBase
    {
        double iva;
        double descuento;
        double total = 0;
        double precioVenta = 0;
        string tipoUser = "";
        public int idMesero = 0;
        public string print = "";
        public int idCliente = 0;
        private int folio;

        // Propiedad para conservar el desglose de pagos procesados
        private Dictionary<string, double> ultimosPagosRealizados = new Dictionary<string, double>();

        public frmCobros()
        {
            InitializeComponent();
            this.MinimumSize = new Size(810, 400);
        }

        private double RecalcularTotal
        {
            get
            {
                precioVenta = 0;
                total = 0;
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    total += Convert.ToDouble(dataGridView1.Rows[i].Cells["Total"].Value);
                    precioVenta += Convert.ToDouble(dataGridView1.Rows[i].Cells["CostoTotal"].Value);
                }
                return total - descuento;
            }
        }

        private void frmCobros_Load(object sender, EventArgs e)
        {
            if (print == "1")
                button1.Visible = false;

            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();
                DataSet ds = new DataSet();
                string query = @"
                    SELECT 
                        A.IdInventario, 
                        A.IdArticulosMesa, 
                        A.Cantidad, 
                        CASE 
                            WHEN A.IdInventario = 0 THEN P.Nombre 
                            ELSE B.Nombre 
                        END AS Nombre,
                        CASE 
                            WHEN A.IdInventario = 0 THEN P.Precio 
                            ELSE B.Precio 
                        END AS Precio, 
                        A.Total,
                        A.FechaHora, 
                        A.Comentario, 
                        A.Ids, 
                        CASE 
                            WHEN A.Ids IS NOT NULL THEN ISNULL(SumCostoTotal.SumCosto, 0)
                            ELSE B.CostoTotal 
                        END AS CostoTotal, 
                        A.IdPromo
                    FROM ArticulosMesa A
                    INNER JOIN INVENTARIO B ON A.IdInventario = B.IdInventario
                    LEFT JOIN Promos P ON A.IdPromo = P.IdPromo
                    OUTER APPLY (
                        SELECT SUM(I.CostoTotal) AS SumCosto
                        FROM dbo.SplitString(A.Ids, ';') S
                        CROSS APPLY (
                            SELECT CAST(SUBSTRING(S.Value, CHARINDEX(',', S.Value) + 1, LEN(S.Value)) AS INT) AS IdProducto
                        ) AS Productos
                        INNER JOIN INVENTARIO I ON Productos.IdProducto = I.IdInventario
                    ) AS SumCostoTotal
                    WHERE A.IdUsuarioCancelo IS NULL 
                    AND A.Estatus = 'COCINA'
                    AND A.IdMesa = @Mesa";

                using (SqlDataAdapter da = new SqlDataAdapter(query, conectar))
                {
                    da.SelectCommand.Parameters.AddWithValue("@Mesa", $"{lblID.Text}");
                    da.Fill(ds, "Articulos");
                }

                if (idCliente != 0)
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Clientes WHERE IdCliente= @IdCliente;", conectar))
                    {
                        cmd.Parameters.AddWithValue("@IdCliente", idCliente);

                        using (SqlDataReader sqlDataReader = cmd.ExecuteReader())
                        {
                            if (sqlDataReader.Read())
                            {
                                lblNombre.Text = sqlDataReader["Nombre"].ToString();
                                string telefono = sqlDataReader["Telefono"].ToString();
                                string telefonoFormateado = $"({telefono.Substring(0, 3)}) {telefono.Substring(3, 3)}-{telefono.Substring(6, 4)}";
                                lblTelefono.Text = telefonoFormateado;
                                lblDirección.Text = sqlDataReader["Direccion"].ToString();
                                lblColonia.Text = sqlDataReader["Colonia"].ToString();
                                gbClientes.Visible = true;
                            }
                        }
                    }
                }

                dataGridView1.DataSource = ds.Tables["Articulos"];
                dataGridView1.Columns["Precio"].DefaultCellStyle.Format = "N2";
                dataGridView1.Columns["Total"].DefaultCellStyle.Format = "N2";
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns["Ids"].Visible = false;
                dataGridView1.Columns["IdPromo"].Visible = false;
                dataGridView1.Columns["CostoTotal"].Visible = false;
                dataGridView1.Columns[1].Visible = false;
                lblTotal.Text = $"{RecalcularTotal:C}";
            }
        }

        public void imprimir()
        {
            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();
                using (SqlCommand cmd = new SqlCommand("UPDATE MESAS SET Impresion = 1 WHERE IdMesa = @IdMesa;", conectar))
                {
                    cmd.Parameters.AddWithValue("@IdMesa", lblID.Text);
                    cmd.ExecuteNonQuery();
                }
            }

            List<Producto> listaProductos = new List<Producto>();

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                var row = dataGridView1.Rows[i];

                listaProductos.Add(new Producto
                {
                    Nombre = row.Cells["Nombre"].Value?.ToString() ?? "",
                    Cantidad = Convert.ToDouble(row.Cells["Cantidad"].Value),
                    PrecioUnitario = Convert.ToDouble(row.Cells["Precio"].Value),
                    Total = Convert.ToDouble(row.Cells["Total"].Value),
                    Comentario = row.Cells["Comentario"].Value?.ToString()
                });

                string idInventario = row.Cells["IdInventario"].Value?.ToString() ?? "";
                string ids = row.Cells["Ids"].Value?.ToString() ?? "";

                if (idInventario.StartsWith("C") && !string.IsNullOrEmpty(ids))
                {
                    string[] itemsCombo = ids.Split(';');
                    foreach (var item in itemsCombo)
                    {
                        string[] partes = item.Split(',');
                        if (partes.Length >= 2)
                        {
                            double cantidadSub = Convert.ToDouble(partes[0]);
                            string idProductoSub = partes[1];

                            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
                            {
                                conectar.Open();
                                using (SqlCommand cmd = new SqlCommand("SELECT Nombre FROM Inventario WHERE IdInventario = @Id;", conectar))
                                {
                                    cmd.Parameters.AddWithValue("@Id", idProductoSub);
                                    object nombreSub = cmd.ExecuteScalar();

                                    if (nombreSub != null)
                                    {
                                        listaProductos.Add(new Producto
                                        {
                                            Nombre = $"  - {nombreSub}",
                                            Cantidad = cantidadSub,
                                            PrecioUnitario = 0,
                                            Total = 0
                                        });
                                    }
                                }
                            }
                        }
                    }
                }
            }

            // Preparar los totales para el ticket respetando los conceptos adicionales
            Dictionary<string, double> totalesDict = new Dictionary<string, double>();

            if (descuento > 0)
            {
                totalesDict.Add("DESCUENTO", descuento);
            }

            totalesDict.Add("TOTAL", total - descuento);

            // Incluir el desglose de Propina en los totales del TicketPrinter si existe
            if (ultimosPagosRealizados != null)
            {
                double propina = ultimosPagosRealizados
                    .Where(p => p.Key.Contains("PROPINA"))
                    .Sum(p => p.Value);

                if (propina > 0)
                {
                    totalesDict.Add("PROPINA", propina);
                }
            }

            string[] encabezados = Conexion.datosTicket ?? new string[] { };
            string[] pie = Conexion.pieDeTicket ?? new string[] { };
            string logoPath = @"C:\Jaeger Soft\logo.jpg";

            // Determinar la forma de pago excluyendo la propina para el resumen principal
            string formaPagoImpresion = "EFECTIVO";
            if (ultimosPagosRealizados != null && ultimosPagosRealizados.Count > 0)
            {
                var pagosSinPropina = ultimosPagosRealizados.Where(p => !p.Key.Contains("PROPINA")).ToList();
                formaPagoImpresion = pagosSinPropina.Count > 1 ? "MIXTO" : (pagosSinPropina.FirstOrDefault().Key ?? "EFECTIVO");
            }

            TicketPrinter printer = new TicketPrinter(
                encabezados: encabezados,
                pieDePagina: pie,
                logoPath: logoPath,
                productos: listaProductos,
                folio: folio.ToString(),
                mesa: lblMesa.Text,
                mesero: lblMesero.Text,
                total: (total - descuento),
                corte: false,
                totales: totalesDict,
                formaPago: formaPagoImpresion
            );
            printer.ImprimirTicket();

            //button1.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            imprimir();
        }

        private void txtPago_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as System.Windows.Forms.TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtCambio.Text = $"{(Convert.ToDouble(txtPago.Text) - total):C}";
                txtPago.Text = $"{(Convert.ToDouble(txtPago.Text)):C}";
                button2.Focus();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                using (frmCredito ori = new frmCredito())
                {
                    if (ori.ShowDialog() == DialogResult.OK)
                    {
                        iva = ori.iva;
                        double lol = total;
                        lol = Math.Truncate((lol * (iva / 100 + 1)) * 100) / 100;
                        lblTotal.Text = $"{lol:C}";
                    }
                }
            }
            else
            {
                lblTotal.Text = $"{total:C}";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount == 0)
            {
                MessageBox.Show("No hay productos cargados para cobrar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            double totalCobrar = RecalcularTotal;
            double efectivoRecibido = 0;
            double cambioDevuelto = 0;
            Dictionary<string, double> pagosFinales = new Dictionary<string, double>();

            // 1. Invocación al formulario de pagos
            using (frmPago formPago = new frmPago())
            {
                formPago.total = totalCobrar;
                if (formPago.ShowDialog() == DialogResult.OK)
                {
                    efectivoRecibido = formPago.efectivo;
                    cambioDevuelto = formPago.cambio;
                    pagosFinales = formPago.PagosRealizados;
                    ultimosPagosRealizados = new Dictionary<string, double>(formPago.PagosRealizados);
                }
                else
                {
                    return; // El usuario canceló la pantalla de pago
                }
            }

            // Excluir PROPINA para determinar la FormaPago del Folio
            var pagosVenta = pagosFinales.Where(p => !p.Key.Contains("PROPINA")).ToList();
            string formaPagoPrincipal = pagosVenta.Count > 1 ? "MIXTO" : (pagosVenta.FirstOrDefault().Key ?? "EFECTIVO");

            double ventas = 0;
            int mesas = 0;

            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();
                SqlTransaction transaccion = conectar.BeginTransaction();

                try
                {
                    // 2. Insertar el folio principal
                    string insertFolioQuery = @"INSERT INTO Folios (ModalidadVenta, Estatus, idCliente, FechaHora, Total, Descuento, Utilidad, IdMesa, FormaPago) 
                                        VALUES (@ModalidadVenta, @Estatus, @idCliente, @FechaHora, @Total, @Descuento, @Utilidad, @IdMesa, @FormaPago); 
                                        SELECT SCOPE_IDENTITY();";

                    int lastIdFolio = 0;

                    using (SqlCommand cmd = new SqlCommand(insertFolioQuery, conectar, transaccion))
                    {
                        string modalidadVenta;
                        if (lblMesa.Text == "Para llevar")
                            modalidadVenta = "PARA LLEVAR";
                        else if (idCliente == 0)
                            modalidadVenta = "MESA";
                        else
                            modalidadVenta = "DOMICILIO";

                        cmd.Parameters.AddWithValue("@ModalidadVenta", modalidadVenta);
                        cmd.Parameters.AddWithValue("@Estatus", "COBRADO");
                        cmd.Parameters.AddWithValue("@idCliente", idCliente == 0 ? (object)DBNull.Value : idCliente);
                        cmd.Parameters.AddWithValue("@FechaHora", DateTime.Now);
                        cmd.Parameters.AddWithValue("@Total", totalCobrar);
                        cmd.Parameters.AddWithValue("@Descuento", descuento);
                        cmd.Parameters.AddWithValue("@Utilidad", (totalCobrar - precioVenta));
                        cmd.Parameters.AddWithValue("@IdMesa", lblID.Text);
                        cmd.Parameters.AddWithValue("@FormaPago", formaPagoPrincipal);

                        lastIdFolio = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    // 3. Guardar detalle de los artículos cobrados
                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                        string insertArticuloQuery = @"INSERT INTO ArticulosFolio (IdInventario, IdFolio, Cantidad, Comentario, Total, IdExtra, IdPromo) 
                                               VALUES (@IdProducto, @IdFolio, @Cantidad, @Comentario, @Total, @IdExtra, @IdPromo);";

                        string ide = null;
                        if (dataGridView1.Rows[i].Cells["Ids"].Value != null && dataGridView1.Rows[i].Cells["Ids"].Value.ToString().Length > 0)
                        {
                            ide = dataGridView1.Rows[i].Cells["Ids"].Value.ToString();
                        }

                        using (SqlCommand cmd2 = new SqlCommand(insertArticuloQuery, conectar, transaccion))
                        {
                            cmd2.Parameters.AddWithValue("@IdProducto", dataGridView1.Rows[i].Cells["IdInventario"].Value);
                            cmd2.Parameters.AddWithValue("@IdFolio", lastIdFolio);
                            cmd2.Parameters.AddWithValue("@Cantidad", Convert.ToDecimal(dataGridView1.Rows[i].Cells["Cantidad"].Value));
                            cmd2.Parameters.AddWithValue("@Comentario", dataGridView1.Rows[i].Cells["Comentario"].Value?.ToString() ?? (object)DBNull.Value);
                            cmd2.Parameters.AddWithValue("@Total", Convert.ToDecimal(dataGridView1.Rows[i].Cells["Total"].Value));
                            cmd2.Parameters.AddWithValue("@IdExtra", (object)ide ?? DBNull.Value);
                            cmd2.Parameters.AddWithValue("@IdPromo",
                                dataGridView1.Rows[i].Cells["IdInventario"].Value?.ToString() == "0"
                                    ? (object)dataGridView1.Rows[i].Cells["IdPromo"].Value ?? DBNull.Value
                                    : DBNull.Value
                            );
                            cmd2.ExecuteNonQuery();
                        }

                        // Descontar temporal de inventario
                        string tempInvQuery = @"INSERT INTO tempInventario(id, cantidad, ide) VALUES (@id, @cantidad, @ide);";
                        using (SqlCommand cmd2 = new SqlCommand(tempInvQuery, conectar, transaccion))
                        {
                            cmd2.Parameters.AddWithValue("@id", dataGridView1.Rows[i].Cells["IdInventario"].Value?.ToString() == "0"
                                                                ? dataGridView1.Rows[i].Cells["IdPromo"].Value
                                                                : dataGridView1.Rows[i].Cells["IdInventario"].Value);
                            cmd2.Parameters.AddWithValue("@cantidad", Convert.ToDecimal(dataGridView1.Rows[i].Cells["Cantidad"].Value));
                            cmd2.Parameters.AddWithValue("@ide", (object)ide ?? DBNull.Value);

                            cmd2.ExecuteNonQuery();
                        }
                    }

                    // 4. REGISTRAR LOS PAGOS E INCLUIR LA PROPINA EN VentasPagos Y EN CORTE
                    foreach (KeyValuePair<string, double> pago in pagosFinales)
                    {
                        if (pago.Value > 0)
                        {
                            // Guardar en VentasPagos (incluye PROPINA)
                            string queryVentaPago = "INSERT INTO VentasPagos (IdFolio, MetodoPago, Monto) VALUES (@IdFolio, @Metodo, @Monto)";
                            using (SqlCommand cmdVP = new SqlCommand(queryVentaPago, conectar, transaccion))
                            {
                                cmdVP.Parameters.AddWithValue("@IdFolio", lastIdFolio);
                                cmdVP.Parameters.AddWithValue("@Metodo", pago.Key);
                                cmdVP.Parameters.AddWithValue("@Monto", pago.Value);
                                cmdVP.ExecuteNonQuery();
                            }

                            // Guardar en CORTE identificando la propina
                            string conceptoCorte = pago.Key.Contains("PROPINA")
                                ? $"PROPINA FOLIO: {lastIdFolio}"
                                : $"VENTA FOLIO: {lastIdFolio} [{pago.Key}]";

                            string queryCorte = @"INSERT INTO CORTE (Concepto, Total, FechaHora, FormaPago) 
                                          VALUES (@Concepto, @Total, GETDATE(), @FormaPago)";
                            using (SqlCommand cmdCorte = new SqlCommand(queryCorte, conectar, transaccion))
                            {
                                cmdCorte.Parameters.AddWithValue("@Concepto", conceptoCorte);
                                cmdCorte.Parameters.AddWithValue("@Total", pago.Value);
                                cmdCorte.Parameters.AddWithValue("@FormaPago", pago.Key);
                                cmdCorte.ExecuteNonQuery();
                            }
                        }
                    }

                    // 5. Actualizar estadísticas del mesero y estatus del sistema
                    folio = lastIdFolio;

                    using (SqlCommand cmd = new SqlCommand("SELECT Ventas, Mesas FROM Usuarios WHERE IdUsuario = @IdMesero;", conectar, transaccion))
                    {
                        cmd.Parameters.AddWithValue("@IdMesero", idMesero);
                        using (SqlDataReader sqlDataReader = cmd.ExecuteReader())
                        {
                            if (sqlDataReader.Read())
                            {
                                ventas = Convert.ToDouble(sqlDataReader["Ventas"]);
                                mesas = Convert.ToInt32(sqlDataReader["Mesas"]);
                            }
                        }
                    }

                    ventas += totalCobrar;
                    mesas++;

                    using (SqlCommand cmd2 = new SqlCommand("UPDATE Usuarios SET Ventas = @Ventas, Mesas = @Mesas WHERE IdUsuario = @IdMesero;", conectar, transaccion))
                    {
                        cmd2.Parameters.AddWithValue("@Ventas", ventas);
                        cmd2.Parameters.AddWithValue("@Mesas", mesas);
                        cmd2.Parameters.AddWithValue("@IdMesero", idMesero);
                        cmd2.ExecuteNonQuery();
                    }

                    using (SqlCommand cmd2 = new SqlCommand("UPDATE MESAS SET Estatus = 'COBRADO' WHERE IdMesa = @IdMesa;", conectar, transaccion))
                    {
                        cmd2.Parameters.AddWithValue("@IdMesa", lblID.Text);
                        cmd2.ExecuteNonQuery();
                    }

                    using (SqlCommand cmd2 = new SqlCommand("UPDATE ArticulosMesa SET Estatus = 'COBRADO' WHERE IdMesa = @IdMesa;", conectar, transaccion))
                    {
                        cmd2.Parameters.AddWithValue("@IdMesa", lblID.Text);
                        cmd2.ExecuteNonQuery();
                    }

                    // Confirmamos la transacción
                    transaccion.Commit();

                    // 6. Impresión de Ticket
                    if (print == "0")
                    {
                        // imprimir();
                    }

                    DialogResult dialogResult = MessageBox.Show("¿Imprimir otro ticket?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        imprimir();
                    }

                    MessageBox.Show("¡EL COBRO SE HA REALIZADO CON ÉXITO!", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                }
                catch (Exception ex)
                {
                    transaccion.Rollback();
                    MessageBox.Show("Error crítico al procesar el cobro: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un producto PARA CANCELAR", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool permisoMeseroMesa = idMesero > 0 && UsuarioTienePermiso(idMesero, "MOD_CANC_MESA");

            // 1. Si no tiene permiso, pedir la contraseña de un usuario autorizado (Administrador)
            if (!permisoMeseroMesa)
            {
                using (frmClaveVendendor ori = new frmClaveVendendor())
                {
                    if (ori.ShowDialog() == DialogResult.OK)
                    {
                        tipoUser = ori.Tipo;
                        if (tipoUser != "ADMINISTRADOR")
                        {
                            MessageBox.Show("El usuario ingresado no cuenta con privilegios de Administrador para autorizar la cancelación.",
                                            "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return;
                        }
                    }
                    else
                    {
                        return; // El usuario cerró o canceló la ventana de autorización
                    }
                }
            }

            // 2. Solicitar el motivo/comentario obligatorio para la auditoría de cancelación
            using (frmComentarios com = new frmComentarios())
            {
                if (com.ShowDialog() != DialogResult.OK || string.IsNullOrWhiteSpace(com.Comentario))
                {
                    MessageBox.Show("SE REQUIERE UN COMENTARIO PARA CANCELAR", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
                {
                    conectar.Open();

                    // 3. Cancelar el artículo seleccionado
                    string queryCancelArticulo = @"UPDATE ArticulosMesa 
                                         SET Comentario = @Comentario, 
                                             Estatus = 'CANCELADO', 
                                             IdUsuarioCancelo = @IdUsuarioCancelo 
                                         WHERE IdArticulosMesa = @IdArticulosMesa;";

                    using (SqlCommand cmd = new SqlCommand(queryCancelArticulo, conectar))
                    {
                        cmd.Parameters.AddWithValue("@Comentario", com.Comentario);
                        cmd.Parameters.AddWithValue("@IdUsuarioCancelo", idMesero);
                        cmd.Parameters.AddWithValue("@IdArticulosMesa", dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["IdArticulosMesa"].Value);

                        cmd.ExecuteNonQuery();
                    }

                    // 4. SELECT COUNT: Verificar en la BD si quedan artículos ACTIVOS en la mesa
                    string queryCount = @"SELECT COUNT(1) 
                                 FROM ArticulosMesa 
                                 WHERE IdMesa = @IdMesa 
                                   AND Estatus = 'COCINA' 
                                   AND IdUsuarioCancelo IS NULL;";

                    int articulosRestantes = 0;
                    using (SqlCommand cmdCount = new SqlCommand(queryCount, conectar))
                    {
                        cmdCount.Parameters.AddWithValue("@IdMesa", lblID.Text);
                        articulosRestantes = Convert.ToInt32(cmdCount.ExecuteScalar());
                    }
                    List<Producto> productosParaImprimir = new List<Producto>();
                    productosParaImprimir.Add(new Producto
                    {
                        Nombre = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Nombre"].Value?.ToString() ?? "",
                        Cantidad = Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Cantidad"].Value),
                        PrecioUnitario = Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Precio"].Value),
                        Total = Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Total"].Value),
                        Comentario = com.Comentario
                    });
                    TicketPrinter ticket = new TicketPrinter(productosParaImprimir, lblMesa.Text, lblMesero.Text, null, true);
                    ticket.ImprimirComanda(Conexion.impresora2);
                    // 5. Si ya NO quedan artículos activos en la mesa, cancelar la mesa completa
                    if (articulosRestantes == 0)
                    {
                        using (SqlCommand cmdMesa = new SqlCommand("UPDATE MESAS SET Estatus = 'CANCELADO' WHERE IdMesa = @IdMesa;", conectar))
                        {
                            cmdMesa.Parameters.AddWithValue("@IdMesa", lblID.Text);
                            cmdMesa.ExecuteNonQuery();
                        }

                        MessageBox.Show("¡EL PRODUCTO SE HA ELIMINADO Y LA MESA FUE CANCELADA CON ÉXITO AL NO QUEDAR MÁS ARTÍCULOS!", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        // Si aún quedan más artículos, solo remover de la vista local y recalcular totals
                        dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
                        ResetearDescuento();
                        MessageBox.Show("¡EL PRODUCTO SE HA ELIMINADO CON ÉXITO!", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
        /// <summary>
        /// Consulta en la Base de Datos si un usuario específico (ej. el mesero de la mesa) 
        /// tiene un permiso asignado o acceso total (ADMIN_TODO).
        /// </summary>
        private bool UsuarioTienePermiso(int idUsuario, string clavePermiso)
        {
            try
            {
                using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
                {
                    conectar.Open();
                    string query = @"SELECT COUNT(1) 
                             FROM PermisosUsuario 
                             WHERE IdUsuario = @IdUsuario 
                               AND (Permiso = @Permiso OR Permiso= 'ADMIN_TODO');";

                    using (SqlCommand cmd = new SqlCommand(query, conectar))
                    {
                        cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                        cmd.Parameters.AddWithValue("@Permiso", clavePermiso);

                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
        private void printDocument1_PrintPage_1(object sender, PrintPageEventArgs e)
        {
            int posicion = 10;
            Image logo = Image.FromFile("C:\\Jaeger Soft\\logo.jpg");
            e.Graphics.DrawImage(logo, new PointF(1, 10));

            posicion += 200;
            e.Graphics.DrawString("********  NOTA DE CONSUMO  ********", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(1, posicion));
            posicion += 20;
            e.Graphics.DrawString("FOLIO DE VENTA: " + folio, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(1, posicion));
            posicion += 20;
            e.Graphics.DrawString("MESA: " + lblMesa.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(1, posicion));
            posicion += 20;
            e.Graphics.DrawString("LE ATENDIO: " + lblMesero.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(1, posicion));
            posicion += 20;
            e.Graphics.DrawString("FECHA: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString(), new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(1, posicion));
            posicion += 50;

            e.Graphics.DrawString("Cant   Producto        P.Unit  Importe", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(1, posicion));
            posicion += 20;
            e.Graphics.DrawLine(new Pen(Color.Black), 1, posicion, 420, posicion);
            posicion += 10;

            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Far;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                double precio = Convert.ToDouble(dataGridView1.Rows[i].Cells["Total"].Value);
                string producto = dataGridView1.Rows[i].Cells["Nombre"].Value.ToString();
                double cant = Convert.ToDouble(dataGridView1.Rows[i].Cells["Cantidad"].Value);
                string item = cant.ToString("0.00", CultureInfo.InvariantCulture);
                string pre = precio.ToString("00.00", CultureInfo.InvariantCulture);
                double precioUni = Convert.ToDouble(dataGridView1.Rows[i].Cells["Precio"].Value.ToString());
                string uni = precioUni.ToString("00.00", CultureInfo.InvariantCulture);
                if (producto.Length > 20)
                {
                    producto = producto.Substring(0, 20);
                }

                e.Graphics.DrawString(item, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(1, posicion));
                e.Graphics.DrawString(producto, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(40, posicion));
                e.Graphics.DrawString(uni, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(230, posicion), sf);
                e.Graphics.DrawString(String.Format(CultureInfo.InvariantCulture, "{0:0,0.00}", precio), new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(280, posicion), sf);
                posicion += 20;

                string ide;
                if (dataGridView1[0, i].Value.ToString().Substring(0, 1) == "C")
                {
                    if (dataGridView1.Rows[i].Cells["Ids"].Value.ToString().Length > 0)
                    {
                        ide = dataGridView1[9, i].Value.ToString();
                        string[] ids = ide.Split(';');
                        string RESULT = "";
                        foreach (var word in ids)
                        {
                            string[] ids2 = word.Split(',');
                            for (int i2 = 0; i2 < ids2.Length - 1; i2 = i2 + 2)
                            {
                                using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
                                {
                                    conectar.Open();
                                    using (SqlCommand cmd = new SqlCommand("SELECT IdInventario,Nombre FROM Inventario where IdInventario= @IdCliente;", conectar))
                                    {
                                        cmd.Parameters.AddWithValue("@IdCliente", ids2[1]);

                                        using (SqlDataReader sqlDataReader = cmd.ExecuteReader())
                                        {
                                            while (sqlDataReader.Read())
                                            {
                                                precio = 0;
                                                producto = sqlDataReader[1].ToString();
                                                cant = Convert.ToDouble(ids2[0]);
                                                item = cant.ToString("0.00", CultureInfo.InvariantCulture);
                                                pre = precio.ToString("00.00", CultureInfo.InvariantCulture);
                                                precioUni = 0;
                                                uni = precioUni.ToString("00.00", CultureInfo.InvariantCulture);
                                                double cantCombo = Convert.ToDouble(dataGridView1[1, i].Value.ToString()) * cant;
                                                if (producto.Length > 20)
                                                {
                                                    producto = producto.Substring(0, 20);
                                                }

                                                e.Graphics.DrawString(item, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(1, posicion));
                                                e.Graphics.DrawString(producto, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(40, posicion));
                                                e.Graphics.DrawString(uni, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(230, posicion), sf);
                                                e.Graphics.DrawString(String.Format(CultureInfo.InvariantCulture, "{0:0,0.00}", precio), new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(280, posicion), sf);
                                                posicion += 20;
                                                RESULT += ids2[0] + " : " + sqlDataReader[1].ToString() + "\n";
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            double to = Convert.ToDouble(total - descuento);
            string toty = String.Format(CultureInfo.InvariantCulture, "{0:0,0.00}", to);
            e.Graphics.DrawLine(new Pen(Color.Black), 210, posicion + 10, 420, posicion + 10);
            posicion += 15;
            e.Graphics.DrawString("TOTAL: $" + toty, new Font("Arial", 10, FontStyle.Bold), Brushes.Black, new Point(280, posicion), sf);
            posicion += 20;

            // Imprimir línea de PROPINA si existe en la impresión tradicional de GDI+
            if (ultimosPagosRealizados != null)
            {
                double propina = ultimosPagosRealizados
                    .Where(p => p.Key.Contains("PROPINA"))
                    .Sum(p => p.Value);

                if (propina > 0)
                {
                    string propinaStr = String.Format(CultureInfo.InvariantCulture, "{0:0,0.00}", propina);
                    e.Graphics.DrawString("PROPINA: $" + propinaStr, new Font("Arial", 10, FontStyle.Bold), Brushes.Black, new Point(280, posicion), sf);
                    posicion += 20;
                }
            }

            posicion += 30;

            for (int i = 0; i < Conexion.pieDeTicket.Length; i++)
            {
                e.Graphics.DrawString(Conexion.pieDeTicket[i], new Font("Arial", 10, FontStyle.Bold), Brushes.Black, new Point(1, posicion));
                posicion += 20;
            }
            posicion += 20;
            e.Graphics.DrawLine(new Pen(Color.Black), 1, posicion, 2, posicion);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            txtDescuento.Enabled = true;
            txtDescuento.Focus();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            txtDescuento.Enabled = true;
            txtDescuento.Focus();
        }

        private void txtDescuento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Enter))
            {
                MessageBox.Show("Solo se permiten numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
            else if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                RealizarDescuento();
            }
        }

        private void RealizarDescuento()
        {
            if (radioButton1.Checked)
            {
                descuento = Convert.ToDouble(txtDescuento.Text);
                if (descuento > 100)
                {
                    MessageBox.Show("No se puede hacer un descuento mayor al 100%, favor de verificar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    ResetearDescuento();
                    return;
                }
                descuento = ((Convert.ToDouble(txtDescuento.Text) / 100)) * total;
            }
            else if (radioButton2.Checked)
            {
                descuento = Convert.ToDouble(txtDescuento.Text);
                if (descuento > total)
                {
                    MessageBox.Show("No se puede hacer un descuento mayor al total, favor de verificar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    ResetearDescuento();
                    return;
                }
            }
            lblTotal.Text = $"{RecalcularTotal:C}";
            lblDescuento.Text = $"{descuento:C}";
            label10.Visible = true;
            lblDescuento.Visible = true;
            MessageBox.Show($"DESCUENTO REALIZADO POR LA CANTIDAD DE: {descuento:C}", "DESCUENTO", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ResetearDescuento()
        {
            radioButton1.Checked = false;
            radioButton2.Checked = false;

            txtDescuento.Clear();
            descuento = 0;
            lblDescuento.Text = $"{descuento:C}";
            label10.Visible = false;
            lblDescuento.Visible = false;
            lblTotal.Text = $"{RecalcularTotal:C}";
        }

        private void txtDescuento_Leave(object sender, EventArgs e)
        {
            if (descuento != 0)
                return;
            else if (String.IsNullOrEmpty(txtDescuento.Text))
            {
                MessageBox.Show("Ingrese un valor de moneda válido (ejemplo: 13.45)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                RealizarDescuento();
            }
        }

        private void txtPago_Click(object sender, EventArgs e)
        {
            txtPago.Clear();
            txtCambio.Clear();
        }
    }
}