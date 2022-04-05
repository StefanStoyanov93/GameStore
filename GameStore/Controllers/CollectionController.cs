using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers
{
    public class CollectionController : Controller
    {
        public IActionResult All()
        {
            return View();
        }
    }
}
