using UserTransactions.Application.Mappers.User;
using UserTransactions.Communication.Dtos.User.Request;
using UserTransactions.Communication.Dtos.User.Response;
using UserTransactions.Domain.Repositories.User;
using UserTransactions.Exception.Exceptions;
using UserEntity = UserTransactions.Domain.Entities.User;

namespace UserTransactions.Application.UseCases.User.Create
{
    public class CreateUserUseCase : ICreateUserUseCase
    {
        private readonly IUserRepository _userRepository;

        public CreateUserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ResponseCreateUserDto> ExecuteAsync(RequestCreateUserDto requestCreateUserDto)
        {
            var user = requestCreateUserDto.MapToUser();

            await ValidateAsync(user);

            await _userRepository.AddAsync(user);

            var responseCreateUserDto = user.MapFromUser();

            return responseCreateUserDto;
        }

        //Todo: Trocar por validação do fluent validation
        private async Task ValidateAsync(UserEntity user)
        {
            var isEmailAlreadyRegistered = await _userRepository.IsEmailAlreadyRegistered(user.Email);

            if (isEmailAlreadyRegistered) throw new EmailIsNotUniqueException();

            var isCpfAlreadyRegistered = await _userRepository.IsCpfAlreadyRegistered(user.CPF);

            if (isCpfAlreadyRegistered) throw new CpfIsNotUniqueException();
        }
    }
}
