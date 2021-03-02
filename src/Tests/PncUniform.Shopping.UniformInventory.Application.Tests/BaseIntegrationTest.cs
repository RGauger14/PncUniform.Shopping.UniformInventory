using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using PncUniform.Shopping.UniformInventory.API;

namespace PncUniform.Shopping.UniformInventory.Application.Tests
{
    public abstract class BaseIntegrationTest : IDisposable
    {
        protected TestServer _testServer;
        private bool _disposedValue;

        public BaseIntegrationTest()
        {
            SetupTestAsync();
        }

        protected void ConfigureTestHost()
        {
            var webHostBuilder = new WebHostBuilder().UseStartup<Startup>();
            _testServer = new TestServer(webHostBuilder);
        }

        protected virtual Task SetupTestAsync()
        {
            ConfigureTestHost();

            return Task.CompletedTask;
        }

        protected virtual Task TeardownTestAsync()
        {
            _testServer.Dispose();

            return Task.CompletedTask;
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
