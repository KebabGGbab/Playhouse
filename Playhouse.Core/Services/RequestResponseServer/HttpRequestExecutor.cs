//using System.IO;
//using System.Net.Http;
//using System.Net.Http.Headers;
//using System.Net.Http.Json;
//using Playhouse.Services.RequestResponseServer.Abstractions;

//namespace Playhouse.Services.RequestResponseServer
//{
//	internal sealed class HttpRequestExecutor : IHttpRequestExecutor, IDisposable
//	{
//		private const string SERVER = "http://localhost:5097/api/";

//		private bool _isDisposed;

//		private static readonly HttpClient _httpClient = new()
//		{
//			BaseAddress = new Uri(SERVER),
//		};

//		public HttpRequestExecutor()
//		{
//			_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/json", 1));
//		}

//		public async Task<TResponse?> GetAsync<TResponse>(string relativeUri, CancellationToken cancellationToken = default)
//		{
//			using HttpResponseMessage response = await _httpClient.GetAsync(
//				GetUri(relativeUri), 
//				cancellationToken
//				).ConfigureAwait(false);

//			return await ReadJsonAsync<TResponse?>(response, cancellationToken).ConfigureAwait(false);
//		}

//		public async Task<TResponse?> PostAsync<TRequest, TResponse>(TRequest value, string relativeUri, CancellationToken cancellationToken = default)
//		{
//			using HttpResponseMessage response = await _httpClient.PostAsJsonAsync(
//				GetUri(relativeUri),
//				value, 
//				cancellationToken
//				).ConfigureAwait(false);

//			return await ReadJsonAsync<TResponse?>(response, cancellationToken).ConfigureAwait(false);
//		}

//		public async Task<TResponse?> PostAsStreamAsync<TRequest, TResponse>(TRequest value, string relativeUri, CancellationToken cancellationToken = default) where TResponse : Stream
//		{
//			using HttpResponseMessage response = await _httpClient.PostAsJsonAsync(
//				GetUri(relativeUri), 
//				value, 
//				cancellationToken
//				).ConfigureAwait(false);

//			return await ReadStreamAsync<TResponse>(response, cancellationToken).ConfigureAwait(false);
//		}

//		private static async Task<TResponse?> ReadJsonAsync<TResponse>(HttpResponseMessage response, CancellationToken cancellationToken)
//		{
//			return await response.Content.ReadFromJsonAsync<TResponse?>(cancellationToken).ConfigureAwait(false);
//		}

//		private static async Task<TResponse> ReadStreamAsync<TResponse>(HttpResponseMessage response, CancellationToken cancellationToken) where TResponse : Stream
//		{
//			return (TResponse)await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
//		}

//		private static Uri GetUri(string relativeUri)
//		{
//			return new Uri(SERVER + relativeUri);
//		}

//		public void Dispose()
//		{
//			if (_isDisposed)
//			{
//				return;
//			}

//			_httpClient.Dispose();

//			_isDisposed = true;
//			GC.SuppressFinalize(this);
//		}
//	}
//}
