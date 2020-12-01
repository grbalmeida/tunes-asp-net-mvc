using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Tunes.AspNet.Mvc.HttpClients;
using Tunes.AspNet.Mvc.Models;

namespace Tunes.AspNet.Mvc.Controllers
{
    [Route("funcionarios")]
    public class FuncionarioController : Controller
    {
        private readonly FuncionarioApiClient _funcionarioApiClient;

        public FuncionarioController(FuncionarioApiClient funcionarioApiClient)
        {
            _funcionarioApiClient = funcionarioApiClient;
        }

        [HttpGet("listar-todos")]
        public async Task<IActionResult> Index()
        {
            var funcionarios = await _funcionarioApiClient.GetFuncionariosAsync();
            return View(funcionarios);
        }

        [HttpGet("detalhes/{id:int}")]
        public async Task<IActionResult> Detalhes(int id)
        {
            var funcionario = await _funcionarioApiClient.GetFuncionarioAsync(id);
            return View(funcionario);
        }

        [Route("novo")]
        public async Task<IActionResult> Novo()
        {
            var funcionarioViewModel = await PopularGerentes(new FuncionarioViewModel());

            return View(funcionarioViewModel);
        }

        [HttpPost("novo")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Novo(FuncionarioViewModel funcionarioViewModel)
        {
            if (!ModelState.IsValid) return View(funcionarioViewModel);

            funcionarioViewModel.Gerente = await _funcionarioApiClient.GetFuncionarioAsync(funcionarioViewModel.GerenteId);

            var resposta = await _funcionarioApiClient.PostFuncionarioAsync(funcionarioViewModel);

            if (resposta.Errors != null)
            {
                ModelState.AddModelError("Erro", resposta.Errors.FirstOrDefault());
                return View(funcionarioViewModel);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("editar/{id:int}")]
        public async Task<IActionResult> Editar(int id)
        {
            var funcionarioViewModel = await _funcionarioApiClient.GetFuncionarioAsync(id);

            funcionarioViewModel = await PopularGerentes(funcionarioViewModel);

            funcionarioViewModel.GerenteId = funcionarioViewModel.Gerente?.FuncionarioId ?? 0;

            if (funcionarioViewModel == null) return NotFound();

            return View(funcionarioViewModel);
        }

        [HttpPost("editar/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, FuncionarioViewModel funcionarioViewModel)
        {
            if (id != funcionarioViewModel.FuncionarioId) return NotFound();

            if (!ModelState.IsValid) return View(funcionarioViewModel);

            funcionarioViewModel.Gerente = await _funcionarioApiClient.GetFuncionarioAsync(funcionarioViewModel.GerenteId);

            var resposta = await _funcionarioApiClient.PutFuncionarioAsync(id, funcionarioViewModel);

            if (resposta.Errors != null)
            {
                ModelState.AddModelError("Erro", resposta.Errors.FirstOrDefault());
                return View(funcionarioViewModel);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("excluir/{id:int}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var funcionario = await _funcionarioApiClient.GetFuncionarioAsync(id);
            return View(funcionario);
        }

        [HttpPost("excluir/{id:int}"), ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmacaoExclusao(int id)
        {
            var funcionario = await _funcionarioApiClient.GetFuncionarioAsync(id);

            if (funcionario == null) return NotFound();

            var resposta = await _funcionarioApiClient.DeleteFuncionarioAsync(id);

            if (resposta.Errors != null)
            {
                ModelState.AddModelError("Erro", resposta.Errors.FirstOrDefault());
                return View(funcionario);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<FuncionarioViewModel> PopularGerentes(FuncionarioViewModel funcionarioViewModel)
        {
            funcionarioViewModel.Gerentes = await _funcionarioApiClient.GetFuncionariosAsync();
            return funcionarioViewModel;
        }
    }
}
