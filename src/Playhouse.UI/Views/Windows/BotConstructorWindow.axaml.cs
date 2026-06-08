using Avalonia.Controls;
using Avalonia.Interactivity;
using Playhouse.ViewModels.ViewModels;

namespace Playhouse.UI.Views.Windows;

internal sealed partial class BotConstructorWindow : Window
{
    private readonly BotConstructorViewModel _vm = null!;

    /// <summary>
    /// Конструктор для дизайнера
    /// </summary>
    public BotConstructorWindow()
    {
        if (!Design.IsDesignMode)
        {
            throw new InvalidOperationException();
        }

        InitializeComponent();
    }

    public BotConstructorWindow(BotConstructorViewModel viewModel)
    {
        ArgumentNullException.ThrowIfNull(viewModel);

        _vm = viewModel;
        DataContext = viewModel;
        InitializeComponent();
    }

    private async void Window_Loaded(object? sender, RoutedEventArgs e)
    {
        if (Design.IsDesignMode)
        {
            return;
        }

        await _vm.StartConstructionCommand.ExecuteAsync(null);
    }

    private async void Save(object? sender, RoutedEventArgs e)
    {
        if (Design.IsDesignMode)
        {
            return;
        }
        _vm.Bot.SaveCommand.Execute(null);
        
        await _vm.CompleteConstructionCommand.ExecuteAsync(null);
        Close(_vm.Bot);
    }
}