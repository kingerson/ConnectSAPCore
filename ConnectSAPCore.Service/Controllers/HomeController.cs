using Microsoft.AspNetCore.Mvc;

namespace ConnectSAPCore.Service.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return new RedirectResult("~/swagger");
        }
    }
}