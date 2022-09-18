using Newtonsoft.Json;
using RssFeeder.Infrastructure;

namespace RssFeeder.Models
{
    
    public class SessionFeedSettings : FeedSettings
    {
        [JsonIgnore]
        public ISession Session { get; set; }

        public static FeedSettings GetSettings(IServiceProvider service)
        {
            ISession session = service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            SessionFeedSettings settings = session?.GetJson<SessionFeedSettings>("Settings")
            ?? new SessionFeedSettings();
            settings.Session = session;
            return settings;
        }

        public override void Update(FeedSettings settings)
        {
            base.Update(settings);            
            Session.SetJson("Settings", settings);
        }

        public override void SetDefault()
        {
            base.SetDefault();
            Session.SetJson("Settings", this);
        }

        public override void ChangeFormat(bool isFormatting)
        {
            base.ChangeFormat(isFormatting);
            Session.SetJson("Settings", this);
        }
    }
}
