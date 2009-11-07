namespace MonMon
{
    partial class DockTest
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._tabControlView = new System.Windows.Forms.TabControl();
            this._tabPageFunctionList = new System.Windows.Forms.TabPage();
            this._tabPageFiles = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this._tabControl = new System.Windows.Forms.TabControl();
            this._listBoxFindResults = new System.Windows.Forms.ListBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this._statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._functionListControl = new MonMon.FunctionList();
            this._openFilesControl = new MonMon.OpenFilesList();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this._tabControlView.SuspendLayout();
            this._tabPageFunctionList.SuspendLayout();
            this._tabPageFiles.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._tabControlView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(628, 427);
            this.splitContainer1.SplitterDistance = 105;
            this.splitContainer1.TabIndex = 10;
            // 
            // _tabControlView
            // 
            this._tabControlView.Controls.Add(this._tabPageFunctionList);
            this._tabControlView.Controls.Add(this._tabPageFiles);
            this._tabControlView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tabControlView.Location = new System.Drawing.Point(0, 0);
            this._tabControlView.Multiline = true;
            this._tabControlView.Name = "_tabControlView";
            this._tabControlView.SelectedIndex = 0;
            this._tabControlView.Size = new System.Drawing.Size(105, 427);
            this._tabControlView.TabIndex = 4;
            // 
            // _tabPageFunctionList
            // 
            this._tabPageFunctionList.Controls.Add(this._functionListControl);
            this._tabPageFunctionList.Location = new System.Drawing.Point(4, 22);
            this._tabPageFunctionList.Name = "_tabPageFunctionList";
            this._tabPageFunctionList.Padding = new System.Windows.Forms.Padding(3);
            this._tabPageFunctionList.Size = new System.Drawing.Size(97, 401);
            this._tabPageFunctionList.TabIndex = 0;
            this._tabPageFunctionList.Text = "Functions";
            this._tabPageFunctionList.UseVisualStyleBackColor = true;
            // 
            // _tabPageFiles
            // 
            this._tabPageFiles.Controls.Add(this._openFilesControl);
            this._tabPageFiles.Location = new System.Drawing.Point(4, 22);
            this._tabPageFiles.Name = "_tabPageFiles";
            this._tabPageFiles.Padding = new System.Windows.Forms.Padding(3);
            this._tabPageFiles.Size = new System.Drawing.Size(97, 401);
            this._tabPageFiles.TabIndex = 1;
            this._tabPageFiles.Text = "Files";
            this._tabPageFiles.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this._tabControl);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this._listBoxFindResults);
            this.splitContainer2.Size = new System.Drawing.Size(519, 427);
            this.splitContainer2.SplitterDistance = 354;
            this.splitContainer2.TabIndex = 0;
            // 
            // _tabControl
            // 
            this._tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tabControl.Location = new System.Drawing.Point(0, 0);
            this._tabControl.Margin = new System.Windows.Forms.Padding(2);
            this._tabControl.Name = "_tabControl";
            this._tabControl.Padding = new System.Drawing.Point(0, 0);
            this._tabControl.SelectedIndex = 0;
            this._tabControl.Size = new System.Drawing.Size(519, 354);
            this._tabControl.TabIndex = 3;
            // 
            // _listBoxFindResults
            // 
            this._listBoxFindResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this._listBoxFindResults.FormattingEnabled = true;
            this._listBoxFindResults.HorizontalScrollbar = true;
            this._listBoxFindResults.Location = new System.Drawing.Point(0, 0);
            this._listBoxFindResults.Name = "_listBoxFindResults";
            this._listBoxFindResults.Size = new System.Drawing.Size(519, 69);
            this._listBoxFindResults.TabIndex = 4;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._statusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 427);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(628, 22);
            this.statusStrip1.TabIndex = 9;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // _statusLabel
            // 
            this._statusLabel.Name = "_statusLabel";
            this._statusLabel.Size = new System.Drawing.Size(118, 17);
            this._statusLabel.Text = "toolStripStatusLabel1";
            // 
            // _functionListControl
            // 
            this._functionListControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._functionListControl.Location = new System.Drawing.Point(3, 3);
            this._functionListControl.Name = "_functionListControl";
            this._functionListControl.Size = new System.Drawing.Size(91, 395);
            this._functionListControl.TabIndex = 0;
            // 
            // _openFilesControl
            // 
            this._openFilesControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._openFilesControl.Location = new System.Drawing.Point(3, 3);
            this._openFilesControl.Name = "_openFilesControl";
            this._openFilesControl.Size = new System.Drawing.Size(91, 395);
            this._openFilesControl.TabIndex = 0;
            // 
            // DockTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 449);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "DockTest";
            this.Text = "DockTest";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this._tabControlView.ResumeLayout(false);
            this._tabPageFunctionList.ResumeLayout(false);
            this._tabPageFiles.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl _tabControlView;
        private System.Windows.Forms.TabPage _tabPageFunctionList;
        private FunctionList _functionListControl;
        private System.Windows.Forms.TabPage _tabPageFiles;
        private OpenFilesList _openFilesControl;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TabControl _tabControl;
        private System.Windows.Forms.ListBox _listBoxFindResults;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel _statusLabel;

    }
}