using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Security;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading;
using TecnologiaTextil.FacturacionClient;
using TecnologiaTextil.FacturacionClient.DataAccess;

// NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
public class Service : IService
{

	public byte[] ObtenerLog(string Nombre)
	{
		byte[] retorno = null;
		try
		{ 

			string RutaLog = ConfigurationManager.AppSettings["PathLogs"].ToString() +
						 DateTime.Today.ToShortDateString().Replace("/", "") + @"\";

			string RutaFile = string.Format(@"{0}{1}.txt", RutaLog,  Nombre );

			FileStream stream = File.OpenRead(RutaFile);
			retorno = new byte[stream.Length];

			stream.Read(retorno, 0, retorno.Length);
			stream.Close();
		}
		catch (Exception ex)
		{
			Utils.Fatal("Error ObtenerLog => " + ex.Message);

		}
		return retorno;
	}

	private void ProcesoFacturacionConEXE(string Tabla, string Usuario)
	{
		//*******************************************
		//********** problemas con permisos *********
		//******************************************* 
		string Ruta = ConfigurationManager.AppSettings["PathClient"];
		////Utils.Log("Ejecutando Cliente Factura => " + Ruta);
		Process process = new Process();

		//ProcessStartInfo info = new ProcessStartInfo
		//{
		//    FileName = "notepad",
		//    UserName = "admin",
		//    Domain = "",
		//    Password = "myAdminPassword",
		//    UseShellExecute = false,
		//    RedirectStandardOutput = true,
		//    RedirectStandardError = true
		//};

		//process.Start(info);
		//Utils.Log("Parametros => " + string.Format("{0} {1}", Tabla, Usuario));
		ProcessStartInfo proceso = new ProcessStartInfo(Ruta, string.Format("{0} {1}", Tabla, Usuario));
		//proceso.UseShellExecute = false;
		proceso.Verb = "runas";
		proceso.WorkingDirectory = @"D:\";
		//proceso.UserName = "ADMINSIST";
		//proceso.Password = ConvertToSecureString("F1V5P11R2");

		DATabla service = new DATabla();
		DataTable returnTable = service.GetTabla(Tabla);
		string NumeroDocumento = "", TipoDocumento = "", RutaCarpeta = "";
		if (returnTable.Rows.Count > 0)
		{
			NumeroDocumento = returnTable.Rows[0]["doc163"].ToString();
			TipoDocumento = returnTable.Rows[0]["tdo163"].ToString();
			RutaCarpeta = returnTable.Rows[0]["rut163"].ToString();
		}

		ResultFacturacion retorno = null;
		try
		{
			bool Termino = false;
			//Utils.Log("Procesando informacion con Cliente Facturacion");
			Process ejecutando = Process.Start(proceso);
			do
			{
				if (ejecutando.HasExited)
				{
					//Utils.Log("Proceso Concluido - Cliente Termino...");
					Termino = true;
				}
			} while (!ejecutando.WaitForExit(1000) && !Termino);
			int ValueReturn = ejecutando.ExitCode;
			string DescripcionReturn = ValueReturn == 0 ? "Ejecucion Correcta!" : "Hubo Errores...";
			//Utils.Log("Valor Devuelto  => " + ValueReturn.ToString() + " " + DescripcionReturn);
			//Utils.Log("Cliente Facturacion Termino su Ejecucion...");
			if (ValueReturn.ToString().Equals("0"))
			{
				byte[] bytes = ObtenerPDFByte(NumeroDocumento, RutaCarpeta);
				retorno = new ResultFacturacion() { Codigo = "0", Mensaje = "Archivo Procesado Correctamente", Contenido = bytes };
			}
			else
				retorno = new ResultFacturacion() { Codigo = "1", Mensaje = "Archivo Procesado Contiene Errores", Contenido = null };

		}
		catch (Exception ex)
		{
			//Utils.Log("Error No Controlado => " + ex.Message);
			retorno = new ResultFacturacion() { Codigo = "1", Mensaje = ex.Message, Contenido = null };
		}
	}
	private static void ProcessXCOPY(string SolutionDirectory, string TargetDirectory)
	{
		Utils.Debug("Iniciando Proceso de XCOPY...");
		ProcessStartInfo startInfo = new ProcessStartInfo();
		startInfo.CreateNoWindow = false;
		startInfo.UseShellExecute = false;
		startInfo.FileName = "xcopy.exe";
		startInfo.WorkingDirectory = @"C:\";
		startInfo.WindowStyle = ProcessWindowStyle.Hidden;
		string arguments = ConfigurationManager.AppSettings["ArgumentsXCopy"].ToString();
		Utils.Debug("Argumentos de XCOPY => " + arguments);
		startInfo.Arguments = string.Format(@"""{0}"" ""{1}""* {2}", SolutionDirectory, TargetDirectory, arguments);
		try
		{
			using (Process exeProcess = Process.Start(startInfo))
			{
				exeProcess.WaitForExit();
			}
		}
		catch (Exception exp)
		{
			Utils.Fatal("Iniciando Copia PDF con XCOPY => " + exp.Message);
		}
	}
	private ResultFile ObtenerPDF(string ticket)
	{
		int Intentos = 0;
		ServiceClient service = new ServiceClient();
		Utils.Informacion("------------------- Iniciando Obtencion de PDF -------------------");
		Utils.Informacion(string.Format("Tipo y Numero de Documento: {0}-{1}", Profile.TipoDocumento, Profile.NumeroDocumento));

		ResultFile rpdf;
		do
		{
			Intentos++;
			Utils.Debug(string.Format("##### Numero de Intento ({0}) #####", Intentos));
			rpdf = service.getPDF(ticket, Profile.RutaCarpeta, Profile.NumeroDocumento + ".pdf");
			if (Intentos > 1)
				Thread.Sleep(Profile.TimeoutEnvio);

		} while (Intentos <= 3 && !rpdf.code.Equals("0"));
		Utils.Informacion("");
		Utils.Debug(string.Format("====== [Total de Numero de Intentos => {0}]", Intentos));
		return rpdf;
	}
	private ResultFile ObtenerXML(string ticket)
	{
		int Intentos = 0;
		ServiceClient service = new ServiceClient();
		Utils.Informacion("------------------- Iniciando Obtencion de XML -------------------");
		Utils.Informacion(string.Format("Tipo y Numero de Documento: {0}-{1}", Profile.TipoDocumento, Profile.NumeroDocumento));

		ResultFile rxml;
		do
		{
			Intentos++;
			Utils.Debug(string.Format("##### Numero de Intento ({0}) #####", Intentos));
			rxml = service.getXML(ticket, Profile.RutaCarpeta, Profile.NumeroDocumento + ".xml");
			if (Intentos > 1)
				Thread.Sleep(Profile.TimeoutEnvio);

		} while (Intentos <= 3 && !rxml.code.Equals("0"));
		Utils.Informacion("");
		Utils.Debug(string.Format("====== [Total de Numero de Intentos => {0}]", Intentos));
		return rxml;
	}
	private ResultFile ObtenerCDR(string ticket)
	{
		int Intentos = 0;
		ServiceClient service = new ServiceClient();
		Utils.Informacion("------------------- Iniciando Obtencion de CDR -------------------");
		Utils.Informacion(string.Format("Tipo y Numero de Documento: {0}-{1}", Profile.TipoDocumento, Profile.NumeroDocumento));

		ResultFile rcdr;
		do
		{
			Intentos++;
			Utils.Debug(string.Format("##### Numero de Intento ({0}) #####", Intentos));
			rcdr = service.getCDR(ticket, Profile.RutaCarpeta, Profile.NumeroDocumento + "-CDR.xml");
			if (Intentos > 1)
				Thread.Sleep(Profile.TimeoutEnvio);

		} while (Intentos <= 3 && !rcdr.code.Equals("0"));
		Utils.Informacion("");
		Utils.Debug(string.Format("====== [Total de Numero de Intentos => {0}]", Intentos));
		return rcdr;
	}
	private ResultFile ObtenerTickets(string documento)
	{
		int Intentos = 0;
		ServiceClient service = new ServiceClient();
		Utils.Informacion("------------------- INICIANDO OBTENCION DE TICKET -------------------");
		Utils.Informacion(string.Format("Tipo y Numero de Documento: [{0}] {1}", Profile.TipoDocumento, Profile.NumeroDocumento));

		ResultFile rticket;
		do
		{
			Intentos++;
			Utils.Debug(string.Format("##### Numero de Intento ({0}) #####", Intentos));
			rticket = service.getTicket(documento);
			if (Intentos > 1)
				Thread.Sleep(Profile.TimeoutEnvio);

		} while (Intentos <= 3 && !rticket.code.Equals("0"));
		Utils.Informacion("");
		Utils.Debug(string.Format("===> [Total de Numero de Intentos => {0}]", Intentos));
		return rticket;
	}

	private void ObtenerDocumentosOutPdf(string ticket)
	{
		DATabla dtabla = new DATabla();
		Utils.Informacion("Ticket usado para recuperar documentos es : " + ticket);

		//*******************************************************
		ResultFile rcdr = null;
		rcdr = ObtenerCDR(ticket);

		//******************************************************* 
		if (rcdr.code.Equals("0"))
		{
			Utils.Informacion("Archivo CDR Obtenido Correctamente...");
			//*******************************************************
			ResultFile rxml = null;
			rxml = ObtenerXML(ticket);
			//*******************************************************
			if (rxml.code.Equals("0"))
			{
				Utils.Informacion("Archivo XML Obtenido Correctamente...");
			}
			else
				dtabla.UpdateTabla(Profile.NombreTabla, "2", "Error XML -> " + rxml.description);
		}
		else
			dtabla.UpdateTabla(Profile.NombreTabla, "2", "Error CDR -> " + rcdr.description);
	}

	private void ObtenerTicketDocumento(string documento)
	{
		DATabla dtabla = new DATabla();
		Utils.Informacion("");
		Utils.Informacion("======================================================");
		Utils.Informacion(string .Format ("======= DOCUMENTO A OBTENER TICKET => {0} =======",  documento));
		Utils.Informacion("======================================================");
		Utils.Informacion("");
		//*******************************************************
		ResultFile rticket = null;
		rticket = ObtenerTickets(documento);

		//******************************************************* 
		if (rticket.code.Equals("0"))
		{
			//AQUI DEBE DE ACTUALIZAR EL TICKET Y BUSCAR EL CDR
			bool OKTICKET = false;
			string TicketTemp = "";
			foreach (var item in rticket.description.Split('@'))
			{
				ResultFile temp = null;
				Utils.Informacion(string.Format("TICKET USADO ====> {0}", item));
				temp = ObtenerCDR(item);
				if (temp.code.Equals("0") || OKTICKET )
				{
					OKTICKET = true;
					TicketTemp = item;
					break;
				}
			}
			Utils.Debug("Termino de Ejecutar el FOREACH (Recorrido de Tickets)");
			if (OKTICKET)
			{
				Utils.Informacion(string.Format("Actualizando el numero de Ticket {2} en la Tabla => {0} al Doc.=> {1}", Profile.NombreTabla, Profile.NumeroDocumento, TicketTemp ));
				//dtabla.InsertaToken(Profile.NombreTabla, rticket.description, documento);
				dtabla.InsertaToken(Profile.NombreTabla, TicketTemp , documento);
				Utils.Informacion("Archivo Ticket Obtenido Correctamente...");
			}
			else
			{
				Utils.Error("Archivo Ticket No pudo ser Actualizado...");
			}
		}
		//*******************************************************
		else
		{
			Utils.Informacion("Error Ticket -> " + rticket.description);
			dtabla.UpdateTabla(Profile.NombreTabla, "2", "Error Ticket -> " + rticket.description);
		}
	}
	private void ObtenerDocumentos(string ticket)
	{
		Utils.Informacion("Ticket usado para recuperar documentos es : " + ticket);
		DATabla dtabla = new DATabla();
		//*******************************************************
		ResultFile rcdr = null;
		rcdr = ObtenerCDR(ticket);

		//******************************************************* 
		if (rcdr.code.Equals("0"))
		{
			Utils.Informacion("Archivo CDR Obtenido Correctamente...");
			//Utils.Log("Archivo CDR Obtenido Correctamente...");
			//*******************************************************
			ResultFile rxml = null;
			rxml = ObtenerXML(ticket);
			//*******************************************************
			if (rxml.code.Equals("0"))
			{
				Utils.Informacion("Archivo XML Obtenido Correctamente...");
				//Utils.Log("Archivo XML Obtenido Correctamente...");
				string pdforigen = Profile.RutaCarpeta + @"PDF\" + Profile.NumeroDocumento + ".pdf";
				string pdfdestino = Utils.GenerarRutaCopia();
				Utils.Debug("Ruta Origen PDF => " + pdforigen);
				Utils.Debug("Ruta Destino PDF => " + pdfdestino);
				//******************************************************* 
				ResultFile rpdf = null;
				rpdf = ObtenerPDF(ticket);
				//*******************************************************
				if (rpdf.code.Equals("0"))
				{
					Utils.Informacion("Archivo PDF Obtenido Correctamente...");
					//Utils.Log("Archivo PDF Obtenido Correctamente...");
					//if (Profile.Usuario == null)
					//	Process.Start(pdforigen);
					//else
					//Utils.Informacion("En este modo no se puede Previsualizar el PDF [ MODO SERVER ] ");
					//Utils.Log("En este modo no se puede Previsualizar el PDF [ MODO SERVER ] ");
					//COPIANDO PDF A NUEVA UBICACION....
					try
					{
						if (!System.IO.File.Exists(pdfdestino))
						{
							Utils.Informacion("Copiando PDF a Destino -> " + pdfdestino);
							System.IO.File.Copy(pdforigen, pdfdestino, true);
						}
						else
							Utils.Informacion("No es necesario copiar el pdf ya existe");
					}
					catch (Exception ex)
					{
						Utils.Fatal("Generar Copia: " + ex.Message);
						ProcessXCOPY(pdforigen, pdfdestino);
					}

				}
				else
					dtabla.UpdateTabla(Profile.NombreTabla, "1", Profile.NumeroDocumento, "Error PDF -> " + rpdf.description);
			}
			else
				dtabla.UpdateTabla(Profile.NombreTabla, "1", Profile.NumeroDocumento, "Error XML -> " + rxml.description);
		}
		else
			dtabla.UpdateTabla(Profile.NombreTabla, "2", Profile.NumeroDocumento, "Error CDR -> " + rcdr.description);
	}

	public ResultFacturacion ProcesaFacturaElectronica(string Tabla, string Usuario)
	{
		ServiceClient service = new ServiceClient();
		DATabla dtabla = new DATabla();
		Profile.NombreTabla = Tabla;
		Profile.Usuario = Usuario;


		Profile.Tabla = dtabla.GetTabla(Profile.NombreTabla);
		Profile.TimeoutEnvio = int.Parse(ConfigurationManager.AppSettings["TimeoutEnvio"].ToString());


		string mensaje = "";
		bool FacturacionActiva = Utils.EstadoBool(dtabla.GetValorTABCONTR("840", "VALTCO"));
		if (FacturacionActiva)
		{

			if (Profile.Tabla.Rows.Count > 0)
			{
				Profile.AsignaVariables();

				Utils.Informacion("");
				Utils.Informacion("");
				Utils.Informacion("====================================================================");
				Utils.Informacion("Iniciando Programa Facturacion Electronica EFACT REST SERVICIO WEB");
				Utils.Informacion("====================================================================");
				Utils.Informacion("");
				Utils.Informacion("");

				Profile.IndicadorImpresion = Utils.EstadoBool(dtabla.GetValorTABCONTR("809", "VALTCO"));
				Utils.Informacion("Valor de Indicacion de Impresion -> " + Profile.IndicadorImpresion.ToString());
				Profile.RutaCopiaPDF = dtabla.GetValorTABCONTR("810", "TEXTCO");
				Utils.Informacion("Valor de Ruta Copia PDF -> " + Profile.RutaCopiaPDF);
				string TipoOperacion = Profile.Tabla.Rows[0]["FLA163"].ToString().Trim();
				Utils.Informacion(string.Format("FLAG163 => {0} ", Profile.Tabla.Rows[0]["FLA163"].ToString()));
				if (TipoOperacion.Equals("1"))
				{
					Utils.Informacion(string.Format("************** ENVIO DE DOCUMENTO {0}-{1} **************",
													Profile.TipoDocumento,
													Profile.NumeroDocumento));

					return EnvioDocumento(service, dtabla);
				}
				else if (TipoOperacion.Equals("2"))
				{
					Utils.Informacion(string.Format("************** RESTAURACION DE DOCUMENTOS ({0}) **************",
									Profile.NombreTabla));
					DataTable archivos = dtabla.GetTablaRestaurar(Profile.NombreTabla);
					return RestauraDocumentos(archivos, service, dtabla);
				}
				else if (TipoOperacion.Equals("3"))
				{
					Utils.Informacion(string.Format("************** REGULARIZACION DE TICKETS {0} **************",
									Profile.NombreTabla));
					DataTable archivos = dtabla.GetTablaRestaurar(Profile.NombreTabla);
					return RegularizaTickets(archivos, service, dtabla);
				}
				else
				{
					mensaje = "Operacion no Permitida Revisar el campo FLA163 - Tabla " + Profile.NombreTabla;
					Utils.Warning(mensaje);
					return new ResultFacturacion() { Codigo = "1", Contenido = null, Mensaje = mensaje };
				}

			}
			else
			{
				mensaje = "Tabla no Existe , Verifique si tabla " + Profile.NombreTabla + " existe";
				Utils.Warning(mensaje);
				//Utils.Log(mensaje);
				return new ResultFacturacion() { Codigo = "1", Contenido = null, Mensaje = mensaje };
			}
		}
		else
		{

			mensaje = "Facturacion se DESACTIVO, comuniquese con sistemas!";
			Utils.Warning(mensaje);
			//Utils.Log(mensaje);
			return new ResultFacturacion() { Codigo = "1", Contenido = null, Mensaje = mensaje };
		}

	}

	private ResultFacturacion RegularizaTickets(DataTable archivos, ServiceClient service, DATabla dtabla)
	{
		string mensaje = "";
		ResultFacturacion retorno;
		if (Utils.PingEfact())
		{
			int TotalRegistros = archivos.Rows.Count;
			Utils.Debug("Total de Registros a Regularizar => " + TotalRegistros.ToString());
			int Errores = 0;
			if (TotalRegistros > 0)
			{
				foreach (DataRow fila in archivos.Rows)
				{
					string mTipo, mSerie;
					int mNumero, mAnio, mCodCia;
					Profile.RutaCarpeta = fila["RUT163"].ToString();
					Profile.TipoDocumento = fila["TDO163"].ToString();
					Profile.NumeroDocumento = fila["DOC163"].ToString();
					string[] temp = Profile.NumeroDocumento.Split('-');
					mTipo = fila["TDO163"].ToString();
					mAnio = int.Parse(fila["AÑO163"].ToString());
					mCodCia = int.Parse(fila["CIA163"].ToString());
					mSerie = temp[2];
					mNumero = int.Parse(temp[3]);
					string documento = Profile.NumeroDocumento;

					ObtenerTicketDocumento(documento);

				}
				Errores += dtabla.GetTablaRestaurarErrores(Profile.NombreTabla).Rows.Count;
				if (Errores > 0)
					retorno = new ResultFacturacion() { Codigo = "1", Contenido = new byte[] { 0 }, Mensaje = string.Format("Problemas en la restauracion en ({0}) documentos! ", Errores) };
				else
					retorno = new ResultFacturacion() { Codigo = "0", Contenido = new byte[] { 0 }, Mensaje = "Restauracion realizada sin Errores!" };
			}
			else
			{
				mensaje = "No se Encontraron Archivos a Restaurar...";
				Utils.Warning(mensaje);
				retorno = new ResultFacturacion() { Codigo = "1", Contenido = null, Mensaje = mensaje };
			}
		}
		else
			retorno = new ResultFacturacion() { Codigo = "1", Contenido = null, Mensaje = "Problemas de comunicacion con EFACT" };

		return retorno;
	}

	private ResultFacturacion RestauraDocumentos(DataTable archivos, ServiceClient service, DATabla dtabla)
	{
		string mensaje = "";
		ResultFacturacion retorno;
		if (Utils.PingEfact())
		{
			int posicion = 0;
			int TotalRegistros = archivos.Rows.Count;
			int Errores = 0;
			if (TotalRegistros > 0)
			{
				foreach (DataRow fila in archivos.Rows)
				{
					string mTipo, mSerie;
					int mNumero, mAnio, mCodCia;

					Profile.RutaCarpeta = fila["RUT163"].ToString();
					Profile.TipoDocumento = fila["TDO163"].ToString();
					Profile.NumeroDocumento = fila["DOC163"].ToString();
					string[] temp = Profile.NumeroDocumento.Split('-');
					mTipo = fila["TDO163"].ToString();
					mAnio = int.Parse(fila["AÑO163"].ToString());
					mCodCia = int.Parse(fila["CIA163"].ToString());
					mSerie = temp[2];
					mNumero = int.Parse(temp[3]);
					string ticket = dtabla.GetTokenDocumento(mCodCia, mTipo, mAnio, mSerie, mNumero);
					if (!ticket.Equals(string.Empty))
					{
						posicion++;
						mensaje = string.Format("Valores a Actualizar (Documento) => {0} - {1} - {2}", Profile.NombreTabla, "1", Profile.NumeroDocumento);
						Utils.Informacion(mensaje);
						dtabla.UpdateTabla(Profile.NombreTabla, "1", Profile.NumeroDocumento, "Archivo enviado correctamente OK");
						Utils.Informacion("Archivo recuperado correctamente OK");
						//Utils.Log(mensaje);
						ObtenerDocumentos(ticket);
					}
					else
					{
						mensaje = "";
						mensaje = "El Ticket del Documento " + Profile.NumeroDocumento + " No Existe o no pudo ser Recuperado ";
						dtabla.UpdateTabla(Profile.NombreTabla, "2", Profile.NumeroDocumento, mensaje);
						Utils.Warning(mensaje);
					}
					//http://bit.ly/2wTwQfP 

				}
				Errores = dtabla.GetTablaRestaurarErrores(Profile.NombreTabla).Rows.Count;
				if (Errores > 0)
					retorno = new ResultFacturacion() { Codigo = "1", Contenido = new byte[] { 0 }, Mensaje = string.Format("Problemas en la restauracion en ({0}) documentos! ", Errores) };
				else
					retorno = new ResultFacturacion() { Codigo = "0", Contenido = new byte[] { 0 }, Mensaje = "Restauracion realizada sin Errores!" };
			}
			else
			{
				mensaje = "No se Encontraron Archivos a Restaurar...";
				Utils.Warning(mensaje);
				//Utils.Log(mensaje);
				retorno = new ResultFacturacion() { Codigo = "1", Contenido = null, Mensaje = mensaje };
			}
		}
		else
			retorno = new ResultFacturacion() { Codigo = "1", Contenido = null, Mensaje = "Problemas de comunicacion con EFACT" };

		return retorno;
	}
	private ResultFacturacion EnvioDocumento(ServiceClient service, DATabla dtabla)
	{
		string mensaje = "";
		if (Utils.PingEfact())
		{
			//Utils.Log("Servicios Efact en linea");
			//MessageBox.Show("xxx");
			//http://fijo.gestionwebmovistar.com.pe/?cd1=SwBHAGEARgBhAE0AZABVAHcAVwBlAGMAegB2AEwAcwBRAE0AVQAxAFYAegBCAG0AaABRADMAMABzAG4ANABNAA==
			string rutaarchivo = string.Format(@"{0}{1}", Profile.RutaCarpeta, Profile.NumeroDocumento + ".csv");
			if (!System.IO.File.Exists(rutaarchivo))
			{
				mensaje = "Archivo a Procesar no Existe -> " + rutaarchivo;
				Utils.Fatal(mensaje);
				//Utils.Log(mensaje);
				return new ResultFacturacion() { Codigo = "1", Contenido = null, Mensaje = mensaje };
			}

			ResultResponse respuesta = service.EnviarDocumento(rutaarchivo);

			if (Utils.ValidaResponse(respuesta.code))
			{
				string ticket = respuesta.description;
				if (dtabla.InsertaToken(Profile.NombreTabla, ticket))
				{
					dtabla.UpdateTabla(Profile.NombreTabla, "1", "Archivo enviado correctamente OK");
					Utils.Informacion("Archivo enviado a EFACT correctamente OK");
					//Utils.Log("Archivo enviado a EFACT correctamente OK");
					if ((!Profile.TipoDocumento.Equals("RR")) &&
						(!Profile.TipoDocumento.Equals("RA")))
					{
						ObtenerDocumentos(ticket);
						Byte[] data = ObtenerPDFByte(Profile.NumeroDocumento, Profile.RutaCarpeta);
						if (data == null)
						{
							mensaje = "Documento Enviado OK, problemas al recuperar el PDF";

							Utils.Warning(mensaje);
							return new ResultFacturacion() { Codigo = "1", Contenido = null, Mensaje = mensaje };
						}
						else
						{
							mensaje = "Documento Enviado correctamente OK";

							Utils.Warning(mensaje);
							return new ResultFacturacion() { Codigo = "0", Contenido = data, Mensaje = mensaje };
						}
					}
					else
					{
						mensaje = "Documentos Tipos RR o RA no tienen PDF";
						Utils.Informacion(mensaje);
						//Utils.Log(mensaje);
						ObtenerDocumentosOutPdf(ticket);
						mensaje = "Documento Enviado correctamente OK";

						Utils.Warning(mensaje);
						return new ResultFacturacion() { Codigo = "0", Contenido = new byte[] { 0 }, Mensaje = mensaje };
					}
					//VALIDANDO PROCESO


				}
				else
				{
					dtabla.UpdateTabla(Profile.NombreTabla, "2", respuesta.description);
					mensaje = "Envio de Documento = " + respuesta.description;
					//Utils.Log(mensaje);
					Utils.Warning(mensaje);
					return new ResultFacturacion() { Codigo = "1", Contenido = null, Mensaje = respuesta.description };
				}
			}
			else
			{
				//VER EL ABANICO DE ERRORES QUE PUEDEN SALIR CPE ya registrador 403434,344,45565,etc
				dtabla.UpdateTabla(Profile.NombreTabla, "2", respuesta.description);
				//MessageBox.Show(respuesta.description, "Error al procesar Documento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				mensaje = "Error en el Envio de Documento EFACT";
				Utils.Error(mensaje);
				//Utils.Log(mensaje);
				return new ResultFacturacion() { Codigo = "1", Contenido = null, Mensaje = mensaje };
			}
		}
		else
			return new ResultFacturacion() { Codigo = "1", Contenido = null, Mensaje = "Problemas de comunicacion con EFACT" };
	}

	private static byte[] ObtenerPDFByte(string NumeroDocumento, string RutaCarpeta)
	{
		byte[] bytes;
		string RutaPDF = RutaCarpeta + @"PDF\" + NumeroDocumento + ".pdf";
		string mensaje = string.Format("Ruta a recuperar PDF => {0}", RutaPDF);
		Utils.Informacion(mensaje);
		//Utils.Log(mensaje);
		if (System.IO.File.Exists(RutaPDF))
		{
			FileStream stream = File.OpenRead(RutaPDF);
			bytes = new byte[stream.Length];

			stream.Read(bytes, 0, bytes.Length);
			stream.Close();
		}
		else
			bytes = null;
		return bytes;
	}

	public List<ResultListLog> ObtenerListaLog(string Usuario)
	{
		List<ResultListLog> retorno = new List<ResultListLog>();
		string RutaLog = ConfigurationManager.AppSettings["PathLogs"].ToString() +
						 DateTime.Today.ToShortDateString().Replace("/", "") + @"\";

		DirectoryInfo files = new DirectoryInfo(RutaLog);

		foreach (FileInfo item in files.GetFiles(string.Format("*{0}.txt", Usuario)))
		{
			retorno.Add(new ResultListLog()
			{
				Nombre = Path.GetFileNameWithoutExtension (item.Name ),
				Hora = item.LastWriteTime.ToLongTimeString(),
				Tamano = item.Length
			});
		}
		return retorno;
	}
}
