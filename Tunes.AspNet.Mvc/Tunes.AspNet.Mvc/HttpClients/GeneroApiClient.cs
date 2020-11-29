using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Tunes.AspNet.Mvc.Models;

namespace Tunes.AspNet.Mvc.HttpClients
{
    public class GeneroApiClient : BaseClient
    {
        private readonly HttpClient _httpClient;

        public GeneroApiClient(HttpClient httpClient, IHttpContextAccessor accessor) : base(httpClient, accessor)
        {
            _httpClient = httpClient;
        }

        public async Task<List<GeneroViewModel>> GetGenerosAsync()
        {
            var resposta = await _httpClient.GetAsync("");
            resposta.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<GeneroViewModel>>(await resposta.Content.ReadAsStringAsync(), JsonSerializerOptions);
        }

        public async Task<GeneroViewModel> GetGeneroAsync(int id)
        {
            var resposta = await _httpClient.GetAsync($"{id}");
            resposta.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<GeneroViewModel>(await resposta.Content.ReadAsStringAsync(), JsonSerializerOptions);
        }

        public async Task<CustomResponse<GeneroViewModel>> PostGeneroAsync(GeneroViewModel generoViewModel)
        {
            var genero = JsonSerializer.Serialize(generoViewModel, JsonSerializerOptions);

            var conteudo = new StringContent(genero, Encoding.UTF8, "application/json");
            var resposta = await _httpClient.PostAsync("", conteudo);

            var json = await resposta.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<CustomResponse<GeneroViewModel>>(json, JsonSerializerOptions);
        }

        public async Task<CustomResponse<GeneroViewModel>> PutGeneroAsync(int id, GeneroViewModel generoViewModel)
        {
            var genero = JsonSerializer.Serialize(generoViewModel, JsonSerializerOptions);

            var conteudo = new StringContent(genero, Encoding.UTF8, "application/json");
            var resposta = await _httpClient.PutAsync($"{id}", conteudo);

            var json = await resposta.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<CustomResponse<GeneroViewModel>>(json, JsonSerializerOptions);
        }

        public async Task<CustomResponse<GeneroViewModel>> DeleteGeneroAsync(int id)
        {
            var resposta = await _httpClient.DeleteAsync($"{id}");

            return JsonSerializer.Deserialize<CustomResponse<GeneroViewModel>>(await resposta.Content.ReadAsStringAsync(), JsonSerializerOptions);
        }
    }
}
