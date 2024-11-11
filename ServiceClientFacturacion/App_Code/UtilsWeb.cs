using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Utils
/// </summary>
public static class UtilsWeb
{
    private static string carpeta = ConfigurationManager.AppSettings["PathLogs"].ToString();
    private static bool printLog = Boolean.Parse(ConfigurationManager.AppSettings["OnLog"].ToString());
  
    public static void Log(string texto)
    {
        if (printLog)
        {
            if (!Directory.Exists(carpeta))
                Directory.CreateDirectory(carpeta);

            string ruta = carpeta;
            ruta = string.Format("{0}{1}Services.txt", ruta, DateTime.Today.ToShortDateString().Replace("/", ""));

            using (StreamWriter escri = new StreamWriter(ruta, true))
            {
                escri.WriteLine(DateTime.Now.ToLongTimeString() + "> " + texto);
                escri.Flush();
                escri.Close();
            }

        }

    }
}