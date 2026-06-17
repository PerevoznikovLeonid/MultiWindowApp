using System;
using System.Collections.Generic;
using System.Linq;
using MultiWindowApp.Models.Enums;
using ReactiveUI;
using ReactiveUI.SourceGenerators;

namespace MultiWindowApp.ViewModels;

public partial class RegistrationViewModel : ViewModelBase, IRoutableViewModel
{
    public string UrlPathSegment => "registration";
    public IScreen HostScreen { get; }
    
    public List<KeyValuePair<Gender, string>> Genders { get; } =
    [
        new(Gender.Unspecified, "Не указан"),
        new(Gender.Male, "Мужской"),
        new(Gender.Female, "Женский")
    ];
    
    public DateTimeOffset? BirthDatePicker
    {
        get => DateOfBirth.ToDateTime(TimeOnly.MinValue, DateTimeKind.Unspecified);
        set
        {
            if (value.HasValue)
                DateOfBirth = DateOnly.FromDateTime(value.Value.DateTime);
        }
    }

    [Reactive] private string _firstName = string.Empty;
    [Reactive] private string _lastName = string.Empty;
    [Reactive] private Gender _gender = Gender.Unspecified;
    [Reactive] private DateOnly _dateOfBirth;
    [Reactive] private string _email = string.Empty;
    [Reactive] private string _password = string.Empty;
    [Reactive] private string _passwordConfirm = string.Empty;
    
    private readonly IObservable<bool> _canRegister;

    public RegistrationViewModel(IScreen hostScreen)
    {
        HostScreen = hostScreen;

        _canRegister = this.WhenAnyValue(
            x => x.FirstName,
            x => x.LastName,
            x => x.DateOfBirth,
            x => x.Email,
            x => x.Password,
            (firstName, lastName, dateOfBirth, email, password) =>
                !string.IsNullOrWhiteSpace(firstName) &&
                !string.IsNullOrWhiteSpace(lastName) &&
                dateOfBirth != default &&
                !string.IsNullOrWhiteSpace(email) &&
                !string.IsNullOrWhiteSpace(password));
    }
    
    [ReactiveCommand(CanExecute = nameof(_canRegister))]
    public void Register()
    {
        HostScreen.Router.Navigate.Execute(new MainViewModel(HostScreen));
    }

    [ReactiveCommand]
    public void NavigateToLogin()
    {
        HostScreen.Router.Navigate.Execute(new LoginViewModel(HostScreen));
    }
}