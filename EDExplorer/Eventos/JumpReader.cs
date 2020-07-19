using System;
using System.Collections.Generic;
using System.Linq;

namespace EDExplorer
{
    class JumpReader
    {
        private readonly Properties.Settings settings;
        public List<Interes> Interest { get; private set; }
        private Alertas alertas;
        private LogMonitor logMonitor;
        private string detalle;
        private double valor;

        public JumpReader(Base b)
        {
            this.logMonitor = b.logMonitor;
            this.alertas = b.alertas;
            Interest = new List<Interes>();
            this.settings = Properties.Settings.Default;
        }

        FsdJump jumpEvent;
        DetallesAlerta da;
        public bool hayAlertas()
        {
            jumpEvent = logMonitor.LastJump;

            // Contabilizar distancia acumulada en saltos
            da = alertas.n[Alerta.DistanciaJump];
            if (da.flag)
            {
                valor = logMonitor.acumuladoJump + jumpEvent.JumpDist;

                if (alertas.CumpleCriterios(da, valor))
                {
                    detalle = $"{valor:N0}al acumulados en saltos.";
                    Interest.Add(new Interes(jumpEvent.StarSystem, da.nombre, detalle, alertas.isRecord));
                    logMonitor.acumuladoJump = 0;
                }
                else
                {
                    logMonitor.acumuladoJump = valor;
                }
            }

            // Distancia al punto de inicio de sesion
            //da = alertas.n[Alerta.DistanciaStart];
            //if (da.flag)
            //{
            //    if (logMonitor.posInicial == null)
            //    {
            //        // Primera posicion leida
            //        logMonitor.posInicial = jumpEvent.StarPos;
            //    }
            //    else
            //    {
            //        valor = Math.Pow(logMonitor.posInicial[0] - jumpEvent.StarPos[0], 2);
            //        valor += Math.Pow(logMonitor.posInicial[1] - jumpEvent.StarPos[1], 2);
            //        valor += Math.Pow(logMonitor.posInicial[2] - jumpEvent.StarPos[2], 2);
            //        valor = Math.Sqrt(valor);

            //        if (alertas.CumpleCriterios(da, valor))
            //        {
            //            detalle = $"{valor:N0}al recorridos en esta sesión.";
            //            Interest.Add(new Interes(jumpEvent.StarSystem, da.nombre, detalle, alertas.isRecord));

            //            // Una vez alcanzada la distancia se comienza de nuevo
            //            logMonitor.posInicial = jumpEvent.StarPos;
            //        }
            //    }
            //}

            //da = alertas.n[Alerta.EjeX];
            //if (da.flag)
            //{
            //    valor = jumpEvent.StarPos[0];

            //    if (alertas.CumpleCriterios(da, valor))
            //    {
            //        detalle = $"{valor:N0}al distancia eje.";
            //        Interest.Add(new Interes(jumpEvent.StarSystem, da.nombre, detalle, alertas.isRecord));

            //        if (alertas.isRecord)
            //            Interest.Add(new Interes(jumpEvent.StarSystem, "Record Personal", alertas.recordDesc));
            //    }
            //}

            //da = alertas.n[Alerta.EjeY];
            //if (da.flag)
            //{
            //    valor = jumpEvent.StarPos[1];

            //    if (alertas.CumpleCriterios(da, valor))
            //    {
            //        detalle = $"{valor:N0}al distancia eje.";
            //        Interest.Add(new Interes(jumpEvent.StarSystem, da.nombre, detalle, alertas.isRecord));

            //        if (alertas.isRecord)
            //            Interest.Add(new Interes(jumpEvent.StarSystem, "Record Personal", alertas.recordDesc));
            //    }
            //}

            //da = alertas.n[Alerta.EjeZ];
            //if (da.flag)
            //{
            //    valor = jumpEvent.StarPos[2];

            //    if (alertas.CumpleCriterios(da, valor))
            //    {
            //        detalle = $"{valor:N0}al distancia eje.";
            //        Interest.Add(new Interes(jumpEvent.StarSystem, da.nombre, detalle, alertas.isRecord));

            //        if (alertas.isRecord)
            //            Interest.Add(new Interes(jumpEvent.StarSystem, "Record Personal", alertas.recordDesc));
            //    }
            //}
            //////////////////////////
            return Interest.Count > 0;
        }
    }
}
