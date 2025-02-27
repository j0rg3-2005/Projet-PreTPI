using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BiblioTech
{
    public partial class frmBooksDashboard : Form
    {
        TextBox txtSelectedBookInfo;
        TableLayoutPanel tblBooks;

        Button btnBack; // Déclaration du bouton de retour
        int spacing = 20; // Espacement entre la TextBox et la liste
        int paddingMargin = 20; // Marge globale

        public frmBooksDashboard()
        {
            InitializeComponent();
        }

        private void frmBooksDashboard_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.Padding = new Padding(paddingMargin); // Ajouter une marge globale autour du formulaire

            // Création du bouton de retour en haut à gauche
            btnBack = new Button();
            btnBack.Text = "Retour";
            btnBack.AutoSize = true;
            btnBack.Padding = new Padding(5);
            btnBack.Click += btnBack_Click;
            btnBack.Location = new Point(paddingMargin, paddingMargin); // Positionné en haut à gauche
            this.Controls.Add(btnBack);

            // Création de la TextBox pour afficher les infos du livre
            txtSelectedBookInfo = new TextBox();
            txtSelectedBookInfo.ReadOnly = true;
            txtSelectedBookInfo.Multiline = true;
            txtSelectedBookInfo.WordWrap = true;
            txtSelectedBookInfo.ScrollBars = ScrollBars.Vertical;
            txtSelectedBookInfo.Width = (int)(this.ClientSize.Width * 0.3) - (2 * paddingMargin);
            txtSelectedBookInfo.Height = this.ClientSize.Height - 50 - (2 * paddingMargin); // Ajustement dynamique
            txtSelectedBookInfo.Location = new Point(paddingMargin, paddingMargin + btnBack.Height); // Juste en dessous du bouton Retour

            this.Controls.Add(txtSelectedBookInfo);

            // Création du TableLayoutPanel pour les livres
            tblBooks = new TableLayoutPanel();
            tblBooks.AutoSize = true;
            tblBooks.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tblBooks.ColumnCount = 2;
            tblBooks.RowCount = 0;
            tblBooks.Padding = new Padding(10);
            tblBooks.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

            // Ajout des livres
            List<Books> books = Books.GetAll();
            int maxWidthLabel = 0;
            int maxWidthButton = 0;

            foreach (var book in books)
            {
                Label lblBook = new Label();
                lblBook.Text = book.Titre + Environment.NewLine + book.Auteur;
                lblBook.AutoSize = true;
                lblBook.Padding = new Padding(5);
                lblBook.TextAlign = ContentAlignment.MiddleLeft;

                Button btnShowBookInfo = new Button();
                btnShowBookInfo.Text = "Afficher infos";
                btnShowBookInfo.AutoSize = true;
                btnShowBookInfo.Padding = new Padding(5);
                btnShowBookInfo.Click += (s, ev) =>
                {
                    // Mettre à jour le texte de la TextBox avec des informations sur le livre
                    txtSelectedBookInfo.Text = "INFOS DU LIVRE" +
                    "\r\n\r\n- ID : " + book.Id.ToString() +
                    "\r\n\r\n- Titre : " + book.Titre +
                    "\r\n\r\n- Genre : " + book.Genre +
                    "\r\n\r\n- Auteur : " + book.Auteur +
                    "\r\n\r\n- Date de publication : " + book.DatePublication.ToShortDateString();
                };

                int rowIndex = tblBooks.RowCount;
                tblBooks.RowCount++;
                tblBooks.Controls.Add(lblBook, 0, rowIndex);
                tblBooks.Controls.Add(btnShowBookInfo, 1, rowIndex);

                maxWidthLabel = Math.Max(maxWidthLabel, lblBook.Width);
                maxWidthButton = Math.Max(maxWidthButton, btnShowBookInfo.PreferredSize.Width);
            }

            // Définir les largeurs des colonnes
            tblBooks.ColumnStyles.Clear();
            tblBooks.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, maxWidthLabel + 20));
            tblBooks.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, maxWidthButton + 20));

            // Création du FlowLayoutPanel pour contenir les livres
            FlowLayoutPanel flnpnlBooks = new FlowLayoutPanel();
            flnpnlBooks.AutoScroll = true;
            flnpnlBooks.Location = new Point(txtSelectedBookInfo.Right + spacing, txtSelectedBookInfo.Top); // Placer à droite de la TextBox
            flnpnlBooks.Width = this.ClientSize.Width - txtSelectedBookInfo.Width - (3 * paddingMargin);
            flnpnlBooks.Controls.Add(tblBooks);

            this.Controls.Add(flnpnlBooks);

            // Ajustement des tailles et positions
            AdjustLayout();

            this.Resize += new EventHandler(frmBooksDashboard_Resize);
        }

        private void frmBooksDashboard_Resize(object sender, EventArgs e)
        {
            AdjustLayout();
        }

        private void AdjustLayout()
        {
            int marginBottom = 40; // Marge du bas

            // Ajuster la largeur de la TextBox (prend 30% de la largeur de la fenêtre)
            txtSelectedBookInfo.Width = (int)(this.ClientSize.Width * 0.3) - (2 * paddingMargin);
            txtSelectedBookInfo.Height = this.ClientSize.Height - marginBottom - (2 * paddingMargin);

            // Ajuster la position du FlowLayoutPanel
            FlowLayoutPanel flnpnlBooks = (FlowLayoutPanel)this.Controls["flnpnlBooks"];
            flnpnlBooks.Left = txtSelectedBookInfo.Right + spacing;
            flnpnlBooks.Top = txtSelectedBookInfo.Top;
            flnpnlBooks.Width = this.ClientSize.Width - flnpnlBooks.Left - paddingMargin;
            flnpnlBooks.Height = this.ClientSize.Height - flnpnlBooks.Top - marginBottom;

            // Empêcher la liste de dépasser la fenêtre
            if (flnpnlBooks.Right > this.ClientSize.Width)
            {
                flnpnlBooks.Width = this.ClientSize.Width - flnpnlBooks.Left - paddingMargin;
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            frmChoiceMenu frmChoiceMenu = new frmChoiceMenu();
            frmChoiceMenu.Show();
            this.Hide();
        }
    }
}
