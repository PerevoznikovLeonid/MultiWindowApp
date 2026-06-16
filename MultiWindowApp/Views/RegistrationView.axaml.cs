using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MultiWindowApp.ViewModels;
using ReactiveUI;

namespace MultiWindowApp.Views;

public partial class RegistrationView : UserControl, IViewFor<RegistrationViewModel>
{
    public RegistrationView()
    {
        InitializeComponent();
    }
    
    public static readonly StyledProperty<RegistrationViewModel?> ViewModelProperty =
        AvaloniaProperty.Register<RegistrationView, RegistrationViewModel?>(nameof(ViewModel));

    public RegistrationViewModel? ViewModel
    {
        get => GetValue(ViewModelProperty);
        set => SetValue(ViewModelProperty, value);
    }
    
    object? IViewFor.ViewModel
    {
        get => ViewModel;
        set => ViewModel = (RegistrationViewModel?)value;
    }
    
    protected override void OnDataContextChanged(EventArgs e)
    {
        base.OnDataContextChanged(e);
        if (DataContext is RegistrationViewModel vm)
            ViewModel = vm;
    }
}