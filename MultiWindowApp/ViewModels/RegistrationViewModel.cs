using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MultiWindowApp.Models.DAOs;
using MultiWindowApp.Models.Enums;
using MultiWindowApp.Models.Interfaces;
using ReactiveUI;
using ReactiveUI.SourceGenerators;

namespace MultiWindowApp.ViewModels;

public partial class RegistrationViewModel : ViewModelBase, IRoutableViewModel
{
    public string UrlPathSegment => "registration";
    public IScreen HostScreen { get; }
    
    private readonly IAsyncUserRepository _userRepository;
    private readonly INavigationService _navigationService;
    
    public List<KeyValuePair<Gender, string>> Genders { get; } =
    [
        new(Gender.Unspecified, "Не указан"),
        new(Gender.Male, "Мужской"),
        new(Gender.Female, "Женский")
    ];
    
    public DateTimeOffset? BirthDatePicker
    {
        get => BirthDate.ToDateTime(TimeOnly.MinValue, DateTimeKind.Unspecified);
        set
        {
            if (value.HasValue)
                BirthDate = DateOnly.FromDateTime(value.Value.DateTime);
        }
    }

    [Reactive] private string _firstName = string.Empty;
    [Reactive] private string _lastName = string.Empty;
    [Reactive] private Gender _gender = Gender.Unspecified;
    [Reactive] private DateOnly _birthDate;
    [Reactive] private string _email = string.Empty;
    [Reactive] private string _password = string.Empty;
    [Reactive] private string _passwordConfirm = string.Empty;
    
    private readonly IObservable<bool> _canRegister;

    public RegistrationViewModel(INavigationService navigationService, IScreen hostScreen, IAsyncUserRepository userRepository)
    {
        HostScreen = hostScreen;
        _userRepository = userRepository;
        _navigationService = navigationService;

        _canRegister = this.WhenAnyValue(
            x => x.FirstName,
            x => x.LastName,
            x => x.BirthDate,
            x => x.Email,
            x => x.Password,
            x => x.PasswordConfirm,
            (firstName, lastName, dateOfBirth, email, password, passwordConfirm) =>
                !string.IsNullOrWhiteSpace(firstName) &&
                !string.IsNullOrWhiteSpace(lastName) &&
                dateOfBirth != default &&
                !string.IsNullOrWhiteSpace(email) &&
                !string.IsNullOrWhiteSpace(password) &&
                !string.IsNullOrWhiteSpace(passwordConfirm) &&
                password == passwordConfirm);
    }
    
    [ReactiveCommand(CanExecute = nameof(_canRegister))]
    private async Task Register()
    {
        if (await _userRepository.GetUserByEmailAsync(Email) is null)
        {
            var user = new UserDao
            {
                FirstName = this.FirstName,
                LastName = this.LastName,
                Gender = this.Gender,
                BirthDate = this.BirthDate,
                Email = this.Email,
                Password = this.Password
            };
        
            await _userRepository.AddUserAsync(user);
            _navigationService.NavigateToMain(HostScreen, user);
        }
    }

    [ReactiveCommand]
    private void NavigateToLogin()
    {
        _navigationService.NavigateToLogin(HostScreen);
    }
}