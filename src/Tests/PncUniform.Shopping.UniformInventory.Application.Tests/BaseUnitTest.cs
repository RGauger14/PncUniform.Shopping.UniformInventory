using System;
using System.Threading.Tasks;
using AutoFixture;
using Microsoft.Extensions.DependencyInjection;
using PncUniform.Shopping.UniformInventory.Application;

namespace PncUniform.Shopping.uniformInventory.BehaviourTests
{
    public abstract class BaseUnitTest : IDisposable
    {
        private readonly IServiceCollection _services;
        private readonly IServiceProvider _provider;

        public Fixture Fixture { get; }

        public BaseUnitTest()
        {
            _services = new ServiceCollection();
            ConfigureServices(_services);

            _provider = _services.BuildServiceProvider();
            Fixture = new Fixture();

            SetupTestAsync().GetAwaiter().GetResult();
        }

        public T GetRequiredService<T>()
        {
            return _provider.GetRequiredService<T>();
        }

        public virtual IServiceCollection ConfigureServices(IServiceCollection services)
        {
            services.AddApplication();
            return services;
        }

        protected abstract Task SetupTestAsync();

        protected abstract Task TearDownTestAsync();

        public void Dispose()
        {
            TearDownTestAsync().GetAwaiter().GetResult();
        }
    }
}
