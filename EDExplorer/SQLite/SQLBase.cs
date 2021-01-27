using System.Data.SQLite;
using System.IO;

// CREATE TABLE Systems (edsmid INTEGER PRIMARY KEY NOT NULL , sectorid INTEGER, nameid INTEGER, x INTEGER, y INTEGER, z INTEGER)
// CREATE TABLE JournalEntries ( Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,TravelLogId INTEGER NOT NULL REFERENCES TravelLogUnit(Id), CommanderId INTEGER NOT NULL DEFAULT 0,EventTypeId INTEGER NOT NULL, EventType TEXT, EventTime DATETIME NOT NULL, EventData TEXT, EdsmId INTEGER, Synced INTEGER )
// CREATE TABLE route_systems (id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, routeid INTEGER NOT NULL, systemname TEXT NOT NULL)

namespace EDExplorer
{
    public class SQLBase
    {
        private const string DBName = "EDExplorer.sqlite";
        private static bool IsDbRecentlyCreated = false;
        private static SQLiteConnection con;
        private SQLiteCommand command;
        private string sql;

        public static void Up()
        {
            var type = typeof(System.Data.Entity.SqlServer.SqlProviderServices);

            // Crea la base de datos y registra usuario solo una vez
            if (!File.Exists(Path.GetFullPath(DBName)))
            {
                SQLiteConnection.CreateFile(DBName);
                IsDbRecentlyCreated = true;
            }

            con = GetInstance();
            //using (var con = GetInstance())
            //{
            if (IsDbRecentlyCreated)
            {
                ExecuteSQL("create table FileLog (filename varchar(200))");
                ExecuteSQL("CREATE UNIQUE INDEX[IDX_FILELOG_] ON [FileLog]([filename])");

                ExecuteSQL("CREATE TABLE Sistema (Id INTEGER NOT NULL PRIMARY KEY, Nombre varchar(200))");
                ExecuteSQL("CREATE TABLE Cuerpo (Id INTEGER NOT NULL PRIMARY KEY, Nombre varchar(200))");

                AddSystem(1, "Tierra");
                AddSystem(2, "Sol");
            }
        }

        public static SQLiteConnection GetInstance()
        {
            var db = new SQLiteConnection(string.Format("Data Source={0}", DBName));
            db.Open();

            return db;
        }

        private void SetConnection()
        {
            con = new SQLiteConnection
                ("Data Source=c:\\Dev\\MYApp.sqlite;Version=3;New=False;Compress=True;");
        }

        private void ExecuteQuery(string txtQuery)
        {
            SetConnection();
            con.Open();

            SQLiteCommand cmd = new SQLiteCommand();

            using (cmd = new SQLiteCommand(con))
            {
                using (var transaction = con.BeginTransaction())
                {
                    for (var i = 0; i < 1000000; i++)
                    {
                        cmd.CommandText = "insert into Student(FirstName,LastName) values ('John','Doe')";
                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
            }


            con.Close();
            return;
        }

        public static void ExecuteSQL(string sql)
        {
            SQLiteCommand command = new SQLiteCommand(sql, con);
            command.ExecuteNonQuery();
            
            return;
        }

        public static long AddSystem(long id, string name)
        {
            SQLiteCommand cmd = new SQLiteCommand();

            using (var transaction = con.BeginTransaction())
            {
                cmd.CommandText = "INSERT INTO Sistema (Id, Nombre) ";
                cmd.CommandText += string.Format("VALUES ({0},\"{1}\")", id, name);
                cmd.ExecuteNonQuery();

                transaction.Commit();
            }

            return 0;
        }

    }
}
