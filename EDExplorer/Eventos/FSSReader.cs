using System.Collections.Generic;

namespace EDExplorer
{
    class FSSReader
    {
        public List<Interes> Interest { get; private set; }
        private Alertas alertas;
        private LogMonitor logMonitor;
        private string detalle;

        public FSSReader(Base b)
        {
            this.logMonitor = b.logMonitor;
            this.alertas = b.alertas;
            Interest = new List<Interes>();    
        }

        FSSDiscoveryScan fssEvent;
        DetallesAlerta da;
        public bool hayAlertas()
        {
            fssEvent = logMonitor.LastFSS;

            da = alertas.n[Alerta.BodyCount];
            if (da.flag)
            {
                alertas.valor = (double)fssEvent.BodyCount;

                if (alertas.CumpleCriterios(da))
                {
                    //detalle = $"{alertas.valor} cuerpos en el sistema. ({fssEvent.NonBodyCount} otros)";
                    //detalle = $"cuerpos en el sistema. ({fssEvent.NonBodyCount} otros)";
                    Interest.Add(new Interes(fssEvent.SystemName, da.nombre, alertas.valorST, alertas.detalle, alertas.isRecord, alertas.recordDesc));
                }
            }
            //////////////////////////
            return Interest.Count > 0;
        }
    }
}
