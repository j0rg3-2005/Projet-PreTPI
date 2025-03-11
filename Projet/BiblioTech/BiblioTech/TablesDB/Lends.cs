using MySqlConnector;
using System;
using System.Collections.Generic;

namespace BiblioTech
{
    public class Lends
    {
        public int Id { get; set; }
        public int IdUtilisateur { get; set; }
        public int IdLivre { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime? DateFin { get; set; }
        public string Etat { get; set; }
        public string NomUtilisateur { get; set; }  // Ajout de la propriété pour le nom de l'utilisateur
        public string PrenomUtilisateur { get; set; }  // Ajout de la propriété pour le prénom de l'utilisateur
        public string TitreLivre { get; set; }  // Ajout de la propriété pour le titre du livre

        // Méthode pour récupérer tous les Lends de la base de données
        public static List<Lends> GetAll()
        {
            List<Lends> LendsList = new List<Lends>();

            // Requête SQL avec jointures pour récupérer les informations supplémentaires
            string query = @"
                SELECT e.ID, e.id_utilisateur, e.id_livre, e.date_debut, e.date_fin, e.etat, 
                       u.nom AS NomUtilisateur, u.prenom AS PrenomUtilisateur, 
                       l.titre AS TitreLivre
                FROM emprunts e
                JOIN utilisateurs u ON e.id_utilisateur = u.ID
                JOIN livres l ON e.id_livre = l.ID";

            MySqlCommand cmd = new MySqlCommand(query, Program.conn);  // Création de la commande SQL avec la connexion
            MySqlDataReader reader = cmd.ExecuteReader();  // Exécution de la requête et récupération des résultats

            while (reader.Read())
            {
                Lends emprunt = new Lends();

                // Remplir les propriétés avec les données récupérées
                emprunt.Id = reader["ID"] != DBNull.Value ? (int)reader["ID"] : 0;
                emprunt.IdUtilisateur = reader["id_utilisateur"] != DBNull.Value ? (int)reader["id_utilisateur"] : 0;
                emprunt.IdLivre = reader["id_livre"] != DBNull.Value ? (int)reader["id_livre"] : 0;
                emprunt.DateDebut = reader["date_debut"] != DBNull.Value ? (DateTime)reader["date_debut"] : DateTime.MinValue;
                emprunt.DateFin = reader["date_fin"] != DBNull.Value ? (DateTime?)reader["date_fin"] : null;  // Nullable DateTime pour DateFin
                emprunt.Etat = reader["etat"] != DBNull.Value ? (string)reader["etat"] : string.Empty;
                emprunt.NomUtilisateur = reader["NomUtilisateur"] != DBNull.Value ? (string)reader["NomUtilisateur"] : string.Empty;
                emprunt.PrenomUtilisateur = reader["PrenomUtilisateur"] != DBNull.Value ? (string)reader["PrenomUtilisateur"] : string.Empty;
                emprunt.TitreLivre = reader["TitreLivre"] != DBNull.Value ? (string)reader["TitreLivre"] : string.Empty;

                LendsList.Add(emprunt);  // Ajouter l'emprunt à la liste
            }

            reader.Close();  // Fermer le lecteur de données
            return LendsList;  // Retourner la liste des Lends
        }
    }
}
