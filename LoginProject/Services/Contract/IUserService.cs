using Microsoft.EntityFrameworkCore;
using LoginProject.Models;

namespace LoginProject.Services.Contract
{
    public interface IUserService
    {
        Task<User> GetUser(string email, string pass);
        Task<User> SaveUser( User user );
    }
}
