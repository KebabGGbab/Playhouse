using Jobs.Abstractions;

namespace Playhouse.Core.Services.BotRunningService
{
    public sealed class BotManagerRunArgs : RunArgs
    {
        public string PathToBot { get; init; }

        public BotManagerRunArgs(string pathToBot)
        {
            PathToBot = pathToBot;
        }
    }
}
