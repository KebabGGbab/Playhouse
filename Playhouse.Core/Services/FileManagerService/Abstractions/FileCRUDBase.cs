using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using KebabGGbab.TextKit;
using Playhouse.Core.Services.FilePathResolverService.Abstractions;

namespace Playhouse.Core.Services.FileManagerService.Abstractions
{
	public abstract class FileCRUDBase<T>
	{
		protected readonly FileStreamOptions _streamWriteOptions;
		protected readonly FileStreamOptions _streamReadOptions;
		protected readonly JsonSerializerOptions _jsonSerializerOptions;
		protected readonly IFilePathResolver _fullPath;

		public FileCRUDBase(IFilePathResolver fullPath)
		{
			_fullPath = fullPath;

			_streamReadOptions = FileStreamOptionsDefaults.Read;
			_streamWriteOptions = FileStreamOptionsDefaults.Write;
			_jsonSerializerOptions = new JsonSerializerOptions()
			{
				WriteIndented = true,
				Converters =
					{
						new JsonStringEnumConverter()
                    },
				Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
			};
		}
		public abstract Task<IList<T>> GetAsync();
		public abstract Task<T?> GetAsync(int id);
		public abstract Task CreateOrUpdate(T obj);
		public abstract void Delete(int id);
	}
}