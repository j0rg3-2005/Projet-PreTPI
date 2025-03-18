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
        public int Etat { get; set; } // Ajout de la propriété Etat (1 = Disponible, 0 = Indisponible)

        public static List<Books> GetAll(int? etatFilter = null)
        {
            List<Books> livres = new List<Books>();

            string query = "SELECT * FROM livres";
            if (etatFilter.HasValue)
            {
                query += " WHERE etat = @Etat"; // Ajout d'un filtre si un état est fourni
            }

            MySqlCommand cmd = new MySqlCommand(query, Program.conn);
            if (etatFilter.HasValue)
            {
                cmd.Parameters.AddWithValue("@Etat", etatFilter.Value);
            }

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Books _livre = new Books
                {
                    Id = reader["ID"] != DBNull.Value ? (int)reader["ID"] : 0,
                    Titre = reader["titre"] != DBNull.Value ? (string)reader["titre"] : string.Empty,
                    Genre = reader["genre"] != DBNull.Value ? (string)reader["genre"] : string.Empty,
                    Auteur = reader["auteur"] != DBNull.Value ? (string)reader["auteur"] : string.Empty,
                    DatePublication = reader["date_publication"] != DBNull.Value ? (DateTime)reader["date_publication"] : DateTime.MinValue,
                    Etat = reader["etat"] != DBNull.Value ? Convert.ToInt32(reader["etat"]) : 0 // Lecture de l'état
                };

                livres.Add(_livre);
            }

            reader.Close();
            return livres;
        }
    }
}
