using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TecnologiaTextil.FacturacionClient.DataAccess;

namespace GestionEfact
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        DATabla obj = new DATabla();
        private void Form1_Load(object sender, EventArgs e)
        {
            gvListado.DataSource = obj.Obtener_Observaciones_Estados();
            
        }
    }
}
