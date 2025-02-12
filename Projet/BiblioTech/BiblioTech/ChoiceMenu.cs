namespace BiblioTech
{
    public partial class frmChoiceMenu : Form
    {
        public frmChoiceMenu()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void frmChoiceMenu_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            int btnWidth = 120;
            int btnHeight = 40;
            int spacing = 30; // Espacement entre les boutons
            int labelHeight = 30; // Hauteur du label

            // Calcul du centre de l'écran
            int centerX = this.ClientSize.Width / 2;
            int centerY = this.ClientSize.Height / 2;

            // Centrer le Label horizontalement
            lblWelcome.AutoSize = true; // S'assurer que la taille s'adapte au texte
            lblWelcome.Location = new Point(centerX - lblWelcome.Width / 2, centerY - btnHeight - spacing - labelHeight);

            // Positionner les boutons en dessous du label
            btnBooks.Location = new Point(centerX - (btnWidth * 3 + spacing * 2) / 2, centerY - btnHeight / 2);
            btnLends.Location = new Point(centerX - btnWidth / 2, centerY - btnHeight / 2);
            btnUsers.Location = new Point(centerX + (btnWidth + spacing) / 2, centerY - btnHeight / 2);
        }

        private void btnBooks_Click(object sender, EventArgs e)
        {
            frmBooksDashboard frmBooksDashboard = new frmBooksDashboard();
            frmBooksDashboard.Show();
            frmBooksDashboard.Closed += (s, args) => this.Close();
            this.Hide();
        }
        private void btnLends_Click(object sender, EventArgs e)
        {
            frmLendsDashboard frmLendsDashboard = new frmLendsDashboard();
            frmLendsDashboard.Show();
            frmLendsDashboard.Closed += (s, args) => this.Close();
            this.Hide();
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            frmUsersDashboard frmUsersDashboard = new frmUsersDashboard();
            frmUsersDashboard.Show();
            frmUsersDashboard.Closed += (s, args) => this.Close();
            this.Hide();
        }

    }
}
