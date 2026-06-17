using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ReactiveUI;
using ReactiveUI.SourceGenerators;

namespace MultiWindowApp.ViewModels;

public partial class LoginViewModel : ViewModelBase, IRoutableViewModel
{
    public string UrlPathSegment => "login";
    public IScreen HostScreen { get; }

    [Reactive] private string _email = string.Empty;
    [Reactive] private string _password = string.Empty;
    
    private readonly IObservable<bool> _canLogin;

    public LoginViewModel(IScreen hostScreen)
    {
        HostScreen = hostScreen;

        _canLogin = this.WhenAnyValue(
            x => x.Email,
            x => x.Password,
            (email, password) =>
                !string.IsNullOrWhiteSpace(email) &&
                !string.IsNullOrWhiteSpace(password));
    }
    
    [ReactiveCommand(CanExecute = nameof(_canLogin))]
    private void Login()
    {
        HostScreen.Router.Navigate.Execute(new MainViewModel(HostScreen));
    }
    
    [ReactiveCommand]
    public void NavigateToRegistration()
    {
        HostScreen.Router.Navigate.Execute(new RegistrationViewModel(HostScreen));
    }
}