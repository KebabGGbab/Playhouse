using KebabGGbab.CommunityToolkit.MVVM.Extensions.ViewModelAbstractions;
using Playhouse.Core.Services;

namespace Playhouse.ViewModels.ViewModels
{
	public class UpdateViewModel : ViewModelBase
	{
		private readonly IEnumerable<IInitializer> _inits;

		public UpdateViewModel(IEnumerable<IInitializer> inits)
		{
			ArgumentNullException.ThrowIfNull(inits);

			_inits = inits;
		}

        protected override async Task InitializeCoreAsync()
        {
			List<Task> tasks = new(_inits.Count());

			foreach (IInitializer init in _inits)
			{
				tasks.Add(Task.Run(() => init.InitializeAsync()));
			}

			await Task.WhenAll(tasks);
		}
	}
}
