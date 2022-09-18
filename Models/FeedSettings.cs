using Microsoft.AspNetCore.Mvc.ModelBinding;
using RssFeeder.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace RssFeeder.Models
{
    [Serializable]
    public class FeedSettings
    {
        [Required(ErrorMessage = "Введите Uri-адрес RSS-ленты")] 
        public string Uri { get; set; }

        [Required(ErrorMessage = "Введите количество минут")]
        [Range(1, int.MaxValue, ErrorMessage = "Количество минут должно быть больше 0")]
        public int UpdateTime { get; set; }

        [BindNever]
        public string ProxyAdress { get; set; }
        [BindNever]
        public string Login { get; set; }
        [BindNever]
        public string Password { get; set; }

        public bool IsFormatting { get; set; }

        public virtual void Update(FeedSettings settings)
        {
            Uri = settings.Uri;
            UpdateTime = settings.UpdateTime;
        }
        
        public virtual void SetDefault()
        {
            XmlDeserializer deserializer = new();
            deserializer.DeserializeSettings(this);
        }

        public virtual void ChangeFormat(bool isFormatting)
        {
            IsFormatting = isFormatting;
        }
    }
}
