using System.Collections.Generic;
using System.Threading.Tasks;
using MultiWindowApp.Models.DAOs;

namespace MultiWindowApp.Models.Interfaces;

public interface IAsyncUserRepository
{
    Task<IEnumerable<UserDao>> GetUsersAsync(int? amount);
    Task<UserDao?> GetUserByEmailAsync(string email);
    Task<int> AddUserAsync(UserDao user);
    Task<UserDao?> UpdateUserAsync(UserDao user);
    Task<int> SoftDeleteUserAsync(int userId);
}