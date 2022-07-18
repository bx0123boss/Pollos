using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data.OleDb;
using System.IO;
using System.Diagnostics;
using System.Net.Mail;

namespace Punto_Venta
{
    public partial class Form1 : Form
    {
        //MySqlCommand cmd;
        //MySqlDataAdapter ad;
        OleDbConnection conectar = new OleDbConnection(Conexion.CadCon);  
        OleDbCommand cmd;
        int idMesero = 0;
        string usuario = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            

            //else
            //{
            //    frmPrincipal principal = new frmPrincipal();
            //    principal.Show();
            //    this.Hide();
            //}
            //else
            //{
            //    usuario = "administrador";
            //    frmPrincipal principal = new frmPrincipal();
            //    principal.lblUser.Text = usuario;
            //    principal.Show();
            //    this.Hide();
            //}
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            entrar();
        }
        public String Autentica()
        {
            cmd = new OleDbCommand("select Id,Usuario,TipoUsuario from Usuarios where Usuario='" + txtUser.Text + "' AND Contraseña='" + txtContraseña.Text + "';", conectar);
            OleDbDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                idMesero = Convert.ToInt32(reader[0].ToString());
                usuario = reader[1].ToString();
                return Convert.ToString(reader[2].ToString());
                
            }
            else
            {
                return "ERROR";
            }
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.facebook.com/jaegersoft/"); 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Conexion.obtenerConexion();
            try
            {
                this.BackgroundImage = Image.FromFile("C:\\Jaeger Soft\\w1.jpg");
                //Pass the filepath and filename to the StreamWriter Constructor
                StreamWriter sw = new StreamWriter(@"MODIFICACION.txt");
                //Write a line of text
                sw.WriteLine(DateTime.Now.ToShortDateString() + " ");
                //Write a second line of text
                sw.WriteLine(DateTime.Now.ToShortTimeString());
                //Close the file
                sw.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
            conectar.Open();
            DataTable dt = new DataTable();
            cmd = new OleDbCommand("Select Id,Usuario from Usuarios;", conectar);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            da.Fill(dt);
            txtUser.DisplayMember = "Usuario";
            txtUser.ValueMember = "Id";
            txtUser.DataSource = dt;
            txtUser.Text = "";
        }

        private void label1_Click(object sender, EventArgs e)
        {
            frmInventario act = new frmInventario();
            act.Show();
            this.Hide();
        }

