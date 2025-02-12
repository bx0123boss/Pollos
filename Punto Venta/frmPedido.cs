using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;
using LibPrintTicket;
using System.Globalization;
using System.Drawing.Printing;

namespace Punto_Venta
{
    public partial class frmPedido : Form
    {
        private DataSet ds;
        int idMesero = 0;
        int ti1 = 0, ti2 = 0;
        string idCliente;
        string modalidad;
        OleDbConnection conectar = new OleDbConnection(Conexion.CadCon); 
        OleDbDataAdapter da;
        OleDbCommand cmd;
        public String Usuario;
        string mesSelect = "", idMesSel = "";
        double total = 0;
        int suma = 0;
        string categoria = "";
        bool mesaNueva;
        string idMesa;
        private double utilidadTotal;

        public frmPedido()
        {
            InitializeComponent();
            
        }

        private void rbRapido_CheckedChanged(object sender, EventArgs e)
        {
            CmbMesa.Visible = false;
            panel1.Visible = false;
            idCliente = "";
            modalidad = "RAPIDO";
        }

        private void rbDomicilo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDomicilo.Checked)
            {
                CmbMesa.Visible = false;
                using (frmBuscarClientes ori = new frmBuscarClientes())
                {
                    if (ori.ShowDialog() == DialogResult.OK)
                    {
                        LblNombre.Text = ori.Nombre;
                        LblDomicilio.Text = ori.Direccion;
                        LblTelefono.Text = ori.Telefono;
                        LblReferencia.Text = ori.Referencia;
                        idCliente = ori.Id;
                        panel1.Visible = true;
                        cbCliente.Checked = true;
                        lblColonia.Text = ori.Colonia;
                        modalidad = "DOMICILIO";
                    }
                }
            }

        }

        private void rbMesa_CheckedChanged(object sender, EventArgs e)
        {

            idCliente = "";
            CmbMesa.Visible = true;
            panel1.Visible = false;
            modalidad = "MESA";
        }

        public void cargarMesas()
        {

            ds = new DataSet();
            da = new OleDbDataAdapter("select * from mesas Order By Nombre;", conectar);
            da.Fill(ds, "Id");
            dgvMesas.DataSource = ds.Tables["Id"];
            for (int i = 0; i < dgvMesas.RowCount; i++)
            {
                string id = dgvMesas[0, i].Value.ToString();
                Button but = new Button();
                but.FlatStyle = FlatStyle.Flat;
                but.FlatAppearance.BorderSize = 0;
                but.Font = new System.Drawing.Font(new FontFamily("Calibri"), 11, FontStyle.Bold);
                //but.BackColor = Color.Aqua;
                but.Size = new System.Drawing.Size(40, 40);
                but.Text = dgvMesas[1, i].Value.ToString();
                but.Click += new EventHandler(this.Mesa);
                flowBotones.Controls.Add(but);
                cmd = new OleDbCommand("SELECT * FROM ArticulosMesa where Mesa='" + id + "';", conectar);
                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    but.BackColor = Color.Red;
                }
                else
                {
                    but.BackColor = Color.SkyBlue;
                }
            }
        }

        private void Mesa(object sender, EventArgs e)
        {
            if ((sender as Button).BackColor == Color.Red)
            {
                cmd = new OleDbCommand("select IdMesero,Mesero,Id from Mesas where Nombre='" + (sender as Button).Text + "';", conectar);
                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    lblMesero.Text = reader[1].ToString();
                    idMesero = Convert.ToInt32(reader[0].ToString());
                    mesaNueva = false;
                    idMesa = reader[2].ToString();
                }
                else
                {
                    lblMesero.Text = mesSelect;
                    idMesero = Convert.ToInt32(idMesSel);
                    mesaNueva = true;
                }
            }
            else
            {
                lblMesero.Text = mesSelect;
                idMesero = Convert.ToInt32(idMesSel);
                mesaNueva = true;
            }
            CmbMesa.Text = (sender as Button).Text;
        }

        public void cargarCategoriasAutomatico()
        {
            panelCategorias.Hide();
            Button butP = new Button();
            butP.FlatStyle = FlatStyle.Flat;
            butP.FlatAppearance.BorderSize = 0;
            butP.Font = new System.Drawing.Font(new FontFamily("Calibri"), 11, FontStyle.Bold);
            butP.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff8000");
            butP.ForeColor = Color.FromName("White");
            butP.Size = new System.Drawing.Size(104, 56);
            butP.Text = "PIZZA";
            butP.Click += new EventHandler(this.Pizzas);
            flowLayoutPanel2.Controls.Add(butP);
            cmd = new OleDbCommand("SELECT * FROM Categorias WHERE NOT Nombre='MITADES PIZZA' AND NOT Nombre='PIZZAS' AND NOT Nombre='MASAS';", conectar);
            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                //crear boton
                Button but = new Button();
                but.FlatStyle = FlatStyle.Flat;
                but.FlatAppearance.BorderSize = 0;
                but.Font = new System.Drawing.Font(new FontFamily("Calibri"), 11, FontStyle.Bold);
                but.BackColor = System.Drawing.ColorTranslator.FromHtml("#" + reader[2].ToString());
                but.ForeColor = Color.FromName(reader[3].ToString());
                but.Size = new System.Drawing.Size(104, 56);
                but.Text = reader[1].ToString();
                but.Click += new EventHandler(this.Myevent2);
                flowLayoutPanel2.Controls.Add(but);
            }
            Button butC = new Button();
            butC.FlatStyle = FlatStyle.Flat;
            butC.FlatAppearance.BorderSize = 0;
            butC.Font = new System.Drawing.Font(new FontFamily("Calibri"), 11, FontStyle.Bold);
            butC.BackColor = System.Drawing.ColorTranslator.FromHtml("#654321");
            butC.ForeColor = Color.FromName("White");
            butC.Size = new System.Drawing.Size(104, 56);
            butC.Text = "COMBOS";
            butC.Click += new EventHandler(this.Combos);
            flowLayoutPanel2.Controls.Add(butC);
            //groupBox2.Show();
        }

        private void frmPedido_Load(object sender, EventArgs e)
        {            
            conectar.Open();
            cargarMesas();
            cargarCategoriasAutomatico();
            //cargarProductosNormal();

            if (Usuario == "VENTAS")
            {
                DgvPedidoprevio.Columns[4].ReadOnly = true;
                DgvPedidoprevio.Columns[3].ReadOnly = true;
            }
                          
            panel2.Visible = false;
           
            DataTable dt = new DataTable();
            CmbMesa.Text = "";

            dt = new DataTable();
            cmd = new OleDbCommand("select * from mesas Order By Nombre;", conectar);
            da = new OleDbDataAdapter(cmd);
            da.Fill(dt);
            CmbMesa.DisplayMember = "Nombre";
            CmbMesa.ValueMember = "Id";
            CmbMesa.DataSource = dt;
            CmbMesa.Text = "";           


           
            
            using (frmClaveVendendor ori = new frmClaveVendendor())
            {
                if (ori.ShowDialog() == DialogResult.OK)
                {
                    mesSelect = ori.Mesero;
                    idMesSel = ori.Id.ToString();
                    idMesero = ori.Id;
                    lblMesero.Text = ori.Mesero;
                }
                else
                    this.Close();
            }
        }

        private void Myevent(object sender, EventArgs e)
        {

            //BOTON PULSADO ALV
            cmd = new OleDbCommand("select id,Nombre,Precio,Categoria,Comanda, CostoTotal from inventario where Nombre='" + (sender as Button).Text + "';", conectar);
            OleDbDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                using (frmCantidad ori = new frmCantidad())
                {
                    if (ori.ShowDialog() == DialogResult.OK)
                    {
                        
                        double precio = Convert.ToDouble(ori.cantidad) * (Convert.ToDouble(reader[2].ToString()));
                        double pre = (Convert.ToDouble(reader[2].ToString()));
                        string comanda = reader[4].ToString();
                        DgvPedidoprevio.Rows.Add(Convert.ToString(reader[0].ToString()), ori.cantidad, Convert.ToString(reader[1].ToString()), Math.Round(pre, 2), Math.Round(precio, 2), ori.comentario, comanda,"", Convert.ToString(reader[5].ToString()));
                        total += precio;
                        LblTotal.Text = String.Format(CultureInfo.InvariantCulture, "{0:0,0.00}", total);
                    }
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
                        string ides = "1,"+pi.id1+";1," +pi.id2+";1," + pi.idMasa+";";
                        DgvPedidoprevio.Rows.Add("P"+pi.id1, "1", pi.nombre, pi.precio, pi.precio, pi.comentarios, "1", ides);
                        //SEPARAR IDES
                        //string[] ids = ides.Split(';');
                        //foreach (var word in ids)
                        //{
                        //    MessageBox.Show(ids[0] + " " + ids[1]);
                        //}
                        total += Convert.ToDouble(Convert.ToString(pi.precio));
                        for (int i = 0; i < pi.dataGridView1.RowCount; i++)
                        {
                            DgvPedidoprevio.Rows.Add(pi.dataGridView1[0, i].Value.ToString(), pi.dataGridView1[1, i].Value.ToString(), pi.dataGridView1[2, i].Value.ToString(), pi.dataGridView1[3, i].Value.ToString(), pi.dataGridView1[4, i].Value.ToString(), pi.dataGridView1[5, i].Value.ToString(), "1","");
                            total += Convert.ToDouble(pi.dataGridView1[4, i].Value.ToString());
                        }
                    }
                    else
                    {
                        for (int i = 0; i < pi.DgvPedidoprevio.RowCount; i++)
                        {
                            DgvPedidoprevio.Rows.Add(pi.DgvPedidoprevio[0, i].Value.ToString(), pi.DgvPedidoprevio[1, i].Value.ToString(), pi.DgvPedidoprevio[2, i].Value.ToString(), pi.DgvPedidoprevio[3, i].Value.ToString(), pi.DgvPedidoprevio[3, i].Value.ToString(), pi.DgvPedidoprevio[4, i].Value.ToString(), "1","1," + pi.idMasa+";");
                            total += Convert.ToDouble(pi.DgvPedidoprevio[3, i].Value.ToString());
                        }
                        for (int i = 0; i < pi.dataGridView1.RowCount; i++)
                        {
                            DgvPedidoprevio.Rows.Add(pi.dataGridView1[0, i].Value.ToString(), pi.dataGridView1[1, i].Value.ToString(), pi.dataGridView1[2, i].Value.ToString(), pi.dataGridView1[3, i].Value.ToString(), pi.dataGridView1[4, i].Value.ToString(), pi.dataGridView1[5, i].Value.ToString(), "1","");
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
                    string ides="";
                    for (int i = 0; i < pi.DgvPedidoprevio.RowCount; i++)
                    {
                        string idCombo = pi.DgvPedidoprevio[0,i].Value.ToString();
                        double cantCombo = Convert.ToDouble(pi.DgvPedidoprevio[1, i].Value.ToString()) * Convert.ToDouble(pi.cantidad);
                        ides += cantCombo + "," + idCombo + ";";
                    }
                    DgvPedidoprevio.Rows.Add(pi.id, pi.cantidad, pi.nombre, pi.precio, pi.total, pi.comentario, "1",ides);
                }
                LblTotal.Text = String.Format("{0:0.00}", total);
            }      
        }

        private void Myevent2(object sender, EventArgs e)
        {
            if (categoria == (sender as Button).Text)
            {

            }
            else
            {
                flowLayoutPanel1.Controls.Clear();
                cmd = new OleDbCommand("SELECT Color,Letra FROM Categorias where Nombre='" + (sender as Button).Text + "';", conectar);
                OleDbDataReader reader1 = cmd.ExecuteReader();
                if(reader1.Read())
                {
                    cmd = new OleDbCommand("SELECT Nombre FROM Inventario where Categoria='" + (sender as Button).Text + "' order by Nombre;", conectar);
                    OleDbDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Button but = new Button();
                        but.FlatStyle = FlatStyle.Flat;
                        but.FlatAppearance.BorderSize = 0;
                        but.Font = new System.Drawing.Font(new FontFamily("Calibri"), 12, FontStyle.Bold);
                        but.BackColor = System.Drawing.ColorTranslator.FromHtml("#" + reader1[0].ToString());
                        but.ForeColor = Color.FromName(reader1[1].ToString());
                        but.Size = new System.Drawing.Size(135, 70);
                        but.Text = reader[0].ToString();
                        but.Click += new EventHandler(this.Myevent);
                        flowLayoutPanel1.Controls.Add(but);
                    }
                    categoria = (sender as Button).Text;
                }
            }
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
            lblFolio.Text = "V"+ suma ;


        }

        public void obtenerYSumar2()
        {
            suma = suma + 1;
            cmd = new OleDbCommand("UPDATE Folio set Numero=" + suma + " where Folio='Venta';", conectar);
            cmd.ExecuteNonQuery();
        }

        private void TxtBuscaid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (TxtBuscaid.Text == "")
                {

                }
                else
                {
                    cmd = new OleDbCommand("select id,Nombre,Precio,Categoria from inventario where Id=" + TxtBuscaid.Text + ";", conectar);
                    OleDbDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        DgvPedidoprevio.Rows.Add(Convert.ToString(reader[0].ToString()), "1", Convert.ToString(reader[1].ToString()), Convert.ToString(reader[2].ToString()), Convert.ToString(reader[2].ToString()), "");
                        total += Convert.ToDouble(Convert.ToString(reader[2].ToString()));
                        LblTotal.Text = String.Format("{0:0.00}", total);
                    }
                    TxtBuscaid.Clear();
                    TxtComentarios.Clear();
                    TxtBuscanombre.Clear();
                }
            }
        }

        public string separarIdes(string id)
        {
            string[] ids = id.Split(';');
            string resultado = "";
            foreach (var word in ids)
            {
                string[] ids2 = word.Split(',');
                for (int i = 0; i < ids2.Length-1; i=i+2)
                {
                    resultado += ids2[0] + " : " + ids2[1] + "\n";
                }
            }
            return resultado;
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

        private void llevar()
        {
            utilidadTotal = 0;
             total = 0;
            for (int i = 0; i < DgvPedidoprevio.RowCount; i++)
            {
                double compra = Convert.ToDouble(DgvPedidoprevio[8, i].Value.ToString());
                double venta = Convert.ToDouble(DgvPedidoprevio[3, i].Value.ToString());
                double piezas = Convert.ToDouble(DgvPedidoprevio[1, i].Value.ToString());
                double utilidad = (venta - compra) * piezas;
                utilidadTotal += utilidad;
                string subcategoria = obtenerSubcategoria(DgvPedidoprevio[0, i].Value.ToString());
                cmd = new OleDbCommand("insert into ventas(idProducto,Cantidad,Producto,Comentarios,Precio,Total,Folio,Fecha,Estatus,Subcategoria,Utilidad) values('" + DgvPedidoprevio[0, i].Value.ToString() + "','" + DgvPedidoprevio[1, i].Value.ToString() + "','" + DgvPedidoprevio[2, i].Value.ToString() + "','" + DgvPedidoprevio[5, i].Value.ToString() + "','" + DgvPedidoprevio[3, i].Value.ToString() + "','" + DgvPedidoprevio[4, i].Value.ToString() + "','" + lblFolio.Text + "','" + (DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString()) + "','COBRADO','" + subcategoria + "','"+utilidad+"');", conectar);
                cmd.ExecuteNonQuery();
                string ide = "";
                if (DgvPedidoprevio[7, i].Value.ToString().Length > 0)
                    ide = DgvPedidoprevio[7, i].Value.ToString();
                cmd = new OleDbCommand("insert into temp(id,cantidad, producto, precio, total,ide) values ('" + DgvPedidoprevio[0, i].Value.ToString() + "','" + DgvPedidoprevio[1, i].Value.ToString() + "','" + DgvPedidoprevio[2, i].Value.ToString() + "'," + DgvPedidoprevio[3, i].Value.ToString() + ",'" + DgvPedidoprevio[4, i].Value.ToString() + "','" + ide + "');", conectar);
                cmd.ExecuteNonQuery();
                total += Convert.ToDouble(DgvPedidoprevio[4, i].Value.ToString());
            }
            cmd = new OleDbCommand("insert into folios(Folio,ModalidadVenta,Estatus,idCliente,Fecha,Colonia,Monto,FormaPago, Utilidad) values ('" + lblFolio.Text + "','P/LLEVAR','COBRADO','" + idCliente + "','" + (DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString()) + "','" + lblColonia.Text + "','" + total + "','Efectivo','"+utilidadTotal+"');", conectar);
            cmd.ExecuteNonQuery();
            cmd = new OleDbCommand("INSERT INTO corte (concepto, total,fecha,FormaPago) VALUES ('VENTA FOLIO: " + lblFolio.Text + "'," + total + ",'" + (DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString()) + "','Efectivo');", conectar);
            cmd.ExecuteNonQuery();
            int width = 420;
            int height = 540;
            printDocument1.PrinterSettings.DefaultPageSettings.PaperSize = new PaperSize("", width, height);
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(this.printDocument1_PrintPage);
            PrintDialog printdlg = new PrintDialog();
            PrintPreviewDialog printPrvDlg = new PrintPreviewDialog();
            printPrvDlg.Document = pd;
            printdlg.Document = pd;
            pd.Print();
            DialogResult dialogResult = MessageBox.Show("¿Imprimir otro ticket?", "Alto!", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                pd.Print();
            }
            obtenerYSumar2();
        }

        private void enMesa()
        {
            for (int i = 0; i < DgvPedidoprevio.RowCount; i++)
            {
                double compra = Convert.ToDouble(DgvPedidoprevio[8, i].Value.ToString());
                double venta = Convert.ToDouble(DgvPedidoprevio[3, i].Value.ToString());
                double piezas = Convert.ToDouble(DgvPedidoprevio[1, i].Value.ToString());
                double utilidad = (venta - compra) * piezas;
                string ide = "";
                if (DgvPedidoprevio[7, i].Value.ToString().Length > 0)
                    ide = DgvPedidoprevio[7, i].Value.ToString();
                cmd = new OleDbCommand("insert into ArticulosMesa(id,cantidad,producto,precio,total,Comentario,Mesa,Estatus,ids,Utilidad) values('" + DgvPedidoprevio[0, i].Value.ToString() + "','" + DgvPedidoprevio[1, i].Value.ToString() + "','" + DgvPedidoprevio[2, i].Value.ToString() + "','" + DgvPedidoprevio[3, i].Value.ToString() + "','" + DgvPedidoprevio[4, i].Value.ToString() + "','" + DgvPedidoprevio[5, i].Value.ToString() + "','" + CmbMesa.SelectedValue + "','COCINA','" + ide + "','"+ utilidad + "');", conectar);
                cmd.ExecuteNonQuery();
            }
        }

        private void domicilio()
        {
            utilidadTotal = 0;
            total = 0;
            for (int i = 0; i < DgvPedidoprevio.RowCount; i++)
            {
                double compra = Convert.ToDouble(DgvPedidoprevio[8, i].Value.ToString());
                double venta = Convert.ToDouble(DgvPedidoprevio[3, i].Value.ToString());
                double piezas = Convert.ToDouble(DgvPedidoprevio[1, i].Value.ToString());
                double utilidad = (venta - compra) * piezas;
                utilidadTotal += utilidad;
                string subcategoria = obtenerSubcategoria(DgvPedidoprevio[0, i].Value.ToString());
                cmd = new OleDbCommand("insert into ventas(idProducto,Cantidad,Producto,Comentarios,Precio,Total,Folio,Fecha,Estatus,ide,Subcategoria,Utilidad) values('" + DgvPedidoprevio[0, i].Value.ToString() + "','" + DgvPedidoprevio[1, i].Value.ToString() + "','" + DgvPedidoprevio[2, i].Value.ToString() + "','" + DgvPedidoprevio[5, i].Value.ToString() + "','" + DgvPedidoprevio[3, i].Value.ToString() + "','" + DgvPedidoprevio[4, i].Value.ToString() + "','" + lblFolio.Text + "','" + (DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString()) + "','COCINA','" + DgvPedidoprevio[7, i].Value.ToString() + "','" + subcategoria + "','"+utilidad+"');", conectar);
                cmd.ExecuteNonQuery();
                total += Convert.ToDouble(DgvPedidoprevio[4, i].Value.ToString());
            }
            cmd = new OleDbCommand("insert into folios(Folio,ModalidadVenta,Estatus,idCliente,Fecha,Colonia,Monto,FormaPago,Utilidad) values ('" + lblFolio.Text + "','REPARTO','COCINA','" + idCliente + "','" + (DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString()) + "','" + lblColonia.Text + "','" + total + "','Efectivo','"+utilidadTotal+"');", conectar);
            cmd.ExecuteNonQuery();
            obtenerYSumar2();
        }
        private void BtnEntregar_Click(object sender, EventArgs e)
        {
            if (!ordenVacia())            
            {
                BtnEntregar.Visible = false;
                obtenerYSumar();
                if (Conexion.lugar == "TERRAZA")
                    lblFolio.Text = "T" + String.Format("{0:0000}", suma);
                else if (Conexion.lugar == "COCINA")
                    lblFolio.Text = "V" + String.Format("{0:0000}", suma);
                Ticket ticket = new Ticket();
                ticket.FontSize = 10;
                ticket.MaxCharDescription = 26;
                //Llevar
                if (tabControl1.SelectedIndex == 2)
                {
                    ticket.AddHeaderLine("Folio: " + lblFolio.Text);
                    ticket.AddHeaderLine(" ");
                    ticket.AddHeaderLine("****PARA LLEVAR****");
                    ticket.AddHeaderLine(" ");
                    ticket.AddHeaderLine("FECHA: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                    llevar();
                }
                //mesa
                else if (tabControl1.SelectedIndex == 0)
                {
                    ticket.AddHeaderLine("FECHA: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                    ticket.AddHeaderLine(" ");
                    ticket.AddHeaderLine("****MESA " + CmbMesa.Text + "****");
                    ticket.AddHeaderLine(" ");
                    enMesa();
                }
                //domicilio
                else if (tabControl1.SelectedIndex == 1)
                {
                    ticket.AddHeaderLine("Folio: " + lblFolio.Text);
                    ticket.AddHeaderLine(" ");
                    ticket.AddHeaderLine("****ENTREGA A DOMICILIO****");
                    ticket.AddHeaderLine("Folio: " + lblFolio.Text);
                    ticket.AddHeaderLine("Cliente: " + LblNombre.Text);
                    domicilio();
                }
                ticket.AddHeaderLine("MESERO:" + lblMesero.Text);
                ticket.AddHeaderLine("FECHA: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                for (int i = 0; i < DgvPedidoprevio.RowCount; i++)
                {
                    string comentario = " ";
                    if(DgvPedidoprevio[5, i].Value.ToString().Length>0)
                    {
                        comentario+=":" + DgvPedidoprevio[5, i].Value.ToString();
                    }
                    ticket.AddItem(DgvPedidoprevio[1, i].Value.ToString(), DgvPedidoprevio[2, i].Value.ToString() + comentario , "");
                    string ide = "";
                    if (DgvPedidoprevio[0, i].Value.ToString().Substring(0, 1) == "C")
                    {
                        if (DgvPedidoprevio[7, i].Value.ToString().Length > 0)
                        {
                            ide = DgvPedidoprevio[7, i].Value.ToString();
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
                                        ticket.AddItem(ids2[0], reader[1].ToString() + "", "");
                                        RESULT += ids2[0] + " : " + reader[1].ToString() + "\n";
                                    }
                                }
                            }
                        }
                    }                       
                }
                //ticket.PrintTicket(Conexion.impresora2);
                if (mesaNueva)
                {
                    cmd = new OleDbCommand("UPDATE Mesas SET IdMesero='" + idMesero + "',Mesero='" + lblMesero.Text + "' where Id=" + CmbMesa.SelectedValue + ";", conectar);
                    cmd.ExecuteNonQuery();  
                }
                this.Close();
                MessageBox.Show("SE HA REALIZADO LA ORDEN CON EXITO!", "ORDEN REALIZADA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmPedido pedo = new frmPedido();
                pedo.Show();
            }
        }



        private void recalcular()
        {
            total = 0;
            for (int i = 0; i < DgvPedidoprevio.RowCount; i++)
            {
                total += Convert.ToDouble(DgvPedidoprevio[4, i].Value.ToString());
            }
            LblTotal.Text = String.Format("{0:0.00}", total);
        }

        private void DgvPedidoprevio_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {            
            double cantidad = Convert.ToDouble(DgvPedidoprevio[1, DgvPedidoprevio.CurrentRow.Index].Value.ToString());
            double precio = Convert.ToDouble(DgvPedidoprevio[3, DgvPedidoprevio.CurrentRow.Index].Value.ToString());
            double monto = cantidad * precio;
            DgvPedidoprevio.Rows[e.RowIndex].Cells[4].Value = monto;
            recalcular();
        }

        private void frmPedido_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.LControlKey)
            {
                DgvPedidoprevio.Rows.RemoveAt(DgvPedidoprevio.CurrentRow.Index);
                recalcular();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
                cmd = new OleDbCommand("select id,Nombre,Precio,Categoria from inventario where Nombre='" + DgvPidiendo[1, DgvPidiendo.CurrentRow.Index].Value.ToString() + "';", conectar);
                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    DgvPedidoprevio.Rows.Add(Convert.ToString(reader[0].ToString()), "1", Convert.ToString(reader[1].ToString()), Convert.ToString(reader[2].ToString()), Convert.ToString(reader[2].ToString()), "","1","");
                    total += Convert.ToDouble(Convert.ToString(reader[2].ToString()));
                    LblTotal.Text = String.Format("{0:0.00}", total);
                }
                TxtBuscaid.Clear();
                TxtComentarios.Clear();
                TxtBuscanombre.Clear();            
        }

        //ELIMINAR DESDE BOTON
        private void button2_Click(object sender, EventArgs e)
        {
            if(DgvPedidoprevio.RowCount>0)
            {
                DgvPedidoprevio.Rows.RemoveAt(DgvPedidoprevio.CurrentRow.Index);
                recalcular();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ds = new DataSet();
            da = new OleDbDataAdapter("select id,Nombre,Precio,Categoria from inventario where Categoria='"+cmbCategorias.Text+"';", conectar);
            da.Fill(ds, "Id");
            DgvPidiendo.DataSource = ds.Tables["Id"];
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                cmbFolios.Visible = true;
                lblFolio.Visible = false;
                groupBox1.Visible = false;
            }
            else
            {
                cmbFolios.Visible = false;
                lblFolio.Visible = true;
                groupBox1.Visible = true;
            }
        }

        private void cbCliente_CheckedChanged(object sender, EventArgs e)
        {
            CmbMesa.Visible = false;
            using (frmBuscarClientes ori = new frmBuscarClientes())
            {
                if (ori.ShowDialog() == DialogResult.OK)
                {
                    LblNombre.Text = ori.Nombre;
                    LblDomicilio.Text = ori.Direccion;
                    LblTelefono.Text = ori.Telefono;
                    LblReferencia.Text = ori.Referencia;
                    idCliente = ori.Id;
                    panel1.Visible = true;
                    cbCliente.Checked = true;
                    lblColonia.Text = ori.Colonia;
                    modalidad = "DOMICILIO";
                }
            }
            if (cbCliente.Checked)
            {
                panel1.Visible = true;
                panelCategorias.Visible = false;
            }
            else
            {
                panel1.Visible = false;
                panelCategorias.Visible = true;
            }
        }

        
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                int posicion = 10;
                //RESIZE
                Image logo = Image.FromFile("C:\\Jaeger Soft\\logo.jpg");
                e.Graphics.DrawImage(logo, new PointF(1, 10));
                //LOGO
                posicion += 180;
                e.Graphics.DrawString("********  NOTA DE CONSUMO  ********", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(1, posicion));
                posicion += 20;
                e.Graphics.DrawString("Cliente: " + LblNombre.Text, new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(1, posicion));
                posicion += 20;
                e.Graphics.DrawString("Telefono: " + LblTelefono.Text, new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(1, posicion));
                posicion += 20;
                e.Graphics.DrawString("Domicilio: " + LblDomicilio.Text, new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(1, posicion));
                posicion += 20;
                e.Graphics.DrawString("Referencia: " + LblReferencia.Text, new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(1, posicion));
                posicion += 20;
                e.Graphics.DrawString("FOLIO DE VENTA: " + lblFolio.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(1, posicion));
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
                for (int i = 0; i < DgvPedidoprevio.RowCount; i++)
                {

                    double precio = Convert.ToDouble(DgvPedidoprevio[4, i].Value.ToString());
                    string producto = DgvPedidoprevio[2, i].Value.ToString();
                    double cant = Convert.ToDouble(DgvPedidoprevio[1, i].Value.ToString());
                    string item = cant.ToString("0.00", CultureInfo.InvariantCulture);
                    string pre = precio.ToString("00.00", CultureInfo.InvariantCulture);
                    double precioUni = Convert.ToDouble(DgvPedidoprevio[3, i].Value.ToString());
                    string uni = precioUni.ToString("00.00", CultureInfo.InvariantCulture);
                    if (producto.Length > 15)
                    {
                        producto = producto.Substring(0, 15);
                    }

                    e.Graphics.DrawString(item, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(1, posicion));
                    e.Graphics.DrawString(producto, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(40, posicion));
                    e.Graphics.DrawString(uni, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(210, posicion), sf);
                    e.Graphics.DrawString(String.Format(CultureInfo.InvariantCulture, "{0:0,0.00}", precio), new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(280, posicion), sf);
                    posicion += 20;
                }
                double to = Convert.ToDouble(LblTotal.Text);
                string toty = String.Format(CultureInfo.InvariantCulture, "{0:0,0.00}", to);
                e.Graphics.DrawLine(new Pen(Color.Black), 210, posicion + 10, 420, posicion + 10);

                posicion += 15;
                e.Graphics.DrawString("TOTAL: $" + toty, new Font("Arial", 10, FontStyle.Bold), Brushes.Black, new Point(280, posicion), sf);
                posicion += 50;
                e.Graphics.DrawLine(new Pen(Color.Black), 1, posicion, 2, posicion);
            }
            if (tabControl1.SelectedIndex == 2)            
            {
                int posicion = 10;
                //RESIZE
                Image logo = Image.FromFile("C:\\Jaeger Soft\\logo.jpg");
                e.Graphics.DrawImage(logo, new PointF(1, 10));
                //LOGO
                posicion += 200;
                e.Graphics.DrawString("********  NOTA DE CONSUMO  ********", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(1, posicion));
                posicion += 20;
                e.Graphics.DrawString("FOLIO DE VENTA: " + lblFolio.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(1, posicion));
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
                for (int i = 0; i < DgvPedidoprevio.RowCount; i++)
                {

                    double precio = Convert.ToDouble(DgvPedidoprevio[4, i].Value.ToString());
                    string producto = DgvPedidoprevio[2, i].Value.ToString();
                    double cant = Convert.ToDouble(DgvPedidoprevio[1, i].Value.ToString());
                    string item = cant.ToString("0.00", CultureInfo.InvariantCulture);
                    string pre = precio.ToString("00.00", CultureInfo.InvariantCulture);
                    double precioUni = Convert.ToDouble(DgvPedidoprevio[3, i].Value.ToString());
                    string uni = precioUni.ToString("00.00", CultureInfo.InvariantCulture);
                    if (producto.Length > 15)
                    {
                        producto = producto.Substring(0, 15);
                    }

                    e.Graphics.DrawString(item, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(1, posicion));
                    e.Graphics.DrawString(producto, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(40, posicion));
                    e.Graphics.DrawString(uni, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(210, posicion), sf);
                    e.Graphics.DrawString(String.Format(CultureInfo.InvariantCulture, "{0:0,0.00}", precio), new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(280, posicion), sf);
                    posicion += 20;
                }
                double to = Convert.ToDouble(LblTotal.Text);
                string toty = String.Format(CultureInfo.InvariantCulture, "{0:0,0.00}", to);
                e.Graphics.DrawLine(new Pen(Color.Black), 210, posicion + 10, 420, posicion + 10);

                posicion += 15;
                e.Graphics.DrawString("TOTAL: $" + toty, new Font("Arial", 10, FontStyle.Bold), Brushes.Black, new Point(280, posicion), sf);
                posicion += 50;
                e.Graphics.DrawLine(new Pen(Color.Black), 1, posicion, 2, posicion);
            }
        }

       //BUSCAR CLIENTES
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
    }
}
