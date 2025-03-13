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
        Button btnBack;
        Panel pnlAddBook;
        int paddingMargin = 15;
        TextBox txtSearch;
        Button btnSearch;

        public frmBooksDashboard()
        {
            InitializeComponent();
        }

        private void frmBooksDashboard_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.Padding = new Padding(paddingMargin);
            this.Controls.Add(btnBack);

            btnBack = new Button();
            btnBack.Text = "Retour";
            btnBack.AutoSize = true;
            btnBack.Padding = new Padding(5);
            btnBack.Click += btnBack_Click;
            btnBack.Location = new Point(paddingMargin, paddingMargin);
            this.Controls.Add(btnBack);

            txtSearch = new TextBox();
            txtSearch.Width = 250;
            txtSearch.PlaceholderText = "Rechercher un livre par titre, auteur, ID, ou date...";
            txtSearch.Top = paddingMargin;
            txtSearch.Left = (this.ClientSize.Width - txtSearch.Width - 100 - paddingMargin) / 2;
            this.Controls.Add(txtSearch);

            btnSearch = new Button();
            btnSearch.Text = "Rechercher";
            btnSearch.Width = 100;
            btnSearch.Top = paddingMargin;
            btnSearch.Left = txtSearch.Right + paddingMargin;
            btnSearch.Click += BtnSearch_Click;
            this.Controls.Add(btnSearch);

            flpMain = new FlowLayoutPanel();
            flpMain.Dock = DockStyle.Fill;
            flpMain.FlowDirection = FlowDirection.LeftToRight;
            flpMain.Padding = new Padding(paddingMargin, paddingMargin * 2, paddingMargin, paddingMargin);
            flpMain.AutoScroll = true;
            this.Controls.Add(flpMain);

            txtSelectedBookId = new TextBox();
            txtSelectedBookId.Text = "Veuillez sélectionner un livre afin d'afficher les informations.";
            txtSelectedBookId.ReadOnly = true;
            txtSelectedBookId.Multiline = true;
            txtSelectedBookId.WordWrap = true;
            txtSelectedBookId.ScrollBars = ScrollBars.Vertical;
            txtSelectedBookId.Width = (int)(flpMain.Width * 0.3) - (2 * paddingMargin);
            txtSelectedBookId.Height = flpMain.Height - (4 * paddingMargin);
            flpMain.Controls.Add(txtSelectedBookId);

            tblBooks = new TableLayoutPanel();
            tblBooks.Dock = DockStyle.Fill;
            tblBooks.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            tblBooks.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            tblBooks.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            tblBooks.Width = (int)(flpMain.Width * 0.3) - (2 * paddingMargin);
            tblBooks.Height = flpMain.Height - (4 * paddingMargin);
            tblBooks.RowCount = 0;
            tblBooks.AutoScroll = true;

            List<Books> books = Books.GetAll();
            AddBooksToTable(books);
            flpMain.Controls.Add(tblBooks);

            pnlAddBook = new Panel();
            pnlAddBook.Width = (int)(flpMain.Width * 0.3) - (2 * paddingMargin);
            pnlAddBook.Height = flpMain.Height - (4 * paddingMargin);
            pnlAddBook.AutoScroll = true;
            pnlAddBook.BorderStyle = BorderStyle.FixedSingle;

            AddBookFormControls(pnlAddBook);
            flpMain.Controls.Add(pnlAddBook);
        }

        private void AddBooksToTable(List<Books> books)
        {
            flpMain.Controls.Remove(tblBooks);
            tblBooks.Controls.Clear();
            tblBooks.RowCount = 0;

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
                    txtSelectedBookId.Text = "INFOS DU LIVRE" +
                    "\r\n\r\n- ID : " + book.Id.ToString() +
                    "\r\n\r\n- Titre : " + book.Titre +
                    "\r\n\r\n- Genre : " + book.Genre +
                    "\r\n\r\n- Auteur : " + book.Auteur +
                    "\r\n\r\n- Date de publication : " + book.DatePublication.ToShortDateString();
                };

                tblBooks.RowCount++;
                tblBooks.Controls.Add(lblBook, 0, tblBooks.RowCount);
                tblBooks.Controls.Add(btnShowBookInfo, 1, tblBooks.RowCount);
            }
            tblBooks.RowCount++;
            tblBooks.Controls.Add(new Label(), 0, tblBooks.RowCount);
            tblBooks.Controls.Add(new Label(), 1, tblBooks.RowCount);

            flpMain.Controls.Add(txtSelectedBookId);
            flpMain.Controls.Add(tblBooks);
            flpMain.Controls.Add(pnlAddBook);
        }


        private void AddBookFormControls(Panel pnlAddBook)
        {
            Label lblTitre = new Label() { Text = "Titre:", Left = 10, Top = 20 };
            lblTitre.AutoSize = true;
            TextBox txtTitle = new TextBox() { Left = lblTitre.Width + 50, Top = 20, Width = 200 };

            Label lblAuteur = new Label() { Text = "Auteur:", Left = 10, Top = 50 };
            lblAuteur.AutoSize = true;
            TextBox txtAuthor = new TextBox() { Left = lblAuteur.Width + 50, Top = 50, Width = 200 };

            Label lblGenre = new Label() { Text = "Genre:", Left = 10, Top = 80 };
            lblGenre.AutoSize = true;
            TextBox txtGenre = new TextBox() { Left = lblGenre.Width + 50, Top = 80, Width = 200 };

            Label lblPublicationDate = new Label() { Text = "Date de Publication:", Left = 10, Top = 110 };
            lblPublicationDate.AutoSize = true;
            DateTimePicker dtpPublicationDate = new DateTimePicker() { Left = lblPublicationDate.Width + 50, Top = 110, Width = 200, Format = DateTimePickerFormat.Short };

            // Limiter la date de publication à la date actuelle ou dans le passé
            dtpPublicationDate.MaxDate = DateTime.Now;

            // Validation lorsque la date change
            dtpPublicationDate.ValueChanged += (sender, e) =>
            {
                if (dtpPublicationDate.Value > DateTime.Now)
                {
                    MessageBox.Show("La date de publication ne peut pas être dans le futur.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpPublicationDate.Value = DateTime.Now;  // Réinitialiser la date à la date actuelle
                }
            };

            Button btnSubmit = new Button() { Text = "Soumettre", Left = 120, Top = 150 };
            btnSubmit.Click += (sender, e) =>
            {
                string titre = txtTitle.Text.Trim();
                string auteur = txtAuthor.Text.Trim();
                string genre = txtGenre.Text.Trim();
                DateTime datePublication = dtpPublicationDate.Value;

                // Vérifier que tous les champs sont remplis
                if (string.IsNullOrEmpty(titre) || string.IsNullOrEmpty(auteur) || string.IsNullOrEmpty(genre))
                {
                    // Afficher un message si un champ est vide
                    MessageBox.Show("Veuillez remplir tous les champs du formulaire avant de soumettre.",
                                    "Champs manquants",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    return; // Ne pas envoyer la requête si un champ est vide
                }

                // Si tous les champs sont remplis, on procède à l'insertion dans la base de données
                string query = "INSERT INTO livres (titre, auteur, genre, date_publication, etat) " +
                               "VALUES (@Titre, @Auteur, @Genre, @DatePublication, True)";

                MySqlCommand cmd = new MySqlCommand(query, Program.conn);
                cmd.Parameters.AddWithValue("@Titre", titre);
                cmd.Parameters.AddWithValue("@Auteur", auteur);
                cmd.Parameters.AddWithValue("@Genre", genre);
                cmd.Parameters.AddWithValue("@DatePublication", datePublication);

                try
                {
                    cmd.ExecuteNonQuery(); // Exécuter la requête d'insertion
                    MessageBox.Show("Les informations ont été enregistrées dans la base de données.",
                                    "Succès",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                    // Réinitialiser ou fermer le formulaire après la soumission
                    frmBooksDashboard frmBooksDashboard = new frmBooksDashboard();
                    frmBooksDashboard.Show();
                    frmBooksDashboard.Closed += (s, args) => this.Close();
                }
                catch (Exception ex)
                {
                    // Gérer l'exception si la requête échoue
                    MessageBox.Show("Une erreur s'est produite lors de l'enregistrement des données. " + ex.Message,
                                    "Erreur",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            };

            pnlAddBook.Controls.Add(lblTitre);
            pnlAddBook.Controls.Add(txtTitle);
            pnlAddBook.Controls.Add(lblAuteur);
            pnlAddBook.Controls.Add(txtAuthor);
            pnlAddBook.Controls.Add(lblGenre);
            pnlAddBook.Controls.Add(txtGenre);
            pnlAddBook.Controls.Add(lblPublicationDate);
            pnlAddBook.Controls.Add(dtpPublicationDate);
            pnlAddBook.Controls.Add(btnSubmit);
        }


        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim();

            List<Books> books = Books.GetAll();
            List<Books> filteredBooks = new List<Books>();

            foreach (var book in books)
            {
                if (book.Titre.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    book.Auteur.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    book.Id.ToString().Contains(searchTerm) ||
                    book.DatePublication.ToShortDateString().Contains(searchTerm))
                {
                    filteredBooks.Add(book);
                }
            }
            AddBooksToTable(filteredBooks);
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            frmChoiceMenu frmChoiceMenu = new frmChoiceMenu();
            frmChoiceMenu.Show();
            frmChoiceMenu.Closed += (s, args) => this.Close();
        }
    }
}