using ReactiveUI;

namespace MultiWindowApp.ViewModels;

public partial class MainWindowViewModel : ViewModelBase, IScreen
{
    public RoutingState Router { get; } = new();

    public MainWindowViewModel()
    {
        Router.Navigate.Execute(new RegistrationViewModel(this));
    }
}