using GameStore.Core.Models;
using GameStore.Core.Serveces.Contracts;
using GameStore.Data.Data;
using GameStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers
{
    public class StoreController : Controller
    {
        private readonly IStoreServices service;

        public StoreController(IStoreServices _service)
        {
            service = _service;
        }


        [HttpGet]
        public IActionResult Browse()
        {
            var games = service.GetGames();

            return View(games);
        }

        [Authorize]
        public IActionResult Wishlist(List<StoreGamesViewModel> model)
        {
            return View(model);
        }
    }
}
