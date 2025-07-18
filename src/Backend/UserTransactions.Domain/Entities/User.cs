using System.Diagnostics.CodeAnalysis;
using UserTransactions.Domain.Enum;

namespace UserTransactions.Domain.Entities
{
    public sealed class User : BaseEntity
    {
        [ExcludeFromCodeCoverage]
        private User() { }

        public User(string fullName, string email, string cpf, string password, UserType userType)
        {
            FullName = fullName;
            Email = email;
            CPF = cpf;
            Password = password;
            UserType = userType;
        }

        public string FullName { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string CPF { get; private set; } = string.Empty;
        public string Password { get; private set; } = string.Empty;
        public UserType UserType { get; private set; }
    }
}
