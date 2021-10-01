using System;
using System.Collections.Generic;
using System.Linq;

namespace EDExplorer
{
    using M = Properties.Textos;

    class ScanReader
    {
        private bool isRing;
        public List<Interes> Interest { get; private set; }
        private Alertas alertas;
        private LogMonitor logMonitor;
        private readonly Materials PremiumBoostMaterials =
            Materials.Carbon | Materials.Germanium | Materials.Arsenic |
            Materials.Niobium | Materials.Yttrium | Materials.Polonium;
        private readonly Materials GoldSystemMaterials =
            Materials.Antimony | Materials.Arsenic | Materials.Cadmium | Materials.Carbon |
            Materials.Chromium | Materials.Germanium | Materials.Iron | Materials.Manganese |
            Materials.Mercury | Materials.Molybdenum | Materials.Nickel | Materials.Niobium |
            Materials.Phosphorus | Materials.Polonium | Materials.Ruthenium | Materials.Selenium |
            Materials.Sulphur | Materials.Technetium | Materials.Tellurium | Materials.Tin |
            Materials.Tungsten | Materials.Vanadium | Materials.Yttrium | Materials.Zinc |
            Materials.Zirconium;

        public ScanReader(Base b)
        {
            this.logMonitor = b.logMonitor;
            this.alertas = b.alertas; // new Alertas();

            Interest = new List<Interes>();
        }

