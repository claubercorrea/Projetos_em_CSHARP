using ListaFilme.DATA;
using Microsoft.AspNetCore.Mvc;
using ListaFilme.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System;
using System.ComponentModel.DataAnnotations;
using ListaFilme.Migrations;
namespace ListaFilme.Controllers
{
    public class FilmesController : Controller
    {
        private readonly FilmeContext _context;
        public FilmesController(FilmeContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var lista = await _context.Filmes.ToListAsync();

            return View(lista);
        }
        [HttpGet]
        public async Task<IActionResult> Pesquisar(string Title, string Genero)
        {

            var filmes = from m in _context.Filmes select m;
            if (!string.IsNullOrEmpty(Title))
            {
                filmes = filmes.Where(m => m.Title.Contains(Title));
            }
            if (!string.IsNullOrEmpty(Genero))
            {
                filmes = filmes.Where(m => m.Genero.Contains(Genero));
                filmes = filmes.Where(m => m.Genero == Genero);
            }
            ViewData["Titulo"] = Title;
            var resultado = await filmes.ToListAsync();

            return View("Index",resultado); // voltara para index

        }

        [HttpGet]
   
        public IActionResult Adicionar()
        {
            return View();
        }

        [HttpPost]
  
        public async Task<IActionResult> Adicionar(Filme filmes)
        {
            if (ModelState.IsValid)
            {
                _context.Filmes.Add(filmes);
                await _context.SaveChangesAsync();
                return RedirectToAction("index");

            }
            return View(filmes);
        }
        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var filmes = await _context.Filmes.FindAsync(id);
            if (filmes == null)
            {
                return NotFound();
            }
            return View(filmes);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, Filme filmes)
        {
            try
            {
                if (id != filmes.Id)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    _context.Filmes.Update(filmes);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("index");
                }
                return View(filmes);
            }
            catch (Exception ex)
            {
                return View("Error");
            }


        }
        [HttpGet]
      
        public async Task<IActionResult> Excluir(int id)
        {
            try
            {

                var filmes = await _context.Filmes.FindAsync(id);
                if (filmes == null)
                {
                    return NotFound();
                }
                return View(filmes);
            }
            catch (Exception ex)
            {
                return View("Error");
            }

        }


        [HttpPost,ActionName("Excluir")]
   
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExcluirConfirmado(int id)
        {
            try
            {
                var filmes = await _context.Filmes.FindAsync(id);
                if (filmes == null)
                {
                    return NotFound();

                }
                _context.Filmes.Remove(filmes);
                await _context.SaveChangesAsync();
                // ⚠️ Se não tiver mais nenhum filme, reinicia a contagem
                if (!await _context.Filmes.AnyAsync())
                {
                    await _context.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('Filmes', RESEED, 0)");
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }
    }
}


