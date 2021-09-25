using Gerenciador.Context;
using Gerenciador.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gerenciador.Controllers
{
    public class AlunoController : Controller
    {
        private readonly Contexto _context;
        public AlunoController(Contexto context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {

            return View(await _context.aluno.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Aluno aluno)
        {
            try
            {
                if (ModelState.IsValid)
                {
                   var Aaluno = new Aluno
                   {
                       Nome = User.Identity.Name,
                       Disciplina = aluno.Disciplina,
                       Descrição = aluno.Descrição,
                       Tema = aluno.Tema,
                       Integrantes = aluno.Integrantes
                   };
                    _context.Add(Aaluno);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possível inserir os dados");
            }
            return View(aluno);
        }
    }
}
