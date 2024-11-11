using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecnologiaTextil.FacturacionClient.DataAccess
{
    public class DBUtility
    {
        public DBUtility()
        {
            this.AppConexion = "Provider=IBMDA400.DataSource.1;Data source=192.168.166.22;User Id=SA;Password=M1STERI0; Default Collection=ASDATA;";
        }
		protected string SubStr(string mensaje , int total) {
			//string temp = mensaje 
			if (mensaje.Length > total)
				return mensaje.Substring(0, total);
			else
				return mensaje;
		}
        private string AppConexion { get; set; }
        protected string getConnection()
        {
            return AppConexion;
            //string cadena = "Conexion";
            //if (AppConexion != null)
            //{
            //    cadena = AppConexion;
            //}
            //return ConfigurationManager.ConnectionStrings[cadena].ConnectionString;
        }
        protected OleDbConnection Conectar()
        {

            OleDbConnection cn = new OleDbConnection(getConnection());
            if (cn.State == ConnectionState.Open) cn.Close();
            cn.Open();
            return cn;


        }
        protected DataTable ExecuteToDataTable(OleDbConnection cn, string query, OleDbParameter[] param)
        {
            DataTable result = new DataTable("RetornoData");

            IDataReader lector = ExecuteQuery(cn, query, param);
            result.Load(lector);
            return result;
        }
        protected DataTable ExecuteToDataTable(OleDbConnection cn, string query)
        {
            DataTable result = new DataTable();
            IDataReader lector = ExecuteQuery(cn, query, "");
            if (lector != null)
                result.Load(lector);

            return result;
        }
        protected int Execute(OleDbConnection cn, string query, CommandType typecommand = CommandType.Text)
        {
            return Execute(cn, query, null, typecommand);
        }
        protected int Execute(OleDbConnection cn, string query, OleDbParameter[] parameters, CommandType typecommand = CommandType.Text)
        {
            if (cn.State == ConnectionState.Open) cn.Close();
            cn.Open();
            using (OleDbCommand cmd = new OleDbCommand(query, cn))
            {
                cmd.CommandType = typecommand;
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);

                return cmd.ExecuteNonQuery();
            }

        }
        protected IDataReader ExecuteQuery(OleDbConnection cn, string query, string criterio = "")
        {
            Utils.Debug("Query Ejecutado -> " + query);
            IDataReader retorno = null;
            try
            {
                if (cn.State == ConnectionState.Open) cn.Close();
                cn.Open();
                using (OleDbCommand cmd = new OleDbCommand(query, cn))
                {
                    cmd.CommandType = CommandType.Text;
                    if (criterio != "")
                        cmd.Parameters.AddWithValue("criterio", criterio);
                    retorno = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    return retorno;
                }
            }
            catch (Exception ex)
            {
                Utils.Fatal("EXECUTEQUERY: " + ex.Message);
                return null;
            }

        }
        protected IDataReader ExecuteQuery(OleDbConnection cn, string query, OleDbParameter[] parameters, CommandType typecommand = CommandType.Text)
        {
            if (cn.State == ConnectionState.Open) cn.Close();
            cn.Open();
            using (OleDbCommand cmd = new OleDbCommand(query, cn))
            {
                cmd.CommandType = typecommand;
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);

                return cmd.ExecuteReader(CommandBehavior.SingleResult | CommandBehavior.CloseConnection);

            }
        }
    }
}
