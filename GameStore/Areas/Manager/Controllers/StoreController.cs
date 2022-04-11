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
        public async Task<IActionResult> AddGame(GameFormModel game)
        {

            if (await storeService.NameExists(game.Name))
            {
                ModelState.AddModelError(nameof(game.Name), "Game with this name already exist in the database.");
            }

            if (!ModelState.IsValid)
            {
                game.Genres = storeService.GetGenres();

                return View(game);
            }

            await gameService.Create(game.Name, 
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

        public async Task<IActionResult> Edit(string id)
        {
            var model = await gameService.Details(id);

            if (model == null)
            {
                return BadRequest();
            }

            var genres = storeService.GetGenres();

            var genresIds = model.Genres.Select(x => x.Id).ToList();

            var game = new GameFormModel
            {
                Name = model.Name,
                Developer = model.Developer,
                Description = model.Description,
                Publisher = model.Publisher,
                ReleaseDate = model.ReleaseDate,
                Price = model.Price,
                ImageUrl = model.ImageUrl,
                GenreIds = genresIds,
                Genres = genres
            };

            return View(game);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, GameFormModel game)
        {
            if (!ModelState.IsValid)
            {
                game.Genres = storeService.GetGenres();

                return View(game);
            }

            var edited = await gameService.Edit(id, 
                game.Name, 
                game.Developer, 
                game.Publisher, 
                game.Description, 
                game.ImageUrl, 
                game.Price,
                game.ReleaseDate,
                game.GenreIds);

            if (!edited)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Remove(string id)
        {
            var game = await gameService.GetGameForDelete(id);

            return View(game);
        }

        [HttpPost, ActionName(nameof(Remove))]
        public async Task<IActionResult> RemoveConfirmed(string id)
        {
            bool IsDeleated = await gameService.Delete(id);

            if (!IsDeleated)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(All));
        }
    }
}
