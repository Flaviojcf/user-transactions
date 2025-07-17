using Bogus;
using Bogus.Extensions.Brazil;
using UserTransactions.Communication.Dtos.User.Request;
using UserTransactions.Domain.Enum;

namespace UserTransactions.Tests.Shared.Builders.Dtos.Request
{
    public static class RequestCreateUserDtoBuilder
    {
        public static RequestCreateUserDto Build()
        {
            return new Faker<RequestCreateUserDto>()
                 .RuleFor(x => x.FullName, f => f.Name.FullName())
                 .RuleFor(x => x.Email, f => f.Internet.Email())
                 .RuleFor(x => x.CPF, f => f.Person.Cpf())
                 .RuleFor(x => x.Password, f => f.Internet.Password(6))
                 .RuleFor(x => x.UserType, f => f.PickRandom<UserType>())
                 .Generate();
        }
    }
}
