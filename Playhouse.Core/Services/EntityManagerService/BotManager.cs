using Microsoft.Extensions.Options;
using Playhouse.Core.Models;
using Playhouse.Core.Models.ConfigurationOptions;
using Playhouse.Core.Services.ConfigurationService.Abstractions;
using Playhouse.Core.Services.EntityManagerService.Abstractions;
using Playhouse.Core.Services.FileManagerService.Abstractions;

namespace Playhouse.Core.Services.EntityManagerService
{
	public sealed class BotManager : IEntityManager<BotInfo>
	{
		private readonly FileCRUDBase<BotInfo> _fileCRUD;
		private readonly IConfigurationUpdater _configurationUpdater;
		private EntityOptions _options;

		public BotManager(FileCRUDBase<BotInfo> fileCRUD, IOptionsMonitor<EntityOptions> options, IConfigurationUpdater configurationUpdater)
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

		public async Task<IList<BotInfo>> GetAsync()
		{
			return await _fileCRUD.GetAsync().ConfigureAwait(false);
		}

		public async Task<BotInfo?> GetAsync(int id)
		{
			return await _fileCRUD.GetAsync(id).ConfigureAwait(false);
		}

		public async Task CreateOrUpdate(BotInfo obj)
		{
			if (obj.Id == default)
			{
				obj.Id = ++_options.BotLastId;

				if (string.IsNullOrEmpty(obj.Name))
				{
					obj.Name = _options.DefaultBotName + obj.Id;
				}
			}

			await _configurationUpdater.UpdateAsync((c) => 
			{
				c.Entity.BotLastId = obj.Id;
				return c;

            }).ConfigureAwait(false);
			await _fileCRUD.CreateOrUpdate(obj).ConfigureAwait(false);
		}
	}
}
