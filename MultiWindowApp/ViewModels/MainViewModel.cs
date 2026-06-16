using ReactiveUI;

namespace MultiWindowApp.ViewModels;

public partial class MainViewModel(IScreen screen) : ViewModelBase, IRoutableViewModel
{
    public IScreen HostScreen { get; } = screen;
    public string UrlPathSegment => "main";
}