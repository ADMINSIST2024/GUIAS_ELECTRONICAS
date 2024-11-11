using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace TecnologiaTextil.FacturacionClientXP
{
    public static class Utils
    {
        private static string carpeta = ConfigurationManager.AppSettings["PathLogs"].ToString();
        private static string carpetaPDF = ConfigurationManager.AppSettings["PathPDF"].ToString();
        private static bool printLog = Boolean.Parse(ConfigurationManager.AppSettings["OnLog"].ToString());
        public static string RutaPDF()
        {
            return carpetaPDF;
        }
        public static string RutaLog()
        {
            return carpeta;
        }
        public static void Log(string texto)
        {
            if (printLog)
            {
                if (!Directory.Exists(carpeta))
                    Directory.CreateDirectory(carpeta);

                string ruta = carpeta;
                ruta = string.Format("{0}{1}XP.txt", ruta, DateTime.Today.ToShortDateString().Replace("/", ""));

                using (StreamWriter escri = new StreamWriter(ruta, true))
                {
					Console.WriteLine(texto);
                    escri.WriteLine(DateTime.Now.ToLongTimeString() + "> " + texto);
                    escri.Flush();
                    escri.Close();
                }

            }

        }
    }
}
