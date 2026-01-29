using System.Text.Json;
using KebabGGbab.Extensions.Logging;
using KebabGGbab.Json;
using Microsoft.Extensions.Logging;
using Playhouse.Core.Json;
using Playhouse.Core.Models;
using Playhouse.Core.Services.CodeCompileService.Abstractions;
using Playhouse.Core.Services.FileManagerService.Abstractions;
using Playhouse.Core.Services.FilePathResolverService;
using Playhouse.Core.Services.FilePathResolverService.Abstractions;

namespace Playhouse.Core.Services.FileManagerService
{
    public sealed class BotFileCRUD : FileCRUDBase<BotInfo>
	{
		private readonly ILogger<BotFileCRUD> _logger;
		private readonly ICodeCompiler<BotInfo> _codeCompiler;

		public BotFileCRUD(IFilePathResolver fullPath, ILogger<BotFileCRUD> logger, ICodeCompiler<BotInfo> codeCompiler) : base(fullPath)
		{ 
			_logger = logger;
			_codeCompiler = codeCompiler;
			_jsonSerializerOptions.TypeInfoResolver = new PolymorphicTypeResolver();

        }

		public override async Task<IList<BotInfo>> GetAsync()
		{
			string pathDirectory = _fullPath.GetPath(FileType.DirectoryBots);
			string[] directoriesBot;

			try
			{
				directoriesBot = Directory.GetDirectories(pathDirectory);
			}
			catch(UnauthorizedAccessException e)
			{
				_logger.LogException(e, pathDirectory, "Нет разрешения для доступа к каталогу размещения ботов.");
				throw;
			}
			catch(ArgumentNullException e)
			{
				_logger.LogException(e, $"Отсутствует путь к каталогу размещения ботов. Путь: {pathDirectory}.");
				throw;
			}
			catch(ArgumentException e)
			{
				_logger.LogException(e, $"Путь к каталогу размещения ботов пуст, содержит только пробелы или недопустимые символы. Путь: {pathDirectory}");
				throw;
			}
			catch(DirectoryNotFoundException e)
			{
				_logger.LogException(e, pathDirectory, "Каталог размещения ботов не найден.");
				throw;
			}
			catch(PathTooLongException e)
			{
				_logger.LogException(e, pathDirectory, "Указанный путь к каталогу ботов превышает максимальную длину, заданную в системе.");
				throw;
			}
			catch(IOException e)
			{
				_logger.LogException(e, pathDirectory, "Путь к каталогу, содержащему ботов, ведёт к файлу, либо поврежден и недоступен для чтения.");
				throw;
			}

			List<BotInfo> bots = new(directoriesBot.Length);

			foreach (string directoryBot in directoriesBot)
			{
				if (!int.TryParse(Path.GetFileName(directoryBot), out int id))
				{
					continue;
				}

				BotInfo? bot = await GetAsync(id).ConfigureAwait(false);

				if (bot != null)
				{
					bots.Add(bot);
				}
			}

			return bots;
		}

		public override async Task<BotInfo?> GetAsync(int id)
		{
			string path = _fullPath.GetPath(FileType.FileBotInfo, id);

			if (!File.Exists(path))
			{
				throw new FileNotFoundException("Файл с информацией о боте не был найден.", path);
			}

			try
			{
				return await JsonOperations.ReadJsonAsync<BotInfo?>(path, _streamReadOptions, _jsonSerializerOptions).ConfigureAwait(false);
			}
			catch(JsonException e)
			{
				string json = await File.ReadAllTextAsync(path).ConfigureAwait(false);
				_logger.LogException(e, json, _jsonSerializerOptions.MaxDepth, $"Файл с информацией о боте содержит недопустимый текст в JSON-формате.");
				throw;
			}
		}

		public override async Task CreateOrUpdate(BotInfo obj)
		{
			string path = _fullPath.GetPath(FileType.FileBotInfo, obj.Id);

			if (!File.Exists(path))
			{
				string pathDirectoryBot = _fullPath.GetPath(FileType.DirectoryBot, obj.Id);

				try
				{
					Directory.CreateDirectory(pathDirectoryBot);
				}
				catch(DirectoryNotFoundException e)
				{
					_logger.LogException(e, pathDirectoryBot, "Каталог размещения ботов не найдена.");
					throw;
				}
				catch(PathTooLongException e)
				{
					_logger.LogException(e, pathDirectoryBot, "Указанный путь к каталогу бота превышает максимальную длину, заданную в системе.");
					throw;
				}
				catch(IOException e)
				{
					_logger.LogException(e, pathDirectoryBot, "Указанный каталог бота является файлом.");
					throw;
				}
				catch(UnauthorizedAccessException e)
				{
					_logger.LogException(e, pathDirectoryBot, "Нет разрешения для создания файлов в каталогу размещения ботов.");
					throw;
				}
				catch(ArgumentException e)
				{
					_logger.LogException(e, $"Путь к каталогу размещения ботов пуст, содержит только пробелы или недопустимые символы. Путь: {pathDirectoryBot}");
					throw;
				} 
			}

			await JsonOperations.WriteJsonAsync(path, obj, _streamWriteOptions, _jsonSerializerOptions).ConfigureAwait(false);
			_codeCompiler.Compile(obj);
		}

		public override void Delete(int id)
		{
			string path = _fullPath.GetPath(FileType.DirectoryBot, id);

			try
			{
				Directory.Delete(path, true);
			}
			catch(UnauthorizedAccessException e)
			{
				_logger.LogException(e, path, "Нет разрешения для удаления каталога бота.");
				throw;
			}
			catch (ArgumentException e)
			{
				_logger.LogException(e, $"Путь к каталогу бота пуст, содержит только пробелы или недопустимые символы. Путь: {path}");
				throw;
			}
			catch (DirectoryNotFoundException e)
			{
				_logger.LogException(e, path, "Каталог бота не найден для удаления.");
				throw;
			}
			catch(PathTooLongException e)
			{
				_logger.LogException(e, path, "Указанный путь к каталогу бота превышает максимальную длину, заданную в системе.");
				throw;
			}
			catch(IOException e)
			{
				_logger.LogException(e, path, "Каталог бота используется другим процессом.");
				throw;
			}
		}
	}
}