using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Tunes.AspNet.Mvc.Models;

namespace Tunes.AspNet.Mvc.HttpClients
{
    public class ClienteApiClient : BaseClient
    {
        private readonly HttpClient _httpClient;

        public ClienteApiClient(HttpClient httpClient, IHttpContextAccessor accessor) : base(httpClient, accessor)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ClienteViewModel>> GetClientesAsync()
        {
            var resposta = await _httpClient.GetAsync("");
            resposta.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<ClienteViewModel>>(await resposta.Content.ReadAsStringAsync(), JsonSerializerOptions);
        }

        public async Task<ClienteViewModel> GetClienteAsync(int id)
        {
            var resposta = await _httpClient.GetAsync($"{id}");
            resposta.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<ClienteViewModel>(await resposta.Content.ReadAsStringAsync(), JsonSerializerOptions);
        }

        public async Task<CustomResponse<ClienteViewModel>> PostClienteAsync(ClienteViewModel clienteViewModel)
        {
            var cliente = JsonSerializer.Serialize(clienteViewModel, JsonSerializerOptions);

            var conteudo = new StringContent(cliente, Encoding.UTF8, "application/json");
            var resposta = await _httpClient.PostAsync("", conteudo);

            var json = await resposta.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<CustomResponse<ClienteViewModel>>(json, JsonSerializerOptions);
        }

        public async Task<CustomResponse<ClienteViewModel>> PutClienteAsync(int id, ClienteViewModel clienteViewModel)
        {
            var cliente = JsonSerializer.Serialize(clienteViewModel, JsonSerializerOptions);

            var conteudo = new StringContent(cliente, Encoding.UTF8, "application/json");
            var resposta = await _httpClient.PutAsync($"{id}", conteudo);

            var json = await resposta.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<CustomResponse<ClienteViewModel>>(json, JsonSerializerOptions);
        }

        public async Task<CustomResponse<ClienteViewModel>> DeleteClienteAsync(int id)
        {
            var resposta = await _httpClient.DeleteAsync($"{id}");

            return JsonSerializer.Deserialize<CustomResponse<ClienteViewModel>>(await resposta.Content.ReadAsStringAsync(), JsonSerializerOptions);
        }
    }
}