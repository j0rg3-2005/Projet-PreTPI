using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace BiblioTech
{
    public partial class frmLendsDashboard : Form
    {
        TextBox txtSelectedLendId;
        TableLayoutPanel tblLends;

        Button btnBack; // Déclaration du bouton de retour
        int spacing = 20; // Espacement entre la TextBox et la liste
        int paddingMargin = 20; // Marge globale

        public frmLendsDashboard()
        {
            InitializeComponent();
        }

        private void frmLendsDashboard_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.Padding = new Padding(paddingMargin); // Ajouter une marge globale autour du formulaire

            // Création du bouton de retour en haut à droite
            btnBack = new Button();
            btnBack.Text = "Retour";
            btnBack.AutoSize = true;
            btnBack.Padding = new Padding(5);
            btnBack.Click += btnBack_Click;
            btnBack.Location = new Point(paddingMargin, paddingMargin); // Positionné en haut à gauche
            this.Controls.Add(btnBack);

            // Création de la TextBox
            txtSelectedLendId = new TextBox();
            txtSelectedLendId.ReadOnly = true;
            txtSelectedLendId.Multiline = true;
            txtSelectedLendId.WordWrap = true;
            txtSelectedLendId.ScrollBars = ScrollBars.Vertical;
            txtSelectedLendId.Width = (int)(this.ClientSize.Width * 0.3) - (2 * paddingMargin);
            txtSelectedLendId.Height = this.ClientSize.Height - 50 - (2 * paddingMargin); // Ajustement dynamique
            txtSelectedLendId.Location = new Point(paddingMargin, paddingMargin + btnBack.Height); // Juste en dessous du bouton Retour

            this.Controls.Add(txtSelectedLendId);

            // Création du TableLayoutPanel
            tblLends = new TableLayoutPanel();
            tblLends.AutoSize = true;
            tblLends.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tblLends.ColumnCount = 2;
            tblLends.RowCount = 0;
            tblLends.Padding = new Padding(10);
            tblLends.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

            // Ajout des livres
            List<Lends> lends = Lends.GetAll();
            List<Books> books = Books.GetAll();
            List<Users> users = Users.GetAll();
            int maxWidthLabel = 0;
            int maxWidthButton = 0;

            foreach (var lend in lends)
            {
                Label lblBook = new Label();
                lblBook.Text = books[lend.IdLivre].Titre + Environment.NewLine + users[lend.IdUtilisateur].Prenom + " " + users[lend.IdUtilisateur].Nom;
                lblBook.AutoSize = true;
                lblBook.Padding = new Padding(5);
                lblBook.TextAlign = ContentAlignment.MiddleLeft;

                Button btnShowBookInfo = new Button();
                btnShowBookInfo.Text = "Afficher infos";
                btnShowBookInfo.AutoSize = true;
                btnShowBookInfo.Padding = new Padding(5);
                btnShowBookInfo.Click += (s, ev) =>
                {
                    // Mettre à jour le texte de la TextBox avec des informations
                    txtSelectedLendId.Text = "INFOS DU LIVRE" +
                    "\r\n\r\n- ID : " + lend.Id.ToString()
                    + "\r\n\r\n- Livre emprunté : " + books[lend.IdLivre].Titre
                    + "\r\n\r\n- Utilisateur : " + users[lend.IdUtilisateur].Prenom + " " + users[lend.IdUtilisateur].Nom
                    + "\r\n\r\n- Date de début : " + lend.DateDebut
                    + "\r\n\r\n- Date de fin : " + lend.DateFin;
                };

                int rowIndex = tblLends.RowCount;
                tblLends.RowCount++;
                tblLends.Controls.Add(lblBook, 0, rowIndex);
                tblLends.Controls.Add(btnShowBookInfo, 1, rowIndex);

                maxWidthLabel = Math.Max(maxWidthLabel, lblBook.Width);
                maxWidthButton = Math.Max(maxWidthButton, btnShowBookInfo.PreferredSize.Width);
            }

            // Définition des largeurs des colonnes
            tblLends.ColumnStyles.Clear();
            tblLends.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, maxWidthLabel + 20));
            tblLends.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, maxWidthButton + 20));

            // Création du FlowLayoutPanel
            flnpnlLends.AutoScroll = true;
            flnpnlLends.Location = new Point(txtSelectedLendId.Right + spacing, txtSelectedLendId.Top); // Placer à droite de la TextBox
            flnpnlLends.Width = this.ClientSize.Width - txtSelectedLendId.Width - (3 * paddingMargin);
            flnpnlLends.Controls.Add(tblLends);


            // Ajustement des tailles et positions
            AdjustLayout();

            this.Resize += new EventHandler(frmLendsDashboard_Resize);
        }

        private void frmLendsDashboard_Resize(object sender, EventArgs e)
        {
            AdjustLayout();
        }

        private void AdjustLayout()
        {
            int marginBottom = 40; // Marge du bas

            // Ajuster la largeur de la TextBox (prend 30% de la largeur de la fenêtre)
            txtSelectedLendId.Width = (int)(this.ClientSize.Width * 0.3) - (2 * paddingMargin);
            txtSelectedLendId.Height = this.ClientSize.Height - marginBottom - (2 * paddingMargin);

            // Ajuster la position du FlowLayoutPanel
            flnpnlLends.Left = txtSelectedLendId.Right + spacing;
            flnpnlLends.Top = txtSelectedLendId.Top;
            flnpnlLends.Width = this.ClientSize.Width - flnpnlLends.Left - paddingMargin;
            flnpnlLends.Height = this.ClientSize.Height - flnpnlLends.Top - marginBottom;

            // Empêcher la liste de dépasser la fenêtre
            if (flnpnlLends.Right > this.ClientSize.Width)
            {
                flnpnlLends.Width = this.ClientSize.Width - flnpnlLends.Left - paddingMargin;
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            frmChoiceMenu frmChoiceMenu = new frmChoiceMenu();
            frmChoiceMenu.Show();
            frmChoiceMenu.Closed += (s, args) => this.Close();
        }
    }
}
