using System.Data.SqlClient;
using Punto_Venta;
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using static QuestPDF.Helpers.Colors;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Punto_Venta
{
    public partial class frmAgregarCompras : frmBase
    {
        private string idProv = "0";
        private string nombreProv = "";

        public frmAgregarCompras()
        {
            InitializeComponent();
        }

        private void frmAgregarCompras_Load(object sender, EventArgs e)
        {
            rdContado.Checked = true;

            // ESTILOS HEREDADOS DE frmBase
            EstilizarDataGridView(this.dataGridView1);

            EstilizarBotonPrimario(this.button2); // Botón Guardar
            EstilizarBotonPrimario(this.button1); // Botón Agregar
            EstilizarBotonAdvertencia(this.button4); // Buscar Producto
            EstilizarBotonAdvertencia(this.button5); // Buscar Proveedor

            // Ocultamos el botón calcular viejo ya que ahora todo es automático
            this.button3.Visible = false;

            EstilizarTextBox(txtFolio);
            EstilizarTextBox(txtID);
            EstilizarTextBox(txtCantidad);
            EstilizarTextBox(txtCosto);
            EstilizarTextBox(txtVenta);
            EstilizarTextBox(txtMenudeo);
            EstilizarTextBox(textBox1); // Costos Extra

        }

        // =========================================================
        // MOTOR DE CÁLCULO AUTOMÁTICO
        // =========================================================
        private void CalcularTotales()
        {
            double totalCostoGrid = 0;
            double totalIvaGrid = 0;

            double.TryParse(textBox1.Text, out double costosExtra);

            // Sumamos los totales 
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                double cant = Convert.ToDouble(dataGridView1[2, i].Value);
                double costo = Convert.ToDouble(dataGridView1[3, i].Value);
                double ivaU = Convert.ToDouble(dataGridView1[6, i].Value);

                totalCostoGrid += (costo * cant);
                totalIvaGrid += (ivaU * cant);
            }

            // Prorrateo de costos extra
            double porcentaje = totalCostoGrid > 0 ? (costosExtra / totalCostoGrid) : 0;

            // Repartir el costo extra
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                double costo = Convert.ToDouble(dataGridView1[3, i].Value);
                double iv = Convert.ToDouble(dataGridView1[6, i].Value);

                double costoE = costo * porcentaje;
                double costoR = costo + costoE + iv;

                dataGridView1[4, i].Value = Math.Round(costoE, 3);
                dataGridView1[5, i].Value = Math.Round(costoR, 2);
            }

            // Actualizar Interfaz
            lblTotal.Text = (totalCostoGrid + costosExtra).ToString("0.00", CultureInfo.InvariantCulture);
            lblIVA.Text = totalIvaGrid.ToString("0.00", CultureInfo.InvariantCulture);
            lblTotalSi.Text = (totalCostoGrid + costosExtra + totalIvaGrid).ToString("0.00", CultureInfo.InvariantCulture);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            CalcularTotales();
        }

        // =========================================================
        // AGREGAR, BUSCAR Y ELIMINAR (UX)
        // =========================================================
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtID.Text) || string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Busque y seleccione un producto primero.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!double.TryParse(txtCantidad.Text, out double cantidad) || cantidad <= 0)
            {
                MessageBox.Show("Introduzca una cantidad válida mayor a cero.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCantidad.Focus();
                return;
            }

            double.TryParse(txtCosto.Text, out double costo);
            double.TryParse(txtVenta.Text, out double iva);
            double.TryParse(txtMenudeo.Text, out double menudeo);

            dataGridView1.Rows.Add(txtID.Text, txtNombre.Text, cantidad, costo, 0, 0, iva, menudeo);

            txtID.Clear();
            txtNombre.Clear();
            txtCantidad.Text = "0";
            txtCosto.Clear();
            txtVenta.Clear();
            txtMenudeo.Clear();
            txtID.Focus();

            CalcularTotales();
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && dataGridView1.CurrentRow != null)
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
                CalcularTotales();
            }
        }

        private void txtID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtID.Text)) return;

            using (SqlConnection conn = new SqlConnection(Conexion.CadConSql))
            {
                conn.Open();
                string query = "SELECT Nombre, PrecioVenta FROM Inventario WHERE Id = @Id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add("@Id", SqlDbType.VarChar, 50).Value = txtID.Text.Trim();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtNombre.Text = Convert.ToString(reader["Nombre"]);
                            txtMenudeo.Text = Convert.ToString(reader["PrecioVenta"]);
                            txtCantidad.Focus();
                        }
                        else
                        {
                            using (frmBusquedaArticulo buscar = new frmBusquedaArticulo())
                            {
                                if (buscar.ShowDialog() == DialogResult.OK)
                                {
                                    txtID.Text = buscar.Id;
                                    txtNombre.Text = buscar.Nombre;
                                    txtMenudeo.Text = buscar.Precio;
                                    txtCantidad.Focus();
                                }
                                else
                                {
                                    txtNombre.Focus();
                                }
                            }
                        }
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (frmBusquedaArticulo buscar = new frmBusquedaArticulo())
            {
                if (buscar.ShowDialog() == DialogResult.OK)
                {
                    txtID.Text = buscar.Id;
                    txtNombre.Text = buscar.Nombre;
                    txtMenudeo.Text = buscar.Precio;
                    txtCantidad.Focus();
                }
                else
                {
                    txtNombre.Focus();
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (frmBuscarProveedor buscar = new frmBuscarProveedor())
            {
                if (buscar.ShowDialog() == DialogResult.OK)
                {
                    nombreProv = buscar.Nombre;
                    idProv = buscar.ID;
                    lblProveedor.Text = nombreProv;
                }
            }
        }

        private void txtCosto_Leave(object sender, EventArgs e)
        {
            if (double.TryParse(txtCosto.Text, out double costo))
            {
                txtVenta.Text = Math.Round(costo * 0.16, 2).ToString();
            }
            else
            {
                txtCosto.Text = "0";
                txtVenta.Text = "0";
            }
        }

        private void ValidarNumeros(KeyPressEventArgs e)
        {
            CultureInfo cc = System.Threading.Thread.CurrentThread.CurrentCulture;
            if (char.IsNumber(e.KeyChar) || e.KeyChar.ToString() == cc.NumberFormat.NumberDecimalSeparator || Convert.ToInt32(e.KeyChar) == 8)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e) { ValidarNumeros(e); }
        private void txtCosto_KeyPress(object sender, KeyPressEventArgs e) { ValidarNumeros(e); }
        private void txtVenta_KeyPress(object sender, KeyPressEventArgs e) { ValidarNumeros(e); }
        private void txtMenudeo_KeyPress(object sender, KeyPressEventArgs e) { ValidarNumeros(e); }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e) { ValidarNumeros(e); }

        private void txtID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) SendKeys.Send("+{TAB}");
        }

        // =========================================================
        // GUARDADO EN BASE DE DATOS (TRANSACCIÓN NATIVA EN SQL SERVER)
        // =========================================================
        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFolio.Text))
            {
                MessageBox.Show("Ingrese un Folio para guardar la compra.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtFolio.Focus();
                return;
            }
            if (idProv == "0")
            {
                MessageBox.Show("Debe seleccionar un proveedor.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (dataGridView1.RowCount == 0)
            {
                MessageBox.Show("No hay productos en la póliza.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            CalcularTotales();

            string nombreAlmacenDestino = "INVENTARIO";

            using (SqlConnection conn = new SqlConnection(Conexion.CadConSql))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        DateTime fechaOperacion = DateTime.Now;

                        // 1. RECORRER PRODUCTOS DEL GRID
                        for (int i = 0; i < dataGridView1.RowCount; i++)
                        {
                            if (dataGridView1.Rows[i].IsNewRow) continue;

                            string idProdStr = dataGridView1[0, i].Value.ToString();
                            string nombreProd = dataGridView1[1, i].Value.ToString();
                            double cant = Convert.ToDouble(dataGridView1[2, i].Value);
                            double costo = Convert.ToDouble(dataGridView1[3, i].Value);
                            double costoExtra = Convert.ToDouble(dataGridView1[4, i].Value);
                            double costoReal = Convert.ToDouble(dataGridView1[5, i].Value);
                            double iva = Convert.ToDouble(dataGridView1[6, i].Value);
                            double precioVenta = Convert.ToDouble(dataGridView1[7, i].Value);

                            int.TryParse(idProdStr, out int idProdInt);

                            // Insertar detalle poliza
                            string queryProdPoliza = "INSERT INTO ProductosPoliza (Nombre, Cantidad, Costo, CostoExtra, CostoReal, IVA, FolioPoliza) " +
                                                     "VALUES (@Nom, @Cant, @Costo, @CExtra, @CReal, @Iva, @Folio)";

                            using (SqlCommand cmd = new SqlCommand(queryProdPoliza, conn, transaction))
                            {
                                cmd.Parameters.Add("@Nom", SqlDbType.VarChar, 100).Value = nombreProd;
                                cmd.Parameters.Add("@Cant", SqlDbType.Decimal).Value = cant;
                                cmd.Parameters.Add("@Costo", SqlDbType.Decimal).Value = costo;
                                cmd.Parameters.Add("@CExtra", SqlDbType.Decimal).Value = costoExtra;
                                cmd.Parameters.Add("@CReal", SqlDbType.Decimal).Value = costoReal;
                                cmd.Parameters.Add("@Iva", SqlDbType.Decimal).Value = iva;
                                cmd.Parameters.Add("@Folio", SqlDbType.VarChar, 50).Value = txtFolio.Text;
                                cmd.ExecuteNonQuery();
                            }

                            // Comprobar existencia en la tabla PRODUCTOS
                            bool productoExiste = false;
                            double invActual = 0;
                            double precioDB = 0;

                            string queryCheck = "SELECT Cantidad, Especial FROM PRODUCTOS WHERE IdProducto = @Id";
                            using (SqlCommand cmdCheck = new SqlCommand(queryCheck, conn, transaction))
                            {
                                cmdCheck.Parameters.Add("@Id", SqlDbType.Int).Value = idProdInt;
                                using (SqlDataReader reader = cmdCheck.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        productoExiste = true;
                                        invActual = reader["Cantidad"] != DBNull.Value ? Convert.ToDouble(reader["Cantidad"]) : 0;
                                        precioDB = reader["Especial"] != DBNull.Value ? Convert.ToDouble(reader["Especial"]) : 0;
                                    }
                                }
                            }

                            double nuevas = invActual + cant;

                            if (productoExiste)
                            {
                                double nuevoEspecial = costoReal > precioDB ? costoReal : precioDB;

                                // Se quitó FechaUltimaCompra ya que no existe en la tabla PRODUCTOS
                                string updInv = "UPDATE PRODUCTOS SET Especial = @Esp, Cantidad = @Exis, Precio = @PVenta WHERE IdProducto = @Id";
                                using (SqlCommand cmdUpd = new SqlCommand(updInv, conn, transaction))
                                {
                                    cmdUpd.Parameters.Add("@Esp", SqlDbType.Decimal).Value = nuevoEspecial;
                                    cmdUpd.Parameters.Add("@Exis", SqlDbType.Decimal).Value = nuevas;
                                    cmdUpd.Parameters.Add("@PVenta", SqlDbType.Decimal).Value = precioVenta;
                                    cmdUpd.Parameters.Add("@Id", SqlDbType.Int).Value = idProdInt;
                                    cmdUpd.ExecuteNonQuery();
                                }

                                string insKardex1 = "INSERT INTO Kardex (IdProducto, Tipo, Descripcion, ExistenciaAntes, ExistenciaDespues, Fecha, Precio) " +
                                                    "VALUES (@Id, 'ENTRADA', @Desc, @EA, @ED, GETDATE(), @Pre)";
                                using (SqlCommand cmdKardex = new SqlCommand(insKardex1, conn, transaction))
                                {
                                    cmdKardex.Parameters.Add("@Id", SqlDbType.VarChar, 50).Value = idProdStr;
                                    cmdKardex.Parameters.Add("@Desc", SqlDbType.VarChar, 255).Value = "COMPRA DE ARTICULO FOLIO: " + txtFolio.Text;
                                    cmdKardex.Parameters.Add("@EA", SqlDbType.Decimal).Value = invActual;
                                    cmdKardex.Parameters.Add("@ED", SqlDbType.Decimal).Value = nuevas;
                                    cmdKardex.Parameters.Add("@Pre", SqlDbType.Decimal).Value = precioVenta;
                                    //cmdKardex.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                // Se omitió IdProducto (IDENTITY) y FechaUltimaCompra para ajustarse al schema de PRODUCTOS
                                string insInv = "INSERT INTO PRODUCTOS (Nombre, Cantidad, Especial, IdOrigen, Precio, Limite) " +
                                                "VALUES (@Nom, @Cant, @Esp, NULL, @PVenta, 1)";
                                using (SqlCommand cmdIns = new SqlCommand(insInv, conn, transaction))
                                {
                                    cmdIns.Parameters.Add("@Nom", SqlDbType.VarChar, 100).Value = nombreProd;
                                    cmdIns.Parameters.Add("@Cant", SqlDbType.Decimal).Value = cant;
                                    cmdIns.Parameters.Add("@Esp", SqlDbType.Decimal).Value = costoReal;
                                    cmdIns.Parameters.Add("@PVenta", SqlDbType.Decimal).Value = precioVenta;
                                    cmdIns.ExecuteNonQuery();
                                }

                                string insKardex2 = "INSERT INTO Kardex (IdProducto, Tipo, Descripcion, ExistenciaAntes, ExistenciaDespues, Fecha, idProveedor, Proveedor) " +
                                                    "VALUES (@Id, 'ENTRADA', @Desc, 0, @ED, GETDATE(), @idProv, @NomProv)";
                                using (SqlCommand cmdKardex = new SqlCommand(insKardex2, conn, transaction))
                                {
                                    cmdKardex.Parameters.Add("@Id", SqlDbType.VarChar, 50).Value = idProdStr;
                                    cmdKardex.Parameters.Add("@Desc", SqlDbType.VarChar, 255).Value = "COMPRA DE ARTICULO FOLIO: " + txtFolio.Text;
                                    cmdKardex.Parameters.Add("@ED", SqlDbType.Decimal).Value = cant;
                                    cmdKardex.Parameters.Add("@idProv", SqlDbType.VarChar, 50).Value = idProv;
                                    cmdKardex.Parameters.Add("@NomProv", SqlDbType.VarChar, 150).Value = nombreProv;
                                    //cmdKardex.ExecuteNonQuery();
                                }
                            }
                        }

                        // 2. ACTUALIZAR PROVEEDOR (Si es a crédito)
                        if (rdCredito.Checked)
                        {
                            double adeudoActual = 0;
                            string qProv = "SELECT Adeudo FROM Proveedores WHERE Id = @IdP";
                            using (SqlCommand cmdProv = new SqlCommand(qProv, conn, transaction))
                            {
                                cmdProv.Parameters.Add("@IdP", SqlDbType.Int).Value = Convert.ToInt32(idProv);
                                object result = cmdProv.ExecuteScalar();
                                if (result != null && result != DBNull.Value)
                                    adeudoActual = Convert.ToDouble(result);
                            }

                            double.TryParse(lblTotal.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double totalLbl);
                            double.TryParse(textBox1.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double extraTxt);

                            double totalCompra = totalLbl + extraTxt;
                            adeudoActual += totalCompra;

                            string uProv = "UPDATE Proveedores SET Adeudo = @Adeudo WHERE Id = @IdP";
                            using (SqlCommand cmdUProv = new SqlCommand(uProv, conn, transaction))
                            {
                                cmdUProv.Parameters.Add("@Adeudo", SqlDbType.Decimal).Value = adeudoActual;
                                cmdUProv.Parameters.Add("@IdP", SqlDbType.Int).Value = Convert.ToInt32(idProv);
                                cmdUProv.ExecuteNonQuery();
                            }
                        }

                        // 3. INSERTAR PÓLIZA GENERAL
                        string insPoliza = "INSERT INTO Poliza (Folio, Fecha, FechaCaptura, CostoTotal, CostoExtra, IdProv, Proveedor, IVA, Total, IdAlmacen) " +
                                           "VALUES (@Folio, @Fech1, GETDATE(), @CostoT, @CostoE, @IdP, @Prov, @Iva, @Tot, @IdAlmacen)";
                        using (SqlCommand cmdPol = new SqlCommand(insPoliza, conn, transaction))
                        {
                            cmdPol.Parameters.Add("@Folio", SqlDbType.VarChar, 50).Value = txtFolio.Text;
                            cmdPol.Parameters.Add("@Fech1", SqlDbType.DateTime).Value = dateTimePicker1.Value.Date;
                            cmdPol.Parameters.Add("@CostoT", SqlDbType.Decimal).Value = Convert.ToDecimal(lblTotal.Text, CultureInfo.InvariantCulture);
                            cmdPol.Parameters.Add("@CostoE", SqlDbType.Decimal).Value = Convert.ToDecimal(textBox1.Text, CultureInfo.InvariantCulture);
                            cmdPol.Parameters.Add("@IdP", SqlDbType.VarChar, 50).Value = idProv;
                            cmdPol.Parameters.Add("@Prov", SqlDbType.VarChar, 150).Value = nombreProv;
                            cmdPol.Parameters.Add("@Iva", SqlDbType.Decimal).Value = Convert.ToDecimal(lblIVA.Text, CultureInfo.InvariantCulture);
                            cmdPol.Parameters.Add("@Tot", SqlDbType.Decimal).Value = Convert.ToDecimal(lblTotalSi.Text, CultureInfo.InvariantCulture);
                            cmdPol.Parameters.Add("@IdAlmacen", SqlDbType.Int).Value = 0;
                            cmdPol.ExecuteNonQuery();
                        }

                        transaction.Commit();

                        MessageBox.Show("Póliza guardada y procesada correctamente", "COMPRA REGISTRADA", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        frmCompras inv = new frmCompras();
                        inv.Show();
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Ocurrió un error al procesar la compra.\nLa base de datos no fue afectada.\n\nError: " + ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void txtCosto_TextChanged(object sender, EventArgs e) { }
    }
}