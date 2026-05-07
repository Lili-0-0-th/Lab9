using System;
using System.Data.SQLite;
using System.IO;

namespace Lab9
{
    public class DatabaseManager
    {
        private string connectionString = @"Data Source=WnioskiDB.db; Version=3;";

        public DatabaseManager()
        {
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            if (!File.Exists("WnioskiDB.db"))
            {
                SQLiteConnection.CreateFile("WnioskiDB.db");
            }

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string createTableQuery = @"
                    CREATE TABLE IF NOT EXISTS Wnioski (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        DataWniosku TEXT,
                        NumerAlbumu TEXT,
                        ImieNazwisko TEXT,
                        Semestr TEXT,
                        Rok TEXT,
                        Kierunek TEXT,
                        Stopien TEXT,
                        Przedmiot TEXT,
                        Punkty TEXT,
                        Prowadzacy TEXT,
                        Uzasadnienie TEXT,
                        PodpisStudenta TEXT,
                        Decyzja TEXT,
                        SkladKomisji TEXT,
                        DataDecyzji TEXT
                    )";

                using (SQLiteCommand command = new SQLiteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
