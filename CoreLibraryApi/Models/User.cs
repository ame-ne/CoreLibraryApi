using CoreLibraryApi.Infrastructure;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CoreLibraryApi.Models
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public RoleEnum Role { get; set; }
        [JsonIgnore]
        public string PasswordHash { get; set; }
        
        public ICollection<Order> Orders { get; set; }
    }
}
