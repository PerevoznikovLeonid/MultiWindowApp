using System;
using MultiWindowApp.Models.Enums;

namespace MultiWindowApp.Models.DAOs;

public class UserDao
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public Gender Gender { get; set; } = Gender.Unspecified;
    public DateOnly BirthDate { get; set; }
    public bool IsAdmin { get; set; }
    public bool IsDeleted { get; set; }
}