﻿namespace NerdStore.WebApp.MVC
{
    public interface IStartup
    {
        IConfiguration Configuration { get; }
        void Configure(WebApplication app, IWebHostEnvironment env);
        void ConfigureServices(IServiceCollection services);
    }
}
