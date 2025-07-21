using UserTransactions.Application.Mappers.User;
using UserTransactions.Communication.Dtos.User.Response;
using UserTransactions.Domain.Repositories.User;

namespace UserTransactions.Application.UseCases.User.ListAll
{
    public class ListAllUsersUseCase : IListAllUsersUseCase
    {
        private readonly IUserRepository _userRepository;

        public ListAllUsersUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IList<ResponseListAllUsersDto>> ExecuteAsync()
        {
            var users = await _userRepository.ListAllAsync();

            var responseListAllUsersDto = users.MapListAllFromUser();

            return responseListAllUsersDto;
        }
    }
}
