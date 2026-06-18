using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using MultiWindowApp.Models.Interfaces;
using MultiWindowApp.Models.Services;
using ReactiveUI;
using ReactiveUI.SourceGenerators;

namespace MultiWindowApp.ViewModels;

public partial class LoginViewModel : ViewModelBase, IRoutableViewModel
{
    public string UrlPathSegment => "login";
    public IScreen HostScreen { get; }

    private readonly INavigationService _navigationService;
    private readonly IAsyncUserRepository _userRepository;
    
    [Reactive] private string _email = string.Empty;
    [Reactive] private string _password = string.Empty;
    
    private readonly IObservable<bool> _canLogin;

    public LoginViewModel(INavigationService navigationService, IAsyncUserRepository userRepository, IScreen hostScreen)
    {
        HostScreen = hostScreen;
        _navigationService = navigationService;
        _userRepository = userRepository;

        _canLogin = this.WhenAnyValue(
            x => x.Email,
            x => x.Password,
            (email, password) =>
                !string.IsNullOrWhiteSpace(email) &&
                !string.IsNullOrWhiteSpace(password));
    }
    
    [ReactiveCommand(CanExecute = nameof(_canLogin))]
    private async Task Login()
    {
        var user = await _userRepository.GetUserByEmailAsync(Email);
        if (user is not null) _navigationService.NavigateToMain(HostScreen, user);
    }
    
    [ReactiveCommand]
    public void NavigateToRegistration()
    {
        _navigationService.NavigateToRegistration(HostScreen);
    }
}