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
    public partial class frmCategorias : Form
    {
        private DataSet ds;
        OleDbConnection conectar = new OleDbConnection(Conexion.CadCon);
        OleDbDataAdapter da;
        OleDbCommand cmd;
        public string tipo;
        public frmCategorias()
        {
            InitializeComponent();
        }

        private void frmCategorias_Load(object sender, EventArgs e)
        {
            this.Text = tipo;
            ds = new DataSet();
            conectar.Open();
            da = new OleDbDataAdapter("select * from "+tipo+";", conectar);
            da.Fill(ds, "Id");
            dataGridView1.DataSource = ds.Tables["Id"];
            dataGridView1.Columns[0].Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmAgregarCategorias CAT = new frmAgregarCategorias();
            CAT.tipo = tipo;
            CAT.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("¿Estas seguro de elimiar la Categoria?", "Alto!", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                cmd = new OleDbCommand("delete from Categorias where Id=" + dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString() + ";", conectar);
                cmd.ExecuteNonQuery();
                cmd = new OleDbCommand("update Inventario set Categoria='SIN CATEGORIA' where Categoria='" + dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString() + "';", conectar);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Se ha eliminado la " + tipo + " con exito", "ELIMINADO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ds = new DataSet();
                da = new OleDbDataAdapter("select * from " + tipo + ";", conectar);
                da.Fill(ds, "Id");
                dataGridView1.DataSource = ds.Tables["Id"];
                dataGridView1.Columns[0].Visible = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmAgregarCategorias cat = new frmAgregarCategorias();
            cat.tipo = tipo;
            cat.id = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
            //cat.nombre = dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString();
            cat.txtNombre.Text = dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString();
            cat.button2.BackColor = System.Drawing.ColorTranslator.FromHtml("#" + dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString());
            cat.button2.ForeColor = Color.FromName(dataGridView1[3, dataGridView1.CurrentRow.Index].Value.ToString());
            if (dataGridView1[3, dataGridView1.CurrentRow.Index].Value.ToString()=="Black")            
                cat.radioButton1.Checked = true;            
            else
                cat.radioButton2.Checked = true; 
            cat.button1.Text = "Editar";
            cat.Show();
            this.Close();
        }
    }
}
