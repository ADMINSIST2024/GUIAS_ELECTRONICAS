using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TecnologiaTextil.FacturacionClientXP
{
	public partial class FrmListLogs : Form
	{
		public FrmListLogs()
		{
			InitializeComponent();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			this.Close();
		}
		List<ServiceEfact.ResultListLog> data;
		ServiceEfact.ServiceClient client = new ServiceEfact.ServiceClient();
		private void button1_Click(object sender, EventArgs e)
		{
			DataGridViewRow fila = dataGridView1.CurrentRow;
			if (fila == null)
			{
				MessageBox.Show("Debes de Elegir un Archivo a mostrar", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			Utils.Log("Invocacion de metodo obtener log!");
			//client.ClientCredentials.Windows.AllowedImpersonationLevel =
			//System.Security.Principal.TokenImpersonationLevel.Impersonation;
			string Rutalog = Utils.RutaLog();
			string RutaArchivoTemp = string.Format("{0}templog.txt", Rutalog);
			if (System.IO.File.Exists(RutaArchivoTemp))
				System.IO.File.Delete(RutaArchivoTemp);
			string nombre = "";
			nombre = fila.Cells["Nombre"].Value .ToString();
			byte[] resultado = client.ObtenerLog(nombre);
			if (resultado == null)
			{
				Utils.Log("Problemas al Obtener el Archivo Log");
				MessageBox.Show("Problemas al Obtener el Archivo Log", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			Utils.Log("Escribiendo archivo PDF");
			System.IO.File.WriteAllBytes(RutaArchivoTemp, resultado);
			ProcessStartInfo exe = new ProcessStartInfo(RutaArchivoTemp);
			exe.WindowStyle = ProcessWindowStyle.Maximized;
			System.Diagnostics.Process.Start(exe);
		}
		void Consultar(bool filtrar)
		{
			List<ServiceEfact.ResultListLog> temp; 
			if (filtrar)
				temp = data.ToList().Where(x => x.Nombre.Contains(textBox1.Text.ToUpper().Trim())).ToList();
			else
				temp = data; 
			dataGridView1.DataSource = temp;
		}
		private void FrmListLogs_Load(object sender, EventArgs e)
		{
			data = client.ObtenerListaLog(Environment.MachineName).ToList();

			Consultar(false);
		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			Consultar(true);
		}
	}
}
