namespace Playhouse.Core.Models.BrowserEvents.Abstractions
{
    public class BrowserEvent
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public int BotInfoId { get; set; }
        public BotInfo BotInfo { get; set; }

        public BrowserEvent(string title)
        {
            Title = title;
        }

        public override string ToString()
        {
            return Title;
        }

        public virtual void Accept(IBrowserEventVisitor visitor) { }
    }
}
