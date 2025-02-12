namespace BiblioTech
{
    partial class frmUsersDashboard
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
            mySqlCommandBuilder1 = new MySqlConnector.MySqlCommandBuilder();
            flnpnlUsers = new FlowLayoutPanel();
            SuspendLayout();
            // 
            // mySqlCommandBuilder1
            // 
            mySqlCommandBuilder1.DataAdapter = null;
            mySqlCommandBuilder1.QuotePrefix = "`";
            mySqlCommandBuilder1.QuoteSuffix = "`";
            // 
            // flnpnlUsers
            // 
            flnpnlUsers.AutoScroll = true;
            flnpnlUsers.Location = new Point(1034, 26);
            flnpnlUsers.Name = "flnpnlUsers";
            flnpnlUsers.Size = new Size(200, 100);
            flnpnlUsers.TabIndex = 0;
            // 
            // frmUsersDashboard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1578, 718);
            Controls.Add(flnpnlUsers);
            Name = "frmUsersDashboard";
            Text = "UsersDashboard";
            Load += frmUsersDashboard_Load;
            ResumeLayout(false);
        }

        #endregion
        private MySqlConnector.MySqlCommandBuilder mySqlCommandBuilder1;
        private FlowLayoutPanel flnpnlUsers;
    }
}