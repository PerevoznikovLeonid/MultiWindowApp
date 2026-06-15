using System.Collections.Generic;
using System.Linq;
using MultiWindowApp.Models.Daos;

namespace MultiWindowApp.Models.Services;

public class QueryHelper
{
    private readonly UsersContext _db;
    
    public QueryHelper(UsersContext usersContext)
    {
        _db = usersContext;
        _db.Database.EnsureCreated();
    }

    public IEnumerable<UserDao> GetUsers(int? amount)
    {
        return amount is null 
            ? _db.Users
            : _db.Users.Take(amount.Value);
    }

    public UserDao? GetUserById(int userId)
    {
        return _db.Users.FirstOrDefault(x => x.Id == userId);
    }
}