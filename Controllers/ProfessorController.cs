using Gerenciador.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gerenciador.Controllers
{
    public class ProfessorController : Controller
    {
        private readonly Contexto _context;
        public ProfessorController(Contexto context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {

            return View(await _context.professor.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome")] Models.Professor professor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(professor);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possível inserir os dados");
            }
            return View(professor);
        }

    }
}
