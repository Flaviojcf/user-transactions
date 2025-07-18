using Bogus;
using Bogus.Extensions.Brazil;
using UserTransactions.Domain.Entities;
using UserTransactions.Domain.Enum;

namespace UserTransactions.Tests.Shared.Builders.Entities
{
    public static class UserEntityBuilder
    {
        public static User Build()
        {
            return new Faker<User>().CustomInstantiator(faker => new User(faker.Person.FullName, faker.Person.Email, faker.Person.Cpf(), faker.Random.String(), faker.PickRandom<UserType>())).Generate();
        }

        public static User BuildUser()
        {
            return new Faker<User>().CustomInstantiator(faker => new User(faker.Person.FullName, faker.Person.Email, faker.Person.Cpf(), faker.Random.String(), UserType.User)).Generate();
        }

        public static User BuildMerchant()
        {
            return new Faker<User>().CustomInstantiator(faker => new User(faker.Person.FullName, faker.Person.Email, faker.Person.Cpf(), faker.Random.String(), UserType.Merchant)).Generate();
        }
    }
}
