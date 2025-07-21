using UserTransactions.Communication.Dtos.Health.Response;

namespace UserTransactions.Application.UseCases.Health.GetOverallHealth
{
    public interface IGetOverallHealthUseCase
    {
        Task<ResponseOverallHealthDto> ExecuteAsync();
    }
}