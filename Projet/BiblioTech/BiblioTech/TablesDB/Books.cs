using MySqlConnector;
using System;
using System.Collections.Generic;

namespace BiblioTech
{
    public class Books
    {
        public int Id { get; set; }
        public string Titre { get; set; }
        public string Genre { get; set; }
        public string Auteur { get; set; }
        public DateTime DatePublication { get; set; }

        public static List<Books> GetAll()
        {
            List<Books> livres = new List<Books>();

            string query = "SELECT * FROM livres";  // La requête récupère toutes les colonnes
            MySqlCommand cmd = new MySqlCommand(query, Program.conn);  // Utilisation de la connexion
            MySqlDataReader reader = cmd.ExecuteReader();  // Exécution de la requête

            while (reader.Read())
            {
                Books _livre = new Books();

                // Remplir les propriétés avec les données récupérées du reader
                _livre.Id = reader["ID"] != DBNull.Value ? (int)reader["ID"] : 0;  // Sécuriser la lecture de la colonne "ID"
                _livre.Titre = reader["titre"] != DBNull.Value ? (string)reader["titre"] : string.Empty;  // Sécuriser la lecture de la colonne "titre"
                _livre.Genre = reader["genre"] != DBNull.Value ? (string)reader["genre"] : string.Empty;  // Sécuriser la lecture de la colonne "genre"
                _livre.Auteur = reader["auteur"] != DBNull.Value ? (string)reader["auteur"] : string.Empty;  // Sécuriser la lecture de la colonne "auteur"
                _livre.DatePublication = reader["date_publication"] != DBNull.Value ? (DateTime)reader["date_publication"] : DateTime.MinValue;  // Sécuriser la lecture de la colonne "date_publication"

                livres.Add(_livre);  // Ajouter le livre à la liste
            }

            reader.Close();  // Fermer le lecteur de données
            return livres;  // Retourner la liste des livres
        }
    }
}
