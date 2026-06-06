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
            ArgumentNullException.ThrowIfNull(model);

            PathResolver.GetUserDataDir(model.Id).Create();
        }

        public override void Delete(BrowserConfiguration model)
        {
            ArgumentNullException.ThrowIfNull(model);

            PathResolver.GetBrowserDirectory(model.Id).Delete(true);
        }
    }
}
