using System.Collections.Generic;
using System.Threading.Tasks;
using MultiWindowApp.Models.Entities;

namespace MultiWindowApp.Models.Interfaces;

public interface IAsyncUserRepository
{
    Task<IEnumerable<UserEntity>> GetUsersAsync(int? amount);
    Task<UserEntity?> GetUserByIdAsync(int userId);
    Task<UserEntity?> GetUserByEmailAsync(string email);
    Task<int> AddUserAsync(UserEntity user);
    Task<UserEntity?> UpdateUserAsync(UserEntity user);
    Task<int> SoftDeleteUserAsync(int userId);
}