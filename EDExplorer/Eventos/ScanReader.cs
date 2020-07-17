using System;
using System.Collections.Generic;
using System.Linq;

namespace EDExplorer
{
    class ScanReader
    {
        private readonly bool isRing;
        public List<Interes> Interest { get; private set; }
        private readonly Properties.Settings settings;
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
        private string detalle;

        public ScanReader(Base b)
        {
            this.settings = Properties.Settings.Default;
            this.logMonitor = b.logMonitor;
            this.alertas = b.alertas; // new Alertas();
            
            Interest = new List<Interes>();
            isRing = logMonitor.LastScan.BodyName.Contains(" Ring");
        }

        public bool hayAlertas()
        {
            bool interesting = !isRing && DefaultInterest();
            
            if (settings.VeryInteresting && Interest.Count() > 1)
            {
                detalle = $"{Interest.Count()} Criterios Satisfechos";
                Interest.Add(new Interes(logMonitor.LastScan.BodyName, "Criterios Múltiples", detalle));
            }

            if (Interest.Count() == 0)
            {
                Interest.Add(new Interes(logMonitor.LastScan.BodyName, "Sin Interés", string.Empty));
            }
            return interesting;
        }

        ScanEvent scanEvent;
        DetallesAlerta da;
        double valor;
        private bool DefaultInterest()
        {
            scanEvent = logMonitor.LastScan;
            bool flgAterrizable = scanEvent.Landable.GetValueOrDefault(false);

            //if (scanEvent.BodyName == "Byeia Eurk CQ-X b56-1 13")
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
                // Aterrizable con Atmosfera. Ya me gustaria!
                da = alertas.n[Alerta.Atmosfera];
                if (da.flag && scanEvent.Atmosphere.Length > 0)
                {
                    if (alertas.CumpleCriterios(da, 0))
                    {
                        Interest.Add(new Interes(scanEvent.BodyName, da.nombre, string.Empty));
                    }
                }

                // Aterrizable con Anillo
                da = alertas.n[Alerta.Anillo];
                if (da.flag && scanEvent.Rings != null)
                {
                    valor = (double)scanEvent.Rings[scanEvent.Rings.Count()-1].OuterRad;
                    valor = (valor - (double)scanEvent.Rings[0].InnerRad);
                    valor = valor / 1000;
                    //valor = (ring.OuterRad.GetValueOrDefault(0) - ring.InnerRad.GetValueOrDefault(0)) / (double)scanEvent.Radius;
                    //valor = scanEvent.Rings?.Count() ?? 0;

                    if (alertas.CumpleCriterios(da, valor))
                    {
                        //detalle = $"{valor.ToString("0")} anillo/s";
                        detalle = $"{valor:N0} Km anchura anillo/s";
                        Interest.Add(new Interes(scanEvent.BodyName, da.nombre, detalle, alertas.isRecord));

                        if (alertas.isRecord)
                            Interest.Add(new Interes(scanEvent.BodyName, "Record Personal", alertas.recordDesc));
                    }
                }


                // Aterrizable Bajo-g
                da = alertas.n[Alerta.GravedadP];
                if (da.flag)
                {
                    valor = (double)scanEvent.SurfaceGravity / 9.81;

                    if (alertas.CumpleCriterios(da, valor))
                    {
                        detalle = $"Gravedad en superficie: {valor:0.0000}g";
                        Interest.Add(new Interes(scanEvent.BodyName, da.nombre, detalle, alertas.isRecord));

                        if (alertas.isRecord)
                            Interest.Add(new Interes(scanEvent.BodyName, "Record Personal", alertas.recordDesc));
                    }
                }

                // Aterrizable Alto-g
                da = alertas.n[Alerta.GravedadG];
                if (da.flag)
                {
                    valor = (double)scanEvent.SurfaceGravity / 9.81;

                    if (alertas.CumpleCriterios(da, valor))
                    {
                        detalle = $"Gravedad en superficie: {valor:0.00}g";
                        Interest.Add(new Interes(scanEvent.BodyName, da.nombre, detalle, alertas.isRecord));

                        if (alertas.isRecord)
                            Interest.Add(new Interes(scanEvent.BodyName, "Record Personal", alertas.recordDesc));
                    }
                }

                // Aterrizable Pequeño
                da = alertas.n[Alerta.CuerpoP];
                if (da.flag)
                {
                    valor = (double)scanEvent.Radius / 1000;

                    if (alertas.CumpleCriterios(da, valor))
                    {
                        detalle = $"Radio: {valor:0}km";
                        Interest.Add(new Interes(scanEvent.BodyName, da.nombre, detalle, alertas.isRecord));

                        if (alertas.isRecord)
                            Interest.Add(new Interes(scanEvent.BodyName, "Record Personal", alertas.recordDesc));
                    }
                }

                // Aterrizable Grande
                da = alertas.n[Alerta.CuerpoG];
                if (da.flag)
                {
                    valor = (double)scanEvent.Radius / 1000;

                    if (alertas.CumpleCriterios(da, valor))
                    {
                        detalle = $"Radio: {valor:0}km";
                        Interest.Add(new Interes(scanEvent.BodyName, da.nombre, detalle, alertas.isRecord));

                        if (alertas.isRecord)
                            Interest.Add(new Interes(scanEvent.BodyName, "Record Personal", alertas.recordDesc));
                    }
                }
            }

