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

        [HttpGet]
        public IActionResult Browse(AllGamesQueryModel query)
        {
            var modelResult = storeService.Browse(query.SearchTerm,
                query.GenreId,
                query.Sorting,
                query.IndexPage, 
                AllGamesQueryModel.gamesPerPage );

            query.TotalGames = modelResult.TotalGames;
            query.Games = modelResult.Games;
            query.GamesExist = modelResult.GameExist;

            query.Genres = storeService.GetGenres();


            return View(query);
        }

        [Authorize]
        public IActionResult Wishlist(List<StoreGamesViewModel> model)
        {
            return View(model);
        }
    }
}
