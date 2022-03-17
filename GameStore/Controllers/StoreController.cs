using GameStore.Data.Data;
using GameStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers
{
    public class StoreController : Controller
    {
        private readonly GameStoreDbContext data;

        public StoreController(GameStoreDbContext context)
        {
            data = context;
        }


        [HttpGet]
        public IActionResult Games()
        {
            var games = data.Games
                .Select(g => new StoreGamesViewModel
                {
                    ImageUrl = g.ImageUrl,
                    Name = g.Name,
                    Price = g.Price
                })
                .ToList();

            return View(games);
        }

        [Authorize]
        public IActionResult Wishlist()
        {
            return View();
        }
    }
}
