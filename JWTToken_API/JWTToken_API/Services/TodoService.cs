using AutoMapper;
using JWTToken_API.DatabaseContext;
using JWTToken_API.Interfaces.ReposInterfaces;
using JWTToken_API.Interfaces.ServicesInterfaces;
using JWTToken_API.Model;

namespace JWTToken_API.Services
{
    public class TodoService : ITodoService
    {        
        private readonly IMapper _IMapper;
        private readonly ITodoRepository _ITodoRepository;
        public TodoService(IMapper iMapper, ITodoRepository iTodoRepository)
        {           
            _IMapper = iMapper;
            _ITodoRepository = iTodoRepository;
        }


        /// <summary>
        /// Get list of all active/inactive Todo task without any filter.
        /// </summary>
        /// <returns></returns>
        public async Task<List<TodoModel>> GetAll()
        {
            return await _ITodoRepository.GetAll();
        }

        /// <summary>
        /// Get todo task by todo id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Todo task details</returns>      
        public async Task<TodoModel> GetById(int Id)
        {
            return await _ITodoRepository.GetById(Id);

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
                if ((await _ITodoRepository.GetByTaskName(todoinput.TaskName)).Count > 0)
                {
                    response.Status = false;
                    response.Message = "Task already already exists.";
                    return response;
                }
                var todo = _IMapper.Map<TodoModel>(todoinput);

                var result = await _ITodoRepository.Insert(todo);
                if (result > 0)
                {
                    response.Status = true;
                    response.Message = "Todo task successfully saved.";
                }
                else
                {
                    response.Status = false;
                    response.Message = "Todo task not saved. Please try again with proper details.";
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
        /// Update todo task into database whose id exists in the database.
        /// </summary>
        /// <param name="todo"></param>
        /// <returns></returns>
        public async Task<ServiceResponse> Update(int Id, TodoInputModel todoinput)
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
                if ((await _ITodoRepository.GetByTaskName(todoinput.TaskName)).Count > 0)
                {
                    response.Status = false;
                    response.Message = "Task already already exists.";
                    return response;
                }
                var todo = _IMapper.Map<TodoModel>(todoinput);
                todo.Id = Id;              
                int result = await _ITodoRepository.Update(todo);
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
        public async Task<ServiceResponse> Delete(int Id)
        {
            ServiceResponse response = new ServiceResponse();
            try
            {
                var todo = await _ITodoRepository.GetById(Id);
                if (todo == null)
                {
                    response.Status = false;
                    response.Message = "Todo task does not exists. Please enter proper id.";
                    return response;
                }                
                int result = await _ITodoRepository.Delete(todo);
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
