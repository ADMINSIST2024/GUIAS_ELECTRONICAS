using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecnologiaTextil.FacturacionClient
{
  public   class Profile
    {
        public static  DataTable Tabla { get; set; }
        public static string  NombreTabla { get; set; }
        public static string  RutaCarpeta { get; set; }
        public static string NumeroDocumento { get; set; }
        public static string  TipoDocumento { get; set; }
        public static bool IndicadorImpresion { get; set; }
        public static string  RutaCopiaPDF { get; set; }
        public static string  Usuario { get; set; }
        public static int ERROR_APPLICATION { get; set; }
        public static int TimeoutEnvio { get; set; }

        public static int Cia { get; set; }
        public static  void AsignaVariables() {
            if (Tabla != null )
            {
               // NumeroDocumento = Tabla.Rows[0]["doc163"].ToString();
               // TipoDocumento = Tabla.Rows[0]["tdo163"].ToString();
               // RutaCarpeta = Tabla.Rows[0]["rut163"].ToString();
                Cia = Convert.ToInt32(Tabla.Rows[0]["CIA163"].ToString());
                NumeroDocumento = "20297986130-03-B001-00003910";
                TipoDocumento = "BV";
                RutaCarpeta = @"D:\\\\PRUEBACSV\\";
               // RutaCarpeta = Tabla.Rows[0]["rut163"].ToString();

                //fACTURAS
                //  NumeroDocumento = "20297986130-08-F005-00000001";
                //  TipoDocumento = "08";
                //  RutaCarpeta = "\\\\192.168.166.22\\factelectr\\10\\08\\";


            }
            //_numero = _dt.Rows(0).Item("doc163").ToString  'numero
            //_ruta = _dt.Rows(0).Item("rut163").ToString  'ruta
            //_usua = _dt.Rows(0).Item("usu163").ToString  'usuario
            //_pass = _dt.Rows(0).Item("pas163").ToString  'password
            //_codtdc = _dt.Rows(0).Item("tdo163").ToString  'tipo de documento
        }
    }
}
