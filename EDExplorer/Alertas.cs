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

        public DetallesAlerta(TipoEvento TE, string NC, string N, double D, double H, string U, TipoParametro TP = TipoParametro.RangoIncluido)
        {
            tipoEvento = TE;
            flag = true;
            nombreCheck = NC;
            nombre = N;
            unidades = U;
            desde = D;
            hasta = H;
            tipoParam = TP;
            recMenor = null;
            recMayor = null;
            
            if (D == 0 && H == 0)
            {
                tipoParam = TipoParametro.None;
            }
            if (D == 0 && H != 0)
            {
                desde = H;
                hasta = 0;
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
        }
    }
    public class Alertas
    {
        private readonly Properties.Settings settings = Properties.Settings.Default;
        public Alertas()
        {
            settings.vAlertasNew = "v1.04.010";

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
            try { n.Add(Alerta.Terraformable,   new DetallesAlerta(TipoEvento.Scan, "Aterrizable Terra.", "Aterrizable y Terraformable", 0, 0, "")); } catch { }
            try { n.Add(Alerta.Atmosfera,       new DetallesAlerta(TipoEvento.Scan, "Aterrizable Atmos.", "Aterrizable con Atmosfera", 0, 0, "")); } catch { }
            try { n.Add(Alerta.CuerpoP,         new DetallesAlerta(TipoEvento.Scan, "Aterrizable Pequeño", "Planeta Pequeño", 0, 300, "Km radio")); } catch { }
            try { n.Add(Alerta.CuerpoG,         new DetallesAlerta(TipoEvento.Scan, "Aterrizable Grande", "Planeta Grande", 18000, 0, "Km radio")); } catch { }
            //try { n.Add(Alerta.Radio,           new DetallesAlerta(TipoEvento.Scan, "Aterrizable Radio", "Aterrizable Radio", 300, 1800, "Km (radio <)",TipoParametro.RangoExcluido)); } catch { }
            
            try { n.Add(Alerta.Anillo,          new DetallesAlerta(TipoEvento.Scan, "Aterrizable Anillado", "Aterrizable con Anillo", 1, 0, "anillos")); } catch { }
            try { n.Add(Alerta.GravedadP,       new DetallesAlerta(TipoEvento.Scan, "Baja Gravedad", "Planeta con Baja Gravedad", 0, 0.028, "g superficie")); } catch { }
            try { n.Add(Alerta.GravedadG,       new DetallesAlerta(TipoEvento.Scan, "Alta Gravedad", "Planeta con Alta Gravedad", 2.75, 0, "g superficie")); } catch { }
            try { n.Add(Alerta.OrbitaP,         new DetallesAlerta(TipoEvento.Scan, "Orbita Cercana", "Orbita Cercana", 0, 1000, "Km separacion")); } catch { }
            try { n.Add(Alerta.OrbitaG,         new DetallesAlerta(TipoEvento.Scan, "Orbita Lejana", "Orbita Lejana", 50000, 0, "SL separacion")); } catch { }
            try { n.Add(Alerta.Potenciar,       new DetallesAlerta(TipoEvento.Scan, "Potenciar Salto", "Materiales para Potenciar Salto", 5, 0, "material")); } catch { }
            try { n.Add(Alerta.Grado5,          new DetallesAlerta(TipoEvento.Scan, "Grado 5", "Materiales de Grado 5", 0, 0, "material")); } catch { }
            try { n.Add(Alerta.NombreEspecial,  new DetallesAlerta(TipoEvento.Scan, "Nombre Especial", "Nombre Especial", 0, 0, "")); } catch { }
            // Criterios multiples
            try { n.Add(Alerta.Crematoria,      new DetallesAlerta(TipoEvento.Scan, "Crematoria", "CREMATORIA", 0.5, 0, "dias (rotacion <)")); } catch { }
            //try { n.Add(Alerta.Pastor, new DetallesAlerta(true, "Luna Pastor", "Luna de Pastor", 0, "")); } catch { }
            //try { n.Add(Alerta.Anidada, new DetallesAlerta(true, "Luna Anidada", "Luna Anidada", 0, "")); } catch { }
            try { n.Add(Alerta.RotacionR,       new DetallesAlerta(TipoEvento.Scan, "Rotación Rápida", "Rotación Rápida", 0, 5, "horas rotación")); } catch { }
            try { n.Add(Alerta.OrbitaR,         new DetallesAlerta(TipoEvento.Scan, "Orbita Rápida", "Orbita Rápida", 0, 4, "horas periodo")); } catch { }
            try { n.Add(Alerta.Excentricidad,   new DetallesAlerta(TipoEvento.Scan, "Orb. Excéntrica", "Orbita Excéntrica", 0.9, 0, "% excentricidad")); } catch { }
            try { n.Add(Alerta.AnilloG,         new DetallesAlerta(TipoEvento.Scan, "Anillo Ancho", "Anillo Ancho", 12, 0, "x veces radio")); } catch { }
            try { n.Add(Alerta.Binario,         new DetallesAlerta(TipoEvento.Scan, "Binaria Cercana", "Pareja Binaria Cercana", 2, 0, "radios x veces Eje")); } catch { }
            try { n.Add(Alerta.AnilloP,         new DetallesAlerta(TipoEvento.Scan, "Anillo Próximo", "Próximo al Anillo", 0, 500, "km separacion")); } catch { }
            // Señales
            try { n.Add(Alerta.Tritio,          new DetallesAlerta(TipoEvento.Signal, "Veta Tritio", "Veta Tritio", 1, 0, "número vetas")); } catch { }
            try { n.Add(Alerta.LTD,             new DetallesAlerta(TipoEvento.Signal, "Veta LTD", "Veta LTD", 1, 0, "número vetas")); } catch { }
            try { n.Add(Alerta.Opal,            new DetallesAlerta(TipoEvento.Signal, "Veta Ópalos", "Veta Ópalos", 1, 0, "número vetas")); } catch { }
            try { n.Add(Alerta.Painita,         new DetallesAlerta(TipoEvento.Signal, "Veta Painita", "Veta Painita", 1, 0, "número vetas")); } catch { }
            try { n.Add(Alerta.Benitoita,       new DetallesAlerta(TipoEvento.Signal, "Veta Benitoita", "Veta Benitoita", 1, 0, "número vetas")); } catch { }
            try { n.Add(Alerta.Serendibita,     new DetallesAlerta(TipoEvento.Signal, "Veta Serendibita", "Veta Serendibita", 1, 0, "número vetas")); } catch { }
            try { n.Add(Alerta.Musgravita,      new DetallesAlerta(TipoEvento.Signal, "Veta Musgravita", "Veta Musgravita", 1, 0, "número vetas")); } catch { }
            try { n.Add(Alerta.Geological,      new DetallesAlerta(TipoEvento.Signal, "Geológica", "Geológica", 1, 0, "número señales")); } catch { }
            try { n.Add(Alerta.Biological,      new DetallesAlerta(TipoEvento.Signal, "Biológica", "Biológica", 1, 0, "número señales")); } catch { }
            // FSS
            try { n.Add(Alerta.BodyCount,       new DetallesAlerta(TipoEvento.FSS, "FSS Cuerpos", "Número de Cuerpos", 20, 0, "cuerpos")); } catch { }
        }

        //public Dictionary<Alerta, DetallesAlerta> n;
        public ListaAlertas n;
        public bool newRecordMenor;
        public bool newRecordMayor;

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
                    if (valor <= da.desde)
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

            if (retorno)
            {
                newRecordMenor = false;
                newRecordMayor = false;

                if (valor < da.recMenor || da.recMenor == null)
                {
                    da.recMenor = valor;
                    newRecordMenor = true;
                }
                if (valor > da.recMayor || da.recMayor == null)
                {
                    da.recMayor = valor;
                    newRecordMayor = true;
                }
            }

            return retorno;
        }
    }

    //private class DetallesRecord
    //{
    //    public double menor;
    //    public double mayor;
    //    public DetallesRecord(double D, double H)
    //    {
    //        menor = D;
    //        mayor = H;
    //    }
    //}
    //public class Records
    //{
    //    public Dictionary<Alerta, DetallesRecord> n;

    //    private readonly Properties.Settings settings = Properties.Settings.Default;
    //    public Records()
    //    {
    //        settings.vRecordsNew = "v1.01.002";

    //        if (settings.vRecords != settings.vRecordsNew)
    //        {
    //            n = new Dictionary<Alerta, DetallesRecord>();
    //            //setDefault();

    //            settings.Records = JsonConvert.SerializeObject(n);
    //            settings.vRecords = settings.vRecordsNew;
    //            settings.Save();
    //        }
    //        else
    //        {
    //            try
    //            {
    //                n = JsonConvert.DeserializeObject<Dictionary<Alerta, DetallesRecord>>(settings.Records);
    //            }
    //            catch { }

    //            if (n == null) n = new Dictionary<Alerta, DetallesRecord>();

    //            //setDefault();
    //        }
    //    }

    //    public bool NuevoRecord(Alerta A, double valor)
    //    {
    //        bool newRecord = false;

    //        try
    //        {
    //            if (valor < n[A].menor) 
    //            {
    //                n[A].menor = valor;
    //                newRecord = true;
    //            }
    //            if (valor > n[A].mayor)
    //            {
    //                n[A].mayor = valor;
    //                newRecord = true;
    //            }

    //        }
    //        catch (KeyNotFoundException)
    //        {
    //            n.Add(A, new DetallesRecord(valor, valor));
    //            newRecord = true;
    //        }

    //        return newRecord;
    //    }
    //}
}