        ScanEvent scanEvent;
        DetallesAlerta da;
        public bool hayAlertas()
        {
            isRing = logMonitor.LastScan.BodyName.Contains(" Ring");
            if (isRing) return false;

            scanEvent = logMonitor.LastScan;
            bool flgAterrizable = scanEvent.Landable.GetValueOrDefault(false);
            
            //if (scanEvent.BodyName == "Cl Pismis 13 5")
            //{
            //}

            //da = alertas.n[Alerta.Terraformable];
            //if (da.flag && flgAterrizable && scanEvent.TerraformState.Length > 0)
            //{
            //    if (alertas.CumpleCriterios(da, 0))
            //    {
            //        Interest.Add((scanEvent.BodyName, da.nombre, string.Empty));
            //    }
            //}

            if (flgAterrizable)
            {
                // Aterrizable con Atmosfera. 
                da = alertas.n[Alerta.Atmosfera];
                if (da.flag && scanEvent.Atmosphere.Length > 0)
                {
                    if (alertas.CumpleCriterios(da))
                    {
                        Interest.Add(new Interes(scanEvent.BodyName, da.nombre, "", ""));
                    }
                }

                // Aterrizable con Anillo (Km)
                da = alertas.n[Alerta.Anillo];
                if (da.flag && scanEvent.Rings != null)
                {
                    alertas.valor = (double)scanEvent.Rings[scanEvent.Rings.Count() - 1].OuterRad;
                    alertas.valor -= (double)scanEvent.Rings[0].InnerRad; // metros
                    alertas.valor /= 1000;

                    if (alertas.CumpleCriterios(da))
                    {
                        //detalle = "Km anchura anillo/s";
                        Interest.Add(new Interes(scanEvent.BodyName, da.nombre, alertas.valorST, alertas.detalle, alertas.isRecord, alertas.recordDesc));
                    }
                }

                // Aterrizable Bajo-g
                da = alertas.n[Alerta.GravedadP];
                if (da.flag)
                {
                    alertas.valor = (double)scanEvent.SurfaceGravity / 9.81;

                    if (alertas.CumpleCriterios(da))
                    {
                        //detalle = "g Gravedad en superficie";
                        Interest.Add(new Interes(scanEvent.BodyName, da.nombre, alertas.valorST, alertas.detalle, alertas.isRecord, alertas.recordDesc));
                    }
                }

                // Aterrizable Alto-g
                da = alertas.n[Alerta.GravedadG];
                if (da.flag)
                {
                    alertas.valor = (double)scanEvent.SurfaceGravity / 9.81;

                    if (alertas.CumpleCriterios(da))
                    {
                        //detalle = "g Gravedad en superficie";
                        Interest.Add(new Interes(scanEvent.BodyName, da.nombre, alertas.valorST, alertas.detalle, alertas.isRecord, alertas.recordDesc));
                    }
                }

                // Aterrizable Pequeño (Km)
                da = alertas.n[Alerta.CuerpoP];
                if (da.flag)
                {
                    alertas.valor = (double)scanEvent.Radius / 1000;

                    if (alertas.CumpleCriterios(da))
                    {
                        //detalle = "Km de Radio";
                        Interest.Add(new Interes(scanEvent.BodyName, da.nombre, alertas.valorST, alertas.detalle, alertas.isRecord, alertas.recordDesc));
                    }
                }

                // Aterrizable Grande (Km)
                da = alertas.n[Alerta.CuerpoG];
                if (da.flag)
                {
                    alertas.valor = (double)scanEvent.Radius / 1000;

                    if (alertas.CumpleCriterios(da))
                    {
                        //detalle = "Km de Radio.";
                        Interest.Add(new Interes(scanEvent.BodyName, da.nombre, alertas.valorST, alertas.detalle, alertas.isRecord, alertas.recordDesc));
                    }
                }

                // Aterrizable CREMATORIA - Skardee I
                da = alertas.n[Alerta.Crematoria];
                if (da.flag && scanEvent.DistanceFromArrivalLs > 0 && scanEvent.DistanceFromArrivalLs < 10 && scanEvent.Parent?[0].ParentType == "Star")
                {
                    alertas.valor = (double)scanEvent.OrbitalPeriod / 86400; // segundos a días

                    if (alertas.CumpleCriterios(da))
                    {
                        //double g = Math.Abs((double)scanEvent.SurfaceTemperature);
                        //detalle = $"dias rotación  {scanEvent.DistanceFromArrivalLs.ToString("0")} LS  {g.ToString("0")} grados";
                        Interest.Add(new Interes(scanEvent.BodyName, da.nombre, alertas.valorST, alertas.detalle));
                    }
                }
            }
            else
            {
                bool isWorld = false;
                if (scanEvent.PlanetClass == "Earthlike body")
                {
                    // Tipo Tierra
                    da = alertas.n[Alerta.Tierra];
                    isWorld = da.flag;
                }
                else if (scanEvent.PlanetClass == "Water world")
                {
                    // Tipo Acuatico
                    da = alertas.n[Alerta.Acuatico];
                    isWorld = da.flag;
                }
                else if (scanEvent.PlanetClass == "Ammonia world")
                {
                    // Tipo Amoniaco
                    da = alertas.n[Alerta.Amoniaco];
                    isWorld = da.flag;
                }

                if (isWorld)
                {
                    // No entra en la funcion normal de alertas
                    //alertas.detalle = da.detalle;
                    alertas.detalle = (scanEvent.WasDiscovered ? M.str_Descubierto : M.str_Nuevo);
                    alertas.detalle += " " + (scanEvent.WasMapped ? M.str_Mapeado : M.str_Virgen);
                    Interest.Add(new Interes(scanEvent.BodyName, da.nombre, "", alertas.detalle));
                }
            }

            // Nombre especial
            da = alertas.n[Alerta.NombreEspecial];
            if (da.flag && !scanEvent.BodyName.Contains(logMonitor.CurrentSystem))
            {
                // No entra en la funcion normal de alertas
                alertas.detalle = da.detalle;
                Interest.Add(new Interes(scanEvent.BodyName, da.nombre, "", alertas.detalle));
            }

            alertaAnillo(Alerta.AnilloIcy);
            alertaAnillo(Alerta.AnilloRock);
            alertaAnillo(Alerta.AnilloMetal);
            alertaAnillo(Alerta.AnilloMetalRich);

            // Ancho del Anillo x veces el Radio (unidades distintas)
            da = alertas.n[Alerta.AnilloG];
            if (da.flag && scanEvent.Rings != null)
            {
                // Si el primero no es un Belt ya no hay luego
                if (!scanEvent.Rings[0].Name.Contains("Belt"))
                {
                    alertas.valor = ((double)scanEvent.Rings[scanEvent.Rings.Count() - 1].OuterRad - (double)scanEvent.Rings[0].InnerRad) / (double)scanEvent.Radius;

                    if (alertas.CumpleCriterios(da))
                    {
                        // Anillo de {valor / 1000:N0}km, 
                        //detalle = "veces el radio del Planeta.";
                        Interest.Add(new Interes(scanEvent.Rings[0].Name, da.nombre, alertas.valorST, alertas.detalle, alertas.isRecord, alertas.recordDesc));
                    }
                }
            }

            // Comprobaciones relativas al Padre (puede no haberse escaneado todavía)
            if ((alertas.n[Alerta.OrbitaP].flag || alertas.n[Alerta.OrbitaG].flag || alertas.n[Alerta.AnilloP].flag) &&
                (scanEvent.Parent?[0].ParentType == "Planet" || scanEvent.Parent?[0].ParentType == "Star"))
                if (logMonitor.SystemBody.ContainsKey((logMonitor.CurrentSystem, scanEvent.Parent[0].Body)))
                {
                    ScanEvent parent = logMonitor.SystemBody[(logMonitor.CurrentSystem, scanEvent.Parent[0].Body)];
                    alertaPadreHijo(parent, scanEvent);
                }
                else
                {
                    // ** POSIBLE ERROR si el padre no se ha escaneado antes no hay info.
                    if (!logMonitor.EsperandoPadre.ContainsKey((logMonitor.CurrentSystem, scanEvent.Parent[0].Body)))
                        logMonitor.EsperandoPadre[(logMonitor.CurrentSystem, scanEvent.Parent[0].Body)] = new List<long>() { scanEvent.BodyId };
                    else
                        logMonitor.EsperandoPadre[(logMonitor.CurrentSystem, scanEvent.Parent[0].Body)].Insert(0, scanEvent.BodyId); 
                }

            // ** Realizar las llamadas pendientes de los hijos.
            if (logMonitor.EsperandoPadre.ContainsKey((logMonitor.CurrentSystem, scanEvent.BodyId)))
            {
                ScanEvent hijo;
                List<long> Hijos = logMonitor.EsperandoPadre[(logMonitor.CurrentSystem, scanEvent.BodyId)];

                for (int i = 0; i < Hijos.Count; i++)
                {
                    hijo = logMonitor.SystemBody[(logMonitor.CurrentSystem, Hijos[i])];
                    alertaPadreHijo(scanEvent, hijo);
                }
            }

            // Binaria Cercana (dos cuerpos orbitando entre si)
            da = alertas.n[Alerta.Binario];
            if (da.flag && scanEvent.Parent?[0].ParentType == "Null") // && scanEvent.Radius / scanEvent.SemiMajorAxis > 0.4)
            {
                alertas.valor = (double)(scanEvent.Radius / scanEvent.SemiMajorAxis);

                if (alertas.CumpleCriterios(da))
                {
                    //detalle = "relación Radio vs Distancia.";
                    Interest.Add(new Interes(scanEvent.BodyName, da.nombre, alertas.valorST, alertas.detalle, alertas.isRecord, alertas.recordDesc));
                }
            }

            // Rotacion Rápida
            da = alertas.n[Alerta.RotacionR];
            if (alertas.n[Alerta.RotacionR].flag && scanEvent.RotationPeriod != null && !scanEvent.TidalLock.GetValueOrDefault(true))
            {
                alertas.valor = Math.Abs((double)scanEvent.RotationPeriod / 3600);
                
                if (alertas.CumpleCriterios(da))
                {
                    //detalle = "horas para completar una rotación.";
                    Interest.Add(new Interes(scanEvent.BodyName, da.nombre, alertas.valorST, alertas.detalle, alertas.isRecord, alertas.recordDesc));
                }
            }

            // Orbita Rápida
            da = alertas.n[Alerta.OrbitaR];
            if (da.flag && scanEvent.OrbitalPeriod != null && !isRing)
            {
                alertas.valor = Math.Abs((double)scanEvent.OrbitalPeriod / 3600);

                if (alertas.CumpleCriterios(da))
                {
                    //detalle = "horas para completar una Orbita.";
                    Interest.Add(new Interes(scanEvent.BodyName, da.nombre, alertas.valorST, alertas.detalle, alertas.isRecord, alertas.recordDesc));
                }
            }

            // High eccentricity
            //if (alertas.n[Alerta.Excentricidad].flag && scanEvent.Eccentricity > alertas.n[Alerta.Excentricidad].desde)
            //{
            //    detalle = $"Excentricidad de {Math.Round((decimal)scanEvent.Eccentricity, 2)}";
            //    Interest.Add((scanEvent.BodyName, alertas.n[Alerta.Excentricidad].nombre, detalle));
            //}

            return Interest.Count > 0;
        }

