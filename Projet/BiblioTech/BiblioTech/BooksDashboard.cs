using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BiblioTech
{
    public partial class frmBooksDashboard : Form
    {
        TextBox txtSelectedBookId;
        TableLayoutPanel tblBooks;
        FlowLayoutPanel flpMain;
        Button btnBack; // Bouton de retour
        int spacing = 10;
        int paddingMargin = 15;

        public frmBooksDashboard()
        {
            InitializeComponent();
        }

        private void frmBooksDashboard_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.Padding = new Padding(paddingMargin);

            // Ajouter le bouton directement à la collection Controls du Form (pas dans flpMain)
            this.Controls.Add(btnBack);

            // Création du FlowLayoutPanel principal
            flpMain = new FlowLayoutPanel();
            flpMain.Dock = DockStyle.Fill;
            flpMain.FlowDirection = FlowDirection.LeftToRight; // Modification pour un flux horizontal
            flpMain.Padding = new Padding(paddingMargin);
            flpMain.Margin = new Padding(paddingMargin);
            flpMain.BorderStyle = BorderStyle.FixedSingle;
            flpMain.AutoScroll = true;
            this.Controls.Add(flpMain);

            // Création de la TextBox
            txtSelectedBookId = new TextBox();
            txtSelectedBookId.ReadOnly = true;
            txtSelectedBookId.Multiline = true;
            txtSelectedBookId.WordWrap = true;
            txtSelectedBookId.ScrollBars = ScrollBars.Vertical;
            txtSelectedBookId.Width = (int)(flpMain.Width * 0.3) - (2 * paddingMargin); // Redimensionnement
            txtSelectedBookId.Height = flpMain.Height - (4 * paddingMargin); // Ajustement de la hauteur

            flpMain.Controls.Add(txtSelectedBookId);

            // Création du TableLayoutPanel pour les livres
            tblBooks = new TableLayoutPanel();
            tblBooks.Dock = DockStyle.Fill; // Permet à la table de s'étendre correctement
            tblBooks.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50)); // Première colonne à 50%
            tblBooks.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50)); // Première colonne à 50%
            tblBooks.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            tblBooks.Width = (int)(flpMain.Width * 0.3) - (2 * paddingMargin); // Redimensionnement
            tblBooks.Height = flpMain.Height - (4 * paddingMargin);
            tblBooks.RowCount = 0;
            tblBooks.AutoScroll = true;

            // Liste des livres
            List<Books> books = Books.GetAll();
            foreach (var book in books)
            {
                Label lblBook = new Label();
                lblBook.Text = book.Titre + " - " + book.Auteur;
                lblBook.Anchor = AnchorStyles.None;
                lblBook.Padding = new Padding(5);
                lblBook.AutoSize = true;

                Button btnShowBookInfo = new Button();
                btnShowBookInfo.Text = "Afficher infos";
                btnShowBookInfo.AutoSize = true;
                btnShowBookInfo.Padding = new Padding(5);
                btnShowBookInfo.Anchor = AnchorStyles.None;
                btnShowBookInfo.Click += (s, ev) =>
                {
                    // Mettre à jour le texte de la TextBox avec des informations sur le livre
                    txtSelectedBookId.Text = "INFOS DU LIVRE" +
                    "\r\n\r\n- ID : " + book.Id.ToString() +
                    "\r\n\r\n- Titre : " + book.Titre +
                    "\r\n\r\n- Genre : " + book.Genre +
                    "\r\n\r\n- Auteur : " + book.Auteur +
                    "\r\n\r\n- Date de publication : " + book.DatePublication.ToShortDateString();
                };

                // Ajouter le livre dans le TableLayoutPanel
                int rowIndex = tblBooks.RowCount;
                tblBooks.RowCount++;
                tblBooks.Controls.Add(lblBook, 0, rowIndex);
                tblBooks.Controls.Add(btnShowBookInfo, 1, rowIndex);
            }

            // Limiter la largeur du TableLayoutPanel des livres pour laisser de l'espace pour le formulaire d'ajout
            int tblWidth = (int)(this.ClientSize.Width * 0.33) - (2 * paddingMargin);
            tblBooks.Width = tblWidth;

            // Ajouter le TableLayoutPanel directement au FlowLayoutPanel
            flpMain.Controls.Add(tblBooks);

            // Création du panneau du formulaire de livre
            Panel pnlAddBook = new Panel();
            pnlAddBook.Width = (int)(flpMain.Width * 0.3) - (2 * paddingMargin); // Redimensionnement
            pnlAddBook.Height = flpMain.Height - (4 * paddingMargin);
            pnlAddBook.AutoScroll = true;
            pnlAddBook.BorderStyle = BorderStyle.FixedSingle;


            Label lblTitre = new Label() { Text = "Titre:", Left = 10, Top = 20 };
            lblTitre.BorderStyle = BorderStyle.FixedSingle;
            lblTitre.AutoSize = true;
            TextBox txtTitre = new TextBox() { Left = 120, Top = 20, Width = 200 };

            Label lblAuteur = new Label() { Text = "Auteur:", Left = 10, Top = 50 };
            lblAuteur.BorderStyle = BorderStyle.FixedSingle;
            TextBox txtAuteur = new TextBox() { Left = 120, Top = 50, Width = 200 };

            Label lblGenre = new Label() { Text = "Genre:", Left = 10, Top = 80 };
            lblGenre.BorderStyle = BorderStyle.FixedSingle;
            TextBox txtGenre = new TextBox() { Left = 120, Top = 80, Width = 200 };

            Label lblDatePublication = new Label() { Text = "Date de Publication:", Left = 10, Top = 110 };
            lblDatePublication.BorderStyle = BorderStyle.FixedSingle;

            TextBox txtDatePublication = new TextBox() { Left = 120, Top = 110, Width = 200 };

            Button btnSubmit = new Button() { Text = "Soumettre", Left = 120, Top = 150 };
            btnSubmit.Click += (sender, e) =>
            {
                // Récupérer les informations des champs de texte
                string titre = txtTitre.Text;
                string auteur = txtAuteur.Text;
                string genre = txtGenre.Text;
                string datePublication = txtDatePublication.Text;

                // Requête d'insertion dans la base de données (sans ISBN)
                string query = "INSERT INTO livres (titre, auteur, genre, date_publication,etat ) VALUES (@Titre, @Auteur, @Genre, @DatePublication, True)";

                // Créer et préparer la commande SQL
                MySqlCommand cmd = new MySqlCommand(query, Program.conn);
                cmd.Parameters.AddWithValue("@Titre", titre);
                cmd.Parameters.AddWithValue("@Auteur", auteur);
                cmd.Parameters.AddWithValue("@Genre", genre);
                cmd.Parameters.AddWithValue("@DatePublication", DateTime.Parse(datePublication));

                // Exécution de la commande d'insertion
                cmd.ExecuteNonQuery();

                // Message de confirmation
                MessageBox.Show("Les informations ont été enregistrées dans la base de données.");

                frmChoiceMenu frmChoiceMenu = new frmChoiceMenu();
                frmChoiceMenu.Show();
                frmChoiceMenu.Closed += (s, args) => this.Close();
            };

            // Ajouter les contrôles au panneau
            pnlAddBook.Controls.Add(lblTitre);
            pnlAddBook.Controls.Add(txtTitre);
            pnlAddBook.Controls.Add(lblAuteur);
            pnlAddBook.Controls.Add(txtAuteur);
            pnlAddBook.Controls.Add(lblGenre);
            pnlAddBook.Controls.Add(txtGenre);
            pnlAddBook.Controls.Add(lblDatePublication);
            pnlAddBook.Controls.Add(txtDatePublication);
            pnlAddBook.Controls.Add(btnSubmit);

            // Ajouter le panneau au FlowLayoutPanel
            flpMain.Controls.Add(pnlAddBook);
        }



    }
}
