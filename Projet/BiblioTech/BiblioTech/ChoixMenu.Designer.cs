namespace BiblioTech
{
    partial class ChoixMenu
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
            lstLends = new DataGridView();
            lblUtilisateurs = new Label();
            lstUsers = new ListBox();
            lblLivres = new Label();
            lstBooks = new ListBox();
            lblEmprunts = new Label();
            ((System.ComponentModel.ISupportInitialize)lstLends).BeginInit();
            SuspendLayout();
            // 
            // lstLends
            // 
            lstLends.GridColor = SystemColors.WindowText;
            lstLends.Location = new Point(816, 92);
            lstLends.Name = "lstLends";
            lstLends.ReadOnly = true;
            lstLends.Size = new Size(550, 833);
            lstLends.TabIndex = 6;
            lstLends.CellContentClick += lstLends_CellContentClick;
            // 
            // lblUtilisateurs
            // 
            lblUtilisateurs.AutoSize = true;
            lblUtilisateurs.Font = new Font("SimSun", 22F, FontStyle.Bold);
            lblUtilisateurs.Location = new Point(151, 59);
            lblUtilisateurs.Name = "lblUtilisateurs";
            lblUtilisateurs.Size = new Size(205, 30);
            lblUtilisateurs.TabIndex = 1;
            lblUtilisateurs.Text = "Utilisateurs";
            // 
            // lstUsers
            // 
            lstUsers.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lstUsers.FormattingEnabled = true;
            lstUsers.ItemHeight = 25;
            lstUsers.Location = new Point(151, 92);
            lstUsers.Name = "lstUsers";
            lstUsers.Size = new Size(205, 754);
            lstUsers.TabIndex = 2;
            lstUsers.SelectedIndexChanged += lstUsers_SelectedIndexChanged_1;
            // 
            // lblLivres
            // 
            lblLivres.AutoSize = true;
            lblLivres.Font = new Font("SimSun", 22F, FontStyle.Bold);
            lblLivres.Location = new Point(435, 59);
            lblLivres.Name = "lblLivres";
            lblLivres.Size = new Size(109, 30);
            lblLivres.TabIndex = 3;
            lblLivres.Text = "Livres";
            // 
            // lstBooks
            // 
            lstBooks.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lstBooks.FormattingEnabled = true;
            lstBooks.ItemHeight = 25;
            lstBooks.Location = new Point(435, 92);
            lstBooks.Name = "lstBooks";
            lstBooks.Size = new Size(205, 754);
            lstBooks.TabIndex = 4;
            lstBooks.SelectedIndexChanged += lstBooks_SelectedIndexChanged;
            // 
            // lblEmprunts
            // 
            lblEmprunts.AutoSize = true;
            lblEmprunts.Font = new Font("SimSun", 22F, FontStyle.Bold);
            lblEmprunts.Location = new Point(816, 59);
            lblEmprunts.Name = "lblEmprunts";
            lblEmprunts.Size = new Size(141, 30);
            lblEmprunts.TabIndex = 5;
            lblEmprunts.Text = "Emprunts";
            lblEmprunts.Click += lblEmprunts_Click;
            // 
            // ChoixMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Tan;
            ClientSize = new Size(1555, 638);
            Controls.Add(lstLends);
            Controls.Add(lblEmprunts);
            Controls.Add(lstBooks);
            Controls.Add(lblLivres);
            Controls.Add(lstUsers);
            Controls.Add(lblUtilisateurs);
            Name = "ChoixMenu";
            Text = "ChoixMenu";
            Load += ChoixMenu_Load;
            ((System.ComponentModel.ISupportInitialize)lstLends).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label lblUtilisateurs;
        private ListBox lstUsers;
        private Label lblLivres;
        private ListBox lstBooks;
        private Label lblEmprunts;
        private DataGridView lstLends;
    }
}