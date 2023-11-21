using JWTToken_API.Model;
using JWTToken_API.Services;

namespace JWTToken_API.Interfaces.ReposInterfaces
{
    public interface ITodoRepository
    {
        Task<List<TodoModel>> GetAll();
        Task<TodoModel> GetById(int Id);
        Task<int> Insert(TodoModel todo);
        Task<int> Update(TodoModel todo);
        Task<int> Delete(TodoModel todo);
        Task<List<TodoModel>> GetByTaskName(string taskname);
    }
}
