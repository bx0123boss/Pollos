using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Punto_Venta
{
    public partial class frmSeleccionarCombo : Form
    {
        private DataSet ds;
        OleDbConnection conectar = new OleDbConnection(Conexion.CadCon);
        OleDbDataAdapter da;
        OleDbCommand cmd;
        string idPromo = "";
        string nombrePromo = "";
        double precioPromo = 0;
        string idArtPromos = "";
        string subFiltrar = "";
        public string id { get; set; }
        public string cantidad { get; set; }
        public string nombre { get; set; }
        public double precio { get; set; }
        public double total { get; set; }
        public string comentario { get; set; }
        public frmSeleccionarCombo()
        {
            InitializeComponent();
        }

       

        private void frmSeleccionarCombo_Load(object sender, EventArgs e)
        {
            Random random = new Random();
            conectar.Open();
            //ds = new DataSet();
            //da = new OleDbDataAdapter("select Id,Nombre,Precio,Subcategoria from Inventario WHERE NOT Subcategoria='' ORDER BY Nombre;", conectar);
            //da.Fill(ds, "Id");
            //dataGridView1.DataSource = ds.Tables["Id"];
            ds = new DataSet();
            da = new OleDbDataAdapter("select * from Promos where " + DateTime.Now.ToString("dddd") + "=True ORDER BY Nombre;", conectar);
            da.Fill(ds, "Id");
            dgvInventario.DataSource = ds.Tables["Id"];
            dgvInventario.Columns[0].Visible = false;
            articulosPromo(dgvInventario[0, 0].Value.ToString());
            dgvInventario.Rows[0].Selected = true;
            //lenar prueba
            foreach (DataRow row in ds.Tables["Id"].Rows)
            {
                // Generar valores aleatorios para los componentes de color
                int red = random.Next(256);    // Valores entre 0 y 255 (256 excluido)
                int green = random.Next(256);
                int blue = random.Next(256);

                // Crear un nuevo objeto Color con los valores aleatorios
                Color randomColor = Color.FromArgb(red, green, blue);

                StringBuilder sb = new StringBuilder();
                string id = Convert.ToString(row[0]); // Suponiendo que el primer campo es de tipo entero (Id)
                string nombre = row[1].ToString(); // Suponiendo que el segundo campo es de tipo cadena (Nombre)
                double precio = Convert.ToDouble(row[2]); // Suponiendo que el tercer campo es de tipo decimal (Precio)
                                                         //crear boton
                Button but = new Button();
                but.FlatStyle = FlatStyle.Flat;
                but.FlatAppearance.BorderSize = 0;
                but.Font = new System.Drawing.Font(new FontFamily("Calibri"), 11, FontStyle.Bold);
                but.BackColor = randomColor;
                //but.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                but.ForeColor = Color.FromName("White");
                but.Size = new System.Drawing.Size(104, 56);
                but.Text = row[1].ToString();
                but.Tag = new Tuple<string, string, double>(id, nombre, precio);
                sb.Append(nombre+"\n");
                sb.Append($"Precio: ${precio} \nSe vende:");
                sb.Append(row["Lunes"].ToString() == "True" ? " Lunes" : "");
                sb.Append(row["Martes"].ToString() == "True" ? ", Martes" : "");
                sb.Append(row["Miércoles"].ToString() == "True" ? ", Miércoles" : "");
                sb.Append(row["Jueves"].ToString() == "True" ? ", Jueves" : "");
                sb.Append(row["Viernes"].ToString() == "True" ? ", Viernes" : "");
                sb.Append(row["Sábado"].ToString() == "True" ? ", Sábado" : "");
                sb.Append(row["Domingo"].ToString() == "True" ? ", Domingo" : "");
                toolTip1.SetToolTip(but, sb.ToString());
                flowLayoutPanel2.Controls.Add(but);
                but.Click += new EventHandler(this.botonPromos);
                // Acceder a los valores de cada columna por índice

            }
        }

        private void botonPromos(object sender, EventArgs e)
        {
           
            if ((sender as Button).Tag is Tuple<string, string, double> tupla)
            {
                idPromo = tupla.Item1;
                if (idArtPromos != idPromo)
                {
                    dgvPromo.Rows.Clear();
                    flowLayoutPanel1.Controls.Clear();
                    nombrePromo = tupla.Item2;
                    precioPromo = tupla.Item3;
                    articulosPromo(idPromo);
                    idArtPromos = idPromo;
                }
            }
            
        }
        private void dgvInventario_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            seleccionarPromo();
        }

        private void seleccionarPromo()
        {
            dgvPromo.Rows.Clear();
            flowLayoutPanel1.Controls.Clear();
            idPromo = dgvInventario[0, dgvInventario.CurrentRow.Index].Value.ToString();
            nombrePromo = dgvInventario[1, dgvInventario.CurrentRow.Index].Value.ToString();
            precioPromo = Convert.ToDouble(dgvInventario[2, dgvInventario.CurrentRow.Index].Value.ToString());
            articulosPromo(dgvInventario[0, dgvInventario.CurrentRow.Index].Value.ToString());
        }

        private void filtroBoton(object sender, EventArgs e)
        {
            if ((sender as Button).Tag is Tuple<string, double, string> tupla)
            {
                string id = tupla.Item3;
                filtrar(id);
            }
        }
        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtBuscar.Text != "")
                    da = new OleDbDataAdapter("select Id,Nombre,Precio,Subcategoria from Inventario WHERE Nombre LIKE '%" + txtBuscar.Text + "%' ORDER BY Nombre;", conectar);                
                else
                    da = new OleDbDataAdapter("select Id,Nombre,Precio,Subcategoria from Inventario ORDER BY Nombre;", conectar);
                ds = new DataSet();
                da.Fill(ds, "Id");
                dataGridView1.DataSource = ds.Tables["Id"];
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {                
                comparaCategorias();
                if (todoOK())
                {
                    id = idPromo;
                    nombre = nombrePromo;
                    cantidad = lblCant.Text;
                    precio = precioPromo;
                    total = precioPromo * Convert.ToDouble(cantidad);
                    comentario = txtComentario.Text;
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;                           
                }
                else
                {
                    MessageBox.Show("FALTAN ELEMENTOS DEL COMBO, FAVOR DE CHECAR", "ALTO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch {
                MessageBox.Show("NO HA SELECCIONADO COMBO", "ALTO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //MessageBox.Show("Error " + ex.ToString(), "Alto", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {

        }

        

       
        public void comparaCategorias() 
        {
            for (int x = 0; x < dgvPromo.RowCount; x++)
            {
                dgvPromo[3, x].Value = dgvPromo[0, x].Value.ToString();
            }
            for (int i = 0; i < DgvPedidoprevio.RowCount; i++)
            {
                string categoria = DgvPedidoprevio[3, i].Value.ToString();
                double cantidad = Convert.ToDouble(DgvPedidoprevio[1, i].Value.ToString());
                for (int x = 0; x < dgvPromo.RowCount; x++)
                {
                    if (categoria == dgvPromo[1, x].Value.ToString())
                    {
                        double cantidadPromo = Convert.ToDouble(dgvPromo[3, x].Value.ToString());
                        double cantidadActual = cantidadPromo - cantidad;                       
                        dgvPromo[3, x].Value = cantidadActual;
                       
                        //break;
                    }
                }
            }

            for (int x = 0; x < dgvPromo.RowCount; x++)
            {
                dgvPromo[2, x].Value = false;
            }
                for (int i = 0; i < DgvPedidoprevio.RowCount; i++)
            {
                string categoria = DgvPedidoprevio[3, i].Value.ToString();
                double cantidad = Convert.ToDouble(DgvPedidoprevio[1, i].Value.ToString());
                for (int x = 0; x < dgvPromo.RowCount; x++)
                {
                    if (categoria == dgvPromo[1, x].Value.ToString())
                    {
                        double cantidadPromo = Convert.ToDouble(dgvPromo[3, x].Value.ToString());
                        if (cantidadPromo == 0)
                        {
                            DgvPedidoprevio[5, i].Value = cantidadPromo;
                            DgvPedidoprevio[4, i].Value = true;
                            dgvPromo[2, x].Value = true;
                            //break;
                        }
                        else
                        {
                            DgvPedidoprevio[4, i].Value = false;
                            dgvPromo[2, x].Value = false;
                            DgvPedidoprevio[5, i].Value = cantidadPromo;
                            //break;
                        }
                    }
                }
            }
        }
        public void articulosPromo(string id)
        {
            
                string Articulos = "";
                cmd = new OleDbCommand("SELECT * FROM ArticulosPromos where IdPromo='" + id + "';", conectar);
                OleDbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Articulos += reader[2].ToString() + ":" + reader[3].ToString() + "\n";
                    dgvPromo.Rows.Add(reader[2].ToString(), reader[3].ToString(), false, reader[2].ToString());

                    Button but = new Button();
                    but.FlatStyle = FlatStyle.Flat;
                    but.FlatAppearance.BorderSize = 0;
                    but.Font = new System.Drawing.Font(new FontFamily("Calibri"), 12, FontStyle.Bold);
                    but.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                    but.ForeColor = Color.FromName("RED");
                    but.Size = new System.Drawing.Size(350, 30);
                    but.Text = reader[2].ToString() + " - " + reader[3].ToString();
                    but.Tag = new Tuple<string, double, string>(reader[1].ToString(), Convert.ToDouble(reader[2]), reader[3].ToString());
                    but.Click += new EventHandler(this.filtroBoton);
                    flowLayoutPanel1.Controls.Add(but);
                    //categoria = (sender as Button).Text;
                }
            //MessageBox.Show(Articulos, "La promoción incluye:");
        }
        public bool todoOK()
        {
            bool ok=true;
           
            for (int i = 0; i < dgvPromo.RowCount; i++)
            {
                int cantidad = Convert.ToInt32(dgvPromo[3, i].Value.ToString());
                if (dgvPromo[2, i].Value.ToString() == "False"  || cantidad != 0)
                {
                    ok = false;
                    break;
                }
            }
            if (DgvPedidoprevio.RowCount==0)
            {
                ok = false;
            }
            for (int x = 0; x < DgvPedidoprevio.RowCount; x++)
            {
                if (DgvPedidoprevio[4, x].Value.ToString() == "False")
                {
                    ok = false;
                    break;
                }

            }
            return ok;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                DgvPedidoprevio.Rows.RemoveAt(DgvPedidoprevio.CurrentRow.Index);
                comparaCategorias();
            }
            catch
            {

            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(dgvPromo[2, dgvPromo.CurrentRow.Index].Value.ToString());
        }

        private void DgvPedidoprevio_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                double cantidad = Convert.ToDouble(DgvPedidoprevio[0, DgvPedidoprevio.CurrentRow.Index].Value.ToString());
                comparaCategorias();
            }
            catch 
            {
                MessageBox.Show("Solo puedes introducir números", "Alto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DgvPedidoprevio.Rows[e.RowIndex].Cells[1].Value = "1"; 
                comparaCategorias();
            }
        }

        private void dgvPromo_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            filtrar(dgvPromo[1, dgvPromo.CurrentRow.Index].Value.ToString());            
        }
        public void filtrar(string subcategoria)
        {
            if (subFiltrar != subcategoria)
            {
                Random random = new Random();
                flowLayoutPanel3.Controls.Clear();
                da = new OleDbDataAdapter("select Id,Nombre,Precio,Subcategoria from Inventario WHERE Subcategoria='" + subcategoria + "' ORDER BY Nombre;", conectar);
                ds = new DataSet();
                da.Fill(ds, "Id");
                dataGridView1.DataSource = ds.Tables["Id"];
                foreach (DataRow row in ds.Tables["Id"].Rows)
                {
                    int red = random.Next(256);    // Valores entre 0 y 255 (256 excluido)
                    int green = random.Next(256);
                    int blue = random.Next(256);
                    Color randomColor = Color.FromArgb(red, green, blue);
                    string id = Convert.ToString(row[0]); // Suponiendo que el primer campo es de tipo entero (Id)
                    string nombre = row[1].ToString();
                    string sub = row[3].ToString();
                    //crear boton
                    Button but = new Button();
                    but.FlatStyle = FlatStyle.Flat;
                    but.FlatAppearance.BorderSize = 0;
                    but.Font = new System.Drawing.Font(new FontFamily("Calibri"), 11, FontStyle.Bold);
                    but.BackColor = randomColor;
                    //but.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                    but.ForeColor = Color.FromName("White");
                    but.Size = new System.Drawing.Size(104, 56);
                    but.Text = row[1].ToString();
                    but.Tag = new Tuple<string, string, string>(id, nombre, sub);
                    flowLayoutPanel3.Controls.Add(but);
                    but.Click += new EventHandler(this.botonProductos);
                }
                subFiltrar = subcategoria;
            }
        }
        private void botonProductos(object sender, EventArgs e)
        {
            if ((sender as Button).Tag is Tuple<string, string, string> tupla)
            {
                string id = tupla.Item1;
                string nombre = tupla.Item2;
                string categoria = tupla.Item3;
                DgvPedidoprevio.Rows.Add(id, "1", nombre, categoria, false, "1");
                comparaCategorias();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string id = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
            string nombre = dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString();
            string categoria = dataGridView1[3, dataGridView1.CurrentRow.Index].Value.ToString();
            DgvPedidoprevio.Rows.Add(id, "1", nombre, categoria, false, "1");
            comparaCategorias();
        }
        private void lblCant_Click(object sender, EventArgs e)
        {
            using (frmCantidad ori = new frmCantidad())
            {
                if (ori.ShowDialog() == DialogResult.OK)
                {
                    lblCant.Text = Math.Round(Convert.ToDouble(ori.cantidad)) + "";
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            bool valorCk = checkBox1.Checked;
            flowLayoutPanel2.Visible = valorCk;
            dgvInventario.Visible = !valorCk;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            bool valorCk = checkBox2.Checked;
            flowLayoutPanel1.Visible = valorCk;
            dgvPromo.Visible = !valorCk;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            bool valorCk = checkBox3.Checked;
            flowLayoutPanel3.Visible = valorCk;
            dataGridView1.Visible = !valorCk;
        }
    }
}
