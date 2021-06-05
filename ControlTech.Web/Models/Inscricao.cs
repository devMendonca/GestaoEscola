using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlTech.Web.Models
{
    public class Inscricao
    {
        public int InscricaoID { get; set; }
        public int CursoID { get; set; }
        public int EstudanteID { get; set; }
        public Grade? Grade { get; set; }

        // propriedade de navegação
        public Curso Curso { get; set; }
        public Estudante Estudante { get; set; }
    }

    public enum Grade
    {
        TECNOLOGIA, RH, ADMINISTRACAO, DIREITO, PEDAGOGIA, ENFERMAGEM
    }
}
