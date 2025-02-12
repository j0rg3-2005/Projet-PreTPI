using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BiblioTech
{
    public partial class frmUsersDashboard : Form
    {
        TextBox txtSelectedUserId;  // Déclaration de la TextBox en tant que membre de la classe
        TableLayoutPanel tblUsers;  // Déclaration du TableLayoutPanel comme membre de la classe

        public frmUsersDashboard()
        {
            InitializeComponent();
        }

        private void frmUsersDashboard_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            // Ajouter la TextBox à gauche du tableau (en dehors du TableLayoutPanel)
            txtSelectedUserId = new TextBox();
            txtSelectedUserId.Width = 400;  // Largeur de la TextBox
            txtSelectedUserId.Height = 700; // Hauteur suffisante pour plusieurs lignes
            txtSelectedUserId.Location = new Point(10, 10); // Positionner la TextBox à gauche
            txtSelectedUserId.ReadOnly = true; // Empêcher la modification de l'ID
            txtSelectedUserId.Multiline = true; // Permettre le retour à la ligne
            txtSelectedUserId.WordWrap = true; // Permettre le retour à la ligne automatique
            txtSelectedUserId.ScrollBars = ScrollBars.Vertical; // Activer les barres de défilement verticales si nécessaire

            // Ajouter la TextBox au formulaire
            this.Controls.Add(txtSelectedUserId);

            // Créer le TableLayoutPanel
            tblUsers = new TableLayoutPanel();  // Utilisation de tblUsers comme membre
            tblUsers.AutoSize = true;
            tblUsers.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tblUsers.ColumnCount = 2; // 2 colonnes : Label, Button
            tblUsers.RowCount = 0;
            tblUsers.Padding = new Padding(10);
            tblUsers.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single; // Debug

            // Ajouter chaque utilisateur dans une nouvelle ligne
            List<Users> utilisateurs = Users.GetAll();
            int maxWidthLabel = 0;
            int maxWidthButton = 0;

            foreach (var utilisateur in utilisateurs)
            {
                // Créer le Label
                Label lblUser = new Label();
                lblUser.Text = utilisateur.Prenom + " " + utilisateur.Nom;
                lblUser.AutoSize = true;
                lblUser.Padding = new Padding(5);
                lblUser.TextAlign = ContentAlignment.MiddleLeft;
                lblUser.Height = 30;

                // Créer le bouton "Afficher infos"
                Button btnShowUserInfo = new Button();
                btnShowUserInfo.Text = "Afficher infos";
                btnShowUserInfo.AutoSize = true;
                btnShowUserInfo.Padding = new Padding(5);
                btnShowUserInfo.Click += (sender, e) =>
                {
                    // Mettre à jour le texte de la TextBox avec des informations
                    txtSelectedUserId.Text = "Infos de l'utilisateur : " +
                    Environment.NewLine + "ID : " + utilisateur.Id.ToString() +
                    Environment.NewLine + "Prénom : " + utilisateur.Prenom +
                    Environment.NewLine + "Nom : " + utilisateur.Nom +
                    Environment.NewLine + "Num. de téléphone : " + utilisateur.Telephone.ToString() +
                    Environment.NewLine + "Adresse postale : " + utilisateur.AdressePostale +
                    Environment.NewLine + "Courriel : " + utilisateur.AdresseEmail;
                };

                // Ajouter le Label et le Button dans une nouvelle ligne du TableLayoutPanel
                int rowIndex = tblUsers.RowCount;
                tblUsers.RowCount++;
                tblUsers.Controls.Add(lblUser, 0, rowIndex); // Colonne 0 : Label
                tblUsers.Controls.Add(btnShowUserInfo, 1, rowIndex); // Colonne 1 : Button

                // Mettre à jour la largeur maximale trouvée
                maxWidthLabel = Math.Max(maxWidthLabel, lblUser.Width);
                maxWidthButton = Math.Max(maxWidthButton, btnShowUserInfo.PreferredSize.Width);
            }

            // Définir la largeur des colonnes
            tblUsers.ColumnStyles.Clear();
            tblUsers.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, maxWidthLabel + 20)); // Colonne ajustée au label
            tblUsers.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, maxWidthButton + 20)); // Colonne ajustée au bouton

            // Ajouter le TableLayoutPanel au FlowLayoutPanel (si nécessaire)
            flnpnlUsers.Controls.Clear();
            flnpnlUsers.Controls.Add(tblUsers);

            // Appliquer la taille et position du FlowLayoutPanel
            SetFlowLayoutPanelSize(tblUsers);

            // Gérer le redimensionnement
            this.Resize += new EventHandler(frmUsersDashboard_Resize);
        }

        private void frmUsersDashboard_Resize(object sender, EventArgs e)
        {
            // Calculer la nouvelle taille et position de la TextBox
            int marge = 20; // Marge entre la TextBox et le tableau
            txtSelectedUserId.Width = this.ClientSize.Width / 3 - marge; // Largeur de la TextBox
            txtSelectedUserId.Height = this.ClientSize.Height - tblUsers.Bottom - marge; // Hauteur de la TextBox
            txtSelectedUserId.Location = new Point(10, tblUsers.Bottom + marge); // Positionner la TextBox sous le TableLayoutPanel
        }

        private void SetFlowLayoutPanelSize(TableLayoutPanel tblUsers)
        {
            int marge = 40; // 2 cm ≈ 40 pixels
            flnpnlUsers.Width = tblUsers.PreferredSize.Width + 20; // Ajuster la largeur au contenu
            flnpnlUsers.Height = this.ClientSize.Height - flnpnlUsers.Top - marge; // Ajuster la hauteur
            flnpnlUsers.Left = (this.ClientSize.Width - flnpnlUsers.Width) / 2; // Centrer
        }
    }

}
