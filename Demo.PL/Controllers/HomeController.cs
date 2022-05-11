using Demo.PL.Language;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Demo.PL.Controllers
{

    [Authorize]
    public class HomeController : Controller
    {

        private readonly IStringLocalizer<SharedResource> localizer;

        public HomeController(IStringLocalizer<SharedResource> localizer)
        {
            this.localizer = localizer;
        }
        public IActionResult Index()
        {
            ViewBag.data = "DASHBOARD";
            return View();
        }
        public IActionResult Test()
        {
            return View();
        }
    }
}
