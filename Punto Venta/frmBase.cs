using iTextSharp.text;
using iTextSharp.text.pdf;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using System;
using System.Diagnostics; // Importante para manejar procesos (FreeVK)
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Document = iTextSharp.text.Document;
using Excel = Microsoft.Office.Interop.Excel;

namespace Punto_Venta
{
    public partial class frmBase : Form
    {
        public frmBase()
        {
            // Estilos base del Formulario
            this.BackColor = Color.FromArgb(25, 25, 25);
            this.ForeColor = Color.White;
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, FontStyle.Regular);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.KeyPreview = true;

            this.KeyDown += FrmBase_KeyDown;
            this.Load += FrmBase_Load;
            this.FormClosing += FrmBase_FormClosing;
        }

        private void FrmBase_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                RegistrarEventosTecladoTouch(this);
            }
        }
        private void FrmBase_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Si la opción está habilitada, cerramos el teclado al cerrar el Form
            if (Conexion.AbrirTeclado)
            {
                CerrarTeclado();
            }
        }

        /// <summary>
        /// Recorre recursivamente todos los controles del Formulario y registra
        /// el evento GotFocus/Click en los TextBox para abrir el teclado virtual.
        /// </summary>
        private void RegistrarEventosTecladoTouch(Control parentControl)
        {
            foreach (Control ctrl in parentControl.Controls)
            {
                if (ctrl is TextBox txt)
                {
                    // Desuscribimos primero para evitar eventos duplicados
                    txt.GotFocus -= TextBox_GotFocus_AbrirTeclado;
                    txt.GotFocus += TextBox_GotFocus_AbrirTeclado;
                }

                // Recorrido recursivo para controles contenedores (Panels, GroupBoxes, TabControls, etc.)
                if (ctrl.HasChildren)
                {
                    RegistrarEventosTecladoTouch(ctrl);
                }
            }
        }

        private void TextBox_GotFocus_AbrirTeclado(object sender, EventArgs e)
        {
            if (Conexion.AbrirTeclado)
            {
                AbrirTeclado();
            }
        }

        /// <summary>
        /// Abre el ejecutable FreeVK solo si no se encuentra en ejecución actualmente.
        /// </summary>
        private void AbrirTeclado()
        {
            string RutaTeclado = @"C:\Jaeger Soft\FreeVK.exe";
            try
            {
                if (File.Exists(RutaTeclado))
                {
                    // Verifica si el proceso YA está corriendo para evitar abrir múltiples instancias
                    if (Process.GetProcessesByName("FreeVK").Length == 0)
                    {
                        Process.Start(RutaTeclado);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al abrir el teclado: " + ex.Message);
            }
        }

        /// <summary>
        /// Cierra todas las instancias abiertas del teclado virtual FreeVK.
        /// </summary>
        public void CerrarTeclado()
        {
            try
            {
                foreach (var proceso in Process.GetProcessesByName("FreeVK"))
                {
                    proceso.Kill();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cerrar el teclado: " + ex.Message);
            }
        }

        protected virtual void FrmBase_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        /// <summary>
        /// Aplica el estilo "dark mode" estándar a un DataGridView
        /// y agrega funcionalidad de exportación con Click Derecho.
        /// </summary>
        public void EstilizarDataGridView(DataGridView dgv)
        {
            dgv.BackgroundColor = Color.FromArgb(40, 40, 40);
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = Color.FromArgb(60, 60, 60);
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.RowHeadersVisible = false;
            dgv.MultiSelect = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.ReadOnly = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            // Estilo del Encabezado
            DataGridViewCellStyle headerStyle = new DataGridViewCellStyle();
            headerStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            headerStyle.BackColor = Color.FromArgb(60, 60, 60);
            headerStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 12.5F, FontStyle.Bold);
            headerStyle.ForeColor = Color.White;
            headerStyle.SelectionBackColor = Color.FromArgb(70, 130, 180);
            headerStyle.SelectionForeColor = Color.White;
            headerStyle.WrapMode = DataGridViewTriState.True;
            dgv.ColumnHeadersDefaultCellStyle = headerStyle;
            dgv.ColumnHeadersHeight = 32;

            // Estilo de Celdas
            DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();
            cellStyle.BackColor = Color.FromArgb(35, 35, 35);
            cellStyle.ForeColor = Color.White;
            cellStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 12.5F, FontStyle.Regular);
            cellStyle.SelectionBackColor = Color.FromArgb(70, 130, 180);
            cellStyle.SelectionForeColor = Color.White;
            dgv.DefaultCellStyle = cellStyle;

            // Estilo Alterno
            DataGridViewCellStyle alternatingCellStyle = new DataGridViewCellStyle();
            alternatingCellStyle.BackColor = Color.FromArgb(55, 55, 55);
            alternatingCellStyle.ForeColor = Color.White;
            alternatingCellStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 12.5F, FontStyle.Regular);
            alternatingCellStyle.SelectionBackColor = Color.FromArgb(70, 130, 180);
            alternatingCellStyle.SelectionForeColor = Color.White;
            dgv.AlternatingRowsDefaultCellStyle = alternatingCellStyle;

            AgregarMenuContextualExcel(dgv);
            AgregarMenuContextualPDF(dgv, this.Text);
            AgregarMenuContextualCopiar(dgv);
        }

        private void AgregarMenuContextualExcel(DataGridView dgv)
        {
            ContextMenuStrip menu = new ContextMenuStrip();
            ToolStripMenuItem itemExportar = new ToolStripMenuItem();
            itemExportar.Text = "Exportar a Excel";

            itemExportar.Click += (s, e) => { ExportarGridAExcel(dgv); };
            menu.Items.Add(itemExportar);

            if (dgv.ContextMenuStrip == null)
            {
                dgv.ContextMenuStrip = menu;
            }
            else
            {
                dgv.ContextMenuStrip.Items.Add(new ToolStripSeparator());
                dgv.ContextMenuStrip.Items.Add(itemExportar);
            }
        }

        public void AgregarMenuContextualPDF(DataGridView dgv, string tituloReporte)
        {
            string nombreArchivoSeguro = tituloReporte;
            foreach (char c in System.IO.Path.GetInvalidFileNameChars())
            {
                nombreArchivoSeguro = nombreArchivoSeguro.Replace(c, '_');
            }

            if (dgv.ContextMenuStrip == null)
            {
                dgv.ContextMenuStrip = new ContextMenuStrip();
            }

            ToolStripMenuItem itemExportarPdf = new ToolStripMenuItem();
            itemExportarPdf.Text = "Exportar a PDF";

            itemExportarPdf.Click += (s, e) => { ExportarGridAPDF(dgv, tituloReporte); };

            if (dgv.ContextMenuStrip.Items.Count > 0)
            {
                dgv.ContextMenuStrip.Items.Add(new ToolStripSeparator());
            }

            dgv.ContextMenuStrip.Items.Add(itemExportarPdf);
        }

        private void AgregarMenuContextualCopiar(DataGridView dgv)
        {
            if (dgv.ContextMenuStrip == null)
                dgv.ContextMenuStrip = new ContextMenuStrip();

            if (dgv.ContextMenuStrip.Items.Count > 0)
                dgv.ContextMenuStrip.Items.Add(new ToolStripSeparator());

            ToolStripMenuItem itemCopiar = new ToolStripMenuItem();
            itemCopiar.Text = "Copiar al portapapeles";
            itemCopiar.Click += (s, e) => CopiarGridAlPortapapeles(dgv);

            dgv.ContextMenuStrip.Items.Add(itemCopiar);
        }

        private void CopiarGridAlPortapapeles(DataGridView dgv)
        {
            if (dgv.Rows.Count == 0)
            {
                MessageBox.Show(
                    "No hay datos para copiar.",
                    "Atención",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            try
            {
                StringBuilder sb = new StringBuilder();
                bool primera = true;

                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    if (!col.Visible) continue;
                    if (!primera) sb.Append('\t');
                    sb.Append(col.HeaderText);
                    primera = false;
                }

                sb.AppendLine();

                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (row.IsNewRow) continue;
                    primera = true;

                    foreach (DataGridViewColumn col in dgv.Columns)
                    {
                        if (!col.Visible) continue;
                        if (!primera) sb.Append('\t');

                        string valor = row.Cells[col.Index].FormattedValue?.ToString() ?? "";
                        valor = valor.Replace("\r", " ").Replace("\n", " ");

                        sb.Append(valor);
                        primera = false;
                    }

                    sb.AppendLine();
                }

                Clipboard.SetText(sb.ToString());

                MessageBox.Show(
                    "La información fue copiada al portapapeles.",
                    "Copiar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al copiar:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ExportarGridAPDF(DataGridView dgv, string titulo)
        {
            if (dgv.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para exportar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nombreArchivoSeguro = titulo;
            foreach (char c in System.IO.Path.GetInvalidFileNameChars())
            {
                nombreArchivoSeguro = nombreArchivoSeguro.Replace(c, '_');
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";
            saveFileDialog.FileName = nombreArchivoSeguro + "_" + DateTime.Now.ToString("ddMMyyyy") + ".pdf";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Document doc = new Document(iTextSharp.text.PageSize.LETTER.Rotate(), 10, 10, 10, 10);
                    PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(saveFileDialog.FileName, FileMode.Create));

                    doc.Open();

                    iTextSharp.text.Font fuenteTitulo = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 14, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    Paragraph parrafoTitulo = new Paragraph(titulo, fuenteTitulo);
                    parrafoTitulo.Alignment = Element.ALIGN_CENTER;
                    doc.Add(parrafoTitulo);

                    iTextSharp.text.Font fuenteFecha = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.DARK_GRAY);
                    Paragraph parrafoFecha = new Paragraph("Fecha de emisión: " + DateTime.Now.ToString(), fuenteFecha);
                    parrafoFecha.Alignment = Element.ALIGN_CENTER;
                    doc.Add(parrafoFecha);

                    doc.Add(iTextSharp.text.Chunk.NEWLINE);

                    int columnasVisibles = 0;
                    foreach (DataGridViewColumn col in dgv.Columns) { if (col.Visible) columnasVisibles++; }

                    PdfPTable pdfTable = new PdfPTable(columnasVisibles);
                    pdfTable.WidthPercentage = 100;

                    iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    iTextSharp.text.Font _headerFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.BOLD, BaseColor.WHITE);

                    foreach (DataGridViewColumn col in dgv.Columns)
                    {
                        if (col.Visible)
                        {
                            PdfPCell clHeader = new PdfPCell(new Phrase(col.HeaderText, _headerFont));
                            clHeader.BorderWidth = 0;
                            clHeader.BorderWidthBottom = 0.75f;
                            clHeader.BackgroundColor = new BaseColor(50, 50, 50);
                            clHeader.HorizontalAlignment = Element.ALIGN_CENTER;
                            clHeader.Padding = 5;
                            pdfTable.AddCell(clHeader);
                        }
                    }

                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            foreach (DataGridViewColumn col in dgv.Columns)
                            {
                                if (col.Visible)
                                {
                                    string valor = row.Cells[col.Index].FormattedValue != null ? row.Cells[col.Index].FormattedValue.ToString() : "";

                                    PdfPCell clData = new PdfPCell(new Phrase(valor, _standardFont));
                                    clData.BorderWidth = 0;
                                    clData.BorderWidthBottom = 0.25f;
                                    clData.BorderColorBottom = BaseColor.LIGHT_GRAY;

                                    if (valor.Contains("$") || decimal.TryParse(valor, out _))
                                        clData.HorizontalAlignment = Element.ALIGN_RIGHT;
                                    else if (DateTime.TryParse(valor, out _))
                                        clData.HorizontalAlignment = Element.ALIGN_CENTER;
                                    else
                                        clData.HorizontalAlignment = Element.ALIGN_LEFT;

                                    pdfTable.AddCell(clData);
                                }
                            }
                        }
                    }

                    doc.Add(pdfTable);
                    doc.Close();
                    writer.Close();

                    MessageBox.Show("PDF generado exitosamente en:\n" + saveFileDialog.FileName, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    try { System.Diagnostics.Process.Start(saveFileDialog.FileName); } catch { }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al exportar PDF: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ExportarGridAExcel(DataGridView dgv)
        {
            if (dgv.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para exportar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;

                Excel.Application excelApp = new Excel.Application();
                Excel.Workbook workbook = excelApp.Workbooks.Add();
                Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Worksheets[1];

                int colIndex = 1;
                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    if (dgv.Columns[i].Visible)
                    {
                        worksheet.Cells[1, colIndex] = dgv.Columns[i].HeaderText;
                        colIndex++;
                    }
                }

                Excel.Range headerRange = worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, colIndex - 1]];
                headerRange.Font.Bold = true;
                headerRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(Color.LightGray);

                int rowIndex = 2;
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (row.IsNewRow) continue;

                    colIndex = 1;
                    for (int i = 0; i < dgv.Columns.Count; i++)
                    {
                        if (dgv.Columns[i].Visible)
                        {
                            object valor = row.Cells[i].Value;

                            if (valor != null)
                            {
                                if (valor is DateTime)
                                {
                                    worksheet.Cells[rowIndex, colIndex] = ((DateTime)valor).ToString("dd/MM/yyyy HH:mm");
                                }
                                else
                                {
                                    worksheet.Cells[rowIndex, colIndex] = valor.ToString();
                                }
                            }
                            colIndex++;
                        }
                    }
                    rowIndex++;
                }

                worksheet.Columns.AutoFit();
                excelApp.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al exportar a Excel: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void ConfigurarBotonBase(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            btn.UseVisualStyleBackColor = false;
        }

        public void EstilizarBotonPrimario(Button btn)
        {
            ConfigurarBotonBase(btn);
            btn.BackColor = Color.FromArgb(52, 152, 219);
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(41, 128, 185);
        }

        public void EstilizarBotonPeligro(Button btn)
        {
            ConfigurarBotonBase(btn);
            btn.BackColor = Color.FromArgb(231, 76, 60);
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(192, 57, 43);
        }

        public void EstilizarBotonAdvertencia(Button btn)
        {
            ConfigurarBotonBase(btn);
            btn.BackColor = Color.FromArgb(241, 196, 15);
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(243, 156, 18);
        }

        public void EstilizarTextBox(TextBox txt)
        {
            txt.BorderStyle = BorderStyle.FixedSingle;
            txt.BackColor = Color.FromArgb(60, 60, 60);
            txt.ForeColor = Color.White;
        }

        public void EstilizarComboBox(ComboBox cmb)
        {
            cmb.FlatStyle = FlatStyle.Flat;
            cmb.BackColor = Color.FromArgb(60, 60, 60);
            cmb.ForeColor = Color.White;

            cmb.DrawMode = DrawMode.OwnerDrawFixed;
            int paddingVertical = 8;
            cmb.ItemHeight = cmb.Font.Height + paddingVertical;

            cmb.DrawItem -= ComboBox_DrawItem;
            cmb.DrawItem += new DrawItemEventHandler(ComboBox_DrawItem);
        }

        private void ComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            ComboBox cmb = (ComboBox)sender;
            string text = cmb.GetItemText(cmb.Items[e.Index]);

            Color backgroundColor;
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                backgroundColor = Color.FromArgb(70, 130, 180);
            }
            else
            {
                backgroundColor = Color.FromArgb(45, 45, 45);
            }

            e.Graphics.FillRectangle(new SolidBrush(backgroundColor), e.Bounds);

            System.Drawing.Rectangle textBounds = e.Bounds;
            int padding = 4;
            textBounds.X += padding;
            textBounds.Width -= padding;

            TextRenderer.DrawText(e.Graphics, text, e.Font, textBounds,
                                  Color.White,
                                  TextFormatFlags.Left |
                                  TextFormatFlags.VerticalCenter |
                                  TextFormatFlags.EndEllipsis);

            e.DrawFocusRectangle();
        }

        public void EstilizarCheckBox(CheckBox chk)
        {
            chk.ForeColor = Color.White;
            chk.BackColor = Color.Transparent;
        }

        public void AjustarAnchoDropDown(ComboBox cmb)
        {
            int maxWidth = 0;

            using (Graphics g = cmb.CreateGraphics())
            {
                foreach (object item in cmb.Items)
                {
                    string text = cmb.GetItemText(item);
                    Size size = TextRenderer.MeasureText(g, text, cmb.Font);

                    if (size.Width > maxWidth)
                    {
                        maxWidth = size.Width;
                    }
                }
            }

            int padding = 25;
            int newWidth = maxWidth + padding;

            if (newWidth < cmb.Width)
            {
                cmb.DropDownWidth = cmb.Width;
            }
            else
            {
                cmb.DropDownWidth = newWidth;
            }
        }
    }
}