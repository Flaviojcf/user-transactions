using UserTransactions.Domain.Enum;

namespace UserTransactions.Communication.Dtos.User.Response
{
    public class ResponseListAllUsersDto
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public UserType UserType { get; set; }
    }
}
