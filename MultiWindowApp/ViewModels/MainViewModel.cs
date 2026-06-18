using System;
using System.Collections.ObjectModel;
using MultiWindowApp.Extensions;
using MultiWindowApp.Models.Entities;
using MultiWindowApp.Models.Enums;
using MultiWindowApp.Models.Interfaces;
using ReactiveUI;
using ReactiveUI.SourceGenerators;

namespace MultiWindowApp.ViewModels;

public partial class MainViewModel : ViewModelBase, IRoutableViewModel
{
    public IScreen HostScreen { get; }
    public string UrlPathSegment => "main";
    
    private readonly IAsyncUserRepository _userRepository;
    private readonly INavigationService _navigationService;

    private ObservableCollection<UserViewModel> Users { get; }
    
    [Reactive] private UserViewModel? _selectedUser;
    [Reactive] private UserViewModel? _originalUser;
    [Reactive] private int? _id;
    [Reactive] private string? _firstName;
    [Reactive] private string? _lastName;
    [Reactive] private Gender? _gender;
    [Reactive] private DateOnly? _birthDate;
    [Reactive] private string? _email;
    [Reactive] private string? _password;
    
    public MainViewModel(IAsyncUserRepository userRepository, INavigationService navigationService, IScreen hostScreen, UserEntity userEntity)
    {
        HostScreen = hostScreen;
        _userRepository = userRepository;
        _navigationService = navigationService;
        this.WhenAnyValue(x => x.SelectedUser)
            .Subscribe(su =>
            {
                Id = su?.Id;
                LastName = su?.LastName;
                FirstName = su?.FirstName;
                Gender = su?.Gender;
                BirthDate = su?.BirthDate;
                
                OriginalUser = su is not null
                    ? userEntity.ToUserViewModel()
                    : null;
            });

        Users = new ObservableCollection<UserViewModel>(_userRepository.GetUsersAsync(null).Result.ToUserViewModels());
        Users.Insert(0, new UserViewModel(userEntity));
    }
}