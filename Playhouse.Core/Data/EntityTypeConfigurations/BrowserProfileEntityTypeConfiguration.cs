using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Playhouse.Core.Models;

namespace Playhouse.Core.Data.EntityTypeConfigurations
{
    /// <summary>
    /// Конфигурация типа сущности <see cref="BrowserProfile"/>.
    /// </summary>
    internal sealed class BrowserProfileEntityTypeConfiguration : IEntityTypeConfiguration<BrowserProfile>
    {
        public void Configure(EntityTypeBuilder<BrowserProfile> builder)
        {
            builder.ToTable(t => t.HasComment("Настройки контекста браузера."));

            builder.Ignore(p => p.Args);
            builder.Ignore(p => p.BaseURL);
            builder.Ignore(p => p.BypassCSP);
            builder.Ignore(p => p.ClientCertificates);
            builder.Ignore(p => p.ColorScheme);
            builder.Ignore(p => p.Contrast);
            builder.Ignore(p => p.DeviceScaleFactor);
            builder.Ignore(p => p.Devtools);
            builder.Ignore(p => p.Env);
            builder.Ignore(p => p.ExecutablePath);
            builder.Ignore(p => p.ExtraHTTPHeaders);
            builder.Ignore(p => p.FirefoxUserPrefs);
            builder.Ignore(p => p.ForcedColors);
            builder.Ignore(p => p.Geolocation);
            builder.Ignore(p => p.HandleSIGHUP);
            builder.Ignore(p => p.HandleSIGINT);
            builder.Ignore(p => p.HandleSIGTERM);
            builder.Ignore(p => p.HasTouch);
            builder.Ignore(p => p.HttpCredentials);
            builder.Ignore(p => p.IgnoreAllDefaultArgs);
            builder.Ignore(p => p.IgnoreDefaultArgs);
            builder.Ignore(p => p.IgnoreHTTPSErrors);
            builder.Ignore(p => p.IsMobile);
            builder.Ignore(p => p.JavaScriptEnabled);
            builder.Ignore(p => p.Locale);
            builder.Ignore(p => p.Offline);
            builder.Ignore(p => p.Permissions);
            builder.Ignore(p => p.Proxy);
            builder.Ignore(p => p.RecordHarContent);
            builder.Ignore(p => p.RecordHarMode);
            builder.Ignore(p => p.RecordHarOmitContent);
            builder.Ignore(p => p.RecordHarPath);
            builder.Ignore(p => p.RecordHarUrlFilter);
            builder.Ignore(p => p.RecordHarUrlFilterRegex);
            builder.Ignore(p => p.RecordHarUrlFilterString);
            builder.Ignore(p => p.RecordVideoDir);
            builder.Ignore(p => p.RecordVideoSize);
            builder.Ignore(p => p.ReducedMotion);
            builder.Ignore(p => p.ScreenSize);
            builder.Ignore(p => p.ServiceWorkers);
            builder.Ignore(p => p.StrictSelectors);
            builder.Ignore(p => p.Timeout);
            builder.Ignore(p => p.TimezoneId);
            builder.Ignore(p => p.TracesDir);
            builder.Ignore(p => p.UserAgent);
            builder.Ignore(p => p.ViewportSize);
        }
    }
}
