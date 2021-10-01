using Newtonsoft.Json;
using System.Collections.Generic;

namespace EDExplorer
{
    using ListaAlertas = Dictionary<Alerta, DetallesAlerta>;

    public class Interes
    {
        public Interes (string B, string N, string V, string D, bool R = false, string RD = "")
        {
            BodyName = B;
            Nombre = N;
            ValorST = V;
            Detalle = D;
            isRecord = R;
            RecordDesc = RD;
        }

        public string BodyName;
        public string Nombre;
        public string ValorST;
        public string Detalle;
        public bool isRecord;
        public string RecordDesc;
    }
    public enum TipoEvento
    {
        None, 
        Scan,
        Signal,
        FSS,
        Codex,
        Jump,
        Hyperspace
    }
    public enum Alerta
    {
        None,
        Terraformable, Atmosfera, 
        CuerpoG, CuerpoP, 
        Anillo, 
        AnilloG, AnilloP, 
        AnilloIcy, AnilloRock, AnilloMetal, AnilloMetalRich,
        GravedadP, GravedadG,
        OrbitaP, OrbitaG, OrbitaR, RotacionR,
        Potenciar, Grado5,
        NombreEspecial,
        Crematoria,
        Pastor, Anidada,
        Excentricidad,
        Binario,
        Tritio, LTD, Opal, Painita, Benitoita, Serendibita, Musgravita,
        Alejandrita, Grandidierita, Monacita, Rhodplumsita, 
        Geological, Biological, Human, Guardian, Thargoid,
        BodyCount,
        DistanciaStart, DistanciaSol, AcumuladoJump, NumeroJump,
        Poblacion,
        Tierra, Acuatico, Amoniaco
    }
    public enum TipoParametro
    {
        None,
        ValorDesde,
        ValorHasta,
        RangoIncluido,
        RangoExcluido
    }
    public class DetallesAlerta
    {
        public TipoEvento tipoEvento;
        public bool flag;
        public string nombreCheck;
        public string nombre;
        public double desde;
        public double hasta;
        public TipoParametro tipoParam;
        public string unidades; 
        public string detalle;
        public double? recMenor;
        public double? recMayor;
        public double? MediaRecord;
        public long numeroRecord;
        public string mask;
        public string TipoLog;

