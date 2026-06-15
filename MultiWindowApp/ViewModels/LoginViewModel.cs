using System.Reactive.Linq;
using System.Threading.Tasks;
using ReactiveUI;
using ReactiveUI.SourceGenerators;

namespace MultiWindowApp.ViewModels;

public partial class LoginViewModel: ViewModelBase, IRoutableViewModel
{
    public string UrlPathSegment => "login";
    public IScreen HostScreen { get; }
    
    [Reactive] private string _email = string.Empty;
    [Reactive] private string _password = string.Empty;

    public LoginViewModel(IScreen hostScreen)
    {
        HostScreen = hostScreen;
    }
    
    [ReactiveCommand]
    private async Task Login()
    {
        await HostScreen.Router.Navigate.Execute(new MainViewModel(HostScreen));
    }
}