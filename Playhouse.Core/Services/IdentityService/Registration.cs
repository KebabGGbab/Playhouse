//using Playhouse.Services.RequestResponseServer.Abstractions;
//using PlayhouseShare.DTO;

//namespace Playhouse.Services.IdentityService
//{
//	internal class Registration : IRegistration
//	{
//		private readonly IRequestToServer<UserDTO, LoginDetails> _request;

//		public Registration(IRequestToServer<UserDTO, LoginDetails> request)
//		{
//			_request = request;
//		}

//		public async Task<bool> RegisterAsync(UserDTO user)
//		{
//			LoginDetails loginDetails = await _request.SendAsync(user);

//			if (string.IsNullOrEmpty(loginDetails.Token))
//			{
//				return false;
//			}

//			return true;
//		}
//	}
//}
