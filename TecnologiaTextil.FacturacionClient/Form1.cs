using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TecnologiaTextil.FacturacionClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            rpdf = getPDF("eebbc980-989f-4772-80c9-7de5ba660ce8", "D:\\", "prueba.pdf");
            //rpdf = getPDF("ebb04f15-b775-43cb-9c99-1393bc0a148a", "D:\\", "prueba2.pdf");
            //rpdf = getPDF("b928d89e-a95e-45f0-9f2e-990b4b58be55", "D:\\", "prueba3.pdf");
        }
        // AMBIENTE DE PRODUCCION
        private const string BaseUrlEnvio = "https://ose.efact.pe/api-efact-ose/v1/document";
        private const string BaseUrlToken = "https://ose.efact.pe/api-efact-ose/oauth/token";
        private const string BaseUrlCdr = "https://ose.efact.pe/api-efact-ose/v1/cdr/";
        private const string BaseUrlXML = "https://ose.efact.pe/api-efact-ose/v1/xml/";
        private const string BaseUrlPDF = "https://ose.efact.pe/api-efact-ose/v1/pdf/";
        ResultFile rpdf;
      

        public ResultFile getPDF(string ticket, string pathArchivo, string nombrepdf)
        {
            ServiceClient a = new ServiceClient();

            IRestResponse response;
            ResultFile retorno;
            try
            {

                string token =  a.getToken();
                if (!token.Equals("ERROR"))
                {
                    var client = new RestClient(BaseUrlPDF + ticket);
                    var request = new RestRequest(Method.GET);
                    request.AddHeader("authorization", "Bearer " + token);
                    response = client.Execute(request);

                    Utils.Debug("Mensaje devuelvo RAW (PDF) => " + response.Content.ToString());
                    Utils.Debug("");
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        System.IO.File.WriteAllBytes(pathArchivo + @"PDF\" + nombrepdf, response.RawBytes);
                        retorno = a.SetOK("PDF Obtenido Correctamente");
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.Accepted ||
                             response.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                    {
                        retorno = JsonConvert.DeserializeObject<ResultFile>(response.Content.ToString());
                        Utils.Warning(string.Format("Error N° [ {0} ] - ({1})", retorno.code, retorno.description));
                    }
                    else
                        retorno = a.SetError("1", string.Format("Error Obtener PDF codigo ({0}) - descripcion: {1}", (int)response.StatusCode, response.StatusDescription));
                }
                else
                    retorno = a.SetError("3", "Problema de obtenecion de Token de Autorizacion");
            }
            catch (Exception ex)
            {
                retorno =   a.SetFatal("2", "Error PDF: " + ex.Message);
            }

            return retorno;
        }

   

    }
}
