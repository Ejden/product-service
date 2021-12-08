using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using product_service.Domain;
using product_service.Infrastructure.Db;
using product_service.Infrastructure.Db.Config;
using product_service.Infrastructure.Db.Models;
using product_service.Infrastructure.ExceptionHandlers;

namespace product_service
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ProductServiceDatabaseProperties>(Configuration.GetSection("ProductServiceDatabase"));
            services.AddScoped<ModelMapper>();
            services.AddScoped<IProductProvider, DatabaseProductProvider>();
            services.AddScoped<ProductValidator>();
            services.AddScoped<ProductFactory>();
            services.AddScoped<ProductService>();
            // var productProvider = new InMemoryProductProvider();
            // var productValidator = new ProductValidator();
            // var productFactory = new ProductFactory();
            // var productService = new ProductService(productProvider, productValidator, productFactory);
            // services.AddSingleton(productService);
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "product_service", Version = "v1"});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "product_service v1"));
            }
            
            app.ConfigureCustomExceptionMiddleware();

            app.UseHttpsRedirection();
            
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}