        private void alertaAnillo(Alerta a)
        {
            // Contiene un Anillo del tipo indicado?
            da = alertas.n[a];
            if (da.flag && scanEvent.Rings != null)
            {
                string clase = "";

                switch (a)
                {
                    case Alerta.AnilloIcy: { clase = "eRingClass_Icy"; break; }
                    case Alerta.AnilloMetal: { clase = "eRingClass_Metalic"; break; }
                    case Alerta.AnilloMetalRich: { clase = "eRingClass_MetalRich"; break; }
                    case Alerta.AnilloRock: { clase = "eRingClass_Rocky"; break; }
                }

                if (scanEvent.Rings.Where(ring => ring.RingClass == clase && !ring.Name.Contains(" Belt")).Count() > 0)
                {
                    Ring r = scanEvent.Rings.Where(ring => ring.RingClass == clase && !ring.Name.Contains(" Belt")).OrderByDescending(ring => ring.MassMT).First();

                    alertas.valor = (double)r.MassMT / Math.Pow(10, 12);

                    if (alertas.CumpleCriterios(da))
                    {
                        //detalle = "Mt. (x10^12) de masa.";
                        Interest.Add(new Interes(r.Name, da.nombre, alertas.valorST, alertas.detalle, alertas.isRecord, alertas.recordDesc));
                    }
                }
            }
        }

