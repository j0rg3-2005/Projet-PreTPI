namespace BiblioTech
{
    partial class Demarrage
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
            lblBienvenue = new Label();
            btnSuivant = new Button();
            SuspendLayout();
            // 
            // lblBienvenue
            // 
            lblBienvenue.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblBienvenue.AutoSize = true;
            lblBienvenue.Font = new Font("SimSun", 72F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblBienvenue.Location = new Point(379, 133);
            lblBienvenue.Name = "lblBienvenue";
            lblBienvenue.Size = new Size(1267, 97);
            lblBienvenue.TabIndex = 0;
            lblBienvenue.Text = "Bienvenue dans BiblioTech";
            lblBienvenue.Click += lblBienvenue_Click;
            // 
            // btnSuivant
            // 
            btnSuivant.AutoSize = true;
            btnSuivant.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnSuivant.Font = new Font("Bodoni MT", 27.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSuivant.Location = new Point(941, 467);
            btnSuivant.Name = "btnSuivant";
            btnSuivant.Size = new Size(152, 54);
            btnSuivant.TabIndex = 1;
            btnSuivant.Text = "Suivant";
            btnSuivant.UseVisualStyleBackColor = true;
            btnSuivant.Click += btnSuivant_Click;
            // 
            // Demarrage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Tan;
            ClientSize = new Size(1463, 687);
            Controls.Add(btnSuivant);
            Controls.Add(lblBienvenue);
            Name = "Demarrage";
            Text = "frmDemarrage";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblBienvenue;
        private Button btnSuivant;
    }
}
