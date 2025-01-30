using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace BiblioTech
{
    public partial class ChoixMenu : Form
    {
        public ChoixMenu()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        public class ListItem
        {
            public int ID { get; set; }
            public string Text { get; set; }

            public ListItem(int id, string text)
            {
                ID = id;
                Text = text;
            }

            public override string ToString()
            {
                return Text;
            }
        }

        private void ChoixMenu_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            // Récupérer tous les utilisateurs et livres
            List<Users> utilisateurs = Users.GetAll();
            List<Books> livres = Books.GetAll();

            // Ajouter les utilisateurs et ajuster la largeur de la ListBox
            foreach (var utilisateur in utilisateurs)
            {
                ListItem item = new ListItem(utilisateur.Id, utilisateur.Prenom + " " + utilisateur.Nom);
                lstUsers.Items.Add(item);
            }

            // Ajouter les livres et ajuster la largeur de la ListBox
            foreach (var livre in livres)
            {
                ListItem item = new ListItem(livre.Id, livre.Titre);
                lstBooks.Items.Add(item);
            }

            // Ajuster la largeur des ListBox en fonction du texte le plus long
            AdjustListBoxWidth(lstUsers);
            AdjustListBoxWidth(lstBooks);

            // Récupérer les emprunts
            List<Emprunts> emprunts = Emprunts.GetAll();

            // Configurer les colonnes du DataGridView (lstLends)
            lstLends.Columns.Clear();
            lstLends.Columns.Add("Id", "ID");
            lstLends.Columns.Add("IdUtilisateur", "ID Utilisateur");
            lstLends.Columns.Add("IdLivre", "ID Livre");
            lstLends.Columns.Add("DateDebut", "Date Début");
            lstLends.Columns.Add("DateFin", "Date Fin");
            lstLends.Columns.Add("Etat", "État");

            // Ajouter les emprunts au DataGridView
            foreach (var emprunt in emprunts)
            {
                // Ajouter une ligne pour chaque emprunt
                lstLends.Rows.Add(
                    emprunt.Id,
                    emprunt.IdUtilisateur,
                    emprunt.IdLivre,
                    emprunt.DateDebut.ToString("yyyy-MM-dd"),
                    emprunt.DateFin.HasValue ? emprunt.DateFin.Value.ToString("yyyy-MM-dd") : "Non retourné",
                    emprunt.Etat
                );
            }
        }

        // Fonction pour ajuster la largeur de la ListBox
        private void AdjustListBoxWidth(ListBox listBox)
        {
            int maxWidth = 0;

            // Parcourir tous les éléments pour calculer la largeur maximale
            foreach (var item in listBox.Items)
            {
                string text = item.ToString();
                int width = TextRenderer.MeasureText(text, listBox.Font).Width;

                // Garder la largeur maximale
                if (width > maxWidth)
                {
                    maxWidth = width;
                }
            }

            // Ajouter un peu de marge (par exemple 20 pixels)
            listBox.Width = maxWidth + 20;
        }

        private void lstUsers_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (lstUsers.SelectedItem != null)
            {
                List<Users> utilisateurs = Users.GetAll();
                ListItem selectedItem = (ListItem)lstUsers.SelectedItem;
                int userId = selectedItem.ID;
                Users selectedUser = utilisateurs.FirstOrDefault(u => u.Id == userId);

                MessageBox.Show("ID : " + selectedUser.Id.ToString() + "\n" +
                                "Nom : " + selectedUser.Nom + "\n" +
                                "Prénom : " + selectedUser.Prenom + "\n" +
                                "Téléphone : " + selectedUser.Telephone + "\n" +
                                "Courriel : " + selectedUser.AdresseEmail + "\n" +
                                "Adresse : " + selectedUser.AdressePostale,
                                selectedItem.Text);

            }
        }

        private void lstBooks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstBooks.SelectedItem != null)
            {
                List<Books> livres = Books.GetAll();
                ListItem selectedItem = (ListItem)lstBooks.SelectedItem;
                int bookId = selectedItem.ID;
                Books selectedBook = livres.FirstOrDefault(b => b.Id == bookId);

                MessageBox.Show("ID : " + selectedBook.Id.ToString() + "\n" +
                                "Titre : " + selectedBook.Titre + "\n" +
                                "Genre : " + selectedBook.Genre + "\n" +
                                "Auteur : " + selectedBook.Auteur + "\n" +
                                "Date de publication : " + selectedBook.DatePublication.ToString("yyyy-MM-dd"),
                                selectedItem.Text);
            }
        }

        private void lstLends_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Vous pouvez ajouter une fonctionnalité spécifique au clic sur les cellules de lstLends
        }

        private void lblEmprunts_Click(object sender, EventArgs e)
        {

        }
    }
}
