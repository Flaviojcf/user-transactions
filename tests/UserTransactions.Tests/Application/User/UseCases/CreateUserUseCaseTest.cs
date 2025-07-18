using FluentAssertions;
using UserTransactions.Application.UseCases.User.Create;
using UserTransactions.Domain.Repositories.User;
using UserTransactions.Exception.Exceptions;
using UserTransactions.Tests.Shared.Builders.Dtos.Request.User;
using UserTransactions.Tests.Shared.Builders.Repositories;

namespace UserTransactions.Tests.Application.User.UseCases
{
    public class CreateUserUseCaseTest
    {
        private readonly IUserRepository _userRepository;
        private readonly ICreateUserUseCase _sut;

        public CreateUserUseCaseTest()
        {
            _userRepository = UserRepositoryBuilder.Build();
            _sut = new CreateUserUseCase(_userRepository);
        }

        [Fact]
        public async Task Given_ValidRequest_When_ExecuteAsyncIsCalled_Then_ShouldReturnValidResult()
        {
            // Arrange
            var request = RequestCreateUserDtoBuilder.Build();

            // Act
            var result = await _sut.ExecuteAsync(request);

            // Assert
            result.Should().NotBeNull();

            result.Id.Should().NotBeEmpty();
            result.FullName.Should().Be(request.FullName);
            result.Email.Should().Be(request.Email);
        }

        [Fact]
        public async Task Given_ExistingEmail_When_ExecuteAsyncIsCalled_Then_ShouldThrowErrorOnValidationException()
        {
            // Arrange
            var request = RequestCreateUserDtoBuilder.Build();
            UserRepositoryBuilder.SetupIsEmailAlreadyRegistered(request.Email);

            // Act
            Func<Task> act = async () => await _sut.ExecuteAsync(request);

            // Assert
            await act.Should().ThrowAsync<ErrorOnValidationException>();
        }

        [Fact]
        public async Task Given_ExistingCpf_When_ExecuteAsyncIsCalled_Then_ShouldThrowErrorOnValidationException()
        {
            // Arrange
            var request = RequestCreateUserDtoBuilder.Build();
            UserRepositoryBuilder.SetupIsCpfAlreadyRegistered(request.CPF);

            // Act
            Func<Task> act = async () => await _sut.ExecuteAsync(request);

            // Assert
            await act.Should().ThrowAsync<ErrorOnValidationException>();
        }
    }
}
