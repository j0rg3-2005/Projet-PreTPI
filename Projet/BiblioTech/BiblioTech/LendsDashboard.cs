using MySqlConnector;
using System.Drawing.Printing;
namespace BiblioTech
{
    public partial class frmLendsDashboard : Form
    {
        TextBox txtSelectedLendId;
        Button btnCloseLend;
        Button btnReportStolen;
        TableLayoutPanel tblLends;
        CheckBox chkLateOnly;
        FlowLayoutPanel flpMain;
        Panel pnlCreateLend;
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
            flpMain.Padding = new Padding(paddingMargin, paddingMargin * 2, paddingMargin, paddingMargin);
            flpMain.Margin = new Padding(paddingMargin);
            flpMain.AutoScroll = true;
            this.Controls.Add(flpMain);

            txtSelectedLendId = new TextBox();
            txtSelectedLendId.Text = "Veuillez sélectionner un livre afin d'afficher les informations.";
            txtSelectedLendId.ReadOnly = true;
            txtSelectedLendId.Multiline = true;
            txtSelectedLendId.WordWrap = true;
            txtSelectedLendId.ScrollBars = ScrollBars.Vertical;
            txtSelectedLendId.Width = (int)(flpMain.Width * 0.3) - (2 * paddingMargin);
            txtSelectedLendId.Height = (int)(flpMain.Height * 0.5) - (4 * paddingMargin);

            btnCloseLend = new Button();
            btnCloseLend.Text = "Fermer le prêt";
            btnCloseLend.AutoSize = true;
            btnCloseLend.Margin = new Padding(5);
            btnCloseLend.Anchor = AnchorStyles.Left;

            btnReportStolen = new Button();
            btnReportStolen.Text = "Déclarer le livre volé";
            btnReportStolen.AutoSize = true;
            btnReportStolen.Margin = new Padding(5);
            btnReportStolen.Anchor = AnchorStyles.Left;

            // Ajouter les boutons à flpMain
            flpMain.Controls.Add(txtSelectedLendId);
            flpMain.Controls.Add(btnCloseLend);
            flpMain.Controls.Add(btnReportStolen);

            tblLends = new TableLayoutPanel();
            tblLends.Dock = DockStyle.Fill;
            tblLends.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            tblLends.Width = (int)(flpMain.Width * 0.2) - (2 * paddingMargin);
            tblLends.Height = flpMain.Height - (4 * paddingMargin);
            tblLends.RowCount = 0;
            tblLends.AutoScroll = true;
            LoadLends();

            pnlCreateLend = new Panel();
            pnlCreateLend.Width = (int)(flpMain.Width * 0.2) - (2 * paddingMargin);
            pnlCreateLend.Height = flpMain.Height - (4 * paddingMargin);
            pnlCreateLend.AutoScroll = true;
            pnlCreateLend.BorderStyle = BorderStyle.FixedSingle;
            pnlCreateLend.Anchor = AnchorStyles.Right;

            CreateLendFormControls(pnlCreateLend);
            flpMain.Controls.Add(pnlCreateLend);
        }
        private void LoadLends()
        {
            flpMain.Controls.Remove(tblLends);
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
                    txtSelectedLendId.Text = "INFOS DE L'EMPRUNT" +
                    "\r\n\r\n- ID : " + lend.Id.ToString()
                    + "\r\n\r\n- Livre emprunté : " + lend.TitreLivre
                    + "\r\n\r\n- Utilisateur : " + lend.PrenomUtilisateur
                    + "\r\n\r\n- Date de début : " + lend.DateDebut
                    + "\r\n\r\n- Date de fin : " + lend.DateFin
                    + "\r\n\r\n- Etat : " + lend.Etat;
                    if(lend.Etat == "Non retourné")
                    {
                        CreateActionButtons(lend);
                    }
                };

