﻿using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Tunes.AspNet.Mvc.Models;

namespace Tunes.AspNet.Mvc.HttpClients
{
    public class ArtistaApiClient : BaseClient
    {
        private readonly HttpClient _httpClient;

        public ArtistaApiClient(HttpClient httpClient, IHttpContextAccessor accessor) : base(httpClient, accessor)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ArtistaViewModel>> GetArtistasAsync()
        {
            var resposta = await _httpClient.GetAsync("");
            resposta.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<ArtistaViewModel>>(await resposta.Content.ReadAsStringAsync(), JsonSerializerOptions);
        }

        public async Task<ArtistaViewModel> GetArtistaAsync(int id)
        {
            var resposta = await _httpClient.GetAsync($"{id}");
            resposta.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<ArtistaViewModel>(await resposta.Content.ReadAsStringAsync(), JsonSerializerOptions);
        }

        public async Task<CustomResponse<ArtistaViewModel>> PostArtistaAsync(ArtistaViewModel artistaViewModel)
        {
            var artista = JsonSerializer.Serialize(artistaViewModel, JsonSerializerOptions);

            var conteudo = new StringContent(artista, Encoding.UTF8, "application/json");
            var resposta = await _httpClient.PostAsync("", conteudo);

            var json = await resposta.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<CustomResponse<ArtistaViewModel>>(json, JsonSerializerOptions);
        }

        public async Task<CustomResponse<ArtistaViewModel>> PutArtistaAsync(int id, ArtistaViewModel artistaViewModel)
        {
            var artista = JsonSerializer.Serialize(artistaViewModel, JsonSerializerOptions);

            var conteudo = new StringContent(artista, Encoding.UTF8, "application/json");
            var resposta = await _httpClient.PutAsync($"{id}", conteudo);

            var json = await resposta.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<CustomResponse<ArtistaViewModel>>(json, JsonSerializerOptions);
        }

        public async Task<CustomResponse<ArtistaViewModel>> DeleteArtistaAsync(int id)
        {
            var resposta = await _httpClient.DeleteAsync($"{id}");

            return JsonSerializer.Deserialize<CustomResponse<ArtistaViewModel>>(await resposta.Content.ReadAsStringAsync(), JsonSerializerOptions);
        }
    }
}
