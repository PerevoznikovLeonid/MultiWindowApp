using ReactiveUI;

namespace MultiWindowApp.ViewModels;

public partial class RegistrationViewModel: ViewModelBase, IRoutableViewModel
{
    public string UrlPathSegment => "registration";
    public IScreen HostScreen { get; }
    
    public RegistrationViewModel(IScreen hostScreen)
    {
        HostScreen = hostScreen;
    }
}