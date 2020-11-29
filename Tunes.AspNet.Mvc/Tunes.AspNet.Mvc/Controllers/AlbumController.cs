using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tunes.AspNet.Mvc.HttpClients;
using Tunes.AspNet.Mvc.Models;

namespace Tunes.AspNet.Mvc.Controllers
{
    [Route("albuns")]
    public class AlbumController : Controller
    {
        private readonly AlbumApiClient _albumApiClient;
        private readonly ArtistaApiClient _artistaApiClient;

        public AlbumController(AlbumApiClient albumApiClient, ArtistaApiClient artistaApiClient)
        {
            _albumApiClient = albumApiClient;
            _artistaApiClient = artistaApiClient;
        }

        [HttpGet("listar-todos")]
        public async Task<IActionResult> Index()
        {
            var albuns = await _albumApiClient.GetAlbunsAsync();
            return View(albuns);
        }

        [HttpGet("detalhes/{id:int}")]
        public async Task<IActionResult> Detalhes(int id)
        {
            var album = await _albumApiClient.GetAlbumAsync(id);
            return View(album);
        }

        [Route("novo")]
        public async Task<IActionResult> Novo()
        {
            var albumViewModel = await PopularArtistas(new AlbumViewModel());

            return View(albumViewModel);
        }

        [HttpPost("novo")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Novo(AlbumViewModel albumViewModel)
        {
            if (!ModelState.IsValid) return View(albumViewModel);

            albumViewModel.Artista = await _artistaApiClient.GetArtistaAsync(albumViewModel.ArtistaId);

            var resposta = await _albumApiClient.PostAlbumAsync(albumViewModel);

            if (resposta.Errors != null)
            {
                ModelState.AddModelError("Erro", resposta.Errors.FirstOrDefault());
                return View(albumViewModel);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("editar/{id:int}")]
        public async Task<IActionResult> Editar(int id)
        {
            var albumViewModel = await _albumApiClient.GetAlbumAsync(id);

            albumViewModel = await PopularArtistas(albumViewModel);

            albumViewModel.ArtistaId = albumViewModel.Artista.ArtistaId;

            if (albumViewModel == null) return NotFound();

            return View(albumViewModel);
        }

        [HttpPost("editar/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, AlbumViewModel albumViewModel)
        {
            if (id != albumViewModel.AlbumId) return NotFound();

            if (!ModelState.IsValid) return View(albumViewModel);

            albumViewModel.Artista = await _artistaApiClient.GetArtistaAsync(albumViewModel.ArtistaId);

            var resposta = await _albumApiClient.PutAlbumAsync(id, albumViewModel);

            if (resposta.Errors != null)
            {
                ModelState.AddModelError("Erro", resposta.Errors.FirstOrDefault());
                return View(albumViewModel);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("excluir/{id:int}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var album = await _albumApiClient.GetAlbumAsync(id);
            return View(album);
        }

        [HttpPost("excluir/{id:int}"), ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmacaoExclusao(int id)
        {
            var album = await _albumApiClient.GetAlbumAsync(id);

            if (album == null) return NotFound();

            var resposta = await _albumApiClient.DeleteAlbumAsync(id);

            if (resposta.Errors != null)
            {
                ModelState.AddModelError("Erro", resposta.Errors.FirstOrDefault());
                return View(album);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<AlbumViewModel> PopularArtistas(AlbumViewModel albumViewModel)
        {
            albumViewModel.Artistas = await _artistaApiClient.GetArtistasAsync();
            return albumViewModel;
        }
    }
}
