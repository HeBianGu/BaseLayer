using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Design;
using System.IO;
using GrapeCity.ActiveReports.Design;
using GrapeCity.ActiveReports.Design.Resources;
using GrapeCity.ActiveReports;
using System.Xml;
using AppLoadingMT;
namespace GrapeCity.ActiveReports.Samples.EndUserDesigner
{
    public partial class EndUserDesigner : Form
    {
        ToolStripDropDownItem filemenu;
        public EndUserDesigner()
        {
            InitializeComponent();

            Splasher.Status = "Loading Files...";

            //Set the ToolBox, ReportExplorer and PropertyGrid in the Designer.
            this.reportdesigner.Toolbox = this.reporttoolbox;//Attaches the toolbox to the report designer
            this.reportExplorer.ReportDesigner = this.reportdesigner;//Attaches the report explorer to the report designer
            this.reportdesigner.PropertyGrid = this.reportpropertyGrid;//Attaches the Property Grid to the report designer
            //Populate the menu.
            ToolStrip toolstrip = this.reportdesigner.CreateToolStrips(new DesignerToolStrips[]{
               DesignerToolStrips.Menu,
            })[0];
            filemenu = (ToolStripDropDownItem)toolstrip.Items[0];
            this.CreateFileMenu(filemenu);
            this.AppendToolStrips(0, new ToolStrip[]
                    {
                            toolstrip
                     });
            this.AppendToolStrips(1, this.reportdesigner.CreateToolStrips(new DesignerToolStrips[]
                    {
                         DesignerToolStrips.Edit, 
                         DesignerToolStrips.Undo, 
                         DesignerToolStrips.Zoom
                     }));
            ToolStrip item = this.CreateReportToolbar();
            this.AppendToolStrips(1, new List<ToolStrip>
	             {
		               item
	             });
            this.AppendToolStrips(2, this.reportdesigner.CreateToolStrips(new DesignerToolStrips[]
                {
                   DesignerToolStrips.Format, 
                   DesignerToolStrips.Layout
                }));
            LoadTools(this.reporttoolbox);
            reportdesigner.LayoutChanged += (sender, args) => { if (args.Type == LayoutChangeType.ReportLoad || args.Type == LayoutChangeType.ReportClear) RefreshExportEnabled(); };
            RefreshExportEnabled();

            Splasher.Close();
        }
        private void RefreshExportEnabled()
        {
            reportdesigner.ActiveTabChanged -= OnEnableExport;
            reportdesigner.ActiveTabChanged += OnEnableExport;
            OnEnableExport(this, EventArgs.Empty);
        }
        private void OnEnableExport(object sender, EventArgs eventArgs)
        {
            filemenu.DropDownItems[2].Enabled = reportdesigner.ActiveTab == DesignerTab.Preview;
        }
        private static void LoadTools(IToolboxService toolbox)
        {
            //Add Data Providers.
            foreach (Type type in new Type[]
				{
					typeof (System.Data.DataSet),
					typeof (System.Data.DataView),
					typeof (System.Data.OleDb.OleDbConnection),
					typeof (System.Data.OleDb.OleDbDataAdapter),
					typeof (System.Data.Odbc.OdbcConnection),
					typeof (System.Data.Odbc.OdbcDataAdapter),
					typeof (System.Data.SqlClient.SqlConnection),
					typeof (System.Data.SqlClient.SqlDataAdapter)
				})
            {
                toolbox.AddToolboxItem(new ToolboxItem(type), "Data");
            }
        }
        //Adding DropDownItems to the ToolStripDropDownItem.
        private void CreateFileMenu(ToolStripDropDownItem fileMenu)
        {
            fileMenu.DropDownItems.Clear();
            fileMenu.DropDownItems.Add(new ToolStripMenuItem("New", Images.CmdNewReport, new EventHandler(this.OnNew), (Keys)131150));
            fileMenu.DropDownItems.Add(new ToolStripMenuItem("Open", Images.CmdOpen, new EventHandler(this.OnOpen), (Keys)131151));
            fileMenu.DropDownItems.Add(new ToolStripMenuItem("Export", null, new EventHandler(this.OnExport), (Keys)131141));
            fileMenu.DropDownItems.Add(new ToolStripMenuItem("Save", Images.CmdSave, new EventHandler(this.OnSave), (Keys)131155));
            fileMenu.DropDownItems.Add(new ToolStripMenuItem("Save As", Images.CmdSaveAs, new EventHandler(this.OnSaveAs)));
            fileMenu.DropDownItems.Add(new ToolStripSeparator());
            fileMenu.DropDownItems.Add(new ToolStripButton("Exit", null, new EventHandler(this.OnExit)));
            filemenu.DropDownItems[2].Enabled = false;
        }
        private ToolStrip CreateReportToolbar()
        {
            return new ToolStrip(new ToolStripButton[]
            {
                EndUserDesigner.CreateToolStripButton("New",Images.CmdNewReport,new EventHandler(this.OnNew),"New"),
                EndUserDesigner.CreateToolStripButton("Open",Images.CmdOpen,new EventHandler(this.OnOpen),"Open"),
                EndUserDesigner.CreateToolStripButton("Save",Images.CmdSave,new EventHandler(this.OnSave),"Save")
            });
        }
        //Click "New" to open a new report.
        private void OnNew(object sender, EventArgs e)
        {
            if (this.ConfirmSaveChanges())
            {
                this.reportdesigner.ExecuteAction(DesignerAction.NewReport);
                EnableTabs();    
            }
        }
        //Click "Open" to open an existing report.
        private void OnOpen(object sender, EventArgs e)
        {
            if (this.ConfirmSaveChanges())
            {
                this.reportdesigner.ExecuteAction(DesignerAction.FileOpen);
                EnableTabs();
            }
        }
        private void OnExport(object sender, EventArgs e)
        {
            ExportForm _exportForm = new ExportForm(this.reportdesigner.ReportType, this.reportdesigner.Report, reportdesigner.ReportViewer);
            _exportForm.Show();
        }
        //Click "Save" to save a report.
        private void OnSave(object sender, EventArgs e)
        {
            this.reportdesigner.ExecuteAction(DesignerAction.FileSave);
        }
        //Click "Save as" to save a report with a name.
        private void OnSaveAs(object sender, EventArgs e)
        {
            this.reportdesigner.ExecuteAction(DesignerAction.FileSave);
        }
        private void OnExit(object sender, EventArgs e)
        {
            base.Close();
        }
        //Checking whether modifications have been made to the report loaded to the designer.
        private bool ConfirmSaveChanges()
        {
            if (this.reportdesigner.IsDirty)
            {
                DialogResult dialogresult = MessageBox.Show("Report has been changed!!Do you wish to save it?", "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dialogresult == DialogResult.Cancel)
                {
                    return false;
                }
                else if (dialogresult == DialogResult.Yes)
                {
                    this.reportdesigner.ExecuteAction(DesignerAction.FileSave);
                }
            }
            return true;
        }
        private void AppendToolStrips(int row, IList<ToolStrip> toolStrips)
        {
            ToolStripPanel topToolStripPanel = this.toolStripContainer1.TopToolStripPanel;
            int num = toolStrips.Count;
            while (--num >= 0)
            {
                topToolStripPanel.Join(toolStrips[num], row);
            }
        }
        private static ToolStripButton CreateToolStripButton(string text, Image image, EventHandler handler, string toolTip)
        {
            return new ToolStripButton(text, image, handler)
            {
                DisplayStyle = ToolStripItemDisplayStyle.Image,
                ToolTipText = toolTip,
                DoubleClickEnabled = true
            };
        }
        private void EnableTabs()
        {
            reporttoolbox.Reorder(reportdesigner);
            reporttoolbox.EnsureCategories();
            reporttoolbox.Refresh();
        }
    }
}
