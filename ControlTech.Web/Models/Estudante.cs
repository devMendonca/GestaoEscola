using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControlTech.Web.Models
{
    public class Estudante
    {
        // por padro a coluna é eleita pelo EF Core como primary key
        public int ID { get; set; }

        [Required]
        public string Nome { get; set; }
        public string Sobrenome { get; set; }

        
        public DateTime InscricaoData { get; set; }

        // propriedade de navegação - cria o relacionamento
        public ICollection<Inscricao> Incricoes { get; set; }

    }
}
