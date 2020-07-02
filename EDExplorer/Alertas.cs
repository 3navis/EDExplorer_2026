using System;
using System.Collections.Generic;
using System.Dynamic;
using Newtonsoft.Json;


namespace EDExplorer
{
    using ListaAlertas = Dictionary<Alerta, DetallesAlerta>;
    public enum TipoEvento
    {
        None,
        Scan,
        Signal,
        FSS
    }
    public enum Alerta
    {
        None,
        Terraformable, Atmosfera, 
        CuerpoG, CuerpoP, 
        Anillo,
        AnilloG, AnilloP,
        GravedadP, GravedadG,
        OrbitaP, OrbitaG, OrbitaR, RotacionR,
        Potenciar, Grado5,
        NombreEspecial,
        Crematoria,
        Pastor, Anidada,
        Excentricidad,
        Binario,
        Tritio, LTD, Opal, Painita, Benitoita, Serendibita, Musgravita,
        Geological, Biological,
        BodyCount
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
        public string TipoLog;

        public DetallesAlerta(TipoEvento TE, string NC, string N, double D, double H, string U, string TL = "")
        {
            tipoEvento = TE;
            flag = true;
            nombreCheck = NC;
            nombre = N;
            unidades = U;
            desde = D;
            hasta = H;
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
                tipoParam = TipoParametro.ValorDesde;
            }
            else if (D > H)
            {
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
            settings.vAlertasNew = "v1.04.015";

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
            try { n.Add(Alerta.Atmosfera,       new DetallesAlerta(TipoEvento.Scan, "Aterrizable Atmos.", "Aterrizable con Atmosfera", 0, 0, "")); } catch { }
            try { n.Add(Alerta.CuerpoP,         new DetallesAlerta(TipoEvento.Scan, "Aterrizable Pequeño", "Planeta Pequeño", 0, 300, "Km radio")); } catch { }
            try { n.Add(Alerta.CuerpoG,         new DetallesAlerta(TipoEvento.Scan, "Aterrizable Grande", "Planeta Grande", 18000, 0, "Km radio")); } catch { }
            //try { n.Add(Alerta.Radio,           new DetallesAlerta(TipoEvento.Scan, "Aterrizable Radio", "Aterrizable Radio", 300, 1800, "Km (radio <)",TipoParametro.RangoExcluido)); } catch { }
            
            try { n.Add(Alerta.Anillo,          new DetallesAlerta(TipoEvento.Scan, "Aterrizable Anillado", "Aterrizable con Anillo", 1, 0, "anillos")); } catch { }
            try { n.Add(Alerta.GravedadP,       new DetallesAlerta(TipoEvento.Scan, "Baja Gravedad", "Planeta con Baja Gravedad", 0, 0.028, "g superficie")); } catch { }
            try { n.Add(Alerta.GravedadG,       new DetallesAlerta(TipoEvento.Scan, "Alta Gravedad", "Planeta con Alta Gravedad", 2.75, 0, "g superficie")); } catch { }
            try { n.Add(Alerta.OrbitaP,         new DetallesAlerta(TipoEvento.Scan, "Orbita Cercana", "Orbita Cercana", 0, 1000, "Km separacion")); } catch { }
            try { n.Add(Alerta.OrbitaG,         new DetallesAlerta(TipoEvento.Scan, "Orbita Lejana", "Orbita Lejana", 50000, 0, "SL separacion")); } catch { }
            //try { n.Add(Alerta.Potenciar,       new DetallesAlerta(TipoEvento.Scan, "Potenciar Salto", "Materiales para Potenciar Salto", 5, 0, "material")); } catch { }
            //try { n.Add(Alerta.Grado5,          new DetallesAlerta(TipoEvento.Scan, "Grado 5", "Materiales de Grado 5", 0, 0, "material")); } catch { }
            try { n.Add(Alerta.NombreEspecial,  new DetallesAlerta(TipoEvento.Scan, "Nombre Especial", "Nombre Especial", 0, 0, "")); } catch { }
            // Criterios multiples
            try { n.Add(Alerta.Crematoria,      new DetallesAlerta(TipoEvento.Scan, "Crematoria", "CREMATORIA", 0.5, 0, "dias (rotacion <)")); } catch { }
            //try { n.Add(Alerta.Pastor, new DetallesAlerta(true, "Luna Pastor", "Luna de Pastor", 0, "")); } catch { }
            //try { n.Add(Alerta.Anidada, new DetallesAlerta(true, "Luna Anidada", "Luna Anidada", 0, "")); } catch { }
            try { n.Add(Alerta.RotacionR,       new DetallesAlerta(TipoEvento.Scan, "Rotación Rápida", "Rotación Rápida", 0, 5, "horas rotación")); } catch { }
            try { n.Add(Alerta.OrbitaR,         new DetallesAlerta(TipoEvento.Scan, "Orbita Rápida", "Orbita Rápida", 0, 4, "horas periodo")); } catch { }
            //try { n.Add(Alerta.Excentricidad,   new DetallesAlerta(TipoEvento.Scan, "Orb. Excéntrica", "Orbita Excéntrica", 0.9, 0, "% excentricidad")); } catch { }
            try { n.Add(Alerta.AnilloG,         new DetallesAlerta(TipoEvento.Scan, "Anillo Ancho", "Anillo Ancho", 12, 0, "x veces radio")); } catch { }
            try { n.Add(Alerta.Binario,         new DetallesAlerta(TipoEvento.Scan, "Binaria Cercana", "Pareja Binaria Cercana", 0.5, 0, "radios vs distancia")); } catch { }
            try { n.Add(Alerta.AnilloP,         new DetallesAlerta(TipoEvento.Scan, "Anillo Próximo", "Próximo al Anillo", 0, 500, "km separacion")); } catch { }
            // Señales
            try { n.Add(Alerta.Tritio,          new DetallesAlerta(TipoEvento.Signal, "Veta Tritio", "Veta Tritio", 1, 0, "número vetas", "Tritium")); } catch { }
            try { n.Add(Alerta.LTD,             new DetallesAlerta(TipoEvento.Signal, "Veta LTD", "Veta LTD", 1, 0, "número vetas", "LowTemperatureDiamond")); } catch { }
            try { n.Add(Alerta.Opal,            new DetallesAlerta(TipoEvento.Signal, "Veta Ópalos", "Veta Ópalos", 1, 0, "número vetas", "Opal")); } catch { }
            try { n.Add(Alerta.Painita,         new DetallesAlerta(TipoEvento.Signal, "Veta Painíta", "Veta Painíta", 1, 0, "número vetas", "Painite")); } catch { }
            try { n.Add(Alerta.Benitoita,       new DetallesAlerta(TipoEvento.Signal, "Veta Benitoíta", "Veta Benitoíta", 1, 0, "número vetas", "Benitoite")); } catch { }
            try { n.Add(Alerta.Serendibita,     new DetallesAlerta(TipoEvento.Signal, "Veta Serendibíta", "Veta Serendibíta", 1, 0, "número vetas", "Serendibite")); } catch { }
            try { n.Add(Alerta.Musgravita,      new DetallesAlerta(TipoEvento.Signal, "Veta Musgravíta", "Veta Musgravíta", 1, 0, "número vetas", "Musgravite")); } catch { }
            try { n.Add(Alerta.Geological,      new DetallesAlerta(TipoEvento.Signal, "Geológica", "Geológica", 1, 0, "número señales")); } catch { }
            try { n.Add(Alerta.Biological,      new DetallesAlerta(TipoEvento.Signal, "Biológica", "Biológica", 1, 0, "número señales")); } catch { }
            // FSS
            try { n.Add(Alerta.BodyCount,       new DetallesAlerta(TipoEvento.FSS, "FSS Cuerpos", "Número de Cuerpos", 20, 0, "cuerpos")); } catch { }
        }

