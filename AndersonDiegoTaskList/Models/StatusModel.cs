using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AndersonDiegoTaskList.Models
{
    public class StatusModel
    {
        public int Id { get; set; }

        public char Codigo { get; set; }

        public string Descricao { get; set; }

        public DateTime DataCriacao { get; set; }

        public DateTime DataAtualizacao { get; set; }

        public DateTime DataExclusao { get; set; }
    }
}