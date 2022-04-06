using GameStore.Core.Models;
using GameStore.Core.Models.Manager;
using GameStore.Core.Serveces.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Areas.Manager.Controllers
{
    public class StoreController : ManagerController
    {
        private readonly IStoreServices storeService;
        private readonly IGameService gameService;

        public StoreController(IStoreServices _storeService, IGameService _gameService)
        {
            this.storeService = _storeService;  
            this.gameService = _gameService;
        }


        [HttpPost]
        public IActionResult AddGame(GameFormModel game)
        {

            if (storeService.NameExists(game.Name))
            {
                ModelState.AddModelError(nameof(game.Name), "Game with this name already exist in the database.");
            }

            if (!ModelState.IsValid)
            {
                game.Genres = storeService.GetGenres();

                return View(game);
            }

            storeService.Create(game.Name, 
                game.Developer, 
                game.Publisher, 
                game.Description, 
                game.Price,
                game.ImageUrl,
                game.ReleaseDate,
                game.GenreIds.ToList());


            return RedirectToAction("All");
        }

        public IActionResult All(AllGamesManagerModel model)
        {
            var result = storeService.All(model.SearchTerm, model.IndexPage, AllGamesManagerModel.gamesPerPage);

            model.TotalGames = result.TotalGames;
            model.Games = result.Games;
 

            return View(model);
        }

        public IActionResult AddGame()
        {
            return View(new GameFormModel
            {
                Genres = storeService.GetGenres()
            });
        }

        public IActionResult Edit(string id)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Remove(string id)
        {
            var game = gameService.GetGameForDelete(id);

            return View(game);
        }

        [HttpPost, ActionName(nameof(Remove))]
        public IActionResult RemoveConfirmed(string id)
        {
            bool IsDeleated = gameService.Delete(id);

            if (!IsDeleated)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(All));
        }
    }
}
