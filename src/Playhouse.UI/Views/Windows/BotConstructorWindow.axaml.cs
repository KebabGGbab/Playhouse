using Avalonia.Controls;
using Playhouse.ViewModels.ViewModels;

namespace Playhouse.UI.Views.Windows;

internal partial class BotConstructorWindow : Window
{
    public BotConstructorWindow(BotConstructorViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}