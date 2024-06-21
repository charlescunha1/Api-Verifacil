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
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add services to the container.

            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddScoped<INumeroCelularAppService, NumeroCelularAppService>();
            services.AddScoped<ICnpjAppService, CnpjAppService>();
            services.AddScoped<ICpfAppService, CpfAppService>();
            services.AddScoped<IEmailAppService, EmailAppService>();
        }
    }
}
