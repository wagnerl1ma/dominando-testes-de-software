using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NerdStore.Catalogo.Application.AutoMapper;
using NerdStore.Catalogo.Data;
using NerdStore.Vendas.Data;
using NerdStore.WebApp.MVC.Data;
using NerdStore.WebApp.MVC.Security;
using NerdStore.WebApp.MVC.Setup;

namespace NerdStore.WebApp.MVC
{
    public class Startup : IStartup
    {
        private readonly string _apiName;
        private readonly string _apiDescription;
        private readonly string _apiVersion;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _apiName = Configuration["ApiName"];
            _apiDescription = Configuration["ApiDescription"];
            _apiVersion = Configuration["ApiVersion"];
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<CatalogoContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<VendasContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentityCore<IdentityUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            //services.AddMvc();

            services.AddHttpContextAccessor();

            services.AddAutoMapper(typeof(DomainToViewModelMappingProfile), typeof(ViewModelToDomainMappingProfile));

            services.AddMediatR(typeof(Startup));

            services.RegisterServices();

            services.AddCors(setup =>
            {
                setup.AddPolicy(AllowWhitelistCorsPolicy.Name, AllowWhitelistCorsPolicy.Get(Configuration["CorsWhitelist"].Split(',')));
            });

            services.AddSwaggerGen(options =>
            {
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Description = "Insira o token JWT desta maneira: Bearer {seu token}",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                };
                options.AddSecurityDefinition("Bearer", jwtSecurityScheme);

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() }
                });

                options.SwaggerDoc(_apiVersion, new OpenApiInfo { Title = _apiName, Version = _apiVersion, Description = _apiDescription });

                /* Due to the need to put a description in each property of the "Request / Dto" classes used in the [FromQuery] of the endpoints, 
                 * the implementation below was necessary, where through forEach the inclusion of all XML files is performed. 
                 * (Microsoft's official documentation only explains how to do it for one XML file) */
                List<string> xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly).ToList();
                xmlFiles.ForEach(xmlFile => options.IncludeXmlComments(xmlFile));
            });

            services.AddAutoMapper(typeof(DomainToViewModelMappingProfile), typeof(ViewModelToDomainMappingProfile));

            services.AddMediatR(typeof(Startup));

            services.RegisterServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // Swagger
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint($"{_apiVersion}/swagger.json", $"{_apiName} {_apiVersion}");
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseDeveloperExceptionPage();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Vitrine}/{action=Index}/{id?}");
        }
    }
}
