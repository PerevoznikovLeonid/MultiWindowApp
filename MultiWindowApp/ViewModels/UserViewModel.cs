using System;
using MultiWindowApp.Models.Entities;
using MultiWindowApp.Models.Enums;

namespace MultiWindowApp.ViewModels;

public class UserViewModel : ViewModelBase
{
    public int Id { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public string Email { get; }
    public string Password { get; }
    public Gender Gender { get; }
    public DateOnly BirthDate { get; }
    public bool IsAdmin { get; }
    public bool IsDeleted { get; }

    public string FullName => $"{FirstName} {LastName}";

    public UserViewModel(UserEntity user)
    {
        Id = user.Id;
        FirstName = user.FirstName;
        LastName = user.LastName;
        Email = user.Email;
        Password = user.Password;
        Gender = user.Gender;
        BirthDate = user.BirthDate;
        IsAdmin = user.IsAdmin;
        IsDeleted = user.IsDeleted;
    }

    public UserViewModel(UserViewModel user)
    {
        Id = user.Id;
        FirstName = user.FirstName;
        LastName = user.LastName;
        Email = user.Email;
        Password = user.Password;
        Gender = user.Gender;
        BirthDate = user.BirthDate;
        IsAdmin = user.IsAdmin;
        IsDeleted = user.IsDeleted;
    }
    
    public UserEntity ToUserEntity() => new()
    {
        Id = this.Id,
        FirstName = this.FirstName,
        LastName = this.LastName,
        Email = this.Email,
        Password = this.Password,
        Gender = this.Gender,
        BirthDate = this.BirthDate,
        IsAdmin = this.IsAdmin,
        IsDeleted = this.IsDeleted
    };
}