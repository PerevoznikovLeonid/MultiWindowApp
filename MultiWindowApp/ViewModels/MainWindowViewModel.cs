using System.Collections.ObjectModel;
using ReactiveUI.SourceGenerators;

namespace MultiWindowApp.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<ViewItem> Views =>
    [
        
    ];
    
    [Reactive] private ViewItem _currentViewModel;

    public MainWindowViewModel()
    {
        CurrentViewModel = Views[0];
    }
}