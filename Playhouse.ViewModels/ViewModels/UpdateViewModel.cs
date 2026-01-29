using CommunityToolkit.Mvvm.ComponentModel;
using Playhouse.Core.Services.PlaywrightService.Abstractions;

namespace Playhouse.ViewModels.ViewModels
{
	public class UpdateViewModel : ObservableObject
	{
		public byte TotalSteps { get; init; } = 1;

		private readonly IPlaywrightBrowserInstaller _playwrightBrowserInstaller;

		public bool IsUpdateSuccessful { get; private set; }

		public byte Step
		{
			get => field;
			set => SetProperty(ref field, value);
		}
		public string Message
		{
			get => field;
			set => SetProperty(ref field, value);
		}


		public UpdateViewModel(IPlaywrightBrowserInstaller playwrightBrowserInstaller)
		{
			_playwrightBrowserInstaller = playwrightBrowserInstaller;
		}

		public async Task CheckAndInstallUpdateAsync()
		{
			await Task.Run(() =>
			{
				Step = 1;
				Message = "Поиск, установка и обновление браузеров";
				_playwrightBrowserInstaller.Install();
			}).ConfigureAwait(false);
			IsUpdateSuccessful = true;
		}
	}
}
