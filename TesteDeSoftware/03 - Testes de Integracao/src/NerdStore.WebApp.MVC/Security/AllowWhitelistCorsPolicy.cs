using Microsoft.AspNetCore.Cors.Infrastructure;

namespace NerdStore.WebApp.MVC.Security
{
    public class AllowWhitelistCorsPolicy
    {
        public const string Name = "AllowWhitelist";

        public static CorsPolicy Get(string[] whitelist)
        {
            return new CorsPolicyBuilder()
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .Build();
        }
    }
}
