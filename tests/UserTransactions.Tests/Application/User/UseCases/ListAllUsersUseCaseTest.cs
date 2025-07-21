using FluentAssertions;
using UserTransactions.Application.UseCases.User.ListAll;
using UserTransactions.Domain.Enum;
using UserTransactions.Domain.Repositories.User;
using UserTransactions.Tests.Shared.Builders.Entities;
using UserTransactions.Tests.Shared.Builders.Repositories;
using UserEntity = UserTransactions.Domain.Entities.User;

namespace UserTransactions.Tests.Application.User.UseCases
{
    public class ListAllUsersUseCaseTest
    {
        private readonly IUserRepository _userRepository;
        private readonly IListAllUsersUseCase _sut;

        public ListAllUsersUseCaseTest()
        {
            _userRepository = UserRepositoryBuilder.Build();
            _sut = new ListAllUsersUseCase(_userRepository);
        }

        [Fact]
        public async Task Given_ValidRequest_When_ExecuteAsyncIsCalled_Then_ShouldReturnValidResult()
        {
            // Arrange
            var users = new List<UserEntity>();
            for (int i = 0; i < 3; i++)
            {
                users.Add(UserEntityBuilder.Build());
            }
            UserRepositoryBuilder.SetupListAllAsync(users);

            // Act
            var result = await _sut.ExecuteAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(3);
            result.Should().AllSatisfy(user =>
            {
                user.FullName.Should().NotBeNullOrEmpty();
                user.Email.Should().NotBeNullOrEmpty();
                user.CPF.Should().NotBeNullOrEmpty();
                user.UserType.Should().BeDefined();
            });
        }

        [Fact]
        public async Task Given_NoUsers_When_ExecuteAsyncIsCalled_Then_ShouldReturnEmptyList()
        {
            // Arrange
            var users = new List<UserEntity>();
            UserRepositoryBuilder.SetupListAllAsync(users);

            // Act
            var result = await _sut.ExecuteAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(10)]
        public async Task Given_SpecificNumberOfUsers_When_ExecuteAsyncIsCalled_Then_ShouldReturnCorrectCount(int userCount)
        {
            // Arrange
            var users = new List<UserEntity>();
            for (int i = 0; i < userCount; i++)
            {
                users.Add(UserEntityBuilder.Build());
            }
            UserRepositoryBuilder.SetupListAllAsync(users);

            // Act
            var result = await _sut.ExecuteAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(userCount);
        }

        [Fact]
        public async Task Given_UsersWithDifferentTypes_When_ExecuteAsyncIsCalled_Then_ShouldReturnAllUsers()
        {
            // Arrange
            var users = new List<UserEntity>
            {
                UserEntityBuilder.BuildUser(),
                UserEntityBuilder.BuildMerchant()
            };
            UserRepositoryBuilder.SetupListAllAsync(users);

            // Act
            var result = await _sut.ExecuteAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.Should().Contain(u => u.UserType == UserType.User);
            result.Should().Contain(u => u.UserType == UserType.Merchant);
        }
    }
}