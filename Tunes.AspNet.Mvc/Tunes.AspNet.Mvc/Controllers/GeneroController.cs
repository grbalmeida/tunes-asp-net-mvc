using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Tunes.AspNet.Mvc.HttpClients;
using Tunes.AspNet.Mvc.Models;

namespace Tunes.AspNet.Mvc.Controllers
{
    [Route("generos")]
    public class GeneroController : Controller
    {
        private readonly GeneroApiClient _generoApiClient;

        public GeneroController(GeneroApiClient generoApiClient)
        {
            _generoApiClient = generoApiClient;
        }

        [HttpGet("listar-todos")]
        public async Task<IActionResult> Index()
        {
            var generos = await _generoApiClient.GetGenerosAsync();
            return View(generos);
        }

        [HttpGet("detalhes/{id:int}")]
        public async Task<IActionResult> Detalhes(int id)
        {
            var genero = await _generoApiClient.GetGeneroAsync(id);
            return View(genero);
        }

        [Route("novo")]
        public IActionResult Novo()
        {
            return View();
        }

        [HttpPost("novo")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Novo(GeneroViewModel generoViewModel)
        {
            if (!ModelState.IsValid) return View(generoViewModel);

            var resposta = await _generoApiClient.PostGeneroAsync(generoViewModel);

            if (resposta.Errors != null)
            {
                ModelState.AddModelError("Erro", resposta.Errors.FirstOrDefault());
                return View(generoViewModel);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("editar/{id:int}")]
        public async Task<IActionResult> Editar(int id)
        {
            var generoViewModel = await _generoApiClient.GetGeneroAsync(id);

            if (generoViewModel == null) return NotFound();

            return View(generoViewModel);
        }

        [HttpPost("editar/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, GeneroViewModel generoViewModel)
        {
            if (id != generoViewModel.GeneroId) return NotFound();

            if (!ModelState.IsValid) return View(generoViewModel);

            var resposta = await _generoApiClient.PutGeneroAsync(id, generoViewModel);

            if (resposta.Errors != null)
            {
                ModelState.AddModelError("Erro", resposta.Errors.FirstOrDefault());
                return View(generoViewModel);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("excluir/{id:int}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var genero = await _generoApiClient.GetGeneroAsync(id);
            return View(genero);
        }

        [HttpPost("excluir/{id:int}"), ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmacaoExclusao(int id)
        {
            var genero = await _generoApiClient.GetGeneroAsync(id);

            if (genero == null) return NotFound();

            var resposta = await _generoApiClient.DeleteGeneroAsync(id);

            if (resposta.Errors != null)
            {
                ModelState.AddModelError("Erro", resposta.Errors.FirstOrDefault());
                return View(genero);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
