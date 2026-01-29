//using System.IO;

//namespace Playhouse.Services.RequestResponseServer.Abstractions
//{
//	internal interface IHttpRequestExecutor : IDisposable
//	{
//		Task<TResponse?> GetAsync<TResponse>(string relativeUri, CancellationToken cancellationToken = default);

//		Task<TResponse?> PostAsync<TRequest, TResponse>(TRequest value, string relativeUri, CancellationToken cancellationToken = default);

//		Task<TResponse?> PostAsStreamAsync<TRequest, TResponse>(TRequest value, string relativeUri,CancellationToken cancellationToken = default) where TResponse : Stream;
//	}
//}