            // CREMATORIA - Skardee I
            // scanEvent.Parent?[0].ParentType == "Star"
            //if (alertas.n[Alerta.Crematoria].flag && scanEvent.Landable.GetValueOrDefault(false) && scanEvent.DistanceFromArrivalLs < 8 && (double)scanEvent.OrbitalPeriod / 86400 < alertas.n[Alerta.Crematoria].desde)
            da = alertas.n[Alerta.Crematoria];
            if (da.flag && scanEvent.DistanceFromArrivalLs > 0 && scanEvent.DistanceFromArrivalLs < 8 && (double)scanEvent.OrbitalPeriod / 86400 < alertas.n[Alerta.Crematoria].desde)
            {
                double d = (double)scanEvent.OrbitalPeriod / 86400;
                double g = Math.Abs((double)scanEvent.SurfaceTemperature);
                detalle = $"{scanEvent.DistanceFromArrivalLs.ToString("0")} LS  {d.ToString("0.00")} dias  {g.ToString("0")} grados";
                Interest.Add(new Interes(scanEvent.BodyName, da.nombre, detalle));
            }

            // Nombre especial
            da = alertas.n[Alerta.NombreEspecial];
            if (da.flag && !scanEvent.BodyName.Contains(logMonitor.CurrentSystem))
            {
                detalle = "Nombre del cuerpo independiente del Sistema";
                Interest.Add(new Interes(scanEvent.BodyName, da.nombre, detalle));
            }

            //if (scanEvent.BodyName == "Byeia Eurk CQ-X b56-1 13")
            //{
            //}

            alertaAnillo(Alerta.AnilloIcy);
            alertaAnillo(Alerta.AnilloRock);
            alertaAnillo(Alerta.AnilloMetal);
            alertaAnillo(Alerta.AnilloMetalRich);

            //// Contiene un Anillo Helado
            //da = alertas.n[Alerta.AnilloIcy];
            //if (da.flag && scanEvent.Rings?.Count() > 0)
            //{
            //    valor = scanEvent.Rings.Where(ring => ring.RingClass == "eRingClass_Icy").Count();
            //    //eRingClass_MetalRich eRingClass_Rocky eRingClass_Metalic

            //    if (alertas.CumpleCriterios(da, valor))
            //    {
            //        // Anillo de {valor / 1000:N0}km, 
            //        detalle = $"{valor.ToString("0")} anillos helados.";
            //        Interest.Add((scanEvent.BodyName, da.nombre, detalle));

            //        if (alertas.isRecord)
            //            Interest.Add((scanEvent.BodyName, "Record Personal", alertas.recordDesc));
            //    }
            //}

            // Ancho del Anillo x veces el Radio
            da = alertas.n[Alerta.AnilloG];
            if (da.flag && scanEvent.Rings != null)
            {
                foreach (Ring ring in scanEvent.Rings.Where(ring => !ring.Name.Contains("Belt")))
                {
                    valor = (ring.OuterRad.GetValueOrDefault(0) - ring.InnerRad.GetValueOrDefault(0)) / (double)scanEvent.Radius;

                    if (alertas.CumpleCriterios(da, valor))
                    {
                        // Anillo de {valor / 1000:N0}km, 
                        detalle = $"{valor.ToString("0.0")} veces el radio del Planeta.";
                        Interest.Add(new Interes(ring.Name, da.nombre, detalle, alertas.isRecord));

                        if (alertas.isRecord)
                            Interest.Add(new Interes(scanEvent.BodyName, "Record Personal", alertas.recordDesc));
                    }
                }
            }

