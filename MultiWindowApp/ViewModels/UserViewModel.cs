using System;
using MultiWindowApp.Models.Entities;
using MultiWindowApp.Models.Enums;

namespace MultiWindowApp.ViewModels;

public class UserViewModel(UserEntity user) : ViewModelBase
{
    public int Id { get; } = user.Id;
    public string FirstName { get; } = user.FirstName;
    public string LastName { get; } = user.LastName;
    public string Email { get; } = user.Email;
    public Gender Gender { get; } = user.Gender;
    public DateOnly BirthDate { get; } = user.BirthDate;
    public bool IsAdmin { get; } = user.IsAdmin;
    public bool IsDeleted { get; } = user.IsDeleted;

    public string FullName => $"{FirstName} {LastName}";

    public UserEntity ToUserDao() => new()
    {
        Id = Id,
        FirstName = FirstName,
        LastName = LastName,
        Email = Email,
        Gender = Gender,
        BirthDate = BirthDate,
        IsAdmin = IsAdmin,
        IsDeleted = IsDeleted
    };
}