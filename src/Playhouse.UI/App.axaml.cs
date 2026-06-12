using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Playhouse.Core.Data;
using Playhouse.Core.Services.ApplicationSettingsService;
using Playhouse.Core.Services.BotRunningService;
using Playhouse.Core.Services.ConstructorService;
using Playhouse.Core.Services.FileManagerService;
using Playhouse.Core.Services.FilePathResolverService;
using Playhouse.Core.Services.PlaywrightService;
using Playhouse.UI.Services.LocalizationService;
using Playhouse.UI.Services.WindowCreatorService;
using Playhouse.UI.Services.WindowCreatorService.Abstractions;
using Playhouse.UI.Views.Windows;
using Playhouse.ViewModels.Services.ViewModelFactories;
using Playhouse.ViewModels.ViewModels;

namespace Playhouse.UI
{
    public partial class App : Application, IDisposable
    {
        private readonly IHost _host;

        private bool _disposed;

        public App()
        {
            HostApplicationBuilder hostBuilder = Host.CreateApplicationBuilder();
            ConfigureServices(hostBuilder.Services);
            ConfigureConfiguration(hostBuilder.Configuration);
            ConfigureLogging(hostBuilder.Logging);
            _host = hostBuilder.Build();
            _host.RunAsync();
        }

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                if (Design.IsDesignMode)
                {
                    desktop.MainWindow = _host.Services.GetRequiredService<MainWindow>();
                }
                else
                {
                    desktop.ShutdownMode = ShutdownMode.OnLastWindowClose;
                    UpdateWindow window = _host.Services.GetRequiredService<UpdateWindow>();
                    window.Closed += UpdateWindowClosed;
                    window.Show();
                }
            }

            base.OnFrameworkInitializationCompleted();
        }

        private void UpdateWindowClosed(object? sender, EventArgs e)
        {
            _host.Services.GetRequiredService<MainWindow>().Show();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;

            if (disposing)
            {
                _host.Dispose();
            }
        }

        ~App()
        {
            Dispose(false);
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<UpdateWindow>();
            services.AddSingleton<MainWindow>();
            services.AddSingleton<BotConstructorWindow>();
            services.AddMainWindowViewModel();
            services.AddSingleton<BotConstructorViewModel>();
            services.AddSingleton<IWindowFactory, WindowFactory>();
            services.AddBotConstruction();
            services.AddFilePathResolver();
            services.AddPlaywright();
            services.AddRunning();
            services.AddSettings();
            services.AddFileManagers();
            services.AddViewModelFactories();
            services.AddLocalizationApp();
            services.AddData("LocalDb");
        }

        private static void ConfigureConfiguration(ConfigurationManager configuration)
        {
            configuration.Sources.Clear();

            if (Design.IsDesignMode == false)
            {
                configuration.AddJsonFile(FilePathResolver.AppSettings.FullName, false, true);
            }
        }

        private static void ConfigureLogging(ILoggingBuilder logging)
        {
            logging.ClearProviders();
        }
    }
}