using CoreLibraryApi.Infrastructure.Dto;
using System.Threading.Tasks;

namespace CoreLibraryApi.Infrastructure.Interfaces
{
    public interface IAuthService
    {
        UserResponse Authenticate(AuthenticateRequest model);
        Task<UserResponse> Registration(RegistrationRequest model);
    }
}
