using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Playwright;
using PlayhouseShare;
using Playhouse.Core.Models.BrowserEvents.Abstractions;
using Playhouse.Core.Models;
using Playhouse.Core.Services.CodeCompileService.Abstractions;

namespace Playhouse.Core.Services.CodeCompileService
{
    public sealed class BotCodeGenerator : ICodeGenerator
    {
        private readonly BotInfo _botInfo;

        public BotCodeGenerator(BotInfo botInfo)
        {
            _botInfo = botInfo;
        }

        public IEnumerable<SyntaxTree> Generate()
        {
            CompilationUnitSyntax compilationUnit = GenerateCompilationUnit().NormalizeWhitespace();
            IEnumerable<SyntaxTree> tree = [CSharpSyntaxTree.Create(compilationUnit)];

            return tree;
        }

        private CompilationUnitSyntax GenerateCompilationUnit() 
        {
            SyntaxList<UsingDirectiveSyntax> usings = GenerateUsingList();
            SyntaxList<MemberDeclarationSyntax> members = List<MemberDeclarationSyntax>([
                GenerateNamespace()
                ]);
            CompilationUnitSyntax compilationUnit = CompilationUnit(default, usings, default, members);

            return compilationUnit;
        }

        private static SyntaxList<UsingDirectiveSyntax> GenerateUsingList()
        {
            return 
                [
                    UsingDirective(ParseName(typeof(IPlaywright).Namespace!)),
                    UsingDirective(ParseName(typeof(Bot).Namespace!)),
                    UsingDirective(ParseName(typeof(Task).Namespace!)),
                    UsingDirective(ParseName(typeof(CancellationToken).Namespace!)),
                ];
        }

        private NamespaceDeclarationSyntax GenerateNamespace()
        {
            IdentifierNameSyntax baseNameProject = IdentifierName(nameof(Playhouse));
            QualifiedNameSyntax namespaceName = QualifiedName(baseNameProject, IdentifierName("Bots"));
            SyntaxList<MemberDeclarationSyntax> members = List<MemberDeclarationSyntax>([
                GenerateClassProgram()
                ]);
            NamespaceDeclarationSyntax namespaceDeclaration = NamespaceDeclaration(
                name: namespaceName,
                externs: default,
                usings: default,
                members: members
                );

            return namespaceDeclaration;
        }

        private ClassDeclarationSyntax GenerateClassProgram()
        {
            SyntaxTokenList modifiers = TokenList(
                Token(SyntaxKind.InternalKeyword),
                Token(SyntaxKind.SealedKeyword)
                );
            SyntaxToken className = Identifier("Program");
            SyntaxList<MemberDeclarationSyntax> members = List<MemberDeclarationSyntax>([
                GenerateMethodRun(),
                ]);
            BaseListSyntax baseList = BaseList(SeparatedList<BaseTypeSyntax>([
                SimpleBaseType(IdentifierName(nameof(Bot)))
                ]));
            ClassDeclarationSyntax classDeclaration = ClassDeclaration(
                attributeLists: default,
                modifiers: modifiers,
                identifier: className,
                typeParameterList: default,
                baseList: baseList,
                constraintClauses: default,
                members: members
                );

            return classDeclaration;
        }

        private MethodDeclarationSyntax GenerateMethodRun()
        {
            SyntaxToken parameterNameBrowser = Identifier("browser");
            SyntaxToken parameterNameCancellation = Identifier("cancellation");
            ParameterListSyntax parameters = ParameterList(SeparatedList([
                Parameter(
                    attributeLists: default,
                    modifiers: default,
                    type: IdentifierName(nameof(IBrowserContext)),
                    identifier: parameterNameBrowser,
                    @default: default
                    ),
                Parameter(
                    attributeLists: default,
                    modifiers: default,
                    type: NullableType(IdentifierName(nameof(CancellationToken))),
                    identifier: parameterNameCancellation,
                    @default: EqualsValueClause(LiteralExpression(SyntaxKind.NullLiteralExpression))
                    ),
                ]));
            SyntaxTokenList modifiers = TokenList(
                Token(SyntaxKind.PublicKeyword),
                Token(SyntaxKind.AsyncKeyword),
                Token(SyntaxKind.OverrideKeyword)
                );
            SyntaxToken methodName = Identifier(nameof(Bot.RunAsync));
            IdentifierNameSyntax returnType = IdentifierName(nameof(Task));
            MethodDeclarationSyntax method = MethodDeclaration(
                attributeLists: default,
                modifiers: modifiers,
                returnType: returnType,
                explicitInterfaceSpecifier: default!,
                identifier: methodName,
                typeParameterList: default!,
                parameterList: parameters,
                constraintClauses: default,
                body: GenerateBodyRunMethod(),
                semicolonToken: default
                );

            return method;
        }

        private BlockSyntax GenerateBodyRunMethod()
        {
            BotGenerateBrowserEventVisitor visitor = new();
            
            foreach (BrowserEvent eventArg in _botInfo.BrowserEvents)
            {
                eventArg.Accept(visitor);
            }

            SyntaxList<StatementSyntax> statements = List(visitor.Statements);

            return Block(statements);
        }
    }
}