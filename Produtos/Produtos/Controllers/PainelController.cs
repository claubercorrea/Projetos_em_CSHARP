using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Produtos.Data;
using Produtos.Models;

namespace Produtos.Controllers
{
    [Authorize]
    public class PainelController : Controller
    {
        private UsuarioContext _context;
        public PainelController(UsuarioContext context)
        {
            _context = context;
        }
        [HttpGet]
        [HttpGet]
        public async Task<IActionResult> Index(string buscar)
        {
            var emailLogado = User.Identity.Name;

            // AQUI ESTÁ A CORREÇÃO:
            // Se a busca retornar nulo ou não encontrar nada, o .ToListAsync() 
            // garante que o Model chegue como uma lista vazia, e não como 'null'.
            var lista = await _context.Produtos
                .Where(p => p.Email == emailLogado && (string.IsNullOrEmpty(buscar) || p.NomeProduto.Contains(buscar)))
                .ToListAsync() ?? new List<Produto>(); // O ?? new List... é a rede de segurança

            return View(lista);
        }
        [HttpGet]

        public IActionResult Pesquisar(string buscar)
        {
            // Aqui você acessa o e-mail do usuário logado facilmente
            var emailLogado = User.Identity.Name;
            // Listagem com Busca
            var lista = _context.Produtos
                .Where(p => p.Email == emailLogado && (string.IsNullOrEmpty(buscar) || p.NomeProduto.Contains(buscar)));

            return View();
        }
        // Exibe o formulário vazio
        [HttpGet]
        public IActionResult Salvar()
        {
            return View(new Produto());
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken] // Proteção importante
        public async Task<IActionResult> Salvar(Produto p)
        {
            // 1. Remove o campo Email da validação, pois ele virá de outra fonte
            ModelState.Remove("Email");

            if (ModelState.IsValid)
            {
                // 2. Define o e-mail manualmente (obrigatório já que não está no form)
                p.Email = User.Identity.Name;

                // 3. Adiciona ao banco
                _context.Produtos.Add(p);

                // 4. Salva no banco (await é crucial)
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            // Se chegar aqui, o ModelState é false (algum campo está faltando)
            return View(p);
        }
        // 1. Exibe a tela de edição com os dados atuais
        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var produto = await _context.Produtos
                .FirstOrDefaultAsync(p => p.Id == id && p.Email == User.Identity.Name);

            if (produto == null) return NotFound();

            return View(produto);
        }

        // 2. Processa o envio do formulário
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, Produto p)
        {
            // Opcional: validar se o ID da URL bate com o ID do objeto
            if (id != p.Id) return NotFound();

            ModelState.Remove("Email"); // Novamente, removemos o Email da validação

            if (ModelState.IsValid)
            {
                var produtoExistente = await _context.Produtos
                    .FirstOrDefaultAsync(p_db => p_db.Id == id && p_db.Email == User.Identity.Name);

                if (produtoExistente == null) return NotFound();

                // Atualização dos dados
                produtoExistente.NomeProduto = p.NomeProduto;
                produtoExistente.validade = p.validade;
                produtoExistente.Empresa = p.Empresa;

                // O Entity Framework detecta as mudanças automaticamente.
                // O .Update() é desnecessário se o objeto estiver sendo "rastreado" pelo contexto.
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(p);
        }

        [HttpGet]
        public async Task<IActionResult> Excluir(int id)
        {
            // Busca o produto pelo ID E pelo e-mail do usuário logado
            var produto = await _context.Produtos
                .FirstOrDefaultAsync(p => p.Id == id && p.Email == User.Identity.Name);

            if (produto == null) return NotFound();

            return View(produto);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExcluirConfirmado(int id)
        {
            var produto = await _context.Produtos
                .FirstOrDefaultAsync(p => p.Id == id && p.Email == User.Identity.Name);

            if (produto == null) return NotFound();

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


    }
}
