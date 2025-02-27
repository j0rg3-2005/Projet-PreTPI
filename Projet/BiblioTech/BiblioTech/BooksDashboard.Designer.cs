namespace BiblioTech
{
    partial class frmBooksDashboard
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
            flnpnlBooks = new FlowLayoutPanel();
            SuspendLayout();
            // 
            // flnpnlBooks
            // 
            flnpnlBooks.AutoScroll = true;
            flnpnlBooks.Location = new Point(588, 42);
            flnpnlBooks.Name = "flnpnlBooks";
            flnpnlBooks.Size = new Size(200, 100);
            flnpnlBooks.TabIndex = 0;
            // 
            // frmBooksDashboard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(flnpnlBooks);
            Name = "frmBooksDashboard";
            Text = "BooksDashboard";
            Load += frmBooksDashboard_Load;
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel flnpnlBooks;
    }
}