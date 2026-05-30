using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Playwright;
using Playhouse.Core.Models.BrowserEvents;
using Playhouse.Core.Models.BrowserEvents.Abstractions;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Playhouse.Core.Services.CodeCompileService
{
    public sealed class BotGenerateBrowserEventVisitor : IBrowserEventVisitor
    {
        private readonly GenerateContext _context = new();

        public IList<StatementSyntax> Statements { get; } = [];

        public void Visit(PageCreatedBrowserEvent browserEvent)
        {
            ArgumentNullException.ThrowIfNull(browserEvent, nameof(browserEvent));

            Statements.Add(
                LocalDeclarationStatement(
                    VariableDeclaration(
                        IdentifierName(nameof(IPage)),
                        SeparatedList([
                        VariableDeclarator(
                            Identifier(_context.GetPageName(browserEvent.Number)),
                            null,
                            EqualsValueClause(
                                AwaitExpression(
                                    InvocationExpression(
                                        MemberAccessExpression(
                                            SyntaxKind.SimpleMemberAccessExpression,
                                            IdentifierName("browser"),
                                            IdentifierName(nameof(IBrowserContext.NewPageAsync)))))))]))));
        }

        public void Visit(PageClosedBrowserEvent browserEvent)
        {
            
        }

        public void Visit(PageGoToBrowserEvent browserEvent)
        {
            ArgumentNullException.ThrowIfNull(browserEvent, nameof(browserEvent));

            Statements.Add(
                ExpressionStatement(
                    AwaitExpression(
                        InvocationExpression(
                            MemberAccessExpression(
                                SyntaxKind.SimpleMemberAccessExpression,
                                IdentifierName(_context.GetPageName(browserEvent.Number)),
                                IdentifierName(nameof(IPage.GotoAsync))
                            ),
                            ArgumentList(
                                SeparatedList([
                                    Argument(
                                        LiteralExpression(
                                            SyntaxKind.StringLiteralExpression,
                                            Literal(browserEvent.Url.ToString())))]))))));
        }

        public void Visit(BrowserContextClosedBrowserEvent browserEvent)
        {

        }

        public void Visit(LocatorClickBrowserEvent browserEvent)
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
