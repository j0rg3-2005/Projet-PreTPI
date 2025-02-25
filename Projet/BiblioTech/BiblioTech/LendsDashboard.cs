using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BiblioTech
{
    public partial class frmLendsDashboard : Form
    {
        TextBox txtSelectedLendId;
        TableLayoutPanel tblLends;
        int spacing = 20; // Espacement entre la TextBox et la liste

        public frmLendsDashboard()
        {
            InitializeComponent();
        }

        private void frmLendsDashboard_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            // Création de la TextBox
            txtSelectedLendId = new TextBox();
            txtSelectedLendId.ReadOnly = true;
            txtSelectedLendId.Multiline = true;
            txtSelectedLendId.WordWrap = true;
            txtSelectedLendId.ScrollBars = ScrollBars.Vertical;
            txtSelectedLendId.Width = (int)(this.ClientSize.Width * 0.3);
            txtSelectedLendId.Height = this.ClientSize.Height - 50; // Ajustement dynamique
            txtSelectedLendId.Location = new Point(10, 10);

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
                lblBook.Text = books[lend.IdLivre].Titre + Environment.NewLine + users[lend.IdUtilisateur].Prenom + users[lend.IdUtilisateur].Nom;
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
                    txtSelectedLendId.Text = "INFOS DU LIVRE" + Environment.NewLine +
                    Environment.NewLine + "- ID : " + lend.Id.ToString() + Environment.NewLine +
                    Environment.NewLine + "- Livre emprunté : " + books[lend.IdLivre].Titre + Environment.NewLine +
                    Environment.NewLine + "- Utilisateur : " + users[lend.IdUtilisateur].Prenom + users[lend.IdUtilisateur].Nom + Environment.NewLine +
                    Environment.NewLine + "- Date de début : " + lend.DateDebut + Environment.NewLine +
                    Environment.NewLine + "- Date de fin : " + lend.DateFin;

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

            // Ajout du TableLayoutPanel au FlowLayoutPanel
            flnpnlLends.Controls.Clear();
            flnpnlLends.Controls.Add(tblLends);

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
            txtSelectedLendId.Width = (int)(this.ClientSize.Width * 0.3);
            txtSelectedLendId.Height = this.ClientSize.Height - marginBottom;

            // Positionner la liste après la TextBox avec un espacement
            flnpnlLends.Left = txtSelectedLendId.Right + spacing;
            flnpnlLends.Width = tblLends.PreferredSize.Width + 20;
            flnpnlLends.Height = this.ClientSize.Height - flnpnlLends.Top - marginBottom;

            // Empêcher la liste de dépasser la fenêtre
            if (flnpnlLends.Right > this.ClientSize.Width)
            {
                flnpnlLends.Width = this.ClientSize.Width - flnpnlLends.Left - 10;
            }
        }
    }
}
