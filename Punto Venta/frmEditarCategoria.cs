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
    public partial class frmEditarCategoria : Form
    {
        public string id = "",nombre="";
        OleDbConnection conectar = new OleDbConnection(Conexion.CadCon);
        OleDbCommand cmd;
        public frmEditarCategoria()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text == "" || txtNombre.Text == nombre)
            {
                MessageBox.Show("Nombre invalido", "Editar Categoria", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                bool existe = false;
                cmd = new OleDbCommand("select Nombre from Categorias where Nombre='" + txtNombre.Text + "';", conectar);
                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    existe = true;
                }
                if (existe)
                {
                    MessageBox.Show("Existe una categoria similar, favor de verificar", "Editar Categoria", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {                   
                    cmd = new OleDbCommand("update Categorias set Nombre='" + txtNombre.Text + "' where Id="+id +";", conectar);
                    cmd.ExecuteNonQuery();
                    cmd = new OleDbCommand("update Inventario set Categoria='" + txtNombre.Text + "' where Categoria='" + nombre +"';", conectar);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Se ha editado la categoria con exito", "EXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmCategorias apart = new frmCategorias();
                    apart.Show();
                    this.Close();
                }
            }
        }

        private void frmEditarCategoria_Load(object sender, EventArgs e)
        {
            conectar.Open();
        }
    }
}
