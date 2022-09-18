using RssFeeder.Models;
using System.Xml.Serialization;

namespace RssFeeder.Infrastructure
{
    public class XmlDeserializer
    {
        public void DeserializeSettings(FeedSettings settings)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(FeedSettings), new XmlRootAttribute("FeederSettings"));

            // десериализуем объект
            using FileStream fs = new FileStream("FeederSettings.xml", FileMode.OpenOrCreate);
            FeedSettings xmlSettings = xmlSerializer.Deserialize(fs) as FeedSettings;
            if(xmlSettings.Uri != null && xmlSettings.UpdateTime > 0)
            {
                settings.Uri = xmlSettings.Uri;
                settings.UpdateTime = xmlSettings.UpdateTime;
            }    
            if(xmlSettings.ProxyAdress != null)
            {
                settings.ProxyAdress = xmlSettings.ProxyAdress;
                settings.Login = xmlSettings.Login;
                settings.Password = xmlSettings.Password;
            }

        }
    }
}
