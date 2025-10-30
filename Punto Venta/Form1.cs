using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net.Mail;
using System.Windows.Forms;
using Tickets80mm;
using static ComandasReportPdf;

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
        }

        private void label2_Click(object sender, EventArgs e)
        {
            var hdr = new ReportHeader
            {
                Empresa = "WINGS LAS ALITAS",
                Rfc = "FERIA",
                Direccion = "FERIA TEZIUTLAN PUEBLA MEXICO  CP",
                CiudadCpTel = "FERIA, Tel.",
                Desde = new System.DateTime(2025, 7, 31, 6, 0, 0),
                Hasta = new System.DateTime(2025, 8, 1, 5, 59, 59),
                MeseroFiltro = "(TODOS)",
                FooterNote = "SoftRestaurant® Copyright National Soft "
            };
            #region consulta datos
            var desde = DateTime.Parse("01-01-2025");
            var hasta = DateTime.Parse("01-01-2025");

            var data = ConsultarComandas(desde, hasta);
            #endregion
            using (var sfd = new SaveFileDialog { Filter = "PDF (*.pdf)|*.pdf", FileName = "ReporteComandas.pdf" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    ComandasReportPdf.Generate(hdr, data, sfd.FileName);
                    MessageBox.Show("PDF generado.");
                }
            }
        }

        public static List<ComandasReportPdf.ComandaRow> ConsultarComandas(DateTime desde, DateTime hasta)
        {
            var lista = new List<ComandasReportPdf.ComandaRow>();

            const string sql = @"
                SELECT 
                    D.folio                              AS FolioComanda,
                    D.orden                              AS Orden,
                    D.fecha                              AS FechaApertura,
                    D.cierre                             AS FechaCierre,
                    RIGHT('0'+CAST(D.idmesero AS varchar(2)),2) AS MeseroCuenta, 
                    RIGHT('0'+CAST(ISNULL(D.idmesero,D.idmesero) AS varchar(2)),2) AS MeseroProd, 
                    CAST(C.cantidad AS decimal(10,3))    AS Cantidad,
                    C.hora                                AS FechaCaptura,      
                    CONCAT(A.idproducto,'-',A.descripcion) AS Producto,
                    CAST(C.precio AS decimal(10,2))      AS Importe,
                    CAST(ISNULL(C.descuento,0) AS decimal(10,2)) AS Descuento  
                FROM productos A
                JOIN cheqdet  C ON A.idproducto = C.idproducto
                JOIN cheques  D ON C.foliodet   = D.folio
                WHERE D.fecha >= @Desde AND D.fecha < @Hasta;";

            using (var cn = new SqlConnection(Conexion.CadConRestaurantSoft))
            using (var cmd = new SqlCommand(sql, cn))
            {
                var start = desde.Date;
                var next = hasta.Date.AddDays(1);

                cmd.Parameters.Add("@Desde", SqlDbType.DateTime).Value = start;
                cmd.Parameters.Add("@Hasta", SqlDbType.DateTime).Value = next;

                cn.Open();
                using (var rd = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    // Construir mapa nombre→ordinal (case-insensitive)
                    var ord = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                    for (int i = 0; i < rd.FieldCount; i++)
                        ord[rd.GetName(i)] = i;

                    // Función para obtener ordinal validando nombre y dando error claro
                    int Need(string name)
                    {
                        if (!ord.TryGetValue(name, out var idx))
                        {
                            // Mensaje útil con columnas disponibles
                            var cols = string.Join(", ", ord.Keys);
                            throw new IndexOutOfRangeException(
                                $"La columna '{name}' no existe en el lector. Columnas disponibles: {cols}");
                        }
                        return idx;
                    }

                    // Resolver una vez todos los índices
                    int iFolioComanda = Need("FolioComanda");
                    int iOrden = Need("Orden");
                    int iFechaApertura = Need("FechaApertura");
                    int iFechaCierre = Need("FechaCierre");
                    int iMeseroCuenta = Need("MeseroCuenta");
                    int iMeseroProd = Need("MeseroProd");
                    int iCantidad = Need("Cantidad");
                    int iFechaCaptura = Need("FechaCaptura");
                    int iProducto = Need("Producto");
                    int iImporte = Need("Importe");
                    int iDescuento = Need("Descuento");

                    while (rd.Read())
                    {
                        var row = new ComandasReportPdf.ComandaRow
                        {
                            FolioComanda = 0l,
                            FolioCuenta = rd.IsDBNull(iFolioComanda) ? 0L : Convert.ToInt64(rd.GetValue(iFolioComanda)),
                            Orden = rd.IsDBNull(iOrden) ? 0 : Convert.ToInt32(rd.GetValue(iOrden)),
                            FechaApertura = rd.IsDBNull(iFechaApertura) ? DateTime.MinValue : rd.GetDateTime(iFechaApertura),
                            FechaCierre = rd.IsDBNull(iFechaCierre) ? DateTime.MinValue : rd.GetDateTime(iFechaCierre),
                            MeseroCuenta = rd.IsDBNull(iMeseroCuenta) ? "" : rd.GetString(iMeseroCuenta),
                            MeseroProd = rd.IsDBNull(iMeseroProd) ? "" : rd.GetString(iMeseroProd),
                            Cantidad = rd.IsDBNull(iCantidad) ? 0m : Convert.ToDecimal(rd.GetValue(iCantidad), CultureInfo.InvariantCulture),
                            FechaCaptura = rd.IsDBNull(iFechaCaptura) ? DateTime.MinValue : rd.GetDateTime(iFechaCaptura),
                            Producto = rd.IsDBNull(iProducto) ? "" : rd.GetString(iProducto),
                            Importe = rd.IsDBNull(iImporte) ? 0m : Convert.ToDecimal(rd.GetValue(iImporte), CultureInfo.InvariantCulture),
                            Descuento = rd.IsDBNull(iDescuento) ? 0m : Convert.ToDecimal(rd.GetValue(iDescuento), CultureInfo.InvariantCulture)
                        };

                        lista.Add(row);
                    }
                }
            }

            return lista;
        }

    }
}
