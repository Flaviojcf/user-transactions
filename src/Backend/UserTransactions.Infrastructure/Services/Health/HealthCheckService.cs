using Confluent.Kafka;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Diagnostics.CodeAnalysis;
using UserTransactions.Domain.Services.Health;

namespace UserTransactions.Infrastructure.Services.Health
{
    [ExcludeFromCodeCoverage]
    public class HealthCheckService : IHealthCheckService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private const int timeout = 3;
        private const int kafkaTimeout = 3000;

        public HealthCheckService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<HealthCheckResult> CheckDatabaseAsync()
        {
            try
            {
                var connectionString = _configuration["HealthCheck:SqlServerUrl"];

                var builder = new SqlConnectionStringBuilder(connectionString)
                {
                    ConnectTimeout = timeout
                };

                using var connection = new SqlConnection(builder.ConnectionString);

                using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));

                await connection.OpenAsync(cts.Token);

                var command = new SqlCommand("SELECT 1", connection)
                {
                    CommandTimeout = timeout
                };

                var result = await command.ExecuteScalarAsync(cts.Token);

                bool isHealthy = result != null && Convert.ToInt32(result) == 1;

                return new HealthCheckResult
                {
                    Service = "sqlServer",
                    Status = "healthy"
                };
            }
            catch (OperationCanceledException)
            {
                return new HealthCheckResult
                {
                    Service = "sqlServer",
                    Status = "unhealthy"
                };
            }
            catch
            {
                return new HealthCheckResult
                {
                    Service = "sqlServer",
                    Status = "unhealthy",
                };
            }
        }

        public async Task<HealthCheckResult> CheckKafkaAsync()
        {
            var config = new ProducerConfig
            {
                BootstrapServers = _configuration["HealthCheck:KafkaUrl"],
                MessageTimeoutMs = kafkaTimeout,
                RequestTimeoutMs = kafkaTimeout,
                SocketTimeoutMs = kafkaTimeout,
                ApiVersionRequestTimeoutMs = kafkaTimeout
            };

            var testTopic = _configuration["HealthCheck:KafkaHealthTopic"];
            var testMessage = new Message<string, string> { Key = "health", Value = "check" };

            try
            {
                using var producer = new ProducerBuilder<string, string>(config).Build();

                using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(timeout));

                var deliveryResult = await producer.ProduceAsync(testTopic, testMessage, cts.Token);

                bool isHealthy = deliveryResult.Status == PersistenceStatus.Persisted ||
                                 deliveryResult.Status == PersistenceStatus.NotPersisted;

                return new HealthCheckResult
                {
                    Service = "kafka",
                    Status = isHealthy ? "healthy" : "unhealthy"
                };
            }
            catch (OperationCanceledException)
            {
                return new HealthCheckResult
                {
                    Service = "kafka",
                    Status = "unhealthy"
                };
            }
            catch
            {
                return new HealthCheckResult
                {
                    Service = "kafka",
                    Status = "unhealthy"
                };
            }
        }

        public async Task<HealthCheckResult> CheckKafkaUIAsync()
        {
            try
            {
                using var httpClient = _httpClientFactory.CreateClient();
                var kafkaUIUrl = _configuration["HealthCheck:KafkaUIUrl"];
                var response = await httpClient.GetAsync(kafkaUIUrl);
                var content = await response.Content.ReadAsStringAsync();

                return new HealthCheckResult
                {
                    Service = "kafka-ui",
                    Status = response.IsSuccessStatusCode ? "healthy" : "unhealthy",
                };
            }
            catch
            {
                return new HealthCheckResult
                {
                    Service = "kafka-ui",
                    Status = "unhealthy",
                };
            }
        }
    }
}