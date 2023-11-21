using JWTToken_API.Model;
using JWTToken_API.Services;

namespace JWTToken_API.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResponse> Register(UserModel user);
        Task<List<UserModel>> GetAllUsers();

        Task<UserModel> Authenticate(string email, string password);        
    }
}
