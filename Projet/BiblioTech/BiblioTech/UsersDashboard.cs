using MySqlConnector;
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
        FlowLayoutPanel flpMain;
        Button btnBack; // Bouton de retour
        int spacing = 10;
        int paddingMargin = 15;

        public frmUsersDashboard()
        {
            InitializeComponent();
        }

        private void frmUsersDashboard_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.Padding = new Padding(paddingMargin);

            // Création du bouton de retour en haut à gauche
            btnBack = new Button();
            btnBack.Text = "Retour";
            btnBack.AutoSize = true;
            btnBack.Padding = new Padding(5);
            btnBack.Click += btnBack_Click;
            btnBack.Location = new Point(paddingMargin, paddingMargin); // Positionné en haut à gauche
            this.Controls.Add(btnBack);

            // Création du FlowLayoutPanel principal
            flpMain = new FlowLayoutPanel();
            flpMain.Dock = DockStyle.Fill;
            flpMain.FlowDirection = FlowDirection.LeftToRight; // Modification pour un flux horizontal
            flpMain.AutoScroll = true;
            flpMain.Padding = new Padding(paddingMargin);
            this.Controls.Add(flpMain);

            // Création de la TextBox
            txtSelectedUserId = new TextBox();
            txtSelectedUserId.ReadOnly = true;
            txtSelectedUserId.Multiline = true;
            txtSelectedUserId.WordWrap = true;
            txtSelectedUserId.ScrollBars = ScrollBars.Vertical;
            txtSelectedUserId.Width = (int)(this.ClientSize.Width * 0.35) - (2 * paddingMargin); // Redimensionnement
            txtSelectedUserId.Height = this.ClientSize.Height - 100 - (2 * paddingMargin); // Ajustement de la hauteur
            txtSelectedUserId.Margin = new Padding(10); // Marge autour de la TextBox

            flpMain.Controls.Add(txtSelectedUserId);

            // Création du TableLayoutPanel pour les utilisateurs
            tblUsers = new TableLayoutPanel();
            tblUsers.AutoSize = false; // Désactive l'auto-redimensionnement
            tblUsers.Dock = DockStyle.Fill; // Permet à la table de s'étendre correctement
            tblUsers.ColumnCount = 2;
            tblUsers.RowCount = 0;
            tblUsers.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            tblUsers.Padding = new Padding(10); // Marges internes réduites
            tblUsers.Margin = new Padding(5); // Marges autour du TableLayoutPanel
            tblUsers.AutoScroll = true;

            // Liste des utilisateurs
            List<Users> utilisateurs = Users.GetAll();
            foreach (var utilisateur in utilisateurs)
            {
                Label lblUser = new Label();
                lblUser.Text = utilisateur.Prenom + " " + utilisateur.Nom;

                Button btnShowUserInfo = new Button();
                btnShowUserInfo.Text = "Afficher infos";
                btnShowUserInfo.AutoSize = true;
                btnShowUserInfo.Padding = new Padding(5);
                btnShowUserInfo.Click += (s, ev) =>
                {
                    txtSelectedUserId.Text = "Infos de l'utilisateur" + Environment.NewLine +
                    "\r\n- ID : " + utilisateur.Id +
                    "\r\n- Prénom : " + utilisateur.Prenom +
                    "\r\n- Nom : " + utilisateur.Nom +
                    "\r\n- Num. de téléphone : " + utilisateur.Telephone +
                    "\r\n- Adresse postale : " + utilisateur.AdressePostale +
                    "\r\n- Courriel : " + utilisateur.AdresseEmail;
                };

                // Ajouter l'utilisateur dans le TableLayoutPanel
                int rowIndex = tblUsers.RowCount;
                tblUsers.RowCount++;
                tblUsers.Controls.Add(lblUser, 0, rowIndex);
                tblUsers.Controls.Add(btnShowUserInfo, 1, rowIndex);
            }

            // Limiter la largeur du TableLayoutPanel des utilisateurs pour laisser de l'espace pour le formulaire d'ajout
            int tblWidth = (int)(this.ClientSize.Width * 0.33) - (2 * paddingMargin);
            tblUsers.Width = tblWidth;

            // Ajouter le TableLayoutPanel directement au FlowLayoutPanel
            flpMain.Controls.Add(tblUsers);

            // Création du panneau du formulaire utilisateur
            Panel pnlAddUser = new Panel();
            pnlAddUser.Width = 300;
            pnlAddUser.Height = this.ClientSize.Height - 50;
            pnlAddUser.Margin = new Padding(10);
            pnlAddUser.AutoScroll = true;
            pnlAddUser.AutoSize = true;
            pnlAddUser.Padding = new Padding(5);

            Label lblNom = new Label() { Text = "Nom:", Left = 10, Top = 20 };
            TextBox txtNom = new TextBox() { Left = 120, Top = 20, Width = 200 };

            Label lblPrenom = new Label() { Text = "Prénom:", Left = 10, Top = 50 };
            TextBox txtPrenom = new TextBox() { Left = 120, Top = 50, Width = 200 };

            Label lblTelephone = new Label() { Text = "Téléphone:", Left = 10, Top = 80 };
            TextBox txtTelephone = new TextBox() { Left = 120, Top = 80, Width = 200 };

            Label lblAdresse = new Label() { Text = "Adresse Postale:", Left = 10, Top = 110 };
            TextBox txtAdresse = new TextBox() { Left = 120, Top = 110, Width = 200 };

            Label lblEmail = new Label() { Text = "Adresse Email:", Left = 10, Top = 140 };
            TextBox txtEmail = new TextBox() { Left = 120, Top = 140, Width = 200 };

            Button btnSubmit = new Button() { Text = "Soumettre", Left = 120, Top = 180 };
            btnSubmit.Click += (sender, e) =>
            {
                // Récupérer les informations des champs de texte
                string nom = txtNom.Text;
                string prenom = txtPrenom.Text;
                string telephone = txtTelephone.Text;
                string adresse = txtAdresse.Text;
                string email = txtEmail.Text;

                // Requête d'insertion dans la base de données
                string query = "INSERT INTO utilisateurs (nom, prenom, telephone, adresse_postale, adresse_email) VALUES (@Nom, @Prenom, @Telephone, @Adresse, @Email)";

                // Créer et préparer la commande SQL
                MySqlCommand cmd = new MySqlCommand(query, Program.conn);
                cmd.Parameters.AddWithValue("@Nom", nom);
                cmd.Parameters.AddWithValue("@Prenom", prenom);
                cmd.Parameters.AddWithValue("@Telephone", telephone);
                cmd.Parameters.AddWithValue("@Adresse", adresse);
                cmd.Parameters.AddWithValue("@Email", email);

                // Exécution de la commande d'insertion
                cmd.ExecuteNonQuery();

                // Message de confirmation
                MessageBox.Show("Les informations ont été enregistrées dans la base de données.");
            };

            pnlAddUser.Controls.Add(lblNom);
            pnlAddUser.Controls.Add(txtNom);
            pnlAddUser.Controls.Add(lblPrenom);
            pnlAddUser.Controls.Add(txtPrenom);
            pnlAddUser.Controls.Add(lblTelephone);
            pnlAddUser.Controls.Add(txtTelephone);
            pnlAddUser.Controls.Add(lblAdresse);
            pnlAddUser.Controls.Add(txtAdresse);
            pnlAddUser.Controls.Add(lblEmail);
            pnlAddUser.Controls.Add(txtEmail);
            pnlAddUser.Controls.Add(btnSubmit);

            flpMain.Controls.Add(pnlAddUser);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            frmChoiceMenu frmChoiceMenu = new frmChoiceMenu();
            frmChoiceMenu.Show();
            frmChoiceMenu.Closed += (s, args) => this.Close();
        }
    }
}
