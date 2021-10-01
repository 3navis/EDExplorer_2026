namespace EDExplorer
{
    using global::EDExplorer.Pantallas;
    using Properties;
    using System;
    using System.Drawing;
    using System.Reflection;
    using System.Windows.Forms;

    public interface IUserNotificationInterface
    {
        void ShowErrorNotification(string error);
    }
    public class TrayIconController : IUserNotificationInterface, IDisposable
    {
        private Base basi;

        private readonly NotifyIcon trayIcon;
        private bool disposedValue = false; // To detect redundant calls
        private Properties.Settings settings = Properties.Settings.Default;

        public TrayIconController()
        {
            basi = new Base();
            trayIcon = CreateTrayIcon();
        }

        private NotifyIcon CreateTrayIcon()
        {
//            var components = new Container();
//            var notifyIcon = new NotifyIcon(components)
            var notifyIcon = new NotifyIcon()
            {
                ContextMenuStrip = CreateMenuStrip(),
                Icon = Resources.EDExplorer,
                Text = "EDExplorer Agent",
                Visible = true,
            };

            //notifyIcon.BalloonTipClicked += (o, e) => basi.OpenConfiguracionFrm(basi.alertas);
            //notifyIcon.Click += (o, e) => Prueba();
            notifyIcon.DoubleClick += (o, e) => basi.OpenConfiguracionFrm(basi.alertas);

            return notifyIcon;
        }

        private static ToolStripSeparator ToolStripSeparatorLeft => new ToolStripSeparator { Alignment = ToolStripItemAlignment.Left };

        private ContextMenuStrip CreateMenuStrip()
        {
            var menuStrip = new ContextMenuStrip(); 
            //menuStrip.Items.Add(new ToolStripLabel($"Version: {Application.ProductVersion}") { ForeColor = SystemColors.ControlDark });
            menuStrip.Items.Add(new ToolStripLabel($"Version: {Assembly.GetExecutingAssembly().GetName().Version}") { ForeColor = SystemColors.ControlDark });
            menuStrip.Items.Add("Acerca de ...", null, (o, e) => About());

            menuStrip.Items.Add(ToolStripSeparatorLeft);
            menuStrip.Items.Add("Configuración", Resources.CONFIG, (o, e) => basi.OpenConfiguracionFrm(basi.alertas));

            menuStrip.Items.Add(ToolStripSeparatorLeft);
            menuStrip.Items.Add(textoLISTA(), recursoLISTA(), (o, e) => Mostrar_Lista(o));
            menuStrip.Items.Add(textoNOTIFY(), recursoNOTIFY(), (o, e) => Mostrar_Notificaciones(o));
            menuStrip.Items.Add(textoTALK(), recursoTALK(), (o, e) => Alertas_Audibles(o));

            menuStrip.Items.Add(ToolStripSeparatorLeft);
            menuStrip.Items.Add(textoSTART(), recursoSTART(), (o, e) => Start_Stop(o));

            menuStrip.Items.Add(ToolStripSeparatorLeft);
            menuStrip.Items.Add("Salir", SystemIcons.Error.ToBitmap(), (o, e) => Application.Exit());

            return menuStrip;
        }

        private ToolStripMenuItem menuItem;

        private string textoSTART(bool flag = true)
        {
            if (flag) // al crear el menu coge el valor por defecto
                flag = basi.autoStart;
            else
                flag = basi.logMonitor.IsMonitoring();

            if (flag)
                return "PARAR monitor eventos";
            else
                return "INICIAR monitor eventos";
        }
        private Image recursoSTART(bool flag = true)
        {
            if (flag) // al crear el menu coge el valor por defecto
                flag = basi.autoStart;
            else
                flag = basi.logMonitor.IsMonitoring();
            
            if (flag)
                return Resources.STOP;
            else
                return Resources.START;
        }
        private void Start_Stop(object obj)
        {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)obj;

            if (basi.logMonitor.IsMonitoring())
                basi.logMonitor.MonitorStop();
            else
                basi.logMonitor.MonitorStart();

            menuItem.Text = textoSTART(false);
            menuItem.Image = recursoSTART(false);
        }

        private string textoLISTA()
        {
            if (settings.activarLista)
                return "Ocultar Lista";
            else
                return "Mostrar Lista";
        }
        private Image recursoLISTA()
        {
            if (settings.activarLista)
                return Resources.LISTA_ON;
            else
                return Resources.LISTA_OFF;
        }
        private void Mostrar_Lista(object obj)
        {
            menuItem = (ToolStripMenuItem)obj;

            settings.activarLista = !settings.activarLista;
            settings.Save();

            if (settings.activarLista)
            {
                basi.OpenEDExplorerForm();
            }
            else
            {
                basi.CloseEDExplorerForm();
            }

            menuItem.Image = recursoLISTA();
            menuItem.Text = textoLISTA();
        }
        private Image recursoNOTIFY()
        {
            if (settings.activarNotificaciones)
                return Resources.NOTIFY_ON;
            else
                return Resources.NOTIFY_OFF;
        }
        private string textoNOTIFY()
        {
            if (settings.activarNotificaciones)
                return "Parar Notificaciones";
            else
                return "Activar Notificaciones";
        }
        private void Mostrar_Notificaciones(object obj)
        {
            menuItem = (ToolStripMenuItem)obj;

            settings.activarNotificaciones = !settings.activarNotificaciones;
            settings.Save();

            if (settings.activarNotificaciones)
            {
                basi.OpenNotifyForm("Notificaciones Activadas", 3000);
            }
           
            menuItem.Image = recursoNOTIFY();
            menuItem.Text = textoNOTIFY();
        }
        private Image recursoTALK()
        {
            if (settings.activarAudio)
                return Resources.TALK_ON;
            else
                return Resources.TALK_OFF;
        }
        private string textoTALK()
        {
            if (settings.activarAudio)
                return "Parar Locutor";
            else
                return "Activar Locutor";
        }
        private void Alertas_Audibles(object obj)
        {
            menuItem = (ToolStripMenuItem)obj;

            settings.activarAudio = !settings.activarAudio;
            settings.Save();

            if (settings.activarAudio)
            {
                basi.ActivarAudio();
                basi.TestSound();
            }

            menuItem.Image = recursoTALK();
            menuItem.Text = textoTALK();
        }

        public void ShowErrorNotification(string error) => trayIcon.ShowBalloonTip(30000, "EDExplorer: Error", error, ToolTipIcon.Error);

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    trayIcon.Visible = false;
                    trayIcon?.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.
                disposedValue = true;

                settings.Save();
                if (basi.speech != null) basi.speech.Dispose();
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~TrayController() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);

            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        private void About()
        {
            AboutFrm aboutFrm;
            aboutFrm = new AboutFrm();
            aboutFrm.Show();
        }

    }
}
