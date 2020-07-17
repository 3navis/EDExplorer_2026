using System;
using System.Collections.Generic;
using System.Linq;

namespace EDExplorer
{
    class FSSReader
    {
        private readonly Properties.Settings settings;
        public List<Interes> Interest { get; private set; }
        private Alertas alertas;
        private LogMonitor logMonitor;
        private string detalle;

        public FSSReader(Base b)
        {
            this.logMonitor = b.logMonitor;
            this.alertas = b.alertas;
            Interest = new List<Interes>();
            this.settings = Properties.Settings.Default;
        }

        FSSDiscoveryScan fssEvent;
        DetallesAlerta da;
        public bool hayAlertas()
        {
            fssEvent = logMonitor.LastFSS;

            da = alertas.n[Alerta.BodyCount];
            if (da.flag)
            {
                double nCuerpos = (double)fssEvent.BodyCount;

                if (alertas.CumpleCriterios(da, nCuerpos))
                {
                    detalle = $"{nCuerpos} cuerpos en el sistema. ({fssEvent.NonBodyCount} otros)";
                    Interest.Add(new Interes(fssEvent.SystemName, da.nombre, detalle, alertas.isRecord));

                    if (alertas.isRecord)
                        Interest.Add(new Interes(fssEvent.SystemName, "Record Personal", alertas.recordDesc));
                }
            }
            //////////////////////////
            return Interest.Count > 0;
        }
    }
}
