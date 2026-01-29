//using Playhouse.Services.RequestResponseServer.Abstractions;
//using PlayhouseShare.DTO;

//namespace Playhouse.Services.RequestResponseServer
//{
//	internal sealed class AuthorizationRequest : IRequestToServer<UserCredentialsDTO, LoginDetails>
//	{
//		private const string _uri = "Authentification/Authorization";
//		private readonly IHttpRequestExecutor _executer;

//		public AuthorizationRequest(IHttpRequestExecutor executer)
//		{
//			_executer = executer;
//		}

//		public async Task<LoginDetails?> SendAsync(UserCredentialsDTO request, CancellationToken cancellationToken = default)
//		{
//			return await _executer.PostAsync<UserCredentialsDTO, LoginDetails>(request, _uri, cancellationToken).ConfigureAwait(false);
//		}
//	}
//}
