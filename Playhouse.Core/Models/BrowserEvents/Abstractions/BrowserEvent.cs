namespace Playhouse.Core.Models.BrowserEvents.Abstractions
{
    public abstract class BrowserEvent
    {
        public string Title { get; }

        protected BrowserEvent(string title)
        {
            Title = title;
        }

        public override string ToString()
        {
            return Title;
        }

        public abstract void Accept(IBrowserEventVisitor visitor);
    }
}
