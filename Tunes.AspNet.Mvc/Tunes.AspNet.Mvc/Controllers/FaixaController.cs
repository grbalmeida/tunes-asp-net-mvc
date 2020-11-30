using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Tunes.AspNet.Mvc.HttpClients;
using Tunes.AspNet.Mvc.Models;

namespace Tunes.AspNet.Mvc.Controllers
{
    [Route("faixas")]
    public class FaixaController : Controller
    {
        private readonly FaixaApiClient _faixaApiClient;
        private readonly AlbumApiClient _albumApiClient;
        private readonly TipoMidiaApiClient _tipoMidiaApiClient;
        private readonly GeneroApiClient _generoApiClient;

        public FaixaController(
            FaixaApiClient faixaApiClient,
            AlbumApiClient albumApiClient,
            TipoMidiaApiClient tipoMidiaApiClient,
            GeneroApiClient generoApiClient)
        {
            _faixaApiClient = faixaApiClient;
            _albumApiClient = albumApiClient;
            _tipoMidiaApiClient = tipoMidiaApiClient;
            _generoApiClient = generoApiClient;
        }

        [HttpGet("listar-todos")]
        public async Task<IActionResult> Index()
        {
            var faixas = await _faixaApiClient.GetFaixasAsync();
            return View(faixas);
        }

        [HttpGet("detalhes/{id:int}")]
        public async Task<IActionResult> Detalhes(int id)
        {
            var faixa = await _faixaApiClient.GetFaixaAsync(id);
            return View(faixa);
        }

        [Route("novo")]
        public async Task<IActionResult> Novo()
        {
            var faixaViewModel = await PopularCombos(new FaixaViewModel());

            return View(faixaViewModel);
        }

        [HttpPost("novo")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Novo(FaixaViewModel faixaViewModel)
        {
            if (!ModelState.IsValid) return View(faixaViewModel);

            faixaViewModel.Album = await _albumApiClient.GetAlbumAsync(faixaViewModel.AlbumId);
            faixaViewModel.TipoMidia = await _tipoMidiaApiClient.GetTipoMidiaAsync(faixaViewModel.TipoMidiaId);
            faixaViewModel.Genero = await _generoApiClient.GetGeneroAsync(faixaViewModel.GeneroId);

            var resposta = await _faixaApiClient.PostFaixaAsync(faixaViewModel);

            if (resposta.Errors != null)
            {
                ModelState.AddModelError("Erro", resposta.Errors.FirstOrDefault());
                return View(faixaViewModel);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("editar/{id:int}")]
        public async Task<IActionResult> Editar(int id)
        {
            var faixaViewModel = await _faixaApiClient.GetFaixaAsync(id);

            faixaViewModel = await PopularCombos(faixaViewModel);

            faixaViewModel.AlbumId = faixaViewModel.Album?.AlbumId ?? 0;
            faixaViewModel.TipoMidiaId = faixaViewModel.TipoMidia?.TipoMidiaId ?? 0;
            faixaViewModel.GeneroId = faixaViewModel.Genero?.GeneroId ?? 0;

            if (faixaViewModel == null) return NotFound();

            return View(faixaViewModel);
        }

        [HttpPost("editar/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, FaixaViewModel faixaViewModel)
        {
            if (id != faixaViewModel.FaixaId) return NotFound();

            if (!ModelState.IsValid) return View(faixaViewModel);

            faixaViewModel.Album = await _albumApiClient.GetAlbumAsync(faixaViewModel.AlbumId);
            faixaViewModel.TipoMidia = await _tipoMidiaApiClient.GetTipoMidiaAsync(faixaViewModel.TipoMidiaId);
            faixaViewModel.Genero = await _generoApiClient.GetGeneroAsync(faixaViewModel.GeneroId);

            var resposta = await _faixaApiClient.PutFaixaAsync(id, faixaViewModel);

            if (resposta.Errors != null)
            {
                ModelState.AddModelError("Erro", resposta.Errors.FirstOrDefault());
                return View(faixaViewModel);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("excluir/{id:int}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var faixa = await _faixaApiClient.GetFaixaAsync(id);
            return View(faixa);
        }

        [HttpPost("excluir/{id:int}"), ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmacaoExclusao(int id)
        {
            var faixa = await _faixaApiClient.GetFaixaAsync(id);

            if (faixa == null) return NotFound();

            var resposta = await _faixaApiClient.DeleteFaixaAsync(id);

            if (resposta.Errors != null)
            {
                ModelState.AddModelError("Erro", resposta.Errors.FirstOrDefault());
                return View(faixa);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<FaixaViewModel> PopularCombos(FaixaViewModel faixaViewModel)
        {
            faixaViewModel.Albuns = await _albumApiClient.GetAlbunsAsync();
            faixaViewModel.TiposDeMidia = await _tipoMidiaApiClient.GetTiposDeMidiaAsync();
            faixaViewModel.Generos = await _generoApiClient.GetGenerosAsync();
            return faixaViewModel;
        }
    }
}
