using JWTToken_API.DatabaseContext;
using JWTToken_API.Enums;
using JWTToken_API.Interfaces;
using JWTToken_API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace JWTToken_API.Services
{
    public class UserService : IUserService
    {
        private readonly MyDBContext _myDBContext;
        public UserService(MyDBContext myDBContext)
        {
            _myDBContext = myDBContext;
        }
        /// <summary>
        /// To authanticate user with valid email id and password.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<UserModel> Authenticate(string email, string password)
        {            
            UserModel objUser = await _myDBContext.Users
                .FirstOrDefaultAsync(x => x.Email == email && x.Password == password && x.IsActive == true);
            
            if (objUser != null && objUser.Email == email)
            {
                return objUser;
            }
            else
            {
                return null;
            }           
        }
        /// <summary>
        /// Get list of all active/inactive users without any filter.
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserModel>> GetAllUsers()
        {
            return await _myDBContext.Users.ToListAsync();
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

                if (_myDBContext.Users.Where(x => x.Email == user.Email).ToList().Count > 0)
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
                await _myDBContext.Users.AddAsync(user);
                await _myDBContext.SaveChangesAsync();

                response.Status = true;
                response.Message = "User successfully registered.";

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
