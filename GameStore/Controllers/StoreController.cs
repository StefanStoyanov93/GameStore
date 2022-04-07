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
        private readonly IStoreServices storeService;

        public StoreController(IStoreServices _service)
        {
            storeService = _service;
        }

        public IActionResult Browse([FromQuery] AllGamesQueryModel games)
        {
            var modelResult = storeService.BrowseAll(games.SearchTerm, 
                games.GenreName, 
                games.Sorting, 
                games.IndexPage, 
                AllGamesQueryModel.gamesPerPage );

            games.TotalGames = modelResult.TotalGames;
            games.Games = modelResult.Games;

            games.Genres = storeService.GetGenres();


            return View(games);
        }

        [Authorize]
        public IActionResult Wishlist(List<StoreGamesViewModel> model)
        {
            return View(model);
        }
    }
}
