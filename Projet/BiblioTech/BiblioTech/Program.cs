using MySqlConnector;


namespace BiblioTech
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        /// 

        public static MySqlConnection conn;

        [STAThread]
        static void Main(string[] args)
        {
            string server = "localhost";
            string database = "bibliotech";
            string username = "root";
            string password = "Pa$$w0rd";
            string constring = "SERVER=" + server + ";" + "DATABASE=" + database + ";" +
                "UID=" + username + ";" + "PASSWORD=" + password + ";";

            conn = new MySqlConnection(constring);
            conn.Open();

            ApplicationConfiguration.Initialize();
            Application.Run(new frmChoiceMenu());
        }
    }
}