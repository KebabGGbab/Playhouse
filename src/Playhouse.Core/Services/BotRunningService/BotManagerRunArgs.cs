using Jobs.Abstractions;

namespace Playhouse.Core.Services.BotRunningService
{
    public sealed class BotManagerRunArgs : RunArgs
    {
        public string PathToBot { get; }

        public BotManagerRunArgs(string pathToBot)
        {
            PathToBot = pathToBot;
        }
    }
}
