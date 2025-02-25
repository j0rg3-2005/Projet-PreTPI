namespace BiblioTech
{
    partial class frmLendsDashboard
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
            flnpnlLends = new FlowLayoutPanel();
            SuspendLayout();
            // 
            // flnpnlLends
            // 
            flnpnlLends.AutoScroll = true;
            flnpnlLends.Location = new Point(1301, 72);
            flnpnlLends.Name = "flnpnlLends";
            flnpnlLends.Size = new Size(200, 100);
            flnpnlLends.TabIndex = 1;
            // 
            // frmLendsDashboard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1624, 696);
            Controls.Add(flnpnlLends);
            Name = "frmLendsDashboard";
            Text = "LendsDashboard";
            Load += frmLendsDashboard_Load;
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel flnpnlLends;
    }
}