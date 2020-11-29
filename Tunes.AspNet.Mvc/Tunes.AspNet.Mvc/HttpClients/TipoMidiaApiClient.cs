using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Tunes.AspNet.Mvc.Models;

namespace Tunes.AspNet.Mvc.HttpClients
{
    public class TipoMidiaApiClient : BaseClient
    {
        private readonly HttpClient _httpClient;

        public TipoMidiaApiClient(HttpClient httpClient, IHttpContextAccessor accessor) : base(httpClient, accessor)
        {
            _httpClient = httpClient;
        }

        public async Task<List<TipoMidiaViewModel>> GetTiposDeMidiaAsync()
        {
            var resposta = await _httpClient.GetAsync("");
            resposta.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<TipoMidiaViewModel>>(await resposta.Content.ReadAsStringAsync(), JsonSerializerOptions);
        }

        public async Task<TipoMidiaViewModel> GetTipoMidiaAsync(int id)
        {
            var resposta = await _httpClient.GetAsync($"{id}");
            resposta.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<TipoMidiaViewModel>(await resposta.Content.ReadAsStringAsync(), JsonSerializerOptions);
        }

        public async Task<CustomResponse<TipoMidiaViewModel>> PostTipoMidiaAsync(TipoMidiaViewModel tipoMidiaViewModel)
        {
            var tipoMidia = JsonSerializer.Serialize(tipoMidiaViewModel, JsonSerializerOptions);

            var conteudo = new StringContent(tipoMidia, Encoding.UTF8, "application/json");
            var resposta = await _httpClient.PostAsync("", conteudo);

            var json = await resposta.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<CustomResponse<TipoMidiaViewModel>>(json, JsonSerializerOptions);
        }

        public async Task<CustomResponse<TipoMidiaViewModel>> PutTipoMidiaAsync(int id, TipoMidiaViewModel tipoMidiaViewModel)
        {
            var tipoMidia = JsonSerializer.Serialize(tipoMidiaViewModel, JsonSerializerOptions);

            var conteudo = new StringContent(tipoMidia, Encoding.UTF8, "application/json");
            var resposta = await _httpClient.PutAsync($"{id}", conteudo);

            var json = await resposta.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<CustomResponse<TipoMidiaViewModel>>(json, JsonSerializerOptions);
        }

        public async Task<CustomResponse<TipoMidiaViewModel>> DeleteTipoMidiaAsync(int id)
        {
            var resposta = await _httpClient.DeleteAsync($"{id}");

            return JsonSerializer.Deserialize<CustomResponse<TipoMidiaViewModel>>(await resposta.Content.ReadAsStringAsync(), JsonSerializerOptions);
        }
    }
}
