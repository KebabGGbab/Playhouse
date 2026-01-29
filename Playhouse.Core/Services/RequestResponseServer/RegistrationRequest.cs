//using Playhouse.Services.RequestResponseServer.Abstractions;
//using PlayhouseShare.DTO;

//namespace Playhouse.Services.RequestResponseServer
//{
//	internal class RegistrationRequest : IRequestToServer<UserDTO, LoginDetails>
//	{
//		private const string _uri = "Authentification/Registration";
//		private readonly IHttpRequestExecutor _executer;

//		public RegistrationRequest(IHttpRequestExecutor executer)
//		{
//			_executer = executer;
//		}

//		public async Task<LoginDetails?> SendAsync(UserDTO request, CancellationToken cancellationToken = default)
//		{
//			return await _executer.PostAsync<UserDTO, LoginDetails>(request, _uri, cancellationToken).ConfigureAwait(false);
//		}
//	}
//}
