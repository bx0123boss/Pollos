using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;

namespace Punto_Venta
{
    public partial class frmDetallesPoliza : Form
    {
        public string Id { get; set; }
        public int que { get; set; }
        public string usuario { get; set; } = "";

        public frmDetallesPoliza()
        {
            InitializeComponent();
        }

        private void frmDetallesPoliza_Load(object sender, EventArgs e)
        {
            CargarDetallePoliza();

            if (usuario == "Invitado")
            {
                button1.Hide();
            }

            if (lblExtra.Text == ".00" || string.IsNullOrWhiteSpace(lblExtra.Text))
            {
                lblExtra.Text = "0.00";
            }

            if (!string.IsNullOrWhiteSpace(lblFechaPoli.Text))
            {
                string[] fecha = lblFechaPoli.Text.Split(' ');
                lblFechaPoli.Text = fecha[0];
            }
        }

        private void CargarDetallePoliza()
        {
            try
            {
                using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
                {
                    conectar.Open();
                    string query;

                    if (que == 2)
                    {
                        query = "SELECT * FROM productosPoliza2 WHERE IdPoliza = @IdPoliza;";
                    }
                    else
                    {
                        query = "SELECT * FROM productosPoliza WHERE FolioPoliza = @FolioPoliza;";
                    }

                    using (SqlCommand cmd = new SqlCommand(query, conectar))
                    {
                        if (que == 2)
                            cmd.Parameters.Add("@IdPoliza", SqlDbType.VarChar, 50).Value = Id;
                        else
                            cmd.Parameters.Add("@FolioPoliza", SqlDbType.VarChar, 50).Value = lblFolio.Text.Trim();

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            dataGridView1.DataSource = dt;

                            if (dataGridView1.Columns.Count > 7)
                            {
                                dataGridView1.Columns[7].Visible = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los detalles de la póliza: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("¿Estás seguro de cancelar la póliza?", "¡Alto!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dialogResult != DialogResult.Yes) return;

            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();
                using (SqlTransaction transaction = conectar.BeginTransaction())
                {
                    try
                    {
                        // 1. RECORRER PRODUCTOS Y REVERTIR INVENTARIO
                        for (int i = 0; i < dataGridView1.RowCount; i++)
                        {
                            if (dataGridView1.Rows[i].IsNewRow) continue;

                            string idProducto = Convert.ToString(dataGridView1[0, i].Value);
                            string nombreProducto = Convert.ToString(dataGridView1[1, i].Value);
                            double cantidad = Convert.ToDouble(dataGridView1[2, i].Value);
                            double costo = Convert.ToDouble(dataGridView1[3, i].Value);
                            double costoExtra = Convert.ToDouble(dataGridView1[4, i].Value);
                            double costoReal = Convert.ToDouble(dataGridView1[5, i].Value);
                            double iva = Convert.ToDouble(dataGridView1[6, i].Value);

                            // Insertar en productosPoliza2 (Histórico de canceladas)
                            string qInsProdHist = "INSERT INTO productosPoliza2 (Id, Nombre, Cantidad, Costo, CostoExtra, CostoReal, IVA, IdPoliza) " +
                                                  "VALUES (@Id, @Nom, @Cant, @Costo, @CExtra, @CReal, @Iva, @IdPoliza);";

                            using (SqlCommand cmdInsProd = new SqlCommand(qInsProdHist, conectar, transaction))
                            {
                                cmdInsProd.Parameters.Add("@Id", SqlDbType.VarChar, 50).Value = idProducto;
                                cmdInsProd.Parameters.Add("@Nom", SqlDbType.VarChar, 150).Value = nombreProducto;
                                cmdInsProd.Parameters.Add("@Cant", SqlDbType.Decimal).Value = cantidad;
                                cmdInsProd.Parameters.Add("@Costo", SqlDbType.Decimal).Value = costo;
                                cmdInsProd.Parameters.Add("@CExtra", SqlDbType.Decimal).Value = costoExtra;
                                cmdInsProd.Parameters.Add("@CReal", SqlDbType.Decimal).Value = costoReal;
                                cmdInsProd.Parameters.Add("@Iva", SqlDbType.Decimal).Value = iva;
                                cmdInsProd.Parameters.Add("@IdPoliza", SqlDbType.VarChar, 50).Value = string.IsNullOrWhiteSpace(Id) ? (object)DBNull.Value : Id;
                                cmdInsProd.ExecuteNonQuery();
                            }

                            // Consultar existencia actual del producto
                            double existenciaActual = 0;
                            bool productoExiste = false;

                            string qCheckInv = "SELECT Existencia FROM Inventario WHERE Id = @Id;";
                            using (SqlCommand cmdCheck = new SqlCommand(qCheckInv, conectar, transaction))
                            {
                                cmdCheck.Parameters.Add("@Id", SqlDbType.VarChar, 50).Value = idProducto;
                                object res = cmdCheck.ExecuteScalar();
                                if (res != null && res != DBNull.Value)
                                {
                                    existenciaActual = Convert.ToDouble(res);
                                    productoExiste = true;
                                }
                            }

                            if (productoExiste)
                            {
                                double existenciaDespues = existenciaActual - cantidad;

                                // Descontar del inventario principal
                                string qUpdInv = "UPDATE Inventario SET Existencia = @Exis WHERE Id = @Id;";
                                using (SqlCommand cmdUpd = new SqlCommand(qUpdInv, conectar, transaction))
                                {
                                    cmdUpd.Parameters.Add("@Exis", SqlDbType.Decimal).Value = existenciaDespues;
                                    cmdUpd.Parameters.Add("@Id", SqlDbType.VarChar, 50).Value = idProducto;
                                    cmdUpd.ExecuteNonQuery();
                                }

                                // Registrar movimiento en Kardex (SALIDA por cancelación)
                                string qKardex = "INSERT INTO Kardex (IdProducto, Tipo, Descripcion, ExistenciaAntes, ExistenciaDespues, Fecha) " +
                                                 "VALUES (@IdProd, 'SALIDA', @Desc, @EA, @ED, GETDATE());";
                                using (SqlCommand cmdKardex = new SqlCommand(qKardex, conectar, transaction))
                                {
                                    cmdKardex.Parameters.Add("@IdProd", SqlDbType.VarChar, 50).Value = idProducto;
                                    cmdKardex.Parameters.Add("@Desc", SqlDbType.VarChar, 255).Value = "CANCELACION DE COMPRA FOLIO: " + lblFolio.Text.Trim();
                                    cmdKardex.Parameters.Add("@EA", SqlDbType.Decimal).Value = existenciaActual;
                                    cmdKardex.Parameters.Add("@ED", SqlDbType.Decimal).Value = existenciaDespues;
                                    cmdKardex.ExecuteNonQuery();
                                }
                            }
                        }

                        // 2. REGISTRAR PÓLIZA CANCELADA EN POLIZA2
                        double.TryParse(lblMonto.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double montoVal);
                        double.TryParse(lblExtra.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double extraVal);

                        string qInsPoliza2 = "INSERT INTO Poliza2 (Folio, Fecha, FechaCancelacion, CostoTotal, CostoExtra, IdPoliza) " +
                                             "VALUES (@Folio, @Fecha, GETDATE(), @CostoT, @CostoE, @IdPoliza);";
                        using (SqlCommand cmdPol2 = new SqlCommand(qInsPoliza2, conectar, transaction))
                        {
                            cmdPol2.Parameters.Add("@Folio", SqlDbType.VarChar, 50).Value = lblFolio.Text.Trim();
                            cmdPol2.Parameters.Add("@Fecha", SqlDbType.VarChar, 50).Value = lblFechaPoli.Text.Trim();
                            cmdPol2.Parameters.Add("@CostoT", SqlDbType.Decimal).Value = montoVal;
                            cmdPol2.Parameters.Add("@CostoE", SqlDbType.Decimal).Value = extraVal;
                            cmdPol2.Parameters.Add("@IdPoliza", SqlDbType.VarChar, 50).Value = string.IsNullOrWhiteSpace(Id) ? (object)DBNull.Value : Id;
                            cmdPol2.ExecuteNonQuery();
                        }

                        // 3. ELIMINAR DE TABLAS ACTIVAS
                        string qDelPoliza = "DELETE FROM Poliza WHERE Folio = @Folio;";
                        using (SqlCommand cmdDel1 = new SqlCommand(qDelPoliza, conectar, transaction))
                        {
                            cmdDel1.Parameters.Add("@Folio", SqlDbType.VarChar, 50).Value = lblFolio.Text.Trim();
                            cmdDel1.ExecuteNonQuery();
                        }

                        string qDelProds = "DELETE FROM productosPoliza WHERE FolioPoliza = @Folio;";
                        using (SqlCommand cmdDel2 = new SqlCommand(qDelProds, conectar, transaction))
                        {
                            cmdDel2.Parameters.Add("@Folio", SqlDbType.VarChar, 50).Value = lblFolio.Text.Trim();
                            cmdDel2.ExecuteNonQuery();
                        }

                        // CONFIRMAR TRANSACCIÓN
                        transaction.Commit();

                        MessageBox.Show("¡Póliza eliminada correctamente!", "Póliza", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Error al cancelar la póliza. Ningún cambio fue aplicado.\n\nDetalle: " + ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}