using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ControlTech.Web.Data;
using ControlTech.Web.Models;

namespace ControlTech.Web.Controllers
{
    public class EstudantesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EstudantesController(ApplicationDbContext context)
        {
            _context = context;
        }

       
        public async Task<IActionResult> Index(string sortOrder)
        {
            /* se quiser executar sem o filtro utilizar somente esta parte de cima */
            // viewbag TEMPDATA[Key] = "value" 
            //return View(await _context.Estudantes.ToListAsync());

            /*********************************************/
            /* filtro da pagina abaixo */
            /*********************************************/

            // ViewData - vamo utilizar para criar nossos filtros na pagina de estudantes
            ViewData["NomeOrdem"] = String.IsNullOrEmpty(sortOrder) ? "Nome" : "";
            ViewData["DataOrdem"] = sortOrder == "Data" ? "Data" : "Data_Decrescente";

            // Realiza o Select na tabela ESTUDANTE
            var estudantes = from e in _context.Estudantes
                             select e;

            // vamos criar a estrutura de seleção dos filtros
            switch (sortOrder)
            {
                case "Nome":
                    estudantes = estudantes.OrderByDescending(e => e.Nome);
                    break;
                case "Data":
                    estudantes = estudantes.OrderBy(x => x.InscricaoData);
                    break;
                case "Data_Decrescente":
                    estudantes = estudantes.OrderByDescending(x => x.InscricaoData);
                    break;
                default:
                    estudantes = estudantes.OrderBy(x => x.Nome);
                    break;
            }

            //  AsNoTracking() -> utilizado para diminuir as execuções de querys com as tabelas relacionadas
            return View(await estudantes.AsNoTracking().ToListAsync());

        }

       
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudante = await _context.Estudantes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (estudante == null)
            {
                return NotFound();
            }

            return View(estudante);
        }

      
        public IActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nome,Sobrenome,InscricaoData")] Estudante estudante)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estudante);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(estudante);
        }

       
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudante = await _context.Estudantes.FindAsync(id);
            if (estudante == null)
            {
                return NotFound();
            }
            return View(estudante);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nome,Sobrenome,InscricaoData")] Estudante estudante)
        {
            if (id != estudante.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estudante);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstudanteExists(estudante.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(estudante);
        }

    
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudante = await _context.Estudantes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (estudante == null)
            {
                return NotFound();
            }

            return View(estudante);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estudante = await _context.Estudantes.FindAsync(id);
            _context.Estudantes.Remove(estudante);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstudanteExists(int id)
        {
            return _context.Estudantes.Any(e => e.ID == id);
        }
    }
}
