using GameStore.Core.Models;
using GameStore.Core.Serveces.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers
{
    public class CollectionController : Controller
    {
        private readonly IStoreServices storeService;

        public CollectionController(IStoreServices _storeService)
        {
            this.storeService = _storeService;
        }

        public IActionResult All(AllGamesQueryModel query)
        {
            var userId = User.GetUserId();
            var modelResult = storeService.Collection(query.SearchTerm,
                  query.GenreId,
                  query.Sorting,
                  query.IndexPage,
                  AllGamesQueryModel.gamesPerPage,
                  userId);

            query.TotalGames = modelResult.TotalGames;
            query.Games = modelResult.Games;
            query.GamesExist = modelResult.GameExist;

            query.Genres = storeService.GetGenres();


            return View(query);
        }
    }
}
