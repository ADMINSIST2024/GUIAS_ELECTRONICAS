using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TecnologiaTextil.FacturacionClient.DataAccess;

namespace TecnologiaTextil.FacturacionClient
{
	static class Program
	{

		[STAThread]
		static int Main(String[] args) //10 FVT163COB2
        {
            
			
			Utils.Informacion("***********************************************");
			DATabla servicetabla = new DATabla();
			Utils.Informacion("Iniciando Aplicacion Global");
			Utils.Informacion("***********************************************");
			if (args.Length == 0)
			{
				Utils.Warning("Parametro vacio, asegurese que se esta enviando un valor");
				Application.Exit();
				return 1;
			}
			Profile.NombreTabla = args[1];

		

			if (args.Length > 1)
			//	Profile.Usuario = args[1];
            Profile.Usuario =null;
            //MessageBox.Show("ENTRO");
            // MessageBox.Show(args[0]);
            //MessageBox.Show(args[1]);

            Utils.Informacion("Invocando Informacion de la Tabla => " + Profile.NombreTabla);
			Profile.Tabla = servicetabla.GetTabla(Profile.NombreTabla);
			Profile.TimeoutEnvio = int.Parse(ConfigurationManager.AppSettings["TimeoutEnvio"].ToString());
			Utils.Informacion("");
			Utils.Informacion("");
			Utils.Informacion("====================================================================");
			Utils.Informacion("Iniciando Programa de Facturacion Electronica EFACT REST");
			Utils.Informacion("====================================================================");
			Utils.Informacion("");
			Utils.Informacion("");
			
			bool FacturacionActiva = Utils.EstadoBool(servicetabla.GetValorTABCONTR("840", "VALTCO"));
			
			if (FacturacionActiva)
			{
				//MessageBox.Show("ENTRO2");
				if (Profile.Tabla.Rows.Count > 0)
				{
				
				Profile.AsignaVariables();
				
				Profile.IndicadorImpresion = Utils.EstadoBool(servicetabla.GetValorTABCONTR("809", "VALTCO"));
				
				Utils.Informacion("Valor de Indicacion de Impresion -> " + Profile.IndicadorImpresion.ToString());
				
				Profile.RutaCopiaPDF = servicetabla.GetValorTABCONTR("810", "TEXTCO");
			
				Utils.Informacion("Valor de Ruta Copia PDF -> " + Profile.RutaCopiaPDF);
			
				Application.EnableVisualStyles();
					Application.SetCompatibleTextRenderingDefault(false);

				
				//MessageBox.Show(Profile.Tabla.Rows[0]["FLA163"].ToString().Trim().Equals("1").ToString());
				if (Profile.Tabla.Rows[0]["FLA163"].ToString().Trim().Equals("1"))
					{
					
					Utils.Informacion(string.Format("***************** Envio de documento {0}-{1} ****************", Profile.TipoDocumento, Profile.NumeroDocumento));
                       	Application.Run(new FrmEnvio());
                     
                    }
					else
					{
						Utils.Informacion("Opcion de Restauracion de documentos");
						Application.Run(new FrmRestaurar());
					}
				}
				else
				{
					Utils.Warning("Tabla no Existe , Verifique si tabla " + Profile.NombreTabla + " existe");
				}
			}
			else {
				Profile.ERROR_APPLICATION = 1;
				Utils.Warning("Facturacion se DESACTIVO, comuniquese con sistemas!");
			}
			return Profile.ERROR_APPLICATION;

			
        }
	}
}
