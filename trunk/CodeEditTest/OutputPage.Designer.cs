namespace MonMon
{
    partial class OutputPage
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
            this._listBoxFindResults = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // _listBoxFindResults
            // 
            this._listBoxFindResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this._listBoxFindResults.FormattingEnabled = true;
            this._listBoxFindResults.HorizontalScrollbar = true;
            this._listBoxFindResults.Location = new System.Drawing.Point(0, 0);
            this._listBoxFindResults.Name = "_listBoxFindResults";
            this._listBoxFindResults.Size = new System.Drawing.Size(284, 264);
            this._listBoxFindResults.TabIndex = 5;
            // 
            // OutputPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 264);
            this.Controls.Add(this._listBoxFindResults);
            this.Name = "OutputPage";
            this.Text = "OutputPage";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox _listBoxFindResults;
    }
}