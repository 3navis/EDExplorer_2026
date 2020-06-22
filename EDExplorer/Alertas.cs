using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using Newtonsoft.Json;

namespace EDExplorer
{
    public enum Alerta
    {
        None,
        Terraformable, Atmosfera, CuerpoG, CuerpoP, Anillo,
        AnilloG, AnilloP,
        GravedadP, GravedadG,
        OrbitaP, OrbitaG, OrbitaR, RotacionR,
        Potenciar, Grado5,
        NombreEspecial,
        Crematoria,
        Pastor, Anidada,
        Excentricidad,
        Binario,
        Tritio, LTD, Opal,
        Geological, Biological
    }
    public class DetallesAlerta
    {
        public DetallesAlerta(bool F, string T, string N, double D, string U)
        {
            flag = F;
            tipo = T;
            nombre = N;
            desde = D;
            unidades = U;
        }

        public bool flag;
        public string tipo;
        public string nombre;
        public double desde;
        public string unidades;
    }
    public class Alertas
    {
        private readonly Properties.Settings settings = Properties.Settings.Default;
        public Alertas()
        {
            try
            {
                n = JsonConvert.DeserializeObject<Dictionary<Alerta, DetallesAlerta>>(settings.Alertas);
            }
            catch { }

            if (n == null)
                n = new Dictionary<Alerta, DetallesAlerta>();

            //n = new Dictionary<Alerta, DetallesAlerta>();
            setDefault();
        }
        public void setDefault()
        {
            try { n.Add(Alerta.Terraformable, new DetallesAlerta(true, "Aterrizable Terra.", "Aterrizable y Terraformable", 0, "")); } catch { }
            try { n.Add(Alerta.Atmosfera, new DetallesAlerta(true, "Aterrizable Atmos.", "Aterrizable con Atmosfera", 0, "")); } catch { }
            try { n.Add(Alerta.CuerpoG, new DetallesAlerta(true, "Aterrizable Grande", "Aterrizable de Gran Tamaño", 18000, "Km (> radio)")); } catch { }
            try { n.Add(Alerta.CuerpoP, new DetallesAlerta(true, "Aterrizable Pequeño", "Aterrizable de Pequeño Tamaño", 300, "Km (> radio)")); } catch { }
            try { n.Add(Alerta.Anillo, new DetallesAlerta(true, "Aterrizable Anillado", "Aterrizable con Anillo", 1, "")); } catch { }
            try { n.Add(Alerta.GravedadP, new DetallesAlerta(true, "Baja Gravedad", "Aterrizable con Baja Gravedad", 0.028, "g (> g superficie)")); } catch { }
            try { n.Add(Alerta.GravedadG, new DetallesAlerta(true, "Alta Gravedad", "Aterrizable con Alta Gravedad", 2.75, "g (> g superficie)")); } catch { }
            try { n.Add(Alerta.OrbitaP, new DetallesAlerta(true, "Orbita Cercana", "Orbita Cercana", 1000, "Km (< separacion)")); } catch { }
            try { n.Add(Alerta.OrbitaG, new DetallesAlerta(true, "Orbita Lejana", "Orbita Lejana", 50000, "sl (> separacion)")); } catch { }
            try { n.Add(Alerta.Potenciar, new DetallesAlerta(true, "Potenciar Salto", "Materiales para Potenciar Salto", 5, "material")); } catch { }
            try { n.Add(Alerta.Grado5, new DetallesAlerta(true, "Grado 5", "Materiales de Grado 5", 0, "material")); } catch { }
            try { n.Add(Alerta.NombreEspecial, new DetallesAlerta(true, "Nombre Especial", "Nombre Especial", 0, "nombre")); } catch { }
            // Criterios multiples
            try { n.Add(Alerta.Crematoria, new DetallesAlerta(true, "Crematoria", "CREMATORIA", 0.5, "dias (> rotacion)")); } catch { }
            try { n.Add(Alerta.Pastor, new DetallesAlerta(true, "Luna Pastor", "Luna de Pastor", 0, "")); } catch { }
            try { n.Add(Alerta.Anidada, new DetallesAlerta(true, "Luna Anidada", "Luna Anidada", 0, "")); } catch { }
            try { n.Add(Alerta.RotacionR, new DetallesAlerta(true, "Rotación Rápida", "Rotación Rápida", 5, "horas (< rotación)")); } catch { }
            try { n.Add(Alerta.OrbitaR, new DetallesAlerta(true, "Orbita Rápida", "Orbita Rápida", 4, "horas (< periodo)")); } catch { }
            try { n.Add(Alerta.Excentricidad, new DetallesAlerta(true, "Orb. Excéntrica", "Orbita Excéntrica", 0.9, "(> excentricidad)")); } catch { }
            try { n.Add(Alerta.AnilloG, new DetallesAlerta(true, "Anillo Ancho", "Anillo Ancho", 12, "x veces radio")); } catch { }
            try { n.Add(Alerta.Binario, new DetallesAlerta(true, "Binaria Cercana", "Pareja Binaria Cercana", 2, "radios x veces Eje")); } catch { }
            try { n.Add(Alerta.AnilloP, new DetallesAlerta(true, "Anillo Próximo", "Próximo al Anillo", 500, "km (< separacion)")); } catch { }
            // Señales
            try { n.Add(Alerta.Tritio, new DetallesAlerta(true, "Veta Tritio", "Veta Tritio", 0, "vetas (< numero vetas)")); } catch { }
            try { n.Add(Alerta.LTD, new DetallesAlerta(true, "Veta LTD", "Veta LTD", 0, "vetas (< numero vetas)")); } catch { }
            try { n.Add(Alerta.Opal, new DetallesAlerta(true, "Veta Ópalos", "Veta Ópalos", 0, "vetas (< numero vetas)")); } catch { }
            try { n.Add(Alerta.Geological, new DetallesAlerta(true, "Geológica", "Geológica", 0, "señales (< numero)")); } catch { }
            try { n.Add(Alerta.Biological, new DetallesAlerta(true, "Biológica", "Biológica", 0, "señales (< numero)")); } catch { }
        }

        public Dictionary<Alerta, DetallesAlerta> n;
    }

    /// <summary>
    /// Instanciar una Fuente no instalada
    /// </summary>
    public class FontElite
    {
        public static PrivateFontCollection private_fonts = new PrivateFontCollection();
        public FontElite()
        {
            // Use this if you can not find your resource System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceNames();
            string resource = "EDExplorer.Resources.elitedanger.ttf";
            // receive resource stream
            Stream fontStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource);

            //create an unsafe memory block for the data
            System.IntPtr data = Marshal.AllocCoTaskMem((int)fontStream.Length);
            //create a buffer to read in to
            Byte[] fontData = new Byte[fontStream.Length];
            //fetch the font program from the resource
            fontStream.Read(fontData, 0, (int)fontStream.Length);
            //copy the bytes to the unsafe memory block
            Marshal.Copy(fontData, 0, data, (int)fontStream.Length);

            // We HAVE to do this to register the font to the system (Weird .NET bug !)
            //uint cFonts = 0;
            //AddFontMemResourceEx(data, (uint)fontData.Length, IntPtr.Zero, ref cFonts);

            //pass the font to the font collection
            private_fonts.AddMemoryFont(data, (int)fontStream.Length);
            //close the resource stream
            fontStream.Close();
            //free the unsafe memory
            Marshal.FreeCoTaskMem(data);
        }
    }
}
