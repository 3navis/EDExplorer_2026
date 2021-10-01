// TrayIcon
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

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

            //CultureInfo.CurrentCulture = new CultureInfo("es-ES", false);
            if (System.Globalization.CultureInfo.CurrentUICulture.Name != Properties.Settings.Default.Idioma)
            {
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Properties.Settings.Default.Idioma);
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
