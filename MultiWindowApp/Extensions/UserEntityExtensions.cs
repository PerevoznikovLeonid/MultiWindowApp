using System.Collections.Generic;
using System.Linq;
using MultiWindowApp.Models.Entities;
using MultiWindowApp.ViewModels;

namespace MultiWindowApp.Extensions;

public static class UserEntityExtensions
{
    public static UserViewModel ToUserViewModel(this UserEntity user)
        => new(user);
    
    public static IEnumerable<UserViewModel> ToUserViewModels(this IEnumerable<UserEntity> users)
        => users.Select(u => u.ToUserViewModel());
}