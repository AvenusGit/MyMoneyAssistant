using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.OpenApi.Models;
using MyMoneyAssistant.Database;
using Newtonsoft.Json.Converters;
using System.Reflection;

namespace MyMoneyAssistant
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services
                .AddControllers()
                .AddNewtonsoftJson(options => { options.SerializerSettings.Converters.Add(new StringEnumConverter());
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore; });  
            builder.Services.AddEndpointsApiExplorer();

            // Генератор документации
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "MyMoneyAssistant API",
                    Description = "Тестовый проект приложения управления финансами",
                });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            // контекст БД
            builder.Services.AddScoped<ApplicationDatabaseContext>();

            var app = builder.Build();

            // тестовое приложение использует сваггер не только в дебаге
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
