using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using TecnologiaTextil.FacturacionClientXP.ServiceEfact;

namespace TecnologiaTextil.FacturacionClientXP
{
	public partial class FrmEnvio : Form
	{
		public string Tabla { get; set; }
		public FrmEnvio(string mTabla)
		{
			this.Tabla = mTabla;
			InitializeComponent();
		}
		// public bool  ProcesoTerminado { get; set; }


		//clien
		int TimeoutExceed = int.Parse ( ConfigurationManager.AppSettings["TimeoutExceeded"].ToString());
		private void FrmEnvio_Load(object sender, EventArgs e)
		{
			tiempo = 0;
			Utils.Log("Conectando al Servidor Web Services... 1");
			ServiceEfact.ServiceClient client = new ServiceEfact.ServiceClient();
			//client.ClientCredentials.Windows.AllowedImpersonationLevel =
			//System.Security.Principal.TokenImpersonationLevel.Impersonation;
			//this.ProcesoTerminado = false; 
			string direccion = client.Endpoint.Address.Uri.AbsoluteUri;
			Utils.Log("Conectando al Servidor Web Services... 2");
			Utils.Log("URL => " + direccion);
			string NombrePC = Environment.MachineName.ToString();

			client.ProcesaFacturaElectronicaCompleted += new EventHandler<ServiceEfact.ProcesaFacturaElectronicaCompletedEventArgs>(ProcesoFacturacion);
			Utils.Log("Inicinado Invocacion Asincrona");
			Utils.Log("Parametros Enviados => " + Tabla + " " + NombrePC);
			label2.ForeColor = Color.Blue;
			label2.Text = "Espere un momento por favor... ";

			client.ProcesaFacturaElectronicaAsync(Tabla, NombrePC);
		}

		private void ProcesoFacturacion(object sender, ProcesaFacturaElectronicaCompletedEventArgs e)
		{
			try
			{
			
				Utils.Log("Obteniendo Mensaje de Respuesta => " + e.Result.Mensaje);
				Utils.Log("Obteniendo Codigo de Respuesta => " + e.Result.Codigo );
				label2.Text = e.Result.Mensaje;
				//this.ProcesoTerminado = true;
				
				if (e.Result.Codigo == "0")
				{
					label2.ForeColor = Color.Blue;
					linkLabel1.Visible = false;
					linkLabel2.Visible = false;
					Utils.Log("Invocacion Exitosa ...");
					System.Threading.Thread.Sleep(2000);
					string RutaPDFTemp = Utils.RutaPDF();
					string RutaFilePDF = RutaPDFTemp + "temppdf.pdf";
					if (e.Result.Contenido == null)
					{
						Utils.Log("Problema al Obtener el PDF...");
						label2.Font = new Font("Helvetica-Light", 12, FontStyle.Regular);
						label2.ForeColor = Color.Red;
						linkLabel1.Visible = true;
						linkLabel2.Visible = true ;
						return;
					}

					if (e.Result.Contenido.Length == 1)
					{
						Utils.Log("Restauracion Realizada Correctamente...");
						label2.Font = new Font("Helvetica-Light", 12, FontStyle.Regular);
						label2.ForeColor = Color.Blue; 
						linkLabel2.Visible = true ;
						timer1.Stop();
						Thread.Sleep(5000);
						Application.Exit();
					}
					else
					{
						Utils.Log("Restauracion Realizada Correctamente...");
						Utils.Log( string.Format ("Contenido Recibido => {0}" , e.Result .Contenido .Length ));
						timer1.Stop();
						if (!System.IO.Directory.Exists(RutaPDFTemp))
							System.IO.Directory.CreateDirectory(RutaPDFTemp);

						if (System.IO.File.Exists(RutaFilePDF))
							System.IO.File.Delete(RutaFilePDF);

						System.IO.File.WriteAllBytes(RutaFilePDF, e.Result.Contenido);
						Process.Start(RutaFilePDF);
						Application.Exit();
					}
				}
				else
				{ 

					timer1.Stop();
					label2.Font = new Font("Helvetica-Light", 12, FontStyle.Regular);
					label2.ForeColor = Color.Red;
					linkLabel1.Visible = true;
					linkLabel2.Visible = true ;
					Thread.Sleep(5000);
					Application.Exit();
				}
			}
			catch (Exception ex)
			{
				Utils.Log("ERROR NO CONTROLADO PROCESOFACTURACION => " + ex.Message);
				timer1.Stop();
				label2.Text = ex.Message ;
				label2.Font = new Font("Helvetica-Light", 12, FontStyle.Regular);
				label2.ForeColor = Color.Red;
				linkLabel1.Visible = true;
				linkLabel2.Visible = true;
				Thread.Sleep(5000);
				Application.Exit();
			}


		}
		int tiempo = 0;

		private void timer1_Tick(object sender, EventArgs e)
		{
			tiempo++;
			label3.Text = tiempo.ToString("00");
			if (tiempo>TimeoutExceed )
			{
				timer1.Stop();
				MessageBox.Show("Tiempo de ejecucion excedio el limite establecido!","Tecnologia Textil S.A.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				Application.Exit();
			}
		}

		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			//ServiceEfact.ServiceClient client = new ServiceEfact.ServiceClient();
			//try
			//{

			//}
			//catch (Exception ex)
			//{
			//	Utils.Log("ERROR NO CONTROLADO => " + ex.Message);
			//}
			FrmListLogs x = new FrmListLogs();
			x.ShowDialog();
		}

		private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Application.Exit();
		}

        private void label2_Click(object sender, EventArgs e)
        {

        }
	}
}
