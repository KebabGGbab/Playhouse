using Microsoft.CodeAnalysis;

namespace Playhouse.Core.Services.CodeCompileService.Abstractions
{
    public interface ICodeGenerator
    {
        IEnumerable<SyntaxTree> Generate();
    }
}
