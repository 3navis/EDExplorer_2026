//using System.Data.SQLite;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;
//using Microsoft.Data.SQLite;

// CREATE TABLE Systems (edsmid INTEGER PRIMARY KEY NOT NULL , sectorid INTEGER, nameid INTEGER, x INTEGER, y INTEGER, z INTEGER)
// CREATE TABLE JournalEntries ( Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,TravelLogId INTEGER NOT NULL REFERENCES TravelLogUnit(Id), CommanderId INTEGER NOT NULL DEFAULT 0,EventTypeId INTEGER NOT NULL, EventType TEXT, EventTime DATETIME NOT NULL, EventData TEXT, EdsmId INTEGER, Synced INTEGER )
// CREATE TABLE route_systems (id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, routeid INTEGER NOT NULL, systemname TEXT NOT NULL)

namespace EDExplorer
{
    public class SQLBase
    {
        private const string DBName = "EDExplorer.sqlite";
        private static bool IsDbRecentlyCreated = false;
        private static SqliteConnection ctx;
        

        public static void Up()
        {
            // Crea la base de datos y registra usuario solo una vez
            if (!File.Exists(Path.GetFullPath(DBName)))
            {
                //SqliteConnection.CreateFile(DBName);
                IsDbRecentlyCreated = true;
            }

            ctx = GetInstance();
            //using (var ctx = GetInstance())
            //{
            if (IsDbRecentlyCreated)
            {
                string sql = "create table FileLog (filename varchar(200))";

                SqliteCommand command = new SqliteCommand(sql, ctx);
                command.ExecuteNonQuery();

                sql = "CREATE UNIQUE INDEX[IDX_FILELOG_] ON [FileLog]([filename])";

                command = new SqliteCommand(sql, ctx);
                command.ExecuteNonQuery();
            }

            for (var i = 1; i <= 100; i++)
            {
                var query = "INSERT INTO FileLog (filename) VALUES (?)";

                using (var comando = new SqliteCommand(query, ctx))
                {
                    comando.Parameters.Add(new SqliteParameter("filename", "Name " + i));
                    //comando.ExecuteNonQuery();
                }
            }
        }

        public static SqliteConnection GetInstance()
        {
            var db = new SqliteConnection(string.Format("Data Source={0}", DBName));
            //SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlite3());
            //SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_dynamic_cdecl());
            db.Open();

            return db;
        }
    }
}
