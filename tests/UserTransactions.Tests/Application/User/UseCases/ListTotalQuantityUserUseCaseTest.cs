using FluentAssertions;
using UserTransactions.Application.UseCases.User.ListTotal;
using UserTransactions.Domain.Repositories.User;
using UserTransactions.Tests.Shared.Builders.Repositories;

namespace UserTransactions.Tests.Application.User.UseCases
{
    public class ListTotalQuantityUserUseCaseTest
    {
        private readonly IUserRepository _userRepository;
        private readonly IListTotalQuantityUserUseCase _sut;

        public ListTotalQuantityUserUseCaseTest()
        {
            _userRepository = UserRepositoryBuilder.Build();
            _sut = new ListTotalQuantityUserUseCase(_userRepository);
        }

        [Fact]
        public async Task Given_ValidRequest_When_ExecuteAsyncIsCalled_Then_ShouldReturnValidResult()
        {
            // Arrange
            var expectedTotalQuantity = 25;
            UserRepositoryBuilder.SetupListTotalQuantityAsync(expectedTotalQuantity);

            // Act
            var result = await _sut.ExecuteAsync();

            // Assert
            result.Should().NotBeNull();
            result.TotalQuantity.Should().Be(expectedTotalQuantity);
        }

        [Fact]
        public async Task Given_ZeroUsers_When_ExecuteAsyncIsCalled_Then_ShouldReturnZeroQuantity()
        {
            // Arrange
            var expectedTotalQuantity = 0;
            UserRepositoryBuilder.SetupListTotalQuantityAsync(expectedTotalQuantity);

            // Act
            var result = await _sut.ExecuteAsync();

            // Assert
            result.Should().NotBeNull();
            result.TotalQuantity.Should().Be(0);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(50)]
        [InlineData(100)]
        public async Task Given_SpecificTotalQuantity_When_ExecuteAsyncIsCalled_Then_ShouldReturnCorrectQuantity(int totalQuantity)
        {
            // Arrange
            UserRepositoryBuilder.SetupListTotalQuantityAsync(totalQuantity);

            // Act
            var result = await _sut.ExecuteAsync();

            // Assert
            result.Should().NotBeNull();
            result.TotalQuantity.Should().Be(totalQuantity);
        }

        [Fact]
        public async Task Given_LargeNumberOfUsers_When_ExecuteAsyncIsCalled_Then_ShouldReturnCorrectQuantity()
        {
            // Arrange
            var expectedTotalQuantity = 999;
            UserRepositoryBuilder.SetupListTotalQuantityAsync(expectedTotalQuantity);

            // Act
            var result = await _sut.ExecuteAsync();

            // Assert
            result.Should().NotBeNull();
            result.TotalQuantity.Should().Be(expectedTotalQuantity);
        }
    }
}