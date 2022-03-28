using Microsoft.AspNetCore.Mvc;

namespace GameStore.Areas.Manager.Controllers
{
    public class StoreController : ManagerController
    {
        public IActionResult AddGame()
        {
            return View();
        }
    }
}
