using ReactiveUI;

namespace MultiWindowApp.ViewModels;

public partial class RegistrationViewModel(IScreen hostScreen) : ViewModelBase, IRoutableViewModel
{
    public string UrlPathSegment => "registration";
    public IScreen HostScreen { get; } = hostScreen;
}