using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CoreLibraryApi.Infrastructure
{
    public class AuthOptions
    {
        public const string ISSUER = "CoreLibraryApi"; // издатель токена
        public const string AUDIENCE = "ClientApp"; // потребитель токена
        public const int LIFETIME = 30; // время жизни токена - 30 минут
        public static SymmetricSecurityKey GetSymmetricSecurityKey(string key)
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
        }
    }
}
