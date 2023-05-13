namespace AccountManagement.Application.Contracts.AccountContracts
{
    public class AccountSearchModel
    {
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string Mobile { get; set; }
        public long RoleId { get; set; }
    }
}