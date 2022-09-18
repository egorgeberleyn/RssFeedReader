using RssFeeder.Models;

namespace RssFeeder.ViewModels
{
    public class FeedItemsViewModel
    {
        public List<FeedItem> Items { get; set; }
        public int UpdateTime { get; set; }
        public bool IsFormatting { get; set; }
    }
}
