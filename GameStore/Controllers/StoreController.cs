using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers
{
    public class StoreController : Controller
    {
        public IActionResult Games() => View();
    }
}
