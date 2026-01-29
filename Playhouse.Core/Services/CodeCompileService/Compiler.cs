using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using Playhouse.Core.Services.CodeCompileService.Abstractions;

namespace Playhouse.Core.Services.CodeCompileService
{
    public sealed class Compiler : ICompiler
    {
        private readonly CSharpCompilationOptions _options;
        private readonly IEnumerable<MetadataReference> _references;

        public Compiler(CSharpCompilationOptions options, IEnumerable<MetadataReference> references) 
        {
            ArgumentNullException.ThrowIfNull(options);
            ArgumentNullException.ThrowIfNull(references);

            _options = options;
            _references = references;
        }

        public bool Compile(CompilationInfo info)
        {
            CSharpCompilation compilation = CSharpCompilation.Create(
                assemblyName: info.AssemblyName ?? Guid.NewGuid().ToString(),
                syntaxTrees: info.Trees,
                references: _references,
                options: _options
                );

            string code = compilation.SyntaxTrees[0].ToString();

            EmitResult emitResult = compilation.Emit(info.Path);

            if (emitResult.Diagnostics.Length != 0)
            {
                foreach(Diagnostic diagnostic in emitResult.Diagnostics)
                {
                    //TODO: Сделать логгирование ошибок компиляции бота
                }

                return false;
            }

            return true;
        }
    }
}
