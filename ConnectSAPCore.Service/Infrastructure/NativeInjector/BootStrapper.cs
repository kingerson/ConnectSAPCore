using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ConnectSAPCore.Service.Infrastructure.NativeInjector
{
    public class BootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}
