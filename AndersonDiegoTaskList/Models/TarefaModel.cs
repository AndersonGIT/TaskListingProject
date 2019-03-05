using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AndersonDiegoTaskList.Models
{
    public class TarefaModel
    {
        public int Id { get; set; }

        public string Titulo { get; set; }

        public string Descricao { get; set; }

        public char Status { get; set; }

        public DateTime? DataCriacao { get; set; }

        public DateTime? DataAtualizacao { get; set; }

        public DateTime? DataExclusao { get; set; }

        

    }
}