using Playhouse.Core.Models;
using Playhouse.Core.Services.FileManagerService.Abstractions;
using Playhouse.Core.Services.FilePathResolverService.Abstractions;

namespace Playhouse.Core.Services.FileManagerService
{
    public class BrowserProfileFileManager : FileManager<BrowserConfiguration>
    {
        public BrowserProfileFileManager(IFilePathResolver pathResolver) : base(pathResolver) 
        {
        } 

        public override void Create(BrowserConfiguration model)
        {
            ArgumentNullException.ThrowIfNull(model, nameof(model));

            Directory.CreateDirectory(PathResolver.GetPathToDirectoryUserDataDirProfile(model.Id));
        }

        public override void Delete(BrowserConfiguration model)
        {
            ArgumentNullException.ThrowIfNull(model, nameof(model));

            Directory.Delete(PathResolver.GetPathToDirectoryProfile(model.Id), true);
        }
    }
}
