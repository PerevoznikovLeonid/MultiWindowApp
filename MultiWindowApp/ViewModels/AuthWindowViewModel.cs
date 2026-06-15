using System.Collections.ObjectModel;
using ReactiveUI.SourceGenerators;

namespace MultiWindowApp.ViewModels;

public partial class AuthWindowViewModel: ViewModelBase
{
    public ObservableCollection<ViewItem> Views =>
    [
        new ()
        {
            Name = "Регистрация",
            ViewModel = new RegistrationViewModel()
        },
        new ()
        {
            Name = "Вход",
            ViewModel = new LoginViewModel()
        }
    ];
    
    [Reactive] private ViewItem _currentViewModel;

    public AuthWindowViewModel()
    {
        CurrentViewModel = Views[0];
    }
}