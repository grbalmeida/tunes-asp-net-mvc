using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Tunes.AspNet.Mvc.HttpClients;
using Tunes.AspNet.Mvc.Models;

namespace Tunes.AspNet.Mvc.Controllers
{
    [Route("playlists")]
    public class PlaylistController : Controller
    {
        private readonly PlaylistApiClient _playlistApiClient;

        public PlaylistController(PlaylistApiClient playlistApiClient)
        {
            _playlistApiClient = playlistApiClient;
        }

        [HttpGet("listar-todos")]
        public async Task<IActionResult> Index()
        {
            var playlists = await _playlistApiClient.GetPlaylistsAsync();
            return View(playlists);
        }

        [HttpGet("detalhes/{id:int}")]
        public async Task<IActionResult> Detalhes(int id)
        {
            var playlist = await _playlistApiClient.GetPlaylistAsync(id);
            return View(playlist);
        }

        [Route("novo")]
        public IActionResult Novo()
        {
            return View();
        }

        [HttpPost("novo")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Novo(PlaylistViewModel playlistViewModel)
        {
            if (!ModelState.IsValid) return View(playlistViewModel);

            var resposta = await _playlistApiClient.PostPlaylistAsync(playlistViewModel);

            if (resposta.Errors != null)
            {
                ModelState.AddModelError("Erro", resposta.Errors.FirstOrDefault());
                return View(playlistViewModel);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("editar/{id:int}")]
        public async Task<IActionResult> Editar(int id)
        {
            var playlistViewModel = await _playlistApiClient.GetPlaylistAsync(id);

            if (playlistViewModel == null) return NotFound();

            return View(playlistViewModel);
        }

        [HttpPost("editar/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, PlaylistViewModel playlistViewModel)
        {
            if (id != playlistViewModel.PlaylistId) return NotFound();

            if (!ModelState.IsValid) return View(playlistViewModel);

            var resposta = await _playlistApiClient.PutPlaylistAsync(id, playlistViewModel);

            if (resposta.Errors != null)
            {
                ModelState.AddModelError("Erro", resposta.Errors.FirstOrDefault());
                return View(playlistViewModel);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("excluir/{id:int}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var playlist = await _playlistApiClient.GetPlaylistAsync(id);
            return View(playlist);
        }

        [HttpPost("excluir/{id:int}"), ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmacaoExclusao(int id)
        {
            var playlist = await _playlistApiClient.GetPlaylistAsync(id);

            if (playlist == null) return NotFound();

            var resposta = await _playlistApiClient.DeletePlaylistAsync(id);

            if (resposta.Errors != null)
            {
                ModelState.AddModelError("Erro", resposta.Errors.FirstOrDefault());
                return View(playlist);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
