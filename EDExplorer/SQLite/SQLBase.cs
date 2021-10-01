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
        //private static string ftUS = "CultureInfo.CreateSpecificCulture(\"en-GB\")";
        private static System.Globalization.CultureInfo ftGB = new System.Globalization.CultureInfo("en-GB");

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

            IsDbRecentlyCreated = true;
            if (IsDbRecentlyCreated)
            {
                //ExecuteSQL("create table FileLog (filename varchar(200))");
                //ExecuteSQL("CREATE UNIQUE INDEX[IDX_FILELOG_] ON [FileLog]([filename])");

                //ExecuteSQL("CREATE TABLE Sistema (Id INTEGER, Nombre varchar(200) NOT NULL PRIMARY KEY, PrimeraVisita varchar(20), UltimaVisita varchar(20), NumVisitas integer, PosX real, PosY real, PosZ real)");
                //ExecuteSQL("CREATE UNIQUE INDEX[IDX_SISTEMAID_] ON [Sistema]([Nombre])");

                ExecuteSQL("DROP TABLE Cuerpo");
                ExecuteSQL("CREATE TABLE Cuerpo (SistemaId INTEGER, Id INTEGER, Nombre varchar(200) NOT NULL PRIMARY KEY)");
                ExecuteSQL("CREATE UNIQUE INDEX[IDX_CUERPOID_] ON [Cuerpo]([Nombre])");
            }
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

        public static long JumpSystem(ulong id, string name, DateTime fecha, double x, double y, double z)
        {
            try
            {
                SQLiteDataReader dr;
                SQLiteCommand cmd = new SQLiteCommand();

                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT id, PrimeraVisita, UltimaVisita, PosX, PosY, PosZ FROM Sistema ";
                cmd.CommandText += string.Format("WHERE (Nombre=\"{0}\")", name);

                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    //double dx = (double)dr[3];
                    //double dy = (double)dr[4];
                    //double dz = (double)dr[5];

                    //if (Math.Round(dx) != Math.Round(x) || Math.Round(dy) != Math.Round(y) || Math.Round(dz) != Math.Round(z))
                    //{ name = name; } // NO HAY SISTEMAS QUE SE MUEVAN

                    string setSQL = ""; // "SET NumVisitas = NumVisitas + 1 ";
                    ulong SistemaID = (ulong)dr.GetInt64(0); // (ulong)dr[0];

                    if (SistemaID == 0 & id != 0)
                        setSQL += string.Format(", id = {0} ", id);

                    string PrimeraVisita = (string)dr[1]; // GetString(1);
                    string UltimaVisita = (string)dr[2];
                    string f = fecha.ToString("u", DateTimeFormatInfo.InvariantInfo);

                    if (String.Compare(UltimaVisita, f, StringComparison.Ordinal) < 0)
                        setSQL += string.Format(", UltimaVisita = \"{0}\" ", f);

                    if (String.Compare(PrimeraVisita, f, StringComparison.Ordinal) > 0)
                        setSQL += string.Format(", PrimeraVisita = \"{0}\" ", f);

                    if (setSQL != "")
                        using (var transaction = con.BeginTransaction())
                        {
                            //cmd = new SQLiteCommand();
                            cmd = con.CreateCommand();

                            cmd.CommandText = "UPDATE Sistema ";
                            cmd.CommandText += "SET NumVisitas = NumVisitas + 1 ";
                            cmd.CommandText += setSQL;
                            cmd.CommandText += string.Format("WHERE (Nombre=\"{0}\")", name);
                            cmd.ExecuteNonQuery();

                            transaction.Commit();
                        }
                }
                else 
                {
                    using (var transaction = con.BeginTransaction())
                    {
                        String f;
                        f = fecha.ToString("u", DateTimeFormatInfo.InvariantInfo);

                        //cmd = new SQLiteCommand();
                        cmd = con.CreateCommand();

                        cmd.CommandText = "INSERT INTO Sistema (Id, Nombre, PrimeraVisita, UltimaVisita, NumVisitas, PosX, PosY, PosZ) ";
                        cmd.CommandText += string.Format(ftGB, "VALUES ({0},\"{1}\",\"{2}\",\"{3}\",1,{4},{5},{6})", id,name,f,f,x,y,z);
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

        public static long AddSystem(ulong id, string name)
        {
            try
            {
                SQLiteDataReader dr;
                SQLiteCommand cmd = new SQLiteCommand();

                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT id FROM Sistema ";
                cmd.CommandText += string.Format("WHERE (Nombre=\"{0}\")", name);

                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    ulong SistemaID = (ulong)dr.GetInt64(0); 

                    if (SistemaID == 0 & id != 0)
                        using (var transaction = con.BeginTransaction())
                        {
                            //cmd = new SQLiteCommand();
                            cmd = con.CreateCommand();

                            cmd.CommandText = "UPDATE Sistema ";
                            cmd.CommandText += string.Format("SET id = {0} ", id);
                            cmd.CommandText += string.Format("WHERE (Nombre=\"{0}\")", name);
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

        public static long AddCuerpo(ulong SistemaId, long id, string name)
        {
            try
            {
                SQLiteDataReader dr;
                SQLiteCommand cmd = new SQLiteCommand();

                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT id FROM Cuerpo ";
                cmd.CommandText += string.Format("WHERE (Nombre=\"{0}\")", name);

                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    long CuerpoID = (long)dr.GetInt64(0);

                    if (CuerpoID == 0 & id != 0)
                        using (var transaction = con.BeginTransaction())
                        {
                            //cmd = new SQLiteCommand();
                            cmd = con.CreateCommand();

                            cmd.CommandText = "UPDATE Cuerpo ";
                            cmd.CommandText += string.Format("SET id = {0} ", id);
                            cmd.CommandText += string.Format("WHERE (Nombre=\"{0}\")", name);
                            cmd.ExecuteNonQuery();

                            transaction.Commit();
                        }
                }
                else
                {
                    using (var transaction = con.BeginTransaction())
                    {
                        //cmd = new SQLiteCommand();
                        cmd = con.CreateCommand();

                        cmd.CommandText = "INSERT INTO Cuerpo (SistemaId, Id, Nombre) ";
                        cmd.CommandText += string.Format(ftGB, "VALUES ({0},{1},\"{2}\")", SistemaId, id, name);
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
