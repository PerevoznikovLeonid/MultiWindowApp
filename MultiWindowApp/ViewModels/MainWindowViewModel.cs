using Microsoft.Extensions.DependencyInjection;
using MultiWindowApp.Models.Interfaces;
using ReactiveUI;

namespace MultiWindowApp.ViewModels;

public partial class MainWindowViewModel : ViewModelBase, IScreen
{
    public RoutingState Router { get; }

    public MainWindowViewModel(INavigationService navigationService)
    {
        Router = new RoutingState();

        navigationService.NavigateToRegistration(this);
    }
}