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
        public bool Etat { get; set; }  // Boolean bien déclaré

        public static List<Books> GetAll()
        {
            List<Books> livres = new List<Books>();

            string query = "SELECT * FROM livres";
            MySqlCommand cmd = new MySqlCommand(query, Program.conn);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Books _livre = new Books
                {
                    Id = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0,
                    Titre = reader["titre"] != DBNull.Value ? reader["titre"].ToString() : string.Empty,
                    Genre = reader["genre"] != DBNull.Value ? reader["genre"].ToString() : string.Empty,
                    Auteur = reader["auteur"] != DBNull.Value ? reader["auteur"].ToString() : string.Empty,
                    DatePublication = reader["date_publication"] != DBNull.Value ? Convert.ToDateTime(reader["date_publication"]) : DateTime.MinValue,
                    Etat = reader["etat"] != DBNull.Value && Convert.ToBoolean(reader["etat"])
                };

                livres.Add(_livre);
            }

            reader.Close();
            return livres;
        }
    }
}
