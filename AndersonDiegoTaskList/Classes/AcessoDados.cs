using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace AndersonDiegoTaskList
{
    public class AcessoDados
    {
        private static string varConnectionString = ConfigurationManager.ConnectionStrings["TaskListingDataCon"].ConnectionString;

        SqlConnection vCon = new SqlConnection(varConnectionString);
        SqlCommand vCom = null;

        public void Conectar()
        {
            if (vCon.State == System.Data.ConnectionState.Closed) vCon.Open();
        }
        public bool EstaConectado()
        {            
            if (vCon.State == System.Data.ConnectionState.Open)
                return true;
            else
                return false;           
        }

        public SqlConnection Conexao()
        {
            return vCon;
        }

        public int PersistirDados(SqlConnection pCon, string pSql)
        {
            vCom = new SqlCommand(pSql, pCon);
            return vCom.ExecuteNonQuery();
        }

        public SqlDataReader ObterDados(SqlConnection pCon, string pSql)
        {
            vCom = new SqlCommand(pSql, pCon);
            return vCom.ExecuteReader();
        }

    }
}