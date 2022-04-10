using Microsoft.Extensions.DependencyInjection;
using System;
namespace MarsRover
{
   public class DI
    {
        public static IServiceProvider Configure()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddTransient<IRover, Rover>();
        
            return services.BuildServiceProvider();
        }
    }
}
