using FluentAssertions;
using UserTransactions.Application.UseCases.Wallet.Create;
using UserTransactions.Exception;
using UserTransactions.Tests.Shared.Builders.Dtos.Request.Wallet;

namespace UserTransactions.Tests.Application.Wallet.Validators
{
    public class CreateWalletValidatorTest
    {
        [Fact]
        public void Given_ValidRequest_When_ValidateIsCalled_Then_ResultShouldBeTrue()
        {
            // Arrange
            var request = RequestCreateWalletDtoBuilder.Build();

            // Act
            var validator = new CreateWalletValidator();
            var result = validator.Validate(request);

            // Assert
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Given_EmptyUserIdRequest_When_ValidateIsCalled_Then_ResultShouldThrowsUserIdRequiredException()
        {
            // Arrange
            var request = RequestCreateWalletDtoBuilder.Build();
            request.UserId = Guid.Empty;

            // Act
            var validator = new CreateWalletValidator();
            var result = validator.Validate(request);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(error => error.ErrorMessage.Equals(ResourceMessagesException.UserIdRequired));
        }
    }
}
