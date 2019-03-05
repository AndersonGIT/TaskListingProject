using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AndersonDiegoTaskList.Models;
using System.Data.SqlClient;

namespace AndersonDiegoTaskList
{
    public class TaskDados
    {
        AcessoDados objDados = new AcessoDados();
        public int InserirTarefa(TarefaModel pTarefa)
        {
            if(!objDados.EstaConectado()) objDados.Conectar();

            string sql = string.Format(@"INSERT INTO [dbo].[TL_TASKS]
           ([TITULO]
           ,[DESCRICAO]
           ,[STATUS]
           ,[DATA_CRIACAO]
           ,[DATA_ATUALZIACAO]
           ,[DATA_EXCLUSAO])
        VALUES
           ('{0}'
           ,'{1}'
           ,'{2}'
           ,'{3}'
           ,NULL
           ,NULL)", pTarefa.Titulo, pTarefa.Descricao, pTarefa.Status = 'A', pTarefa.DataCriacao = DateTime.Now);

            return objDados.PersistirDados(objDados.Conexao(), sql);

        }

        public List<TarefaModel> ListarTarefas()
        {
            if (!objDados.EstaConectado()) objDados.Conectar();
            List<TarefaModel> Tarefas = new List<TarefaModel>();

            string sql = string.Format(@"SELECT * FROM TL_TASKS WHERE DATA_EXCLUSAO IS NULL");
            SqlDataReader vResultado = objDados.ObterDados(objDados.Conexao(), sql);

            while (vResultado.Read())
            {
                Tarefas.Add(new TarefaModel
                {
                    Id = Convert.ToInt32(vResultado["ID"]),
                    Titulo = vResultado["TITULO"].ToString(),
                    Descricao = vResultado["DESCRICAO"].ToString(),
                    Status = Convert.ToChar(vResultado["STATUS"]),
                    DataCriacao = vResultado.IsDBNull(vResultado.GetOrdinal("DATA_CRIACAO")) ? null : (DateTime?)vResultado["DATA_CRIACAO"],
                    DataAtualizacao = vResultado.IsDBNull(vResultado.GetOrdinal("DATA_ATUALZIACAO")) ? null : (DateTime?)vResultado["DATA_ATUALZIACAO"],
                    DataExclusao = vResultado.IsDBNull(vResultado.GetOrdinal("DATA_EXCLUSAO")) ? null : (DateTime?)vResultado["DATA_EXCLUSAO"]

                });
            }

            return Tarefas;
            
        }

        public bool ExcluirTarefa(int pId)
        {
            if (!objDados.EstaConectado()) objDados.Conectar();
            string sql = string.Format(@"UPDATE TL_TASKS SET DATA_EXCLUSAO = GETDATE() WHERE ID = {0}", pId);
            if (objDados.PersistirDados(objDados.Conexao(), sql) > 0)
                return true;
            else
                return false;

        }

        public bool ConcluirTarefa(int pId)
        {
            if (!objDados.EstaConectado()) objDados.Conectar();
            string sql = string.Format(@"UPDATE TL_TASKS SET STATUS = '{0}', DATA_ATUALZIACAO = '{1}' WHERE ID = {2}", 'F', DateTime.Now, pId);
            if (objDados.PersistirDados(objDados.Conexao(), sql) > 0)
                return true;
            else
                return false;

        }

        public TarefaModel VisualziarTarefa(int pId)
        {
            TarefaModel Tarefa = new TarefaModel();
            if (!objDados.EstaConectado()) objDados.Conectar();
            string sql = string.Format(@"SELECT * FROM TL_TASKS where ID = {0}", pId);
            SqlDataReader vResultado = objDados.ObterDados(objDados.Conexao(), sql);
            if (vResultado.Read())
            {
                Tarefa.Id = Convert.ToInt32(vResultado["ID"]);
                Tarefa.Titulo = vResultado["TITULO"].ToString();
                Tarefa.Descricao = vResultado["DESCRICAO"].ToString();
                Tarefa.Status = Convert.ToChar(vResultado["STATUS"]);
                Tarefa.DataCriacao = vResultado.IsDBNull(vResultado.GetOrdinal("DATA_CRIACAO")) ? null : (DateTime?)vResultado["DATA_CRIACAO"];
                Tarefa.DataAtualizacao = vResultado.IsDBNull(vResultado.GetOrdinal("DATA_ATUALZIACAO")) ? null : (DateTime?)vResultado["DATA_ATUALZIACAO"];
                Tarefa.DataExclusao = vResultado.IsDBNull(vResultado.GetOrdinal("DATA_EXCLUSAO")) ? null : (DateTime?)vResultado["DATA_EXCLUSAO"];

            }
            return Tarefa;
        }

        public bool AtualizarTarefa(TarefaModel pTarefa)
        {
            string sql = string.Format(@"UPDATE TL_TASKS set TITULO = '{0}', DESCRICAO = '{1}', DATA_ATUALZIACAO = GETDATE() where ID = {2}", pTarefa.Titulo, pTarefa.Descricao, pTarefa.Id);
            if (!objDados.EstaConectado()) objDados.Conectar();
            if (objDados.PersistirDados(objDados.Conexao(), sql) > 0)
                return true;
            else
                return false;
        }
    }
}