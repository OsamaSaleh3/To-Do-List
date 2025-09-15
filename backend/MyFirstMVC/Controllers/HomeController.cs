using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyFirstMVC.Models;

namespace MyFirstMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

       public ViewResult Show()
        {
            var ViewResult = new ViewResult();
            ViewResult.ViewName = "view1";
            return ViewResult;
        }

        public ContentResult Say()
        {
            var contentResult = new ContentResult();
            contentResult.Content = "hi";
            return contentResult;
        }

        public IActionResult Mix(int id)
        {
            if (id % 2 == 0)
            {
                return Content("Osama");
            }
            else
            {
                return View("view1");
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
