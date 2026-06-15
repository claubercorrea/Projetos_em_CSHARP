using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Produtos.Data;
using Produtos.Models;
using System.Security.Claims;

namespace Produtos.Controllers
{
    public class AuthController : Controller
    {
        private readonly UsuarioContext _context;

        public AuthController(UsuarioContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Cadastro()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Cadastro(Usuario u)
        {
            // Verifica se já existe um usuário com esse e-mail antes de salvar
            if (_context.Usuarios.Any(x => x.Email == u.Email))
            {
                ViewBag.Error = "Este e-mail já está cadastrado.";
                return View();
            }

            u.Senha = BCrypt.Net.BCrypt.HashPassword(u.Senha);
            _context.Usuarios.Add(u);
            await _context.SaveChangesAsync();
            return RedirectToAction("Login", "Auth");
        }
        [HttpGet]
        public IActionResult EsqueciSenha()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EsqueciSenha(string Email)
        {
            // Adicionado o Async e o await aqui
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == Email);

            if (user != null)
            {
                return RedirectToAction("NovaSenha", new { email = Email });
            }

            ViewBag.Error = "E-mail não encontrado.";
            return View("EsqueciSenha");
        }
        public IActionResult NovaSenha(string email)
        {
            ViewBag.Email = email;
            return View();
        }
            [HttpPost]
             public async Task<IActionResult> NovaSenha(string Email, string NovaSenha)
               {
                // Adicionado o Async e o await aqui
                var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == Email);

                if (user != null)
                {
                    user.Senha = BCrypt.Net.BCrypt.HashPassword(NovaSenha);

                    // No EF Core, se você carregar o objeto do banco, 
                    // o .Update() é opcional, mas manter está ok.
                    _context.Usuarios.Update(user);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Login", "Auth");
                }

                ViewBag.Error = "Falha ao atualizar a senha.";
                return View("NovaSenha");
            }
        


            [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string Email, string Senha)
        {
            var user = _context.Usuarios.FirstOrDefault(u => u.Email == Email);

            if (user != null && BCrypt.Net.BCrypt.Verify(Senha, user.Senha))
            {
                // Usamos o Email no Name para ser a chave mestra de filtros
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Email),
            new Claim("NomeUsuario", user.Nome)
        };

                var identity = new ClaimsIdentity(claims, "CookieAuth");
                await HttpContext.SignInAsync("CookieAuth", new ClaimsPrincipal(identity));

                return RedirectToAction("Index", "Painel");
            }

            ViewBag.Error = "E-mail ou senha inválidos.";
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("CookieAuth");
            return RedirectToAction("Login");
        }

    }
}
        