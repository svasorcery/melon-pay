﻿using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace MelonPay.Shared.Infrastructure.Postgres
{
    internal sealed class DbContextAppInitializer : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<DbContextAppInitializer> _logger;

        public DbContextAppInitializer(IServiceProvider serviceProvider, ILogger<DbContextAppInitializer> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var dbContextTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => typeof(DbContext).IsAssignableFrom(x) && !x.IsInterface && x != typeof(DbContext));

            using var scope = _serviceProvider.CreateScope();
            foreach (var dbContextType in dbContextTypes)
            {
                var dbContext = scope.ServiceProvider.GetService(dbContextType) as DbContext;
                if (dbContext is null)
                {
                    continue;
                }

                _logger.LogInformation($"Running DB context for module: '{dbContextType.Name}'.");

                await dbContext.Database.MigrateAsync(cancellationToken);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
            => Task.CompletedTask;
    }
}
