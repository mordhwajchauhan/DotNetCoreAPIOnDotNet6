using JWTToken_API.DatabaseContext;
using JWTToken_API.Enums;
using JWTToken_API.Interfaces;
using JWTToken_API.Interfaces.ReposInterfaces;
using JWTToken_API.Model;
using Microsoft.EntityFrameworkCore;

namespace JWTToken_API.Services
{
    public class UserService : IUserService
    {        
        private readonly IUserRepository _IUserRepository;
        public UserService( IUserRepository iUserRepository)
        {            
            _IUserRepository = iUserRepository;
        }
        /// <summary>
        /// To authanticate user with valid email id and password.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<UserModel> Authenticate(string email, string password)
        {
            return await _IUserRepository.Authenticate(email, password);
        }
        /// <summary>
        /// Get list of all active/inactive users without any filter.
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserModel>> GetAllUsers()
        {
            return await _IUserRepository.GetAll();
        }

        /// <summary>
        /// Insert users into database whose emailid does not exist in the database.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<ServiceResponse> Register(UserModel user)
        {
            ServiceResponse response = new ServiceResponse();
            try
            {
                if (user == null)
                {
                    response.Status = false;
                    response.Message = "Please enter all required fields.";
                    return response;
                }

                if ((await _IUserRepository.GetByEmail(user.Email)).Count > 0)
                {
                    response.Status = false;
                    response.Message = "Email Id already exists.";
                    return response;
                }

                if (user.Role != UserRoles.Admin && user.Role != UserRoles.User)
                {
                    response.Status = false;
                    response.Message = "Please enter correct role. Admin or User";
                    return response;
                }
                var result = await _IUserRepository.Insert(user);
                if (result > 0)
                {
                    response.Status = true;
                    response.Message = "User successfully registered.";
                }
                else
                {
                    response.Status = false;
                    response.Message = "User not registered. Please try again with proper details.";
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
