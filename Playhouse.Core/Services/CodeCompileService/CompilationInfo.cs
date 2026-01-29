using Microsoft.CodeAnalysis;

namespace Playhouse.Core.Services.CodeCompileService
{
    public sealed class CompilationInfo
    {
        public required IEnumerable<SyntaxTree> Trees { get; set; }
        public string? AssemblyName { get; set; }
        public required string Path { get; set; }
    }
}
