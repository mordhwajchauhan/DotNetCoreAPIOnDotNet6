using JWTToken_API.Model;
using JWTToken_API.Services;

namespace JWTToken_API.Interfaces.ServicesInterfaces
{
    public interface ITodoService
    {
        Task<List<TodoModel>> GetAll();
        Task<TodoModel> GetById(int Id);
        Task<ServiceResponse> Insert(TodoInputModel todoinput);
        Task<ServiceResponse> Update(int Id, TodoInputModel todoinput);
        Task<ServiceResponse> Delete(int Id);
    }
}
