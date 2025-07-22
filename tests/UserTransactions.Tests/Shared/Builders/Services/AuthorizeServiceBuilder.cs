using Moq;
using UserTransactions.Domain.Services.Authorize;
using UserTransactions.Exception;
using UserTransactions.Exception.Exceptions;

namespace UserTransactions.Tests.Shared.Builders.Services
{
    public static class AuthorizeServiceBuilder
    {
        private static readonly Mock<IAuthorizeService> _mock = new Mock<IAuthorizeService>();

        public static IAuthorizeService Build() => _mock.Object;

        public static void SetupValidateAuthorizeService()
        {
            _mock.Setup(x => x.ValidateAuthorizeService()).Returns(Task.CompletedTask);
        }

        public static void SetupValidateAuthorizeServiceThrowsException()
        {
            _mock.Setup(x => x.ValidateAuthorizeService()).ThrowsAsync(new ErrorOnValidationException([ResourceMessagesException.TransactionNotAuthorized]));
        }
    }
}
