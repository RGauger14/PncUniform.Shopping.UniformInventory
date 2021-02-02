using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;

namespace PncUniform.Shopping.UniformInventory.Application
{
    public static class DepedencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
