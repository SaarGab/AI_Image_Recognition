namespace WinFormsApp1
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtFolderPath;
        private System.Windows.Forms.Button btnBrowseFolder;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.PictureBox pictureMatchPreview;
        private System.Windows.Forms.Button btnNextArrow;
        private System.Windows.Forms.Label lblStatus;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtFolderPath = new System.Windows.Forms.TextBox();
            this.btnBrowseFolder = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.pictureMatchPreview = new System.Windows.Forms.PictureBox();
            this.btnNextArrow = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureMatchPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // txtFolderPath
            this.txtFolderPath.Location = new System.Drawing.Point(20, 20);
            this.txtFolderPath.ReadOnly = true;
            this.txtFolderPath.Size = new System.Drawing.Size(400, 27);
            // 
            // btnBrowseFolder
            this.btnBrowseFolder.Location = new System.Drawing.Point(430, 18);
            this.btnBrowseFolder.Size = new System.Drawing.Size(100, 30);
            this.btnBrowseFolder.Text = "Browse...";
            this.btnBrowseFolder.Click += new System.EventHandler(this.btnBrowseFolder_Click);
            // 
            // txtSearch
            this.txtSearch.Location = new System.Drawing.Point(20, 60);
            this.txtSearch.Size = new System.Drawing.Size(400, 27);
            this.txtSearch.PlaceholderText = "Enter search term...";
            // 
            // btnSearch
            this.btnSearch.Location = new System.Drawing.Point(430, 58);
            this.btnSearch.Size = new System.Drawing.Size(100, 30);
            this.btnSearch.Text = "Search";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // pictureMatchPreview
            this.pictureMatchPreview.Location = new System.Drawing.Point(20, 100);
            this.pictureMatchPreview.Size = new System.Drawing.Size(400, 400);
            this.pictureMatchPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureMatchPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            // 
            // btnNextArrow
            this.btnNextArrow.Location = new System.Drawing.Point(430, 100);
            this.btnNextArrow.Size = new System.Drawing.Size(100, 50);
            this.btnNextArrow.Text = "➡ Next";
            this.btnNextArrow.Click += new System.EventHandler(this.btnNextArrow_Click);
            // 
            // lblStatus
            this.lblStatus.Location = new System.Drawing.Point(20, 520);
            this.lblStatus.Size = new System.Drawing.Size(510, 20);
            this.lblStatus.Text = "Idle";
            // 
            // Form1
            this.ClientSize = new System.Drawing.Size(560, 550);
            this.Controls.Add(this.txtFolderPath);
            this.Controls.Add(this.btnBrowseFolder);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.pictureMatchPreview);
            this.Controls.Add(this.btnNextArrow);
            this.Controls.Add(this.lblStatus);
            this.Name = "Form1";
            this.Text = "Image Finder";
            ((System.ComponentModel.ISupportInitialize)(this.pictureMatchPreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
