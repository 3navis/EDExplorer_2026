using System;
using System.Collections.Generic;
using System.Linq;

namespace EDExplorer
{
    class FSSReader
    {

        private readonly Properties.Settings settings;
        public List<(string BodyName, string Description, string Detail)> Interest { get; private set; }
        private Alertas alertas;
        //private Records records;
        private LogMonitor logMonitor;
        private string detalle;

        public FSSReader(Base b)
        {
            this.logMonitor = b.logMonitor;
            this.alertas = b.alertas;
            //this.records = b.records;
            Interest = new List<(string BodyName, string Description, string Detail)>();
            this.settings = Properties.Settings.Default;
        }

        public bool hayAlertas()
        {
            FSSDiscoveryScan fssEvent = logMonitor.LastFSS;

            if (alertas.n[Alerta.BodyCount].flag)
            {
                double nCuerpos = (double)fssEvent.BodyCount;

                if (alertas.CumpleCriterios(alertas.n[Alerta.BodyCount], nCuerpos))
                {
                    detalle = $"{nCuerpos} cuerpos en el sistema. {fssEvent.NonBodyCount}";
                    Interest.Add((fssEvent.SystemName, alertas.n[Alerta.BodyCount].nombre, detalle));

                    if (alertas.newRecordMenor || alertas.newRecordMayor)
                    {
                        if (alertas.newRecordMenor) detalle = "alcanzado nuevo limite inferior";
                        else detalle = "alcanzado nuevo limite superior";

                        Interest.Add((fssEvent.SystemName, "Record Personal", detalle));
                    }
                }
            }
            //////////////////////////
            return Interest.Count > 0;
        }
    }
}
