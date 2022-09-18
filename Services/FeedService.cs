using RssFeeder.Models;
using System.Net;
using System.ServiceModel.Syndication;
using System.Xml;

namespace RssFeeder.Services
{
    public class FeedService : IFeedService
    {
        static readonly HttpClient client = new();
        
        public async Task<List<FeedItem>> GetFeedItems(string uri, string proxyAdress, string login, string password)
        {
            
            try
            {
                var feedItems = new List<FeedItem>();

                //config proxy
                if (proxyAdress != null)
                    HttpClient.DefaultProxy = new WebProxy(proxyAdress) { Credentials = new NetworkCredential(login, password) };

                var responseBody = await client.GetStreamAsync(uri);
                XmlReader reader = XmlReader.Create(responseBody);
                SyndicationFeed feed = SyndicationFeed.Load(reader);
                reader.Close();

                foreach (SyndicationItem item in feed.Items)
                {
                    FeedItem itemModel = new()
                    {
                        Title = item.Title.Text,
                        Description = item.Summary.Text.Replace("&nbsp;", " ").Replace("<![CDATA[", string.Empty).Replace("]]>", string.Empty),
                        Link = item.Links[0].Uri.ToString(),
                        PublicationDate = item.PublishDate.ToString()[..item.PublishDate.ToString().IndexOf('+')]
                    };
                    feedItems.Add(itemModel);
                }
                return feedItems;
            }
            catch (HttpRequestException e)
            {
                throw new HttpRequestException(e.Message);                
            }
            catch(Exception e)
            {
                throw new Exception("Error", e);
            }           
        }
    }
}
