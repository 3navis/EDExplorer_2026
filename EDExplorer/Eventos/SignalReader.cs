using System;
using System.Collections.Generic;
using System.Linq;

namespace EDExplorer
{
    class SignalReader
    {

        private readonly bool isRing;
        public List<(string BodyName, string Description, string Detail)> Interest { get; private set; }
        private readonly Properties.Settings settings;
        private Alertas alertas;
        private LogMonitor logMonitor;
        private readonly Minerales BestMinerales =
            Minerales.Tritio | Minerales.LowTemperatureDiamond;
        private string detalle;
        private SaaSignalsFound.Signal signal;
        
        SaaSignalsFound signalEvent;
        DetallesAlerta da;
        public SignalReader(Base b)
        {
            this.logMonitor = b.logMonitor;
            this.settings = Properties.Settings.Default;
            Interest = new List<(string BodyName, string Description, string Detail)>();
            isRing = logMonitor.LastSignal.BodyName.Contains(" Ring");
            this.alertas = b.alertas; // new Alertas();
        }

        private void alertaVeta(Alerta a)
        {
            da = alertas.n[a];
            if (da.flag)
            {
                foreach (var signal in signalEvent.Signals.Where(signal => signal.Type == da.TipoLog))
                {
                    if (alertas.CumpleCriterios(da, signal.Count))
                    {
                        detalle = $"{signal.Count} vetas de {signal.TypeLocalised}";
                        Interest.Add((signalEvent.BodyName, da.nombre, detalle));

                        if (alertas.newRecordMenor || alertas.newRecordMayor)
                            Interest.Add((signalEvent.BodyName, "Record Personal", alertas.recordDesc));
                    }
                }
            }

        }
        public bool hayAlertas()
        {
            signalEvent = logMonitor.LastSignal; 
            //Minerales mineFound = Minerales.None;

            alertaVeta(Alerta.Tritio);
            alertaVeta(Alerta.LTD);
            alertaVeta(Alerta.Opal);
            alertaVeta(Alerta.Benitoita);
            alertaVeta(Alerta.Serendibita);
            alertaVeta(Alerta.Musgravita);
            alertaVeta(Alerta.Painita);

            da = alertas.n[Alerta.Geological];
            if (da.flag)
            {
                signal = signalEvent.Signals.Where(signal => signal.Type == "$SAA_SignalType_Geological;").FirstOrDefault();

                if (signal != null && alertas.CumpleCriterios(da, signal.Count))
                {
                        detalle = $"{signal.Count} señales {signal.TypeLocalised}";
                    Interest.Add((signalEvent.BodyName, da.nombre, detalle));

                    if (alertas.newRecordMenor || alertas.newRecordMayor)
                        Interest.Add((signalEvent.BodyName, "Record Personal", alertas.recordDesc));
                }
            }

            if (signalEvent.BodyName == "Antares B 5")
                    {
                    }

            da = alertas.n[Alerta.Biological];
            if (da.flag)
            {
                signal = signalEvent.Signals.Where(signal => signal.Type == "$SAA_SignalType_Biological;").FirstOrDefault();

                if (signal != null && alertas.CumpleCriterios(da, signal.Count))
                {
                    detalle = $"{signal.Count} señales {signal.TypeLocalised}";
                    Interest.Add((signalEvent.BodyName, da.nombre, detalle));

                    if (alertas.newRecordMenor || alertas.newRecordMayor)
                        Interest.Add((signalEvent.BodyName, "Record Personal", alertas.recordDesc));
                }
            }

            //////////////////////////
            return Interest.Count > 0;
        }
    }
}
