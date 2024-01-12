using aehyok.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Configuration;

namespace aehyok.Serilog
{
    public static class LoggerExtensions
    {
        public static LoggerConfiguration WithIpAddress(this LoggerEnrichmentConfiguration enrichmentConfiguration, IServiceProvider serviceProvider)
        {
            if (enrichmentConfiguration == null)
            {
                throw new ArgumentNullException(nameof(enrichmentConfiguration));
            }

            var httpContextAccessor = serviceProvider.GetService<IHttpContextAccessor>();
            var ipAddressEnricher = new IpAddressEnricher(httpContextAccessor);
            return enrichmentConfiguration.With(ipAddressEnricher);
        }

        public static LoggerConfiguration WithWorker(this LoggerEnrichmentConfiguration enrichmentConfiguration)
        {
            if (enrichmentConfiguration == null)
            {
                throw new ArgumentNullException(nameof(enrichmentConfiguration));
            }

            var workerEnricher = new WorkerEnricher();
            return enrichmentConfiguration.With(workerEnricher);
        }

        public static LoggerConfiguration WithToken(this LoggerEnrichmentConfiguration enrichmentConfiguration, IServiceProvider serviceProvider)
        {
            if (enrichmentConfiguration is null)
            {
                throw new ArgumentNullException(nameof(enrichmentConfiguration));
            }

            serviceProvider = serviceProvider.CreateAsyncScope().ServiceProvider;
            var currentUser = serviceProvider.GetService<ICurrentUser>();
            var enricher = new TokenEnricher(currentUser);
            return enrichmentConfiguration.With(enricher);
        }
    }
}
