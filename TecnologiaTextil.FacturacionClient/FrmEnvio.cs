using Guna.UI.Animation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using TecnologiaTextil.FacturacionClient.DataAccess;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TecnologiaTextil.FacturacionClient
{
	public partial class FrmEnvio : Form
	{
		private ServiceClient service;
		private DATabla dtabla;
        int mensaje = 0;
        public FrmEnvio()
		{
			//MessageBox.Show("HOLA");
			InitializeComponent();
			service = new ServiceClient();
			dtabla = new DATabla();
		}

        public static void ProcessXCOPY(string SolutionDirectory, string TargetDirectory)
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
        public ResultFile ObtenerPDF(string ticket, int Intentos)
		{
			Utils.Informacion("------------------- Iniciando Obtencion de PDF -------------------");
			Utils.Informacion(string.Format("Tipo y Numero de Documento: {0}-{1}", Profile.TipoDocumento, Profile.NumeroDocumento));
            bool continuar = true;
            string codigosPermitidos = ConfigurationManager.AppSettings["codigosPermitidos"].ToString();
            int NumeroIntentoPDF = Convert.ToInt32(ConfigurationManager.AppSettings["NumeroIntentoPDF"].ToString());
            ResultFile rpdf;
			do
			{
				Intentos++;
				Utils.Debug(string.Format("##### Numero de Intento ({0}) #####", Intentos));
				rpdf = service.getPDF(ticket, Profile.RutaCarpeta, Profile.NumeroDocumento + ".pdf");
				if (Intentos > 1)
					Thread.Sleep(Profile.TimeoutEnvio);
                continuar = codigosPermitidos.Split(',').Contains(rpdf.code);
            } while (Intentos <= NumeroIntentoPDF && continuar);
			return rpdf;
		}
        public ResultFile ObtenerXML(string ticket, int Intentos)
		{
			Utils.Informacion("------------------- Iniciando Obtencion de XML -------------------");
			Utils.Informacion(string.Format("Tipo y Numero de Documento: {0}-{1}", Profile.TipoDocumento, Profile.NumeroDocumento));
            bool continuar = true;
            string codigosPermitidos = ConfigurationManager.AppSettings["codigosPermitidos"].ToString();
            int NumeroIntentoXML = Convert.ToInt32(ConfigurationManager.AppSettings["NumeroIntentoXML"].ToString());
            ResultFile rxml;
			do
			{
				Intentos++;
				Utils.Debug(string.Format("##### Numero de Intento ({0}) #####", Intentos));
				rxml = service.getXML(ticket, Profile.RutaCarpeta, Profile.NumeroDocumento + ".xml");
				if (Intentos > 1)
					Thread.Sleep(Profile.TimeoutEnvio);
                continuar = codigosPermitidos.Split(',').Contains(rxml.code);
            } while (Intentos <= NumeroIntentoXML && continuar);
			return rxml;
		}
		public ResultFile ObtenerCDR(string ticket, int Intentos)
		{
			Utils.Informacion("------------------- Iniciando Obtencion de CDR -------------------");
			Utils.Informacion(string.Format("Tipo y Numero de Documento: {0}-{1}", Profile.TipoDocumento, Profile.NumeroDocumento));
            bool continuar = true;
            string codigosPermitidos = ConfigurationManager.AppSettings["codigosPermitidos"].ToString();
            int NumeroIntentoCDR = Convert.ToInt32(ConfigurationManager.AppSettings["NumeroIntentoCDR"].ToString());
            ResultFile rcdr;
			do
			{
				Intentos++;
				Utils.Debug(string.Format("##### Numero de Intento ({0}) #####", Intentos));
				rcdr = service.getCDR(ticket, Profile.RutaCarpeta, Profile.NumeroDocumento + "-CDR.xml");
				if (Intentos > 1)
					Thread.Sleep(Profile.TimeoutEnvio);
                continuar = codigosPermitidos.Split(',').Contains(rcdr.code);
            } while (Intentos <= NumeroIntentoCDR && continuar);
			return rcdr;
		}
        public void ObtenerDocumentosOutPdf(string ticket)
		{
			Utils.Informacion("Ticket usado para recuperar documentos es : " + ticket);

			//*******************************************************
			ResultFile rcdr = null;
			rcdr = ObtenerCDR(ticket, 0);

			//******************************************************* 
			if (rcdr.code.Equals("0"))
			{
				Utils.Informacion("Archivo CDR Obtenido Correctamente...");
				//*******************************************************
				ResultFile rxml = null;
				rxml = ObtenerXML(ticket, 0);
				//*******************************************************
				if (rxml.code.Equals("0"))
				{
					Utils.Informacion("Archivo XML Obtenido Correctamente...");
				}
				else
					dtabla.UpdateTabla(Profile.NombreTabla, "1", "Error XML -> " + rxml.description);
			}
			else
				dtabla.UpdateTabla(Profile.NombreTabla, "2", "Error CDR -> " + rcdr.description);
		}
        public void ObtenerDocumentos(string ticket)
        {
            Utils.Informacion("Ticket usado para recuperar documentos es : " + ticket);

            //*******************************************************
            ResultFile rcdr = null;
            rcdr = ObtenerCDR(ticket, 0);

            //******************************************************* 
            if (rcdr.code.Equals("0"))
            {
                dtabla.UpdateTicket_Documentos(rcdr.code, rcdr.description, "CDR", "0", Profile.TipoDocumento, Profile.NumeroDocumento);

                Utils.Informacion("Archivo CDR Obtenido Correctamente...");
                //*******************************************************
                ResultFile rxml = null;
                rxml = ObtenerXML(ticket, 0);
                //*******************************************************
                if (rxml.code.Equals("0"))
                {
                    Utils.Informacion("Archivo XML Obtenido Correctamente...");
                    dtabla.UpdateTicket_Documentos(rxml.code, rxml.description, "XML", "0", Profile.TipoDocumento, Profile.NumeroDocumento);

                    string pdforigen = Profile.RutaCarpeta + @"PDF\" + Profile.NumeroDocumento + ".pdf";
                    string pdfdestino = Utils.GenerarRutaCopia();
                    Utils.Debug("Ruta Origen PDF => " + pdforigen);
                    Utils.Debug("Ruta Destino PDF => " + pdfdestino);
                    //******************************************************* 
                    ResultFile rpdf = null;
                    rpdf = ObtenerPDF(ticket, 0);
                    //*******************************************************
                    if (rpdf.code.Equals("0"))
                    {
                        dtabla.UpdateTicket_Documentos(rpdf.code, rpdf.description, "PDF", "0", Profile.TipoDocumento, Profile.NumeroDocumento);

                        string cadena = Profile.NumeroDocumento;
                        char separador = '-';
                        string[] partes = cadena.Split(separador);
                        int anio = DateTime.Now.Year;
                            string ruc = partes[0];
                            string tipoDocumento = partes[1];
                            string serieDocumento = partes[2];
                            int numeroDocumento = Convert.ToInt32(partes[3]);

                         dtabla.UpdateTablaFVTGUIA1(Profile.Cia, anio, serieDocumento, numeroDocumento);

                        Utils.Informacion("Archivo PDF Obtenido Correctamente...");
                        if (Profile.Usuario == null)
                            Process.Start(pdforigen);
                        else
                            Utils.Informacion("En este modo no se puede Previsualizar el PDF [ MODO SERVER ] ");
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
                        // Se cerro fin de log para pdf
                        Utils.Informacion("***********************************************");
                        Utils.Informacion("Fin de Aplicacion Global");
                        Utils.Informacion("***********************************************");


                    }
                    else
                    {
                        dtabla.UpdateTabla(Profile.NombreTabla, "1", "Error PDF -> " + rpdf.description);
                        dtabla.UpdateTicket_Documentos(rpdf.code, rpdf.description, "PDF", "1", Profile.TipoDocumento, Profile.NumeroDocumento);
                       
                        Utils.Informacion("***********************************************");
                        Utils.Informacion("Fin de Aplicacion Global");
                        Utils.Informacion("***********************************************");

                    }

                }
                else
                {
                    dtabla.UpdateTabla(Profile.NombreTabla, "1", "Error XML -> " + rxml.description);
                    dtabla.UpdateTicket_Documentos(rxml.code, rxml.description, "XML", "1", Profile.TipoDocumento, Profile.NumeroDocumento);
                 
                    Utils.Informacion("***********************************************");
                    Utils.Informacion("Fin de Aplicacion Global");
                    Utils.Informacion("***********************************************");

                }
            }
            else { 
                dtabla.UpdateTabla(Profile.NombreTabla, "2", "Error CDR -> " + rcdr.description);
            dtabla.UpdateTicket_Documentos(rcdr.code, rcdr.description, "CDR", "2", Profile.TipoDocumento, Profile.NumeroDocumento);

                Utils.Informacion("***********************************************");
                Utils.Informacion("Fin de Aplicacion Global");
                Utils.Informacion("***********************************************");


            }

        }

		async Task<string> EnviarDocumentoEfact()
		{
			string ticket="", ticket_error="";

            await Task.Factory.StartNew(() =>
			{
				if (Utils.PingEfact())
				{
                    //MessageBox.Show("xxx");
                    //http://fijo.gestionwebmovistar.com.pe/?cd1=SwBHAGEARgBhAE0AZABVAHcAVwBlAGMAegB2AEwAcwBRAE0AVQAxAFYAegBCAG0AaABRADMAMABzAG4ANABNAA==
                   string rutaarchivo = string.Format(@"{0}{1}", Profile.RutaCarpeta, Profile.NumeroDocumento + ".csv");
                    //string rutaarchivo = @"D:\\PRUEBACSV\20297986130-09-T900-00000001.CSV";
                    if (!System.IO.File.Exists(rutaarchivo))
					{
						Utils.Fatal("Archivo a Procesar no Existe -> " + rutaarchivo);
						return;
					}
                     ticket = dtabla.Validar_Tiene_Ticket(Profile.TipoDocumento, Profile.NumeroDocumento);
                    ticket_error = dtabla.Validar_Tiene_Ticket_con_Error(Profile.TipoDocumento, Profile.NumeroDocumento);



                    if (ticket_error.Length > 0)
                    {
                        ResultResponse respuesta = service.EnviarDocumento(rutaarchivo);

                       


                        if (dtabla.Validar_Nro_Doc_Ticket(Profile.TipoDocumento, Profile.NumeroDocumento) == 0)
                        {
                            dtabla.InsertaTicket("", "", "CSV", "", Profile.TipoDocumento, Profile.NumeroDocumento, "");
                        }
                        else
                        {
                            dtabla.EliminarTicket(Profile.TipoDocumento, Profile.NumeroDocumento);
                            dtabla.InsertaTicket("", "", "CSV", "", Profile.TipoDocumento, Profile.NumeroDocumento, "");
                        }


                        if (Utils.ValidaResponse(respuesta.code))
                        {
                            ticket = respuesta.description;

                            if (dtabla.InsertaToken(Profile.NombreTabla, ticket))
                            {

                                dtabla.UpdateTabla(Profile.NombreTabla, "1", "Archivo enviado correctamente OK");
                                Utils.Informacion("Archivo enviado correctamente OK");
                                dtabla.UpdateTicket(respuesta.code, "Archivo enviado correctamente OK", ticket, "CSV", "1", Profile.TipoDocumento, Profile.NumeroDocumento);

                                if ((!Profile.TipoDocumento.Equals("RR")) &&
                                    (!Profile.TipoDocumento.Equals("RA")))
                                    ObtenerDocumentos(ticket);
                                else
                                {
                                    Utils.Informacion("Documentos Tipos RR o RA no tienen PDF");
                                    ObtenerDocumentosOutPdf(ticket);
                                }
                            }
                            else
                            {
                                dtabla.UpdateTabla(Profile.NombreTabla, "2", respuesta.description);
                                Utils.Warning("Envio de Documento = " + respuesta.description);
                                dtabla.UpdateTicket(respuesta.code, respuesta.description, "", "CSV", "2", Profile.TipoDocumento, Profile.NumeroDocumento);
                            }

                        }


                        else
                        {
                            //VER EL ABANICO DE ERRORES QUE PUEDEN SALIR CPE ya registrador 403434,344,45565,etc
                            dtabla.UpdateTabla(Profile.NombreTabla, "2", respuesta.description);
                            //MessageBox.Show(respuesta.description, "Error al procesar Documento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            Utils.Error("Error en el Envio de Documento EFACT");
                            // dtabla.UpdateTicket(respuesta.code, "Error en el Envio de Documento EFACT", respuesta.description, "ENVIO_DOC", "2", Profile.TipoDocumento, Profile.NumeroDocumento);
                            dtabla.UpdateTicket(respuesta.code, respuesta.description, "", "CSV", "2", Profile.TipoDocumento, Profile.NumeroDocumento);

                        }
                    }
                    else 
                    {
                        if (ticket.Length > 0)
                        {
                            if ((!Profile.TipoDocumento.Equals("RR")) &&
                                   (!Profile.TipoDocumento.Equals("RA")))
                                ObtenerDocumentos(ticket);
                            else
                            {
                                Utils.Informacion("Documentos Tipos RR o RA no tienen PDF");
                                ObtenerDocumentosOutPdf(ticket);
                            }
                        }
                        else
                        {
                            ResultResponse respuesta = service.EnviarDocumento(rutaarchivo);

                            if (dtabla.Validar_Nro_Doc_Ticket(Profile.TipoDocumento, Profile.NumeroDocumento) == 0)
                            {
                                dtabla.InsertaTicket("", "", "CSV", "", Profile.TipoDocumento, Profile.NumeroDocumento, "");
                            }


                            if (Utils.ValidaResponse(respuesta.code))
                            {
                                ticket = respuesta.description;

                                if (dtabla.InsertaToken(Profile.NombreTabla, ticket))
                                {

                                    dtabla.UpdateTabla(Profile.NombreTabla, "1", "Archivo enviado correctamente OK");
                                    Utils.Informacion("Archivo enviado correctamente OK");
                                    dtabla.UpdateTicket(respuesta.code, "Archivo enviado correctamente OK", ticket, "CSV", "1", Profile.TipoDocumento, Profile.NumeroDocumento);

                                    if ((!Profile.TipoDocumento.Equals("RR")) &&
                                        (!Profile.TipoDocumento.Equals("RA")))
                                        ObtenerDocumentos(ticket);
                                    else
                                    {
                                        Utils.Informacion("Documentos Tipos RR o RA no tienen PDF");
                                        ObtenerDocumentosOutPdf(ticket);
                                    }
                                }
                                else
                                {
                                    dtabla.UpdateTabla(Profile.NombreTabla, "2", respuesta.description);
                                    Utils.Warning("Envio de Documento = " + respuesta.description);
                                    dtabla.UpdateTicket(respuesta.code, respuesta.description, "", "CSV", "2", Profile.TipoDocumento, Profile.NumeroDocumento);
                                }

                            }


                            else
                            {
                                //VER EL ABANICO DE ERRORES QUE PUEDEN SALIR CPE ya registrador 403434,344,45565,etc
                                dtabla.UpdateTabla(Profile.NombreTabla, "2", respuesta.description);
                                //MessageBox.Show(respuesta.description, "Error al procesar Documento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                Utils.Error("Error en el Envio de Documento EFACT");
                                // dtabla.UpdateTicket(respuesta.code, "Error en el Envio de Documento EFACT", respuesta.description, "ENVIO_DOC", "2", Profile.TipoDocumento, Profile.NumeroDocumento);
                                dtabla.UpdateTicket(respuesta.code, respuesta.description, "", "CSV", "2", Profile.TipoDocumento, Profile.NumeroDocumento);

                            }

                        }

                    }









                   

                
				}
			});

			return ticket;
        }
		private async void Form1_Load(object sender, EventArgs e)
		{
            
            //UtilNetDrive oNetDrive = new UtilNetDrive();
            //oNetDrive.LocalDrive = "X:";
            //oNetDrive.Persistent = true;
            //oNetDrive.SaveCredentials = true;
            //oNetDrive.ShareName = @"\\192.168.166.22\factelectr";
            //oNetDrive.MapDrive("ADMINSIST", "F1V5P11R2");

            //MessageBox.Show("PASO TODO OK");

            //MessageBox.Show("Enviando Documento de Pruebas.....");
            //Application.Exit();

            string ticket =   await EnviarDocumentoEfact();
            Obtener_Observaciones(ticket);

         
            // Application.Exit();

        }
		int tiempo = 1;
       
        private void timer1_Tick(object sender, EventArgs e)
		{
			
             int TimerGeneral = Convert.ToInt32(ConfigurationManager.AppSettings["TimerGeneral"].ToString());
            tiempo++;
			label3.Text = tiempo.ToString("00");
			if (tiempo > TimerGeneral )
			{
              
                timer1.Stop();
                Utils.Informacion("Se Supero el Tiempo limite para realizar el envio del documento");
                    DialogResult resultado = MessageBox.Show("Se Supero el Tiempo limite para realizar el envio del documento", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    if (resultado == DialogResult.OK)
                {
                    Utils.Informacion("***********************************************");
                    Utils.Informacion("Fin de Aplicacion Global");
                    Utils.Informacion("***********************************************");

                    Application.Exit();
               }
                
			}
		}

		private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Application.Exit();
		}

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        public void Obtener_Observaciones(string ticket)
        {
            timer1.Stop();
            DataTable tabla = new DataTable();
            tabla = dtabla.Obtener_Observaciones(ticket);
            string TIPDOC = "", NRODOC = "", ESTADO = "", CODIGO = "", DESCRIP = "", PROCESO = "";
            if (tabla.Rows.Count > 0)
            {

                TIPDOC = tabla.Rows[0]["TIPDOC"].ToString();
                NRODOC = tabla.Rows[0]["NRODOC"].ToString();
                ESTADO = tabla.Rows[0]["ESTADO"].ToString();
                CODIGO = tabla.Rows[0]["CODIGO"].ToString();
                DESCRIP = tabla.Rows[0]["DESCRIP"].ToString();
                PROCESO = tabla.Rows[0]["PROCESO"].ToString();


            }
            // Asignamos el valor al Label
            txtticket.Text = ticket;
            txttip_doc.Text = TIPDOC;
            txtdoc.Text = NRODOC;
            txtestado.Text = ESTADO;
            txtcoderror.Text = CODIGO;
            txtdescripcionerror.Text = DESCRIP;
            txtproceso.Text = PROCESO;

            if (CODIGO == "-9998")
            {
                pictureBox1.Visible = false;
                pictureBox3.Visible = true;
                label2.Visible = false;
                btnReintentar.Visible = true;
            }
            else if (CODIGO == "0")
            {
                pictureBox1.Visible = false;
                pictureBox3.Visible = false;
                pictureBox4.Visible = true;
                label2.Visible = false;
                btnReintentar.Visible = false;
            }
            else { btnReintentar.Visible = false;
                pictureBox1.Visible = false;
                pictureBox3.Visible = true;
                label2.Visible = false;
            }
            //colocado test
           // btnReintentar.Visible = true;
        }
       




        private async void btnReintentar_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = true;
            pictureBox3.Visible = false;
            pictureBox4.Visible = false;
            label2.Visible = true;
            btnReintentar.Visible = false;


            string ticket = txtticket.Text;
            string proceso = txtproceso.Text;
        
           await EnviarDocumentoEfact();
            Obtener_Observaciones(ticket);
        }
        public void ReintentarObtenerDocumentos(string ticket, string proceso)
        {
            Utils.Informacion("Ticket usado para recuperar documentos es : " + ticket);

            //*******************************************************
            if (proceso == "CDR")
            {
                ResultFile rcdr = null;
                rcdr = ObtenerCDR(ticket, 0);

                //******************************************************* 
                if (rcdr.code.Equals("0"))
                {
                    dtabla.UpdateTicket(rcdr.code, rcdr.description, ticket, "CDR", "0", Profile.TipoDocumento, Profile.NumeroDocumento);
                    Utils.Informacion("Archivo CDR Obtenido Correctamente...");
                    //*******************************************************
                    ResultFile rxml = null;
                    rxml = ObtenerXML(ticket, 0);
                    //*******************************************************
                    if (rxml.code.Equals("0"))
                    {
                        Utils.Informacion("Archivo XML Obtenido Correctamente...");
                        dtabla.UpdateTicket(rcdr.code, rcdr.description, ticket, "XML", "0", Profile.TipoDocumento, Profile.NumeroDocumento);

                        string pdforigen = Profile.RutaCarpeta + @"PDF\" + Profile.NumeroDocumento + ".pdf";
                        string pdfdestino = Utils.GenerarRutaCopia();
                        Utils.Debug("Ruta Origen PDF => " + pdforigen);
                        Utils.Debug("Ruta Destino PDF => " + pdfdestino);
                        //******************************************************* 
                        ResultFile rpdf = null;
                        rpdf = ObtenerPDF(ticket, 0);
                        //*******************************************************
                        if (rpdf.code.Equals("0"))
                        {
                            dtabla.UpdateTicket(rcdr.code, rcdr.description, ticket, "PDF", "0", Profile.TipoDocumento, Profile.NumeroDocumento);

                            Utils.Informacion("Archivo PDF Obtenido Correctamente...");
                            if (Profile.Usuario == null)
                                Process.Start(pdforigen);
                            else
                                Utils.Informacion("En este modo no se puede Previsualizar el PDF [ MODO SERVER ] ");
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
                                FrmEnvio.ProcessXCOPY(pdforigen, pdfdestino);
                            }


                        }
                        else
                            dtabla.UpdateTabla(Profile.NombreTabla, "1", "Error PDF -> " + rpdf.description);
                        dtabla.UpdateTicket(rcdr.code, rcdr.description, ticket, "PDF", "1", Profile.TipoDocumento, Profile.NumeroDocumento);
                    }
                    else
                        dtabla.UpdateTabla(Profile.NombreTabla, "1", "Error XML -> " + rxml.description);
                    dtabla.UpdateTicket(rcdr.code, rcdr.description, ticket, "XML", "1", Profile.TipoDocumento, Profile.NumeroDocumento);
                }
                else
                    dtabla.UpdateTabla(Profile.NombreTabla, "2", "Error CDR -> " + rcdr.description);
                dtabla.UpdateTicket(rcdr.code, rcdr.description, ticket, "CDR", "2", Profile.TipoDocumento, Profile.NumeroDocumento);
            }
            else if (proceso == "XML")
            {

                //*******************************************************
                ResultFile rxml = null;
                rxml = ObtenerXML(ticket, 0);
                //*******************************************************
                if (rxml.code.Equals("0"))
                {
                    Utils.Informacion("Archivo XML Obtenido Correctamente...");
                    dtabla.UpdateTicket(rxml.code, rxml.description, ticket, "XML", "0", Profile.TipoDocumento, Profile.NumeroDocumento);

                    string pdforigen = Profile.RutaCarpeta + @"PDF\" + Profile.NumeroDocumento + ".pdf";
                    string pdfdestino = Utils.GenerarRutaCopia();
                    Utils.Debug("Ruta Origen PDF => " + pdforigen);
                    Utils.Debug("Ruta Destino PDF => " + pdfdestino);
                    //******************************************************* 
                    ResultFile rpdf = null;
                    rpdf = ObtenerPDF(ticket, 0);
                    //*******************************************************
                    if (rpdf.code.Equals("0"))
                    {
                        dtabla.UpdateTicket(rpdf.code, rpdf.description, ticket, "PDF", "0", Profile.TipoDocumento, Profile.NumeroDocumento);

                        Utils.Informacion("Archivo PDF Obtenido Correctamente...");
                        if (Profile.Usuario == null)
                            Process.Start(pdforigen);
                        else
                            Utils.Informacion("En este modo no se puede Previsualizar el PDF [ MODO SERVER ] ");
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
                            FrmEnvio.ProcessXCOPY(pdforigen, pdfdestino);
                        }


                    }
                    else
                        dtabla.UpdateTabla(Profile.NombreTabla, "1", "Error PDF -> " + rpdf.description);
                    dtabla.UpdateTicket(rpdf.code, rpdf.description, ticket, "PDF", "1", Profile.TipoDocumento, Profile.NumeroDocumento);
                }
                else
                    dtabla.UpdateTabla(Profile.NombreTabla, "1", "Error XML -> " + rxml.description);
                dtabla.UpdateTicket(rxml.code, rxml.description, ticket, "XML", "1", Profile.TipoDocumento, Profile.NumeroDocumento);
            }
            else if (proceso == "PDF")
            {
                string pdforigen = Profile.RutaCarpeta + @"PDF\" + Profile.NumeroDocumento + ".pdf";
                string pdfdestino = Utils.GenerarRutaCopia();
                Utils.Debug("Ruta Origen PDF => " + pdforigen);
                Utils.Debug("Ruta Destino PDF => " + pdfdestino);
                //******************************************************* 
                ResultFile rpdf = null;
                rpdf = ObtenerPDF(ticket, 0);
                //*******************************************************
                if (rpdf.code.Equals("0"))
                {
                    dtabla.UpdateTicket(rpdf.code, rpdf.description, ticket, "PDF", "0", Profile.TipoDocumento, Profile.NumeroDocumento);

                    Utils.Informacion("Archivo PDF Obtenido Correctamente...");
                    if (Profile.Usuario == null)
                        Process.Start(pdforigen);
                    else
                        Utils.Informacion("En este modo no se puede Previsualizar el PDF [ MODO SERVER ] ");
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
                        FrmEnvio.ProcessXCOPY(pdforigen, pdfdestino);
                    }


                }
                else
                    dtabla.UpdateTabla(Profile.NombreTabla, "1", "Error PDF -> " + rpdf.description);
                dtabla.UpdateTicket(rpdf.code, rpdf.description, ticket, "PDF", "1", Profile.TipoDocumento, Profile.NumeroDocumento);

            }



        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void gunaLabel7_Click(object sender, EventArgs e)
        {

        }
    }
}
