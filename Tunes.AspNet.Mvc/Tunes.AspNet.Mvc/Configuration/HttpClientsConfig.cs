using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Tunes.AspNet.Mvc.HttpClients;

namespace Tunes.AspNet.Mvc.Configuration
{
    public static class HttpClientsConfig
    {
        public static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration configuration)
        {
            var apiUrlV1 = configuration.GetSection("ApiUrlV1").Value;

            services.AddHttpClient<AuthApiClient>(options =>
            {
                options.BaseAddress = new Uri($"{apiUrlV1}conta/");
            });

            services.AddHttpClient<ArtistaApiClient>(options =>
            {
                options.BaseAddress = new Uri($"{apiUrlV1}artistas/");
            });

            services.AddHttpClient<AlbumApiClient>(options =>
            {
                options.BaseAddress = new Uri($"{apiUrlV1}albuns/");
            });

            services.AddHttpClient<GeneroApiClient>(options =>
            {
                options.BaseAddress = new Uri($"{apiUrlV1}generos/");
            });

            services.AddHttpClient<TipoMidiaApiClient>(options =>
            {
                options.BaseAddress = new Uri($"{apiUrlV1}tipos-de-midia/");
            });

            services.AddHttpClient<PlaylistApiClient>(options =>
            {
                options.BaseAddress = new Uri($"{apiUrlV1}playlists/");
            });

            services.AddHttpClient<FaixaApiClient>(options =>
            {
                options.BaseAddress = new Uri($"{apiUrlV1}faixas/");
            });

            services.AddHttpClient<FuncionarioApiClient>(options =>
            {
                options.BaseAddress = new Uri($"{apiUrlV1}funcionarios/");
            });

            services.AddHttpClient<ClienteApiClient>(options =>
            {
                options.BaseAddress = new Uri($"{apiUrlV1}clientes/");
            });

            return services;
        }
    }
}