        private void txtContraseña_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                entrar();
            }
        }
        public void entrar()
        {


            if (txtUser.Text == "" && txtContraseña.Text == "@TUGFA")
            {
                frmPrincipal principal = new frmPrincipal();
                principal.Show();
                this.Hide();
            }
            else
            {
                cmd = new OleDbCommand("select * from inicio where id=1;", conectar);
                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    if (Convert.ToString(reader[1].ToString()) == "0")
                    {
                        string aut = Autentica();
                        if (aut != "ERROR")
                        {
                            frmAbrirCaja caja = new frmAbrirCaja();
                            caja.usuario = aut;
                            caja.id = idMesero;
                            caja.nombre = usuario;
                            caja.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("El usuario y/o contraseña no son valids,\nFavor de introducirlas nuevamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtUser.Text = "";
                            txtContraseña.Clear();
                            txtContraseña.Focus();
                        }
                    }
                    else
                    {
                        string aut = Autentica();
                        if (aut != "ERROR")
                        {
                            frmPrincipal principal = new frmPrincipal();
                            principal.id = idMesero;
                            principal.lblUser.Text = aut;
                            principal.usuario = usuario;
                            principal.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("El usuario y/o contraseña no son valids,\nFavor de introducirlas nuevamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtUser.Text = "";
                            txtContraseña.Clear();
                            txtContraseña.Focus();
                        }
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //creamos nuestra lista de archivos a enviar
            List<string> lstArchivos = new List<string>();
            lstArchivos.Add("C:/Jaeger Resp/r.pdf");
            //lstArchivos.Add("c:/archivo2.txt");
            lstArchivos.Add("C:/Jaeger Resp/reporte.pdf");


            //creamos nuestro objeto de la clase que hicimos
            Mail oMail = new Mail("bran.7594@gmail.com", "conejos_azul@hotmail.com",
                                "HOLIWIS", "un mensaje bien chevere", lstArchivos);

            //y enviamos
            if (oMail.enviaMail())
            {
                MessageBox.Show("se envio el mail");

            }
            else
            {
                MessageBox.Show("no se envio el mail: " + oMail.error);

            }
        }
        class Mail
        {

            string From = ""; //de quien procede, puede ser un alias
            string To= "";  //a quien vamos a enviar el mail
            string Message;  //mensaje
            string Subject; //asunto
            List<string> Archivo = new List<string>(); //lista de archivos a enviar
            string DE = "bran.7594@gmail.com"; //nuestro usuario de smtp
            string PASS = "Br759400"; //nuestro password de smtp

            System.Net.Mail.MailMessage Email;

            public string error = "";

            /// <summary>
            /// constructor
            /// </summary>
            /// <param name="FROM">Procedencia</param>
            /// <param name="Para">Mail al cual se enviara</param>
            /// <param name="Mensaje">Mensaje del mail</param>
            /// <param name="Asunto">Asunto del mail</param>
            /// <param name="ArchivoPedido_">Archivo a adjuntar, no es obligatorio</param>
            public Mail(string FROM, string Para, string Mensaje, string Asunto, List<string> ArchivoPedido_ = null)
            {
                From = FROM;
                To = Para;
                Message = Mensaje;
                Subject = Asunto;
                Archivo = ArchivoPedido_;

            }

            /// <summary>
            /// metodo que envia el mail
            /// </summary>
            /// <returns></returns>
            public bool enviaMail()
            {

                //una validación básica
                if (To.Trim().Equals("") || Message.Trim().Equals("") || Subject.Trim().Equals(""))
                {
                    error = "El mail, el asunto y el mensaje son obligatorios";
                    return false;
                }

                //aqui comenzamos el proceso
                //comienza-------------------------------------------------------------------------
                try
                {
                    //creamos un objeto tipo MailMessage
                    //este objeto recibe el sujeto o persona que envia el mail,
                    //la direccion de procedencia, el asunto y el mensaje
                    Email = new System.Net.Mail.MailMessage(From, To, Subject, Message);

                    //si viene archivo a adjuntar
                    //realizamos un recorrido por todos los adjuntos enviados en la lista
                    //la lista se llena con direcciones fisicas, por ejemplo: c:/pato.txt
                    if (Archivo != null)
                    {
                        //agregado de archivo
                        foreach (string archivo in Archivo)
                        {
                            //comprobamos si existe el archivo y lo agregamos a los adjuntos
                            if (System.IO.File.Exists(@archivo))
                                Email.Attachments.Add(new Attachment(@archivo));

                        }
                    }

                    Email.IsBodyHtml = true; //definimos si el contenido sera html
                    Email.From = new MailAddress(From); //definimos la direccion de procedencia

                    //aqui creamos un objeto tipo SmtpClient el cual recibe el servidor que utilizaremos como smtp
                    //en este caso me colgare de gmail
                    System.Net.Mail.SmtpClient smtpMail = new System.Net.Mail.SmtpClient("smtp.gmail.com");

                    smtpMail.EnableSsl = false;//le definimos si es conexión ssl
                    smtpMail.UseDefaultCredentials = false; //le decimos que no utilice la credencial por defecto
                    smtpMail.Host = "smtp.gmail.com"; //agregamos el servidor smtp
                    smtpMail.Port = 465; //le asignamos el puerto, en este caso gmail utiliza el 465
                    smtpMail.Credentials = new System.Net.NetworkCredential(DE, PASS); //agregamos nuestro usuario y pass de gmail

                    //enviamos el mail
                    smtpMail.Send(Email);

                    //eliminamos el objeto
                    smtpMail.Dispose();

                    //regresamos true
                    return true;
                }
                catch (Exception ex)
                {
                    //si ocurre un error regresamos false y el error
                    error = "Ocurrio un error: " + ex.Message;
                    return false;
                }

                return false;

            }
        }
    }
}
