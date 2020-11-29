using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Tunes.AspNet.Mvc.HttpClients;
using Tunes.AspNet.Mvc.Models;

namespace Tunes.AspNet.Mvc.Controllers
{
    [Route("tipos-de-midia")]
    public class TipoMidiaController : Controller
    {
        private readonly TipoMidiaApiClient _tipoMidiaApiClient;

        public TipoMidiaController(TipoMidiaApiClient tipoMidiaApiClient)
        {
            _tipoMidiaApiClient = tipoMidiaApiClient;
        }

        [HttpGet("listar-todos")]
        public async Task<IActionResult> Index()
        {
            var tiposDeMidia = await _tipoMidiaApiClient.GetTiposDeMidiaAsync();
            return View(tiposDeMidia);
        }

        [HttpGet("detalhes/{id:int}")]
        public async Task<IActionResult> Detalhes(int id)
        {
            var tipoMidia = await _tipoMidiaApiClient.GetTipoMidiaAsync(id);
            return View(tipoMidia);
        }

        [Route("novo")]
        public IActionResult Novo()
        {
            return View();
        }

        [HttpPost("novo")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Novo(TipoMidiaViewModel tipoMidiaViewModel)
        {
            if (!ModelState.IsValid) return View(tipoMidiaViewModel);

            var resposta = await _tipoMidiaApiClient.PostTipoMidiaAsync(tipoMidiaViewModel);

            if (resposta.Errors != null)
            {
                ModelState.AddModelError("Erro", resposta.Errors.FirstOrDefault());
                return View(tipoMidiaViewModel);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("editar/{id:int}")]
        public async Task<IActionResult> Editar(int id)
        {
            var tipoMidiaViewModel = await _tipoMidiaApiClient.GetTipoMidiaAsync(id);

            if (tipoMidiaViewModel == null) return NotFound();

            return View(tipoMidiaViewModel);
        }

        [HttpPost("editar/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, TipoMidiaViewModel tipoMidiaViewModel)
        {
            if (id != tipoMidiaViewModel.TipoMidiaId) return NotFound();

            if (!ModelState.IsValid) return View(tipoMidiaViewModel);

            var resposta = await _tipoMidiaApiClient.PutTipoMidiaAsync(id, tipoMidiaViewModel);

            if (resposta.Errors != null)
            {
                ModelState.AddModelError("Erro", resposta.Errors.FirstOrDefault());
                return View(tipoMidiaViewModel);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("excluir/{id:int}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var tipoMidia = await _tipoMidiaApiClient.GetTipoMidiaAsync(id);
            return View(tipoMidia);
        }

        [HttpPost("excluir/{id:int}"), ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmacaoExclusao(int id)
        {
            var tipoMidia = await _tipoMidiaApiClient.GetTipoMidiaAsync(id);

            if (tipoMidia == null) return NotFound();

            var resposta = await _tipoMidiaApiClient.DeleteTipoMidiaAsync(id);

            if (resposta.Errors != null)
            {
                ModelState.AddModelError("Erro", resposta.Errors.FirstOrDefault());
                return View(tipoMidia);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
