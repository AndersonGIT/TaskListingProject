using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AndersonDiegoTaskList.Models;

namespace AndersonDiegoTaskList.Controllers
{
    public class TaskController : Controller
    {
        // GET: Task
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ListarTarefas()
        {
            List<TarefaModel> vLista = new TaskDados().ListarTarefas();
            return Json(vLista);
        }

        [HttpPost]
        public JsonResult InserirTarefa(FormCollection pFormulario)
        {
            var task = new TarefaModel();

            var vObjetoResultado = new
            {
                statusCodigo = "OK-NOK",
                mensagem = "Objeto criado mas não persistido.",
                texto = "Titulo = " + task.Titulo + " || Descricao = " + task.Descricao + " || DataCriacao = " + task.DataCriacao
            };

            try
            {
                task.Titulo = pFormulario["titulo"];
                task.Descricao = pFormulario["descricao"];
                

                if (new TaskDados().InserirTarefa(task) > 0) {
                    vObjetoResultado = new
                    {
                        statusCodigo = "OK",
                        mensagem = "Dados armazenados com sucesso.",
                        texto = "Titulo = " + task.Titulo + " || Descricao = " + task.Descricao + " || DataCriacao = " + task.DataCriacao
                    };
                   
                }
                else
                {
                    vObjetoResultado = new
                    {
                        statusCodigo = "NOK",
                        mensagem = "Os dados não foram persistidos.",
                        texto = "Titulo = " + task.Titulo + " || Descricao = " + task.Descricao + " || DataCriacao = " + task.DataCriacao
                    };
                    
                }

            }
            catch (Exception e)
            {
                vObjetoResultado = new
                {
                    statusCodigo = "NOK-EX",
                    mensagem = e.Message,
                    texto = "Titulo = " + task.Titulo + " || Descricao = " + task.Descricao + " || DataCriacao = " + task.DataCriacao
                };
            }
            return Json(vObjetoResultado);
        }

        [HttpPost]
        public JsonResult TestarConexao()
        {
            var vObjeto = new AcessoDados();
            vObjeto.Conectar();

            if (vObjeto.EstaConectado())
            {

                var vObjetoResultado = new
                {
                    statusCodigo = "OK",
                    mensagem = "Conectado com sucesso.",
                    
                };
                return Json(vObjetoResultado);
            }
            else
            {
                var vObjetoResultado = new
                {
                    statusCodigo = "NOK",
                    mensagem = "Problemas na conexão.",

                };
                return Json(vObjetoResultado);
            }
            
        }

        
        public ActionResult Excluir(int pId)
        {
            
            if(new TaskDados().ExcluirTarefa(pId))
            {
                ViewBag.Resposta = "OK";
                //var vObjetoResultado = new

                //{
                //    statusCodigo = "OK",
                //    mensagem = "Tarefa excluida com sucesso."
                //};

                //return Json(vObjetoResultado, JsonRequestBehavior.AllowGet);
            }
            else
            {
                ViewBag.Resposta = "NOK";
                //var vObjetoResultado = new
                //{
                //    statusCodigo = "NOK",
                //    mensagem = "Problemas para excluir a tarefa."
                //};

                //return Json(vObjetoResultado, JsonRequestBehavior.AllowGet);
            }

            

            return View();
        }
        
        public ActionResult VisualizarTarefa(int pId)
        {
            return View(new TaskDados().VisualziarTarefa(pId));
        }

        [HttpPost]
        public JsonResult MarcarFinalizada(int pId)
        {
            if (new TaskDados().ConcluirTarefa(pId))
            {
                var vObjetoResultado = new

                {
                    statusCodigo = "OK",
                    mensagem = "Tarefa concluida com sucesso."
                };

                return Json(vObjetoResultado, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var vObjetoResultado = new
                {
                    statusCodigo = "NOK",
                    mensagem = "Problemas para concluir a tarefa."
                };

                return Json(vObjetoResultado, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult EditarTarefa(int pId)
        {

            return View(new TaskDados().VisualziarTarefa(pId));
        }

        [HttpPost]
        public JsonResult EditarTarefa(FormCollection pForm)
        {
            TarefaModel Tarefa = new TarefaModel();
            

            Tarefa.Id = Convert.ToInt32(pForm["Id"]);
            Tarefa.Titulo = pForm["Titulo"].ToString();
            Tarefa.Descricao = pForm["Descricao"].ToString();
            if (new TaskDados().AtualizarTarefa(Tarefa))
            {
                var vObjetoResultado = new

                {
                    statusCodigo = "OK",
                    mensagem = "Tarefa editada com sucesso."
                };

                return Json(vObjetoResultado, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var vObjetoResultado = new
                {
                    statusCodigo = "NOK",
                    mensagem = "Problemas para editar" +
                    " a tarefa."
                };

                return Json(vObjetoResultado, JsonRequestBehavior.AllowGet);
            }
        }
    }
}