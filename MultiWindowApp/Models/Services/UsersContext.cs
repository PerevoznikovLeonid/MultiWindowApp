using Microsoft.EntityFrameworkCore;
using MultiWindowApp.Models.Daos;

namespace MultiWindowApp.Models.Services;

public class UsersContext(string connectionString): DbContext
{
    public DbSet<UserDao> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(connectionString);
    }
}