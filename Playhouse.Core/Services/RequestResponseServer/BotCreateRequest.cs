//using Playhouse.Services.RequestResponseServer.Abstractions;
//using PlayhouseShare.DTO;
//using System.IO;

//namespace Playhouse.Services.RequestResponseServer
//{
//    internal sealed class BotCreateRequest : IRequestToServer<BotCreateData, Stream>
//    {
//        private const string _uri = "bot/create";
//        private readonly IHttpRequestExecutor _executor;

//        public BotCreateRequest(IHttpRequestExecutor executor)
//        {
//            _executor = executor;
//        }

//		public async Task<Stream?> SendAsync(BotCreateData value, CancellationToken cancellationToken = default)
//		{
//            return await _executor.PostAsStreamAsync<BotCreateData, Stream>(value, _uri, cancellationToken).ConfigureAwait(false);
//		}
//	}
//}
