namespace UserTransactions.Domain.Services.Health
{
    public interface IHealthCheckService
    {
        Task<HealthCheckResult> CheckDatabaseAsync();
        Task<HealthCheckResult> CheckKafkaAsync();
        Task<HealthCheckResult> CheckKafkaUIAsync();
    }

    public class HealthCheckResult
    {
        public string Service { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}