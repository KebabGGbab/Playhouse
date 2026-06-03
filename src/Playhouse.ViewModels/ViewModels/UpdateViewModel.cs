using KebabGGbab.CommunityToolkit.MVVM.Extensions.ViewModelAbstractions;
using Playhouse.Core.Services;
using Playhouse.Core.Services.ApplicationSettingsService;

namespace Playhouse.ViewModels.ViewModels
{
	public class UpdateViewModel : ViewModelBase
	{
		private readonly ISettingsService _settings;

		private readonly IEnumerable<IInitializer> _inits;

		public UpdateViewModel(ISettingsService settings, IEnumerable<IInitializer> inits)
		{
			ArgumentNullException.ThrowIfNull(settings);
			ArgumentNullException.ThrowIfNull(inits);

			_settings = settings;
			_inits = inits;
		}

        protected override async Task InitializeCoreAsync()
        {
			await _settings.LoadAsync().ConfigureAwait(false);
			List<Task> tasks = new(_inits.Count());

			foreach (IInitializer init in _inits)
			{
				tasks.Add(Task.Run(() => init.InitializeAsync()));
			}

			await Task.WhenAll(tasks);
		}
	}
}