        public DetallesAlerta(TipoEvento TE, string NC, string N, double D, double H, int M, string U, string UC, string TL = "")
        {
            tipoEvento = TE;
            flag = true;
            nombreCheck = NC;
            nombre = N;
            unidades = U;
            detalle = (UC == "")?U:UC;
            desde = D;
            hasta = H;
            mask = (M >= 0)?"N"+M.ToString():"";
            tipoParam = TipoParametro.None;
            recMenor = null;
            recMayor = null;
            MediaRecord = null;
            numeroRecord = 0;
            TipoLog = TL;
            
            if (D == 0 && H == 0)
            {
                tipoParam = TipoParametro.None;
            }
            else if (D == 0 && H != 0)
            {
                tipoParam = TipoParametro.ValorHasta;
            }
            else if (D != 0 && H == 0)
            {
                if (D == -1) desde = 0;
                tipoParam = TipoParametro.ValorDesde;
            }
            else if (D > H)
            {
                desde = H;
                hasta = D;
                tipoParam = TipoParametro.RangoExcluido;
            }
            else
                tipoParam = TipoParametro.RangoIncluido;
        }
    }
    public class Alertas
    {
        private readonly Properties.Settings settings = Properties.Settings.Default;
        public Alertas()
        {
            settings.vAlertasNew = "v1.05.009";

            if (settings.vAlertas != settings.vAlertasNew)
            {
                // Si se cambia la version se resetean todas las alertas
                // Solo hay que hacerlo si cambia la estructura de las mismas

                //n = new Dictionary<Alerta, DetallesAlerta>();
                n = new ListaAlertas();
                setDefault();

                settings.Alertas = JsonConvert.SerializeObject(n);
                settings.vAlertas = settings.vAlertasNew;
                settings.Save();
            }
            else
            {
                try
                {
                    //n = JsonConvert.DeserializeObject<Dictionary<Alerta, DetallesAlerta>>(settings.Alertas);
                    n = JsonConvert.DeserializeObject<ListaAlertas>(settings.Alertas);
                }
                catch {  }

                //if (n == null) n = new Dictionary<Alerta, DetallesAlerta>();
                if (n == null) n = new ListaAlertas();

                setDefault();
            }
        }
        public void setDefault()
        {
            //try { n.Add(Alerta.Terraformable,   new DetallesAlerta(TipoEvento.Scan, "Aterrizable Terra.", "Aterrizable y Terraformable", 0, 0, "")); } catch { }
            try { n.Add(Alerta.Atmosfera,       new DetallesAlerta(TipoEvento.Scan, "Aterrizable Atmos.", "Aterrizable Atmosférico", 0, 0, -1, "","")); } catch { }
            try { n.Add(Alerta.CuerpoP,         new DetallesAlerta(TipoEvento.Scan, "Aterrizable Pequeño", "Aterrizable Pequeño", 0, 300, 0, "Km radio", "Km de Radio")); } catch { }
            try { n.Add(Alerta.CuerpoG,         new DetallesAlerta(TipoEvento.Scan, "Aterrizable Grande", "Aterrizable Grande", 18000, 0, 0, "Km radio", "Km de Radio")); } catch { }
            //try { n.Add(Alerta.Radio,           new DetallesAlerta(TipoEvento.Scan, "Aterrizable Radio", "Aterrizable Radio", 300, 1800, "Km (radio <)",TipoParametro.RangoExcluido)); } catch { }
            
            try { n.Add(Alerta.Anillo,          new DetallesAlerta(TipoEvento.Scan, "Aterrizable Anillado", "Aterrizable Anillado", 1, 0, 0, "Km ancho", "Km anchura anillo / s")); } catch { }
            try { n.Add(Alerta.GravedadP,       new DetallesAlerta(TipoEvento.Scan, "Baja Gravedad", "Aterrizable con Baja Gravedad", 0, 0.028, 4, "g superficie", "g Gravedad en superficie")); } catch { }
            try { n.Add(Alerta.GravedadG,       new DetallesAlerta(TipoEvento.Scan, "Alta Gravedad", "Aterrizable con Alta Gravedad", 2.75, 0, 2, "g superficie", "g Gravedad en superficie")); } catch { }
            try { n.Add(Alerta.OrbitaP,         new DetallesAlerta(TipoEvento.Scan, "Orbita Cercana", "Orbita Cercana", 0, 1000, 0, "Km separacion", "Km de distancia entre superficies")); } catch { }
            try { n.Add(Alerta.OrbitaG,         new DetallesAlerta(TipoEvento.Scan, "Orbita Lejana", "Orbita Lejana", 50000, 0, 0, "SL separacion", "SL de distancia entre superficies")); } catch { }
            //try { n.Add(Alerta.Potenciar,       new DetallesAlerta(TipoEvento.Scan, "Potenciar Salto", "Materiales para Potenciar Salto", 5, 0, "material")); } catch { }
            //try { n.Add(Alerta.Grado5,          new DetallesAlerta(TipoEvento.Scan, "Grado 5", "Materiales de Grado 5", 0, 0, "material")); } catch { }
            try { n.Add(Alerta.NombreEspecial,  new DetallesAlerta(TipoEvento.Scan, "Nombre Especial", "Nombre Especial", 0, 0, -1, "", "El nombre del cuerpo no incluye al Sistema")); } catch { }
            // Criterios multiples
            try { n.Add(Alerta.Crematoria,      new DetallesAlerta(TipoEvento.Scan, "Crematoria", "CREMATORIA", 0, 0.8, 2, "dias rotación", "dias rotación")); } catch { }
            //try { n.Add(Alerta.Pastor, new DetallesAlerta(true, "Luna Pastor", "Luna de Pastor", 0, "")); } catch { }
            //try { n.Add(Alerta.Anidada, new DetallesAlerta(true, "Luna Anidada", "Luna Anidada", 0, "")); } catch { }
                try { n.Add(Alerta.RotacionR,       new DetallesAlerta(TipoEvento.Scan, "Rotación Rápida", "Rotación Rápida", 0, 5, 1, "horas rotación", "horas para completar una rotación")); } catch { }
            try { n.Add(Alerta.OrbitaR,         new DetallesAlerta(TipoEvento.Scan, "Orbita Rápida", "Orbita Rápida", 0, 4, 1, "horas orbita", "horas para completar una Orbita")); } catch { }
            //try { n.Add(Alerta.Excentricidad,   new DetallesAlerta(TipoEvento.Scan, "Orb. Excéntrica", "Orbita Excéntrica", 0.9, 0, "% excentricidad")); } catch { }
            try { n.Add(Alerta.AnilloG,         new DetallesAlerta(TipoEvento.Scan, "Anillo Ancho", "Anillo Ancho", 12, 0, 1, "x veces radio", "veces el radio del Planeta")); } catch { }
            try { n.Add(Alerta.Binario,         new DetallesAlerta(TipoEvento.Scan, "Binaria Cercana", "Binaria Cercana", 0.5, 0, 2, "radio vs distancia", "relación Radio vs Distancia")); } catch { }
            try { n.Add(Alerta.AnilloP,         new DetallesAlerta(TipoEvento.Scan, "Anillo Próximo", "Próximo al Anillo", 0, 500, 0, "km separacion", "Km de distancia al borde del anillo")); } catch { }

            // Diferentes tipos de Anillo (eliminado Belt)
            try { n.Add(Alerta.AnilloIcy,       new DetallesAlerta(TipoEvento.Scan, "Anillo Helado", "Anillo Helado", -1, 0, 4, "Mt (x10^12) masa", "Mt. (x10^12) de masa")); } catch { }
            try { n.Add(Alerta.AnilloRock,      new DetallesAlerta(TipoEvento.Scan, "Anillo Rocoso", "Anillo Rocoso", -1, 0, 4, "Mt (x10^12) masa", "Mt. (x10^12) de masa")); } catch { }
            try { n.Add(Alerta.AnilloMetal,     new DetallesAlerta(TipoEvento.Scan, "Anillo Metálico", "Anillo Metálico", -1, 0, 4, "Mt (x10^12) masa", "Mt. (x10^12) de masa")); } catch { }
            try { n.Add(Alerta.AnilloMetalRich, new DetallesAlerta(TipoEvento.Scan, "Anillo Rico Metal", "Anillo Rico en Metal", -1, 0, 4, "Mt (x10^12) masa", "Mt. (x10^12) de masa")); } catch { }

            // Planetas interesantes
            try { n.Add(Alerta.Tierra,          new DetallesAlerta(TipoEvento.Scan, "tipo Tierra", "tipo Tierra", 0, 0, -1, "", "")); } catch { }
            try { n.Add(Alerta.Acuatico,        new DetallesAlerta(TipoEvento.Scan, "tipo Acuatico", "tipo Acuatico", 0, 0, -1, "", "")); } catch { }
            try { n.Add(Alerta.Amoniaco,        new DetallesAlerta(TipoEvento.Scan, "tipo Amoniaco", "tipo Amoniaco", 0, 0, -1, "", "")); } catch { }

            // Señales
            try { n.Add(Alerta.Tritio,          new DetallesAlerta(TipoEvento.Signal, "Veta Tritio", "Veta Tritio", 1, 0, 0, "número vetas", "vetas", "tritium")); } catch { }
            try { n.Add(Alerta.LTD,             new DetallesAlerta(TipoEvento.Signal, "Veta LTD", "Veta LTD", 1, 0, 0, "número vetas", "vetas", "LowTemperatureDiamond")); } catch { }
            try { n.Add(Alerta.Opal,            new DetallesAlerta(TipoEvento.Signal, "Veta Ópalos", "Veta Ópalos", 1, 0, 0, "número vetas", "vetas", "Opal")); } catch { }
            try { n.Add(Alerta.Painita,         new DetallesAlerta(TipoEvento.Signal, "Veta Painíta", "Veta Painíta", 1, 0, 0, "número vetas", "vetas", "Painite")); } catch { }
            try { n.Add(Alerta.Benitoita,       new DetallesAlerta(TipoEvento.Signal, "Veta Benitoíta", "Veta Benitoíta", 1, 0, 0, "número vetas", "vetas", "Benitoite")); } catch { }
            try { n.Add(Alerta.Serendibita,     new DetallesAlerta(TipoEvento.Signal, "Veta Serendibíta", "Veta Serendibíta", 1, 0, 0, "número vetas", "vetas", "Serendibite")); } catch { }
            try { n.Add(Alerta.Musgravita,      new DetallesAlerta(TipoEvento.Signal, "Veta Musgravíta", "Veta Musgravíta", 1, 0, 0, "número vetas", "vetas", "Musgravite")); } catch { }
            try { n.Add(Alerta.Alejandrita,     new DetallesAlerta(TipoEvento.Signal, "Veta Alejandríta", "Veta Alejandrita", 1, 0, 0, "número vetas", "vetas", "Alexandrite")); } catch { }
            try { n.Add(Alerta.Monacita,        new DetallesAlerta(TipoEvento.Signal, "Veta Grandidieríta", "Veta Grandidieríta", 1, 0, 0, "número vetas", "vetas", "Grandidierite")); } catch { }
            try { n.Add(Alerta.Grandidierita,   new DetallesAlerta(TipoEvento.Signal, "Veta Monacita", "Veta Monacita", 1, 0, 0, "número vetas", "vetas", "Monazite")); } catch { }
            try { n.Add(Alerta.Rhodplumsita,    new DetallesAlerta(TipoEvento.Signal, "Veta Rhodplumsita", "Veta Rhodplumsita", 1, 0, 0, "número vetas", "vetas", "Rhodplumsite")); } catch { }
            
            try { n.Add(Alerta.Geological,      new DetallesAlerta(TipoEvento.Signal, "Señal Geológica", "Geológica", 1, 0, 0, "número señales", "señales", "$SAA_SignalType_Geological;")); } catch { }
            try { n.Add(Alerta.Biological,      new DetallesAlerta(TipoEvento.Signal, "Señal Biológica", "Biológica", 1, 0, 0, "número señales", "señales", "$SAA_SignalType_Biological;")); } catch { }
            try { n.Add(Alerta.Human,           new DetallesAlerta(TipoEvento.Signal, "Señal Humana", "Humana", 1, 0, 0, "número señales", "señales", "$SAA_SignalType_Human;")); } catch { }
            try { n.Add(Alerta.Guardian,        new DetallesAlerta(TipoEvento.Signal, "Señal Guardian", "Guardian", 1, 0, 0, "número señales", "señales", "$SAA_SignalType_Guardian;")); } catch { }
            try { n.Add(Alerta.Thargoid,        new DetallesAlerta(TipoEvento.Signal, "Señal Thargoide", "Thargoide", 1, 0, 0, "número señales", "señales", "$SAA_SignalType_Thargoid;")); } catch { }
            
            // FSS
            try { n.Add(Alerta.BodyCount,       new DetallesAlerta(TipoEvento.FSS, "FSS Cuerpos", "Número de Cuerpos", 20, 0, 0, "cuerpos", "")); } catch { }
            // Ejes de Coordenadas
 //           try { n.Add(Alerta.DistanciaStart,  new DetallesAlerta(TipoEvento.Jump, "Alejado", "Distancia Alejada", 10, 0, "al separación")); } catch { }
            try { n.Add(Alerta.AcumuladoJump,   new DetallesAlerta(TipoEvento.Jump, "Acumulado", "Mostrar Resumen", 1000, 0, 0, "al acumulados", "al de distancia acumulada en saltos")); } catch { }
            try { n.Add(Alerta.NumeroJump,      new DetallesAlerta(TipoEvento.Jump, "Saltos", "Mostrar Resumen", 10, 0, 0, "saltos acumulados", "saltos acumulados")); } catch { }
            try { n.Add(Alerta.Poblacion,       new DetallesAlerta(TipoEvento.Jump, "Poblacion", "Poblacion", 1, 0, 2, "M habitantes", "M habitantes")); } catch { }
        }

