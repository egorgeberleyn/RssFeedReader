using RssFeeder.Models;
using System.Net;

namespace RssFeeder.Services
{
    public interface IFeedService
    {
        public Task<List<FeedItem>> GetFeedItems(string uri, string proxyAdress, string login, string password);
    }
}
