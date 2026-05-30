using Avalonia.Controls;
using Playhouse.ViewModels.ViewModels;

namespace Playhouse.UI.Views;

internal partial class BotConstructorWindow : Window
{
    public BotConstructorWindow(BotConstructorViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}