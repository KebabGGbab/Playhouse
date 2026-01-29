using Microsoft.Extensions.Options;
using Playhouse.Core.Models;
using Playhouse.Core.Models.ConfigurationOptions;
using Playhouse.Core.Services.ConfigurationService.Abstractions;
using Playhouse.Core.Services.EntityManagerService.Abstractions;
using Playhouse.Core.Services.FileManagerService.Abstractions;

namespace Playhouse.Core.Services.EntityManagerService
{
	public sealed class ProfileManager : IEntityManager<BrowserProfile>
	{
		private readonly FileCRUDBase<BrowserProfile> _fileCRUD;
		private readonly IConfigurationUpdater _configurationUpdater;
		private EntityOptions _options;

		public ProfileManager(FileCRUDBase<BrowserProfile> fileCRUD, IOptionsMonitor<EntityOptions> options, IConfigurationUpdater configurationUpdater) 
		{
			_fileCRUD = fileCRUD;
			_configurationUpdater = configurationUpdater;
			_options = options.CurrentValue;
			options.OnChange((e) => _options = e);
		}

		public void Delete(int id)
		{
			_fileCRUD.Delete(id);
		}

		public async Task<IList<BrowserProfile>> GetAsync()
		{
			return await _fileCRUD.GetAsync().ConfigureAwait(false);
		}

		public async Task<BrowserProfile?> GetAsync(int id)
		{
			return await _fileCRUD.GetAsync(id).ConfigureAwait(false);
		}

		public async Task CreateOrUpdate(BrowserProfile obj)
		{
			if (obj.Id == default)
			{
				obj.Id = ++_options.ProfileLastId;

				if (string.IsNullOrEmpty(obj.Name))
				{
					obj.Name = _options.DefaultProfileName + obj.Id;
				}
			}

			await _configurationUpdater.UpdateAsync((c) =>
			{
				c.Entity.ProfileLastId = obj.Id;
				return c;

            }).ConfigureAwait(false);
			await _fileCRUD.CreateOrUpdate(obj).ConfigureAwait(false);
		}
	}
}
