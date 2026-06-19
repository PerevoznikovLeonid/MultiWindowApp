using System;
using Avalonia;
using Avalonia.Controls;
using MultiWindowApp.ViewModels;
using ReactiveUI;
using ReactiveUI.Avalonia;

namespace MultiWindowApp.Views;

public partial class MainView : ReactiveUserControl<MainViewModel>
{
    public MainView()
    {
        InitializeComponent();
    }
}