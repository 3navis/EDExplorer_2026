using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace EDExplorer
{
    // Some explanation is probably in order here, since this on it own probably doesn't appear to do anything.
    // The journal files don't reliably fire the filesystem created/changed events when Elite Dangerous writes to them.
    // This will poke the most recent file in the journal folder once every 250ms by opening it and immediately closing
    // it again, forcing the filesystem to commit whatever changes are pending, since it looks like something else is
    // about to read the file, and fire the delayed events.

    class JournalPoker
    {
        public bool Reset = false;
        private bool Running = false;
        private DirectoryInfo directoryInfo;
        
        public JournalPoker(string dir)
        {
            directoryInfo = new DirectoryInfo(dir);
        }

        public async void Start()
        {
            FileStream stream;
            FileInfo fileToPoke = null;
            int nTicks = 0; // Se verifica el archivo cada 30s
            string JournalNameMask = Properties.Settings.Default.JournalName;

            // Optimizacion definitiva
            // mientras se van leyendo modificaciones, significa que se esta utilizando el mismo fichero
            // usar un semaforo que marque la hora de la ultima utilización

            Running = true;
            await Task.Run(() =>
            {
                while (Running)
                {
                    // 120 ticks entre 4 (250mls cada tick. 4 ticks por segundo) son 30s
                    if (fileToPoke == null || nTicks > 120)
                    {
                        fileToPoke = directoryInfo.GetFiles(JournalNameMask)
 //                           .Where(f => f.CreationTime >= DateTime.Today.AddDays(-10))
                            .OrderByDescending(f => f.CreationTime).FirstOrDefault();

                        nTicks = 0;
                    }

                    if (fileToPoke != null)
                    {
                        stream = fileToPoke.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                        stream.Close();
                    }

                    System.Threading.Thread.Sleep(250);
                    
                    // Reset es true si se modifica el fichero actual
                    nTicks = Reset ? 0 : nTicks + 1;
                    Reset = false;
                }
            });
        }

        public void Stop()
        {
            Running = false;
        }
    }
}
