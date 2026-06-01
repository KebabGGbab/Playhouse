using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Playwright;
using Playhouse.Core.Models.BotActions;
using Playhouse.Core.Models.BotActions.Abstractions;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Playhouse.Core.Services.CodeCompileService
{
    public sealed class BotGenerateBrowserEventVisitor : IBotActionVisitor
    {
        private readonly GenerateContext _context = new();

        public IList<StatementSyntax> Statements { get; } = [];

        public void Visit(PageCreatedBotAction action)
        {
            ArgumentNullException.ThrowIfNull(action, nameof(action));

            Statements.Add(
                LocalDeclarationStatement(
                    VariableDeclaration(
                        IdentifierName(nameof(IPage)),
                        SeparatedList([
                        VariableDeclarator(
                            Identifier(_context.GetPageName(action.Number)),
                            null,
                            EqualsValueClause(
                                AwaitExpression(
                                    InvocationExpression(
                                        MemberAccessExpression(
                                            SyntaxKind.SimpleMemberAccessExpression,
                                            IdentifierName("browser"),
                                            IdentifierName(nameof(IBrowserContext.NewPageAsync)))))))]))));
        }

        public void Visit(PageClosedBotAction action)
        {
            
        }

        public void Visit(PageGoToBotAction action)
        {
            ArgumentNullException.ThrowIfNull(action, nameof(action));

            Statements.Add(
                ExpressionStatement(
                    AwaitExpression(
                        InvocationExpression(
                            MemberAccessExpression(
                                SyntaxKind.SimpleMemberAccessExpression,
                                IdentifierName(_context.GetPageName(action.Number)),
                                IdentifierName(nameof(IPage.GotoAsync))
                            ),
                            ArgumentList(
                                SeparatedList([
                                    Argument(
                                        LiteralExpression(
                                            SyntaxKind.StringLiteralExpression,
                                            Literal(action.Url.ToString())))]))))));
        }

        public void Visit(BrowserContextClosedBotAction action)
        {

        }

        public void Visit(LocatorClickBotAction action)
        {
            
        }

        private sealed class GenerateContext
        {
            private readonly Dictionary<int, string> Pages = [];

            public string GetPageName(int pageNumber)
            {
                if (!Pages.TryGetValue(pageNumber, out string? value))
                {
                    value = $"page{Pages.Count}";
                    Pages.Add(pageNumber, value);
                }

                return value;
            }
        }
    }
}
