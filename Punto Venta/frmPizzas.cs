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
    public partial class frmPizzas : Form
    {

        private DataSet ds;
        OleDbConnection conectar = new OleDbConnection(Conexion.CadCon);
        OleDbDataAdapter da;
        OleDbCommand cmd;
        string tamaño, masa;
        public string tipo;
        public string id1 { get; set; }
        public string id2 { get; set; }
        public string idMasa { get; set; }
        public string nombre { get; set; }
        public double precio { get; set; }
        public string comentarios{ get; set; }
        string[] reservadas = { "MITAD", "PIZZA", "DELGADA", "NORMAL", "GRUESA", "CHICA","MEDIANA","GRANDE","EXTRA GRANDE", "FAMILIAR" ,"COMPLETA"};
        int pizzas = 0;
        
        public frmPizzas()
        {
            InitializeComponent();
        }

        private void frmPizzas_Load(object sender, EventArgs e)
        {
            conectar.Open();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            masa = "DELGADA";
            flpMasa.Visible = false;
            flpTamaño.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            masa = "NORMAL";
            flpMasa.Visible = false;
            flpTamaño.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            masa = "GRUESA";
            flpMasa.Visible = false;
            flpTamaño.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tamaño = "CHICA";
            mostrarPizzas();

        }
        public void mostrarPizzas()
        {
            cmd = new OleDbCommand("select id,Nombre from inventario where Nombre='MASA " + tamaño + " " + masa + "';", conectar);
            OleDbDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                idMasa = (reader[0].ToString());
            }
            flpPizzas.Controls.Clear();
            if (tipo == "MITAD")
            {
                if (tamaño == "GRANDE")
                    cmd = new OleDbCommand("SELECT Nombre, Id FROM Inventario where Categoria='MITADES PIZZA' and Nombre like '%" + tamaño + "%' AND Nombre NOT LIKE '%EXTRA GRANDE%'  order by Nombre;", conectar);
                else
                    cmd = new OleDbCommand("SELECT Nombre, Id FROM Inventario where Categoria='MITADES PIZZA' and Nombre like '%" + tamaño + "%' order by Nombre;", conectar);
            }
            else
            {
                if (tamaño == "GRANDE")
                    cmd = new OleDbCommand("SELECT Nombre, Id FROM Inventario where Categoria='PIZZAS' and Nombre like '%" + tamaño + "%' AND Nombre NOT LIKE '%EXTRA GRANDE%' order by Nombre;", conectar);
                else
                    cmd = new OleDbCommand("SELECT Nombre, Id FROM Inventario where Categoria='PIZZAS' and Nombre like '%" + tamaño + "%' order by Nombre;", conectar);
            }
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Button but = new Button();
                but.FlatStyle = FlatStyle.Flat;
                but.FlatAppearance.BorderSize = 0;
                but.Font = new System.Drawing.Font(new FontFamily("Calibri"), 12, FontStyle.Bold);
                but.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff8000");
                but.ForeColor = Color.FromName("White");
                but.Size = new System.Drawing.Size(135, 70);
                string nombre = reader[0].ToString();
                for (int i = 0; i < reservadas.Length; i++)
                {
                    nombre = nombre.Replace(reservadas[i], string.Empty);
                }
                but.Text = nombre;
                but.Name = reader[1].ToString();
                but.Click += new EventHandler(this.Myevent);
                flpPizzas.Controls.Add(but);
            }

            flpExtras.Controls.Clear();
            if (tamaño == "GRANDE")
                cmd = new OleDbCommand("SELECT Nombre, Id FROM Inventario where Categoria='EXTRAS' and Nombre like '%" + tamaño + "%' AND Nombre NOT LIKE '%EXTRA GRANDE%' order by Nombre;", conectar);
            else
                cmd = new OleDbCommand("SELECT Nombre, Id FROM Inventario where Categoria='EXTRAS' and Nombre like '%" + tamaño + "%' order by Nombre;", conectar);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Button but = new Button();
                but.FlatStyle = FlatStyle.Flat;
                but.FlatAppearance.BorderSize = 0;
                but.Font = new System.Drawing.Font(new FontFamily("Calibri"), 12, FontStyle.Bold);
                but.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff8000");
                but.ForeColor = Color.FromName("White");
                but.Size = new System.Drawing.Size(135, 70);
                nombre = reader[0].ToString();
                for (int i = 0; i < reservadas.Length; i++)
                {
                    nombre = nombre.Replace(reservadas[i], string.Empty);
                }
                but.Text = nombre;
                but.Name = reader[1].ToString();
                but.Click += new EventHandler(this.Extras);
                flpExtras.Controls.Add(but);
            }
            flpTamaño.Visible = false;
            flpExtras.Visible = true;
            flpPizzas.Visible = true;
            lblPizza.Text = tamaño + " " + masa;
        }
        private void Myevent(object sender, EventArgs e)
        {
            //MessageBox.Show((sender as Button).Name.ToString());
            if (tipo == "COMPLETAS")
            {
                cmd = new OleDbCommand("select id,Nombre,Precio,Categoria,Comanda from inventario where Id=" + (sender as Button).Name + ";", conectar);
                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    double pre = (Convert.ToDouble(reader[2].ToString()));
                    DgvPedidoprevio.Rows.Add(Convert.ToString(reader[0].ToString()), "1", "PIZZA" + Convert.ToString(reader[1].ToString()).Replace("COMPLETA", string.Empty) + " MASA " + masa, Math.Round(pre, 2), "");
                }
            }
            else if (pizzas < 2)
            {
                cmd = new OleDbCommand("select id,Nombre,Precio,Categoria,Comanda from inventario where Id=" + (sender as Button).Name + ";", conectar);
                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    double pre = (Convert.ToDouble(reader[2].ToString()));
                    DgvPedidoprevio.Rows.Add(Convert.ToString(reader[0].ToString()), "1", Convert.ToString(reader[1].ToString()), Math.Round(pre, 2), "");
                    pizzas++;
                }
            }
            else
                MessageBox.Show("Solo se pueden poner 2 mitades de pizza", "ALTO!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }

        private void Extras(object sender, EventArgs e)
        {
            
                cmd = new OleDbCommand("select id,Nombre,Precio,Categoria,Comanda from inventario where Id=" + (sender as Button).Name + ";", conectar);
                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    double pre = (Convert.ToDouble(reader[2].ToString()));
                    dataGridView1.Rows.Add(Convert.ToString(reader[0].ToString()), "1", Convert.ToString(reader[1].ToString()), Math.Round(pre, 2), Math.Round(pre, 2), "", "EXTRA");
                }           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            tamaño = "MEDIANA";
            mostrarPizzas();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            tamaño = "GRANDE";
            mostrarPizzas();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            tamaño = "EXTRA GRANDE";
            mostrarPizzas();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            tamaño = "FAMILIAR";
            mostrarPizzas();
        }

        private void BtnEntregar_Click(object sender, EventArgs e)
        {
            if ( tipo == "COMPLETAS")
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else if (pizzas==2)
            {
                id1 = DgvPedidoprevio[0, 0].Value.ToString();
                id2 = DgvPedidoprevio[0, 1].Value.ToString();
                string n1 = DgvPedidoprevio[2, 0].Value.ToString();
                string n2 = DgvPedidoprevio[2, 1].Value.ToString();
                nombre = "PIZZA";
                string[] subs = n1.Split(' ');
                for (int i = 0; i < reservadas.Length; i++)
                {
                    n1=n1.Replace(reservadas[i], string.Empty);
                    n2 = n2.Replace(reservadas[i], string.Empty);
                }
                nombre += n1 + "-" + n2 + "MASA "+lblPizza.Text;
                double precio1 = Convert.ToDouble(DgvPedidoprevio[3, 0].Value.ToString());
                double precio2 = Convert.ToDouble(DgvPedidoprevio[3, 1].Value.ToString());
                if (precio1>=precio2)                
                    precio = precio1;
                else
                    precio = precio2;
                comentarios = DgvPedidoprevio[4, 0].Value.ToString() +" "+ DgvPedidoprevio[4, 1].Value.ToString();
                //MessageBox.Show(nombre);
                this.DialogResult = System.Windows.Forms.DialogResult.OK;                
            }
            else
                MessageBox.Show("NO HAY 2 MITADES DE PIZZA", "ALTO!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            tipo = "MITAD";
            flpMasa.Visible = true;
            flowLayoutPanel1.Visible = false;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            tipo = "COMPLETAS";
            flpMasa.Visible = true;
            flowLayoutPanel1.Visible = false;
        }

        private void DgvPedidoprevio_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DgvPedidoprevio_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            pizzas--;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (DgvPedidoprevio.RowCount > 0)
            {
                DgvPedidoprevio.Rows.RemoveAt(DgvPedidoprevio.CurrentRow.Index);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount > 0)
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }
        
        }
    }

