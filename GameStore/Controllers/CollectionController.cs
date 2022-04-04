using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers
{
    public class CollectionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
