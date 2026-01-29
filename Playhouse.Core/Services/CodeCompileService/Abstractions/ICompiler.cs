namespace Playhouse.Core.Services.CodeCompileService.Abstractions
{
    public interface ICompiler
    {
        bool Compile(CompilationInfo info);
    }
}
