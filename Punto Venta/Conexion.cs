using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace Punto_Venta
{
    class Conexion
    {
        //funcional con main
        public static string CadCon = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Jaeger Soft\FastFood.accdb; Jet OLEDB:Database Password=yolo1234@";

        //public static string CadConSql = @"Server=DESKTOP-UCR1IUV\SQLEXPRESS;Database=FastFood;Integrated Security=True;";
        public static string CadConSql = @"Server=BRANDON-PC\SQLEXPRESS;Database=FastFood;Integrated Security=True;";
        //public static string CadCon = @"Server=localhost;Database=FastFood;User Id=sa;Password=yolo1234@;";
        //public static string CadCon = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=G:\Mi unidad\JS\FastFood.accdb; Jet OLEDB:Database Password=yolo1234@";
        //public static string CadCon = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Jaeger Soft\FastFood.accdb";
        //public static string CadCon = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=\\107.180.55.14\JS\FastFood.accdb;Jet OLEDB:System Database=system.mdw;";
        //public static string CadCon = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=\\18.222.246.68\rts\FastFood.accdb;Jet OLEDB:System Database=system.mdw; User ID=Brandon;Password=75941232;";
        public static string lugar = "COCINA";
        //public static string lugar = "TERRAZA";

        //DATOS DEL TICKET
        //CERQUITA DEL CIELO
        //public static string empresa = "CERQUITA";
        //public static string[] datosTicket = new string[] { "LAURA JUÁREZ PÉREZ", "RFC JUPL641210AQ0", "Calle 1° de Mayo s/n", "Colonia Tepepan, Chignautla, Puebla ", "CP73950 Régimen de Incorporación fiscal " };
        //public static string[] pieDeTicket = new string[] { "   *GRACIAS POR SU PREFERENCIA*","Si desea factura envíenos sus datos a email restaurantecerquitadelcielo@hotmail.com" ,"            Visitanos en Facebook:", "     facebook.com/cerquitadelcielotez" };
        //public static string Font = "";
        //public static string impresora = "print";
        //public static string impresora2 = "print2";


        //public static string empresa = "CAZADORES";
        public static string empresa = "CERQUITA";
        public static string[] datosTicket = new string[] { "PIZZAS ANGELOTTI", "", "", "" };
        public static string[] pieDeTicket = new string[] { "   *GRACIAS POR SU PREFERENCIA*", "            Visitanos en Facebook:", "Pizzas Angelotti" };
        public static string Font = "";
        public static string impresora = "print";
        public static string impresora2 = "print";
    }
}
