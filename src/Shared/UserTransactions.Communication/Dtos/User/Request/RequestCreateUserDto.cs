using UserTransactions.Domain.Enum;

namespace UserTransactions.Communication.Dtos.User.Request
{
    public class RequestCreateUserDto
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public UserType UserType { get; set; }
    }
}
