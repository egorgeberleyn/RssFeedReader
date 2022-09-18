using Microsoft.AspNetCore.Mvc;
using RssFeeder.Models;
using RssFeeder.Services;
using RssFeeder.ViewModels;

namespace RssFeeder.Controllers
{
    public class HomeController : Controller
    {      
        private readonly IFeedService _feedService;
        private readonly FeedSettings _feedSettings;
        
      
        public HomeController(IFeedService feedService, FeedSettings feedSettings)
        {           
            _feedService = feedService; 
            _feedSettings = feedSettings;
        }

        public IActionResult ChangeFormatting(FeedItemsViewModel model)
        {
            _feedSettings.ChangeFormat(model.IsFormatting);
            return RedirectToAction("Index");
        }
        
        public async Task<IActionResult> Index()
        {                                  
            if (_feedSettings.Uri == null)
                _feedSettings.SetDefault();  
                                  
            return View(new FeedItemsViewModel
            {
                 Items = await _feedService.GetFeedItems(_feedSettings.Uri, _feedSettings.ProxyAdress, _feedSettings.Login, _feedSettings.Password),
                 UpdateTime = _feedSettings.UpdateTime,
                 IsFormatting = _feedSettings.IsFormatting
            });
        }        
    }
}