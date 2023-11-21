using JWTToken_API.Enums;
using JWTToken_API.Interfaces;
using JWTToken_API.Model;
using JWTToken_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace JWTToken_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize(Roles = UserRoles.Admin + "," + UserRoles.User)]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        /// <summary>
        /// Get list of all Todo tasks without any filter
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _todoService.GetAll());
        }

       /// <summary>
       /// Get a todo task by id.
       /// </summary>
       /// <param name="id">todo task id</param>
       /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _todoService.GetById(id));
        }

        /// <summary>
        /// Insert any new Todo task whose taskname doest not exists in the database.
        /// </summary>
        /// <param name="todo">todo input model object</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TodoInputModel todo)
        {
            ServiceResponse response = await _todoService.Insert(todo);
            if (response.Status == false)
                return BadRequest(response);

            return Ok(response);
        }

        /// <summary>
        /// Update any todo task by providing task id.
        /// </summary>
        /// <param name="id">todo task id</param>
        /// <param name="todo">todo input model</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] TodoInputModel todo)
        {
            ServiceResponse response = await _todoService.Update(id, todo);
            if (response.Status == false)
                return BadRequest(response);

            return Ok(response);
        }

        /// <summary>
        /// Delete any existing todo task.
        /// </summary>
        /// <param name="id">todo task id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            ServiceResponse response = await _todoService.Delete(id);
            if (response.Status == false)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
