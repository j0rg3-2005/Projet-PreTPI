using BiblioTech;
using MySqlConnector;
using System;
using System.Collections.Generic;


namespace BiblioTech
{
    public class Utilisateurs
    {

        public int IdUtilisateur { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Telephone { get; set; }
        public string AdressePostale { get; set; }
        public string AdresseEmail { get; set; }


        public static List<Utilisateurs> GetAll(int IdUtilisateur)
        {
            List<Utilisateurs> utilisateurs = new List<Utilisateurs>();

            string query = "SELECT * FROM utilisateurs";
            MySqlCommand cmd = new MySqlCommand(query, DataConnection.conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Utilisateurs _utilisateurs = new Utilisateurs();

                // Remplir les propriétés avec les données récupérées du reader
                _utilisateurs.IdUtilisateur = (int)reader["id_utilisateur"];
                _utilisateurs.Nom = (string)reader["nom"];
                _utilisateurs.Prenom = (string)reader["prenom"];
                _utilisateurs.Telephone = reader["telephone"] != DBNull.Value ? (string)reader["telephone"] : null;  // Vérification pour gérer les valeurs nulles
                _utilisateurs.AdressePostale = reader["adresse_postale"] != DBNull.Value ? (string)reader["adresse_postale"] : null;
                _utilisateurs.AdresseEmail = (string)reader["adresse_email"];



                utilisateurs.Add(_utilisateurs);
            }
            reader.Close();

            return utilisateurs;
        }

    }

}
