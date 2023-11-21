using AutoMapper;
using JWTToken_API.DatabaseContext;
using JWTToken_API.Interfaces.ReposInterfaces;
using JWTToken_API.Model;
using Microsoft.EntityFrameworkCore;

namespace JWTToken_API.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly MyDBContext _myDBContext;

        public TodoRepository(MyDBContext myDBContext)
        {
            _myDBContext = myDBContext;           
        }

        public async Task<int> Delete(TodoModel todo)
        {
            _myDBContext.Todos.Remove(todo);
            return await _myDBContext.SaveChangesAsync();
        }

        public async Task<List<TodoModel>> GetAll()
        {
            return await _myDBContext.Todos.ToListAsync();
        }

        /// <summary>
        /// Get todo task by todo id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Todo task details</returns>
        public async Task<TodoModel> GetById(int Id)
        {
            return await _myDBContext.Todos
                .FirstOrDefaultAsync(x => x.Id == Id);
        }

        /// <summary>
        /// Get list of tasks by taskname
        /// </summary>
        /// <param name="taskname"></param>
        /// <returns>todo task list </returns>
        public async Task<List<TodoModel>> GetByTaskName(string taskname)
        {
            return await _myDBContext.Todos
                 .Where(x => x.TaskName == taskname).ToListAsync();
        }

        /// <summary>
        /// Insert Todo task into database whose taskname does not exist in the database.
        /// </summary>
        /// <param name="todo"></param>
        /// <returns>number of rows affected</returns>
        public async Task<int> Insert(TodoModel todo)
        {
            int todoid = await _myDBContext.Todos.OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefaultAsync();
            todo.Id = todoid + 1;
            await _myDBContext.Todos.AddAsync(todo);
            return await _myDBContext.SaveChangesAsync();
        }

        /// <summary>
        /// Update todo task into database whose id exists in the database.
        /// </summary>
        /// <param name="todo"></param>
        /// <returns>number of rows affected</returns>
        public async Task<int> Update(TodoModel todo)
        {
            _myDBContext.Entry(todo).State = EntityState.Modified;
            return await _myDBContext.SaveChangesAsync();
        }
    }
}
