using System;
using System.Data.SQLite;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

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

            if (IsDbRecentlyCreated)
            {
                ExecuteSQL("create table FileLog (filename varchar(200))");
                ExecuteSQL("CREATE UNIQUE INDEX[IDX_FILELOG_] ON [FileLog]([filename])");

                ExecuteSQL("CREATE TABLE Sistema (Id INTEGER, Nombre varchar(200) NOT NULL PRIMARY KEY, PrimeraVisita varchar(20), UltimaVisita varchar(20), NumVisitas integer)");
                ExecuteSQL("CREATE UNIQUE INDEX[IDX_SISTEMAID_] ON [Sistema]([Nombre])");

                ExecuteSQL("CREATE TABLE Cuerpo (Id INTEGER NOT NULL PRIMARY KEY, Nombre varchar(200))");
            }

 //           AddSystem(1, "Tierra");
 //           AddSystem(2, "Sol");
        }

        public static SQLiteConnection GetInstance()
        {
            var db = new SQLiteConnection(string.Format("Data Source={0}", DBName));
            db.Open();

            return db;
        }

        public static void ExecuteSQL(string sql)
        {
            SQLiteCommand command = new SQLiteCommand(sql, con);
            command.ExecuteNonQuery();
            
            return;
        }

        public static long AddSystem(ulong id, string name, DateTime fecha)
        {
            try
            {
                //if (id == 0) return 0;

                SQLiteDataReader dr;
                SQLiteCommand cmd = new SQLiteCommand();

                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT id, PrimeraVisita, UltimaVisita FROM Sistema ";
                cmd.CommandText += string.Format("WHERE (Nombre=\"{0}\")", name);

                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    //ulong id2 = dr.GetInt64(0);
                    //string myreader = dr.GetString(1);
                    //var name = reader.GetString(0);
                    //long l = (long)reader[0];

                    string PrimeraVisita = (string)dr[1];
                    string UltimaVisita = (string)dr[2];
                    string date = fecha.ToString("u", DateTimeFormatInfo.InvariantInfo);

                    if ((String.Compare(UltimaVisita, date, StringComparison.Ordinal) < 0)
                      | (String.Compare(PrimeraVisita, date, StringComparison.Ordinal) > 0))
                        using (var transaction = con.BeginTransaction())
                        {
                            //cmd = new SQLiteCommand();
                            cmd = con.CreateCommand();

                            cmd.CommandText = "UPDATE Sistema ";
                            if (String.Compare(UltimaVisita, date, StringComparison.Ordinal) < 0)
                                cmd.CommandText += string.Format("SET UltimaVisita = \"{0}\" ", date);
                            else
                                cmd.CommandText += string.Format("SET PrimeraVisita = \"{0}\" ", date);
                            cmd.CommandText += string.Format("  , NumVisitas = NumVisitas + 1 ", date);
                            cmd.CommandText += string.Format("WHERE (Nombre=\"{0}\")", name);
                            cmd.ExecuteNonQuery();

                            transaction.Commit();
                        }
                }
                else
                {
                    using (var transaction = con.BeginTransaction())
                    {
                        String date;
                        date = fecha.ToString("u", DateTimeFormatInfo.InvariantInfo);

                        //cmd = new SQLiteCommand();
                        cmd = con.CreateCommand();

                        cmd.CommandText = "INSERT INTO Sistema (Id, Nombre, PrimeraVisita, UltimaVisita, NumVisitas) ";
                        cmd.CommandText += string.Format("VALUES ({0},\"{1}\",\"{2}\",\"{3}\",1)", id, name, date, date);
                        cmd.ExecuteNonQuery();

                        transaction.Commit();
                    }
                }

                dr.Dispose();
                return 0;
            }
            catch (Exception ex)
            {
                DialogResult response = MessageBox.Show("Ha ocurrido un error en ProcessLine. ¿Quiere ver información de Detalle adicional?", "Error Procesando Linea", MessageBoxButtons.YesNo);
                if (response == DialogResult.Yes)
                {
                    MessageBox.Show($"Evento: \r\nLinea: \r\nException message: {ex.Message}\r\n\r\nStack trace: {ex.StackTrace}", "Detalle del Error", MessageBoxButtons.OK);
                }
                return -1;
            }
        }

    }
}