                int rowIndex = tblLends.RowCount;
                tblLends.RowCount++;
                tblLends.Controls.Add(lblLendId, 0, rowIndex);
                tblLends.Controls.Add(lblUser, 1, rowIndex);
                tblLends.Controls.Add(lblBook, 2, rowIndex);
                tblLends.Controls.Add(lblStartDate, 3, rowIndex);
                tblLends.Controls.Add(lblEndDate, 4, rowIndex);
                tblLends.Controls.Add(lblState, 5, rowIndex);
                tblLends.Controls.Add(btnShowBookInfo, 6, rowIndex);
            }

            tblLends.RowCount++;
            tblLends.Controls.Add(new Label(), 0, tblLends.RowCount);
            tblLends.Controls.Add(new Label(), 1, tblLends.RowCount);
            tblLends.Controls.Add(new Label(), 2, tblLends.RowCount);
            tblLends.Controls.Add(new Label(), 3, tblLends.RowCount);
            tblLends.Controls.Add(new Label(), 4, tblLends.RowCount);
            tblLends.Controls.Add(new Label(), 5, tblLends.RowCount);
            tblLends.Controls.Add(new Label(), 6, tblLends.RowCount);

            flpMain.Controls.Add(txtSelectedLendId);
            flpMain.Controls.Add(btnCloseLend);
            flpMain.Controls.Add(btnReportStolen);
            flpMain.Controls.Add(tblLends);
            flpMain.Controls.Add(pnlCreateLend);
        }

        private void CreateActionButtons(Lends lend)
        {

            btnCloseLend.Click += (s, ev) =>
            {
                // Mark the lend as returned
                string updateQuery = "UPDATE emprunts SET etat = 'Retourné' WHERE id = @LendId";
                MySqlCommand cmd = new MySqlCommand(updateQuery, Program.conn);
                cmd.Parameters.AddWithValue("@LendId", lend.Id);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Le prêt a été marqué comme retourné.");
                LoadLends(); // Refresh the list
            };

            // Create and add "Déclarer volé" button

            btnReportStolen.Click += (s, ev) =>
            {
                // Delete the lend record (as the book is reported stolen)
                string deleteQuery = "DELETE FROM emprunts WHERE id = @LendId";
                MySqlCommand cmd = new MySqlCommand(deleteQuery, Program.conn);
                cmd.Parameters.AddWithValue("@LendId", lend.Id);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Le livre a été déclaré volé et le prêt a été supprimé.");
                LoadLends(); // Refresh the list
            };
        }


        // Créez une classe pour associer l'ID à l'affichage du texte
        public class ComboBoxItem
        {
            public int Id { get; set; }
            public string DisplayText { get; set; }

            public ComboBoxItem(int id, string displayText)
            {
                Id = id;
                DisplayText = displayText;
            }

            public override string ToString() => DisplayText;  // Affiche le texte dans la ComboBox
        }

        private void CreateLendFormControls(Panel pnlCreateLend)
        {
            List<Books> books = Books.GetAll(); // Liste des livres
            List<Users> users = Users.GetAll(); // Liste des utilisateurs

            // Create ComboBox for users
            Label lblUser = new Label() { Text = "User:", Left = 10, Top = 20 };
            lblUser.BorderStyle = BorderStyle.FixedSingle;
            lblUser.AutoSize = true;

            ComboBox cmbUser = new ComboBox() { Left = 120, Top = 20, Width = 200 };
            cmbUser.DropDownStyle = ComboBoxStyle.DropDownList;

            // Remplir ComboBox des utilisateurs avec l'objet ComboBoxItem
            foreach (var user in users)
            {
                cmbUser.Items.Add(new ComboBoxItem(user.Id, user.Prenom + " " + user.Nom)); // Ajout de l'ID et du Nom
            }

            pnlCreateLend.Controls.Add(lblUser);
            pnlCreateLend.Controls.Add(cmbUser);

            // Create ComboBox for books
            Label lblBook = new Label() { Text = "Book Title:", Left = 10, Top = 50 };
            lblBook.BorderStyle = BorderStyle.FixedSingle;
            lblBook.AutoSize = true;

            ComboBox cmbBook = new ComboBox() { Left = 120, Top = 50, Width = 200 };
            cmbBook.DropDownStyle = ComboBoxStyle.DropDownList;

            // Remplir ComboBox des livres avec l'objet ComboBoxItem
            foreach (var book in books)
            {
                cmbBook.Items.Add(new ComboBoxItem(book.Id, book.Titre)); // Ajout de l'ID et du Titre
            }

            pnlCreateLend.Controls.Add(lblBook);
            pnlCreateLend.Controls.Add(cmbBook);

            // Date fields (using DateTimePicker)
            Label lblStartDate = new Label() { Text = "Start Date:", Left = 10, Top = 80 };
            lblStartDate.BorderStyle = BorderStyle.FixedSingle;
            lblStartDate.AutoSize = true;
            DateTimePicker dtpStartDate = new DateTimePicker() { Left = 120, Top = 80, Width = 200, Format = DateTimePickerFormat.Short };
            pnlCreateLend.Controls.Add(lblStartDate);
            pnlCreateLend.Controls.Add(dtpStartDate);

            Label lblEndDate = new Label() { Text = "End Date:", Left = 10, Top = 110 };
            lblEndDate.BorderStyle = BorderStyle.FixedSingle;
            lblEndDate.AutoSize = true;
            DateTimePicker dtpEndDate = new DateTimePicker() { Left = 120, Top = 110, Width = 200, Format = DateTimePickerFormat.Short };
            pnlCreateLend.Controls.Add(lblEndDate);
            pnlCreateLend.Controls.Add(dtpEndDate);

            // Empêcher la sélection d'une date de fin antérieure à la date de début
            dtpStartDate.ValueChanged += (s, e) =>
            {
                if (dtpEndDate.Value < dtpStartDate.Value)
                {
                    dtpEndDate.Value = dtpStartDate.Value;
                }
            };

            dtpEndDate.ValueChanged += (s, e) =>
            {
                if (dtpEndDate.Value < dtpStartDate.Value)
                {
                    MessageBox.Show("La date de fin ne peut pas être avant la date de début.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpEndDate.Value = dtpStartDate.Value;
                }
            };


            // Submit button
            Button btnSubmit = new Button() { Text = "Submit", Left = 120, Top = 150 };
            btnSubmit.Click += (sender, e) =>
            {
                var selectedUser = cmbUser.SelectedItem as ComboBoxItem;
                var selectedBook = cmbBook.SelectedItem as ComboBoxItem;

                int userId = selectedUser?.Id ?? 0;
                int bookId = selectedBook?.Id ?? 0;

                DateTime startDate = dtpStartDate.Value;
                DateTime endDate = dtpEndDate.Value;

                string query = "INSERT INTO emprunts (id_utilisateur, id_livre, date_debut, date_fin, etat) " +
                               "VALUES (@UserId, @BookId, @StartDate, @EndDate, 'Not Returned')";

                MySqlCommand cmd = new MySqlCommand(query, Program.conn);
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@BookId", bookId);
                cmd.Parameters.AddWithValue("@StartDate", startDate);
                cmd.Parameters.AddWithValue("@EndDate", endDate);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Information has been saved to the database.");
            };
            pnlCreateLend.Controls.Add(btnSubmit);
        }
        private void PreloadData()
        {
            allLends = Lends.GetAll();
            lateLends = allLends.FindAll(l => l.DateFin < DateTime.Now && l.Etat == "Non retourné");
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            frmChoiceMenu frmChoiceMenu = new frmChoiceMenu();
            frmChoiceMenu.Show();
            frmChoiceMenu.Closed += (s, args) => this.Close();
        }
    }
}
