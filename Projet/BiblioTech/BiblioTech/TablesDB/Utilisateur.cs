using BiblioTech;
using MySqlConnector;
using System;
using System.Collections.Generic;

namespace BiblioTech
{
    public class Utilisateur
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Telephone { get; set; }
        public string AdressePostale { get; set; }
        public string AdresseEmail { get; set; }

        public static List<Utilisateur> GetAll()
        {
            List<Utilisateur> utilisateurs = new List<Utilisateur>();

            string query = "SELECT * FROM utilisateurs";  // La requête récupère toutes les colonnes
            MySqlCommand cmd = new MySqlCommand(query, Program.conn);  // Utilisation de la connexion
            MySqlDataReader reader = cmd.ExecuteReader();  // Exécution de la requête

            while (reader.Read())
            {
                Utilisateur _utilisateurs = new Utilisateur();

                // Remplir les propriétés avec les données récupérées du reader
                _utilisateurs.Id = reader["ID"] != DBNull.Value ? (int)reader["ID"] : 0;  // Sécuriser la lecture de la colonne "ID"
                _utilisateurs.Nom = reader["nom"] != DBNull.Value ? (string)reader["nom"] : string.Empty;  // Sécuriser la lecture de la colonne "nom"
                _utilisateurs.Prenom = reader["prenom"] != DBNull.Value ? (string)reader["prenom"] : string.Empty;  // Sécuriser la lecture de la colonne "prenom"
                _utilisateurs.Telephone = reader["telephone"] != DBNull.Value ? (string)reader["telephone"] : string.Empty;  // Sécuriser la lecture de la colonne "telephone"
                _utilisateurs.AdressePostale = reader["adresse_postale"] != DBNull.Value ? (string)reader["adresse_postale"] : null;  // Sécuriser la lecture de la colonne "adresse_postale"
                _utilisateurs.AdresseEmail = reader["adresse_email"] != DBNull.Value ? (string)reader["adresse_email"] : null;  // Sécuriser la lecture de la colonne "adresse_email"

                utilisateurs.Add(_utilisateurs);  // Ajouter l'utilisateur à la liste
            }

            reader.Close();  // Fermer le lecteur de données
            return utilisateurs;  // Retourner la liste des utilisateurs
        }
    }
}
