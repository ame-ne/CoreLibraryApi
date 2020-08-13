using System.ComponentModel.DataAnnotations;

namespace CoreLibraryApi.Infrastructure.Dto
{
    public class AuthenticateRequest
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
