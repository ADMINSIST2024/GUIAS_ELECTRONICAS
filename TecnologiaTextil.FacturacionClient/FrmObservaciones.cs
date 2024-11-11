using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TecnologiaTextil.FacturacionClient.DataAccess;

namespace TecnologiaTextil.FacturacionClient
{
    public partial class FrmObservaciones : Form
    {
        

     
        public FrmObservaciones(string ticket)
        {
            InitializeComponent();

            //Application.OpenForms["FrmObservaciones"].Show();
          //  Obtener_Observaciones(ticket);
          //  Application.OpenForms["FrmEnvio"].Close();
        }



        FrmEnvio obj_frmenvio = new FrmEnvio();
        DATabla dtabla = new DATabla();
       

        private void FrmObservaciones_Load(object sender, EventArgs e)
        {
            Application.OpenForms["FrmObservaciones"].Show();
            
           


        }
        public void Obtener_Observaciones(string ticket)
        {
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
                btnReintentar.Visible = true;
            }
            else { btnReintentar.Visible = false; }

          
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnObtenerCDR_Click(object sender, EventArgs e)
        {

            string ticket = txtticket.Text;
            ResultFile rcdr = null;
            rcdr = obj_frmenvio.ObtenerCDR(ticket, 0);
            if (rcdr.code.Equals("0"))
            {
               
                Utils.Informacion("Archivo CDR Obtenido Correctamente...");
                MessageBox.Show("CDR Obtenido Correctamente", "Codigo - " + rcdr.code);
            }
            else
            {
                dtabla.UpdateTabla(Profile.NombreTabla, "2", "Error CDR -> " + rcdr.description);
                MessageBox.Show(rcdr.description, "Codigo - " + rcdr.code);
            }
        }

        private void btnObtenerXML_Click(object sender, EventArgs e)
        {
            string ticket = txtticket.Text;
            ResultFile rxml = null;
            rxml = obj_frmenvio.ObtenerXML(ticket, 0);
            //*******************************************************
            if (rxml.code.Equals("0"))
            {
                Utils.Informacion("Archivo XML Obtenido Correctamente...");
                MessageBox.Show("XML Obtenido Correctamente", "Codigo - " + rxml.code);
            }
            else
            {
                dtabla.UpdateTabla(Profile.NombreTabla, "1", "Error XML -> " + rxml.description);
                MessageBox.Show(rxml.description, "Codigo - " + rxml.code);
            }
        }

        private void btnObtenerPDF_Click(object sender, EventArgs e)
        {
            string ticket = txtticket.Text;
            ResultFile rpdf = null;
            rpdf = obj_frmenvio.ObtenerXML(ticket, 0);
            string pdforigen = Profile.RutaCarpeta + @"PDF\" + Profile.NumeroDocumento + ".pdf";
            string pdfdestino = Utils.GenerarRutaCopia();
            Utils.Debug("Ruta Origen PDF => " + pdforigen);
            Utils.Debug("Ruta Destino PDF => " + pdfdestino);
            //*******************************************************
            if (rpdf.code.Equals("0"))
            {
                Utils.Informacion("Archivo PDF Obtenido Correctamente...");
                MessageBox.Show("XML Obtenido Correctamente", "Codigo - " + rpdf.code);

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
            {
                dtabla.UpdateTabla(Profile.NombreTabla, "1", "Error PDF -> " + rpdf.description);
                MessageBox.Show(rpdf.description, "Codigo - " + rpdf.code);
            }
        }

       

        public void ReintentarObtenerDocumentos(string ticket, string proceso)
        {
            Utils.Informacion("Ticket usado para recuperar documentos es : " + ticket);

            //*******************************************************
            if (proceso == "CDR")
            {
                ResultFile rcdr = null;
                rcdr = obj_frmenvio.ObtenerCDR(ticket, 0);

                //******************************************************* 
                if (rcdr.code.Equals("0"))
                {
                    dtabla.UpdateTicket(rcdr.code, rcdr.description, ticket, "CDR", "0", Profile.TipoDocumento, Profile.NumeroDocumento);

                    Utils.Informacion("Archivo CDR Obtenido Correctamente...");
                    //*******************************************************
                    ResultFile rxml = null;
                    rxml = obj_frmenvio.ObtenerXML(ticket, 0);
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
                        rpdf = obj_frmenvio.ObtenerPDF(ticket, 0);
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
                rxml = obj_frmenvio.ObtenerXML(ticket, 0);
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
                    rpdf =  obj_frmenvio.ObtenerPDF(ticket, 0);
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
                rpdf = obj_frmenvio.ObtenerPDF(ticket, 0);
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

        private void btnReintentar_Click(object sender, EventArgs e)
        {
            string ticket = txtticket.Text;
            string proceso = txtproceso.Text;
            ReintentarObtenerDocumentos(ticket, proceso);
        }

      
    }
}
