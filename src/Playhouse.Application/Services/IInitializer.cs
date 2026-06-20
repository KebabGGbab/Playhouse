namespace Playhouse.Application.Services
{
    public interface IInitializer
    {
        bool IsInitialized { get; }

        Task InitializeAsync(CancellationToken cancellation = default);
    }
}
