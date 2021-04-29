using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

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