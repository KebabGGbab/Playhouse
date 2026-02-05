using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Playhouse.Core.Models;
using Playhouse.Core.Models.ConfigurationOptions;
using Playhouse.Core.Services.BotConstructorService;
using Playhouse.Core.Services.BotConstructorService.Abstractions;
using Playhouse.Core.Services.BotRunningService;
using Playhouse.Core.Services.BotRunningService.Abstrtactions;
using Playhouse.Core.Services.ConfigurationService;
using Playhouse.Core.Services.ConfigurationService.Abstractions;
using Playhouse.Core.Services.EntityManagerService;
using Playhouse.Core.Services.EntityManagerService.Abstractions;
using Playhouse.Core.Services.FileManagerService;
using Playhouse.Core.Services.FileManagerService.Abstractions;
using Playhouse.Core.Services.FilePathResolverService;
using Playhouse.Core.Services.FilePathResolverService.Abstractions;
using Playhouse.Core.Services.PlaywrightService;
using Playhouse.Core.Services.PlaywrightService.Abstractions;
using Playhouse.UI.Resources.Localization;
using Playhouse.UI.Services.WindowCreatorService;
using Playhouse.UI.Services.WindowCreatorService.Abstractions;
using Playhouse.UI.Views;
using Playhouse.ViewModels.CoreExtensions.Services;
using Playhouse.ViewModels.Services.LocalizationService;
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
            ConfigureConfiguration(hostBuilder.Services, hostBuilder.Configuration);
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
                desktop.MainWindow = _host.Services.GetRequiredService<MainWindow>();
            }

            base.OnFrameworkInitializationCompleted();
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
            services.AddSingleton<UpdateViewModel>();
            services.AddSingleton<ProfilesViewModel>();
            services.AddSingleton<RunViewModel>();
            services.AddSingleton<BotViewModel>();
            services.AddSingleton<SettingsViewModel>();
            services.AddSingleton<BotConstructorViewModel>();
            services.AddSingleton<IFilePathResolver, FilePathResolver>();
            services.AddSingleton<FileCRUDBase<BrowserProfile>, ProfileFileCRUD>();
            services.AddSingleton<FileCRUDBase<BotInfo>, BotFileCRUD>();
            services.AddSingleton<IEntityManager<BrowserProfile>, ProfileManager>();
            services.AddSingleton<IEntityManager<BotInfo>, BotManager>();
            services.AddSingleton<IPlaywrightBrowserInstaller, PlaywrightBrowserInstaller>();
            services.AddSingleton<IPlaywrightFactory, PlaywrightFactory>();
            services.AddSingleton<IConfigurationUpdater, ConfigurationUpdater>();
            services.AddSingleton<IWindowFactory, WindowFactory>();
            services.AddSingleton<IBotConstructor, BotConstructor>();
            services.AddSingleton<IBotJobManagerFactory, BotJobManagerFactory>();
            services.AddBotCompiler();
            services.AddLocalization(StringsUI.ResourceManager);
        }

        private static void ConfigureConfiguration(IServiceCollection services, ConfigurationManager configuration)
        {
            configuration.Sources.Clear();
            if (!Design.IsDesignMode)
            {
                configuration.AddJsonFile(FilePathResolver.AppSettings, true, true);
                configuration.AddJsonFile(FilePathResolver.UserSettings, false, true);
            }
            services.Configure<FileLocationsOptions>(configuration.GetSection(FileLocationsOptions.Name));
            services.Configure<EntityOptions>(configuration.GetSection(EntityOptions.Name));
            services.Configure<PlaywrightOptions>(configuration.GetSection(PlaywrightOptions.Name));
            services.Configure<ViewOptions>(configuration.GetSection(ViewOptions.Name));
            services.Configure<UserSettings>(configuration);
        }

        private static void ConfigureLogging(ILoggingBuilder logging)
        {
            logging.ClearProviders();
        }
    }
}