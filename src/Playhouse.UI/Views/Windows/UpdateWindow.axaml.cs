using Avalonia.Controls;
using Avalonia.Interactivity;
using Playhouse.ViewModels.ViewModels;

namespace Playhouse.UI.Views.Windows;

internal sealed partial class UpdateWindow : Window
{
    private readonly UpdateViewModel _vm = null!;

    /// <summary>
    /// Конструктор для дизайнера
    /// </summary>
    public UpdateWindow()
    {
        if (!Design.IsDesignMode)
        {
            throw new InvalidOperationException();
        }

        InitializeComponent();
    }

    public UpdateWindow(UpdateViewModel vm)
    {
        ArgumentNullException.ThrowIfNull(vm);

        DataContext = vm;
        _vm = vm;
        InitializeComponent();
    }

    private async void Window_Loaded(object? sender, RoutedEventArgs e)
    {
        if (Design.IsDesignMode)
        {
            return;
        }

        await _vm.InitializeCommand.ExecuteAsync(null);
        Close();
    }
}