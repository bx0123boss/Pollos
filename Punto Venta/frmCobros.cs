using System;
using LibPrintTicket;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.IO;
using System.Data.OleDb;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Punto_Venta
{
    public partial class frmCobros : Form
    {
        private DataSet ds;
        OleDbConnection conectar = new OleDbConnection(Conexion.CadCon); 
        OleDbDataAdapter da;
        OleDbCommand cmd;
        double iva;
        double descuento;
        double total = 0;
        int suma = 0;
        string folio = "";
        string tipoUser = "";
        public int idMesero = 0;
        public string print = "";
        public frmCobros()
        {
            InitializeComponent();
        }
        public void obtenerYSumar()
        {
            cmd = new OleDbCommand("SELECT TOP 1 Folio AS MaxFolio FROM folios ORDER BY fecha DESC, Folio DESC;", conectar);
            Console.WriteLine(cmd.ToString());
            OleDbDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                int numero = Convert.ToInt32(reader[0].ToString().Substring(1));
                suma = numero;
                suma++;
            }
            lblFolio.Text = "V" + suma;
        }
        public void obtenerYSumar2()
        {
            suma = suma + 1;
            cmd = new OleDbCommand("UPDATE Folio set Numero=" + suma + " where Folio='Venta';", conectar);
            cmd.ExecuteNonQuery();
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
                return total;
            }
        }

        private void frmCobros_Load(object sender, EventArgs e)
        {
            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();
                DataSet ds = new DataSet();
                string query = @"
                    SELECT 
                    A.IdArticulosMesa, A.Cantidad, B.Precio, A.Total, A.Comentario, A.FechaHora
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

                // Ocultar la primera columna (si es necesario)
                if (dataGridView1.Columns.Count > 0)
                {
                    dataGridView1.Columns[0].Visible = false;
                }
                lblTotal.Text = $"{RecalcularTotal:C}";
            }

        }

        public void imprimir()
        {
            print = "0";
            cmd = new OleDbCommand("UPDATE Mesas SET Print='0' where Id=" + lblID.Text + ";", conectar);
            cmd.ExecuteNonQuery();
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
                txtCambio.Text = "" + (Convert.ToDouble(txtPago.Text) - total);
                txtPago.Text = "" + txtPago.Text;
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
                        lol = Math.Truncate((lol * (iva/100+1)) * 100) / 100;
                        lblTotal.Text = $"{lol:C}";
                    }
                    
                }
                
            }
            else
            {
                lblTotal.Text = $"{total:C}";
            }


        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            total = 0;
            float cantidad = Convert.ToSingle(dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString());
            float precio = Convert.ToSingle(dataGridView1[3, dataGridView1.CurrentRow.Index].Value.ToString());
            float monto = cantidad * precio;
            dataGridView1.Rows[e.RowIndex].Cells[4].Value = monto;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                total += Convert.ToSingle(dataGridView1[4, i].Value.ToString(), CultureInfo.CreateSpecificCulture("es-ES"));
            }

            if (checkBox1.Checked)
            {
                double lol = total;
                lol = Math.Truncate((lol * 1.03) * 100) / 100;
                lblTotal.Text = $"{lol:C}";
            }
            else
            {
                lblTotal.Text = $"{total:C}";
            }
        }
        public string obtenerSubcategoria(string id)
        {
            string subcategoria = "";
            try
            {
                cmd = new OleDbCommand("select subcategoria from Inventario where Id=" + id + ";", conectar);
                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    subcategoria = reader[0].ToString();
                }
            }
            catch
            {

            }
            return subcategoria;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            button2.Hide();
            obtenerYSumar();
            folio = "V" + String.Format("{0:0000}", suma);
            string formaPago = "Efectivo";
            if (checkBox1.Checked)
            {
                formaPago = "Tarjeta";
            }
            double utilidad = 0;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {

                utilidad += Convert.ToDouble(dataGridView1[10, i].Value.ToString());
                double lol = Convert.ToDouble(dataGridView1[4, i].Value.ToString());
                string producto;
                if (dataGridView1[2, i].Value.ToString().Length > 12)
                {
                    producto = dataGridView1[2, i].Value.ToString().Substring(0, 12);
                }
                else
                {
                    producto = dataGridView1[2, i].Value.ToString();
                }
                string ide = "";
                if (dataGridView1[9, i].Value.ToString().Length>0)
                {
                    ide = dataGridView1[9, i].Value.ToString();
                }
                cmd = new OleDbCommand("insert into temp(id,cantidad, producto, precio, total,ide) values ('" + dataGridView1[0, i].Value.ToString() + "','" + dataGridView1[1, i].Value.ToString() + "','" + dataGridView1[2, i].Value.ToString() + "'," + dataGridView1[3, i].Value.ToString() + ",'" + dataGridView1[4, i].Value.ToString() + "','"+ide+"');", conectar);
                cmd.ExecuteNonQuery();
               //SUBCATEGORIALV
                string subcategoria =obtenerSubcategoria(dataGridView1[0, i].Value.ToString());
                cmd = new OleDbCommand("insert into ventas(idProducto,cantidad, producto, precio, total,folio,Fecha,Estatus,ide,subcategoria,Utilidad) values ('" + dataGridView1[0, i].Value.ToString() + "','" + dataGridView1[1, i].Value.ToString() + "','" + dataGridView1[2, i].Value.ToString() + "'," + dataGridView1[3, i].Value.ToString() + ",'" + dataGridView1[4, i].Value.ToString() + "','" + folio + "','" + (DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString()) + "','COBRADO','"+ ide + "','"+subcategoria+"','"+ dataGridView1[10, i].Value.ToString()+"');", conectar);
                cmd.ExecuteNonQuery();
            }
            cmd = new OleDbCommand("INSERT INTO folios(Folio,ModalidadVenta,Estatus,idCliente,Vehiculo,Repartidor,Fecha,Monto,FormaPago, Utilidad) VALUES ('" + folio + "','Mesa','COBRADO','0','" + idMesero + "','" + lblMesero.Text + "','" + (DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString()) + "','" + total + "','" + formaPago + "','"+utilidad+"');", conectar);
            cmd.ExecuteNonQuery();

            cmd = new OleDbCommand("INSERT INTO corte (concepto, total,fecha,FormaPago) VALUES ('VENTA FOLIO:" + folio + " MESA " + lblMesa.Text + "'," + total + ",'" + (DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString()) + "','"+formaPago+"');", conectar);
                cmd.ExecuteNonQuery(); 
                double ventas = 0;
                int mesas = 0;
            cmd = new OleDbCommand("select * from Usuarios where Id=" + idMesero + ";", conectar);
            OleDbDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                ventas = Convert.ToDouble(reader[4].ToString());
                mesas = Convert.ToInt32(reader[5].ToString());
            }
            ventas += total;
            mesas++;
            cmd = new OleDbCommand("UPDATE Usuarios SET Ventas='" + ventas + "', Mesas='" + mesas + "' where Id=" + idMesero + ";", conectar);
            cmd.ExecuteNonQuery();
            cmd = new OleDbCommand("UPDATE Mesas SET idMesero='0', Mesero='',Print='1' where Id=" + lblID.Text+ ";", conectar);
            cmd.ExecuteNonQuery();
            cmd = new OleDbCommand("delete from ArticulosMesa where Mesa='" + lblID.Text + "';", conectar);
            cmd.ExecuteNonQuery();
            obtenerYSumar2();
            if (Conexion.impresora == "")
            {
            }
            else
            {
                imprimir();
                DialogResult dialogResult = MessageBox.Show("¿Imprimir otro ticket?", "Alto!", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    imprimir();
                }
            }
            MessageBox.Show("EL COBRO SE HA REALIZADO CON EXITO!", "EXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (print=="0")
                {
                    cmd = new OleDbCommand("delete from ArticulosMesa where ID1=" + dataGridView1[5, dataGridView1.CurrentRow.Index].Value.ToString() + ";", conectar);
                    cmd.ExecuteNonQuery();
                    cmd = new OleDbCommand("INSERT INTO ArticulosCancelados(Cantidad, Producto, Comentario, Mesa, Fecha, Mesero, Cancelo) VALUES ('" + dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString() + "','" + dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString() + "','" + dataGridView1[6, dataGridView1.CurrentRow.Index].Value.ToString() + "','"+lblMesa.Text+"','" + (DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString()) + "','"+lblMesero.Text+"','"+lblMesero.Text+"');", conectar);
                    cmd.ExecuteNonQuery();
                    string idInsertado = "";
                    cmd = new OleDbCommand("select @@IDENTITY;", conectar);
                    OleDbDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        idInsertado = reader[0].ToString();
                        //MessageBox.Show("ID cancelado: " + idInsertado);
                    }
                    if (dataGridView1.RowCount == 1)
                    {
                        cmd = new OleDbCommand("UPDATE Mesas set IdMesero='0', Mesero='', Print='1' Where Id=" + lblID.Text + ";", conectar);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("EL PRODUCTO SE HA ELIMINADO CON EXITO!", "EXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        total = 0;
                        ds = new DataSet();
                        da = new OleDbDataAdapter("select * from ArticulosMesa where Mesa='" + lblID.Text + "';", conectar);
                        da.Fill(ds, "Id");
                        dataGridView1.DataSource = ds.Tables["Id"];
                        dataGridView1.Columns[0].Visible = false;
                        dataGridView1.Columns[5].Visible = false;
                        for (int i = 0; i < dataGridView1.RowCount; i++)
                        {
                            total += Convert.ToDouble(dataGridView1[4, i].Value.ToString());
                        }
                        lblTotal.Text = $"{RecalcularTotal:C}";
                        MessageBox.Show("EL PRODUCTO SE HA ELIMINADO CON EXITO!", "EXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tipoUser = "";
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
                                        string comentario = com.Comentario;
                                        cmd = new OleDbCommand("delete from ArticulosMesa where ID1=" + dataGridView1[5, dataGridView1.CurrentRow.Index].Value.ToString() + ";", conectar);
                                        cmd.ExecuteNonQuery();
                                        cmd = new OleDbCommand("INSERT INTO ArticulosCancelados(Cantidad, Producto, Comentario, Mesa, Fecha, Mesero, Cancelo) VALUES ('" + dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString() + "','" + dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString() + "','" + com.Comentario+ "','" + lblMesa.Text + "','" + (DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString()) + "','" + lblMesero.Text + "','" + ori.Mesero + "');", conectar);
                                        cmd.ExecuteNonQuery();
                                        string idInsertado = "";
                                        cmd = new OleDbCommand("select @@IDENTITY;", conectar);
                                        OleDbDataReader reader = cmd.ExecuteReader();
                                        if (reader.Read())
                                        {
                                            idInsertado = reader[0].ToString();
                                            //MessageBox.Show("ID cancelado: " + idInsertado);
                                        }
                                        if (dataGridView1.RowCount == 1)
                                        {
                                            cmd = new OleDbCommand("UPDATE Mesas set IdMesero='0', Mesero='' Where Id=" + lblID.Text + ";", conectar);
                                            cmd.ExecuteNonQuery();
                                            MessageBox.Show("EL PRODUCTO SE HA ELIMINADO CON EXITO!", "EXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            this.Close();
                                        }
                                        else
                                        {
                                            total = 0;
                                            ds = new DataSet();
                                            da = new OleDbDataAdapter("select * from ArticulosMesa where Mesa='" + lblID.Text + "';", conectar);
                                            da.Fill(ds, "Id");
                                            dataGridView1.DataSource = ds.Tables["Id"];
                                            dataGridView1.Columns[0].Visible = false;
                                            dataGridView1.Columns[5].Visible = false;
                                            for (int i = 0; i < dataGridView1.RowCount; i++)
                                            {
                                                total += Convert.ToDouble(dataGridView1[4, i].Value.ToString());
                                            }
                                            lblTotal.Text = $"{RecalcularTotal:C}";

                                            MessageBox.Show("EL PRODUCTO SE HA ELIMINADO CON EXITO!", "EXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            tipoUser = "";
                                        }
                                    }
                                    else
                                    MessageBox.Show("SE REQUIERE UN COMENTARIO PARA CANCELAR", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                                MessageBox.Show("SE REQUIERE CLAVE DE ADMINISTRADOR PARA CANCELAR", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                            this.Close();

                    }
                }
                
            }
            catch
            {

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
            posicion+=20;
            e.Graphics.DrawLine(new Pen(Color.Black), 1, posicion, 420, posicion);
            posicion += 10;
            //Lista de Productos
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Far;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {

                double precio = Convert.ToDouble(dataGridView1[4, i].Value.ToString());
                string producto = dataGridView1[2, i].Value.ToString();
                double cant = Convert.ToDouble(dataGridView1[1, i].Value.ToString());
                string item = cant.ToString("0.00", CultureInfo.InvariantCulture);
                string pre = precio.ToString("00.00", CultureInfo.InvariantCulture);
                double precioUni = Convert.ToDouble(dataGridView1[3, i].Value.ToString());
                string uni = precioUni.ToString("00.00", CultureInfo.InvariantCulture);
                if (producto.Length > 20)
                {
                    producto = producto.Substring(0, 20);
                }
                
                e.Graphics.DrawString(item , new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(1, posicion));
                e.Graphics.DrawString(producto, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(40, posicion));
                e.Graphics.DrawString(uni, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(230, posicion),sf);
                e.Graphics.DrawString(String.Format(CultureInfo.InvariantCulture, "{0:0,0.00}", precio), new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(280, posicion),sf);
                posicion += 20;
                string ide;
                if (dataGridView1[0, i].Value.ToString().Substring(0, 1) == "C")
                {
                    if (dataGridView1[9, i].Value.ToString().Length > 0)
                    {
                        ide = dataGridView1[9, i].Value.ToString();
                        string[] ids = ide.Split(';');
                        string RESULT = "";
                        foreach (var word in ids)
                        {
                            string[] ids2 = word.Split(',');
                            for (int i2 = 0; i2 < ids2.Length - 1; i2 = i2 + 2)
                            {
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
                            }
                        }
                    }
                }
                //ticket.AddItem(item, producto, uni + "|" + String.Format(CultureInfo.InvariantCulture, "{0:0,0.00}", precio));
            }
            double to = Convert.ToDouble(total);
            string toty = String.Format(CultureInfo.InvariantCulture, "{0:0,0.00}", to);
            e.Graphics.DrawLine(new Pen(Color.Black), 210, posicion+10, 420, posicion+10);            
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
            txtDescuento.Focus();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
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
                if (radioButton1.Checked)
                {
                    descuento = ((Convert.ToDouble(txtDescuento.Text) / 100)) * total;
                }
                else if (radioButton2.Checked)
                {
                    descuento = Convert.ToDouble(txtDescuento.Text);
                }
                lblTotal.Text = $"{Math.Round(RecalcularTotal - descuento, 0):C}";
                MessageBox.Show("DESCUENTO REALIZADO POR LA CANTIDAD DE: $" + descuento, "DESCUENTO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
