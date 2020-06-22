using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace EDExplorer
{
    // Some explanation is probably in order here, since this on it own probably doesn't appear to do anything.
    // The journal files don't reliably fire the filesystem created/changed events when Elite Dangerous writes to them.
    // This will poke the most recent file in the journal folder once every 250ms by opening it and immediately closing
    // it again, forcing the filesystem to commit whatever changes are pending, since it looks like something else is
    // about to read the file, and fire the delayed events.

    class JournalPoker
    {
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

            Running = true;
            await Task.Run(() =>
            {
                while (Running)
                {
                    // Busca el ultimo fichero del directorio ??????
                    /*                    foreach (var file in directoryInfo.GetFiles(Properties.Settings.Default.JournalName))
                                        {
                                            if (fileToPoke == null || string.Compare(file.Name, fileToPoke.Name) > 0)
                                            {
                                                fileToPoke = file;
                                            }
                                        }*/
                    if (fileToPoke == null || nTicks > 120)
                    {
                        fileToPoke = directoryInfo.GetFiles(Properties.Settings.Default.JournalName)
                            .Where(f => f.CreationTime >= DateTime.Today.AddDays(-10))
                            .OrderByDescending(f => f.LastWriteTime).FirstOrDefault();

                        nTicks = 0;

                    }

                    stream = fileToPoke.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    stream.Close();

                    System.Threading.Thread.Sleep(250);
                    nTicks++;
                }
            });
        }

        public void Stop()
        {
            Running = false;
        }
    }
}
