using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TecnologiaTextil.FacturacionClientXP
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Utils.Log("Iniciando Programa Cliente XP");
            if (args.Length == 0)
            {
                Utils.Log("Parametro vacio, asegurese que se esta enviando un valor");
                MessageBox.Show("Parametro vacio, asegurese que se esta enviando un valor", "Aviso", MessageBoxButtons.OK , MessageBoxIcon.Error );
                Application.Exit();
                return;
            }
			if (args.Length > 1)
			{
				string operacion = args[1];
				if (operacion.Equals("LOGS"))
				{
					FrmListLogs form = new FrmListLogs();
					form.ShowDialog();
				}
				else {
					MessageBox.Show("No se puede procesar la informacion enviada", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return; 
				}
			}
			else
			{
				Application.Run(new FrmEnvio(args[0]));
			}
        }
    }
}
