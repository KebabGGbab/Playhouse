namespace Playhouse.Core.Services.FilePathResolverService.Abstractions
{
	public interface IFilePathResolver
	{
		string GetPath(FileType fileTypes);
		string GetPath(FileType fileTypes, int id);
	}
}
