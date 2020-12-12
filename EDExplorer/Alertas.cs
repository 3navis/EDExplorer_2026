using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace EDExplorer
{
    using ListaAlertas = Dictionary<Alerta, DetallesAlerta>;

    public class Interes
    {
        public Interes (string B, string N, string V, string D, bool R = false)
        {
            BodyName = B;
            Nombre = N;
            ValorST = V;
            Detalle = D;
            isRecord = R;
        }

        public string BodyName;
        public string Nombre;
        public string ValorST;
        public string Detalle;
        public bool isRecord;
    }
    public enum TipoEvento
    {
        None, 
        Scan,
        Signal,
        FSS,
        Codex,
        Jump
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
        Poblacion
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
        public double? recMenor;
        public double? recMayor;
        public string mask;
        public string TipoLog;

        public DetallesAlerta(TipoEvento TE, string NC, string N, double D, double H, int M, string U, string TL = "")
        {
            tipoEvento = TE;
            flag = true;
            nombreCheck = NC;
            nombre = N;
            unidades = U;
            desde = D;
            hasta = H;
            mask = (M >= 0)?"N"+M.ToString():"";
            tipoParam = TipoParametro.None;
            recMenor = null;
            recMayor = null;
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
            settings.vAlertasNew = "v1.04.039";

            if (settings.vAlertas != settings.vAlertasNew)
            {
                // Si se cambia la version se resetean todas las alertas
                // Esto hay que hacerlo solo si cambia la estructura

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
            try { n.Add(Alerta.Atmosfera,       new DetallesAlerta(TipoEvento.Scan, "Aterrizable Atmos.", "Aterrizable Atmosférico", 0, 0, -1, "")); } catch { }
            try { n.Add(Alerta.CuerpoP,         new DetallesAlerta(TipoEvento.Scan, "Aterrizable Pequeño", "Aterrizable Pequeño", 0, 300, 0, "Km radio")); } catch { }
            try { n.Add(Alerta.CuerpoG,         new DetallesAlerta(TipoEvento.Scan, "Aterrizable Grande", "Aterrizable Grande", 18000, 0, 0, "Km radio")); } catch { }
            //try { n.Add(Alerta.Radio,           new DetallesAlerta(TipoEvento.Scan, "Aterrizable Radio", "Aterrizable Radio", 300, 1800, "Km (radio <)",TipoParametro.RangoExcluido)); } catch { }
            
            try { n.Add(Alerta.Anillo,          new DetallesAlerta(TipoEvento.Scan, "Aterrizable Anillado", "Aterrizable Anillado", 1, 0, 0, "Km ancho")); } catch { }
            try { n.Add(Alerta.GravedadP,       new DetallesAlerta(TipoEvento.Scan, "Baja Gravedad", "Aterrizable con Baja Gravedad", 0, 0.028, 4, "g superficie")); } catch { }
            try { n.Add(Alerta.GravedadG,       new DetallesAlerta(TipoEvento.Scan, "Alta Gravedad", "Aterrizable con Alta Gravedad", 2.75, 0, 2, "g superficie")); } catch { }
            try { n.Add(Alerta.OrbitaP,         new DetallesAlerta(TipoEvento.Scan, "Orbita Cercana", "Orbita Cercana", 0, 1000, 0, "Km separacion")); } catch { }
            try { n.Add(Alerta.OrbitaG,         new DetallesAlerta(TipoEvento.Scan, "Orbita Lejana", "Orbita Lejana", 50000, 0, 0, "SL separacion")); } catch { }
            //try { n.Add(Alerta.Potenciar,       new DetallesAlerta(TipoEvento.Scan, "Potenciar Salto", "Materiales para Potenciar Salto", 5, 0, "material")); } catch { }
            //try { n.Add(Alerta.Grado5,          new DetallesAlerta(TipoEvento.Scan, "Grado 5", "Materiales de Grado 5", 0, 0, "material")); } catch { }
            try { n.Add(Alerta.NombreEspecial,  new DetallesAlerta(TipoEvento.Scan, "Nombre Especial", "Nombre Especial", 0, 0, -1, "")); } catch { }
            // Criterios multiples
            try { n.Add(Alerta.Crematoria,      new DetallesAlerta(TipoEvento.Scan, "Crematoria", "CREMATORIA", 0, 0.5, 2, "dias rotación")); } catch { }
            //try { n.Add(Alerta.Pastor, new DetallesAlerta(true, "Luna Pastor", "Luna de Pastor", 0, "")); } catch { }
            //try { n.Add(Alerta.Anidada, new DetallesAlerta(true, "Luna Anidada", "Luna Anidada", 0, "")); } catch { }
            try { n.Add(Alerta.RotacionR,       new DetallesAlerta(TipoEvento.Scan, "Rotación Rápida", "Rotación Rápida", 0, 5, 1, "horas rotación")); } catch { }
            try { n.Add(Alerta.OrbitaR,         new DetallesAlerta(TipoEvento.Scan, "Orbita Rápida", "Orbita Rápida", 0, 4, 1, "horas periodo")); } catch { }
            //try { n.Add(Alerta.Excentricidad,   new DetallesAlerta(TipoEvento.Scan, "Orb. Excéntrica", "Orbita Excéntrica", 0.9, 0, "% excentricidad")); } catch { }
            try { n.Add(Alerta.AnilloG,         new DetallesAlerta(TipoEvento.Scan, "Anillo Ancho", "Anillo Ancho", 12, 0, 1, "x veces radio")); } catch { }
            try { n.Add(Alerta.Binario,         new DetallesAlerta(TipoEvento.Scan, "Binaria Cercana", "Binaria Cercana", 0.5, 0, 2, "radios vs distancia")); } catch { }
            try { n.Add(Alerta.AnilloP,         new DetallesAlerta(TipoEvento.Scan, "Anillo Próximo", "Próximo al Anillo", 0, 500, 0, "km separacion")); } catch { }

            // Diferentes tipos de Anillo (eliminado Belt)
            try { n.Add(Alerta.AnilloIcy,       new DetallesAlerta(TipoEvento.Scan, "Anillo Helado", "Anillo Helado", -1, 0, 4, "Mt masa (x10^12)")); } catch { }
            try { n.Add(Alerta.AnilloRock,      new DetallesAlerta(TipoEvento.Scan, "Anillo Rocoso", "Anillo Rocoso", -1, 0, 4, "Mt masa (x10^12)")); } catch { }
            try { n.Add(Alerta.AnilloMetal,     new DetallesAlerta(TipoEvento.Scan, "Anillo Metálico", "Anillo Metálico", -1, 0, 4, "Mt masa (x10^12)")); } catch { }
            try { n.Add(Alerta.AnilloMetalRich, new DetallesAlerta(TipoEvento.Scan, "Anillo Rico Metal", "Anillo Rico en Metal", -1, 0, 4, "Mt masa (x10^12)")); } catch { }

            // Señales
            try { n.Add(Alerta.Tritio,          new DetallesAlerta(TipoEvento.Signal, "Veta Tritio", "Veta Tritio", 1, 0, 0, "número vetas", "tritium")); } catch { }
            try { n.Add(Alerta.LTD,             new DetallesAlerta(TipoEvento.Signal, "Veta LTD", "Veta LTD", 1, 0, 0, "número vetas", "LowTemperatureDiamond")); } catch { }
            try { n.Add(Alerta.Opal,            new DetallesAlerta(TipoEvento.Signal, "Veta Ópalos", "Veta Ópalos", 1, 0, 0, "número vetas", "Opal")); } catch { }
            try { n.Add(Alerta.Painita,         new DetallesAlerta(TipoEvento.Signal, "Veta Painíta", "Veta Painíta", 1, 0, 0, "número vetas", "Painite")); } catch { }
            try { n.Add(Alerta.Benitoita,       new DetallesAlerta(TipoEvento.Signal, "Veta Benitoíta", "Veta Benitoíta", 1, 0, 0, "número vetas", "Benitoite")); } catch { }
            try { n.Add(Alerta.Serendibita,     new DetallesAlerta(TipoEvento.Signal, "Veta Serendibíta", "Veta Serendibíta", 1, 0, 0, "número vetas", "Serendibite")); } catch { }
            try { n.Add(Alerta.Musgravita,      new DetallesAlerta(TipoEvento.Signal, "Veta Musgravíta", "Veta Musgravíta", 1, 0, 0, "número vetas", "Musgravite")); } catch { }
            try { n.Add(Alerta.Alejandrita,     new DetallesAlerta(TipoEvento.Signal, "Veta Alejandríta", "Veta Alejandrita", 1, 0, 0, "número vetas", "Alexandrite")); } catch { }
            try { n.Add(Alerta.Monacita,        new DetallesAlerta(TipoEvento.Signal, "Veta Grandidieríta", "Veta Grandidieríta", 1, 0, 0, "número vetas", "Grandidierite")); } catch { }
            try { n.Add(Alerta.Grandidierita,   new DetallesAlerta(TipoEvento.Signal, "Veta Monacita", "Veta Monacita", 1, 0, 0, "número vetas", "Monazite")); } catch { }
            try { n.Add(Alerta.Rhodplumsita,    new DetallesAlerta(TipoEvento.Signal, "Veta Rhodplumsita", "Veta Rhodplumsita", 1, 0, 0, "número vetas", "Rhodplumsite")); } catch { }
            
            try { n.Add(Alerta.Geological,      new DetallesAlerta(TipoEvento.Signal, "Señal Geológica", "Geológica", 1, 0, 0, "número señales", "$SAA_SignalType_Geological;")); } catch { }
            try { n.Add(Alerta.Biological,      new DetallesAlerta(TipoEvento.Signal, "Señal Biológica", "Biológica", 1, 0, 0, "número señales", "$SAA_SignalType_Biological;")); } catch { }
            try { n.Add(Alerta.Human,           new DetallesAlerta(TipoEvento.Signal, "Señal Humana", "Humana", 1, 0, 0, "número señales", "$SAA_SignalType_Human;")); } catch { }
            try { n.Add(Alerta.Guardian,        new DetallesAlerta(TipoEvento.Signal, "Señal Guardian", "Guardian", 1, 0, 0, "número señales", "$SAA_SignalType_Guardian;")); } catch { }
            try { n.Add(Alerta.Thargoid,        new DetallesAlerta(TipoEvento.Signal, "Señal Thargoide", "Thargoide", 1, 0, 0, "número señales", "$SAA_SignalType_Thargoid;")); } catch { }
            
            // FSS
            try { n.Add(Alerta.BodyCount,       new DetallesAlerta(TipoEvento.FSS, "FSS Cuerpos", "Número de Cuerpos", 20, 0, 0, "cuerpos")); } catch { }
            // Ejes de Coordenadas
 //           try { n.Add(Alerta.DistanciaStart,  new DetallesAlerta(TipoEvento.Jump, "Alejado", "Distancia Alejada", 10, 0, "al separación")); } catch { }
            try { n.Add(Alerta.AcumuladoJump,   new DetallesAlerta(TipoEvento.Jump, "Acumulado", "Mostrar Resumen", 1000, 0, 0, "al acumulados")); } catch { }
            try { n.Add(Alerta.NumeroJump,      new DetallesAlerta(TipoEvento.Jump, "Saltos", "Mostrar Resumen", 10, 0, 0, "saltos acumulados")); } catch { }
            try { n.Add(Alerta.Poblacion,       new DetallesAlerta(TipoEvento.Jump, "Poblacion", "Poblacion", 1, 0, 2, "M habitantes")); } catch { }
        }

        //public Dictionary<Alerta, DetallesAlerta> n;
        public ListaAlertas n;
        public bool isRecord;
        public bool isNew;
        public string recordName = "Record Personal";
        public string recordDesc;

        public double valor;
        public string valorST;

        public bool CumpleCriterios(DetallesAlerta da)
        {
            bool retorno;

            switch (da.tipoParam)
            {
                case TipoParametro.ValorDesde:
                    retorno = (valor >= da.desde);
                    break;
                case TipoParametro.ValorHasta:
                    retorno = (valor <= da.hasta);
                    break;
                case TipoParametro.RangoExcluido:
                    retorno = (valor <= da.desde || valor >= da.hasta);
                    break;
                case TipoParametro.RangoIncluido:
                    retorno = (valor >= da.desde && valor <= da.hasta);
                    break;
                case TipoParametro.None:
                    retorno = true;
                    break;
                default:
                    retorno = false;
                    break;
            }

            isRecord = false;
            isNew = false;
            
            if (retorno && da.tipoParam != TipoParametro.None)
            {
                if (da.tipoParam == TipoParametro.ValorHasta || da.tipoParam == TipoParametro.RangoExcluido)
                    if (valor <= da.recMenor || da.recMenor == null)
                    {
                        if (valor == da.recMenor)
                            recordDesc = "record actual";
                        else
                        {
                            da.recMenor = valor;
                            recordDesc = "nueva marca inferior";
                            isNew = true;
                        }
                        
                        isRecord = true;
                    }
                if (da.tipoParam == TipoParametro.ValorDesde || da.tipoParam == TipoParametro.RangoExcluido)
                    if (valor >= da.recMayor || da.recMayor == null)
                    {
                        if (valor == da.recMayor)
                            recordDesc = "record actual";
                        else
                        {
                            da.recMayor = valor;
                            recordDesc = "nueva marca superior";
                            isNew = true;
                        }

                        isRecord = true;
                    }
            }

            valorST = valor.ToString(da.mask);

            //if (valor == Math.Round(valor, 0)) valorST = $"{valor:N0}";
            //else if (valor == Math.Round(valor, 1)) valorST = $"{valor:N1}";
            //else if (valor == Math.Round(valor, 2)) valorST = $"{valor:N2}";
            //else if (valor == Math.Round(valor, 3)) valorST = $"{valor:N3}";
            //else valorST = $"{valor:N4}";

            if (isRecord)
            {
                recordDesc = recordDesc + " " + valorST;
            }

            return retorno;
        }
    }
}
