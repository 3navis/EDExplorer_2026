using Newtonsoft.Json;
using System.Collections.Generic;

namespace EDExplorer
{
    using ListaAlertas = Dictionary<Alerta, DetallesAlerta>;
    using M = Properties.Textos;

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
            settings.vAlertasNew = "v1.06.003";

            if (settings.vAlertas != settings.vAlertasNew)
            {
                // Si se cambia la version se resetean todas las alertas
                // Solo hay que hacerlo si cambia la estructura de las mismas
                //  o si se cambia el idioma

                Reset();
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
        public void Reset()
        {
            // Reinicio de textos y Records (almacena)
            n = new ListaAlertas();
            setDefault();

            settings.Alertas = JsonConvert.SerializeObject(n);
            settings.vAlertas = settings.vAlertasNew;
            settings.Save();
        }
        public void setDefault()
        {
            //try { n.Add(Alerta.Terraformable,   new DetallesAlerta(TipoEvento.Scan, "Aterrizable Terra.", "Aterrizable y Terraformable", 0, 0, "")); } catch { }
            try { n.Add(Alerta.Atmosfera,       new DetallesAlerta(TipoEvento.Scan, M.str_Aterrizable_Atmos, M.str_Aterrizable_Atmosf_rico, 0, 0, -1, "","")); } catch { }
            try { n.Add(Alerta.CuerpoP,         new DetallesAlerta(TipoEvento.Scan, M.str_Aterrizable_Peque, M.str_Aterrizable_Peque, 0, 300, 0, M.str_Km_radio, M.str_Km_de_Radio)); } catch { }
            try { n.Add(Alerta.CuerpoG,         new DetallesAlerta(TipoEvento.Scan, M.str_Aterrizable_Grande, M.str_Aterrizable_Grande, 18000, 0, 0, M.str_Km_radio, M.str_Km_de_Radio)); } catch { }
            //try { n.Add(Alerta.Radio,           new DetallesAlerta(TipoEvento.Scan, "Aterrizable Radio", "Aterrizable Radio", 300, 1800, "Km (radio <)",TipoParametro.RangoExcluido)); } catch { }
            
            try { n.Add(Alerta.Anillo,          new DetallesAlerta(TipoEvento.Scan, M.str_Aterrizable_Anillado, M.str_Aterrizable_Anillado, 1, 0, 0, M.str_Km_ancho, M.str_Km_anchura_anillo)); } catch { }
            try { n.Add(Alerta.GravedadP,       new DetallesAlerta(TipoEvento.Scan, M.str_Baja_Gravedad, M.str_Aterrizable_con_Baja_Gravedad, 0, 0.028, 4, M.str_g_superficie, M.str_Gravedad_en_superficie)); } catch { }
            try { n.Add(Alerta.GravedadG,       new DetallesAlerta(TipoEvento.Scan, M.str_Alta_Gravedad, M.str_Aterrizable_con_Alta_Gravedad, 2.75, 0, 2, M.str_g_superficie, M.str_Gravedad_en_superficie)); } catch { }
            try { n.Add(Alerta.OrbitaP,         new DetallesAlerta(TipoEvento.Scan, M.str_Orbita_Cercana, M.str_Orbita_Cercana, 0, 1000, 0, M.str_Km_separacion, M.str_Km_de_distancia_entre_superficies)); } catch { }
            try { n.Add(Alerta.OrbitaG,         new DetallesAlerta(TipoEvento.Scan, M.str_Orbita_Lejana, M.str_Orbita_Lejana, 50000, 0, 0, M.str_SL_separacion, M.str_Km_de_distancia_entre_superficies)); } catch { }
            //try { n.Add(Alerta.Potenciar,       new DetallesAlerta(TipoEvento.Scan, "Potenciar Salto", "Materiales para Potenciar Salto", 5, 0, "material")); } catch { }
            //try { n.Add(Alerta.Grado5,          new DetallesAlerta(TipoEvento.Scan, "Grado 5", "Materiales de Grado 5", 0, 0, "material")); } catch { }
                try { n.Add(Alerta.NombreEspecial,  new DetallesAlerta(TipoEvento.Scan, M.str_Nombre_Especial, M.str_Nombre_Especial, 0, 0, -1, "", M.str_El_nombre_del_cuerpo_no_incluye_al)); } catch { }
            // Criterios multiples
            try { n.Add(Alerta.Crematoria,      new DetallesAlerta(TipoEvento.Scan, M.str_Crematoria1, M.str_CREMATORIA, 0, 0.8, 2, M.str_dias_rotaci, M.str_dias_rotaci)); } catch { }
            //try { n.Add(Alerta.Pastor, new DetallesAlerta(true, "Luna Pastor", "Luna de Pastor", 0, "")); } catch { }
            //try { n.Add(Alerta.Anidada, new DetallesAlerta(true, "Luna Anidada", "Luna Anidada", 0, "")); } catch { }
                try { n.Add(Alerta.RotacionR,       new DetallesAlerta(TipoEvento.Scan, M.str_Rotaci_pida, M.str_Rotaci_pida, 0, 5, 1, M.str_horas_rotaci, M.str_horas_para_completar_una_rotaci)); } catch { }
            try { n.Add(Alerta.OrbitaR,         new DetallesAlerta(TipoEvento.Scan, M.str_Orbita_pida, M.str_Orbita_pida, 0, 4, 1, M.str_horas_orbita, M.str_horas_para_completar_una_Orbita)); } catch { }
            //try { n.Add(Alerta.Excentricidad,   new DetallesAlerta(TipoEvento.Scan, "Orb. Excéntrica", "Orbita Excéntrica", 0.9, 0, "% excentricidad")); } catch { }
            try { n.Add(Alerta.AnilloG,         new DetallesAlerta(TipoEvento.Scan, M.str_Anillo_Ancho, M.str_Anillo_Ancho, 12, 0, 1, M.str_veces_radio, M.str_veces_el_radio_del_Planeta)); } catch { }
            try { n.Add(Alerta.Binario,         new DetallesAlerta(TipoEvento.Scan, M.str_Binaria_Cercana, M.str_Binaria_Cercana, 0.5, 0, 2, M.str_radio_vs_distancia, M.str_relaci_Radio_vs_Distancia)); } catch { }
            try { n.Add(Alerta.AnilloP,         new DetallesAlerta(TipoEvento.Scan, M.str_Anillo_Pr_ximo, M.str_Pr_ximo_al_Anillo, 0, 500, 0, M.str_Km_separacion, M.str_Km_de_distancia_al_borde_del_anillo)); } catch { }

            // Diferentes tipos de Anillo (eliminado Belt)
            try { n.Add(Alerta.AnilloIcy,       new DetallesAlerta(TipoEvento.Scan, M.str_Anillo_Helado, M.str_Anillo_Helado, -1, 0, 4, M.str_Mt_masa, M.str_Mt_de_masa)); } catch { }
            try { n.Add(Alerta.AnilloRock,      new DetallesAlerta(TipoEvento.Scan, M.str_Anillo_Rocoso, M.str_Anillo_Rocoso, -1, 0, 4, M.str_Mt_masa, M.str_Mt_de_masa)); } catch { }
            try { n.Add(Alerta.AnilloMetal,     new DetallesAlerta(TipoEvento.Scan, M.str_Anillo_Met_lico, M.str_Anillo_Met_lico, -1, 0, 4, M.str_Mt_masa, M.str_Mt_de_masa)); } catch { }
            try { n.Add(Alerta.AnilloMetalRich, new DetallesAlerta(TipoEvento.Scan, M.str_Anillo_Rico_Metal, M.str_Anillo_Rico_en_Metal, -1, 0, 4, M.str_Mt_masa, M.str_Mt_de_masa)); } catch { }

            // Planetas interesantes
            try { n.Add(Alerta.Tierra,          new DetallesAlerta(TipoEvento.Scan, M.str_tipo_Tierra, M.str_tipo_Tierra, 0, 0, -1, "", "")); } catch { }
            try { n.Add(Alerta.Acuatico,        new DetallesAlerta(TipoEvento.Scan, M.str_tipo_Acuatico, M.str_tipo_Acuatico, 0, 0, -1, "", "")); } catch { }
            try { n.Add(Alerta.Amoniaco,        new DetallesAlerta(TipoEvento.Scan, M.str_tipo_Amoniaco, M.str_tipo_Amoniaco, 0, 0, -1, "", "")); } catch { }

            // Señales
            try { n.Add(Alerta.Tritio,          new DetallesAlerta(TipoEvento.Signal, M.str_Veta_Tritio, M.str_Veta_Tritio, 1, 0, 0, M.str_mero_vetas, M.str_vetas, "tritium")); } catch { }
            try { n.Add(Alerta.LTD,             new DetallesAlerta(TipoEvento.Signal, M.str_Veta_LTD, M.str_Veta_LTD, 1, 0, 0, M.str_mero_vetas, M.str_vetas, "LowTemperatureDiamond")); } catch { }
            try { n.Add(Alerta.Opal,            new DetallesAlerta(TipoEvento.Signal, M.str_Veta_palos, M.str_Veta_palos, 1, 0, 0, M.str_mero_vetas, M.str_vetas, "Opal")); } catch { }
            try { n.Add(Alerta.Painita,         new DetallesAlerta(TipoEvento.Signal, M.str_Veta_Pain_ta, M.str_Veta_Pain_ta, 1, 0, 0, M.str_mero_vetas, M.str_vetas, "Painite")); } catch { }
            try { n.Add(Alerta.Benitoita,       new DetallesAlerta(TipoEvento.Signal, M.str_Veta_Benito_ta, M.str_Veta_Benito_ta, 1, 0, 0, M.str_mero_vetas, M.str_vetas, "Benitoite")); } catch { }
            try { n.Add(Alerta.Serendibita,     new DetallesAlerta(TipoEvento.Signal, M.str_Veta_Serendib_ta, M.str_Veta_Serendib_ta, 1, 0, 0, M.str_mero_vetas, M.str_vetas, "Serendibite")); } catch { }
            try { n.Add(Alerta.Musgravita,      new DetallesAlerta(TipoEvento.Signal, M.str_Veta_Musgrav_ta, M.str_Veta_Musgrav_ta, 1, 0, 0, M.str_mero_vetas, M.str_vetas, "Musgravite")); } catch { }
            try { n.Add(Alerta.Alejandrita,     new DetallesAlerta(TipoEvento.Signal, M.str_Veta_Alejandr_ta, M.str_Veta_Alejandr_ta, 1, 0, 0, M.str_mero_vetas, M.str_vetas, "Alexandrite")); } catch { }
            try { n.Add(Alerta.Monacita,        new DetallesAlerta(TipoEvento.Signal, M.str_Veta_Grandidier_ta, M.str_Veta_Grandidier_ta, 1, 0, 0, M.str_mero_vetas, M.str_vetas, "Grandidierite")); } catch { }
            try { n.Add(Alerta.Grandidierita,   new DetallesAlerta(TipoEvento.Signal, M.str_Veta_Monacita, M.str_Veta_Monacita, 1, 0, 0, M.str_mero_vetas, M.str_vetas, "Monazite")); } catch { }
            try { n.Add(Alerta.Rhodplumsita,    new DetallesAlerta(TipoEvento.Signal, M.str_Veta_Rhodplumsita, M.str_Veta_Rhodplumsita, 1, 0, 0, M.str_mero_vetas, M.str_vetas, "Rhodplumsite")); } catch { }
            
            try { n.Add(Alerta.Geological,      new DetallesAlerta(TipoEvento.Signal, M.str_Se_al_Geol_gica, M.str_Geol_gica, 1, 0, 0, M.str_mero_se_ales, M.str_se_ales, "$SAA_SignalType_Geological;")); } catch { }
            try { n.Add(Alerta.Biological,      new DetallesAlerta(TipoEvento.Signal, M.str_Se_al_Biol_gica, M.str_Biol_gica, 1, 0, 0, M.str_mero_se_ales, M.str_se_ales, "$SAA_SignalType_Biological;")); } catch { }
            try { n.Add(Alerta.Human,           new DetallesAlerta(TipoEvento.Signal, M.str_Se_al_Humana, M.str_Humana, 1, 0, 0, M.str_mero_se_ales, M.str_se_ales, "$SAA_SignalType_Human;")); } catch { }
            try { n.Add(Alerta.Guardian,        new DetallesAlerta(TipoEvento.Signal, M.str_Se_al_Guardian, M.str_Guardian, 1, 0, 0, M.str_mero_se_ales, M.str_se_ales, "$SAA_SignalType_Guardian;")); } catch { }
            try { n.Add(Alerta.Thargoid,        new DetallesAlerta(TipoEvento.Signal, M.str_Se_al_Thargoide, M.str_Thargoide, 1, 0, 0, M.str_mero_se_ales, M.str_se_ales, "$SAA_SignalType_Thargoid;")); } catch { }
            
            // FSS
            try { n.Add(Alerta.BodyCount,       new DetallesAlerta(TipoEvento.FSS, M.str_FSS_Cuerpos, M.str_mero_de_Cuerpos, 20, 0, 0, M.str_cuerpos, "")); } catch { }
            // Ejes de Coordenadas
 //           try { n.Add(Alerta.DistanciaStart,  new DetallesAlerta(TipoEvento.Jump, "Alejado", "Distancia Alejada", 10, 0, "al separación")); } catch { }
            try { n.Add(Alerta.AcumuladoJump,   new DetallesAlerta(TipoEvento.Jump, M.str_Acumulado, M.str_Mostrar_Resumen, 1000, 0, 0, M.str_al_acumulados, M.str_al_de_distancia_acumulada_en_saltos)); } catch { }
            try { n.Add(Alerta.NumeroJump,      new DetallesAlerta(TipoEvento.Jump, M.str_Saltos, M.str_Mostrar_Resumen, 10, 0, 0, M.str_saltos_acumulados, M.str_saltos_acumulados)); } catch { }
            try { n.Add(Alerta.Poblacion,       new DetallesAlerta(TipoEvento.Jump, M.str_Poblacion, M.str_Poblacion, 1, 0, 2, M.str_habitantes, M.str_habitantes)); } catch { }
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
                                    recordDesc = M.str_record_actual;
                                else
                                {
                                    da.recMenor = valor;
                                    recordDesc = M.str_record_inferior;
                                    isRecordNew = true;
                                }

                                isRecord = true;
                            }
                            else
                            {
                                superiorMediaST = M.str_inferior_la_media;
                            }
                        }

                    if (da.tipoParam == TipoParametro.ValorDesde || da.tipoParam == TipoParametro.RangoExcluido)
                        if (valor > da.MediaRecord || da.MediaRecord == null)
                        {
                            if (valor >= da.recMayor || da.recMayor == null)
                            {
                                if (valor == da.recMayor)
                                    recordDesc = M.str_record_actual;
                                else
                                {
                                    da.recMayor = valor;
                                    recordDesc = M.str_record_superior;
                                    isRecordNew = true;
                                }

                                isRecord = true;
                            }
                            else
                            {
                                superiorMediaST = M.str_superior_la_media;
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
