using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlTech.Web.Models;

namespace ControlTech.Web.Data
{
    /* classes estaticas são classes que não pdoem ser intanciadas, automaticamente estão na memoria
     * disponiveis para seu uso, você apenas chama a classe e seus metodos */
    public static class DbInicializacao
    {
        public static void Initialize(ApplicationDbContext context)
        {
            /* este comando visao assegurar a criação do banco de dados automaticamente */
            context.Database.EnsureCreated();

            if (context.Estudantes.Any())
            {
                return; // Database já foi inicializado com a semente inicial
            }

            var estudantes = new Estudante[]
            {
                new Estudante{Nome="Chaves", Sobrenome="Da Villa", InscricaoData=DateTime.Parse("2019-09-10")},
                new Estudante{Nome="Kiko", Sobrenome="Da Mamae", InscricaoData=DateTime.Parse("2020-08-01")}
            };

            // vamos percorrer o nosso array e adiciona-lo ao nosso CONTEXTO
            foreach (var item in estudantes)
            {
                context.Estudantes.Add(item);
            }

            // Chama o save o change para executar o INSERT
            context.SaveChanges();

            var cursos = new Curso[]
            {
                new Curso{ Titulo="Desenvolvimento de aplicações comerciais", Valor=3.000m},
                new Curso{ Titulo="Aplicaçõe Mobile com .NET CORE e Blazor", Valor=5.000m}
            };

            // vamos percorrer o nosso array e adiciona-lo ao nosso CONTEXTO
            foreach (var item in cursos)
            {
                context.Cursos.Add(item);
            }

            // Chama o save o change para executar o INSERT
            context.SaveChanges();

            var inscricoes = new Inscricao[]
            {
                new Inscricao{EstudanteID=1, CursoID=1, Grade=Grade.TECNOLOGIA },
                new Inscricao{EstudanteID=2, CursoID=2, Grade=Grade.TECNOLOGIA }
            };

            // vamos percorrer o nosso array e adiciona-lo ao nosso CONTEXTO
            foreach (var item in inscricoes)
            {
                context.Incricoes.Add(item);
            }

            // Chama o save o change para executar o INSERT
            context.SaveChanges();

            }
        }
    }

