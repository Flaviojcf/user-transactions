using UserTransactions.Communication.Dtos.User.Response;
using UserTransactions.Domain.Repositories.User;

namespace UserTransactions.Application.UseCases.User.ListTotal
{
    public class ListTotalQuantityUserUseCase : IListTotalQuantityUserUseCase
    {
        private readonly IUserRepository _userRepository;

        public ListTotalQuantityUserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ResponseListTotalQuantityUserDto> ExecuteAsync()
        {
            var totalQuantity = await _userRepository.ListTotalQuantityAsync();

            return new ResponseListTotalQuantityUserDto
            {
                TotalQuantity = totalQuantity
            };
        }
    }
}
