using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BiblioTech
{
    public partial class ChoixMenu : Form
    {
        public int IdUsers;

        public ChoixMenu()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void ChoixMenu_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            List<Utilisateur> utilisateurs = Utilisateur.GetAll();

            for (int i = 0; i < utilisateurs.Count; i++)
            {
                lstUsers.Items.Add(utilisateurs[i].Prenom + " " + utilisateurs[i].Nom);
            }
        }

        private void lstUsers_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}