        //public Dictionary<Alerta, DetallesAlerta> n;
        public ListaAlertas n;
        public bool newRecordMenor;
        public bool newRecordMayor;
        public string recordDesc;

        public bool CumpleCriterios(DetallesAlerta da, double valor)
        {
            bool retorno = false;

            switch (da.tipoParam)
            {
                case TipoParametro.ValorDesde:
                    if (valor >= da.desde)
                    { retorno = true; }
                    break;
                case TipoParametro.ValorHasta:
                    if (valor <= da.hasta)
                    { retorno = true; }
                    break;
                case TipoParametro.RangoExcluido:
                    if (valor <= da.desde || valor >= da.hasta)
                    { retorno = true; }
                    break;
                case TipoParametro.RangoIncluido:
                    if (valor >= da.desde && valor <= da.hasta)
                    { retorno = true; }
                    break;
                case TipoParametro.None:
                    retorno = true;
                    break;
                default:
                    break;
            }

            newRecordMenor = false;
            newRecordMayor = false;
            
            if (retorno && da.tipoParam != TipoParametro.None)
            {
                if (da.tipoParam == TipoParametro.ValorHasta || da.tipoParam == TipoParametro.RangoExcluido)
                    if (valor < da.recMenor || da.recMenor == null)
                    {
                        da.recMenor = valor;
                        newRecordMenor = true;
                        recordDesc = "nuevo limite inferior";
                    }
                if (da.tipoParam == TipoParametro.ValorDesde || da.tipoParam == TipoParametro.RangoExcluido)
                    if (valor > da.recMayor || da.recMayor == null)
                    {
                        da.recMayor = valor;
                        newRecordMayor = true;
                        recordDesc = "nuevo limite superior";
                    }
                if (newRecordMenor && newRecordMayor)
                {
                    recordDesc = "primer registro";
                }
            }

            return retorno;
        }
    }
}
