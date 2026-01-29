using KebabGGbab.Extensions.Logging;
using KebabGGbab.Json;
using Microsoft.Extensions.Logging;
using Playhouse.Core.Models;
using Playhouse.Core.Services.FileManagerService.Abstractions;
using Playhouse.Core.Services.FilePathResolverService;
using Playhouse.Core.Services.FilePathResolverService.Abstractions;
using System.Text.Json;

namespace Playhouse.Core.Services.FileManagerService
{
    public sealed class ProfileFileCRUD : FileCRUDBase<BrowserProfile>
	{
		private readonly ILogger<ProfileFileCRUD> _logger;

		public ProfileFileCRUD(IFilePathResolver fullPath, ILogger<ProfileFileCRUD> logger) : base(fullPath)
		{ 
			_logger = logger;
		}

		public override async Task<IList<BrowserProfile>> GetAsync()
		{
			string pathDirectory = _fullPath.GetPath(FileType.DirectoryProfiles);
			string[] directoriesProfile;

			try
			{
				directoriesProfile = Directory.GetDirectories(pathDirectory);
			}
			catch (UnauthorizedAccessException e)
			{
				_logger.LogException(e, pathDirectory, "Нет разрешения для доступа к каталогу размещения профилей.");
				throw;
			}
			catch (ArgumentNullException e)
			{
				_logger.LogException(e, $"Отсутствует путь к каталогу размещения профилей. Путь: {pathDirectory}.");
				throw;
			}
			catch (ArgumentException e)
			{
				_logger.LogException(e, $"Путь к каталогу размещения профилей пуст, содержит только пробелы или недопустимые символы. Путь: {pathDirectory}");
				throw;
			}
			catch (DirectoryNotFoundException e)
			{
				_logger.LogException(e, pathDirectory, "Каталог размещения профилей не найдена.");
				throw;
			}
			catch (PathTooLongException e)
			{
				_logger.LogException(e, pathDirectory, "Указанный путь к каталогу профилей превышает максимальную длину, заданную в системе.");
				throw;
			}
			catch (IOException e)
			{
				_logger.LogException(e, pathDirectory, "Путь к каталогу, содержащему профили, ведёт к файлу, либо поврежден и недоступен для чтения.");
				throw;
			}

			List<BrowserProfile> profiles = new(directoriesProfile.Length);

			foreach (string directoryProfile in directoriesProfile)
			{
				if (!int.TryParse(Path.GetFileName(directoryProfile), out int id))
				{
					continue;
				}

				BrowserProfile? profile = await GetAsync(id).ConfigureAwait(false);

				if (profile != null)
				{
					profiles.Add(profile);
				}
			}

			return profiles;
		}

		public override async Task<BrowserProfile?> GetAsync(int id)
		{
			string path = _fullPath.GetPath(FileType.FileProfileInfo, id);

			if (!File.Exists(path))
			{
				throw new FileNotFoundException("Файл с информацией о профиле не был найден.", path);
			}

			try
			{
				return await JsonOperations.ReadJsonAsync<BrowserProfile?>(path, _streamReadOptions, _jsonSerializerOptions).ConfigureAwait(false);
			}
			catch (JsonException e)
			{
				string json = await File.ReadAllTextAsync(path).ConfigureAwait(false);
				_logger.LogException(e, json, _jsonSerializerOptions.MaxDepth, $"Файл с информацией о профиле содержит недопустимый текст в JSON-формате.");
				throw;
			}
		}

		public override async Task CreateOrUpdate(BrowserProfile obj)
		{
			string path = _fullPath.GetPath(FileType.FileProfileInfo, obj.Id);

			if (!File.Exists(path))
			{
				string pathDirectoryProfile = _fullPath.GetPath(FileType.DirectoryUserDataDir, obj.Id);

				try
				{
					Directory.CreateDirectory(pathDirectoryProfile);
				}
				catch (DirectoryNotFoundException e)
				{
					_logger.LogException(e, pathDirectoryProfile, "Каталог размещения профилей не найден.");
					throw;
				}
				catch (PathTooLongException e)
				{
					_logger.LogException(e, pathDirectoryProfile, "Указанный путь к каталогу профиля превышает максимальную длину, заданную в системе.");
					throw;
				}
				catch (IOException e)
				{
					_logger.LogException(e, pathDirectoryProfile, "Указанный каталог профиля является файлом.");
					throw;
				}
				catch (UnauthorizedAccessException e)
				{
					_logger.LogException(e, pathDirectoryProfile, "Нет разрешения для создания файлов в каталоге размещения профилей.");
					throw;
				}
				catch (ArgumentException e)
				{
					_logger.LogException(e, $"Путь к каталогу размещения ботов пуст, содержит только пробелы или недопустимые символы. Путь: {pathDirectoryProfile}");
					throw;
				}
			}

			await JsonOperations.WriteJsonAsync(path, obj, _streamWriteOptions, _jsonSerializerOptions).ConfigureAwait(false);
		}

		public override void Delete(int id)
		{
			string path = _fullPath.GetPath(FileType.DirectoryProfile, id);

			try
			{
				Directory.Delete(path, true);
			}
			catch (UnauthorizedAccessException e)
			{
				_logger.LogException(e, path, "Нет разрешения для удаления каталога профиля.");
				throw;
			}
			catch (ArgumentException e)
			{
				_logger.LogException(e, $"Путь к каталогу профиля пуст, содержит только пробелы или недопустимые символы. Путь: {path}");
				throw;
			}
			catch (DirectoryNotFoundException e)
			{
				_logger.LogException(e, path, "Каталог профиля не найден для удаления.");
				throw;
			}
			catch (PathTooLongException e)
			{
				_logger.LogException(e, path, "Указанный путь к каталогу профиля превышает максимальную длину, заданную в системе.");
				throw;
			}
			catch (IOException e)
			{
				_logger.LogException(e, path, "Каталог профиля используется другим процессом.");
				throw;
			}
		}
	}
}
