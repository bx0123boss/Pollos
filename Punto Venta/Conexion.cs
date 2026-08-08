using System;
using System.Data.SqlClient;

namespace Punto_Venta
{
    public class Conexion
    {
        static string nombrePC = Environment.MachineName;

        public static string CadCon = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Jaeger Soft\FastFood.accdb; Jet OLEDB:Database Password=yolo1234@";
        public static string CadConRestaurantSoft = $@"Server={nombrePC}\SQLEXPRESS;Database=softrestaurant10;Integrated Security=True;MultipleActiveResultSets=True;";
        public static string CadConSql = $@"Server={nombrePC}\SQLEXPRESS;Database=FastFood;Integrated Security=True;MultipleActiveResultSets=True;";
        
        // Variables de configuración globales
        public static string lugar;
        public static string[] datosTicket;
        public static string[] pieDeTicket;
        public static string logoPath = @"C:\Jaeger Soft\logo.jpg";
        public static string Font = "";
        public static string impresora = "print";
        public static string impresora2 = "print";
        public static int MaxChar;
        public static int FontSize;
        public static int MaxCharDescription;
        public static bool ConIva;
        public static bool impresionMediaCarta;
        public static string Whatsapp;
        public static bool Bascula;
        public static bool PuntoB;
        public static string empresa = "nuevo";
        public static bool AbrirTeclado { get; internal set; } = true;

        public static void CargarConfiguracion(int id)
        {
            using (SqlConnection conexion = new SqlConnection(CadConSql))
            {
                conexion.Open();
                string query = "SELECT * FROM Configuracion WHERE Id = @id";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            lugar = reader["Lugar"] != DBNull.Value ? reader["Lugar"].ToString() : "";
                            Font = reader["Font"] != DBNull.Value ? reader["Font"].ToString() : "Arial";
                            impresora = reader["Impresora"] != DBNull.Value ? reader["Impresora"].ToString() : "print";
                            impresora2 = reader["ImpresoraComanda"] != DBNull.Value ? reader["ImpresoraComanda"].ToString() : "print";

                            MaxChar = reader["MaxChar"] != DBNull.Value ? Convert.ToInt32(reader["MaxChar"]) : 40;
                            FontSize = reader["FontSize"] != DBNull.Value ? Convert.ToInt32(reader["FontSize"]) : 10;
                            MaxCharDescription = reader["MaxCharDescription"] != DBNull.Value ? Convert.ToInt32(reader["MaxCharDescription"]) : 20;

                            ConIva = reader["ConIva"] != DBNull.Value && Convert.ToBoolean(reader["ConIva"]);
                            logoPath = reader["LogoPath"] != DBNull.Value ? reader["LogoPath"].ToString() : @"C:\Jaeger Soft\logo.jpg";

                            string datos = reader["DatosTicket"] != DBNull.Value ? reader["DatosTicket"].ToString() : "";
                            datosTicket = datos.Split('|');

                            string pie = reader["PieDeTicket"] != DBNull.Value ? reader["PieDeTicket"].ToString() : "";
                            pieDeTicket = pie.Split('|');

                            impresionMediaCarta = reader["MediaCarta"] != DBNull.Value && Convert.ToBoolean(reader["MediaCarta"]);
                            Whatsapp = reader["Whatsapp"] != DBNull.Value ? reader["Whatsapp"].ToString() : "";
                            Bascula = reader["Bascula"] != DBNull.Value && Convert.ToBoolean(reader["Bascula"]);
                            PuntoB = reader["PuntoB"] != DBNull.Value && Convert.ToBoolean(reader["PuntoB"]);
                        }
                    }
                }
            }
        }
    }
}