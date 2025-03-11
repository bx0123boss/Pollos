using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;

public class TicketPrinter
{
    private string[] _encabezados;
    private string[] _pieDePagina;
    private string _logoPath;
    private List<Producto> _productos;
    private string _folio;
    private string _mesa;
    private string _mesero;
    private double _total;

    public TicketPrinter(string[] encabezados, string[] pieDePagina, string logoPath, List<Producto> productos, string folio, string mesa, string mesero, double total)
    {
        _encabezados = encabezados;
        _pieDePagina = pieDePagina;
        _logoPath = logoPath;
        _productos = productos;
        _folio = folio;
        _mesa = mesa;
        _mesero = mesero;
        _total = total;
    }

    public void ImprimirTicket(string printerName = null)
    {
        PrintDocument pd = new PrintDocument();
        if (!string.IsNullOrEmpty(printerName))
        {
            pd.PrinterSettings.PrinterName = printerName;
        }
        pd.PrintPage += new PrintPageEventHandler(ImprimirPagina);
        pd.Print();
    }

    private void ImprimirPagina(object sender, PrintPageEventArgs e)
    {
        int posicion = 10;

        // Dibujar el logo
        if (System.IO.File.Exists(_logoPath))
        {
            Image logo = Image.FromFile(_logoPath);
            e.Graphics.DrawImage(logo, new PointF(1, posicion));
            posicion += 200;
        }

        // Dibujar los encabezados
        foreach (var encabezado in _encabezados)
        {
            e.Graphics.DrawString(encabezado, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(1, posicion));
            posicion += 20;
        }

        // Dibujar la información del ticket
        e.Graphics.DrawString("FOLIO DE VENTA: " + _folio, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(1, posicion));
        posicion += 20;
        e.Graphics.DrawString("MESA: " + _mesa, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(1, posicion));
        posicion += 20;
        e.Graphics.DrawString("LE ATENDIO: " + _mesero, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(1, posicion));
        posicion += 20;
        e.Graphics.DrawString("FECHA: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString(), new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(1, posicion));
        posicion += 50;

        // Dibujar la lista de productos
        e.Graphics.DrawString("Cant   Producto        P.Unit  Importe", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(1, posicion));
        posicion += 20;
        e.Graphics.DrawLine(new Pen(Color.Black), 1, posicion, 420, posicion);
        posicion += 10;

        StringFormat sf = new StringFormat();
        sf.Alignment = StringAlignment.Far;

        foreach (var producto in _productos)
        {
            e.Graphics.DrawString(producto.Cantidad.ToString("0.00", CultureInfo.InvariantCulture), new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(1, posicion));
            e.Graphics.DrawString(producto.Nombre, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(40, posicion));
            e.Graphics.DrawString($"{producto.PrecioUnitario:C}", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(230, posicion), sf);
            e.Graphics.DrawString($"{producto.Total:C}", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(280, posicion), sf);
            posicion += 20;
        }

        // Dibujar el total
        e.Graphics.DrawLine(new Pen(Color.Black), 210, posicion + 10, 420, posicion + 10);
        posicion += 15;
        e.Graphics.DrawString($"TOTAL: {_total:C}", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, new Point(280, posicion), sf);
        posicion += 50;

        // Dibujar el pie de página
        foreach (var pie in _pieDePagina)
        {
            e.Graphics.DrawString(pie, new Font("Arial", 10, FontStyle.Bold), Brushes.Black, new Point(1, posicion));
            posicion += 20;
        }
    }
}

public class Producto
{
    public string Nombre { get; set; }
    public double Cantidad { get; set; }
    public double PrecioUnitario { get; set; }
    public double Total { get; set; }
}