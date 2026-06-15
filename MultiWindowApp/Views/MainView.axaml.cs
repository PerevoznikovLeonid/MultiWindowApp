using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MultiWindowApp.ViewModels;
using ReactiveUI;

namespace MultiWindowApp.Views;

public partial class MainView : UserControl, IViewFor<MainViewModel>
{
    public MainView() => InitializeComponent();

    private void InitializeComponent() => AvaloniaXamlLoader.Load(this);

    public MainViewModel? ViewModel
    {
        get => (MainViewModel?)GetValue(ViewModelProperty);
        set => SetValue(ViewModelProperty, value);
    }

    object? IViewFor.ViewModel
    {
        get => ViewModel;
        set => ViewModel = (MainViewModel?)value;
    }

    public static readonly StyledProperty<MainViewModel?> ViewModelProperty =
        AvaloniaProperty.Register<MainView, MainViewModel?>(nameof(ViewModel));
}