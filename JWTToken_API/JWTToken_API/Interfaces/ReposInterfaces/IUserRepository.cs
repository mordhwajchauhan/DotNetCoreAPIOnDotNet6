using JWTToken_API.Model;
using JWTToken_API.Services;

namespace JWTToken_API.Interfaces.ReposInterfaces
{
    public interface IUserRepository
    {
        Task<int> Insert(UserModel user);
        Task<List<UserModel>> GetAll();
        Task<UserModel> Authenticate(string email, string password);
        Task<List<UserModel>> GetByEmail(string email);
    }
}
