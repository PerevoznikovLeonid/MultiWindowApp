using System.Reactive.Linq;
using System.Threading.Tasks;
using ReactiveUI;
using ReactiveUI.SourceGenerators;

namespace MultiWindowApp.ViewModels;

public partial class LoginViewModel(IScreen hostScreen) : ViewModelBase, IRoutableViewModel
{
    public string UrlPathSegment => "login";
    public IScreen HostScreen { get; } = hostScreen;

    [Reactive] private string _email = string.Empty;
    [Reactive] private string _password = string.Empty;

    [ReactiveCommand]
    private async Task Login()
    {
        await HostScreen.Router.Navigate.Execute(new MainViewModel(HostScreen));
    }
}