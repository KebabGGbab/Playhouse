namespace Playhouse.Core.Services.FilePathResolverService.Abstractions
{
	public interface IFilePathResolver
	{
        DirectoryInfo Browsers { get; }

        DirectoryInfo Bots { get; }

        FileInfo FileJSEventScripts { get; }

        DirectoryInfo GetBrowserDirectory(int browserId);

        DirectoryInfo GetUserDataDir(int browserId);

        DirectoryInfo GetBotDirectory(int botId);

        FileInfo GetBotDllFile(int botId);

    }
}
