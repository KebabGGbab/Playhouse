namespace Playhouse.Core.Services.CodeCompileService.Abstractions
{
    public interface ICodeCompiler<T> 
    {
        bool Compile(T data);
    }
}
