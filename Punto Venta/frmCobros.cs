using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Globalization;
using System.Data.SqlClient;

namespace Punto_Venta
{
    public partial class frmCobros : Form
    {
        double iva;
        double descuento;
        double total = 0;
        string tipoUser = "";
        public int idMesero = 0;
        public string print = "";
        public int idCliente = 0;
        private int folio;

        public frmCobros()
        {
            InitializeComponent();
            this.MinimumSize = new Size(810, 400);
        }
        private double RecalcularTotal
        {
            get
            {
                total = 0;
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    total += Convert.ToDouble(dataGridView1.Rows[i].Cells["Total"].Value);
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
                    A.IdInventario, A.IdArticulosMesa, A.Cantidad,B.Nombre, B.Precio, A.Total,A.FechaHora, A.Comentario, '0' AS Ids
                    FROM ArticulosMesa A
                    INNER JOIN INVENTARIO B ON A.IdInventario = B.IdInventario
                    WHERE A.IdUsuarioCancelo IS NULL 
                    AND A.Estatus = 'COCINA'
                    AND A.IdMesa = @Mesa";
                using (SqlDataAdapter da = new SqlDataAdapter(query, conectar))
                {
                    da.SelectCommand.Parameters.AddWithValue("@Mesa", $"{lblID.Text}");
                    da.Fill(ds, "Articulos"); // Cambia "Id" por "Origen" para mayor claridad
                }

                dataGridView1.DataSource = ds.Tables["Articulos"];
                dataGridView1.Columns["Precio"].DefaultCellStyle.Format = "N2";
                dataGridView1.Columns["Total"].DefaultCellStyle.Format = "N2";
                dataGridView1.Columns[0].Visible = false;
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

            int width = 420;
            int height = 540;
            printDocument1.PrinterSettings.DefaultPageSettings.PaperSize = new PaperSize("", width, height);
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(this.printDocument1_PrintPage_1);
            PrintDialog printdlg = new PrintDialog();
            PrintPreviewDialog printPrvDlg = new PrintPreviewDialog();
            // preview the assigned document or you can create a different previewButton for it
            printPrvDlg.Document = pd;
            printdlg.Document = pd;
            pd.Print();
            button1.Hide();
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

            // only allow one decimal point
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
            double ventas = 0;
            int mesas = 0;
            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();

                string insertFolioQuery = "INSERT INTO Folios (ModalidadVenta, Estatus, idCliente, FechaHora, Total, Descuento, Utilidad, IdMesa) " +
                                          "VALUES (@ModalidadVenta, @Estatus, @idCliente, @FechaHora, @Total, @Descuento,@Utilidad, @IdMesa); " +
                                          "SELECT SCOPE_IDENTITY();"; // Obtener el último ID insertado

                using (SqlCommand cmd = new SqlCommand(insertFolioQuery, conectar))
                {
                    cmd.Parameters.AddWithValue("@ModalidadVenta", "MESA");
                    cmd.Parameters.AddWithValue("@Estatus", "COBRADO");
                    cmd.Parameters.AddWithValue("@idCliente", idCliente == 0 ? (object)DBNull.Value : idCliente);
                    cmd.Parameters.AddWithValue("@FechaHora", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Total", total);
                    cmd.Parameters.AddWithValue("@Descuento", descuento);
                    cmd.Parameters.AddWithValue("@Utilidad", 20.00);
                    cmd.Parameters.AddWithValue("@IdMesa", lblID.Text);

                    // Ejecutar la inserción y obtener el último ID insertado
                    int lastIdFolio = Convert.ToInt32(cmd.ExecuteScalar());

                    string insertArticuloQuery = "INSERT INTO ArticulosFolio (IdInventario, IdFolio, Cantidad, Comentario, Total, IdExtra) " +
                                                  "VALUES (@IdProducto, @IdFolio, @Cantidad, @Comentario, @Total, @IdExtra);";

                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                        using (SqlCommand cmd2 = new SqlCommand(insertArticuloQuery, conectar))
                        {
                            cmd2.Parameters.AddWithValue("@IdProducto", dataGridView1.Rows[i].Cells["IdInventario"].Value);
                            cmd2.Parameters.AddWithValue("@IdFolio", lastIdFolio);
                            cmd2.Parameters.AddWithValue("@Cantidad", Convert.ToDecimal(dataGridView1.Rows[i].Cells["Cantidad"].Value));
                            cmd2.Parameters.AddWithValue("@Comentario", dataGridView1.Rows[i].Cells["Comentario"].Value?.ToString());
                            cmd2.Parameters.AddWithValue("@Total", Convert.ToDecimal(dataGridView1.Rows[i].Cells["Total"].Value));
                            cmd2.Parameters.AddWithValue("@IdExtra", dataGridView1.Rows[i].Cells["Ids"].Value?.ToString());

                            cmd2.ExecuteNonQuery();
                        }
                    }
                    string query = @"INSERT INTO CORTE (Concepto, Total,FechaHora,FormaPago) VALUES
                                    (@Concepto, @Total, GETDATE(), 'EFECTIVO')";
                    using (SqlCommand cmd2 = new SqlCommand(query, conectar))
                    {
                        cmd2.Parameters.AddWithValue("@Concepto", $"VENTA DE FOLIO: {lastIdFolio}");
                        cmd2.Parameters.AddWithValue("@Total", total);
                        cmd2.ExecuteNonQuery();
                    }
                    folio = lastIdFolio;

                }
                using (SqlCommand cmd = new SqlCommand("SELECT Ventas, Mesas FROM Usuarios WHERE IdUsuario = @IdMesero;", conectar))
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

                    // Actualizar las ventas y mesas
                    ventas += total;
                    mesas++;

                    using (SqlCommand cmd2 = new SqlCommand("UPDATE Usuarios SET Ventas = @Ventas, Mesas = @Mesas WHERE IdUsuario = @IdMesero;", conectar))
                    {
                        cmd2.Parameters.AddWithValue("@Ventas", ventas);
                        cmd2.Parameters.AddWithValue("@Mesas", mesas);
                        cmd2.Parameters.AddWithValue("@IdMesero", idMesero);

                        cmd2.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd2 = new SqlCommand("UPDATE MESAS SET Estatus = 'COBRADO' WHERE IdMesa = @IdMesa;", conectar))
                    {
                        cmd2.Parameters.AddWithValue("@IdMesa", lblID.Text);

                        cmd2.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd2 = new SqlCommand("UPDATE ArticulosMesa SET Estatus = 'COBRADO' WHERE IdMesa = @IdMesa;", conectar))
                    {
                        cmd2.Parameters.AddWithValue("@IdMesa", lblID.Text);

                        cmd2.ExecuteNonQuery();
                    }
                  
                }


            }
            if (print == "1")
                imprimir();
            DialogResult dialogResult = MessageBox.Show("¿Imprimir otro ticket?", "Alto!", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                imprimir();
            }
            MessageBox.Show("EL COBRO SE HA REALIZADO CON EXITO!", "EXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            frmMesasOcupadas mesa = new frmMesasOcupadas();
            mesa.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null) // Verifica si hay una fila seleccionada
            {
                if (print == "0")
                {
                    using (frmComentarios com = new frmComentarios())
                    {
                        if (com.ShowDialog() == DialogResult.OK)
                        {
                            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
                            {
                                conectar.Open();

                                using (SqlCommand cmd = new SqlCommand("UPDATE ArticulosMesa SET Comentario= @Comentario, Estatus = 'CANCELADO', IdUsuarioCancelo = @IdUsuarioCancelo WHERE IdArticulosMesa = @IdArticulosMesa;", conectar))
                                {
                                    cmd.Parameters.AddWithValue("@Comentario", com.Comentario);
                                    cmd.Parameters.AddWithValue("@Estatus", "CANCELADO");
                                    cmd.Parameters.AddWithValue("@IdUsuarioCancelo", idMesero);
                                    cmd.Parameters.AddWithValue("@IdArticulosMesa", dataGridView1[0, dataGridView1.CurrentRow.Index].Value);

                                    cmd.ExecuteNonQuery();
                                }
                                if (dataGridView1.RowCount == 1)
                                {
                                    using (SqlCommand cmd = new SqlCommand("UPDATE Mesas SET Estatus = 'CANCELADO' WHERE IdMesa = @IdMesa;", conectar))
                                    {
                                        cmd.Parameters.AddWithValue("@IdMesa", lblID.Text);

                                        cmd.ExecuteNonQuery();
                                        MessageBox.Show("EL PRODUCTO SE HA ELIMINADO CON EXITO!", "EXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        this.Close();
                                    }

                                }
                                else
                                {
                                    dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
                                    ResetearDescuento();
                                    MessageBox.Show("EL PRODUCTO SE HA ELIMINADO CON EXITO!", "EXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                        else
                            MessageBox.Show("SE REQUIERE UN COMENTARIO PARA CANCELAR", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    using (frmClaveVendendor ori = new frmClaveVendendor())
                    {
                        if (ori.ShowDialog() == DialogResult.OK)
                        {
                            tipoUser = ori.Tipo;
                            if (tipoUser == "Administrador" || tipoUser == "SUPERVISOR")
                            {

                                using (frmComentarios com = new frmComentarios())
                                {
                                    if (com.ShowDialog() == DialogResult.OK)
                                    {
                                        using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
                                        {
                                            conectar.Open();

                                            using (SqlCommand cmd = new SqlCommand("UPDATE ArticulosMesa SET Comentario= @Comentario, Estatus = 'CANCELADO', IdUsuarioCancelo = @IdUsuarioCancelo WHERE IdArticulosMesa = @IdArticulosMesa;", conectar))
                                            {
                                                cmd.Parameters.AddWithValue("@Comentario", com.Comentario);
                                                cmd.Parameters.AddWithValue("@Estatus", "CANCELADO");
                                                cmd.Parameters.AddWithValue("@IdUsuarioCancelo", idMesero);
                                                cmd.Parameters.AddWithValue("@IdArticulosMesa", dataGridView1[0, dataGridView1.CurrentRow.Index].Value);

                                                cmd.ExecuteNonQuery();
                                            }
                                            if (dataGridView1.RowCount == 1)
                                            {
                                                using (SqlCommand cmd = new SqlCommand("UPDATE Mesas SET Estatus = 'CANCELADO' WHERE IdMesa = @IdMesa;", conectar))
                                                {
                                                    cmd.Parameters.AddWithValue("@IdMesa", lblID.Text);

                                                    cmd.ExecuteNonQuery();
                                                    MessageBox.Show("EL PRODUCTO SE HA ELIMINADO CON EXITO!", "EXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                    this.Close();
                                                }

                                            }
                                            else
                                            {
                                                dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
                                                ResetearDescuento();
                                                MessageBox.Show("EL PRODUCTO SE HA ELIMINADO CON EXITO!", "EXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            }
                                        }
                                    }
                                    else
                                        MessageBox.Show("SE REQUIERE UN COMENTARIO PARA CANCELAR", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                                MessageBox.Show("SE REQUIERE CLAVE DE ADMINISTRADOR PARA CANCELAR", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void printDocument1_PrintPage_1(object sender, PrintPageEventArgs e)
        {
            int posicion = 10;
            //RESIZE
            Image logo = Image.FromFile("C:\\Jaeger Soft\\logo.jpg");
            e.Graphics.DrawImage(logo, new PointF(1, 10));
            //LOGO
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
            //Titulo Columna
            e.Graphics.DrawString("Cant   Producto        P.Unit  Importe", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(1, posicion));
            posicion += 20;
            e.Graphics.DrawLine(new Pen(Color.Black), 1, posicion, 420, posicion);
            posicion += 10;
            //Lista de Productos
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
                                /*
                                cmd = new OleDbCommand("SELECT Id,Nombre FROM Inventario where Id=" + ids2[1] + ";", conectar);
                                OleDbDataReader reader = cmd.ExecuteReader();
                                while (reader.Read())
                                {
                                    precio = 0;
                                    producto = reader[1].ToString();
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
                                    RESULT += ids2[0] + " : " + reader[1].ToString() + "\n";
                                }
                                */
                            }
                        }
                    }
                }
                //ticket.AddItem(item, producto, uni + "|" + String.Format(CultureInfo.InvariantCulture, "{0:0,0.00}", precio));
            }
            double to = Convert.ToDouble(total);
            string toty = String.Format(CultureInfo.InvariantCulture, "{0:0,0.00}", to);
            e.Graphics.DrawLine(new Pen(Color.Black), 210, posicion + 10, 420, posicion + 10);
            posicion += 15;
            e.Graphics.DrawString("TOTAL: $" + toty, new Font("Arial", 10, FontStyle.Bold), Brushes.Black, new Point(280, posicion), sf);
            posicion += 50;

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
            lblTotal.Text = $"{Math.Round(RecalcularTotal - descuento, 0):C}";
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
