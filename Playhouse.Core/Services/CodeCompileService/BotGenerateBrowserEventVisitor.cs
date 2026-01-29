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
            Statements.Add(
                LocalDeclarationStatement(
                    VariableDeclaration(
                        IdentifierName(nameof(IPage)),
                        SeparatedList([
                        VariableDeclarator(
                            Identifier(_context.GetPageName(browserEvent.Page)),
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
            Statements.Add(
                ExpressionStatement(
                    AwaitExpression(
                        InvocationExpression(
                            MemberAccessExpression(
                                SyntaxKind.SimpleMemberAccessExpression,
                                IdentifierName(_context.GetPageName(browserEvent.Page)),
                                IdentifierName(nameof(IPage.GotoAsync))
                            ),
                            ArgumentList(
                                SeparatedList([
                                    Argument(
                                        LiteralExpression(
                                            SyntaxKind.StringLiteralExpression,
                                            Literal(browserEvent.Url)))]))))));
        }

        public void Visit(BrowserContextClosedBrowserEvent browserEvent)
        {

        }

        public void Visit(LocatorClickBrowserEvent browserEvent)
        {
            
        }

        private sealed class GenerateContext
        {
            private readonly Dictionary<IPage, string> Pages = [];

            public string GetPageName(IPage page)
            {
                if (!Pages.TryGetValue(page, out string? value))
                {
                    value = $"page{Pages.Count}";
                    Pages.Add(page, value);
                }

                return value;
            }
        }
    }
}
