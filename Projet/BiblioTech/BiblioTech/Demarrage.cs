using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BiblioTech
{
    public partial class Demarrage : Form
    {
        public Demarrage()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            // Centrer horizontalement le texte et le bouton
            lblBienvenue.Location = new Point((this.ClientSize.Width - lblBienvenue.Width) / 2, lblBienvenue.Location.Y);
            btnSuivant.Location = new Point((this.ClientSize.Width - btnSuivant.Width) / 2, btnSuivant.Location.Y);
        }

        private void lblBienvenue_Click(object sender, EventArgs e)
        {
            
        }

        private void btnSuivant_Click(object sender, EventArgs e)
        {
            ChoixMenu frmChoixMenu = new ChoixMenu();
            frmChoixMenu.Show();
            frmChoixMenu.Closed += (s, args) => this.Close();
            this.Hide();

        }


    }
}
