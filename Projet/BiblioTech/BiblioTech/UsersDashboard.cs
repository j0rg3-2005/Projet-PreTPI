using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BiblioTech
{
    public partial class frmUsersDashboard : Form
    {
        public frmUsersDashboard()
        {
            InitializeComponent();
        }

        private void frmUsersDashboard_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;


            // Récupérer tous les utilisateurs
            List<Users> utilisateurs = Users.GetAll();

            // Ajouter les utilisateurs au FlowLayoutPanel avec un Label et un bouton
            foreach (var utilisateur in utilisateurs)
            {
                // Créer le label avec le nom et prénom
                Label lblUser = new Label();
                lblUser.Text = utilisateur.Prenom + " " + utilisateur.Nom;
                lblUser.AutoSize = true;

                // Créer le bouton "Afficher infos"
                Button btnAfficherInfos = new Button();
                btnAfficherInfos.Text = "Afficher infos";
                btnAfficherInfos.Width = 120;
                btnAfficherInfos.Click += (sender, e) =>
                {
                    MessageBox.Show("Affichage des infos de " + utilisateur.Prenom + " " + utilisateur.Nom);
                };

                // Créer un panel pour chaque utilisateur, avec le label et le bouton à côté
                Panel userPanel = new Panel();
                userPanel.Width = Math.Max(lblUser.Width + btnAfficherInfos.Width + 30, flnpnlUsers.Width - 20);
                userPanel.Height = Math.Max(lblUser.Height, btnAfficherInfos.Height) + 10;

                // Positionner les éléments avec un espacement réduit
                lblUser.Location = new Point(10, 5);  // Réduire l'espacement vertical
                btnAfficherInfos.Location = new Point(lblUser.Width + 25, 5);

                // Ajouter les contrôles au panel
                userPanel.Controls.Add(lblUser);
                userPanel.Controls.Add(btnAfficherInfos);

                // Ajouter le panel au FlowLayoutPanel
                flnpnlUsers.Controls.Add(userPanel);
            }


            // Appliquer la taille et position du FlowLayoutPanel
            SetFlowLayoutPanelSize();

            // Gérer le redimensionnement
            this.Resize += new EventHandler(frmUsersDashboard_Resize);
        }

        private void frmUsersDashboard_Resize(object sender, EventArgs e)
        {
            SetFlowLayoutPanelSize();
        }

        private void SetFlowLayoutPanelSize()
        {
            int panelHeight = this.ClientSize.Height - flnpnlUsers.Top - 30; // Hauteur du FlowLayoutPanel
            flnpnlUsers.Width = Math.Min(this.ClientSize.Width - 20, 300);  // Largeur limitée
            flnpnlUsers.Height = panelHeight; // Ajuster la hauteur
            flnpnlUsers.Left = (this.ClientSize.Width - flnpnlUsers.Width) / 2; // Centrer horizontalement
        }
    }
}
