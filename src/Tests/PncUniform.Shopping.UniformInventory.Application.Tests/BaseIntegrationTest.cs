using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using PncUniform.Shopping.UniformInventory.API;
using Respawn;
using AutoFixture;
using Xunit;

namespace PncUniform.Shopping.UniformInventory.Application.Tests
{
    [Collection("Integration")]
    public abstract class BaseIntegrationTest : IDisposable
    {
        protected TestServer _testServer;
        protected Fixture _fixture;
        private ApplicationOptions _appOptions;
        private bool _disposedValue;
        private Checkpoint _checkpoint;

        public BaseIntegrationTest()
        {
            SetupTestAsync();
        }

        protected void ConfigureTestHost()
        {
            
            var webHostBuilder = new WebHostBuilder()
                .ConfigureAppConfiguration(config =>
                {
                    config.AddJsonFile($"{AppDomain.CurrentDomain.BaseDirectory}/appsettings.json");
                    config.AddUserSecrets(Assembly.GetExecutingAssembly(), optional: true);
                })
                .UseStartup<Startup>();

            _testServer = new TestServer(webHostBuilder);
            _fixture = new Fixture();
            _appOptions = _testServer.Services.GetRequiredService<IOptions<ApplicationOptions>>().Value;
        }

        protected virtual Task SetupTestAsync()
        {
            ConfigureTestHost();
            CreateDatabaseCheckpoint();

            return Task.CompletedTask;
        }

        protected async virtual Task TeardownTestAsync()
        {
            await RestoreDatabaseCheckpointAsync();
            _testServer.Dispose();
        }

        private void CreateDatabaseCheckpoint()
        {
            _checkpoint = new Checkpoint();
        }


        private Task RestoreDatabaseCheckpointAsync()
        {
            return _checkpoint.Reset(_appOptions.DbConnectionString);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // dispose managed state (managed objects)
                    TeardownTestAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                }

                // free unmanaged resources (unmanaged objects) and override finalizer
                // set large fields to null
                _disposedValue = true;
            }
        }

        // // override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~BaseIntegrationTest()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
