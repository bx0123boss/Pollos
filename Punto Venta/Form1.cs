using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Net.Mail;
using System.Windows.Forms;
using Tickets80mm;

namespace Punto_Venta
{
    public partial class Form1 : Form
    {
        int idMesero = 0;
        string usuario = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            entrar();
        }
        public String Autentica()
        {
            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();
                using (SqlCommand cmd = new SqlCommand("select IdUsuario,Usuario,TipoUsuario from Usuarios where Usuario='" + txtUser.Text + "' AND Contraseña='" + txtContraseña.Text + "';", conectar))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        idMesero = Convert.ToInt32(reader["IdUsuario"].ToString());
                        usuario = reader["Usuario"].ToString();
                        return Convert.ToString(reader["TipoUsuario"].ToString());

                    }
                    return "ERROR";
                }
            }
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.facebook.com/jaegersoft/");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
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
            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();
                DataTable dt = new DataTable();
                using (SqlCommand cmd = new SqlCommand("SELECT IdUsuario, Usuario FROM Usuarios;", conectar))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                    txtUser.DisplayMember = "Usuario";
                    txtUser.ValueMember = "Id";
                    txtUser.DataSource = dt;
                    txtUser.Text = "";
                }
            }

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
            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();
                using (SqlCommand cmd = new SqlCommand("select * from inicio where id=1;", conectar))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (Convert.ToString(reader["inicio"].ToString()) == "0")
                        {
                            string aut = Autentica();
                            if (aut != "ERROR")
                            {
                                frmAbrirCaja caja = new frmAbrirCaja();
                                caja.usuario = aut;
                                caja.id = idMesero;
                                caja.nombre = usuario;
                                caja.ShowDialog();
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
            string To = "";  //a quien vamos a enviar el mail
            string Message;  //mensaje
            string Subject; //asunto
            List<string> Archivo = new List<string>(); //lista de archivos a enviar
            string DE = "bran.7594@gmail.com"; //nuestro usuario de smtp
            string PASS = "1"; //nuestro password de smtp

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

                //return false;

            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            string[] encabezados = new string[] {   "        TERESA REYES HERNANDEZ", 
                                                    "  Av. Cuauhtémoc No. 501 Col. Centro",
                                                    "   Tels. 231 31 2 0176 y 231 31 223 33", 
                                                    "             Whatsapp 222 586 60 60", 
                                                    "           Teziutlán, Pue. C.P. 73800", 
                                                    "             R.F.C. REHT4908096N1" };
            string[] pieDePagina = new string[] { "ESTO NO ES UN COMPROBANTE FISCAL", "      **GRACIAS POR SU COMPRA**" };
            string logoPath = @"C:\Jaeger Soft\logo.jpg";

            List<Producto> productos = new List<Producto>
            {
                new Producto { Nombre = "VR38952 ", Cantidad = 0, PrecioUnitario = 10.50, Total = 12.18 },
                new Producto { Nombre = "VR38953", Cantidad = 0, PrecioUnitario = 10.50, Total = 12.18 },
                new Producto { Nombre = "VR38954", Cantidad = 0, PrecioUnitario = 10.50, Total = 12.18 },
                new Producto { Nombre = "VR38955", Cantidad = 0, PrecioUnitario = 10.50, Total = 12.18 },
            };

            string folio = "12345";
            string mesa = "Mesa 12";
            string mesero = "Tilin";
            double total = 48.72;
            Dictionary<string, double> _totales = new Dictionary<string, double>();
            _totales.Add("Total", total);
            _totales.Add("Tarjeta", total/2);
            _totales.Add("Retiros", total / 1.20);
            _totales.Add("Efectivo", 50);
            _totales.Add("Cambio", 48.72-50);

            //TicketPrinter ticketPrinter = new TicketPrinter(encabezados, pieDePagina, logoPath, productos, folio, mesa, mesero, total, true, _totales);

            //ticketPrinter.ImprimirTicket();
            TicketPrinter ticketPrinter = new TicketPrinter(productos, mesa, mesero);
            //ticketPrinter.ImprimirComanda();
            // Imprimir en una segunda impresora (especificar el nombre de la impresora)
            // ticketPrinter.ImprimirTicket("Nombre de la segunda impresora");

            var productos2 = new List<Tickets80mm.Producto>
            {
                new Tickets80mm.Producto { Nombre = "JUGO DE NARANJA",     Cantidad = 2, Total = 76m },
                new Tickets80mm.Producto { Nombre = "COPA DE AGUA ESPECIA", Cantidad = 2, Total = 112m }
            };

            string[] encabezado =
            {
        "[B]PLAYA HERMOSA",
        "[B]DANIEL BAEZ TEMIX",
        "RFC: BATD931010K37",
        "SIMON BOLIVAR 10372 VERACRUZ VERACRUZ MEXICO CP",
        "91918",
        "LUGAR DE EXPEDICION",
        "TEL:"
    };

            string[] pie =
            {
        "[B]ESTE NO ES UN COMPROBANTE FISCAL",
        "[B]PROPINA NO INCLUIDA",
        "*** SOFT RESTAURANT V10 ***"
    };

            // CONSTRUCTOR COMPLETO (nota los sufijos: 10f = float)
            var ticket = new TicketPlaya(
                productos2,
                "7A",            // mesa
                "BRANDON",       // mesero
                4,               // personas (int)
                "2",             // orden
                "30954",         // folio
                "DANIEL",        // cajero
                encabezado,
                pie,
                DateTime.Now.AddHours(-2),    // apertura
                DateTime.Now,    // cierre
                "Courier New",   // fuente monoespaciada
                10f              // tamaño en puntos (float)
            );

            // Imprimir (pasa el nombre exacto si quieres fijar la impresora)
            ticket.ImprimirComanda("print");
        }
    }
}
