using FluentAssertions;
using UserTransactions.Application.UseCases.User.Create;
using UserTransactions.Domain.Enum;
using UserTransactions.Exception;
using UserTransactions.Tests.Shared.Builders.Dtos.Request;

namespace UserTransactions.Tests.Application.User.Validators
{
    public class CreateUserValidatorTest
    {
        [Fact]
        public void Given_ValidRequest_When_ValidateIsCalled_Then_ResultShouldBeTrue()
        {
            // Arrange
            var request = RequestCreateUserDtoBuilder.Build();

            // Act
            var validator = new CreateUserValidator();
            var result = validator.Validate(request);

            // Assert
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Given_EmptyFullNameRequest_When_ValidateIsCalled_Then_ResultShouldThrowsFullNameRequiredException()
        {
            // Arrange
            var request = RequestCreateUserDtoBuilder.Build();
            request.FullName = string.Empty;

            // Act
            var validator = new CreateUserValidator();
            var result = validator.Validate(request);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(error => error.ErrorMessage.Equals(ResourceMessagesException.FullNameRequired));
        }

        [Fact]
        public void Given_EmptyEmailRequest_When_ValidateIsCalled_Then_ResultShouldThrowsEmailRequiredException()
        {
            // Arrange
            var request = RequestCreateUserDtoBuilder.Build();
            request.Email = string.Empty;

            // Act
            var validator = new CreateUserValidator();
            var result = validator.Validate(request);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(error => error.ErrorMessage.Equals(ResourceMessagesException.EmailRequired));
        }

        [Fact]
        public void Given_EmptyCpfRequest_When_ValidateIsCalled_Then_ResultShouldThrowsCpfRequiredException()
        {
            // Arrange
            var request = RequestCreateUserDtoBuilder.Build();
            request.CPF = string.Empty;

            // Act
            var validator = new CreateUserValidator();
            var result = validator.Validate(request);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(error => error.ErrorMessage.Equals(ResourceMessagesException.CpfRequired));
        }

        [Fact]
        public void Given_InvalidUserTypeRequest_When_ValidateIsCalled_Then_ResultShouldThrowsInvalidUserTypeException()
        {
            // Arrange
            var request = RequestCreateUserDtoBuilder.Build();
            request.UserType = (UserType)3;

            // Act
            var validator = new CreateUserValidator();
            var result = validator.Validate(request);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(error => error.ErrorMessage.Equals(ResourceMessagesException.InvalidUserType));
        }

        [Fact]
        public void Given_InvalidEmailRequest_When_ValidateIsCalled_Then_ResultShouldThrowsInvalidEmailFormatException()
        {
            // Arrange
            var request = RequestCreateUserDtoBuilder.Build();
            request.Email = "invalidEmailFormat";

            // Act
            var validator = new CreateUserValidator();
            var result = validator.Validate(request);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(error => error.ErrorMessage.Equals(ResourceMessagesException.InvalidEmailFormat));
        }

        [Theory]
        [InlineData("31314")]
        [InlineData("abcde")]
        [InlineData("1231414151131313213")]
        [InlineData("213121")]
        [InlineData("ddddddddddd")]
        public void Given_InvalidCpfRequest_When_ValidateIsCalled_Then_ResultShouldThrowsInvalidCpfFormatException(string cpf)
        {
            // Arrange
            var request = RequestCreateUserDtoBuilder.Build();
            request.CPF = cpf;

            // Act
            var validator = new CreateUserValidator();
            var result = validator.Validate(request);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(error => error.ErrorMessage.Equals(ResourceMessagesException.InvalidCpfFormat));
        }

        [Theory]
        [InlineData("31314")]
        [InlineData("abcde")]
        [InlineData("12345")]
        [InlineData("add")]
        public void Given_InvalidPasswordRequest_When_ValidateIsCalled_Then_ResultShouldNotThrowInvalidPasswordFormatException(string password)
        {
            // Arrange
            var request = RequestCreateUserDtoBuilder.Build();
            request.Password = password;

            // Act
            var validator = new CreateUserValidator();
            var result = validator.Validate(request);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(error => error.ErrorMessage.Equals(ResourceMessagesException.InvalidPassword));

        }
    }
}
