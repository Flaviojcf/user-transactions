using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Diagnostics.CodeAnalysis;
using UserTransactions.API.DI;
using UserTransactions.Application.DI;
using UserTransactions.Infrastructure.DI;
using UserTransactions.Infrastructure.Persistance;

namespace UserTransactions.API
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
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

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<UserTransactionsDbContext>();
                dbContext.Database.Migrate();
            }

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