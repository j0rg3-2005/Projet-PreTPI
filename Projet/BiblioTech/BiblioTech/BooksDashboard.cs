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
        int spacing = 20; // Espacement entre la TextBox et la liste

        public frmBooksDashboard()
        {
            InitializeComponent();
        }

        private void frmBooksDashboard_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            // Création de la TextBox
            txtSelectedBookId = new TextBox();
            txtSelectedBookId.ReadOnly = true;
            txtSelectedBookId.Multiline = true;
            txtSelectedBookId.WordWrap = true;
            txtSelectedBookId.ScrollBars = ScrollBars.Vertical;
            txtSelectedBookId.Width = (int)(this.ClientSize.Width * 0.3);
            txtSelectedBookId.Height = this.ClientSize.Height - 50; // Ajustement dynamique
            txtSelectedBookId.Location = new Point(10, 10);

            this.Controls.Add(txtSelectedBookId);

            // Création du TableLayoutPanel
            tblBooks = new TableLayoutPanel();
            tblBooks.AutoSize = true;
            tblBooks.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tblBooks.ColumnCount = 2;
            tblBooks.RowCount = 0;
            tblBooks.Padding = new Padding(10);
            tblBooks.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

            // Ajout des livres
            List<Books> livres = Books.GetAll();
            int maxWidthLabel = 0;
            int maxWidthButton = 0;

            foreach (var livre in livres)
            {
                Label lblBook = new Label();
                lblBook.Text = livre.Titre;
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
                    txtSelectedBookId.Text = "INFOS DU LIVRE" + Environment.NewLine +
                    Environment.NewLine + "- ID : " + livre.Id.ToString() + Environment.NewLine +
                    Environment.NewLine + "- Titre : " + livre.Titre + Environment.NewLine +
                    Environment.NewLine + "- Auteur : " + livre.Auteur + Environment.NewLine +
                    Environment.NewLine + "- Genre : " + livre.Genre + Environment.NewLine +
                    Environment.NewLine + "- Année de publication : " + livre.DatePublication;
                };

                int rowIndex = tblBooks.RowCount;
                tblBooks.RowCount++;
                tblBooks.Controls.Add(lblBook, 0, rowIndex);
                tblBooks.Controls.Add(btnShowBookInfo, 1, rowIndex);

                maxWidthLabel = Math.Max(maxWidthLabel, lblBook.Width);
                maxWidthButton = Math.Max(maxWidthButton, btnShowBookInfo.PreferredSize.Width);
            }

            // Définition des largeurs des colonnes
            tblBooks.ColumnStyles.Clear();
            tblBooks.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, maxWidthLabel + 20));
            tblBooks.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, maxWidthButton + 20));

            // Ajout du TableLayoutPanel au FlowLayoutPanel
            flnpnlBooks.Controls.Clear();
            flnpnlBooks.Controls.Add(tblBooks);

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
            txtSelectedBookId.Width = (int)(this.ClientSize.Width * 0.3);
            txtSelectedBookId.Height = this.ClientSize.Height - marginBottom;

            // Positionner la liste après la TextBox avec un espacement
            flnpnlBooks.Left = txtSelectedBookId.Right + spacing;
            flnpnlBooks.Width = tblBooks.PreferredSize.Width + 20;
            flnpnlBooks.Height = this.ClientSize.Height - flnpnlBooks.Top - marginBottom;

            // Empêcher la liste de dépasser la fenêtre
            if (flnpnlBooks.Right > this.ClientSize.Width)
            {
                flnpnlBooks.Width = this.ClientSize.Width - flnpnlBooks.Left - 10;
            }
        }
    }
}
