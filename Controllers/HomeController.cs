using Microsoft.AspNetCore.Mvc;

namespace Retail_Banking_API.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
