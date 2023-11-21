using JWTToken_API.DatabaseContext;
using JWTToken_API.Interfaces.ReposInterfaces;
using JWTToken_API.Model;
using Microsoft.EntityFrameworkCore;

namespace JWTToken_API.Repositories
{
    public class UsersRepository : IUserRepository
    {
        private readonly MyDBContext _myDBContext;
        public UsersRepository(MyDBContext myDBContext)
        {
            _myDBContext = myDBContext;
        }
        /// <summary>
        /// To authanticate user with valid email id and password.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>user details</returns>
        public async Task<UserModel> Authenticate(string email, string password)
        {
            return await _myDBContext.Users
                .FirstOrDefaultAsync(x => x.Email == email && x.Password == password && x.IsActive == true);
        }
        /// <summary>
        /// Get list of all active/inactive users without any filter.
        /// </summary>
        /// <returns>list of users</returns>
        public async Task<List<UserModel>> GetAll()
        {
            return await _myDBContext.Users.ToListAsync();
        }

        /// <summary>
        /// Get list of all active/inactive users with email filter.
        /// </summary>
        /// <returns>list of users</returns>
        public async Task<List<UserModel>> GetByEmail(string email)
        {
            return await _myDBContext.Users.Where(x => x.Email == email).ToListAsync();
        }
        /// <summary>
        /// Insert users into database whose emailid does not exist in the database.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>number of affected rows</returns>
        public async Task<int> Insert(UserModel user)
        {
            await _myDBContext.Users.AddAsync(user);
            return await _myDBContext.SaveChangesAsync();
        }
    }
}