        public List<Interes> Interest { get; private set; }

        public ListaAlertas n;
        public bool isRecord;
        public string recordDesc;

        public double valor;
        public string valorST;
        public string detalle;

        public bool CumpleCriterios(in DetallesAlerta da)
        {
            bool isActiva;
            bool isRecordNew = false;
            string superiorMediaST = "";

            switch (da.tipoParam)
            {
                case TipoParametro.ValorDesde:
                    isActiva = (valor >= da.desde);
                    break;
                case TipoParametro.ValorHasta:
                    isActiva = (valor <= da.hasta);
                    break;
                case TipoParametro.RangoExcluido:
                    isActiva = (valor <= da.desde || valor >= da.hasta);
                    break;
                case TipoParametro.RangoIncluido:
                    isActiva = (valor >= da.desde && valor <= da.hasta);
                    break;
                case TipoParametro.None:
                    isActiva = true;
                    break;
                default:
                    isActiva = false;
                    break;
            }

            isRecord = false;

            if (isActiva)
            {
                if (da.tipoParam != TipoParametro.None)
                {
                    if (da.tipoParam == TipoParametro.ValorHasta || da.tipoParam == TipoParametro.RangoExcluido)
                        if (valor < da.MediaRecord || da.MediaRecord == null)
                        {
                            if (valor <= da.recMenor || da.recMenor == null)
                            {
                                if (valor == da.recMenor)
                                    recordDesc = "record actual";
                                else
                                {
                                    da.recMenor = valor;
                                    recordDesc = "record inferior";
                                    isRecordNew = true;
                                }

                                isRecord = true;
                            }
                            else
                            {
                                superiorMediaST = "inferior a la media";
                            }
                        }

                    if (da.tipoParam == TipoParametro.ValorDesde || da.tipoParam == TipoParametro.RangoExcluido)
                        if (valor > da.MediaRecord || da.MediaRecord == null)
                        {
                            if (valor >= da.recMayor || da.recMayor == null)
                            {
                                if (valor == da.recMayor)
                                    recordDesc = "record actual";
                                else
                                {
                                    da.recMayor = valor;
                                    recordDesc = "record superior";
                                    isRecordNew = true;
                                }

                                isRecord = true;
                            }
                            else
                            {
                                superiorMediaST = "superior a la media";
                            }
                        }

                    valorST = valor.ToString(da.mask);
                }

                detalle = da.detalle;
                if (superiorMediaST != "") detalle += " (" + superiorMediaST + ")";

                if (isRecordNew && da.tipoParam != TipoParametro.None)
                {
                    if (da.numeroRecord == 0)
                    {
                        da.numeroRecord = 1;
                        da.MediaRecord = valor;
                    }
                    else
                    {
                        da.numeroRecord++;
                        da.MediaRecord = (da.MediaRecord * (da.numeroRecord - 1) + valor) / da.numeroRecord;
                    }
                }

            }

            return isActiva;
        }
    }
}
