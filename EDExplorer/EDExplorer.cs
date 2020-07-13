using System;
using System.Windows.Forms;
// TrayIcon
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace EDExplorer
{
    static class EDExplorer
    {
        [STAThread]
        static void Main()
        {
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

            using (var container = new WindsorContainer())
            {
                container.Register(Component.For<IUserNotificationInterface>().ImplementedBy<TrayIconController>().LifestyleSingleton());
                var trayController = container.Resolve<IUserNotificationInterface>();

                // Application.Run(new EDExplorerFrm());
                Application.Run();
            }
        }
    }
}
