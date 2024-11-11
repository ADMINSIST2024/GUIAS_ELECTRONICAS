using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace TecnologiaTextil.FacturacionClient.DataAccess
{
	public class DATabla : DBUtility
	{

		public string GetTokenDocumento(int codcia, string codtdc, int anio, string serie, int numero)
		{
			string retorno = "";
			DataTable result = new DataTable();
			OleDbConnection cn = Conectar();

			if (!codtdc.Equals("CR"))
				result = ExecuteToDataTable(cn, string.Format("SELECT TOEMFE FROM  FVTMOVFE WHERE CODCIA ={0} AND CODTDC='{1}' AND ANOCC1 ={2} AND CODSER='{3}'  AND NROCC1 = {4} AND CSCCC1=0", codcia, codtdc, anio, serie, numero));
			else
				result = ExecuteToDataTable(cn, string.Format("SELECT TENCFE FROM CTAPRCFE WHERE CODCIA ={0} AND ANOCR1={1} AND SERCR1 ='{2}' AND NROCR1={3} ", codcia, anio, serie, numero));

			cn.Close();
			if (result.Rows.Count > 0)
				retorno = result.Rows[0][0].ToString();

			return retorno;
		}
		public DataTable GetTablaRestaurarErrores(string archivo)
		{
			DataTable retorno = new DataTable();
			OleDbConnection cn = Conectar();
			retorno = ExecuteToDataTable(cn, string.Format("SELECT * FROM {0} WHERE STA163 = 2 ORDER BY 1 ASC ", archivo));
			cn.Close();
			return retorno;
		}
		public DataTable GetTablaRestaurar(string archivo)
		{
			DataTable retorno = new DataTable();
			OleDbConnection cn = Conectar();
			retorno = ExecuteToDataTable(cn, string.Format("SELECT * FROM {0} WHERE STA163 = 0 ORDER BY 1 ASC ", archivo));
			cn.Close();
			return retorno;
		}
		public DataTable GetTabla(string archivo)
		{
			DataTable retorno = null;
			OleDbConnection cn = Conectar();

			retorno = ExecuteToDataTable(cn, string.Format("SELECT * FROM {0}", archivo));
			cn.Close();
			return retorno;
		}
		public string GetValorTABCONTR(string codigo, string campo)
		{
			string retorno = "";
			string query = String.Format(" SELECT " + campo + " FROM TABCONTR WHERE CODTCO = {0}", codigo);
			OleDbConnection cn = Conectar();
			DataTable result = ExecuteToDataTable(cn, query);
			cn.Close();
			if (result.Rows.Count > 0)
				retorno = result.Rows[0][0].ToString();
			return retorno;

		}

		public string EliminarCaracteres(string texto) {
			string retorno = "";
			retorno = texto.Replace("'", " ");
			return retorno; 
		}
		public bool InsertaToken(string archivo, string token, string documento )
		{
			bool retorno = false;
			OleDbConnection cn = Conectar();
			retorno = Execute(cn, string.Format("UPDATE {0} SET TOK163 ='{1}' WHERE DOC163='{2}'", archivo, EliminarCaracteres( token) , documento )) > 0;
			return retorno;
		}
		public bool InsertaToken(string archivo, string token)
		{
			bool retorno = false;
			OleDbConnection cn = Conectar();
			retorno = Execute(cn, string.Format("UPDATE {0} SET TOK163 ='{1}'", archivo, EliminarCaracteres ( token))) > 0;
			return retorno;
		}

		public bool UpdateTabla(string archivo, string estado, string mensaje)
		{
			bool retorno = false;
			OleDbConnection cn = Conectar();
			retorno = Execute(cn, 
				string.Format("UPDATE {0} SET STA163 ={1}, OBS163='{2}'", 
				archivo, 
				estado, 
				EliminarCaracteres ( SubStr(mensaje, 200)))) > 0;
			return retorno;
		}
		public bool UpdateTabla(string archivo, string estado, string documento, string mensaje)
		{
			bool retorno = false;
			OleDbConnection cn = Conectar();
			retorno = Execute(cn,
				string.Format("UPDATE {0} SET STA163 ={1}, OBS163='{2}' WHERE DOC163='{3}' ",
				archivo,
				estado,
			 EliminarCaracteres (	SubStr(mensaje, 200)),
				documento)) > 0;
			return retorno;
		}
		public int Validar_Nro_Doc_Ticket(string TipoDocumento, string NumeroDocumento)
		{
            DataTable retorno = new DataTable();
            OleDbConnection cn = Conectar();
			retorno = ExecuteToDataTable(cn, string.Format("SELECT COUNT(*) CANT FROM CPE_TICKET_ESTADO WHERE TIPDOC='{0}' AND NRODOC='{1}' ", TipoDocumento, NumeroDocumento));
            cn.Close();
            return Convert.ToInt32(retorno.Rows[0][0].ToString());
        }
        public bool EliminarTicket(string TipoDocumento, string NumeroDocumento)
        {
            bool retorno = false;
            OleDbConnection cn = Conectar();
            retorno = Execute(cn, string.Format("DELETE FROM CPE_TICKET_ESTADO WHERE TIPDOC='{0}' AND NRODOC='{1}' ", TipoDocumento, NumeroDocumento)) > 0;
            return retorno;
        }

        public bool InsertaTicket(string codigo, string descripcion, string proceso, string estado, string TipoDocumento, string NumeroDocumento, string Ticket	)
        {
            bool retorno = false;
            OleDbConnection cn = Conectar();
            retorno = Execute(cn, string.Format("INSERT INTO CPE_TICKET_ESTADO(CODCIA,CODIGO,DESCRIP,PROCESO,ESTADO,TIPDOC,NRODOC,TICKET,FENVIO) VALUES(10,'{0}','{1}','{2}','{3}','{4}','{5}' ,'{6}', CURRENT TIMESTAMP) ", codigo,descripcion,proceso, estado,TipoDocumento, NumeroDocumento,Ticket )) > 0;
            return retorno;
        }
        public bool UpdateTicket_Documentos(string codigo, string descripcion, string proceso, string estado, string TipoDocumento, string NumeroDocumento)
        {
            bool retorno = false;
            OleDbConnection cn = Conectar();
            retorno = Execute(cn, string.Format("UPDATE CPE_TICKET_ESTADO SET PROCESO='{0}' ,ESTADO='{1}',DESCRIP='{2}' ,CODIGO='{3}', FDOC= CURRENT TIMESTAMP WHERE TIPDOC='{4}' AND NRODOC='{5}' ", proceso, estado, descripcion.Replace("'", ""), codigo, TipoDocumento, NumeroDocumento)) > 0;
            return retorno;
        }
        public bool UpdateTicket(string codigo,string descripcion, string ticket,string proceso,string estado, string TipoDocumento , string NumeroDocumento)
        {
            bool retorno = false;
            OleDbConnection cn = Conectar();
            retorno = Execute(cn, string.Format("UPDATE CPE_TICKET_ESTADO SET PROCESO='{0}' ,ESTADO='{1}',DESCRIP='{2}' ,CODIGO='{3}',TICKET='{4}', FDOC= CURRENT TIMESTAMP  WHERE TIPDOC='{5}' AND NRODOC='{6}' ", proceso, estado, descripcion.Replace("'", ""), codigo, ticket, TipoDocumento, NumeroDocumento)) > 0;
            return retorno;
        }
        public DataTable Obtener_Observaciones(string ticket)
        {
            DataTable retorno = new DataTable();
            OleDbConnection cn = Conectar();
            retorno = ExecuteToDataTable(cn, string.Format("SELECT * FROM CPE_TICKET_ESTADO WHERE TICKET = '{0}' ", ticket));
            cn.Close();
            return retorno;
        }
        public string Validar_Tiene_Ticket(string TipoDocumento, string NumeroDocumento)
        {
			string res = "";
            DataTable retorno = new DataTable();
            OleDbConnection cn = Conectar();
            retorno = ExecuteToDataTable(cn, string.Format("SELECT LTRIM(RTRIM(TICKET)) FROM CPE_TICKET_ESTADO WHERE TIPDOC='{0}' AND NRODOC='{1}' ", TipoDocumento, NumeroDocumento));
            if (retorno.Rows.Count == 0)
            {
				res = "";
            }
            else
            {
                res = retorno.Rows[0][0].ToString();
            }

            cn.Close();
			return res;
        }
        public string Validar_Tiene_Ticket_con_Error(string TipoDocumento, string NumeroDocumento)
        {
            string res = "";
            DataTable retorno = new DataTable();
            OleDbConnection cn = Conectar();
            retorno = ExecuteToDataTable(cn, string.Format("SELECT LTRIM(RTRIM(TICKET)) FROM CPE_TICKET_ESTADO WHERE TIPDOC='{0}' AND NRODOC='{1}' AND CODIGO <> '-9998' AND CODIGO<>'0' ", TipoDocumento, NumeroDocumento));
            if (retorno.Rows.Count == 0)
            {
                res = "";
            }
            else
            {
                res = retorno.Rows[0][0].ToString();
            }

            cn.Close();
            return res;
        }

        public DataTable Obtener_Observaciones_Estados()
        {
            DataTable retorno = new DataTable();
            OleDbConnection cn = Conectar();
            retorno = ExecuteToDataTable(cn, string.Format("SELECT * FROM CPE_TICKET_ESTADO"));
            cn.Close();
            return retorno;
        }

        public bool UpdateTablaFVTGUIA1(int CODCIA, int ANOGUI, string CODSER, int NDCGUI)
        {
            bool retorno = false;
            OleDbConnection cn = Conectar();
            retorno = Execute(cn, string.Format("UPDATE FVTGUIA1 SET FSUGUI=CURRENT TIMESTAMP  WHERE CODCIA={0} AND ANOGUI={1} AND CODSER='{2}' AND NDCGUI={3} ", CODCIA, ANOGUI, CODSER, NDCGUI)) > 0;
            return retorno;
        }


    }
}
