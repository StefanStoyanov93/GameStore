using GameStore.Core.Serveces.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers
{
    public class AppController : Controller
    {
        private readonly IGameService gameService;

        public AppController(IGameService _gameService)
        {
            this.gameService = _gameService;
        }

        public IActionResult Details(string id)
        {
            var game = gameService.Details(id);

            return View(game);
        }
    }
}
