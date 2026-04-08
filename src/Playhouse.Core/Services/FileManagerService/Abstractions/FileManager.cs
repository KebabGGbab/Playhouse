using Playhouse.Core.Services.FilePathResolverService.Abstractions;

namespace Playhouse.Core.Services.FileManagerService.Abstractions
{
    public abstract class FileManager<TModel>
    {
        protected IFilePathResolver PathResolver { get; }

        protected FileManager(IFilePathResolver pathResolver)
        {
            ArgumentNullException.ThrowIfNull(pathResolver, nameof(pathResolver));

            PathResolver = pathResolver;
        }

        public abstract void Create(TModel model);

        public abstract void Delete(TModel model);
    }
}
