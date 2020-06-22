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

        public SignalReader(Base b)
        {
            this.logMonitor = b.logMonitor;
            this.settings = Properties.Settings.Default;
            Interest = new List<(string BodyName, string Description, string Detail)>();
            isRing = logMonitor.LastScan.BodyName.Contains(" Ring");
            this.alertas = b.alertas; // new Alertas();
        }

        public bool EsInteresante()
        {
            SaaSignalsFound signalEvent = logMonitor.LastSignal;

            Minerales mineFound = Minerales.None;

            if (alertas.n[Alerta.Tritio].flag)
            {
                foreach (var signal in signalEvent.Signals.Where(signal => signal.Type == "Tritium"))
                {
                    if (signal.Count >= alertas.n[Alerta.Tritio].desde)
                    {
                        detalle = $"{signal.Count} vetas de {signal.TypeLocalised}";
                        Interest.Add((signalEvent.BodyName, alertas.n[Alerta.Tritio].nombre, detalle));
                    }
                }
            }

            if (alertas.n[Alerta.LTD].flag)
            {
                foreach (var signal in signalEvent.Signals.Where(signal => signal.Type == "LowTemperatureDiamond"))
                {
                    if (signal.Count >= alertas.n[Alerta.LTD].desde)
                    {
                        detalle = $"{signal.Count} vetas de {signal.TypeLocalised}";
                        Interest.Add((signalEvent.BodyName, alertas.n[Alerta.LTD].nombre, detalle));
                    }
                }
            }

            if (alertas.n[Alerta.Opal].flag)
            {
                foreach (var signal in signalEvent.Signals.Where(signal => signal.Type == "Opal"))
                {
                    if (signal.Count >= alertas.n[Alerta.Opal].desde)
                    {
                        detalle = $"{signal.Count} vetas de {signal.TypeLocalised}";
                        Interest.Add((signalEvent.BodyName, alertas.n[Alerta.Opal].nombre, detalle));
                    }
                }
            }

            if (alertas.n[Alerta.Geological].flag)
            {
                foreach (var signal in signalEvent.Signals.Where(signal => signal.Type == "$SAA_SignalType_Geological;"))
                {
                    if (signal.Count >= alertas.n[Alerta.Geological].desde)
                    {
                        detalle = $"{signal.Count} señales {signal.TypeLocalised}";
                        Interest.Add((signalEvent.BodyName, alertas.n[Alerta.Geological].nombre, detalle));
                    }
                }
            }

            if (signalEvent.BodyName == "Antares B 5")
                    {
                    }

            if (alertas.n[Alerta.Biological].flag)
            {
                signal = signalEvent.Signals.Where(signal => signal.Type == "$SAA_SignalType_Biological;").FirstOrDefault();

                if (signal != null && signal.Count >= alertas.n[Alerta.Biological].desde)
                {
                    detalle = $"{signal.Count} señales {signal.TypeLocalised}";
                    Interest.Add((signalEvent.BodyName, alertas.n[Alerta.Biological].nombre, detalle));
                }
            }

            //////////////////////////
            return Interest.Count > 0;
        }
    }
}
