﻿using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ProxyKit
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddProxy(
            this IServiceCollection services,
            Action<IHttpClientBuilder> configureHttpClientBuilder = null)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.Configure(configureHttpClientBuilder);

            var httpClientBuilder = services
                .AddHttpClient<ProxyKitClient>()
                .ConfigurePrimaryHttpMessageHandler(sp => new HttpClientHandler { AllowAutoRedirect = false, UseCookies = false });

            configureHttpClientBuilder?.Invoke(httpClientBuilder);

            return services;
        }
    }
}
