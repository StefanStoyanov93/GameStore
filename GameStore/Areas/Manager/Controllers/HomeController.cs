using Microsoft.AspNetCore.Mvc;

namespace GameStore.Areas.Manager.Controllers
{
    public class HomeController : ManagerController
    {
        public IActionResult Index() => View();
    }
}
