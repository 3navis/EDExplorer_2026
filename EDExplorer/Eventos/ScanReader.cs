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
            
            //if (settings.VeryInteresting && Interest.Count() > 1)
            //{
            //    detalle = $"{Interest.Count()} Criterios Satisfechos";
            //    Interest.Add(new Interes(logMonitor.LastScan.BodyName, "Criterios Múltiples", "", detalle));
            //}

            if (Interest.Count() == 0)
            {
                Interest.Add(new Interes(logMonitor.LastScan.BodyName, "Sin Interés", "", string.Empty));
            }
            return interesting;
        }

        ScanEvent scanEvent;
        DetallesAlerta da;
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
                    if (alertas.CumpleCriterios(da))
                    {
                        Interest.Add(new Interes(scanEvent.BodyName, da.nombre, "", string.Empty));
                    }
                }

                // Aterrizable con Anillo (Km)
                da = alertas.n[Alerta.Anillo];
                if (da.flag && scanEvent.Rings != null)
                {
                    alertas.valor = (double)scanEvent.Rings[scanEvent.Rings.Count()-1].OuterRad;
                    alertas.valor -= (double)scanEvent.Rings[0].InnerRad;
                    alertas.valor /= 1000;

                    if (alertas.CumpleCriterios(da))
                    {
                        detalle = "Km anchura anillo/s";
                        Interest.Add(new Interes(scanEvent.BodyName, da.nombre, alertas.valorST, detalle, alertas.isRecord));

                        if (alertas.isRecord)
                            Interest.Add(new Interes(scanEvent.BodyName, "Record Personal", alertas.valorST, alertas.recordDesc));
                    }
                }


                // Aterrizable Bajo-g
                da = alertas.n[Alerta.GravedadP];
                if (da.flag)
                {
                    alertas.valor = (double)scanEvent.SurfaceGravity / 9.81;

                    if (alertas.CumpleCriterios(da))
                    {
                        detalle = "g Gravedad en superficie.";
                        Interest.Add(new Interes(scanEvent.BodyName, da.nombre, alertas.valorST, detalle, alertas.isRecord));

                        if (alertas.isRecord)
                            Interest.Add(new Interes(scanEvent.BodyName, "Record Personal", alertas.valorST, alertas.recordDesc));
                    }
                }

                // Aterrizable Alto-g
                da = alertas.n[Alerta.GravedadG];
                if (da.flag)
                {
                    alertas.valor = (double)scanEvent.SurfaceGravity / 9.81;

                    if (alertas.CumpleCriterios(da))
                    {
                        detalle = "g Gravedad en superficie.";
                        Interest.Add(new Interes(scanEvent.BodyName, da.nombre, alertas.valorST, detalle, alertas.isRecord));

                        if (alertas.isRecord)
                            Interest.Add(new Interes(scanEvent.BodyName, "Record Personal", alertas.valorST, alertas.recordDesc));
                    }
                }

                // Aterrizable Pequeño (Km)
                da = alertas.n[Alerta.CuerpoP];
                if (da.flag)
                {
                    alertas.valor = (double)scanEvent.Radius / 1000;

                    if (alertas.CumpleCriterios(da))
                    {
                        detalle = "Km de Radio.";
                        Interest.Add(new Interes(scanEvent.BodyName, da.nombre, alertas.valorST, detalle, alertas.isRecord));

                        if (alertas.isRecord)
                            Interest.Add(new Interes(scanEvent.BodyName, "Record Personal", alertas.valorST, alertas.recordDesc));
                    }
                }

                // Aterrizable Grande (Km)
                da = alertas.n[Alerta.CuerpoG];
                if (da.flag)
                {
                    alertas.valor = (double)scanEvent.Radius / 1000;

                    if (alertas.CumpleCriterios(da))
                    {
                        detalle = "Km de Radio.";
                        Interest.Add(new Interes(scanEvent.BodyName, da.nombre, alertas.valorST, detalle, alertas.isRecord));

                        if (alertas.isRecord)
                            Interest.Add(new Interes(scanEvent.BodyName, "Record Personal", alertas.valorST, alertas.recordDesc));
                    }
                }
            }

            // CREMATORIA - Skardee I
            // scanEvent.Parent?[0].ParentType == "Star"
            //if (alertas.n[Alerta.Crematoria].flag && scanEvent.Landable.GetValueOrDefault(false) && scanEvent.DistanceFromArrivalLs < 8 && (double)scanEvent.OrbitalPeriod / 86400 < alertas.n[Alerta.Crematoria].desde)
            da = alertas.n[Alerta.Crematoria];
            if (da.flag && scanEvent.DistanceFromArrivalLs > 0 && scanEvent.DistanceFromArrivalLs < 8)
            {
                alertas.valor = (double)scanEvent.OrbitalPeriod / 86400;

                if (alertas.CumpleCriterios(da))
                {
                    double g = Math.Abs((double)scanEvent.SurfaceTemperature);
                    detalle = $"dias rotación  {scanEvent.DistanceFromArrivalLs.ToString("0")} LS  {g.ToString("0")} grados";
                    Interest.Add(new Interes(scanEvent.BodyName, da.nombre, alertas.valorST, detalle));
                }
            }

            // Nombre especial
            da = alertas.n[Alerta.NombreEspecial];
            if (da.flag && !scanEvent.BodyName.Contains(logMonitor.CurrentSystem))
            {
                detalle = "Nombre del cuerpo independiente del Sistema";
                Interest.Add(new Interes(scanEvent.BodyName, da.nombre, "", detalle));
            }

            //if (scanEvent.BodyName == "Byeia Eurk CQ-X b56-1 13")
            //{
            //}

            alertaAnillo(Alerta.AnilloIcy);
            alertaAnillo(Alerta.AnilloRock);
            alertaAnillo(Alerta.AnilloMetal);
            alertaAnillo(Alerta.AnilloMetalRich);

            // Ancho del Anillo x veces el Radio
            da = alertas.n[Alerta.AnilloG];
            if (da.flag && scanEvent.Rings != null)
            {
                // Si el primero no es un Belt ya no hay luego
                if (!scanEvent.Rings[0].Name.Contains("Belt"))
                //foreach (Ring ring in scanEvent.Rings.Where(ring => !ring.Name.Contains("Belt")))
                {
                    //alertas.valor = (ring.OuterRad.GetValueOrDefault(0) - ring.InnerRad.GetValueOrDefault(0)) / (double)scanEvent.Radius;
                    alertas.valor = ((double)scanEvent.Rings[scanEvent.Rings.Count() - 1].OuterRad - (double)scanEvent.Rings[0].InnerRad) / (double)scanEvent.Radius;

                    if (alertas.CumpleCriterios(da))
                    {
                        // Anillo de {valor / 1000:N0}km, 
                        detalle = "veces el radio del Planeta.";
                        Interest.Add(new Interes(scanEvent.Rings[0].Name, da.nombre, alertas.valorST, detalle, alertas.isRecord));

                        if (alertas.isRecord)
                            Interest.Add(new Interes(scanEvent.BodyName, "Record Personal", alertas.valorST, alertas.recordDesc));
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
                alertas.valor = Math.Truncate(((double)scanEvent.SemiMajorAxis - (double)parent.Radius - (double)scanEvent.Radius) / 1000);

                //Orbita Pequeña
                da = alertas.n[Alerta.OrbitaP];
                if (da.flag && alertas.CumpleCriterios(da))    
                {
                    detalle = "Km de distancia entre superficies.";
                    Interest.Add(new Interes(scanEvent.BodyName, da.nombre, alertas.valorST, detalle, alertas.isRecord));

                    if (alertas.isRecord)
                        Interest.Add(new Interes(scanEvent.BodyName, "Record Personal", alertas.valorST, alertas.recordDesc));
                }

                // distancia en SL (1 SL = 299.792,36 Km)
                alertas.valor /= 299792.36; 

                //Orbita Grande
                da = alertas.n[Alerta.OrbitaG];
                if (da.flag && alertas.CumpleCriterios(da))
                {
                    detalle = "Km de distancia entre superficies.";
                    Interest.Add(new Interes(scanEvent.BodyName, da.nombre, alertas.valorST, detalle, alertas.isRecord));

                    if (alertas.isRecord)
                        Interest.Add(new Interes(scanEvent.BodyName, "Record Personal", alertas.valorST, alertas.recordDesc));
                }

                // Proximo al Anillo
                da = alertas.n[Alerta.AnilloP];
                if (da.flag && parent.Rings != null)
                {
                    alertas.valor = Math.Min(Math.Abs(scanEvent.SemiMajorAxis.GetValueOrDefault(0) - (double)scanEvent.Radius - (double)parent.Rings[parent.Rings.Count() - 1].OuterRad) / 1000,
                                             Math.Abs((double)parent.Rings[0].InnerRad - (double)scanEvent.Radius - scanEvent.SemiMajorAxis.GetValueOrDefault(0)) / 1000);

                    if (alertas.CumpleCriterios(da))
                    {
                        detalle = "Km de distancia al borde del anillo.";
                        Interest.Add(new Interes(scanEvent.BodyName, da.nombre, alertas.valorST, detalle, alertas.isRecord));

                        if (alertas.isRecord)
                            Interest.Add(new Interes(scanEvent.BodyName, "Record Personal", alertas.valorST, alertas.recordDesc));
                    }
                }
            }

            // Binaria Cercana
            da = alertas.n[Alerta.Binario];
            if (da.flag && scanEvent.Parent?[0].ParentType == "Null") // && scanEvent.Radius / scanEvent.SemiMajorAxis > 0.4)
            {
                //alertas.valor = (double)(scanEvent.SemiMajorAxis * (1 - scanEvent.Eccentricity));
                //alertas.valor = alertas.valor - (double)scanEvent.Radius;
                //alertas.valor = (double)(scanEvent.Radius) / alertas.valor;

                //alertas.valor = Math.Abs((double)scanEvent.Radius / (double)(scanEvent.SemiMajorAxis - scanEvent.Radius));

                //alertas.valor = (double)scanEvent.SemiMajorAxis;
                //alertas.valor = Math.Abs((double)(alertas.valor - scanEvent.Radius)) / alertas.valor;
                
                alertas.valor = (double)(scanEvent.Radius / scanEvent.SemiMajorAxis);

                if (alertas.CumpleCriterios(da))
                {
                    detalle = "relación Radio vs Distancia.";
                    Interest.Add(new Interes(scanEvent.BodyName, da.nombre, alertas.valorST, detalle, false));
                    
                    if (alertas.isRecord)
                        Interest.Add(new Interes(scanEvent.BodyName, "Record Personal", alertas.valorST, alertas.recordDesc));
                }

                //var binaryPartner = logMonitor.SystemBody.Where(system => system.Key.System == logMonitor.CurrentSystem && scanEvent.Parent?[0].Body == system.Value.Parent?[0].Body && scanEvent.BodyId != system.Value.BodyId);
                //if (binaryPartner.Count() == 1)
                //{
                //    alertas.valor = (double)(binaryPartner.First().Value.SemiMajorAxis * (1 - binaryPartner.First().Value.Eccentricity) + scanEvent.SemiMajorAxis * (1 - scanEvent.Eccentricity));
                //    alertas.valor = alertas.valor - (double)(binaryPartner.First().Value.Radius - scanEvent.Radius);
                //    alertas.valor = (double)(binaryPartner.First().Value.Radius + scanEvent.Radius) / alertas.valor;

                //    if (alertas.CumpleCriterios(da))
                //    {
                //        //detalle = $"Radios vs Distancia relacion: {alertas.valor.ToString("0.0")}";
                //        detalle = "relacion Radios vs Distancia. (old)";
                //        Interest.Add(new Interes(scanEvent.BodyName, da.nombre, alertas.valorST, detalle, alertas.isRecord));

                //        if (alertas.isRecord)
                //            Interest.Add(new Interes(scanEvent.BodyName, "Record Personal", alertas.valorST, alertas.recordDesc));
                //    }
                //}
            }

            // Luna Anidada
            //if (alertas.n[Alerta.Anidada].flag && scanEvent.Parent?.Count() > 1 && scanEvent.Parent[0].ParentType == "Planet" && scanEvent.Parent[1].ParentType == "Planet")
            //{
            //    Interest.Add((scanEvent.BodyName, alertas.n[Alerta.Anidada].nombre, string.Empty));
            //}

            // Rotacion Rápida
            da = alertas.n[Alerta.RotacionR];
            if (alertas.n[Alerta.RotacionR].flag && scanEvent.RotationPeriod != null && !scanEvent.TidalLock.GetValueOrDefault(true))
            {
                alertas.valor = Math.Abs((double)scanEvent.RotationPeriod / 3600);

                if (alertas.CumpleCriterios(da))
                {
                    detalle = "horas para completar una rotación.";
                    Interest.Add(new Interes(scanEvent.BodyName, da.nombre, alertas.valorST, detalle, alertas.isRecord));

                    if (alertas.isRecord)
                        Interest.Add(new Interes(scanEvent.BodyName, "Record Personal", alertas.valorST, alertas.recordDesc));
                }
            }

            // Orbita Rápida
            da = alertas.n[Alerta.OrbitaR];
            if (da.flag && scanEvent.OrbitalPeriod != null && !isRing)
            {
                alertas.valor = (double)scanEvent.OrbitalPeriod / 3600;

                if (alertas.CumpleCriterios(da))
                {
                    detalle = "horas para completar una Orbita.";
                    Interest.Add(new Interes(scanEvent.BodyName, da.nombre, alertas.valorST, detalle, alertas.isRecord));

                    if (alertas.isRecord)
                        Interest.Add(new Interes(scanEvent.BodyName, "Record Personal", alertas.valorST, alertas.recordDesc));
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
                        alertas.valor = (double)r.MassMT / Math.Pow(10,12);

                        if (alertas.CumpleCriterios(da))
                        {
                            // Anillo de {valor / 1000:N0}km, 
                            //detalle = $"masa {alertas.valor:N2} Mt. (x10^12)";
                            detalle = "Mt. (x10^12) de masa.";
                            Interest.Add(new Interes(r.Name, da.nombre, alertas.valorST, detalle, alertas.isRecord));

                            if (alertas.isRecord)
                                Interest.Add(new Interes(r.Name, "Record Personal", alertas.valorST, alertas.recordDesc));
                        }
                    }
                    else
                    {

                    }
                }
            }
        }
    }
}
