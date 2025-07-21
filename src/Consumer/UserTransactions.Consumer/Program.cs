using SendGrid.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using UserTransactions.Consumer.Configuration;
using UserTransactions.Consumer.Services.EmailService;
using UserTransactions.Consumer.Services.Messaging;
namespace UserTransactions.Consumer
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        protected Program() { }

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddSendGrid(o =>
            {
                o.ApiKey = builder.Configuration.GetValue<string>("SendGrid:ApiKey");
            });
            builder.Services.AddSingleton<IEmailService, SendGridService>();
            builder.Services.AddHostedService<KafkaMessageConsumer>();
            builder.Services.Configure<KafkaOptions>(builder.Configuration.GetSection(KafkaOptions.SectionName));
            builder.Services.AddSingleton<KafkaConsumerFactory>();

            var app = builder.Build();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
