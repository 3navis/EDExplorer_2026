using System;
using System.Collections.Generic;
using System.Linq;

namespace EDExplorer
{
    class ScanReader
    {

        private readonly bool isRing;
        public List<(string BodyName, string Description, string Detail)> Interest { get; private set; }
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
            this.logMonitor = b.logMonitor;
            this.settings = Properties.Settings.Default;
            Interest = new List<(string BodyName, string Description, string Detail)>();
            isRing = logMonitor.LastScan.BodyName.Contains(" Ring");
            this.alertas = b.alertas; // new Alertas();
        }

        public bool hayAlertas()
        {
            bool interesting = !isRing && (DefaultInterest() | false);
            // Moved these outside the "DefaultInterest" method so the multiple criteria check would include user criteria, and so the "all jumponium" check would not be counted

            // Add note if multiple checks triggered
            if (settings.VeryInteresting && Interest.Count() > 1)
            {
                detalle = $"{Interest.Count()} Criterios Satisfechos";
                Interest.Add((logMonitor.LastScan.BodyName, "Criterios Múltiples", detalle));
            }

            // Check history to determine if all jumponium materials available in system
            if (!logMonitor.GoldSystemReported && (alertas.n[Alerta.Potenciar].flag || alertas.n[Alerta.Grado5].flag) && logMonitor.LastScan.Landable.GetValueOrDefault(false))
            {
                Materials matsFound = Materials.None;

                foreach (var scan in logMonitor.SystemBody.Where(scan => scan.Key.System == logMonitor.CurrentSystem && scan.Value.Landable.GetValueOrDefault(false)))
                {
                    foreach (MaterialComposition material in scan.Value.Materials)
                    {
                        matsFound |= (Materials)MaterialLookup.ByName(material.Name.ToLower()); //(Materials)Enum.Parse(typeof(Materials), material.Name, true);
                    }

                    if (alertas.n[Alerta.Grado5].flag && (matsFound & GoldSystemMaterials) == GoldSystemMaterials)
                    {
                        interesting = true;
                        logMonitor.GoldSystemReported = true;
                        Interest.Add((logMonitor.CurrentSystem, alertas.n[Alerta.Grado5].nombre, "Disponibles todos los Materiales en el Sistema"));
                    }
                    else if (alertas.n[Alerta.Potenciar].flag && !logMonitor.JumponiumReported && (matsFound & PremiumBoostMaterials) == PremiumBoostMaterials)
                    {
                        interesting = true;
                        logMonitor.JumponiumReported = true;
                        Interest.Add((logMonitor.CurrentSystem, alertas.n[Alerta.Potenciar].nombre, "Disponibles todos los Materiales necesarios en el Sistema"));
                    }
                }
            }

            if (Interest.Count() == 0)
            {
                Interest.Add((logMonitor.LastScan.BodyName, "Sin Interés", string.Empty));
            }
            return interesting;
        }

