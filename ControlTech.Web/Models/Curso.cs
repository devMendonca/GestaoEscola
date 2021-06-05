using RestSharp.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControlTech.Web.Models
{
    public class Curso
    {   
     
        public int CursoID { get; set; }
        
        [Required]
        public string Titulo { get; set; }
        
        [Required]
        [Display(Name = "R$ Valor")]
        public decimal Valor { get; set; }

        // Curso Recebe uma lista de inscrições
        public ICollection<Inscricao> Inscricoes { get; set; }
        

    }
}