            // Comprobaciones relativas al Padre
            if ((alertas.n[Alerta.OrbitaP].flag || alertas.n[Alerta.OrbitaG].flag || alertas.n[Alerta.AnilloP].flag) && 
                (scanEvent.Parent?[0].ParentType == "Planet" || scanEvent.Parent?[0].ParentType == "Star") &&
                logMonitor.SystemBody.ContainsKey((logMonitor.CurrentSystem, scanEvent.Parent[0].Body)))
            {
                ScanEvent parent = logMonitor.SystemBody[(logMonitor.CurrentSystem, scanEvent.Parent[0].Body)];

                // Distancia entre superficies
                valor = Math.Truncate(((double)scanEvent.SemiMajorAxis - (double)parent.Radius - (double)scanEvent.Radius) / 1000);

                //Orbita Pequeña
                da = alertas.n[Alerta.OrbitaP];
                if (da.flag && alertas.CumpleCriterios(da, valor))    
                {
                    detalle = $"Distancia entre superficies {valor:N0} Km";
                    Interest.Add(new Interes(scanEvent.BodyName, da.nombre, detalle, alertas.isRecord));

                    if (alertas.isRecord)
                        Interest.Add(new Interes(scanEvent.BodyName, "Record Personal", alertas.recordDesc));
                }

                // distancia en SL (1 SL = 299.792,36 Km)
                valor = valor / 299792.36; 

                //Orbita Grande
                da = alertas.n[Alerta.OrbitaG];
                if (da.flag && alertas.CumpleCriterios(da, valor))
                {
                    detalle = $"Distancia entre superficies {valor:N0} sl";
                    Interest.Add(new Interes(scanEvent.BodyName, da.nombre, detalle, alertas.isRecord));

                    if (alertas.isRecord)
                        Interest.Add(new Interes(scanEvent.BodyName, "Record Personal", alertas.recordDesc));
                }

                //Luna de Pastor
                //if (alertas.n[Alerta.Pastor].flag && parent.Rings?.Last().OuterRad > scanEvent.SemiMajorAxis && !parent.Rings.Last().Name.Contains(" Belt"))
                //{
                //    Interest.Add((scanEvent.BodyName, alertas.n[Alerta.Pastor].nombre, $"Órbita: {Math.Truncate((double)scanEvent.SemiMajorAxis / 1000):N0}km, Anillo Rádio: {Math.Truncate((double)parent.Rings.Last().OuterRad / 1000):N0}km"));
                //}

                // Proximo al Anillo
                da = alertas.n[Alerta.AnilloP];
                if (da.flag && parent.Rings != null)
                {
                    foreach (var ring in parent.Rings)
                    {
                        valor = Math.Min(Math.Abs(scanEvent.SemiMajorAxis.GetValueOrDefault(0) - (double)scanEvent.Radius - ring.OuterRad.GetValueOrDefault(0)) / 1000, Math.Abs(ring.InnerRad.GetValueOrDefault(0) - (double)scanEvent.Radius - scanEvent.SemiMajorAxis.GetValueOrDefault(0)) / 1000);

                        if (alertas.CumpleCriterios(da, valor))
                        {
                            detalle = $"Distancia al anillo {valor:N0} km";
                            Interest.Add(new Interes(scanEvent.BodyName, da.nombre, detalle, alertas.isRecord));

                            if (alertas.isRecord)
                                Interest.Add(new Interes(scanEvent.BodyName, "Record Personal", alertas.recordDesc));
                        }
                    }
                }
            }

            // Pareja Binaria Cercana
            da = alertas.n[Alerta.Binario];
            if (da.flag && scanEvent.Parent?[0].ParentType == "Null" && scanEvent.Radius / scanEvent.SemiMajorAxis > 0.4)
            {
                var binaryPartner = logMonitor.SystemBody.Where(system => system.Key.System == logMonitor.CurrentSystem && scanEvent.Parent?[0].Body == system.Value.Parent?[0].Body && scanEvent.BodyId != system.Value.BodyId);
                if (binaryPartner.Count() == 1)
                {
                    valor = (double)(binaryPartner.First().Value.SemiMajorAxis * (1 - binaryPartner.First().Value.Eccentricity) + scanEvent.SemiMajorAxis * (1 - scanEvent.Eccentricity));
                    valor = valor - (double)(binaryPartner.First().Value.Radius - scanEvent.Radius);
                    valor = (double)(binaryPartner.First().Value.Radius + scanEvent.Radius) / valor;

                    if (alertas.CumpleCriterios(da, valor))
                    {
                        detalle = $"Radios vs Distancia relacion: {valor.ToString("0.0")}";
                        Interest.Add(new Interes(scanEvent.BodyName, da.nombre, detalle, alertas.isRecord));

                        if (alertas.isRecord)
                            Interest.Add(new Interes(scanEvent.BodyName, "Record Personal", alertas.recordDesc));
                    }
                }
            }

