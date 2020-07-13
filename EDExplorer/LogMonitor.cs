using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace EDExplorer
{
    public class LogMonitor
    {
        public Base basi;
        private readonly FileSystemWatcher logWatcher;
        private string currentSystem;
        private string currentBody;
        public DateTime currentTime;
        public string CurrentLogPath { get; private set; }
        public string CurrentLogLine { get; private set; }
        public int LastLineProcessed { get; private set; }
        public int bytesRead { get; private set; }
        
        public bool JumponiumReported;
        public bool GoldSystemReported;
        private List<string> LinesToProcess;
        private string LogDirectory;
        private string LogName;
        public bool LastScanValid { get; private set; }
        public bool LastCodexValid { get; private set; }
        public bool LastSignalValid { get; private set; }
        public bool LastFSSValid { get; private set; }
        public bool ReadAllInProgress { get; private set; }
        public bool ReadAllComplete { get; private set; }
        public ScanEvent LastScan { get; private set; }
        public CodexEntry LastCodex { get; private set; }
        public SaaSignalsFound LastSignal { get; private set; }
        public FSSDiscoveryScan LastFSS { get; private set; }
        public Dictionary<(string System, long Body), ScanEvent> SystemBody { get; private set; }
        public Dictionary<(string System, long Body), SaaSignalsFound> SystemBodySignal { get; private set; }
        private JournalPoker Poker;
        public string CurrentSystem
        {
            get
            {
                return currentSystem;
            }
            private set
            {
                if (value != currentSystem)
                {
                    JumponiumReported = false;
                    GoldSystemReported = false;
                }
                currentSystem = value;
            }
        }

        public LogMonitor()
        {
            LogDirectory = Properties.Settings.Default.JournalPath;
            LogDirectory = CheckLogPath();
            Properties.Settings.Default.JournalPath = LogDirectory;
            LogName = Properties.Settings.Default.JournalName;
            Properties.Settings.Default.Save();

            //            logWatcher = new FileSystemWatcher(LogDirectory, "Journal.????????????.??.log")
            logWatcher = new FileSystemWatcher(LogDirectory, LogName)
            {
                NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size | NotifyFilters.FileName
            };
            logWatcher.Changed += LogChanged;
            logWatcher.Created += LogChanged;
            SystemBody = new Dictionary<(string, long), ScanEvent>();
            ReadAllInProgress = false;
            ReadAllComplete = false;
            CurrentSystem = string.Empty;
            JumponiumReported = false;
            SystemBodySignal = new Dictionary<(string, long), SaaSignalsFound>();

            if (Properties.Settings.Default.AutoSTART)
                MonitorStart();
        }

        public void MonitorStart()
        {
            if (!IsMonitoring())
            {
                PopulatePastScans();
                logWatcher.EnableRaisingEvents = true;
                Poker = new JournalPoker(LogDirectory);
                Poker.Start();
            }
        }

        public void MonitorStop()
        {
            if (IsMonitoring())
            {
                logWatcher.EnableRaisingEvents = false;
                Poker.Stop();
                Poker = null;
            }
        }

        public void ReadAll(ProgressBar progressBar, int ultimos = 1000)
        {
            ReadAllInProgress = true;
            progressBar.Visible = true;
            SystemBody.Clear();
            SystemBodySignal.Clear();
            DirectoryInfo logDir = new DirectoryInfo(CheckLogPath());
            //FileInfo[] allJournalsLL = logDir.GetFiles(Properties.Settings.Default.JournalName);

            int progress = 0;

            // new DateTime(2017, 04, 12)
            FileInfo[] aux = logDir.GetFiles(Properties.Settings.Default.JournalName)
                .Where(f => f.CreationTime > new DateTime(2017, 04, 12))
                .OrderBy(f => f.LastWriteTime).ToArray();

            if (ultimos > aux.Count() || ultimos <= 0) ultimos = aux.Count();

            FileInfo[] allJournals = new FileInfo[ultimos];
            Array.Copy(aux, aux.Count() - ultimos, allJournals, 0, ultimos);

            try
            {
                progress = 0;

                foreach (var journalFile in allJournals)
                {
                    using (StreamReader currentLog = new StreamReader(File.Open(journalFile.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
                        {
                            while (!currentLog.EndOfStream)
                            {
                                CurrentLogLine = currentLog.ReadLine();
                                ProcessEvent(CurrentLogLine);
                            
                                // IDEA: Se podria salir cuando lleve XX Alertas?
                                // Quizas un Count de la lista en pantalla?

                            }
                        }
                  
                    progressBar.Value = (progress++ * 100) / allJournals.Count();
                    progressBar.Refresh();
                }
            }
            catch (Exception ex)
            {
                DialogResult response = MessageBox.Show("Ha ocurrido un error al leer los archivos de log. ¿Quiere ver información de Detalle adicional?", "Error Leyendo Logs", MessageBoxButtons.YesNo);
                if (response == DialogResult.Yes)
                {
                    MessageBox.Show($"Journal Line: {CurrentLogLine}\r\nException message: {ex.Message}\r\n\r\nStack trace: {ex.StackTrace}", "Detalle del Error", MessageBoxButtons.OK);
                }
            }
            progressBar.Visible = false;
            ReadAllInProgress = false;
            ReadAllComplete = true;
        }

        public bool IsMonitoring()
        {
            return logWatcher.EnableRaisingEvents;
        }

        private string CheckLogPath()
        {
            LogDirectory = string.IsNullOrEmpty(LogDirectory) ? Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Saved Games\\Frontier Developments\\Elite Dangerous" : LogDirectory;
            if (!Directory.Exists(LogDirectory) || new DirectoryInfo(LogDirectory).GetFiles(Properties.Settings.Default.JournalName).Count() == 0)
            {
                System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog
                {
                    RootFolder = Environment.SpecialFolder.MyComputer,
                    ShowNewFolderButton = false,
                    Description = "Seleccionar la carpeta de Elite Dangerous Journal"
                };
                System.Windows.Forms.DialogResult result = folderBrowserDialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                {
                    LogDirectory = folderBrowserDialog.SelectedPath;
                }
            }
            return LogDirectory;
        }

        private void LogChanged(object source, FileSystemEventArgs e)
        {
            if (Poker != null) Poker.Reset = true;

            switch (e.ChangeType)
            {
                case WatcherChangeTypes.Created:
                    CurrentLogPath = e.FullPath;
                    CurrentLogPath = string.Empty;
                    LastLineProcessed = 0;
                    bytesRead = 0;
                    break;

                case WatcherChangeTypes.Changed:
                    if (CurrentLogPath != e.FullPath)
                    {
                        CurrentLogPath = e.FullPath;
                        LastLineProcessed = 0;
                        bytesRead = 0;
                    }

                    //////////////////////////////////////////////////////////////////////////////
                    int newLineBytes = System.Environment.NewLine.Length;
                    string linea;

                    using (StreamReader currentLog = new StreamReader(File.Open(CurrentLogPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
                    {
                        // Primero salta las lineas ya procesadas anteriormente
                        currentLog.BaseStream.Seek(bytesRead, SeekOrigin.Begin);

                        // Segundo actualiza la posicion con las nuevas lineas leidas del fichero
                        for (LinesToProcess = new List<string>(); !currentLog.EndOfStream; LastLineProcessed++)
                        {
                            linea = currentLog.ReadLine();
                            LinesToProcess.Add(linea);
                            bytesRead += linea.Length + newLineBytes;
                        }
                    }
                    //////////////////////////////////////////////////////////////////////////////
                    //using (StreamReader currentLog = new StreamReader(File.Open(CurrentLogPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
                    //{
                    //    // Primero salta las lineas ya procesadas anteriormente
                    //    for (int n = 0; !currentLog.EndOfStream && n < LastLineProcessed; n++)
                    //    { currentLog.ReadLine(); }

                    //    // Segundo actualiza la posicion con las nuevas lineas leidas del fichero
                    //    for (LinesToProcess = new List<string>(); !currentLog.EndOfStream; LastLineProcessed++)
                    //    { LinesToProcess.Add(currentLog.ReadLine()); }
                    //}

                    foreach (string line in LinesToProcess)
                        ProcessEvent(line);
                    
                    break;

                default:
                    break;
            }
        }

        private void ProcessEvent(string logLine)
        {
            if (logLine.Trim().StartsWith("{") && logLine.Trim().EndsWith("}"))
            {
                if (true) // modo exploracion
                {
                    if (logLine.Contains("\"event\":\"Scan\"") ||
                        logLine.Contains("\"event\":\"Location\"") ||
                        logLine.Contains("\"event\":\"FSDJump\"") ||
                        logLine.Contains("\"event\":\"CarrierJump\"") ||
 //                       logLine.Contains("\"event\":\"CodexEntry\"") ||
                        logLine.Contains("\"event\":\"SAASignalsFound\"") ||
                        logLine.Contains("\"event\":\"FSSDiscoveryScan\"") ||
                        logLine.Contains("\"event\":\"SupercruiseExit\""))
                    {
                        ProcessLine(logLine);
                    }
                }
                else if (false) // modo minero
                {
                    if (logLine.Contains("\"event\":\"SAASignalsFound\"") ||
                        logLine.Contains("\"event\":\"ProspectedAsteroid\""))
                    {
                        ProcessLine(logLine);
                    }
                }
            }
        }
        private void ProcessLine(string logLine)
        {

            if (logLine != null)
            {
                JObject lastEvent = (JObject)JsonConvert.DeserializeObject(logLine, new JsonSerializerSettings() { DateParseHandling = DateParseHandling.None });
                currentTime = (DateTime)lastEvent["timestamp"];
                LastScanValid = false;
                LastCodexValid = false;
                LastSignalValid = false;
                LastFSSValid = false;

                switch (lastEvent["event"].ToString())
                {
                    case "Scan":
                        if (!lastEvent["BodyName"].ToString().Contains("Belt Cluster"))
                        {
                            LastScan = lastEvent.ToObject<ScanEvent>();
                            LastScan.JournalEntry = logLine;

                            if (!SystemBody.ContainsKey((CurrentSystem, LastScan.BodyId)))
                            {
                                SystemBody[(CurrentSystem, LastScan.BodyId)] = LastScan;
                                LastScanValid = true;
                            }
                        }
                        break;
                    case "SAASignalsFound":
                        LastSignal = lastEvent.ToObject<SaaSignalsFound>();
                        //LastSignal.Body = currentBody;

                        if (!SystemBodySignal.ContainsKey((CurrentSystem, (long)LastSignal.BodyId)))
                        {
                            SystemBodySignal[(CurrentSystem, (long)LastSignal.BodyId)] = LastSignal;
                            LastSignalValid = true;
                        }                    
                        break;
                    case "FSDJump":
                    case "CarrierJump":
                        // Al entrar los Carriers no se actualizaba el nombre en el salto
                        CurrentSystem = lastEvent["StarSystem"].ToString();
                        break;
                    case "Location":
                        CurrentSystem = lastEvent["StarSystem"].ToString();
                        break;
                    case "FSSDiscoveryScan":
                        LastFSS = lastEvent.ToObject<FSSDiscoveryScan>();
                        if (LastFSS.SystemName == null) 
                        { LastFSS.SystemName = CurrentSystem; }
                        CurrentSystem = LastFSS.SystemName; // lastEvent["SystemName"].ToString();
                        //if (CurrentSystem != null) { LastFSSValid = true; }
                        LastFSSValid = true;
                        break;
                    case "FSSAllBodiesFound":
                        if (lastEvent["SystemName"] != null)
                        {
                            CurrentSystem = lastEvent["SystemName"].ToString();
                        }
                        break;
                    case "CodexEntry":
                        if (LastCodex?.Timestamp != lastEvent.ToObject<CodexEntry>().Timestamp)
                        {
                            LastCodex = lastEvent.ToObject<CodexEntry>();
                            LastCodex.Body = currentBody;
                            LocaliseLastCodex();
                            LastCodexValid = true;
                        }
                        break;
                    case "SupercruiseExit":
                        if (lastEvent["Body"]?.ToString().Length > 0)
                            currentBody = lastEvent["Body"].ToString();
                        else
                            currentBody = null;
                        break;
                    case "ProspectedAsteroid":
                        break;
                    default:
                        break;
                }
            }

            if (LastScanValid || LastSignalValid || LastCodexValid || LastFSSValid)
            {
                //////////////////////////////////////////////////////////////////////////////////
                EventHandler entry = LogEntry;
                entry?.Invoke(this, EventArgs.Empty); // => Base.LogEvent
                //////////////////////////////////////////////////////////////////////////////////
            }
        }

        private void PopulatePastScans()
        {
            FileInfo fileToRead = null;

            foreach (var file in new DirectoryInfo(LogDirectory).GetFiles(Properties.Settings.Default.JournalName))
            {
                if (fileToRead == null || string.Compare(file.Name, fileToRead.Name) > 0)
                {
                    fileToRead = file;
                }
            }

            if (fileToRead != null)
                using (StreamReader currentLog = new StreamReader(fileToRead.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
                {
                    while (!currentLog.EndOfStream)
                    {
                        string logLine = currentLog.ReadLine();
                        if (logLine.Trim().StartsWith("{") && logLine.Trim().EndsWith("}") && logLine.Contains("\"event\":\"Scan\"") || logLine.Contains("\"event\":\"Location\"") || logLine.Contains("\"event\":\"FSDJump\""))
                        {

                            JObject scanEvent = (JObject)JsonConvert.DeserializeObject(logLine, new JsonSerializerSettings() { DateParseHandling = DateParseHandling.None });

                            switch (scanEvent["event"].ToString())
                            {
                                case "Scan":

                                    if (!scanEvent["BodyName"].ToString().Contains("Belt Cluster"))
                                    {
                                        ScanEvent scan = scanEvent.ToObject<ScanEvent>();
                                        if (!SystemBody.ContainsKey((CurrentSystem, scan.BodyId)))
                                        {
                                            SystemBody[(CurrentSystem, scan.BodyId)] = scan;
                                        }
                                    }
                                    break;
                                case "SAASignalsFound":
                                    SaaSignalsFound signal = scanEvent.ToObject<SaaSignalsFound>();

                                    if (!SystemBodySignal.ContainsKey((CurrentSystem, (long)signal.BodyId)))
                                    {
                                        SystemBodySignal[(CurrentSystem, (long)signal.BodyId)] = signal;
                                    }
                                    break;
                                case "FSDJump":
                                case "Location":
                                    CurrentSystem = scanEvent["StarSystem"].ToString();
                                    break;
                            }
                        }
                    }
                }
        }

        //Frontier's codex name localisations are frequently lacking detail or otherwise unhelpful.
        //Need to fix that to display useful descriptions.
        private void LocaliseLastCodex()
        {
            switch (LastCodex.NameLocalised)
            {
                case "Standard gas giant":
                case "Green gas giant":
                    LastCodex.NameLocalised =
                        LastCodex.NameLocalised.Replace("gas giant", "Gas Giant:").Replace("Standard ", "") +
                        LastCodex.Name
                            .Replace("$Codex_Ent_Standard_", " ")
                            .Replace("Sudarsky_", "")
                            .Replace("_Name;", "")
                            .Replace("Giant_With_", "")
                            .Replace("_", " ");
                    break;

                default:

                    switch (LastCodex.Name)
                    {
                        case "$Codex_Ent_Standard_Ammonia_Worlds_Name;":
                            LastCodex.NameLocalised = "Ammonia World";
                            break;
                        case "$Codex_Ent_TRF_Ammonia_Worlds_Name;":
                            LastCodex.NameLocalised = "Terraformable Ammonia World";
                            break;
                        case "$Codex_Ent_Standard_High_Metal_Content_No_Atmos_Name;":
                            LastCodex.NameLocalised = "High Metal Content w/o Atmosphere";
                            break;
                        case "$Codex_Ent_Standard_Ter_High_Metal_Content_Name;":
                            LastCodex.NameLocalised = "High Metal Content w/ Atmosphere";
                            break;
                        case "$Codex_Ent_TRF_High_Metal_Content_No_Atmos_Name;":
                            LastCodex.NameLocalised = "Terraformable High Metal Content w/o Atmosphere";
                            break;
                        case "$Codex_Ent_TRF_Ter_High_Metal_Content_Name;":
                            LastCodex.NameLocalised = "Terraformable High Metal Content w/ Atmosphere";
                            break;
                        case "$Codex_Ent_Standard_Ice_No_Atmos_Name;":
                            LastCodex.NameLocalised = "Icy Body w/o Atmosphere";
                            break;
                        case "$Codex_Ent_Standard_Ter_Ice_Name;":
                            LastCodex.NameLocalised = "Icy Body w/ Atmosphere";
                            break;
                        case "$Codex_Ent_TRF_Ice_No_Atmos_Name;":
                            LastCodex.NameLocalised = "Terraformable Icy Body w/o Atmosphere";
                            break;
                        case "$Codex_Ent_TRF_Ter_Ice_Name;":
                            LastCodex.NameLocalised = "Terraformable Icy Body w/ Atmosphere";
                            break;
                        case "$Codex_Ent_Standard_Rocky_Ice_No_Atmos_Name;":
                            LastCodex.NameLocalised = "Rocky Ice Body w/o Atmosphere";
                            break;
                        case "$Codex_Ent_Standard_Ter_Rocky_Ice_Name;":
                            LastCodex.NameLocalised = "Rocky Ice Body w/ Atmosphere";
                            break;
                        case "$Codex_Ent_TRF_Rocky_Ice_No_Atmos_Name;":
                            LastCodex.NameLocalised = "Terraformable Rocky Ice Body w/o Atmosphere";
                            break;
                        case "$Codex_Ent_TRF_Ter_Rocky_Ice_Name;":
                            LastCodex.NameLocalised = "Terraformable Rocky Ice Body w/ Atmosphere";
                            break;
                        case "$Codex_Ent_Standard_Rocky_No_Atmos_Name;":
                            LastCodex.NameLocalised = "Rocky Body w/o Atmosphere";
                            break;
                        case "$Codex_Ent_Standard_Ter_Rocky_Name;":
                            LastCodex.NameLocalised = "Rocky Body w/ Atmosphere";
                            break;
                        case "$Codex_Ent_TRF_Rocky_No_Atmos_Name;":
                            LastCodex.NameLocalised = "Terraformable Rocky Body w/o Atmosphere";
                            break;
                        case "$Codex_Ent_TRF_Ter_Rocky_Name;":
                            LastCodex.NameLocalised = "Terraformable Rocky Body w/ Atmosphere";
                            break;
                        case "$Codex_Ent_Standard_Metal_Rich_No_Atmos_Name;":
                            LastCodex.NameLocalised = "Metal Rich Body w/o Atmosphere";
                            break;
                        case "$Codex_Ent_Standard_Ter_Metal_Rich_Name;":
                            LastCodex.NameLocalised = "Metal Rich Body w/ Atmosphere";
                            break;
                        case "$Codex_Ent_TRF_Metal_Rich_No_Atmos_Name;":
                            LastCodex.NameLocalised = "Terraformable Metal Rich Body w/o Atmosphere";
                            break;
                        case "$Codex_Ent_TRF_Ter_Metal_Rich_Name;":
                            LastCodex.NameLocalised = "Terraformable Metal Rich Body w/ Atmosphere";
                            break;
                        case "$Codex_Ent_Standard_Water_Worlds_Name;":
                            LastCodex.NameLocalised = "Water World";
                            break;
                        case "$Codex_Ent_TRF_Water_Worlds_Name;":
                            LastCodex.NameLocalised = "Terraformable Water World";
                            break;
                    }
                    break;
            }
        }

        public event EventHandler LogEntry;
    }
}
