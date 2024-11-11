using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecnologiaTextil.FacturacionClient
{
	public static class Utils
	{
		public enum LevelLog
		{
			All = 5,
			DEBUG = 4,
			INFO = 3,
			WARN = 2,
			ERROR = 1,
			FATAL = 0
		}
		private static string carpetaroot = ConfigurationManager.AppSettings["PathLogs"].ToString();
		private static bool printLog = Boolean.Parse(ConfigurationManager.AppSettings["OnLog"].ToString());
		private static bool logfordocument = Boolean.Parse(ConfigurationManager.AppSettings["LogForDocument"].ToString());

		private static string valor = ConfigurationManager.AppSettings["LevelLog"].ToString();

		private static LevelLog nivel = ((LevelLog)Enum.Parse(typeof(LevelLog), valor));
		private static int nivelLog = (int)nivel;

		public static bool PingEfact()
		{
			bool retorno = false;
			System.Net.NetworkInformation.Ping Pings = new System.Net.NetworkInformation.Ping();
			int timeout = 2000;

			if (Pings.Send("ose.efact.pe", timeout).Status == System.Net.NetworkInformation.IPStatus.Success)
			{
				Utils.Informacion("Servicios Efact en linea");
				retorno = true;
			}
			else
				Utils.Error("No se pudo establecer conexion con EFACT - Servicios Caidos");

			return retorno;
		}
		public static string GenerarRutaCopia()
		{
			string retorno = "";
			string[] array = Profile.RutaCarpeta.Split('\\');
			string companiatipo = string.Format("{0}\\{1}\\", array[4], array[5]);
			retorno = Profile.RutaCopiaPDF + companiatipo + Profile.NumeroDocumento + ".pdf";
			return retorno;
		}
		public static bool ValidaResponse(string codigo)
		{
			bool retorno = true;
			if (codigo.Equals("1") || codigo.Equals("2") || codigo.Equals("3"))
			{
				retorno = false;
			}
			return retorno;
		}


		public static bool EstadoBool(string estado)
		{
			return estado.Equals("1");
		}

		public static void All(string texto)
		{
			Log(LevelLog.All, "[ALL] " + texto);
			Profile.ERROR_APPLICATION = 0;
		}
		public static void Warning(string texto)
		{
			Log(LevelLog.WARN, "[WARN] " + texto);
			Profile.ERROR_APPLICATION = 1;
		}

		/// <summary>
		/// Error no Controlados, Uso recomendado en el Caso de TRY CATCH
		/// </summary>
		/// <param name="texto"></param>
		public static void Fatal(string texto)
		{
			Log(LevelLog.FATAL, "[FATAL] " + texto);
			Profile.ERROR_APPLICATION = 1;
		}
		public static void Debug(string texto)
		{
			Log(LevelLog.DEBUG, "[DEBUG] " + texto);
			Profile.ERROR_APPLICATION = 0;
		}
		public static void Error(string texto)
		{
			Log(LevelLog.ERROR, "[ERROR] " + texto);
			Profile.ERROR_APPLICATION = 1;
		}
		public static void Informacion(string texto)
		{
			Log(LevelLog.INFO, "[INFO] " + texto);
			Profile.ERROR_APPLICATION = 0;
		}

		private static void Log(LevelLog level, string texto)
		{
			if (printLog)
			{
				if (nivelLog >= ((int)level))
				{
					string ruta = carpetaroot;
					if (logfordocument)
						ruta = carpetaroot + DateTime.Today.ToShortDateString().Replace("/", "") + @"\";

					if (!Directory.Exists(ruta))
						Directory.CreateDirectory(ruta);

					if (logfordocument)
					{

						if (Profile.Usuario == null)
							ruta = string.Format("{0}{1}.txt", ruta, "GLOBAL");
						else
							ruta = string.Format("{0}{1}_{2}.txt", ruta, Profile.NumeroDocumento , Profile.Usuario);

					}
					else
					{
						if (Profile.Usuario == null)
							ruta = string.Format("{0}{1}.txt", ruta, DateTime.Today.ToShortDateString().Replace("/", ""));
						else
							ruta = string.Format("{0}{1}_{2}.txt", ruta, DateTime.Today.ToShortDateString().Replace("/", ""), Profile.Usuario);
					}


					using (StreamWriter escri = new StreamWriter(ruta, true))
					{
						escri.WriteLine(DateTime.Now.ToLongTimeString() + "> " + texto);
						escri.Flush();
						escri.Close();
					}

				}
			}

		}
	}
}
