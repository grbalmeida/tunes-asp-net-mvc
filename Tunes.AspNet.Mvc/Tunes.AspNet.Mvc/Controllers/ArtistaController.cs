using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Tunes.AspNet.Mvc.HttpClients;
using Tunes.AspNet.Mvc.Models;

namespace Tunes.AspNet.Mvc.Controllers
{
    [Route("artistas")]
    public class ArtistaController : Controller
    {
        private readonly ArtistaApiClient _artistaApiClient;

        public ArtistaController(ArtistaApiClient artistaApiClient)
        {
            _artistaApiClient = artistaApiClient;
        }

        [HttpGet("listar-todos")]
        public async Task<IActionResult> Index()
        {
            var artistas = await _artistaApiClient.GetArtistasAsync();
            return View(artistas);
        }

        [HttpGet("detalhes/{id:int}")]
        public async Task<IActionResult> Detalhes(int id)
        {
            var artista = await _artistaApiClient.GetArtistaAsync(id);
            return View(artista);
        }

        [Route("novo")]
        public IActionResult Novo()
        {
            return View();
        }

        [HttpPost("novo")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Novo(ArtistaViewModel artistaViewModel)
        {
            if (!ModelState.IsValid) return View(artistaViewModel);

            var resposta = await _artistaApiClient.PostArtistaAsync(artistaViewModel);

            if (resposta.Errors != null)
            {
                ModelState.AddModelError("Erro", resposta.Errors.FirstOrDefault());
                return View(artistaViewModel);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("editar/{id:int}")]
        public async Task<IActionResult> Editar(int id)
        {
            var artistaViewModel = await _artistaApiClient.GetArtistaAsync(id);

            if (artistaViewModel == null) return NotFound();

            return View(artistaViewModel);
        }

        [HttpPost("editar/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, ArtistaViewModel artistaViewModel)
        {
            if (id != artistaViewModel.ArtistaId) return NotFound();

            if (!ModelState.IsValid) return View(artistaViewModel);

            var resposta = await _artistaApiClient.PutArtistaAsync(id, artistaViewModel);

            if (resposta.Errors != null)
            {
                ModelState.AddModelError("Erro", resposta.Errors.FirstOrDefault());
                return View(artistaViewModel);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("excluir/{id:int}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var artista = await _artistaApiClient.GetArtistaAsync(id);
            return View(artista);
        }

        [HttpPost("excluir/{id:int}"), ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmacaoExclusao(int id)
        {
            var artista = await _artistaApiClient.GetArtistaAsync(id);

            if (artista == null) return NotFound();

            var resposta = await _artistaApiClient.DeleteArtistaAsync(id);

            if (resposta.Errors != null)
            {
                ModelState.AddModelError("Erro", resposta.Errors.FirstOrDefault());
                return View(artista);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
