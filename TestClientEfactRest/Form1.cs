using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestClientEfactRest
{
	public partial class Form1 : Form
	{
		public class ResultToken
		{
			public string access_token { get; set; }
			public string token_type { get; set; }
			public string refresh_token { get; set; }
			public string scope { get; set; }
			public int expires_in { get; set; }
		}
		public class ResultResponse
		{
			public string code { get; set; }
			public string description { get; set; }
		}
		public class ResultFile
		{
			public string description { get; set; }
			public int code { get; set; }
			public string mensaje { get; set; }
		}
		private const string BaseUrlEnvio = "https://ose.efact.pe/api-efact-ose/v1/document";
		private const string BaseUrlToken = "	https://ose.efact.pe/api-efact-ose/oauth/token";
		private const string BaseUrlCdr = "https://ose.efact.pe/api-efact-ose/v1/cdr/";
		private const string BaseUrlTicket = "https://ose.efact.pe/api-efact-ose/v1/ticket/";
		private const string BaseUrlXML = "https://ose.efact.pe/api-efact-ose/v1/xml/";
		private const string BaseUrlPDF = "https://ose.efact.pe/api-efact-ose/v1/pdf/";

		//{"code":"0","description":
		//"e5441b44-7693-45a3-8b1d-127e1c2ab100"}
		//d3a1c58b-ba7f-4d42-bf0a-d4dfb5a55d02
		//78a6f841-9aa7-4679-87d2-584745f36b06       -   20297986130-01-FUW2-0000891.csv
		//e21f62aa-47cc-4a2f-bda1-a516fe0fdcdd       -   20297986130-07-FU01-100001.csv
		private ResultFile getCDR(string ticket, string pathArchivo, string nombrecdr)
		{
			//var client = new RestClient("https://ose-gw1.efact.pe:443/api-efact-ose/v1/cdr/" + ticket);
			var client = new RestClient( BaseUrlCdr  + ticket);
			var request = new RestRequest(Method.GET);
			request.AddHeader("authorization", "Bearer " + getToken());
			IRestResponse response = client.Execute(request);

			using (var fs = new StreamWriter(pathArchivo + @"\" + nombrecdr, false, Encoding.UTF8))
			{
				fs.Write(response.Content.ToString());
				fs.Flush();
				fs.Close();
			}
			//if (response.StatusCode == System.Net.HttpStatusCode.OK )

			return new ResultFile()
			{
				code = (int)response.StatusCode,
				description = response.StatusDescription,
				mensaje = response.Content.ToString()
			};
		}
		private ResultFile getTicketPerdido(string Documento)
		{
			IRestResponse response = null; 

			string token = getToken();
			if (!token.Equals("ERROR"))
			{
				var client = new RestClient(BaseUrlTicket + Documento);
				var request = new RestRequest(Method.GET);
				request.AddHeader("authorization", "Bearer " + token);
				response = client.Execute(request);

				textBox5.Text = response.Content.ToString();
			}

			return new ResultFile()
			{
				code = (int)response.StatusCode,
				description = response.StatusDescription,
				mensaje = response.Content.ToString()
			};
		}
		private ResultFile getXML(string ticket, string pathArchivo, string nombrexml)
		{
			//var client = new RestClient("https://ose-gw1.efact.pe:443/api-efact-ose/v1/xml/" + ticket);
			var client = new RestClient(BaseUrlXML  + ticket);
			var request = new RestRequest(Method.GET);
			request.AddHeader("authorization", "Bearer " + getToken());
			IRestResponse response = client.Execute(request);
			using (var fs = new StreamWriter(pathArchivo + @"\" + nombrexml, false, Encoding.UTF8))
			{
				fs.Write(response.Content.ToString());
				fs.Flush();
				fs.Close();
			}
			//if (response.StatusCode == System.Net.HttpStatusCode.OK )

			return new ResultFile()
			{
				code = (int)response.StatusCode,
				description = response.StatusDescription,
				mensaje = response.Content.ToString()
			};
		}
		private ResultFile getPDF(string ticket, string pathArchivo, string nombrepdf)
		{
			//var client = new RestClient("https://ose-gw1.efact.pe:443/api-efact-ose/v1/pdf/" + ticket);
			var client = new RestClient(BaseUrlPDF  + ticket);
			var request = new RestRequest(Method.GET);
			request.AddHeader("authorization", "Bearer " + getToken());
			IRestResponse response = client.Execute(request);
			System.IO.File.WriteAllBytes(pathArchivo + @"\" + nombrepdf, response.RawBytes);

			return new ResultFile()
			{
				code = (int)response.StatusCode,
				description = response.StatusDescription,
				mensaje = response.Content.ToString()
			};
		}
		private string getNombreArchivo()
		{
			return DateTime.Now.ToLongTimeString().Replace(":", "").Substring(0, 6);
		}
		private ResultResponse EnviarDocumento(string pathdocument)
		{
			var client = new RestClient("https://ose-gw1.efact.pe:443/api-efact-ose/v1/document");
			var request = new RestRequest(Method.POST);
			request.AddHeader("cache-control", "no-cache");
			request.AddHeader("authorization", "Bearer " + getToken());
			request.AlwaysMultipartFormData = true;
			FileStream fs = new FileStream(pathdocument, FileMode.Open, FileAccess.Read);
			byte[] data = new byte[fs.Length];
			fs.Read(data, 0, data.Length);
			fs.Close();

			string filename = System.IO.Path.GetFileName(pathdocument);
			request.AddFile("file", data, filename);
			IRestResponse response = client.Execute(request);

			ResultResponse respdocument = JsonConvert.DeserializeObject<ResultResponse>(response.Content.ToString());
			return respdocument;
		}

		private string getToken()
		{
			string password = "623973f54ab2e682b1e35538f7926042da207a2b9949abd639cb01ec4c0963fb";
			string user = "20297986130";
			string grantType = "password";
			var client = new RestClient(BaseUrlToken);// "https://ose-gw1.efact.pe:443/api-efact-ose/oauth/token");
			var request = new RestRequest(Method.POST);
			//request.AddHeader("postman-token", "cfeda032-0500-9d6a-2078-ce9728399901");
			request.AddHeader("cache-control", "no-cache");
			request.AddHeader("content-type", "application/x-www-form-urlencoded");
			request.AddHeader("authorization", "Basic Y2xpZW50OnNlY3JldA==");
			request.AddParameter("application/x-www-form-urlencoded", string.Format("password={0}&username={1}&grant_type={2}", password, user, grantType), ParameterType.RequestBody);
			IRestResponse response = client.Execute(request);
			ResultToken token = JsonConvert.DeserializeObject<ResultToken>(response.Content.ToString());
			//textBox1.Text = token.access_token;
			return token.access_token;
		}



		public Form1()
		{
			InitializeComponent();
		}

		string ruta = @"D:\BASES\FACTURA ELECTRONICA\";
		private void button2_Click(object sender, EventArgs e)
		{

			ResultResponse respdocument = EnviarDocumento(textBox2.Text);
			textBox3.Text = respdocument.code + " - [  " + respdocument.description + "  ]";
		}


		private void button3_Click(object sender, EventArgs e)
		{
			textBox3.Text = "INICIANDO RECUPERACION...";
			ResultFile respuesta = getCDR(textBox4.Text, ruta + @"\Pruebas",
											  "CDR" + getNombreArchivo() + ".xml");
			textBox5.Text = respuesta.mensaje;
			if (respuesta.code == ((int)HttpStatusCode.OK))
				textBox3.Text = "OK";
			else
				textBox3.Text = "ERROR";

		}

		private void button4_Click(object sender, EventArgs e)
		{
			textBox3.Text = "INICIANDO RECUPERACION...";
			ResultFile respuesta = getXML(textBox4.Text, ruta + @"\Pruebas",
											  "F00-" + getNombreArchivo() + ".xml");
			textBox5.Text = respuesta.mensaje;
			if (respuesta.code == ((int)HttpStatusCode.OK))
				textBox3.Text = "OK";
			else
				textBox3.Text = "ERROR";
		}

		private void button5_Click(object sender, EventArgs e)
		{
			textBox3.Text = "INICIANDO RECUPERACION...";
			ResultFile respuesta = getPDF(textBox4.Text, ruta + @"\Pruebas",
											  "PDF-" + getNombreArchivo() + ".pdf");
			textBox5.Text = respuesta.mensaje;
			if (respuesta.code == ((int)HttpStatusCode.OK))
				textBox3.Text = "OK";
			else
				textBox3.Text = "ERROR";

		}

		private void Form1_Load(object sender, EventArgs e)
		{
			if (!System.IO.Directory.Exists(ruta + @"\Pruebas"))
			{
				System.IO.Directory.CreateDirectory(ruta + @"\Pruebas");
			}
		}

		private void button6_Click(object sender, EventArgs e)
		{
			textBox3.Text = "INICIANDO RECUPERACION...";
			ResultFile respuesta = getTicketPerdido ( textBox4.Text   );
			textBox5.Text = respuesta.mensaje;
			if (respuesta.code == ((int)HttpStatusCode.OK))
				textBox3.Text = "OK";
			else
				textBox3.Text = "ERROR";
		}
	}
}
