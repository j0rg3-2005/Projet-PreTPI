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
        Button btnBack;
        int paddingMargin = 15;

        public frmUsersDashboard()
        {
            InitializeComponent();
        }

        private void frmUsersDashboard_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.Padding = new Padding(paddingMargin);

            btnBack = new Button();
            btnBack.Text = "Retour";
            btnBack.AutoSize = true;
            btnBack.Padding = new Padding(5);
            btnBack.Click += btnBack_Click;
            btnBack.Location = new Point(paddingMargin, paddingMargin);
            this.Controls.Add(btnBack);

            flpMain = new FlowLayoutPanel();
            flpMain.Dock = DockStyle.Fill;
            flpMain.FlowDirection = FlowDirection.LeftToRight;
            flpMain.AutoScroll = true;
            flpMain.Padding = new Padding(paddingMargin, paddingMargin * 2, paddingMargin, paddingMargin);
            this.Controls.Add(flpMain);

            // Création de la TextBox
            txtSelectedUserId = new TextBox();
            txtSelectedUserId.Text = "Veuillez sélectionner un utilisateur afin d'afficher les informations.";
            txtSelectedUserId.ReadOnly = true;
            txtSelectedUserId.Multiline = true;
            txtSelectedUserId.WordWrap = true;
            txtSelectedUserId.ScrollBars = ScrollBars.Vertical;
            txtSelectedUserId.Width = (int)(this.ClientSize.Width * 0.35) - (2 * paddingMargin);
            txtSelectedUserId.Height = this.ClientSize.Height - 100 - (2 * paddingMargin);
            txtSelectedUserId.Margin = new Padding(10);

            flpMain.Controls.Add(txtSelectedUserId);

            tblUsers = new TableLayoutPanel();
            tblUsers.AutoSize = false;
            tblUsers.Dock = DockStyle.Fill;
            tblUsers.RowCount = 0;
            tblUsers.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            tblUsers.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            tblUsers.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            tblUsers.Padding = new Padding(10);
            tblUsers.Margin = new Padding(5);
            tblUsers.AutoScroll = true;

            List<Users> utilisateurs = Users.GetAll();
            foreach (var utilisateur in utilisateurs)
            {
                Label lblUser = new Label();
                lblUser.Text = utilisateur.Prenom + " " + utilisateur.Nom;
                lblUser.Anchor = AnchorStyles.None;
                lblUser.Padding = new Padding(5);
                lblUser.AutoSize = true;

                Button btnShowUserInfo = new Button();
                btnShowUserInfo.Text = "Afficher infos";
                btnShowUserInfo.AutoSize = true;
                btnShowUserInfo.Padding = new Padding(5);
                btnShowUserInfo.Anchor = AnchorStyles.None;
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

                int rowIndex = tblUsers.RowCount;
                tblUsers.RowCount++;
                tblUsers.Controls.Add(lblUser, 0, rowIndex);
                tblUsers.Controls.Add(btnShowUserInfo, 1, rowIndex);
            }

            int tblWidth = (int)(this.ClientSize.Width * 0.33) - (2 * paddingMargin);
            tblUsers.Width = tblWidth;

            flpMain.Controls.Add(tblUsers);

            Panel pnlAddUser = new Panel();
            pnlAddUser.Width = (int)(flpMain.Width * 0.3) - (2 * paddingMargin);
            pnlAddUser.Height = flpMain.Height - (4 * paddingMargin);
            pnlAddUser.AutoScroll = true;
            pnlAddUser.BorderStyle = BorderStyle.FixedSingle;

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
                string nom = txtNom.Text;
                string prenom = txtPrenom.Text;
                string telephone = txtTelephone.Text;
                string adresse = txtAdresse.Text;
                string email = txtEmail.Text;

                // Vérification si tous les champs sont remplis
                if (string.IsNullOrEmpty(nom) || string.IsNullOrEmpty(prenom) || string.IsNullOrEmpty(telephone) ||
                    string.IsNullOrEmpty(adresse) || string.IsNullOrEmpty(email))
                {
                    MessageBox.Show("Tous les champs doivent être remplis.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string query = "INSERT INTO utilisateurs (nom, prenom, telephone, adresse_postale, adresse_email) VALUES (@Nom, @Prenom, @Telephone, @Adresse, @Email)";

                MySqlCommand cmd = new MySqlCommand(query, Program.conn);
                cmd.Parameters.AddWithValue("@Nom", nom);
                cmd.Parameters.AddWithValue("@Prenom", prenom);
                cmd.Parameters.AddWithValue("@Telephone", telephone);
                cmd.Parameters.AddWithValue("@Adresse", adresse);
                cmd.Parameters.AddWithValue("@Email", email);

                cmd.ExecuteNonQuery();

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
