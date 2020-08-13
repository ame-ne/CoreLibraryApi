using CoreLibraryApi.Infrastructure.Interfaces;
using CoreLibraryApi.Models;
using Microsoft.Extensions.Logging;
using System;

namespace CoreLibraryApi.Infrastructure
{
    public class DbLogger : ILogger
    {
        private readonly IGenericRepository<Log> _logRepo;

        public DbLogger(IGenericRepository<Log> logRepo)
        {
            _logRepo = logRepo;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == LogLevel.Information;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (IsEnabled(logLevel) && state is Log)
            {
                _logRepo.CreateAsync(state as Log);
            }
        }
    }
}
