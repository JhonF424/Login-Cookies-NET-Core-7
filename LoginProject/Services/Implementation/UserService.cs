using Microsoft.EntityFrameworkCore;
using LoginProject.Models;
using LoginProject.Services.Contract;

namespace LoginProject.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly LogindbContext _dbContext;

        public UserService(LogindbContext dbContext)
        {
            _dbContext= dbContext;
        }
        public async Task<User> GetUser( string email, string pass )
        {
            User foundUser = await _dbContext.Users.Where(u => u.Email == email && u.Pass == pass).FirstOrDefaultAsync();
            return foundUser;
        }

        public async Task<User> SaveUser( User user )
        {
            _dbContext.Users.Add( user );
            await _dbContext.SaveChangesAsync();
            return user;
        }
    }
}
