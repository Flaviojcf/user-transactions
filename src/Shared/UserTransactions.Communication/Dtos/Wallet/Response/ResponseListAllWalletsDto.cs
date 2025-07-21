using UserTransactions.Domain.Enum;

namespace UserTransactions.Communication.Dtos.Wallet.Response
{
    public class ResponseListAllWalletsDto
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public UserType UserType { get; set; }
    }
}