            // Luna Anidada
            //if (alertas.n[Alerta.Anidada].flag && scanEvent.Parent?.Count() > 1 && scanEvent.Parent[0].ParentType == "Planet" && scanEvent.Parent[1].ParentType == "Planet")
            //{
            //    Interest.Add((scanEvent.BodyName, alertas.n[Alerta.Anidada].nombre, string.Empty));
            //}

            // Rotacion Rápida
            da = alertas.n[Alerta.RotacionR];
            if (alertas.n[Alerta.RotacionR].flag && scanEvent.RotationPeriod != null && !scanEvent.TidalLock.GetValueOrDefault(true) && !isRing)
            {
                valor = Math.Abs((double)scanEvent.RotationPeriod / 3600);

                if (alertas.CumpleCriterios(da, valor))
                {
                    detalle = $"Periodo rotacional de {valor:0.0} horas";
                    Interest.Add(new Interes(scanEvent.BodyName, da.nombre, detalle, alertas.isRecord));

                    if (alertas.isRecord)
                        Interest.Add(new Interes(scanEvent.BodyName, "Record Personal", alertas.recordDesc));
                }
            }

            // Orbita Rápida
            da = alertas.n[Alerta.OrbitaR];
            if (da.flag && scanEvent.OrbitalPeriod != null && !isRing)
            {
                valor = (double)scanEvent.OrbitalPeriod / 3600;

                if (alertas.CumpleCriterios(da, valor))
                {
                    detalle = $"Orbita completa en {valor.ToString("0.0")} horas";
                    Interest.Add(new Interes(scanEvent.BodyName, da.nombre, detalle, alertas.isRecord));

                    if (alertas.isRecord)
                        Interest.Add(new Interes(scanEvent.BodyName, "Record Personal", alertas.recordDesc));
                }
            }

            // High eccentricity
            //if (alertas.n[Alerta.Excentricidad].flag && scanEvent.Eccentricity > alertas.n[Alerta.Excentricidad].desde)
            //{
            //    detalle = $"Excentricidad de {Math.Round((decimal)scanEvent.Eccentricity, 2)}";
            //    Interest.Add((scanEvent.BodyName, alertas.n[Alerta.Excentricidad].nombre, detalle));
            //}


            // Good jumponium material availability
            //if (alertas.n[Alerta.Potenciar].flag && flgAterrizable)
            //{
            //    int jumpMats = 0;
            //    Materials matsNotFound = PremiumBoostMaterials;
            //    foreach (MaterialComposition material in scanEvent.Materials)
            //    {
            //        Materials matFound = (Materials)MaterialLookup.ByName(material.Name.ToLower()); //(Materials)Enum.Parse(typeof(Materials), material.Name, true);

            //        if ((matFound & PremiumBoostMaterials) == matFound)
            //        {
            //            jumpMats++;
            //            matsNotFound ^= matFound;
            //        }

            //    }
            //    if (jumpMats == 6)
            //    {
            //        Interest.Add((scanEvent.BodyName, alertas.n[Alerta.Potenciar].nombre, "Todos los materiales necesarios disponibles en un solo Planeta"));
            //    }
            //    else if (jumpMats == 5)
            //    {
            //        Interest.Add((scanEvent.BodyName, alertas.n[Alerta.Potenciar].nombre, $"Disponibles 5 materiales de 6, ausencia de {matsNotFound}"));
            //    }
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
                    case Alerta.AnilloIcy:       { clase = "eRingClass_Icy"; break; }
                    case Alerta.AnilloMetal:     { clase = "eRingClass_Metalic"; break; }
                    case Alerta.AnilloMetalRich: { clase = "eRingClass_MetalRich"; break; }
                    case Alerta.AnilloRock:      { clase = "eRingClass_Rocky"; break; }
                }

                if (scanEvent.Rings.Where(ring => ring.RingClass == clase).Count() > 0)
                {
                    Ring r = scanEvent.Rings.Where(ring => ring.RingClass == clase).OrderByDescending(ring => ring.MassMT).First();

                    if (!r.Name.Contains(" Belt"))
                    {
                        valor = (double)r.MassMT / 1000000000000000;

                        if (alertas.CumpleCriterios(da, valor))
                        {
                            // Anillo de {valor / 1000:N0}km, 
                            detalle = $"masa {valor:N2} Mt. (x10^15)";
                            Interest.Add(new Interes(r.Name, da.nombre, detalle, alertas.isRecord));

                            if (alertas.isRecord)
                                Interest.Add(new Interes(r.Name, "Record Personal", alertas.recordDesc));
                        }
                    }
                }
            }
        }
    }
}
