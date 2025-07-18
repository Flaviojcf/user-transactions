using Microsoft.OpenApi.Models;
using System.Diagnostics.CodeAnalysis;
using UserTransactions.API.DI;
using UserTransactions.Application.DI;
using UserTransactions.Infrastructure.DI;

namespace UserTransactions.API
{
    public class Program
    {
        [ExcludeFromCodeCoverage]
        protected Program() { }

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "User Transactions - V1", Version = "v1.0" });
                c.SwaggerDoc("v2", new OpenApiInfo { Title = "User Transactions - V2", Version = "v2.0" });
            });

            builder.Services.AddWebApi(builder.Configuration);
            builder.Services.AddApplication();
            builder.Services.AddInfrastructure();

            var app = builder.Build();

            app.MapHealthChecks("/health");

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}