using Avalonia.Controls;
using Avalonia.Interactivity;
using Playhouse.ViewModels.ViewModels;

namespace Playhouse.UI.Views.Windows;

internal sealed partial class UpdateWindow : Window
{
    public UpdateWindow(UpdateViewModel vm)
    {
        DataContext = vm;
        InitializeComponent();
    }

    private async void Window_Loaded(object? sender, RoutedEventArgs e)
    {
        await ((UpdateViewModel)DataContext!).InitializeCommand.ExecuteAsync(null);
        Close();
    }
}