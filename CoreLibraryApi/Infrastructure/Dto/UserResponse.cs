namespace CoreLibraryApi.Infrastructure.Dto
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public RoleEnum Role { get; set; }
        public string Token { get; set; }
    }
}
