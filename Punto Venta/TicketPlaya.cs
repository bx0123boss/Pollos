// TicketPlaya_NET48.cs  (WinForms .NET Framework 4.8, C# 7.3)
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Tickets80mm
{
    // --------- MODELO ----------
    public class Producto
    {
        public string Nombre { get; set; } = "";
        public decimal Cantidad { get; set; }
        public decimal Total { get; set; } // Importe de la línea
    }

    // --------- NUMEROS -> TEXTO ----------
    public static class NumeroEnTexto
    {
        private static readonly string[] Unidades = {
            "CERO","UNO","DOS","TRES","CUATRO","CINCO","SEIS","SIETE","OCHO","NUEVE",
            "DIEZ","ONCE","DOCE","TRECE","CATORCE","QUINCE","DIECISÉIS","DIECISIETE","DIECIOCHO","DIECINUEVE"
        };
        private static readonly string[] Decenas = {
            "","DIEZ","VEINTE","TREINTA","CUARENTA","CINCUENTA","SESENTA","SETENTA","OCHENTA","NOVENTA"
        };
        private static readonly string[] Centenas = {
            "","CIENTO","DOSCIENTOS","TRESCIENTOS","CUATROCIENTOS",
            "QUINIENTOS","SEISCIENTOS","SETECIENTOS","OCHOCIENTOS","NOVECIENTOS"
        };

        public static string ToPesosEnLetras(int numeroEntero)
        {
            if (numeroEntero == 0) return "CERO PESOS";
            return Convertir(numeroEntero) + " PESOS";
        }

        private static string Convertir(int n)
        {
            if (n < 0) return "MENOS " + Convertir(Math.Abs(n));
            if (n == 0) return "CERO";
            if (n <= 19) return Unidades[n];
            if (n < 100) return DecenasTexto(n);
            if (n == 100) return "CIEN";
            if (n < 1000)
            {
                int c = n / 100; int r = n % 100;
                return (Centenas[c] + (r > 0 ? " " + Convertir(r) : "")).Trim();
            }
            if (n < 1000000)
            {
                int miles = n / 1000; int r = n % 1000;
                string pref = miles == 1 ? "MIL" : Convertir(miles) + " MIL";
                return (pref + (r > 0 ? " " + Convertir(r) : "")).Trim();
            }
            int mill = n / 1000000; int rem = n % 1000000;
            string pre = mill == 1 ? "UN MILLÓN" : Convertir(mill) + " MILLONES";
            return (pre + (rem > 0 ? " " + Convertir(rem) : "")).Trim();
        }

        private static string DecenasTexto(int n)
        {
            int d = n / 10, u = n % 10;
            if (n <= 19) return Unidades[n];
            if (n >= 20 && n < 30)
            {
                if (u == 0) return "VEINTE";
                string suf;
                switch (u)
                {
                    case 2: suf = "DÓS"; break;
                    case 3: suf = "TRÉS"; break;
                    case 6: suf = "SÉIS"; break;
                    default: suf = Unidades[u].ToLower(); break;
                }
                return ("VEINTI" + suf).ToUpperInvariant();
            }
            if (u == 0) return Decenas[d];
            return Decenas[d] + " Y " + Unidades[u];
        }
    }

    // --------- IMPRESION 80mm ----------
    public sealed class TicketPlaya
    {
        private static string B(string s) { return BoldTag + (s ?? ""); }

        // Config ticket 80mm
        private const int Columns = 42;         // ancho lógico (monoespaciado)
        private const int PaperWidth = 315;     // 80mm ≈ 3.15" -> 315 (centésimas de pulgada)
        private const int PaperHeight = 12000;  // alto grande (rollo)

        // Fuente y línea
        private readonly Font _font;
        private readonly Font _fontBold;   // <— NUEVO
        private const string BoldTag = "[B]"; // <— NUEVO

        private readonly int _lineHeightPx;

        // Datos
        private readonly List<Producto> _productos;
        private readonly string[] _encabezado;
        private readonly string[] _pie;
        private readonly string _mesa;
        private readonly string _mesero;
        private readonly int _personas;
        private readonly string _orden;
        private readonly string _folio;
        private readonly string _cajero;
        private readonly DateTime? _apertura;
        private readonly DateTime? _cierre;

        // Render cache
        private readonly string _textoTicket;

        public TicketPlaya(
            List<Producto> productos,
            string mesa,
            string mesero,
            int personas,
            string orden,
            string folio,
            string cajero,
            string[] encabezado,
            string[] pie,
            DateTime? fechaHoraApertura,
            DateTime? fechaHoraCierre,
            string fuente,
            float sizePt)
        {
            _productos = productos ?? new List<Producto>();
            _mesa = mesa ?? "";
            _mesero = mesero ?? "";
            _personas = personas;
            _orden = orden ?? "";
            _folio = folio ?? "";
            _cajero = cajero ?? "";
            _encabezado = encabezado ?? new string[0];
            _pie = pie ?? new string[0];
            _apertura = fechaHoraApertura;
            _cierre = fechaHoraCierre;

            if (string.IsNullOrEmpty(fuente)) fuente = "Courier New";
            if (sizePt <= 0) sizePt = 10f;
            _font = new Font(fuente, sizePt, FontStyle.Regular, GraphicsUnit.Point);
            _fontBold = new Font(_font, FontStyle.Bold);
            _lineHeightPx = (int)Math.Ceiling(_font.SizeInPoints * 1.8);

            _textoTicket = BuildTicketText(); // <- arma todo el contenido aquí
        }

        // Overload simple, como pediste
        public TicketPlaya(List<Producto> productos, string mesa, string mesero)
            : this(productos, mesa, mesero, 1, "", "", "", new string[0], new string[0], null, null, "Courier New", 10f)
        { }

        public void ImprimirComanda(string impresora)
        {
            PrintDocument doc = new PrintDocument();
            if (!string.IsNullOrEmpty(impresora))
                doc.PrinterSettings.PrinterName = impresora;

            doc.DefaultPageSettings.PaperSize = new PaperSize("Ticket80mm", PaperWidth, PaperHeight);
            doc.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);
            doc.PrintController = new StandardPrintController();
            doc.PrintPage += OnPrintPage;

            doc.Print();
            doc.PrintPage -= OnPrintPage;
        }

        public static void ImprimirComanda(TicketPlaya t, string impresora)
        {
            t.ImprimirComanda(impresora);
        }
        private void OnPrintPage(object sender, PrintPageEventArgs e)
        {
            float x = 10f, y = 10f;
            string[] lines = _textoTicket.Replace("\r", "").Split('\n');

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                Font f = _font;

                if (line.StartsWith(BoldTag))
                {
                    f = _fontBold;
                    line = line.Substring(BoldTag.Length); // quita el [B]
                }

                e.Graphics.DrawString(line, f, Brushes.Black, x, y, StringFormat.GenericTypographic);
                y += _lineHeightPx;
            }
            e.HasMorePages = false;
        }

        private static string FormatCantidad(decimal value)
        {
            // Si es entero, sin decimales; si no, hasta 2 decimales (ajusta a 3/4 si lo necesitas)
            if (value == Math.Truncate(value))
                return ((long)value).ToString(System.Globalization.CultureInfo.InvariantCulture);

            return value.ToString("0.##", System.Globalization.CultureInfo.InvariantCulture);
        }

        private string BuildTicketText()
        {
            StringBuilder sb = new StringBuilder();
            CultureInfo mx = CultureInfo.CreateSpecificCulture("es-MX");

            // Encabezado
            for (int i = 0; i < _encabezado.Length; i++)
            {
                string l = _encabezado[i] ?? "";
                bool isBold = false;
                if (l.StartsWith(BoldTag)) { isBold = true; l = l.Substring(BoldTag.Length); }
                string centered = Center(l);
                sb.AppendLine(isBold ? B(centered) : centered);
            }

            sb.AppendLine(new string('=', Columns));
            sb.AppendLine(B(Col2("MESA: " + _mesa, "MESERO: " + _mesero)));
            sb.AppendLine(B(Col2("PERSONAS: " + _personas, string.IsNullOrEmpty(_orden) ? "" : "ORDEN: " + _orden)));
            if (!string.IsNullOrEmpty(_folio)) sb.AppendLine(B("FOLIO: " + _folio));
            if (_apertura.HasValue) sb.AppendLine(B(_apertura.Value.ToString("MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture)));
            if (_cierre.HasValue) sb.AppendLine(B(_cierre.Value.ToString("MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture)));
            if (!string.IsNullOrEmpty(_cajero)) sb.AppendLine(B("CAJERO: " + _cajero));

            sb.AppendLine();
            sb.AppendLine(new string('=', Columns));
            sb.AppendLine(B(Col3("CANT.", "DESCRIPCION", "IMPORTE")));
            // Productos
            for (int i = 0; i < _productos.Count; i++)
            {
                Producto p = _productos[i];
                string cant = FormatCantidad(p.Cantidad);

                string importe = p.Total.ToString("C2", mx);

                List<string> descWrap = WrapText(p.Nombre ?? "", 26);
                if (descWrap.Count == 0) descWrap.Add("");

                sb.AppendLine(Col3(cant, descWrap[0], importe));
                for (int j = 1; j < descWrap.Count; j++)
                    sb.AppendLine(Col3("", descWrap[j], ""));
            }

            // ===== TOTAL con barras del mismo ancho que el texto, alineadas a la derecha =====
            decimal total = _productos.Sum(x => x.Total);
            string totalText = "TOTAL: " + total.ToString("C2", mx);

            // línea de '=' del mismo tamaño que totalText, alineada a la derecha
            string eqRight = Right(new string('=', totalText.Length));

            sb.AppendLine(eqRight);
            sb.AppendLine(Right(totalText));
            sb.AppendLine(eqRight);
            sb.AppendLine(); // espacio


            // ======= SON: en dos renglones centrados =======
            int enteros = (int)Math.Floor(total);
            int centavos = (int)Math.Round((total - enteros) * 100m, 0);

            string textoPesos = NumeroEnTexto.ToPesosEnLetras(enteros).ToUpperInvariant(); // "... PESOS"
            sb.AppendLine(Center("SON: " + textoPesos));                 // línea 1 centrada
            sb.AppendLine(Center(centavos.ToString("D2") + "/100 M.N.")); // línea 2 centrada
            sb.AppendLine(); // espacio

            // ======= Subtotal / IVA =======
            decimal subtotal = Math.Round(total / 1.16m, 2);
            decimal iva = Math.Round(total - subtotal, 2);
            sb.AppendLine(Col2("SUBTOTAL: " + subtotal.ToString("C2", mx),
                               "IVA: " + iva.ToString("C2", mx)));


            // Pie
            if (_pie != null && _pie.Length > 0)
            {
                sb.AppendLine();
                for (int i = 0; i < _pie.Length; i++)
                {
                    string l = _pie[i] ?? "";
                    bool isBold = false;
                    if (l.StartsWith(BoldTag)) { isBold = true; l = l.Substring(BoldTag.Length); }
                    string centered = Center(l);
                    sb.AppendLine(isBold ? B(centered) : centered);
                }
            }

            return sb.ToString(); // <- SIEMPRE devuelve
        }

        // ===== Helpers de maquetado =====
        private static string TrimTo(string s, int width, bool padRight)
        {
            if (s == null) s = "";
            if (s.Length > width) return s.Substring(0, width);
            return padRight ? s.PadRight(width) : s.PadLeft(width);
        }

        private static List<string> WrapText(string text, int width)
        {
            List<string> res = new List<string>();
            if (string.IsNullOrEmpty(text)) return res;

            string[] words = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder line = new StringBuilder();

            for (int i = 0; i < words.Length; i++)
            {
                string w = words[i];
                int extra = line.Length == 0 ? 0 : 1;
                if (line.Length + extra + w.Length > width)
                {
                    res.Add(line.ToString());
                    line.Length = 0;
                }
                if (line.Length > 0) line.Append(' ');
                line.Append(w);
            }
            if (line.Length > 0) res.Add(line.ToString());
            return res;
        }

        private static string Center(string text)
        {
            if (text == null) text = "";
            if (text.Length >= Columns) return text.Substring(0, Columns);
            int pad = (Columns - text.Length) / 2;
            return new string(' ', pad) + text;
        }

        private static string Right(string text)
        {
            if (text == null) text = "";
            if (text.Length >= Columns) return text.Substring(text.Length - Columns);
            return new string(' ', Columns - text.Length) + text;
        }

        // Col2: LEFT | RIGHT
        private static string Col2(string left, string right)
        {
            if (left == null) left = "";
            if (right == null) right = "";

            int space = Columns - right.Length;
            if (space < 0) space = 0;

            left = TrimTo(left, space, true);
            return left + right;
        }

        // Col3: CANT(5) | DESC(26) | IMP(11) = 42
        private static string Col3(string c1, string c2, string c3)
        {
            string s1 = TrimTo(c1 ?? "", 5, true);
            string s2 = TrimTo(c2 ?? "", 26, true);
            string s3 = TrimTo(c3 ?? "", 11, false);
            return s1 + s2 + s3;
        }
    }
}
