using System;
using System.Collections.Generic;

namespace EDExplorer
{
    class JumpReader
    {
        public List<Interes> Interest { get; private set; }
        private Alertas alertas;
        private LogMonitor logMonitor;

        public JumpReader(Base b)
        {
            this.logMonitor = b.logMonitor;
            this.alertas = b.alertas;
            Interest = new List<Interes>();
        }

        FsdJump jumpEvent;
        DetallesAlerta da;
        public bool hayAlertas()
        {
            jumpEvent = logMonitor.LastJump;

            //SQLBase.JumpSystem(jumpEvent.SystemAddress??(ulong)0, jumpEvent.StarSystem, jumpEvent.Timestamp, jumpEvent.StarPos[0], jumpEvent.StarPos[1], jumpEvent.StarPos[2]);

            // Calculo de datos para Resumen
            logMonitor.sesion_numeroJump++;
            logMonitor.numeroJump++;
            logMonitor.acumuladoJump += jumpEvent.JumpDist;
            logMonitor.sesion_acumuladoJump += jumpEvent.JumpDist;

            // Contabilizar distancia acumulada en saltos
            da = alertas.n[Alerta.AcumuladoJump];
            if (da.flag)
            {
                alertas.valor = logMonitor.acumuladoJump;

                if (alertas.CumpleCriterios(da))
                {
                    Interest.Add(new Interes(jumpEvent.StarSystem, da.nombre, alertas.valorST, alertas.detalle, false));
                    logMonitor.acumuladoJump = 0;
                }
            }

            // Contabilizar distancia acumulada en saltos
            da = alertas.n[Alerta.NumeroJump];
            if (da.flag)
            {
                alertas.valor = logMonitor.numeroJump;

                if (alertas.CumpleCriterios(da))
                {
                    Interest.Add(new Interes(jumpEvent.StarSystem, da.nombre, alertas.valorST, alertas.detalle, false));
                    logMonitor.numeroJump = 0;
                }
            }

            // Poblacion
            da = alertas.n[Alerta.Poblacion];
            if (da.flag)
            {
                alertas.valor = jumpEvent.Population / Math.Pow(10, 6);

                if (alertas.CumpleCriterios(da))
                    Interest.Add(new Interes(jumpEvent.StarSystem, da.nombre, alertas.valorST, alertas.detalle, false));
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
