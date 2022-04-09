using GameStore.Core.Models;
using GameStore.Core.Serveces.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers
{
    public class AppController : Controller
    {
        private readonly IGameService gameService;
        private readonly IStoreServices storeService;

        public AppController(IGameService _gameService, IStoreServices _storeServices)
        {
            this.gameService = _gameService;
            this.storeService = _storeServices;
        }

        public IActionResult Details(string id)
        {
            var game = gameService.Details(id);

            var userId = User.GetUserId();
            game.Sorting = gameService.GetOwnership(id, userId);

            return View(game);
        }

        public IActionResult Buy(string id)
		{
            var userId = User.GetUserId();
            var buy = this.storeService.Buy(id, userId);

			if (!buy)
			{
                BadRequest();
			}

           return RedirectToAction("All", "Collection");
		}
    }
}
