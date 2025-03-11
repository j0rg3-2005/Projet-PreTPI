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
        CheckBox chkLateOnly;
        Button btnBack;
        int spacing = 20;
        int paddingMargin = 20;
        List<Lends> allLends;
        List<Lends> lateLends;
        List<Books> books;
        List<Users> users;

        public frmLendsDashboard()
        {
            InitializeComponent();
        }

        private void frmLendsDashboard_Load(object sender, EventArgs e)
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

            chkLateOnly = new CheckBox();
            chkLateOnly.Text = "Afficher seulement les prêts en retard";
            chkLateOnly.AutoSize = true;
            chkLateOnly.Location = new Point(btnBack.Right + 20, paddingMargin);
            chkLateOnly.CheckedChanged += (s, ev) => LoadLends();
            this.Controls.Add(chkLateOnly);

            txtSelectedLendId = new TextBox();
            txtSelectedLendId.ReadOnly = true;
            txtSelectedLendId.Multiline = true;
            txtSelectedLendId.WordWrap = true;
            txtSelectedLendId.ScrollBars = ScrollBars.Vertical;
            txtSelectedLendId.Width = (int)(this.ClientSize.Width * 0.3) - (2 * paddingMargin);
            txtSelectedLendId.Height = this.ClientSize.Height - 50 - (2 * paddingMargin);
            txtSelectedLendId.Location = new Point(paddingMargin, btnBack.Bottom + 10);
            this.Controls.Add(txtSelectedLendId);

            tblLends = new TableLayoutPanel();
            tblLends.AutoSize = true;
            tblLends.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tblLends.ColumnCount = 6;
            tblLends.RowCount = 0;
            tblLends.Padding = new Padding(10);
            tblLends.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

            flnpnlLends.AutoScroll = true;
            flnpnlLends.Location = new Point(txtSelectedLendId.Right + spacing, txtSelectedLendId.Top);
            flnpnlLends.Width = this.ClientSize.Width - txtSelectedLendId.Width - (3 * paddingMargin);
            flnpnlLends.Controls.Add(tblLends);

            PreloadData();
            LoadLends();
            AdjustLayout();

            this.Resize += new EventHandler(frmLendsDashboard_Resize);
        }

        private void PreloadData()
        {
            allLends = Lends.GetAll();
            books = Books.GetAll();
            users = Users.GetAll();
            lateLends = allLends.FindAll(l => l.DateFin < DateTime.Now && l.Etat == "Non retourné");
        }

        private void LoadLends()
        {
            tblLends.Controls.Clear();
            tblLends.RowCount = 0;

            List<Lends> lendsToDisplay = chkLateOnly.Checked ? lateLends : allLends;
            int maxWidthLabel = 0;
            int maxWidthButton = 0;

            foreach (var lend in lendsToDisplay)
            {
                Label lblLendId = new Label();
                lblLendId.Text = lend.Id.ToString();
                lblLendId.AutoSize = true;
                lblLendId.Padding = new Padding(5);
                lblLendId.TextAlign = ContentAlignment.MiddleLeft;
                lblLendId.BorderStyle = BorderStyle.FixedSingle;

                Label lblUser = new Label();
                lblUser.Text = users[lend.IdUtilisateur].Nom + " " + users[lend.IdUtilisateur].Prenom;
                lblUser.AutoSize = true;
                lblUser.Padding = new Padding(5);
                lblUser.TextAlign = ContentAlignment.MiddleLeft;

                Label lblBook = new Label();
                lblBook.Text = books[lend.IdLivre].Titre;
                lblBook.AutoSize = true;
                lblBook.Padding = new Padding(5);
                lblBook.TextAlign = ContentAlignment.MiddleLeft;

                Label lblStartDate = new Label();
                lblStartDate.Text = lend.DateDebut.ToString();
                lblStartDate.AutoSize = true;
                lblStartDate.Padding = new Padding(5);
                lblStartDate.TextAlign = ContentAlignment.MiddleLeft;

                Label lblEndDate = new Label();
                lblEndDate.Text = lend.DateFin.ToString();
                lblEndDate.AutoSize = true;
                lblEndDate.Padding = new Padding(5);
                lblEndDate.TextAlign = ContentAlignment.MiddleLeft;

                Label lblState = new Label();
                lblEndDate.Text = lend.Etat;
                lblEndDate.AutoSize = true;
                lblEndDate.Padding = new Padding(5);
                lblEndDate.TextAlign = ContentAlignment.MiddleLeft;

                Button btnShowBookInfo = new Button();
                btnShowBookInfo.Text = "Afficher infos";
                btnShowBookInfo.AutoSize = true;
                btnShowBookInfo.Padding = new Padding(5);
                btnShowBookInfo.Click += (s, ev) =>
                {
                    txtSelectedLendId.Text = "INFOS DU LIVRE" +
                    "\r\n\r\n- ID : " + lend.Id.ToString()
                    + "\r\n\r\n- Livre emprunté : " + books[lend.IdLivre].Titre
                    + "\r\n\r\n- Utilisateur : " + users[lend.IdUtilisateur].Prenom + " " + users[lend.IdUtilisateur].Nom
                    + "\r\n\r\n- Date de début : " + lend.DateDebut
                    + "\r\n\r\n- Date de fin : " + lend.DateFin
                    + "\r\n\r\n- Etat : " + lend.Etat;

                };

                int rowIndex = tblLends.RowCount;
                tblLends.RowCount++;
                tblLends.Controls.Add(lblLendId, 0, rowIndex);
                tblLends.Controls.Add(lblUser, 1, rowIndex);
                tblLends.Controls.Add(lblBook, 2, rowIndex);
                tblLends.Controls.Add(lblStartDate, 3, rowIndex);
                tblLends.Controls.Add(lblEndDate, 4, rowIndex);
                tblLends.Controls.Add(btnShowBookInfo, 5, rowIndex);

                maxWidthLabel = Math.Max(maxWidthLabel, lblBook.Width);
                maxWidthButton = Math.Max(maxWidthButton, btnShowBookInfo.PreferredSize.Width);
            }

            tblLends.ColumnStyles.Clear();
            tblLends.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, maxWidthLabel + 20));
            tblLends.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, maxWidthButton + 20));
        }

        private void frmLendsDashboard_Resize(object sender, EventArgs e)
        {
            AdjustLayout();
        }

        private void AdjustLayout()
        {
            int marginBottom = 40;
            txtSelectedLendId.Width = (int)(this.ClientSize.Width * 0.3) - (2 * paddingMargin);
            txtSelectedLendId.Height = this.ClientSize.Height - marginBottom - (2 * paddingMargin);
            flnpnlLends.Left = txtSelectedLendId.Right + spacing;
            flnpnlLends.Top = txtSelectedLendId.Top;
            flnpnlLends.Width = this.ClientSize.Width - flnpnlLends.Left - paddingMargin;
            flnpnlLends.Height = this.ClientSize.Height - flnpnlLends.Top - marginBottom;
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