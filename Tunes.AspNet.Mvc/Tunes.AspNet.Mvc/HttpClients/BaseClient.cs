using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Tunes.AspNet.Mvc.HttpClients
{
    public abstract class BaseClient
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _accessor;
        protected JsonSerializerOptions JsonSerializerOptions =>
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        public BaseClient(HttpClient httpClient, IHttpContextAccessor accessor)
        {
            _httpClient = httpClient;
            _accessor = accessor;
            AddBearerToken();
        }

        protected void AddBearerToken()
        {
            var token = _accessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Token")?.Value;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        protected string EnvolveComAspasDuplas(string valor)
        {
            return $"\"{valor}\"";
        }
    }
}