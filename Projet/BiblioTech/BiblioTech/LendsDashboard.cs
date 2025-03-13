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
        FlowLayoutPanel flpUserId;
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
            flpMain.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(flpMain);

            flpUserId = new FlowLayoutPanel();
            flpUserId.Width = (int)(flpMain.Width * 0.3) - (2 * paddingMargin);
            flpUserId.Height =flpMain.Height - (4 * paddingMargin);

            txtSelectedLendId = new TextBox();
            txtSelectedLendId.Text = "Veuillez sélectionner un livre afin d'afficher les informations.";
            txtSelectedLendId.ReadOnly = true;
            txtSelectedLendId.Multiline = true;
            txtSelectedLendId.WordWrap = true;
            txtSelectedLendId.ScrollBars = ScrollBars.Vertical;
            txtSelectedLendId.Width = (int)(flpMain.Width * 0.3) - (4 * paddingMargin);
            txtSelectedLendId.Height = (int)(flpMain.Height * 0.5) - (4 * paddingMargin);
            flpUserId.Controls.Add(txtSelectedLendId);


            btnCloseLend = new Button();
            btnCloseLend.Text = "Fermer le prêt";
            btnCloseLend.AutoSize = true;
            btnCloseLend.Location = new Point(txtSelectedLendId.Left, txtSelectedLendId.Bottom + 10);
            btnCloseLend.Enabled = false;
            flpUserId.Controls.Add(btnCloseLend);

            btnReportStolen = new Button();
            btnReportStolen.Text = "Déclarer le livre volé";
            btnReportStolen.AutoSize = true;
            btnReportStolen.Enabled = false;
            flpUserId.Controls.Add(btnReportStolen);

            flpMain.Controls.Add(flpUserId);

            tblLends = new TableLayoutPanel();
            tblLends.Dock = DockStyle.Fill;
            tblLends.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            tblLends.Width = (int)(flpMain.Width * 0.5) - (2 * paddingMargin);
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
                        btnCloseLend.Enabled = true;
                        btnReportStolen.Enabled = true;
                        btnCloseLend.Click += (s, ev) =>
                        {
                            string updateQuery = "UPDATE emprunts SET Etat = 'Retourné' WHERE id = @LendId";
                            MySqlCommand cmd = new MySqlCommand(updateQuery, Program.conn);
                            cmd.Parameters.AddWithValue("@LendId", lend.Id);
                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Le prêt a été marqué comme retourné.");
                            PreloadData();
                            LoadLends();

                            btnCloseLend.Enabled = false;
                            btnReportStolen.Enabled = false;
                        };

                        btnReportStolen.Click += (s, ev) =>
                        {
                            // Delete the lend record (as the book is reported stolen)
                            string deleteQuery = "DELETE FROM emprunts WHERE id = @LendId";
                            MySqlCommand cmd = new MySqlCommand(deleteQuery, Program.conn);
                            cmd.Parameters.AddWithValue("@LendId", lend.Id);
                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Le livre a été déclaré volé et le prêt a été supprimé.");
                            PreloadData();
                            LoadLends();

                            btnCloseLend.Enabled = false;
                            btnReportStolen.Enabled = false;
                        };
                    }
                };

                int rowIndex = tblLends.RowCount;
                tblLends.RowCount++;
                for (int i = 0; i <= 6; i++)
                {
                    tblLends.Controls.Add(lblLendId, i, rowIndex);
                }
            }

            tblLends.RowCount++;
            for (int i = 0; i <= 6; i++)
            {
                tblLends.Controls.Add(new Label(), i, tblLends.RowCount);
            }

            flpMain.Controls.Add(tblLends);
            flpMain.Controls.Add(pnlCreateLend);
        }
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
            lblStartDate.AutoSize = true;
            DateTimePicker dtpStartDate = new DateTimePicker() { Left = 120, Top = 80, Width = 200, Format = DateTimePickerFormat.Short };
            pnlCreateLend.Controls.Add(lblStartDate);
            pnlCreateLend.Controls.Add(dtpStartDate);

            Label lblEndDate = new Label() { Text = "End Date:", Left = 10, Top = 110 };
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

            Button btnSubmit = new Button() { Text = "Submit", Left = 120, Top = 150 };
            btnSubmit.Click += (sender, e) =>
            {
                var selectedUser = cmbUser.SelectedItem as ComboBoxItem;
                var selectedBook = cmbBook.SelectedItem as ComboBoxItem;

                // Vérification des champs avant la soumission
                if (selectedUser == null || selectedBook == null)
                {
                    MessageBox.Show("Veuillez sélectionner un utilisateur et un livre.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int userId = selectedUser?.Id ?? 0;
                int bookId = selectedBook?.Id ?? 0;

                // Vérification si l'utilisateur a déjà un prêt en retard
                bool userHasLateLend = lateLends.Any(lend => lend.IdUtilisateur == userId);

                if (userHasLateLend)
                {
                    MessageBox.Show("L'utilisateur a déjà un prêt en retard et ne peut pas emprunter un autre livre.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DateTime startDate = dtpStartDate.Value;
                DateTime endDate = dtpEndDate.Value;

                string query = "INSERT INTO emprunts (id_utilisateur, id_livre, date_debut, date_fin, etat) " +
                               "VALUES (@UserId, @BookId, @StartDate, @EndDate, 'Non retourné')";

                MySqlCommand cmd = new MySqlCommand(query, Program.conn);
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@BookId", bookId);
                cmd.Parameters.AddWithValue("@StartDate", startDate);
                cmd.Parameters.AddWithValue("@EndDate", endDate);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Les informations ont été enregistrées dans la base de données.");
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
