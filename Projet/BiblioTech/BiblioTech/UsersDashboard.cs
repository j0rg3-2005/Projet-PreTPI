using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BiblioTech
{
    public partial class frmUsersDashboard : Form
    {
        TextBox txtSelectedUserId;
        TableLayoutPanel tblUsers;
        int spacing = 20; // Espacement entre la TextBox et la liste

        public frmUsersDashboard()
        {
            InitializeComponent();
        }

        private void frmUsersDashboard_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            // Création de la TextBox
            txtSelectedUserId = new TextBox();
            txtSelectedUserId.ReadOnly = true;
            txtSelectedUserId.Multiline = true;
            txtSelectedUserId.WordWrap = true;
            txtSelectedUserId.ScrollBars = ScrollBars.Vertical;
            txtSelectedUserId.Width = 300; // Valeur initiale
            txtSelectedUserId.Height = this.ClientSize.Height - 50; // Ajustement dynamique
            txtSelectedUserId.Location = new Point(10, 10);

            this.Controls.Add(txtSelectedUserId);

            // Création du TableLayoutPanel
            tblUsers = new TableLayoutPanel();
            tblUsers.AutoSize = true;
            tblUsers.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tblUsers.ColumnCount = 2;
            tblUsers.RowCount = 0;
            tblUsers.Padding = new Padding(10);
            tblUsers.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

            // Ajout des utilisateurs
            List<Users> utilisateurs = Users.GetAll();
            int maxWidthLabel = 0;
            int maxWidthButton = 0;

            foreach (var utilisateur in utilisateurs)
            {
                Label lblUser = new Label();
                lblUser.Text = utilisateur.Prenom + " " + utilisateur.Nom;
                lblUser.AutoSize = true;
                lblUser.Padding = new Padding(5);
                lblUser.TextAlign = ContentAlignment.MiddleLeft;

                Button btnShowUserInfo = new Button();
                btnShowUserInfo.Text = "Afficher infos";
                btnShowUserInfo.AutoSize = true;
                btnShowUserInfo.Padding = new Padding(5);
                btnShowUserInfo.Click += (s, ev) =>
                {
                    // Mettre à jour le texte de la TextBox avec des informations
                    txtSelectedUserId.Text = "Infos de l'utilisateur" + Environment.NewLine +
                    Environment.NewLine + "- ID : " + utilisateur.Id.ToString() + Environment.NewLine +
                    Environment.NewLine + "- Prénom : " + utilisateur.Prenom + Environment.NewLine +
                    Environment.NewLine + "- Nom : " + utilisateur.Nom + Environment.NewLine +
                    Environment.NewLine + "- Num. de téléphone : " + utilisateur.Telephone.ToString() + Environment.NewLine +
                    Environment.NewLine + "- Adresse postale : " + utilisateur.AdressePostale + Environment.NewLine +
                    Environment.NewLine + "- Courriel : " + utilisateur.AdresseEmail;
                };

                int rowIndex = tblUsers.RowCount;
                tblUsers.RowCount++;
                tblUsers.Controls.Add(lblUser, 0, rowIndex);
                tblUsers.Controls.Add(btnShowUserInfo, 1, rowIndex);

                maxWidthLabel = Math.Max(maxWidthLabel, lblUser.Width);
                maxWidthButton = Math.Max(maxWidthButton, btnShowUserInfo.PreferredSize.Width);
            }

            // Définition des largeurs des colonnes
            tblUsers.ColumnStyles.Clear();
            tblUsers.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, maxWidthLabel + 20));
            tblUsers.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, maxWidthButton + 20));

            // Ajout du TableLayoutPanel au FlowLayoutPanel
            flnpnlUsers.Controls.Clear();
            flnpnlUsers.Controls.Add(tblUsers);

            // Ajustement des tailles et positions
            AdjustLayout();

            this.Resize += new EventHandler(frmUsersDashboard_Resize);
        }

        private void frmUsersDashboard_Resize(object sender, EventArgs e)
        {
            AdjustLayout();
        }

        private void AdjustLayout()
        {
            int marginBottom = 40; // Marge du bas

            // Ajuster la largeur de la TextBox (prend 30% de la largeur de la fenêtre)
            txtSelectedUserId.Width = (int)(this.ClientSize.Width * 0.3);
            txtSelectedUserId.Height = this.ClientSize.Height - marginBottom;

            // Positionner la liste après la TextBox avec un espacement
            flnpnlUsers.Left = txtSelectedUserId.Right + spacing;
            flnpnlUsers.Width = tblUsers.PreferredSize.Width + 20;
            flnpnlUsers.Height = this.ClientSize.Height - flnpnlUsers.Top - marginBottom;

            // Empêcher la liste de dépasser la fenêtre
            if (flnpnlUsers.Right > this.ClientSize.Width)
            {
                flnpnlUsers.Width = this.ClientSize.Width - flnpnlUsers.Left - 10;
            }
        }
    }
}