        private bool DefaultInterest()
        {
            ScanEvent scanEvent = logMonitor.LastScan;
            bool flgAterrizable = scanEvent.Landable.GetValueOrDefault(false);

            // Aterrizable y Terraformable
            //if (alertas.n[Alerta.Terraformable].flag && flgAterrizable && scanEvent.TerraformState.Length > 0)
            //{
            //    Interest.Add((scanEvent.BodyName, alertas.n[Alerta.Terraformable].nombre, string.Empty));
            //}
            if (alertas.n[Alerta.Terraformable].flag && flgAterrizable && scanEvent.TerraformState.Length > 0)
            {
                if (alertas.CumpleCriterios(alertas.n[Alerta.Terraformable], 0))
                {
                    Interest.Add((scanEvent.BodyName, alertas.n[Alerta.Terraformable].nombre, string.Empty));
                }
            }
            // Aterrizable con Atmosfera. Ya me gustaria!
            if (alertas.n[Alerta.Atmosfera].flag && flgAterrizable && scanEvent.Atmosphere.Length > 0)
            {
                Interest.Add((scanEvent.BodyName, alertas.n[Alerta.Atmosfera].nombre, string.Empty));
            }

            // Aterrizable Alto-g
            if (alertas.n[Alerta.GravedadP].flag && flgAterrizable && (double)scanEvent.SurfaceGravity / 9.81 < alertas.n[Alerta.GravedadP].desde)
            {
                detalle = $"Gravedad en superficie: {((double)scanEvent.SurfaceGravity / 9.81).ToString("0.0000")}g";
                Interest.Add((scanEvent.BodyName, alertas.n[Alerta.GravedadP].nombre, detalle));
            }

            // Aterrizable Bajo-g
            if (alertas.n[Alerta.GravedadG].flag && flgAterrizable && (double)scanEvent.SurfaceGravity / 9.81 > alertas.n[Alerta.GravedadG].desde)
            {
                detalle = $"Gravedad en superficie: {((double)scanEvent.SurfaceGravity / 9.81).ToString("0.00")}g";
                Interest.Add((scanEvent.BodyName, alertas.n[Alerta.GravedadG].nombre, detalle));
            }

            // Aterrizable Pequeño
            if (alertas.n[Alerta.CuerpoP].flag && flgAterrizable)
            {
                double Radio = (double)scanEvent.Radius / 1000;

                if (alertas.CumpleCriterios(alertas.n[Alerta.CuerpoP], Radio))
                {
                    detalle = $"Radio: {(Radio).ToString("0")}km";
                    Interest.Add((scanEvent.BodyName, alertas.n[Alerta.CuerpoP].nombre, detalle));
                }
            }

            // Aterrizable Grande
            if (alertas.n[Alerta.CuerpoG].flag && flgAterrizable)
            {
                double Radio = (double)scanEvent.Radius / 1000;

                if (alertas.CumpleCriterios(alertas.n[Alerta.CuerpoG], Radio))
                {
                    detalle = $"Radio: {(Radio).ToString("0")}km";
                    Interest.Add((scanEvent.BodyName, alertas.n[Alerta.CuerpoG].nombre, detalle));
                }
            }

            //// Aterrizable Pequeño
            //if (alertas.n[Alerta.CuerpoP].flag && scanEvent.Landable.GetValueOrDefault(false) && scanEvent.Radius / 1000 < alertas.n[Alerta.CuerpoP].desde)
            //{
            //    detalle = $"Radio: {((double)scanEvent.Radius / 1000).ToString("0")}km";
            //    Interest.Add((scanEvent.BodyName, alertas.n[Alerta.CuerpoP].nombre, detalle));
            //}

            //// Aterrizable Gigante
            //if (alertas.n[Alerta.CuerpoG].flag && scanEvent.Landable.GetValueOrDefault(false) && scanEvent.Radius/1000 > alertas.n[Alerta.CuerpoG].desde)
            //{
            //    detalle = $"Radio: {((double)scanEvent.Radius / 1000).ToString("0")}km";
            //    Interest.Add((scanEvent.BodyName, alertas.n[Alerta.CuerpoG].nombre, detalle));
            //}

            // Aterrizable con Anillo
            if (alertas.n[Alerta.Anillo].flag && flgAterrizable && scanEvent.Rings?.Count() > alertas.n[Alerta.Anillo].desde)
            {
                detalle = $"{scanEvent.Rings?.Count().ToString("0")} anillo/s";
                Interest.Add((scanEvent.BodyName, alertas.n[Alerta.Anillo].nombre, detalle));
            }

            // CREMATORIA - Skardee I
            // scanEvent.Parent?[0].ParentType == "Star"
            //if (alertas.n[Alerta.Crematoria].flag && scanEvent.Landable.GetValueOrDefault(false) && scanEvent.DistanceFromArrivalLs < 8 && (double)scanEvent.OrbitalPeriod / 86400 < alertas.n[Alerta.Crematoria].desde)
            if (alertas.n[Alerta.Crematoria].flag && scanEvent.DistanceFromArrivalLs > 0 && scanEvent.DistanceFromArrivalLs < 8 && (double)scanEvent.OrbitalPeriod / 86400 < alertas.n[Alerta.Crematoria].desde)
            {
                double d = (double)scanEvent.OrbitalPeriod / 86400;
                double g = Math.Abs((double)scanEvent.SurfaceTemperature);
                detalle = $"{scanEvent.DistanceFromArrivalLs.ToString("0")} LS  {d.ToString("0.00")} dias  {g.ToString("0")} grados";
                Interest.Add((scanEvent.BodyName, alertas.n[Alerta.Crematoria].nombre, detalle));
            }

            //if (true && scanEvent.Landable.GetValueOrDefault(false) && scanEvent.DistanceFromArrivalLs < 5 && (double)scanEvent.SurfaceTemperature > 1000)
            //{
            //    detalle = $"{scanEvent.DistanceFromArrivalLs.ToString("0")} LS   {Math.Abs((double)scanEvent.SurfaceTemperature)} Grados";
            //    Interest.Add((scanEvent.BodyName, "CREMATORIA 2", detalle));
            //}
            // Nombre Especial
            if (alertas.n[Alerta.NombreEspecial].flag && scanEvent.BodyName.Replace(logMonitor.CurrentSystem, "").Trim() == scanEvent.BodyName)
            {
                detalle = "El nombre del cuerpo no contiene el Sistema";
                Interest.Add((scanEvent.BodyName, alertas.n[Alerta.NombreEspecial].nombre, detalle));
            }

            // Ancho del Anillo x veces el Radio
            if (alertas.n[Alerta.AnilloG].flag && scanEvent.Rings?.Count() > 0)
            {
                foreach (Ring ring in scanEvent.Rings.Where(ring => !ring.Name.Contains("Belt")))
                {
                    long ringWidth = (ring.OuterRad.GetValueOrDefault(0) - ring.InnerRad.GetValueOrDefault(0));

                    if (ringWidth > scanEvent.Radius * alertas.n[Alerta.AnilloG].desde)
                    {
                        detalle = $"Anillo de {(double)ringWidth / 1000:N0}km, {(ringWidth / (double)scanEvent.Radius).ToString("0")} veces el radio del Planeta: {Math.Truncate((double)scanEvent.Radius / 1000):N0}km";
                        Interest.Add((ring.Name, alertas.n[Alerta.AnilloG].nombre, detalle));
                    }
                }
            }

            // Comprobaciones relativas al Padre
            if ((alertas.n[Alerta.OrbitaP].flag && alertas.n[Alerta.AnilloP].flag) && (scanEvent.Parent?[0].ParentType == "Planet" || scanEvent.Parent?[0].ParentType == "Star") &&
                logMonitor.SystemBody.ContainsKey((logMonitor.CurrentSystem, scanEvent.Parent[0].Body)))
            {
                ScanEvent parent = logMonitor.SystemBody[(logMonitor.CurrentSystem, scanEvent.Parent[0].Body)];

                double distancia; // Distancia entre superficies
                distancia = Math.Truncate(((double)scanEvent.SemiMajorAxis - (double)parent.Radius - (double)scanEvent.Radius) / 1000);

                //Orbita Pequeña
                if (alertas.n[Alerta.OrbitaP].flag && distancia < alertas.n[Alerta.OrbitaP].desde)
                {
                    detalle = $"Distancia entre superficies {distancia:N0} Km";
                    Interest.Add((scanEvent.BodyName, alertas.n[Alerta.OrbitaP].nombre, detalle));
                }

                distancia = distancia / 299792;
                //Orbita Grande
                if (alertas.n[Alerta.OrbitaG].flag && distancia > alertas.n[Alerta.OrbitaG].desde)
                {
                    detalle = $"Distancia entre superficies {distancia:N0} sl";
                    Interest.Add((scanEvent.BodyName, alertas.n[Alerta.OrbitaG].nombre, detalle));
                }
                //Luna de Pastor
                //if (alertas.n[Alerta.Pastor].flag && parent.Rings?.Last().OuterRad > scanEvent.SemiMajorAxis && !parent.Rings.Last().Name.Contains(" Belt"))
                //{
                //    Interest.Add((scanEvent.BodyName, alertas.n[Alerta.Pastor].nombre, $"Órbita: {Math.Truncate((double)scanEvent.SemiMajorAxis / 1000):N0}km, Anillo Rádio: {Math.Truncate((double)parent.Rings.Last().OuterRad / 1000):N0}km"));
                //}

                // Proximo al Anillo
                if (alertas.n[Alerta.AnilloP].flag && parent.Rings?.Count() > 0)
                {
                    foreach (var ring in parent.Rings)
                    {
                        double separation = Math.Min(Math.Abs(scanEvent.SemiMajorAxis.GetValueOrDefault(0) - (double)scanEvent.Radius - ring.OuterRad.GetValueOrDefault(0)) / 1000, Math.Abs(ring.InnerRad.GetValueOrDefault(0) - (double)scanEvent.Radius - scanEvent.SemiMajorAxis.GetValueOrDefault(0)) / 1000);
                        
                        if (separation < alertas.n[Alerta.AnilloP].desde)
                        {
                            detalle = $"Distancia al anillo {separation:N0} km";
                            Interest.Add((scanEvent.BodyName, alertas.n[Alerta.AnilloP].nombre, detalle));
                        }
                    }
                }
            }

            // Pareja Binaria Cercana
            if (alertas.n[Alerta.Binario].flag && scanEvent.Parent?[0].ParentType == "Null" && scanEvent.Radius / scanEvent.SemiMajorAxis > 0.4)
            {
                var binaryPartner = logMonitor.SystemBody.Where(system => system.Key.System == logMonitor.CurrentSystem && scanEvent.Parent?[0].Body == system.Value.Parent?[0].Body && scanEvent.BodyId != system.Value.BodyId);
                if (binaryPartner.Count() == 1)
                {
                    var distancia = binaryPartner.First().Value.SemiMajorAxis * (1 - binaryPartner.First().Value.Eccentricity) + scanEvent.SemiMajorAxis * (1 - scanEvent.Eccentricity);
                    distancia = distancia - binaryPartner.First().Value.Radius - scanEvent.Radius;

                    if (distancia * alertas.n[Alerta.Binario].desde < binaryPartner.First().Value.Radius + scanEvent.Radius)
                    {
                        detalle = $"Distancia: {Math.Truncate((double)distancia / 1000):N0}km, Radio: {Math.Truncate((double)scanEvent.Radius / 1000):N0}km";
                        Interest.Add((scanEvent.BodyName, alertas.n[Alerta.Binario].nombre, detalle));
                    }
                }
            }

            // Luna Anidada
            //if (alertas.n[Alerta.Anidada].flag && scanEvent.Parent?.Count() > 1 && scanEvent.Parent[0].ParentType == "Planet" && scanEvent.Parent[1].ParentType == "Planet")
            //{
            //    Interest.Add((scanEvent.BodyName, alertas.n[Alerta.Anidada].nombre, string.Empty));
            //}


            // Rotacion Rápida
            if (alertas.n[Alerta.RotacionR].flag && scanEvent.RotationPeriod != null && !scanEvent.TidalLock.GetValueOrDefault(true) && Math.Abs((double)scanEvent.RotationPeriod / 3600) < alertas.n[Alerta.RotacionR].desde && !isRing)
            {
                detalle = $"Periodo rotacional de {Math.Abs(Math.Round((decimal)scanEvent.RotationPeriod / 3600, 2))} horas";
                Interest.Add((scanEvent.BodyName, alertas.n[Alerta.RotacionR].nombre, detalle));
            }

            // Orbita Rápida
            if (alertas.n[Alerta.OrbitaR].flag && scanEvent.OrbitalPeriod != null && (double)scanEvent.OrbitalPeriod / 3600 < alertas.n[Alerta.OrbitaR].desde && !isRing)
            {
                detalle = $"Orbita completa en {Math.Abs(Math.Round((decimal)scanEvent.OrbitalPeriod / 3600, 2))} horas";
                Interest.Add((scanEvent.BodyName, alertas.n[Alerta.OrbitaR].nombre, detalle));
            }

            // High eccentricity
            if (alertas.n[Alerta.Excentricidad].flag && scanEvent.Eccentricity > alertas.n[Alerta.Excentricidad].desde)
            {
                detalle = $"Excentricidad de {Math.Round((decimal)scanEvent.Eccentricity, 2)}";
                Interest.Add((scanEvent.BodyName, alertas.n[Alerta.Excentricidad].nombre, detalle));
            }


            // Good jumponium material availability
            if (alertas.n[Alerta.Potenciar].flag && flgAterrizable)
            {
                int jumpMats = 0;
                Materials matsNotFound = PremiumBoostMaterials;
                foreach (MaterialComposition material in scanEvent.Materials)
                {
                    Materials matFound = (Materials)MaterialLookup.ByName(material.Name.ToLower()); //(Materials)Enum.Parse(typeof(Materials), material.Name, true);

                    if ((matFound & PremiumBoostMaterials) == matFound)
                    {
                        jumpMats++;
                        matsNotFound ^= matFound;
                    }

                }
                if (jumpMats == 6)
                {
                    Interest.Add((scanEvent.BodyName, alertas.n[Alerta.Potenciar].nombre, "Todos los materiales necesarios disponibles en un solo Planeta"));
                }
                else if (jumpMats == 5)
                {
                    Interest.Add((scanEvent.BodyName, alertas.n[Alerta.Potenciar].nombre, $"Disponibles 5 materiales de 6, ausencia de {matsNotFound}"));
                }
            }

            return Interest.Count > 0;
        }
    }
}
