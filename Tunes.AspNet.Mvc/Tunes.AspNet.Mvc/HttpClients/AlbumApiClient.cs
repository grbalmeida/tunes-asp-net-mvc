using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Tunes.AspNet.Mvc.Models;

namespace Tunes.AspNet.Mvc.HttpClients
{
    public class AlbumApiClient : BaseClient
    {
        private readonly HttpClient _httpClient;

        public AlbumApiClient(HttpClient httpClient, IHttpContextAccessor accessor) : base(httpClient, accessor)
        {
            _httpClient = httpClient;
        }

        public async Task<List<AlbumViewModel>> GetAlbunsAsync()
        {
            var resposta = await _httpClient.GetAsync("");
            resposta.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<AlbumViewModel>>(await resposta.Content.ReadAsStringAsync(), JsonSerializerOptions);
        }

        public async Task<AlbumViewModel> GetAlbumAsync(int id)
        {
            var resposta = await _httpClient.GetAsync($"{id}");
            resposta.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<AlbumViewModel>(await resposta.Content.ReadAsStringAsync(), JsonSerializerOptions);
        }

        public async Task<CustomResponse<AlbumViewModel>> PostAlbumAsync(AlbumViewModel albumViewModel)
        {
            var album = JsonSerializer.Serialize(albumViewModel, JsonSerializerOptions);

            var conteudo = new StringContent(album, Encoding.UTF8, "application/json");
            var resposta = await _httpClient.PostAsync("", conteudo);

            var json = await resposta.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<CustomResponse<AlbumViewModel>>(json, JsonSerializerOptions);
        }

        public async Task<CustomResponse<AlbumViewModel>> PutAlbumAsync(int id, AlbumViewModel albumViewModel)
        {
            var album = JsonSerializer.Serialize(albumViewModel, JsonSerializerOptions);

            var conteudo = new StringContent(album, Encoding.UTF8, "application/json");
            var resposta = await _httpClient.PutAsync($"{id}", conteudo);

            var json = await resposta.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<CustomResponse<AlbumViewModel>>(json, JsonSerializerOptions);
        }

        public async Task<CustomResponse<AlbumViewModel>> DeleteAlbumAsync(int id)
        {
            var resposta = await _httpClient.DeleteAsync($"{id}");

            return JsonSerializer.Deserialize<CustomResponse<AlbumViewModel>>(await resposta.Content.ReadAsStringAsync(), JsonSerializerOptions);
        }
    }
}
