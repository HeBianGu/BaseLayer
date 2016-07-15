using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using GrapeCity.ActiveReports.Export.Html.Page;
using GrapeCity.ActiveReports.Rendering.IO;
using System.IO;
using GrapeCity.ActiveReports.Export.Image.Page;
using GrapeCity.ActiveReports.Export.Pdf.Page;
using GrapeCity.ActiveReports.Export.Rdf;
using GrapeCity.ActiveReports.Export.Word.Page;
using GrapeCity.ActiveReports.Export.Xml.Page;
using GrapeCity.ActiveReports.Extensibility.Rendering;
namespace GrapeCity.ActiveReports.Samples.EndUserDesigner
{
    public partial class ExportForm : Form
    {
        GrapeCity.ActiveReports.Design.DesignerReportType _reportType;
        object _report;
        object _exportsettings = null;
        string _filter = "";
        SectionReport _sectionReport = new SectionReport();
        GrapeCity.ActiveReports.Viewer.Win.Viewer _reportViewer;
        FileStreamProvider _exportfile;
        System.Collections.Specialized.NameValueCollection _settings;
        public ExportForm(GrapeCity.ActiveReports.Design.DesignerReportType ReportType,Object Report,Object ReportViewer)
        {
            _reportType = ReportType;
            _report = Report;
            _reportViewer = (GrapeCity.ActiveReports.Viewer.Win.Viewer)ReportViewer;
            InitializeComponent();
        }
        void ExportSectionReport()
        {
            switch (_exportsettings.GetType().Name)
            {
                case "XlsExport":
                    {
                        exportSaveFileDialog.Filter = _filter;
                        if (((GrapeCity.ActiveReports.Export.Excel.Section.XlsExport)_exportsettings).FileFormat.ToString() == "Xlsx")
                        {
                            exportSaveFileDialog.FileName = "ActiveReports8Excel.xlsx";
                            exportSaveFileDialog.Filter = "Excel 2007 files (*.xlsx)|*.xlsx|Excel files (*.xls)|*.xls|All files (*.*)|*.*";
                        }
                        else
                        {
                            exportSaveFileDialog.FileName = "ActiveReports8Excel.xls";
                        }
                        if (exportSaveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            ((GrapeCity.ActiveReports.Export.Excel.Section.XlsExport)_exportsettings).Export(_sectionReport.Document, exportSaveFileDialog.FileName);
                            MessageBox.Show("Excel Export Complete");
                        }
                    }
                    break;
                case "HtmlExport":
                    {
                        exportSaveFileDialog.Filter = _filter;
                        exportSaveFileDialog.FileName = "ActiveReports8HTML.html";
                        if (exportSaveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            ((GrapeCity.ActiveReports.Export.Html.Section.HtmlExport)_exportsettings).Export(_sectionReport.Document, exportSaveFileDialog.FileName);
                            MessageBox.Show("HTML Export Complete");
                        }
                    }
                    break;
                case "PdfExport":
                    {
                        exportSaveFileDialog.Filter = _filter;
                        exportSaveFileDialog.FileName = "ActiveReports8PDF.pdf";
                        if (exportSaveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            ((GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport)_exportsettings).Export(_sectionReport.Document, exportSaveFileDialog.FileName);
                            MessageBox.Show("PDF Export Complete");
                        }
                    }
                    break;
                case "RtfExport":
                    {
                        exportSaveFileDialog.Filter = _filter;
                        exportSaveFileDialog.FileName = "ActiveReports8RTF.rtf";
                        if (exportSaveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            ((GrapeCity.ActiveReports.Export.Word.Section.RtfExport)_exportsettings).Export(_sectionReport.Document, exportSaveFileDialog.FileName);
                            MessageBox.Show("RTF Export Complete");
                        }
                    }
                    break;
                case "TextExport":
                    {
                        exportSaveFileDialog.Filter = _filter;
                        exportSaveFileDialog.FileName = "ActiveReports8Text.txt";
                        if (exportSaveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            ((GrapeCity.ActiveReports.Export.Xml.Section.TextExport)_exportsettings).Export(_sectionReport.Document, exportSaveFileDialog.FileName);
                            MessageBox.Show("Text Export Complete");
                        }
                    }
                    break;
                case "TiffExport":
                    {
                        exportSaveFileDialog.Filter = _filter;
                        exportSaveFileDialog.FileName = "ActiveReports8Tiff.tiff";
                        if (exportSaveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            ((GrapeCity.ActiveReports.Export.Image.Tiff.Section.TiffExport)_exportsettings).Export(_sectionReport.Document, this.exportSaveFileDialog.FileName);
                            MessageBox.Show("TIFF Export Complete");
                        }
                    }
                    break;
            }
        }
        void ExportPageReport(ComboBox PageExportComboBox)
        {
            switch (PageExportComboBox.SelectedIndex)
            {
                case 0:
                    {
                        exportSaveFileDialog.Filter = _filter;
                        if (((GrapeCity.ActiveReports.Export.Excel.Section.XlsExport)_exportsettings).FileFormat.ToString() == "Xlsx")
                        {
                            exportSaveFileDialog.FileName = "ActiveReports8Excel.xlsx";
                            exportSaveFileDialog.Filter = "Excel 2007 files (*.xlsx)|*.xlsx|Excel files (*.xls)|*.xls|All files (*.*)|*.*";
                        }
                        else
                        {
                            exportSaveFileDialog.FileName = "ActiveReports8Excel.xls";
                        }
                        if (exportSaveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            _reportViewer.Export((GrapeCity.ActiveReports.Export.Excel.Section.XlsExport)_exportsettings, new System.IO.FileInfo(exportSaveFileDialog.FileName));
                            MessageBox.Show("Excel Export Complete");
                        }
                    }
                    break;
                case 1:
                    {
                        exportSaveFileDialog.Filter = _filter;
                        exportSaveFileDialog.FileName = "ActiveReports8HTML.html";
                        if (exportSaveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            _exportfile = new FileStreamProvider(new DirectoryInfo(Path.GetDirectoryName(exportSaveFileDialog.FileName)), Path.GetFileNameWithoutExtension(exportSaveFileDialog.FileName));
                            HtmlRenderingExtension _html = new HtmlRenderingExtension();
                             _reportViewer.Render(_html, _exportfile,_settings);
                            _html.Dispose();
                            MessageBox.Show("HTML Export Complete");
                        }
                    }
                    break;
                case 2:
                    {
                        exportSaveFileDialog.Filter = _filter;
                        switch (_settings[2])
                        {
                            case "JPEG": exportSaveFileDialog.FileName = "ActiveReports8Image.Jpeg";
                                break;
                            case "BMP": exportSaveFileDialog.FileName = "ActiveReports8Image.BMP";
                                break;
                            case "EMF": exportSaveFileDialog.FileName = "ActiveReports8Image.EMF";
                                break;
                            case "GIF": exportSaveFileDialog.FileName = "ActiveReports8Image.GIF";
                                break;
                            case "TIFF": exportSaveFileDialog.FileName = "ActiveReports8Image.TIFF";
                                break;
                            case "PNG": exportSaveFileDialog.FileName = "ActiveReports8Image.PNG";
                                break;  
                        }
                        if (exportSaveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            _exportfile = new FileStreamProvider(new DirectoryInfo(Path.GetDirectoryName(exportSaveFileDialog.FileName)), Path.GetFileNameWithoutExtension(exportSaveFileDialog.FileName));
                            ImageRenderingExtension _img = new ImageRenderingExtension();
                            _reportViewer.Render(_img, _exportfile, _settings);
                            _img = null;
                            MessageBox.Show("Image Export Complete");
                        }
                    }
                    break;
                case 3:
                    {
                        exportSaveFileDialog.Filter = _filter;
                        exportSaveFileDialog.FileName = "ActiveReports8PDF.pdf";
                        if (exportSaveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            _exportfile = new FileStreamProvider(new DirectoryInfo(Path.GetDirectoryName(exportSaveFileDialog.FileName)), Path.GetFileNameWithoutExtension(exportSaveFileDialog.FileName));
                            PdfRenderingExtension _pdf = new PdfRenderingExtension();
                            _reportViewer.Render(_pdf, _exportfile, _settings);
                            _pdf = null;
                            MessageBox.Show("PDF Export Complete");
                        }
                    }
                    break;
                case 4:
                    {
                        exportSaveFileDialog.Filter = _filter;
                        exportSaveFileDialog.FileName = "ActiveReports8RDF.rdf";
                        if (exportSaveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            _exportfile = new FileStreamProvider(new DirectoryInfo(Path.GetDirectoryName(exportSaveFileDialog.FileName)), Path.GetFileNameWithoutExtension(exportSaveFileDialog.FileName));
                            RdfRenderingExtension _rdf = new RdfRenderingExtension();
                            _reportViewer.Render(_rdf, _exportfile, _settings);
                            _rdf = null;
                            MessageBox.Show("RDF Export Complete");
                        }
                    }  break;
                case 5:
                    {
                        exportSaveFileDialog.Filter = _filter;
                        exportSaveFileDialog.FileName = "ActiveReports8Word.doc";
                        if (exportSaveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            _exportfile = new FileStreamProvider(new DirectoryInfo(Path.GetDirectoryName(exportSaveFileDialog.FileName)), Path.GetFileNameWithoutExtension(exportSaveFileDialog.FileName));
                            WordRenderingExtension _word = new WordRenderingExtension();
                            _reportViewer.Render(_word, _exportfile, _settings);
                            _word = null;
                            MessageBox.Show("Word Export Complete");
                        }
                    }
                    break;
                case 6:
                    {
                        exportSaveFileDialog.Filter = _filter;
                        exportSaveFileDialog.FileName = "ActiveReports8XML.xml";
                        if (exportSaveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            _exportfile = new FileStreamProvider(new DirectoryInfo(Path.GetDirectoryName(exportSaveFileDialog.FileName)), Path.GetFileNameWithoutExtension(exportSaveFileDialog.FileName));
                            XmlRenderingExtension _xml = new XmlRenderingExtension();
                            _reportViewer.Render(_xml, _exportfile, _settings);
                            _xml = null;
                            MessageBox.Show("XML Export Complete");
                        }
                        break;
                    }
            }
        }
        void AddPageReportExportTypes()
        {
            this.cmbExportFormat.Items.Add("Microsoft Excel Worksheet(Excel)");
            this.cmbExportFormat.Items.Add("Hyper Text Markup Language(HTML)");
            this.cmbExportFormat.Items.Add("Image Format( BMP , EMP , GIF , JPEG , TIFF, PNG )");
            this.cmbExportFormat.Items.Add("Portable Document Format(PDF)");
            this.cmbExportFormat.Items.Add("Report Document Format(RDF)");
            this.cmbExportFormat.Items.Add("Microsoft Word Document(Word)");
            this.cmbExportFormat.Items.Add("Extensible Markup Language(XML)");
            this.cmbExportFormat.SelectedIndex = 0;
        }
        void AddSectionReportExportTypes()
        {
            this.cmbExportFormat.Items.Add("Hypertext Markup Language(HTML)");
            this.cmbExportFormat.Items.Add("Microsoft Excel WorkBook( XLS , XLSX )");
            this.cmbExportFormat.Items.Add("Plain Text(TXT)");
            this.cmbExportFormat.Items.Add("Portable Document Format(PDF)");
            this.cmbExportFormat.Items.Add("Rich Text Format(RTF)");
            this.cmbExportFormat.Items.Add("Tagged Image Format(TIFF)");
            this.cmbExportFormat.SelectedIndex = 0;
        }
        void LoadPageReportPropertyGridSettings(ComboBox ExportFormatComboBox)
        {
            this.lblExportoptions.Visible = true;
            this.exportpropertyGrid.Visible = true;
            switch (ExportFormatComboBox.SelectedIndex)
            {
                case 0:
                    {
                        this.exportpropertyGrid.Visible = true;
                        _exportsettings = new GrapeCity.ActiveReports.Export.Excel.Section.XlsExport();
                        _filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*";
                        break;
                    }
                case 1:
                    {
                        this.exportpropertyGrid.Visible = true;
                        _exportsettings = new GrapeCity.ActiveReports.Export.Html.Page.Settings();
                      _filter = "Html files (*.htm)|*.html|All files (*.*)|*.*";
                        break;
                    }
                case 2:
                    {
                        this.exportpropertyGrid.Visible = true;
                        _exportsettings = new GrapeCity.ActiveReports.Export.Image.Page.Settings();
                       _filter = "Image files(*.BMP;*.EMF;*.GIF;*.JPEG;*.TIFF;*.PNG)|*.BMP;*.EMF;*.GIF;*.JPEG;*.TIFF;*.PNG";
                        break;
                    }
                case 3:
                    {
                        this.exportpropertyGrid.Visible = true;
                        _exportsettings = new GrapeCity.ActiveReports.Export.Pdf.Page.Settings();
                       _filter = "Pdf files (*.pdf)|*.pdf|All files (*.*)|*.*";
                        break;
                    }
                case 4:
                    {
                        this.exportpropertyGrid.Visible = false;
                        this.lblExportoptions.Visible = false;
                        this.exportpropertyGrid.Visible = false;
                        _filter = "Rdf files (*.rdf)|*.rdf|All files (*.*)|*.*";
                        break;
                    }
                case 5:
                    {
                        this.exportpropertyGrid.Visible = true;
                        _exportsettings = new GrapeCity.ActiveReports.Export.Word.Page.Settings();
                       _filter = "MS Word files (*.doc)|*.doc|All files (*.*)|*.*";
                        break;
                    }
                case 6:
                    {
                        this.exportpropertyGrid.Visible = true;
                        _exportsettings = new GrapeCity.ActiveReports.Export.Xml.Page.Settings();
                       _filter = "Xml files (*.xml)|*.xml|All files (*.*)|*.*";
                        break;
                    }
            }
            this.exportpropertyGrid.SelectedObject = _exportsettings;
        }
        void LoadSectionReportPropertyGridSettings(ComboBox ExportFormatComboBox)
        {
            this.lblExportoptions.Visible = true;
            this.exportpropertyGrid.Visible = true;
            switch (ExportFormatComboBox.SelectedIndex)
            {
                case 0:
                    {
                        _exportsettings = new GrapeCity.ActiveReports.Export.Html.Section.HtmlExport();
                        _filter = "Html files (*.htm)|*.html|All files (*.*)|*.*";
                        break;
                    }
                case 1:
                    {
                        _exportsettings = new GrapeCity.ActiveReports.Export.Excel.Section.XlsExport();
                        this.exportpropertyGrid.SelectedObject = _exportsettings;
                       _filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*";
                        break;
                    }
                case 2:
                    {
                          var _textexportsettings = new GrapeCity.ActiveReports.Export.Xml.Section.TextExport();
                        _exportsettings = _textexportsettings;
                        _filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                        break;
                    }
                case 3:
                    {
                        _exportsettings = new GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport();
                       _filter = "pdf files (*.pdf)|*.pdf|All files (*.*)|*.*";
                        break;
                    }
                case 4:
                    {
                        _exportsettings = new GrapeCity.ActiveReports.Export.Word.Section.RtfExport();
                       _filter = "Rtf files (*.rtf)|*.rtf|All files (*.*)|*.*";
                        break;
                    }
                case 5:
                    {
                        _exportsettings = new GrapeCity.ActiveReports.Export.Image.Tiff.Section.TiffExport();
                       _filter = "Tiff files (*.tiff)|*.tiff|All files (*.*)|*.*";
                        break;
                    }
            }
            this.exportpropertyGrid.SelectedObject = _exportsettings;
        }
        private void ExportForm_Load(object sender, EventArgs e)
        {
            if (_reportType.ToString() == "Section")
            {
                AddSectionReportExportTypes();
                System.IO.MemoryStream memory_stream = new System.IO.MemoryStream();
                memory_stream.Position = 0;
                using (System.Xml.XmlWriter writer = System.Xml.XmlWriter.Create(memory_stream))
                {
                    ((SectionReport)_report).SaveLayout(writer);
                }
                memory_stream.Position = 0;
                using (System.Xml.XmlReader reader = System.Xml.XmlReader.Create(memory_stream))
                {
                    _sectionReport.LoadLayout(reader);
                }
                _sectionReport.Run();
            }
            if (_reportType.ToString() == "Page")
            {
                AddPageReportExportTypes();
            }
        }
        private void cmbExportFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_reportType.ToString() == "Page")
            {
                LoadPageReportPropertyGridSettings(this.cmbExportFormat);
            }
            if (_reportType.ToString() == "Section")
            {
                LoadSectionReportPropertyGridSettings(this.cmbExportFormat);
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (_reportType.ToString() == "Page")
            {
                _settings = new System.Collections.Specialized.NameValueCollection();
                if (_exportsettings is ISettings)
                {
                    _settings = ((ISettings)_exportsettings).GetSettings();
                }
                ExportPageReport(this.cmbExportFormat);
            }
            if (_reportType.ToString() == "Section")
            {
                ExportSectionReport();
            }
        }
    }
}
