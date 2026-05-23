// TrayIcon
using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace EDExplorer
{
    static class EDExplorer
    {
        [STAThread]

        static void Main()
        {
            try {
            //MessageBox.Show("Config path:\n");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (SingleLaunch.IsRunning)
                return; // Si se encuentra en ejecucion salir

            // Solo entraría aquí la primera vez, para crear los valores por defecto.
            if (Properties.Settings.Default.SettingsDefault)
            {
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.SettingsDefault = false;
                Properties.Settings.Default.Save();
            }

            //CultureInfo.CurrentCulture = new CultureInfo("es-ES", false);
            if (System.Globalization.CultureInfo.CurrentUICulture.Name != Properties.Settings.Default.Idioma)
            {
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Properties.Settings.Default.Idioma);
            }

            using (var trayController = new TrayIconController())
            {
                // Application.Run(new EDExplorerFrm());
                Application.Run();
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.ToString(),
                    "FATAL ERROR");
            }
        }
    }
}
