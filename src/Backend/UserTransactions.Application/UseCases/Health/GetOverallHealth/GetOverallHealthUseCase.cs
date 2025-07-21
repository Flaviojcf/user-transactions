using System.Diagnostics.CodeAnalysis;
using UserTransactions.Communication.Dtos.Health.Response;
using UserTransactions.Domain.Services.Health;

namespace UserTransactions.Application.UseCases.Health.GetOverallHealth
{
    [ExcludeFromCodeCoverage]
    public class GetOverallHealthUseCase : IGetOverallHealthUseCase
    {
        private readonly IHealthCheckService _healthCheckService;

        public GetOverallHealthUseCase(IHealthCheckService healthCheckService)
        {
            _healthCheckService = healthCheckService;
        }

        public async Task<ResponseOverallHealthDto> ExecuteAsync()
        {
            var databaseHealth = _healthCheckService.CheckDatabaseAsync();
            var kafkaHealth = _healthCheckService.CheckKafkaAsync();
            var kafkaUIHealth = _healthCheckService.CheckKafkaUIAsync();

            var results = await Task.WhenAll(databaseHealth, kafkaHealth, kafkaUIHealth);

            return new ResponseOverallHealthDto
            {
                Services = results.Select(result => new ResponseHealthServiceDto
                {
                    Service = result.Service,
                    Status = result.Status
                }).ToList()
            };
        }
    }
}