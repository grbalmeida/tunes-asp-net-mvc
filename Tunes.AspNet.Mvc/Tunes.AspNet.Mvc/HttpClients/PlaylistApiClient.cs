using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Tunes.AspNet.Mvc.Models;

namespace Tunes.AspNet.Mvc.HttpClients
{
    public class PlaylistApiClient : BaseClient
    {
        private readonly HttpClient _httpClient;

        public PlaylistApiClient(HttpClient httpClient, IHttpContextAccessor accessor) : base(httpClient, accessor)
        {
            _httpClient = httpClient;
        }

        public async Task<List<PlaylistViewModel>> GetPlaylistsAsync()
        {
            var resposta = await _httpClient.GetAsync("");
            resposta.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<PlaylistViewModel>>(await resposta.Content.ReadAsStringAsync(), JsonSerializerOptions);
        }

        public async Task<PlaylistViewModel> GetPlaylistAsync(int id)
        {
            var resposta = await _httpClient.GetAsync($"{id}");
            resposta.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<PlaylistViewModel>(await resposta.Content.ReadAsStringAsync(), JsonSerializerOptions);
        }

        public async Task<CustomResponse<PlaylistViewModel>> PostPlaylistAsync(PlaylistViewModel playlistViewModel)
        {
            var playlist = JsonSerializer.Serialize(playlistViewModel, JsonSerializerOptions);

            var conteudo = new StringContent(playlist, Encoding.UTF8, "application/json");
            var resposta = await _httpClient.PostAsync("", conteudo);

            var json = await resposta.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<CustomResponse<PlaylistViewModel>>(json, JsonSerializerOptions);
        }

        public async Task<CustomResponse<PlaylistViewModel>> PutPlaylistAsync(int id, PlaylistViewModel playlistViewModel)
        {
            var playlist = JsonSerializer.Serialize(playlistViewModel, JsonSerializerOptions);

            var conteudo = new StringContent(playlist, Encoding.UTF8, "application/json");
            var resposta = await _httpClient.PutAsync($"{id}", conteudo);

            var json = await resposta.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<CustomResponse<PlaylistViewModel>>(json, JsonSerializerOptions);
        }

        public async Task<CustomResponse<PlaylistViewModel>> DeletePlaylistAsync(int id)
        {
            var resposta = await _httpClient.DeleteAsync($"{id}");

            return JsonSerializer.Deserialize<CustomResponse<PlaylistViewModel>>(await resposta.Content.ReadAsStringAsync(), JsonSerializerOptions);
        }
    }
}
