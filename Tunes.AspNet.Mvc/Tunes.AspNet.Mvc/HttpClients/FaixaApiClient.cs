using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Tunes.AspNet.Mvc.Models;

namespace Tunes.AspNet.Mvc.HttpClients
{
    public class FaixaApiClient : BaseClient
    {
        private readonly HttpClient _httpClient;

        public FaixaApiClient(HttpClient httpClient, IHttpContextAccessor accessor) : base(httpClient, accessor)
        {
            _httpClient = httpClient;
        }

        public async Task<List<FaixaViewModel>> GetFaixasAsync()
        {
            var resposta = await _httpClient.GetAsync("");
            resposta.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<FaixaViewModel>>(await resposta.Content.ReadAsStringAsync(), JsonSerializerOptions);
        }

        public async Task<FaixaViewModel> GetFaixaAsync(int id)
        {
            var resposta = await _httpClient.GetAsync($"{id}");
            resposta.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<FaixaViewModel>(await resposta.Content.ReadAsStringAsync(), JsonSerializerOptions);
        }

        public async Task<CustomResponse<FaixaViewModel>> PostFaixaAsync(FaixaViewModel faixaViewModel)
        {
            var faixa = JsonSerializer.Serialize(faixaViewModel, JsonSerializerOptions);

            var conteudo = new StringContent(faixa, Encoding.UTF8, "application/json");
            var resposta = await _httpClient.PostAsync("", conteudo);

            var json = await resposta.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<CustomResponse<FaixaViewModel>>(json, JsonSerializerOptions);
        }

        public async Task<CustomResponse<FaixaViewModel>> PutFaixaAsync(int id, FaixaViewModel faixaViewModel)
        {
            var faixa = JsonSerializer.Serialize(faixaViewModel, JsonSerializerOptions);

            var conteudo = new StringContent(faixa, Encoding.UTF8, "application/json");
            var resposta = await _httpClient.PutAsync($"{id}", conteudo);

            var json = await resposta.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<CustomResponse<FaixaViewModel>>(json, JsonSerializerOptions);
        }

        public async Task<CustomResponse<FaixaViewModel>> DeleteFaixaAsync(int id)
        {
            var resposta = await _httpClient.DeleteAsync($"{id}");

            return JsonSerializer.Deserialize<CustomResponse<FaixaViewModel>>(await resposta.Content.ReadAsStringAsync(), JsonSerializerOptions);
        }
    }
}
