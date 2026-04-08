namespace Playhouse.Core.Services.FilePathResolverService.Abstractions
{
	public interface IFilePathResolver
	{
        string DirectoryProfiles { get; }

        string DirectoryBots { get; }

        string FileJSEventScripts { get; }

        string GetPathToDirectoryProfile(int id);

        string GetPathToDirectoryUserDataDirProfile(int id);

        string GetPathToDirectoryBot(int id);

        string GetPathToFileDllBot(int id);

    }
}
