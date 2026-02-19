using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using System;
using System.Collections.Generic;
using System.Globalization;

public static class ComandasReportPdf
{
    private static readonly CultureInfo mx = CultureInfo.CreateSpecificCulture("es-MX");

    public class ReportHeader
    {
        public string Empresa { get; set; }
        public string Rfc { get; set; }
        public string Direccion { get; set; }
        public string CiudadCpTel { get; set; }
        public DateTime Desde { get; set; }
        public DateTime Hasta { get; set; }
        public string MeseroFiltro { get; set; } // "(TODOS)" u otro
        public string FooterNote { get; set; }   // texto libre para el pie
    }

    public class ComandaRow
    {
        public long FolioComanda { get; set; }
        public long FolioCuenta { get; set; }
        public int Orden { get; set; }
        public DateTime FechaApertura { get; set; }
        public DateTime FechaCierre { get; set; }
        public string MeseroCuenta { get; set; }
        public string MeseroProd { get; set; }
        public decimal Cantidad { get; set; }
        public DateTime FechaCaptura { get; set; }
        public string Producto { get; set; }
        public decimal Importe { get; set; }
        public decimal Descuento { get; set; }
    }

    public static void Generate(ReportHeader header, IList<ComandaRow> rows, string outputPath)
    {
        var doc = new Document();
        doc.Info.Title = "Reporte de Comandas";
        DefinirEstilos(doc);

        var sec = doc.AddSection();

        // Letter horizontal + márgenes compactos
        sec.PageSetup.PageFormat = PageFormat.Letter;
        sec.PageSetup.Orientation = Orientation.Landscape;
        sec.PageSetup.TopMargin = Unit.FromMillimeter(22);
        sec.PageSetup.BottomMargin = Unit.FromMillimeter(12);
        sec.PageSetup.LeftMargin = Unit.FromMillimeter(10);
        sec.PageSetup.RightMargin = Unit.FromMillimeter(10);
        sec.PageSetup.HeaderDistance = Unit.FromMillimeter(4);
        sec.PageSetup.FooterDistance = Unit.FromMillimeter(4);

        // ===== Encabezado de PÁGINA (repetido) — compacto =====
        var hp = sec.Headers.Primary;
        var hTable = hp.AddTable();
        hTable.Format.Font.Name = "Arial";
        hTable.Format.Font.Size = 8;   // más chico
        hTable.Borders.Width = 0;
        hTable.AddColumn(Unit.FromMillimeter(180));
        hTable.AddColumn(Unit.FromMillimeter(75));

        var hRow1 = hTable.AddRow();
        hRow1.TopPadding = Unit.FromMillimeter(0.2);
        hRow1.BottomPadding = Unit.FromMillimeter(0.2);
        var pL1 = hRow1.Cells[0].AddParagraph();
        pL1.AddFormattedText(header.Empresa ?? "", TextFormat.Bold);
        var pR1 = hRow1.Cells[1].AddParagraph(DateTime.Now.ToString("dd/MM/yyyy", mx));
        pR1.Format.Alignment = ParagraphAlignment.Right;

        var hRow2 = hTable.AddRow();
        hRow2.TopPadding = Unit.FromMillimeter(0.2);
        hRow2.BottomPadding = Unit.FromMillimeter(0.2);
        var pL2 = hRow2.Cells[0].AddParagraph();
        pL2.AddText((header.Empresa ?? "") + "  RFC: " + (header.Rfc ?? ""));
        var pR2 = hRow2.Cells[1].AddParagraph(DateTime.Now.ToString("HH:mm:ss", mx)); // 24h
        pR2.Format.Alignment = ParagraphAlignment.Right;

        var hRow3 = hTable.AddRow();
        hRow3.TopPadding = Unit.FromMillimeter(0.2);
        hRow3.BottomPadding = Unit.FromMillimeter(0.2);
        hRow3.Cells[0].AddParagraph(header.Direccion ?? "");

        var hRow4 = hTable.AddRow();
        hRow4.TopPadding = Unit.FromMillimeter(0.2);
        hRow4.BottomPadding = Unit.FromMillimeter(0.2);
        hRow4.Cells[0].AddParagraph(header.CiudadCpTel ?? "");

        // ===== Título (cuerpo, compacto) =====
        var pTitle = sec.AddParagraph();
        pTitle.Format.SpaceBefore = Unit.FromMillimeter(1);
        pTitle.Format.SpaceAfter = Unit.FromMillimeter(2); // empuja poco la tabla
        pTitle.Format.Font.Bold = true;
        pTitle.Format.Font.Size = 8;
        pTitle.AddText("COMANDAS DEL  ");
        pTitle.AddText(header.Desde.ToString("dd/MM/yyyy HH:mm:ss", mx));
        pTitle.AddText("  AL  ");
        pTitle.AddText(header.Hasta.ToString("dd/MM/yyyy HH:mm:ss", mx));
        pTitle.AddTab();
        pTitle.AddText("MESERO: ");
        pTitle.AddText(string.IsNullOrEmpty(header.MeseroFiltro) ? "(TODOS)" : header.MeseroFiltro);

        // ===== TABLA DE DATOS (sin bordes, filas compactas) =====
        var table = sec.AddTable();
        table.Format.Font.Name = "Arial";
        table.Format.Font.Size = 8;  // más chico
        table.Borders.Color = Colors.White;
        table.Borders.Width = 0;

        // Compactar altura de filas
       
        table.Rows.HeightRule = RowHeightRule.Auto;
        table.Rows.VerticalAlignment = VerticalAlignment.Center;

        // Columnas — Letter landscape útil ≈ 255 mm
        AddCol(table, 17);  // 0: FOLIO COMANDA
        AddCol(table, 17);  // 1: FOLIO CUENTA
        AddCol(table, 12);  // 2: ORDEN
        AddCol(table, 32);  // 3: FECHA DE APERTURA
        AddCol(table, 32);  // 4: FECHA DE CIERRE
        AddCol(table, 15);  // 5: MESERO CUENTA
        AddCol(table, 15);  // 6: MESERO PROD.
        AddCol(table, 12);  // 7: CANT.
        AddCol(table, 32);  // 8: FECHA DE CAPTURA
        AddCol(table, 46);  // 9: PRODUCTO
        AddCol(table, 17);  // 10: IMPORTE
        AddCol(table, 14);  // 11: DESC.
        // Total = 255 mm

        // Encabezados de columnas (repetibles)
        var hr = table.AddRow();
        hr.HeadingFormat = true;
        hr.Shading.Color = Colors.LightGray;
        hr.TopPadding = Unit.FromMillimeter(0.2);
        hr.BottomPadding = Unit.FromMillimeter(0.2);

        Set(hr, 0, "FOLIO COMANDA");
        Set(hr, 1, "FOLIO CUENTA");
        Set(hr, 2, "ORDEN");
        Set(hr, 3, "FECHA DE APERTURA");
        Set(hr, 4, "FECHA DE CIERRE");
        Set(hr, 5, "MESERO CUENTA");
        Set(hr, 6, "MESERO PROD.");
        Set(hr, 7, "CANT.");
        Set(hr, 8, "FECHA DE CAPTURA");
        Set(hr, 9, "PRODUCTO");
        Set(hr, 10, "IMPORTE");
        Set(hr, 11, "DESC.");
        hr.Borders.Color = Colors.Gray;
        hr.Borders.Top.Width = 0.75;
        hr.Borders.Bottom.Width = 0.75;
        hr.Borders.Left.Width = 0.75;
        hr.Borders.Right.Width = 0.75;

        // Filas
        foreach (var r in rows)
        {
            var row = table.AddRow();
            row.TopPadding = Unit.FromMillimeter(0.2);
            row.BottomPadding = Unit.FromMillimeter(0.2);

            row.Cells[0].AddParagraph(r.FolioComanda.ToString());
            row.Cells[1].AddParagraph(r.FolioCuenta.ToString());
            row.Cells[2].AddParagraph(r.Orden.ToString());
            row.Cells[3].AddParagraph(r.FechaApertura.ToString("dd/MM/yyyy HH:mm:ss", mx));
            row.Cells[4].AddParagraph(r.FechaCierre.ToString("dd/MM/yyyy HH:mm:ss", mx));
            row.Cells[5].AddParagraph(r.MeseroCuenta ?? "");
            row.Cells[6].AddParagraph(r.MeseroProd ?? "");
            row.Cells[7].AddParagraph(FormatCantidad(r.Cantidad));
            row.Cells[8].AddParagraph(r.FechaCaptura.ToString("dd/MM/yyyy HH:mm:ss", mx));
            row.Cells[9].AddParagraph(r.Producto ?? "");
            row.Cells[10].AddParagraph(r.Importe.ToString("C2", mx)).Format.Alignment = ParagraphAlignment.Right;
            row.Cells[11].AddParagraph(r.Descuento.ToString("C2", mx)).Format.Alignment = ParagraphAlignment.Right;
        }

        // ===== Footer: nota (izquierda) + paginación (derecha), compacto =====
        var fTable = sec.Footers.Primary.AddTable();
        fTable.Borders.Width = 0;
        fTable.Format.Font.Name = "Arial";
        fTable.Format.Font.Size = 8;
        fTable.AddColumn(Unit.FromMillimeter(170));
        fTable.AddColumn(Unit.FromMillimeter(85));

        var fRow = fTable.AddRow();
        fRow.TopPadding = Unit.FromMillimeter(0.2);
        var pLeftF = fRow.Cells[0].AddParagraph(header.FooterNote ?? "");
        pLeftF.Format.Alignment = ParagraphAlignment.Left;
        var pRightF = fRow.Cells[1].AddParagraph();
        pRightF.Format.Alignment = ParagraphAlignment.Right;
        pRightF.AddText("Página ");
        pRightF.AddPageField();
        pRightF.AddText(" de ");
        pRightF.AddNumPagesField();

        var renderer = new PdfDocumentRenderer(true);
        renderer.Document = doc;
        renderer.RenderDocument();
        renderer.PdfDocument.Save(outputPath);
    }

    // ---------- Helpers ----------
    private static void DefinirEstilos(Document doc)
    {
        var normal = doc.Styles["Normal"];
        normal.Font.Name = "Arial";
        normal.Font.Size = 7.5; // base aún más pequeña

        var heading1 = doc.Styles["Heading1"];
        heading1.Font.Name = "Arial";
        heading1.Font.Size = 8.5;
        heading1.Font.Bold = true;
    }

    private static void AddCol(Table t, double widthMm)
    {
        var col = t.AddColumn(Unit.FromMillimeter(widthMm));
        col.Format.Alignment = ParagraphAlignment.Left;
        col.Borders.Width = 0; // sin bordes en celdas
    }

    private static void Set(Row r, int cell, string text)
    {
        r.Cells[cell].AddParagraph(text);
        r.Cells[cell].VerticalAlignment = VerticalAlignment.Center;
        r.Cells[cell].Borders.Width = 0; // sin bordes
    }

    private static string FormatCantidad(decimal value)
    {
        if (value == Math.Truncate(value))
            return ((long)value).ToString(CultureInfo.InvariantCulture);
        return value.ToString("0.##", CultureInfo.InvariantCulture);
    }
}
