namespace GrapeCity.ActiveReports.Samples.EndUserDesigner
{
    partial class EndUserDesigner
    {
        /// <summary>
        ///Required designer variable. 
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        /// <summary>
        ///Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EndUserDesigner));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.MainContainer = new System.Windows.Forms.SplitContainer();
            this.bodyContainer = new System.Windows.Forms.SplitContainer();
            this.reporttoolbox = new GrapeCity.ActiveReports.Design.Toolbox.Toolbox();
            this.designerexplorerpropertygridContainer = new System.Windows.Forms.SplitContainer();
            this.reportdesigner = new GrapeCity.ActiveReports.Design.Designer();
            this.explorerpropertygridContainer = new System.Windows.Forms.SplitContainer();
            this.reportExplorer = new GrapeCity.ActiveReports.Design.ReportExplorer.ReportExplorer();
            this.reportpropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.toolStripContainer1.SuspendLayout();
            this.MainContainer.Panel1.SuspendLayout();
            this.MainContainer.Panel2.SuspendLayout();
            this.MainContainer.SuspendLayout();
            this.bodyContainer.Panel1.SuspendLayout();
            this.bodyContainer.Panel2.SuspendLayout();
            this.bodyContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.reporttoolbox)).BeginInit();
            this.designerexplorerpropertygridContainer.Panel1.SuspendLayout();
            this.designerexplorerpropertygridContainer.Panel2.SuspendLayout();
            this.designerexplorerpropertygridContainer.SuspendLayout();
            this.explorerpropertygridContainer.Panel1.SuspendLayout();
            this.explorerpropertygridContainer.Panel2.SuspendLayout();
            this.explorerpropertygridContainer.SuspendLayout();
            this.SuspendLayout();
            // toolStripContainer1
            resources.ApplyResources(this.toolStripContainer1, "toolStripContainer1");
            // toolStripContainer1.BottomToolStripPanel
            resources.ApplyResources(this.toolStripContainer1.BottomToolStripPanel, "toolStripContainer1.BottomToolStripPanel");
            // toolStripContainer1.ContentPanel
            resources.ApplyResources(this.toolStripContainer1.ContentPanel, "toolStripContainer1.ContentPanel");
            // toolStripContainer1.LeftToolStripPanel
            resources.ApplyResources(this.toolStripContainer1.LeftToolStripPanel, "toolStripContainer1.LeftToolStripPanel");
            this.toolStripContainer1.Name = "toolStripContainer1";
            // toolStripContainer1.RightToolStripPanel
            resources.ApplyResources(this.toolStripContainer1.RightToolStripPanel, "toolStripContainer1.RightToolStripPanel");
            // toolStripContainer1.TopToolStripPanel
            resources.ApplyResources(this.toolStripContainer1.TopToolStripPanel, "toolStripContainer1.TopToolStripPanel");
            // MainContainer
            resources.ApplyResources(this.MainContainer, "MainContainer");
            this.MainContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.MainContainer.Name = "MainContainer";
            // MainContainer.Panel1
            resources.ApplyResources(this.MainContainer.Panel1, "MainContainer.Panel1");
            this.MainContainer.Panel1.Controls.Add(this.toolStripContainer1);
            // MainContainer.Panel2
            resources.ApplyResources(this.MainContainer.Panel2, "MainContainer.Panel2");
            this.MainContainer.Panel2.Controls.Add(this.bodyContainer);
            // bodyContainer
            resources.ApplyResources(this.bodyContainer, "bodyContainer");
            this.bodyContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.bodyContainer.Name = "bodyContainer";
            // bodyContainer.Panel1
            resources.ApplyResources(this.bodyContainer.Panel1, "bodyContainer.Panel1");
            this.bodyContainer.Panel1.Controls.Add(this.reporttoolbox);
            // bodyContainer.Panel2
            resources.ApplyResources(this.bodyContainer.Panel2, "bodyContainer.Panel2");
            this.bodyContainer.Panel2.Controls.Add(this.designerexplorerpropertygridContainer);
            // reporttoolbox
            resources.ApplyResources(this.reporttoolbox, "reporttoolbox");
            this.reporttoolbox.DesignerHost = null;
            this.reporttoolbox.Name = "reporttoolbox";
            // designerexplorerpropertygridContainer
            resources.ApplyResources(this.designerexplorerpropertygridContainer, "designerexplorerpropertygridContainer");
            this.designerexplorerpropertygridContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.designerexplorerpropertygridContainer.Name = "designerexplorerpropertygridContainer";
            // designerexplorerpropertygridContainer.Panel1
            resources.ApplyResources(this.designerexplorerpropertygridContainer.Panel1, "designerexplorerpropertygridContainer.Panel1");
            this.designerexplorerpropertygridContainer.Panel1.Controls.Add(this.reportdesigner);
            // designerexplorerpropertygridContainer.Panel2
            resources.ApplyResources(this.designerexplorerpropertygridContainer.Panel2, "designerexplorerpropertygridContainer.Panel2");
            this.designerexplorerpropertygridContainer.Panel2.Controls.Add(this.explorerpropertygridContainer);
            // reportdesigner
            resources.ApplyResources(this.reportdesigner, "reportdesigner");
            this.reportdesigner.IsDirty = false;
            this.reportdesigner.LockControls = false;
            this.reportdesigner.Name = "reportdesigner";
            this.reportdesigner.PreviewPages = 10;
            this.reportdesigner.PromptUser = true;
            this.reportdesigner.PropertyGrid = null;
            this.reportdesigner.ReportTabsVisible = true;
            this.reportdesigner.ShowDataSourceIcon = true;
            this.reportdesigner.Toolbox = null;
            this.reportdesigner.ToolBoxItem = null;
            // explorerpropertygridContainer
            resources.ApplyResources(this.explorerpropertygridContainer, "explorerpropertygridContainer");
            this.explorerpropertygridContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.explorerpropertygridContainer.Name = "explorerpropertygridContainer";
            // explorerpropertygridContainer.Panel1
            resources.ApplyResources(this.explorerpropertygridContainer.Panel1, "explorerpropertygridContainer.Panel1");
            this.explorerpropertygridContainer.Panel1.Controls.Add(this.reportExplorer);
            // explorerpropertygridContainer.Panel2
            resources.ApplyResources(this.explorerpropertygridContainer.Panel2, "explorerpropertygridContainer.Panel2");
            this.explorerpropertygridContainer.Panel2.Controls.Add(this.reportpropertyGrid);
            // reportExplorer
            resources.ApplyResources(this.reportExplorer, "reportExplorer");
            this.reportExplorer.Name = "reportExplorer";
            this.reportExplorer.ReportDesigner = this.reportdesigner;
            // reportpropertyGrid
            resources.ApplyResources(this.reportpropertyGrid, "reportpropertyGrid");
            this.reportpropertyGrid.Name = "reportpropertyGrid";
            // EndUserDesigner
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.MainContainer);
            this.Name = "EndUserDesigner";
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.MainContainer.Panel1.ResumeLayout(false);
            this.MainContainer.Panel2.ResumeLayout(false);
            this.MainContainer.ResumeLayout(false);
            this.bodyContainer.Panel1.ResumeLayout(false);
            this.bodyContainer.Panel2.ResumeLayout(false);
            this.bodyContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.reporttoolbox)).EndInit();
            this.designerexplorerpropertygridContainer.Panel1.ResumeLayout(false);
            this.designerexplorerpropertygridContainer.Panel2.ResumeLayout(false);
            this.designerexplorerpropertygridContainer.ResumeLayout(false);
            this.explorerpropertygridContainer.Panel1.ResumeLayout(false);
            this.explorerpropertygridContainer.Panel2.ResumeLayout(false);
            this.explorerpropertygridContainer.ResumeLayout(false);
            this.ResumeLayout(false);
        }
        #endregion
        private System.Windows.Forms.SplitContainer MainContainer;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.SplitContainer bodyContainer;
        private GrapeCity.ActiveReports.Design.Toolbox.Toolbox reporttoolbox;
        private System.Windows.Forms.SplitContainer designerexplorerpropertygridContainer;
        private GrapeCity.ActiveReports.Design.Designer reportdesigner;
        private System.Windows.Forms.SplitContainer explorerpropertygridContainer;
        private GrapeCity.ActiveReports.Design.ReportExplorer.ReportExplorer reportExplorer;
        private System.Windows.Forms.PropertyGrid reportpropertyGrid;
    }
}
