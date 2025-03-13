using VeriFacil.Application.AppService;
using VeriFacil.Application.Interface;

namespace VeriFacil
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // Map controllers
            });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add services to the container.
            services.AddControllers();

            // Add Swagger for API documentation
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            // Registering the application services
            services.AddScoped<INumeroCelularAppService, NumeroCelularAppService>();
            services.AddScoped<ICnpjAppService, CnpjAppService>();
            services.AddScoped<ICpfAppService, CpfAppService>();
            services.AddScoped<IEmailAppService, EmailAppService>();
        }
    }
}