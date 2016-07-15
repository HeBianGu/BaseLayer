namespace GrapeCity.ActiveReports.Samples.EndUserDesigner
{
    partial class ExportForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        /// <summary>
        /// Clean up any resources being used.
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lblSelectExporttxt = new System.Windows.Forms.Label();
            this.lblExport = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.cmbExportFormat = new System.Windows.Forms.ComboBox();
            this.lblExportFormat = new System.Windows.Forms.Label();
            this.lblExportoptions = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.exportpropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.exportSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // splitContainer1
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            // splitContainer1.Panel1
            resources.ApplyResources(this.splitContainer1.Panel1, "splitContainer1.Panel1");
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.splitContainer1.Panel1.Controls.Add(this.lblSelectExporttxt);
            this.splitContainer1.Panel1.Controls.Add(this.lblExport);
            // splitContainer1.Panel2
            resources.ApplyResources(this.splitContainer1.Panel2, "splitContainer1.Panel2");
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            // lblSelectExporttxt
            resources.ApplyResources(this.lblSelectExporttxt, "lblSelectExporttxt");
            this.lblSelectExporttxt.Name = "lblSelectExporttxt";
            // lblExport
            resources.ApplyResources(this.lblExport, "lblExport");
            this.lblExport.Name = "lblExport";
            // splitContainer2
            resources.ApplyResources(this.splitContainer2, "splitContainer2");
            this.splitContainer2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.splitContainer2.Name = "splitContainer2";
            // splitContainer2.Panel1
            resources.ApplyResources(this.splitContainer2.Panel1, "splitContainer2.Panel1");
            this.splitContainer2.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.splitContainer2.Panel1.Controls.Add(this.cmbExportFormat);
            this.splitContainer2.Panel1.Controls.Add(this.lblExportFormat);
            // splitContainer2.Panel2
            resources.ApplyResources(this.splitContainer2.Panel2, "splitContainer2.Panel2");
            this.splitContainer2.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.splitContainer2.Panel2.Controls.Add(this.lblExportoptions);
            this.splitContainer2.Panel2.Controls.Add(this.btnCancel);
            this.splitContainer2.Panel2.Controls.Add(this.btnOk);
            this.splitContainer2.Panel2.Controls.Add(this.exportpropertyGrid);
            // cmbExportFormat
            resources.ApplyResources(this.cmbExportFormat, "cmbExportFormat");
            this.cmbExportFormat.FormattingEnabled = true;
            this.cmbExportFormat.Name = "cmbExportFormat";
            this.cmbExportFormat.SelectedIndexChanged += new System.EventHandler(this.cmbExportFormat_SelectedIndexChanged);
            // lblExportFormat
            resources.ApplyResources(this.lblExportFormat, "lblExportFormat");
            this.lblExportFormat.Name = "lblExportFormat";
            // lblExportoptions
            resources.ApplyResources(this.lblExportoptions, "lblExportoptions");
            this.lblExportoptions.Name = "lblExportoptions";
            // btnCancel
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // btnOk
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // exportpropertyGrid
            resources.ApplyResources(this.exportpropertyGrid, "exportpropertyGrid");
            this.exportpropertyGrid.Name = "exportpropertyGrid";
            // exportSaveFileDialog
            resources.ApplyResources(this.exportSaveFileDialog, "exportSaveFileDialog");
            // ExportForm
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ExportForm";
            this.Load += new System.EventHandler(this.ExportForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);
        }
        #endregion
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label lblSelectExporttxt;
        private System.Windows.Forms.Label lblExport;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ComboBox cmbExportFormat;
        private System.Windows.Forms.Label lblExportFormat;
        private System.Windows.Forms.Label lblExportoptions;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.PropertyGrid exportpropertyGrid;
        private System.Windows.Forms.SaveFileDialog exportSaveFileDialog;
    }
}
