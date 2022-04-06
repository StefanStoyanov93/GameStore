using GameStore.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers
{
    public class CollectionController : Controller
    {
        public IActionResult All(List<BaseGameModel> model)
        {
            return View(model);
        }
    }
}
