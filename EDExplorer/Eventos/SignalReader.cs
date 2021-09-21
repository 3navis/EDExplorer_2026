using System.Collections.Generic;
using System.Linq;

namespace EDExplorer
{
    class SignalReader
    {
        public List<Interes> Interest { get; private set; }
        private Alertas alertas;
        private LogMonitor logMonitor;
        private string detalle;
        private SaaSignalsFound.Signal signal;
        
        SaaSignalsFound signalEvent;
        DetallesAlerta da;
        public SignalReader(Base b)
        {
            this.logMonitor = b.logMonitor;
            Interest = new List<Interes>();
            this.alertas = b.alertas; // new Alertas();
        }

        private void alertaVeta(Alerta a)
        {
            da = alertas.n[a];
            if (da.flag)
            {
                foreach (var signal in signalEvent.Signals.Where(signal => signal.Type == da.TipoLog))
                {
                    alertas.valor = signal.Count;
                    if (alertas.CumpleCriterios(da))
                    {
                        //detalle = $"{alertas.valor} vetas de {signal.TypeLocalised}";
                        detalle = $"vetas de {signal.TypeLocalised}";
                        Interest.Add(new Interes(signalEvent.BodyName, da.nombre, alertas.valorST, detalle, alertas.isRecord));

                        if (alertas.isRecord)
                            Interest.Add(new Interes(signalEvent.BodyName, "Record Personal", alertas.valorST, alertas.recordDesc));
                    }
                }
            }
        }
        private void alertaSignal(Alerta a)
        {
            da = alertas.n[a];
            if (da.flag)
            {
                signal = signalEvent.Signals.Where(signal => signal.Type == da.TipoLog).FirstOrDefault();

                if (signal != null) 
                {
                    alertas.valor = signal.Count;

                    if (alertas.CumpleCriterios(da))
                    {
                        //detalle = $"{alertas.valor} señales {signal.TypeLocalised}";
                        detalle = $"señales {signal.TypeLocalised}";
                        Interest.Add(new Interes(signalEvent.BodyName, da.nombre, alertas.valorST, detalle, alertas.isRecord));

                        if (alertas.isRecord)
                            Interest.Add(new Interes(signalEvent.BodyName, "Record Personal", alertas.valorST, alertas.recordDesc));
                    }
                }
            }
        }

        public bool hayAlertas()
        {
            signalEvent = logMonitor.LastSignal; 

            alertaVeta(Alerta.Tritio);
            alertaVeta(Alerta.LTD);
            alertaVeta(Alerta.Opal);
            alertaVeta(Alerta.Benitoita);
            alertaVeta(Alerta.Serendibita);
            alertaVeta(Alerta.Musgravita);
            alertaVeta(Alerta.Painita);
            alertaVeta(Alerta.Alejandrita);
            alertaVeta(Alerta.Grandidierita);
            alertaVeta(Alerta.Monacita);
            alertaVeta(Alerta.Rhodplumsita);

            alertaSignal(Alerta.Geological);
            alertaSignal(Alerta.Biological);
            alertaSignal(Alerta.Human);
            alertaSignal(Alerta.Guardian);
            alertaSignal(Alerta.Thargoid);

            //////////////////////////
            return Interest.Count > 0;
        }
    }
}
