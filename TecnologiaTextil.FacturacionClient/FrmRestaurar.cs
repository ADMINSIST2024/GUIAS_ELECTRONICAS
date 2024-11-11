using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TecnologiaTextil.FacturacionClient.DataAccess;

namespace TecnologiaTextil.FacturacionClient
{
    public partial class FrmRestaurar : Form
    {
        private ServiceClient service;
        private DATabla dtabla;
        private SynchronizationContext m_SynchronizationContext;

        public FrmRestaurar()
        {
            InitializeComponent();
            service = new ServiceClient();
            dtabla = new DATabla();
            m_SynchronizationContext = new SynchronizationContext();
        }
        private void ObtenerDocumentos(string ticket)
        {
            Utils.Informacion("Ticket usado para recuperar documentos es : " + ticket);

            ResultFile rcdr = service.getCDR(ticket, Profile.RutaCarpeta, Profile.NumeroDocumento + "-CDR.xml");
            if (rcdr.code.Equals("0"))
            {
                Utils.Informacion("Archivo CDR Obtenido Correctamente...");

                ResultFile rxml = service.getXML(ticket, Profile.RutaCarpeta, Profile.NumeroDocumento + ".xml");
                if (rcdr.code.Equals("0"))
                {
                    Utils.Informacion("Archivo XML Obtenido Correctamente...");

                    string pdforigen = Profile.RutaCarpeta + @"PDF\" + Profile.NumeroDocumento + ".pdf";
                    string pdfdestino = Utils.GenerarRutaCopia(); //Profile.RutaCopiaPDF + @"PDF\" + Profile.NumeroDocumento + ".pdf";

                    ResultFile rpdf = service.getPDF(ticket, Profile.RutaCarpeta, Profile.NumeroDocumento + ".pdf");
                    if (rcdr.code.Equals("0"))
                    {
                        Utils.Informacion("Archivo PDF Obtenido Correctamente...");
                        //COPIANDO PDF A NUEVA UBICACION....
                        try
                        {
							if (!System.IO.File.Exists(pdfdestino))
							{
								Utils.Informacion("Copiando PDF a Destino -> " + pdfdestino);
								System.IO.File.Copy(pdforigen, pdfdestino, true);
							}
							else
							{
								Utils.Informacion("No es necesario copiar el pdf ya existe");

							}
                        }
                        catch (Exception ex)
                        {
                            Utils.Fatal("Generar Copia: " + ex.Message);
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
        //http://bit.ly/2wTwQfP
        public void ShowNotificacion(string mensaje)
        {
            notifyIcon1.BalloonTipText = mensaje;
            notifyIcon1.BalloonTipTitle = "Cliente de Facturacion - Tecnologia Textil";
            notifyIcon1.ShowBalloonTip(1000);
            notifyIcon1.Visible = true;


        }
        void SetMensaje(int posicion, int TotalRegistros)
        {
            //m_SynchronizationContext.Post((@object) => {
            //    Tuple<int, int> minMax = (Tuple<int, int>)@object;
            //    progressBar1.Maximum = minMax.Item1;
            //    progressBar1.Value = minMax.Item2;
            //}, value);
            m_SynchronizationContext.Post((param) =>
            {
                label1.Text = string.Format("Restaurando Documentos( {0} de   )", param);
            }, posicion);

        }

        async Task RecuperarDocumentoEfact(DataTable archivos, IProgress<int> progress)
        {
            await Task.Factory.StartNew(() =>
            {
                if (Utils.PingEfact())
                {
                    int posicion = 0;
                    int TotalRegistros = archivos.Rows.Count;
                    if (TotalRegistros > 0)
                        foreach (DataRow fila in archivos.Rows)
                        {
                            string mTipo, mSerie;
                            int mNumero, mAnio, mCodCia;
							
                            Profile.NumeroDocumento = fila["DOC163"].ToString();
							Profile.RutaCarpeta = fila["RUT163"].ToString();
							Profile.TipoDocumento = fila["TDO163"].ToString();
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
                                progress.Report(posicion);//, TotalRegistros);
								Utils.Informacion(string .Format ("Valores a Actualizar (Documento) => {0} - {1} - {2}",Profile.NombreTabla, "1", Profile.NumeroDocumento ));
								dtabla.UpdateTabla(Profile.NombreTabla, "1", Profile.NumeroDocumento, "Archivo enviado correctamente OK");
                                Utils.Informacion("Archivo enviado correctamente OK");
                                ObtenerDocumentos(ticket);
                            }
                            else
                            {
								string mensaje = "";
								mensaje = "El Ticket del Documento " + Profile.NumeroDocumento + " No Existe o no pudo ser Recuperado ";
								dtabla.UpdateTabla(Profile.NombreTabla, "2", Profile.NumeroDocumento,mensaje  );
                                Utils.Warning(mensaje);
                            }
                            //http://bit.ly/2wTwQfP 
                        }
                    else
                        Utils.Warning("No se Encontraron Archivos a Restaurar...");
                }

            });
        }
        private async void FrmRestaurar_Load(object sender, EventArgs e)
        {
            Utils.Informacion("-----------------------------------------------------------");
            Utils.Informacion("----- INICIANDO PROCESO DE RESTAURACION DE DOCUMENTOS -----");
            Utils.Informacion("-----------------------------------------------------------");
            Utils.Informacion("");
			
            DataTable archivos = dtabla.GetTablaRestaurar(Profile.NombreTabla);
            int TotalRegistros = archivos.Rows.Count;
            var progress = new Progress<int>(percent =>
            { 
                label1.Text = string.Format("Restaurando Documentos( {0} de  {1} )", percent, TotalRegistros);
            });
            await RecuperarDocumentoEfact(archivos, progress);
            Application.Exit();
        }

        int tiempo = 1;

        private void timer1_Tick(object sender, EventArgs e)
        {
            tiempo++;
            label3.Text = tiempo.ToString("00");
            if (tiempo > 30)
            {
                ShowNotificacion("Se Supero el Tiempo limite para realizar el envio del documento");
                Application.Exit();
            }
        }

		private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Application.Exit();
		}
	}
}
