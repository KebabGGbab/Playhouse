using Playhouse.Domain.PlaywrightDecorator;

namespace Playhouse.Domain
{
	public class BrowserConfiguration
    {
        public BrowserTypeLaunchPersistentContextOptionsStrictDecorator Options { get; init; } = null!;

        public int Id { get; private set; }

        public string Name { get; set; } = string.Empty;

        public ISet<Variable> UserVariables { get; } = null!;

        // Конструктор для EntityFramework
        private BrowserConfiguration()
        {
        }

        public BrowserConfiguration(BrowserTypeLaunchPersistentContextOptionsStrictDecorator? options = null)
        {
            Options = options ?? new BrowserTypeLaunchPersistentContextOptionsStrictDecorator();
            UserVariables = new HashSet<Variable>();
        }
    }
}