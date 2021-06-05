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
    public class InscricoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InscricoesController(ApplicationDbContext context)
        {
            _context = context;
        }

       
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Incricoes.Include(i => i.Curso).Include(i => i.Estudante);
            return View(await applicationDbContext.ToListAsync());
        }

       
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inscricao = await _context.Incricoes
                .Include(i => i.Curso)
                .Include(i => i.Estudante)
                .FirstOrDefaultAsync(m => m.InscricaoID == id);
            if (inscricao == null)
            {
                return NotFound();
            }

            return View(inscricao);
        }

        public IActionResult Create()
        {
            ViewData["CursoID"] = new SelectList(_context.Cursos, "CursoID", "CursoID");
            ViewData["EstudanteID"] = new SelectList(_context.Estudantes, "ID", "ID");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InscricaoID,CursoID,EstudanteID,Grade")] Inscricao inscricao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inscricao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CursoID"] = new SelectList(_context.Cursos, "CursoID", "CursoID", inscricao.CursoID);
            ViewData["EstudanteID"] = new SelectList(_context.Estudantes, "ID", "ID", inscricao.EstudanteID);
            return View(inscricao);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inscricao = await _context.Incricoes.FindAsync(id);
            if (inscricao == null)
            {
                return NotFound();
            }
            ViewData["CursoID"] = new SelectList(_context.Cursos, "CursoID", "CursoID", inscricao.CursoID);
            ViewData["EstudanteID"] = new SelectList(_context.Estudantes, "ID", "ID", inscricao.EstudanteID);
            return View(inscricao);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InscricaoID,CursoID,EstudanteID,Grade")] Inscricao inscricao)
        {
            if (id != inscricao.InscricaoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inscricao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InscricaoExists(inscricao.InscricaoID))
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
            ViewData["CursoID"] = new SelectList(_context.Cursos, "CursoID", "CursoID", inscricao.CursoID);
            ViewData["EstudanteID"] = new SelectList(_context.Estudantes, "ID", "ID", inscricao.EstudanteID);
            return View(inscricao);
        }

     
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inscricao = await _context.Incricoes
                .Include(i => i.Curso)
                .Include(i => i.Estudante)
                .FirstOrDefaultAsync(m => m.InscricaoID == id);
            if (inscricao == null)
            {
                return NotFound();
            }

            return View(inscricao);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inscricao = await _context.Incricoes.FindAsync(id);
            _context.Incricoes.Remove(inscricao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InscricaoExists(int id)
        {
            return _context.Incricoes.Any(e => e.InscricaoID == id);
        }
    }
}
