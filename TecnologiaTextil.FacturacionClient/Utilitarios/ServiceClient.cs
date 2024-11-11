using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using TecnologiaTextil.FacturacionClient.Utilitarios;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Security.Policy;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace TecnologiaTextil.FacturacionClient
{
    public class ServiceClient
    {
        // AMBIENTE DE PRODUCCION
        private const string BaseUrlEnvio = "https://ose.efact.pe/api-efact-ose/v1/document";
        private const string BaseUrlToken = "https://ose.efact.pe/api-efact-ose/oauth/token";
        private const string BaseUrlCdr = "https://ose.efact.pe/api-efact-ose/v1/cdr/";
        private const string BaseUrlXML = "https://ose.efact.pe/api-efact-ose/v1/xml/";
        private const string BaseUrlPDF = "https://ose.efact.pe/api-efact-ose/v1/pdf/";

        //AMBIENTE DE PRUEBAS
        // private const string BaseUrlEnvio = "https://ose-gw1.efact.pe/api-efact-ose/v1/document";
        //private const string BaseUrlToken = "https://ose-gw1.efact.pe/api-efact-ose/oauth/token";
        //private const string BaseUrlCdr = "https://ose-gw1.efact.pe/api-efact-ose/v1/cdr/";
        //private const string BaseUrlXML = "https://ose-gw1.efact.pe/api-efact-ose/v1/xml/";
        //private const string BaseUrlPDF = "https://ose-gw1.efact.pe/api-efact-ose/v1/pdf/";





        //private ServiceClient()
        //{
        //    ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(OnValidationCallback); 
        //}


        public static bool OnValidationCallback(object sender, System.Security.Cryptography.X509Certificates.X509Certificate cert, X509Chain chain, SslPolicyErrors errors)
        {
            //Simplemente devolvemos un true indicando que el certificado es válido --> es lo mismo que pulsar el botón continuar.
            return true;
        }

        public ResultFile SetFatal(string codigo, string mensaje)
        {
            Utils.Fatal(mensaje);
            return new ResultFile() { code = codigo, description = mensaje };
        }
        public ResultFile SetError(string codigo, string mensaje)
        {
            Utils.Error(mensaje);
            return new ResultFile() { code = codigo, description = mensaje };
        }
        public ResultFile SetOK(string mensaje)
        {
            Utils.Informacion(mensaje);
            return new ResultFile() { code = "0", description = mensaje };
        }


        public ResultFile getCDR(string ticket, string pathArchivo, string nombrecdr)
        {
            IRestResponse response = null;
            ResultFile retorno;
            try
            {
                string token = getToken();
                if (!token.Equals("ERROR"))
                { 
					var client = new RestClient(BaseUrlCdr  + ticket);
					var request = new RestRequest(Method.GET);
                    request.AddHeader("authorization", "Bearer " + token);
                    response = client.Execute(request);

                    Utils.Debug("Mensaje devuelvo RAW (CDR) => " + response.Content.ToString());
                    Utils.Debug("");
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        using (var fs = new StreamWriter(pathArchivo + @"ZIP\" + nombrecdr, false, Encoding.UTF8))
                        {
                            fs.Write(response.Content.ToString());
                            fs.Flush();
                            fs.Close();
                        }
                        retorno = SetOK("CDR Obtenido Correctamente");
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.PreconditionFailed ||
                        response.StatusCode == System.Net.HttpStatusCode.Accepted)
                    {

                        retorno = JsonConvert.DeserializeObject<ResultFile>(response.Content.ToString());
                        Utils.Error(string.Format("Error N° [ {0} ] - ({1})", retorno.code, retorno.description));
                    }
                    else
                        retorno = SetError("1", String.Format("Error al Obtener el CDR cuyo Status es [{0}]",  response.StatusDescription));
                }
                else
                    retorno = SetError("3", "Problema de obtenecion de Token de Autorizacion");
            }
            catch (Exception ex)
            {
                retorno = SetFatal("2", "Error CDR: " + ex.Message);
            }
            return retorno;
        }
        public ResultFile getXML(string ticket, string pathArchivo, string nombrexml)
        {
            IRestResponse response;
            ResultFile retorno;
            try
            {

                string token = getToken();
                if (!token.Equals("ERROR"))
                { 
					var client = new RestClient( BaseUrlXML+ ticket);
					var request = new RestRequest(Method.GET);
                    request.AddHeader("authorization", "Bearer " + token);
                    response = client.Execute(request);

                    Utils.Debug("Mensaje devuelvo RAW (XML) => " + response.Content.ToString());
                    Utils.Debug("");
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        using (var fs = new StreamWriter(pathArchivo + @"XML\" + nombrexml, false, Encoding.UTF8))
                        {
                            fs.Write(response.Content.ToString());
                            fs.Flush();
                            fs.Close();
                        }
                        retorno = SetOK("XML Obtenido Correctamente");
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.PreconditionFailed ||
                        response.StatusCode == System.Net.HttpStatusCode.Accepted)
                    {
                        retorno = JsonConvert.DeserializeObject<ResultFile>(response.Content.ToString());
                        Utils.Error(string.Format("Error N° [ {0} ] - ({1})", retorno.code, retorno.description));
                    }
                    else
                        retorno = SetError("1", string.Format("Error al Obtener el XML cuyo Status es [{0}]", response.StatusDescription));
                }
                else
                    retorno = SetError("3", "Problema de obtenecion de Token de Autorizacion");
            }
            catch (Exception ex)
            {
                retorno = SetFatal("2", "Error XML: " + ex.Message);
            }
            return retorno;
        }
        public ResultFile getPDF(string ticket, string pathArchivo, string nombrepdf)
        {
            IRestResponse response;
            ResultFile retorno;
            try
            {

                string token = getToken();
                if (!token.Equals("ERROR"))
                { 
					var client = new RestClient( BaseUrlPDF+ ticket);
					var request = new RestRequest(Method.GET);
                    request.AddHeader("authorization", "Bearer " + token);
                    response = client.Execute(request);

                    Utils.Debug("Mensaje devuelvo RAW (PDF) => " + response.Content.ToString());
                    Utils.Debug("");
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        System.IO.File.WriteAllBytes(pathArchivo + @"PDF\" + nombrepdf, response.RawBytes);
                        retorno = SetOK("PDF Obtenido Correctamente");
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.Accepted ||
                             response.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                    {
                        retorno = JsonConvert.DeserializeObject<ResultFile>(response.Content.ToString());
                        Utils.Warning(string.Format("Error N° [ {0} ] - ({1})", retorno.code, retorno.description));
                    }
                    else
                        retorno = SetError("1", string.Format("Error Obtener PDF codigo ({0}) - descripcion: {1}", (int) response.StatusCode, response.StatusDescription));
                }
                else
                    retorno = SetError("3", "Problema de obtenecion de Token de Autorizacion");
            }
            catch (Exception ex)
            {
                retorno = SetFatal("2", "Error PDF: " + ex.Message);
            }

            return retorno;
        }

        public ResultResponse EnviarDocumento(string pathdocument)
        {
            IRestResponse response;
            ResultResponse retorno;
            try
            {
                Utils.Informacion("############# Iniciando el Envio de Documento #############");
                Utils.Informacion(string.Format("Tipo y Numero de Documento: {0}-{1}", Profile.TipoDocumento, Profile.NumeroDocumento));

                string token = getToken();
                if (!token.Equals("ERROR"))
                {
                    var client = new RestClient(BaseUrlEnvio);
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("cache-control", "no-cache");
                    request.AddHeader("authorization", "Bearer " + token);
                    request.AlwaysMultipartFormData = true;
                    FileStream fs = new FileStream(pathdocument, FileMode.Open, FileAccess.Read);
                    byte[] data = new byte[fs.Length];
                    fs.Read(data, 0, data.Length);
                    fs.Close();

                    string filename = System.IO.Path.GetFileName(pathdocument);
                    request.AddFile("file", data, filename);
                    response = client.Execute(request);

                    Utils.Debug("Mensaje devuelvo RAW (Envio documento) => " + response.Content.ToString());

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        Utils.Informacion("Proceso de Envio de Documento OK");
                        retorno = JsonConvert.DeserializeObject<ResultResponse>(response.Content.ToString());
                    }
                    else
                    {
                        Utils.Error(string.Format("Obtener Respuesta de envio documento cuyo Status es [{0}]", response.StatusDescription));
                        retorno = new ResultResponse() { code = "1", description = response.StatusDescription };
                        Utils.Error(string.Format("- mahd - Obtener Respuesta de envio documento cuyo Status es [{0}]", response.ErrorMessage));
                        Utils.Error(string.Format("- mahd - Obtener Respuesta de envio documento cuyo Status es [{0}]", response.ErrorException));                        
                    }
                }
                else
                {
                    Utils.Error("Problema de obtenecion de Token de Autorizacion");
                    retorno = new ResultResponse() { code = "3", description = "Problema de obtenecion de Token de Autorizacion" };
                    //retorno = SetMessage(3, "Problema de obtenecion de Token de Autorizacion");                    
                }

            }
            catch (Exception ex)
            {
                Utils.Fatal("Error envio documento: " + ex.Message);
                retorno = new ResultResponse() { code = "2", description = "Error envio documento: " + ex.Message };
            }
            return retorno;
        }

        public string getToken()
        {
           
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(OnValidationCallback);                
            }
            catch (Exception ex)
            {
                Utils.Error(string.Format("Error de Certificado : {0}", ex.Message));
                return "ERROR";
            }            

            string password = ConfigurationManager.AppSettings["Key"].ToString();
            string user = ConfigurationManager.AppSettings["RUC"].ToString();
            string grantType = "password"; 
			var client = new RestClient(BaseUrlToken);
			var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddHeader("authorization", "Basic Y2xpZW50OnNlY3JldA==");
            request.AddParameter("application/x-www-form-urlencoded", string.Format("password={0}&username={1}&grant_type={2}", password, user, grantType), ParameterType.RequestBody);         
                                 
            IRestResponse response = client.Execute(request);
            Utils.Debug("Mensaje Respuesta Token RAW => " + response.Content.ToString());
             
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                ResultToken token = JsonConvert.DeserializeObject<ResultToken>(response.Content.ToString());
                Utils.Informacion("Proceso de Obtencion de Token OK");
                return token.access_token;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                Utils.Warning(string.Format("Error Autentificacion II : {0}", response.Content.ToString()));
                return "ERROR";
            }
            else
            {
                Utils.Error(string.Format("Error Autentificacion : {0}", response.StatusDescription));                
                Utils.Error(string.Format("Error Autentificacion : {0}", response.ErrorMessage));
                return "ERROR";
            }

        }

    }
}

