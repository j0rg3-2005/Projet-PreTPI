using MySqlConnector;
namespace BiblioTech
{
    public partial class frmLendsDashboard : Form
    {
        TextBox txtSelectedLendId;
        TableLayoutPanel tblLends;
        CheckBox chkLateOnly;
        FlowLayoutPanel flpMain;
        Button btnBack;
        int paddingMargin = 15;
        List<Lends> allLends;
        List<Lends> lateLends;

        public frmLendsDashboard()
        {
            InitializeComponent();
        }

        private void frmLendsDashboard_Load(object sender, EventArgs e)
        {
            PreloadData();
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
            chkLateOnly.Top = paddingMargin;
            this.Controls.Add(chkLateOnly);

            flpMain = new FlowLayoutPanel();
            flpMain.Dock = DockStyle.Fill;
            flpMain.FlowDirection = FlowDirection.LeftToRight;
            flpMain.Padding = new Padding(paddingMargin);
            flpMain.Margin = new Padding(paddingMargin);
            flpMain.BorderStyle = BorderStyle.FixedSingle;
            flpMain.AutoScroll = true;
            this.Controls.Add(flpMain);

            txtSelectedLendId = new TextBox();
            txtSelectedLendId.ReadOnly = true;
            txtSelectedLendId.Multiline = true;
            txtSelectedLendId.WordWrap = true;
            txtSelectedLendId.ScrollBars = ScrollBars.Vertical;
            txtSelectedLendId.Width = (int)(flpMain.Width * 0.3) - (2 * paddingMargin);
            txtSelectedLendId.Height = flpMain.Height - (4 * paddingMargin);
            flpMain.Controls.Add(txtSelectedLendId);

            tblLends = new TableLayoutPanel();
            tblLends.Dock = DockStyle.Fill;
            tblLends.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            tblLends.Width = (int)(flpMain.Width * 0.5) - (2 * paddingMargin);
            tblLends.Height = flpMain.Height - (4 * paddingMargin);
            tblLends.RowCount = 0;
            tblLends.AutoScroll = true;

            Panel pnlCreateLend = new Panel();
            pnlCreateLend.Width = (int)(flpMain.Width * 0.2) - (2 * paddingMargin);
            pnlCreateLend.Height = flpMain.Height - (4 * paddingMargin);
            pnlCreateLend.AutoScroll = true;
            pnlCreateLend.BorderStyle = BorderStyle.FixedSingle;

            CreateLendFormControls(pnlCreateLend);
            flpMain.Controls.Add(pnlCreateLend);

            LoadLends();
        }


        private void CreateLendFormControls(Panel pnlCreateLend)
        {
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
                string titre = txtTitre.Text;
                string auteur = txtAuteur.Text;
                string genre = txtGenre.Text;
                string datePublication = txtDatePublication.Text;

                string query = "INSERT INTO livres (titre, auteur, genre, date_publication,etat ) VALUES (@Titre, @Auteur, @Genre, @DatePublication, True)";

                MySqlCommand cmd = new MySqlCommand(query, Program.conn);
                cmd.Parameters.AddWithValue("@Titre", titre);
                cmd.Parameters.AddWithValue("@Auteur", auteur);
                cmd.Parameters.AddWithValue("@Genre", genre);
                cmd.Parameters.AddWithValue("@DatePublication", DateTime.Parse(datePublication));

                cmd.ExecuteNonQuery();

                MessageBox.Show("Les informations ont été enregistrées dans la base de données.");

                frmBooksDashboard frmBooksDashboard = new frmBooksDashboard();
                frmBooksDashboard.Show();
                frmBooksDashboard.Closed += (s, args) => this.Close();
            };

            pnlCreateLend.Controls.Add(lblTitre);
            pnlCreateLend.Controls.Add(txtTitre);
            pnlCreateLend.Controls.Add(lblAuteur);
            pnlCreateLend.Controls.Add(txtAuteur);
            pnlCreateLend.Controls.Add(lblGenre);
            pnlCreateLend.Controls.Add(txtGenre);
            pnlCreateLend.Controls.Add(lblDatePublication);
            pnlCreateLend.Controls.Add(txtDatePublication);
            pnlCreateLend.Controls.Add(btnSubmit);
        }

        private void PreloadData()
        {
            allLends = Lends.GetAll();
            lateLends = allLends.FindAll(l => l.DateFin < DateTime.Now && l.Etat == "Non retourné");
        }

        private void LoadLends()
        {
            tblLends.Controls.Clear();
            tblLends.RowCount = 0;

            List<Lends> lendsToDisplay = chkLateOnly.Checked ? lateLends : allLends;

            foreach (var lend in lendsToDisplay)
            {
                Label lblLendId = new Label();
                lblLendId.Text = lend.Id.ToString();
                lblLendId.AutoSize = true;
                lblLendId.Padding = new Padding(5);

                Label lblUser = new Label();
                lblUser.Text = lend.PrenomUtilisateur + " " + lend.NomUtilisateur;
                lblUser.AutoSize = true;
                lblUser.Padding = new Padding(5);

                Label lblBook = new Label();
                lblBook.Text = lend.TitreLivre;
                lblBook.AutoSize = true;
                lblBook.Padding = new Padding(5);

                Label lblStartDate = new Label();
                lblStartDate.Text = lend.DateDebut.ToString();
                lblStartDate.AutoSize = true;
                lblStartDate.Padding = new Padding(5);

                Label lblEndDate = new Label();
                lblEndDate.Text = lend.DateFin.ToString();
                lblEndDate.AutoSize = true;
                lblEndDate.Padding = new Padding(5);

                Label lblState = new Label();
                lblState.Text = lend.Etat;
                lblState.AutoSize = true;
                lblState.Padding = new Padding(5);

                Button btnShowBookInfo = new Button();
                btnShowBookInfo.Text = "Afficher infos";
                btnShowBookInfo.AutoSize = true;
                btnShowBookInfo.Padding = new Padding(5);
                btnShowBookInfo.Click += (s, ev) =>
                {
                    txtSelectedLendId.Text = "INFOS DU LIVRE" +
                    "\r\n\r\n- ID : " + lend.Id.ToString()
                    + "\r\n\r\n- Livre emprunté : " + lend.TitreLivre
                    + "\r\n\r\n- Utilisateur : " + lend.PrenomUtilisateur
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
            }
            flpMain.Controls.Add(tblLends);
        }


        private void btnBack_Click(object sender, EventArgs e)
        {
            frmChoiceMenu frmChoiceMenu = new frmChoiceMenu();
            frmChoiceMenu.Show();
            frmChoiceMenu.Closed += (s, args) => this.Close();
        }
    }
}
