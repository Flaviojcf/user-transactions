using FluentAssertions;
using UserTransactions.Application.UseCases.Transaction.Create;
using UserTransactions.Exception;
using UserTransactions.Tests.Shared.Builders.Dtos.Request.Transactions;

namespace UserTransactions.Tests.Application.Transactions.Validators
{
    public class CreateTransactionValidatorTest
    {
        [Fact]
        public void Given_ValidRequest_When_ValidateIsCalled_Then_ResultShouldBeTrue()
        {
            // Arrange
            var request = RequestCreateTransactionDtoBuilder.Build();

            // Act
            var validator = new CreateTransactionValidator();
            var result = validator.Validate(request);

            // Assert
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Given_ZeroAmountRequest_When_ValidateIsCalled_Then_ResultShouldThrowsTransactionAmountMustBeGreaterThanZeroException()
        {
            // Arrange
            var request = RequestCreateTransactionDtoBuilder.Build();
            request.Amount = 0;

            // Act
            var validator = new CreateTransactionValidator();
            var result = validator.Validate(request);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(error => error.ErrorMessage.Equals(ResourceMessagesException.TransactionAmountMustBeGreaterThanZero));
        }

        [Fact]
        public void Given_EmptySenderIdRequest_When_ValidateIsCalled_Then_ResultShouldThrowsSenderIdRequiredException()
        {
            // Arrange
            var request = RequestCreateTransactionDtoBuilder.Build();
            request.SenderId = Guid.Empty;

            // Act
            var validator = new CreateTransactionValidator();
            var result = validator.Validate(request);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(error => error.ErrorMessage.Equals(ResourceMessagesException.SenderIdRequired));
        }

        [Fact]
        public void Given_EmptyReceiverIdRequest_When_ValidateIsCalled_Then_ResultShouldThrowsReceiverIdRequiredException()
        {
            // Arrange
            var request = RequestCreateTransactionDtoBuilder.Build();
            request.ReceiverId = Guid.Empty;

            // Act
            var validator = new CreateTransactionValidator();
            var result = validator.Validate(request);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(error => error.ErrorMessage.Equals(ResourceMessagesException.ReceiverIdRequired));
        }
    }
}
