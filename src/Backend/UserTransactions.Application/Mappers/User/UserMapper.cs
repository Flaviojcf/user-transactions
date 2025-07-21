using UserTransactions.Communication.Dtos.User.Request;
using UserTransactions.Communication.Dtos.User.Response;
using UserEntity = UserTransactions.Domain.Entities.User;

namespace UserTransactions.Application.Mappers.User
{
    public static class UserMapper
    {
        public static UserEntity MapToUser(this RequestCreateUserDto request)
        {
            return new UserEntity(request.FullName, request.Email, request.CPF, request.Password, request.UserType);
        }

        public static ResponseCreateUserDto MapFromUser(this UserEntity user)
        {
            return new ResponseCreateUserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
            };
        }

        public static IList<ResponseListAllUsersDto> MapListAllFromUser(this IList<UserEntity> users)
        {
            return users.Select(user => new ResponseListAllUsersDto
            {
                FullName = user.FullName,
                Email = user.Email,
                CPF = user.CPF,
                UserType = user.UserType
            }).ToList();
        }
    }
}
