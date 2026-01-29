//using Playhouse.Services.RequestResponseServer.Abstractions;
//using PlayhouseShare.DTO;

//namespace Playhouse.Services.IdentityService
//{
//	internal class Authorization : IAuthorization
//	{
//		private readonly IRequestToServer<UserCredentialsDTO, LoginDetails> _request;

//		public Authorization(IRequestToServer<UserCredentialsDTO, LoginDetails> request)
//		{
//			_request = request;
//		}

//		public async Task<bool> LoginAsync(UserCredentialsDTO credentials)
//		{
//			LoginDetails loginDetails = await _request.SendAsync(credentials);

//			if (string.IsNullOrEmpty(loginDetails.Token))
//			{
//				return false;
//			}

//			return true;
//		}
//	}
//}
