namespace FCNS.Calendar
{
    partial class MiniForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MiniForm));
            this.listViewTask = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.查看详情ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.标记为已完成ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.切换报警状态ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBoxUndoneTask = new System.Windows.Forms.CheckBox();
            this.checkBoxPastTask = new System.Windows.Forms.CheckBox();
            this.contextMenuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewTask
            // 
            this.listViewTask.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.listViewTask.ContextMenuStrip = this.contextMenuStrip1;
            this.listViewTask.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewTask.FullRowSelect = true;
            this.listViewTask.Location = new System.Drawing.Point(0, 0);
            this.listViewTask.MultiSelect = false;
            this.listViewTask.Name = "listViewTask";
            this.listViewTask.Size = new System.Drawing.Size(290, 151);
            this.listViewTask.Sorting = System.Windows.Forms.SortOrder.Descending;
            this.listViewTask.TabIndex = 2;
            this.listViewTask.UseCompatibleStateImageBehavior = false;
            this.listViewTask.View = System.Windows.Forms.View.Details;
            this.listViewTask.DoubleClick += new System.EventHandler(this.listViewTask_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "日期时间";
            this.columnHeader1.Width = 76;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "标题";
            this.columnHeader2.Width = 128;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "报警";
            this.columnHeader3.Width = 39;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "完成";
            this.columnHeader4.Width = 37;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.查看详情ToolStripMenuItem,
            this.标记为已完成ToolStripMenuItem,
            this.切换报警状态ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(143, 70);
            // 
            // 查看详情ToolStripMenuItem
            // 
            this.查看详情ToolStripMenuItem.Name = "查看详情ToolStripMenuItem";
            this.查看详情ToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.查看详情ToolStripMenuItem.Text = "查看详情";
            this.查看详情ToolStripMenuItem.Click += new System.EventHandler(this.查看详情ToolStripMenuItem_Click);
            // 
            // 标记为已完成ToolStripMenuItem
            // 
            this.标记为已完成ToolStripMenuItem.Name = "标记为已完成ToolStripMenuItem";
            this.标记为已完成ToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.标记为已完成ToolStripMenuItem.Text = "标记为已完成";
            this.标记为已完成ToolStripMenuItem.Click += new System.EventHandler(this.标记为已完成ToolStripMenuItem_Click);
            // 
            // 切换报警状态ToolStripMenuItem
            // 
            this.切换报警状态ToolStripMenuItem.Name = "切换报警状态ToolStripMenuItem";
            this.切换报警状态ToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.切换报警状态ToolStripMenuItem.Text = "切换报警状态";
            this.切换报警状态ToolStripMenuItem.Click += new System.EventHandler(this.切换报警状态ToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.checkBoxUndoneTask);
            this.panel1.Controls.Add(this.checkBoxPastTask);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 151);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(290, 19);
            this.panel1.TabIndex = 3;
            // 
            // checkBoxUndoneTask
            // 
            this.checkBoxUndoneTask.AutoSize = true;
            this.checkBoxUndoneTask.Location = new System.Drawing.Point(164, 2);
            this.checkBoxUndoneTask.Name = "checkBoxUndoneTask";
            this.checkBoxUndoneTask.Size = new System.Drawing.Size(108, 16);
            this.checkBoxUndoneTask.TabIndex = 0;
            this.checkBoxUndoneTask.Text = "显示未完成任务";
            this.checkBoxUndoneTask.UseVisualStyleBackColor = true;
            this.checkBoxUndoneTask.CheckedChanged += new System.EventHandler(this.checkBoxUndoneTask_CheckedChanged);
            // 
            // checkBoxPastTask
            // 
            this.checkBoxPastTask.AutoSize = true;
            this.checkBoxPastTask.Location = new System.Drawing.Point(2, 2);
            this.checkBoxPastTask.Name = "checkBoxPastTask";
            this.checkBoxPastTask.Size = new System.Drawing.Size(96, 16);
            this.checkBoxPastTask.TabIndex = 0;
            this.checkBoxPastTask.Text = "隐藏过期任务";
            this.checkBoxPastTask.UseVisualStyleBackColor = true;
            this.checkBoxPastTask.CheckedChanged += new System.EventHandler(this.checkBoxPastTask_CheckedChanged);
            // 
            // MiniForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(290, 170);
            this.Controls.Add(this.listViewTask);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MiniForm";
            this.ShowInTaskbar = false;
            this.Text = "迷你窗口";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.MiniForm_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewTask;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox checkBoxPastTask;
        private System.Windows.Forms.CheckBox checkBoxUndoneTask;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 查看详情ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 标记为已完成ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 切换报警状态ToolStripMenuItem;
    }
}