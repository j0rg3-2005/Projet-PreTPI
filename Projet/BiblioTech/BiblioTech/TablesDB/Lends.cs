using MySqlConnector;
using System;
using System.Collections.Generic;

namespace BiblioTech
{
    public class Emprunts
    {
        public int Id { get; set; }
        public int IdUtilisateur { get; set; }
        public int IdLivre { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime? DateFin { get; set; }
        public string Etat { get; set; }

        // Méthode pour récupérer tous les emprunts de la base de données
        public static List<Emprunts> GetAll()
        {
            List<Emprunts> empruntsList = new List<Emprunts>();

            string query = "SELECT * FROM emprunts";  // Requête pour récupérer tous les emprunts
            MySqlCommand cmd = new MySqlCommand(query, Program.conn);  // Création de la commande SQL avec la connexion
            MySqlDataReader reader = cmd.ExecuteReader();  // Exécution de la requête et récupération des résultats

            while (reader.Read())
            {
                Emprunts emprunt = new Emprunts();

                // Remplir les propriétés avec les données récupérées
                emprunt.Id = reader["ID"] != DBNull.Value ? (int)reader["ID"] : 0;
                emprunt.IdUtilisateur = reader["id_utilisateur"] != DBNull.Value ? (int)reader["id_utilisateur"] : 0;
                emprunt.IdLivre = reader["id_livre"] != DBNull.Value ? (int)reader["id_livre"] : 0;
                emprunt.DateDebut = reader["date_debut"] != DBNull.Value ? (DateTime)reader["date_debut"] : DateTime.MinValue;
                emprunt.DateFin = reader["date_fin"] != DBNull.Value ? (DateTime?)reader["date_fin"] : null;  // Nullable DateTime pour DateFin
                emprunt.Etat = reader["etat"] != DBNull.Value ? (string)reader["etat"] : string.Empty;

                empruntsList.Add(emprunt);  // Ajouter l'emprunt à la liste
            }

            reader.Close();  // Fermer le lecteur de données
            return empruntsList;  // Retourner la liste des emprunts
        }
    }
}
