using ReactiveUI;

namespace MultiWindowApp.ViewModels;

public partial class MainViewModel: ViewModelBase, IRoutableViewModel
{
    public IScreen HostScreen { get; }
    public string UrlPathSegment => "main";

    public MainViewModel(IScreen screen)
    {
        HostScreen = screen;
    }
}