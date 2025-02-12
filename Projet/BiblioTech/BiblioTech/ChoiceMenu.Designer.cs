namespace BiblioTech
{
    partial class frmChoiceMenu
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnUsers = new Button();
            btnLends = new Button();
            btnBooks = new Button();
            lblWelcome = new Label();
            SuspendLayout();
            // 
            // btnUsers
            // 
            btnUsers.Anchor = AnchorStyles.None;
            btnUsers.Location = new Point(227, 220);
            btnUsers.Name = "btnUsers";
            btnUsers.Size = new Size(75, 23);
            btnUsers.TabIndex = 0;
            btnUsers.Text = "Utilisateurs";
            btnUsers.UseVisualStyleBackColor = true;
            btnUsers.Click += btnUsers_Click;
            // 
            // btnLends
            // 
            btnLends.Anchor = AnchorStyles.None;
            btnLends.Location = new Point(356, 220);
            btnLends.Name = "btnLends";
            btnLends.Size = new Size(75, 23);
            btnLends.TabIndex = 1;
            btnLends.Text = "Emprunts";
            btnLends.UseVisualStyleBackColor = true;
            btnLends.Click += btnLends_Click;
            // 
            // btnBooks
            // 
            btnBooks.Anchor = AnchorStyles.None;
            btnBooks.Location = new Point(484, 220);
            btnBooks.Name = "btnBooks";
            btnBooks.Size = new Size(75, 23);
            btnBooks.TabIndex = 2;
            btnBooks.Text = "Livres";
            btnBooks.UseVisualStyleBackColor = true;
            btnBooks.Click += btnBooks_Click;
            // 
            // lblWelcome
            // 
            lblWelcome.Anchor = AnchorStyles.None;
            lblWelcome.AutoSize = true;
            lblWelcome.Location = new Point(269, 103);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(270, 15);
            lblWelcome.TabIndex = 3;
            lblWelcome.Text = "Bienvenue ! Quelle fenêtre souhaitez-vous ouvrir ?";
            lblWelcome.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // frmChoiceMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 434);
            Controls.Add(lblWelcome);
            Controls.Add(btnBooks);
            Controls.Add(btnLends);
            Controls.Add(btnUsers);
            Name = "frmChoiceMenu";
            Text = "Menu";
            Load += frmChoiceMenu_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnUsers;
        private Button btnLends;
        private Button btnBooks;
        private Label lblWelcome;
    }
}
