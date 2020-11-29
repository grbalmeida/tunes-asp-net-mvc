using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Tunes.AspNet.Mvc.Models;

namespace Tunes.AspNet.Mvc.HttpClients
{
    public class AuthApiClient : BaseClient
    {
        private readonly HttpClient _httpClient;

        public AuthApiClient(HttpClient httpClient, IHttpContextAccessor accessor) : base(httpClient, accessor)
        {
            _httpClient = httpClient;
        }

        public async Task<LoginRespostaViewModel> PostLoginAsync(LoginUsuarioViewModel model)
        {
            var usuario = JsonSerializer.Serialize(model, JsonSerializerOptions);

            var conteudo = new StringContent(usuario, Encoding.UTF8, "application/json");
            var resposta = await _httpClient.PostAsync("login", conteudo);

            var json = await resposta.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<LoginRespostaViewModel>(json, JsonSerializerOptions);
        }
    }
}