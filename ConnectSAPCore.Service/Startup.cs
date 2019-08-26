using Autofac;
using Autofac.Extensions.DependencyInjection;
using ConnectSAPCore.Service.Infrastructure.AutofacModule;
using ConnectSAPCore.Service.Infrastructure.NativeInjector;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO.Compression;

namespace ConnectSAPCore.Service
{
    public class Startup
    {
        public IConfiguration _configuration { get; }
        public Startup(IConfiguration configuration) => _configuration = configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddCors(); services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Fastest;
            });
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Servicios SAP API", Version = "V1" });
            });

            services.AddSingleton(_configuration);
            services.AddOptions();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_0);

            RegisterServices(services);

            var container = new ContainerBuilder();
            container.Populate(services);

            container.RegisterModule(new ServicesModule(_configuration["ConnectionStringSAP"], _configuration["License"]));

            return new AutofacServiceProvider(container.Build());
        }

        private static void RegisterServices(IServiceCollection services) => BootStrapper.RegisterServices(services);

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseCors(builder => builder
                 .AllowAnyOrigin()
                 .AllowAnyMethod()
                 .AllowAnyHeader());
            app.UseResponseCompression();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Servicios SAP API");
            });
            app.UseMvcWithDefaultRoute();
        }
    }
}
