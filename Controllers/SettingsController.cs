using Microsoft.AspNetCore.Mvc;
using RssFeeder.Models;

namespace RssFeeder.Controllers
{
    public class SettingsController : Controller
    {
        private readonly FeedSettings feedSettings;
        public SettingsController(FeedSettings settings)
        {
            feedSettings = settings;
        }

        [HttpGet]
        public IActionResult FeedSettings() => View(feedSettings);

        [HttpPost]
        public IActionResult FeedSettings(FeedSettings settings)
        {
            if (ModelState.IsValid)
            {
                feedSettings.Update(settings);
                return RedirectToAction("Index", "Home", settings);
            }
            return View(settings);
        }
    }
}
