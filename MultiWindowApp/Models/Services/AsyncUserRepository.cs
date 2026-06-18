using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using MultiWindowApp.Models.DAOs;
using MultiWindowApp.Models.Interfaces;
using Npgsql;

namespace MultiWindowApp.Models.Services;

public class AsyncUserRepository(NpgsqlDataSource dataSource): IAsyncUserRepository
{
    public async Task<IEnumerable<UserDao>> GetUsersAsync(int? amount)
    {
        var sql = "SELECT * FROM table_users WHERE is_deleted = false";
        if (amount.HasValue)
            sql += " LIMIT @amount";
        await using var db = await dataSource.OpenConnectionAsync();
        return await db.QueryAsync<UserDao>(sql, 
            new { amount });
    }

    public async Task<UserDao?> GetUserByEmailAsync(string email)
    {
        const string sql = "SELECT * FROM table_users WHERE is_deleted = false AND email = @email";
        await using var db = await dataSource.OpenConnectionAsync();
        return await db.QueryFirstOrDefaultAsync<UserDao>(sql, 
            new { email });
    }
    
    public async Task<int> AddUserAsync(UserDao user)
    {
        if (user.IsDeleted)
            user.IsDeleted = false;
        const string sql = """
                            INSERT INTO table_users (first_name, last_name, gender, birth_date, email, password, is_admin, is_deleted)
                            VALUES (@FirstName, @LastName, @Gender, @BirthDate, @Email, @Password, @IsAdmin, @IsDeleted)
                            RETURNING id
                            """;
        await using var db = await dataSource.OpenConnectionAsync();
        return await db.ExecuteScalarAsync<int>(sql, user);
    }

    public async Task<UserDao?> UpdateUserAsync(UserDao user)
    {
        const string sql = """
                           UPDATE table_users
                           SET 
                               first_name = @FirstName,
                               last_name = @LastName,
                               gender = @Gender,
                               birth_date = @BirthDate,
                               email = @Email,
                               password = @Password,
                               is_admin = @IsAdmin,
                               is_deleted = @IsDeleted
                           WHERE is_deleted = false
                            AND id = @Id
                           RETURNING *
                           """;
        await using var db = await dataSource.OpenConnectionAsync();
        return await db.QueryFirstOrDefaultAsync<UserDao>(sql, user);
    }
    
    public async Task<int> SoftDeleteUserAsync(int userId)
    {
        const string sql = """
                           UPDATE table_users
                           SET is_deleted = true
                           WHERE is_deleted = false
                             AND id = @Id
                           """;
        await using var db = await dataSource.OpenConnectionAsync();
        return await db.ExecuteAsync(sql, new { Id = userId });
    }
}