using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Tunes.AspNet.Mvc.HttpClients;
using Tunes.AspNet.Mvc.Models;

namespace Tunes.AspNet.Mvc.Controllers
{
    [Route("clientes")]
    public class ClienteController : Controller
    {
        private readonly ClienteApiClient _clienteApiClient;
        private readonly FuncionarioApiClient _funcionarioApiClient;

        public ClienteController(ClienteApiClient clienteApiClient, FuncionarioApiClient funcionarioApiClient)
        {
            _clienteApiClient = clienteApiClient;
            _funcionarioApiClient = funcionarioApiClient;
        }

        [HttpGet("listar-todos")]
        public async Task<IActionResult> Index()
        {
            var clientes = await _clienteApiClient.GetClientesAsync();
            return View(clientes);
        }

        [HttpGet("detalhes/{id:int}")]
        public async Task<IActionResult> Detalhes(int id)
        {
            var cliente = await _clienteApiClient.GetClienteAsync(id);
            return View(cliente);
        }

        [Route("novo")]
        public async Task<IActionResult> Novo()
        {
            var clienteViewModel = await PopularSuporteTecnico(new ClienteViewModel());

            return View(clienteViewModel);
        }

        [HttpPost("novo")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Novo(ClienteViewModel clienteViewModel)
        {
            if (!ModelState.IsValid) return View(clienteViewModel);

            clienteViewModel.Suporte = await _funcionarioApiClient.GetFuncionarioAsync(clienteViewModel.SuporteId);

            var resposta = await _clienteApiClient.PostClienteAsync(clienteViewModel);

            if (resposta.Errors != null)
            {
                ModelState.AddModelError("Erro", resposta.Errors.FirstOrDefault());
                return View(clienteViewModel);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("editar/{id:int}")]
        public async Task<IActionResult> Editar(int id)
        {
            var clienteViewModel = await _clienteApiClient.GetClienteAsync(id);

            clienteViewModel = await PopularSuporteTecnico(clienteViewModel);

            clienteViewModel.SuporteId = clienteViewModel.Suporte?.FuncionarioId ?? 0;

            if (clienteViewModel == null) return NotFound();

            return View(clienteViewModel);
        }

        [HttpPost("editar/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, ClienteViewModel clienteViewModel)
        {
            if (id != clienteViewModel.ClienteId) return NotFound();

            if (!ModelState.IsValid) return View(clienteViewModel);

            clienteViewModel.Suporte = await _funcionarioApiClient.GetFuncionarioAsync(clienteViewModel.SuporteId);

            var resposta = await _clienteApiClient.PutClienteAsync(id, clienteViewModel);

            if (resposta.Errors != null)
            {
                ModelState.AddModelError("Erro", resposta.Errors.FirstOrDefault());
                return View(clienteViewModel);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("excluir/{id:int}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var cliente = await _clienteApiClient.GetClienteAsync(id);
            return View(cliente);
        }

        [HttpPost("excluir/{id:int}"), ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmacaoExclusao(int id)
        {
            var cliente = await _clienteApiClient.GetClienteAsync(id);

            if (cliente == null) return NotFound();

            var resposta = await _clienteApiClient.DeleteClienteAsync(id);

            if (resposta.Errors != null)
            {
                ModelState.AddModelError("Erro", resposta.Errors.FirstOrDefault());
                return View(cliente);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<ClienteViewModel> PopularSuporteTecnico(ClienteViewModel clienteViewModel)
        {
            clienteViewModel.SuporteTecnico = await _funcionarioApiClient.GetFuncionariosAsync();
            return clienteViewModel;
        }
    }
}
