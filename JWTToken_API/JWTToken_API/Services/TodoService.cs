using AutoMapper;
using JWTToken_API.DatabaseContext;
using JWTToken_API.Interfaces;
using JWTToken_API.Model;
using Microsoft.EntityFrameworkCore;

namespace JWTToken_API.Services
{
    public class TodoService : ITodoService
    {
        private readonly MyDBContext _myDBContext;
        private readonly IMapper _IMapper;
        public TodoService(MyDBContext myDBContext, IMapper iMapper)
        {
            _myDBContext = myDBContext;
            _IMapper = iMapper;
        }


        /// <summary>
        /// Get list of all active/inactive Todo task without any filter.
        /// </summary>
        /// <returns></returns>
        public async Task<List<TodoModel>> GetAll()
        {
            return await _myDBContext.Todos.ToListAsync();
        }

        /// <summary>
        /// Get todo task by todo id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<TodoModel> GetById(int Id)
        {
            return await _myDBContext.Todos
                .FirstOrDefaultAsync(x => x.Id == Id);

        }

        /// <summary>
        /// Insert Todo task into database whose taskname does not exist in the database.
        /// </summary>
        /// <param name="todo"></param>
        /// <returns></returns>
        public async Task<ServiceResponse> Insert(TodoInputModel todoinput)
        {
            ServiceResponse response = new ServiceResponse();
            try
            {
                if (todoinput == null)
                {
                    response.Status = false;
                    response.Message = "Please enter all required fields.";
                    return response;
                }

                if ((await _myDBContext.Todos.Where(x => x.TaskName == todoinput.TaskName).ToListAsync()).Count > 0)
                {
                    response.Status = false;
                    response.Message = "Task already already exists.";
                    return response;
                }

                var todo = _IMapper.Map<TodoModel>(todoinput);

                int todoid = await _myDBContext.Todos.OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefaultAsync();
                todo.Id = todoid + 1;
                await _myDBContext.Todos.AddAsync(todo);
                await _myDBContext.SaveChangesAsync();

                response.Status = true;
                response.Message = "Todo task successfully saved.";

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
            }

            return response;
        }

        /// <summary>
        /// Update todo task into database whose id exists in the database.
        /// </summary>
        /// <param name="todo"></param>
        /// <returns></returns>
        public async Task<ServiceResponse> Update(int Id,TodoInputModel todoinput)
        {
            ServiceResponse response = new ServiceResponse();
            try
            {
                if (todoinput == null)
                {
                    response.Status = false;
                    response.Message = "Please enter all required fields.";
                    return response;
                }

                if ((await _myDBContext.Todos.Where(x => x.TaskName == todoinput.TaskName).ToListAsync()).Count > 0)
                {
                    response.Status = false;
                    response.Message = "Task already already exists.";
                    return response;
                }

                var todo = _IMapper.Map<TodoModel>(todoinput);
                todo.Id = Id;
                _myDBContext.Entry(todo).State = EntityState.Modified;
                int result = await _myDBContext.SaveChangesAsync();
                if (result > 0)
                {
                    response.Status = true;
                    response.Message = "Todo task successfully updated.";
                }
                else
                {
                    response.Status = false;
                    response.Message = "Todo task not updated. Please try again with proper details.";
                }

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
            }

            return response;
        }

        /// <summary>
        /// Delete todo task by todo id.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ServiceResponse> Delete(int Id)
        {
            ServiceResponse response = new ServiceResponse();
            try
            {
                var todo = await _myDBContext.Todos.FirstOrDefaultAsync(x => x.Id == Id);
                if (todo == null)
                {
                    response.Status = false;
                    response.Message = "Todo task does not exists. Please enter proper id.";
                    return response;
                }
                _myDBContext.Todos.Remove(todo);
                int result = await _myDBContext.SaveChangesAsync();
                if (result > 0)
                {
                    response.Status = true;
                    response.Message = "Todo task successfully deleted.";
                }
                else
                {
                    response.Status = false;
                    response.Message = "Todo task not deleted. Please try again with proper id.";
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
            }

            return response;
        }
        
    }

}
