using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace NerdStore.WebApp.Tests.Config
{
    public class LojaAppFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseStartup<TStartup>();
            
            // Aqui é onde o ambinte utilizado será o appsettings.Testing, que terá um banco so pra testes e configurações especificas
            builder.UseEnvironment("Testing");
        }
    }
}