        private void alertaPadreHijo(ScanEvent Padre, ScanEvent Hijo)
        {
            // Distancia entre superficies
            alertas.valor = Math.Truncate(((double)Hijo.SemiMajorAxis - (double)Padre.Radius - (double)Hijo.Radius) / 1000);

            //Orbita Pequeña
            da = alertas.n[Alerta.OrbitaP];
            if (da.flag && alertas.CumpleCriterios(da))
            {
                //detalle = "Km de distancia entre superficies";
                Interest.Add(new Interes(Hijo.BodyName, da.nombre, alertas.valorST, alertas.detalle, alertas.isRecord, alertas.recordDesc));
            }

            // distancia en SL (1 SL = 299.792,36 Km)
            alertas.valor /= 299792.36;

            //Orbita Grande
            da = alertas.n[Alerta.OrbitaG];
            if (da.flag && alertas.CumpleCriterios(da))
            {
                //detalle = "SL de distancia entre superficies.";
                Interest.Add(new Interes(Hijo.BodyName, da.nombre, alertas.valorST, alertas.detalle, alertas.isRecord, alertas.recordDesc));
            }

            // Proximo al Anillo
            da = alertas.n[Alerta.AnilloP];
            if (da.flag && Padre.Rings != null)
            {
                alertas.valor = Math.Min(Math.Abs(Hijo.SemiMajorAxis.GetValueOrDefault(0) - (double)Hijo.Radius - (double)Padre.Rings[Padre.Rings.Count() - 1].OuterRad) / 1000,
                                         Math.Abs((double)Padre.Rings[0].InnerRad - (double)Hijo.Radius - Hijo.SemiMajorAxis.GetValueOrDefault(0)) / 1000);

                if (alertas.CumpleCriterios(da))
                {
                    //detalle = "Km de distancia al borde del anillo.";
                    Interest.Add(new Interes(Hijo.BodyName, da.nombre, alertas.valorST, alertas.detalle, alertas.isRecord, alertas.recordDesc));
                }
            }
        }
    }
}
