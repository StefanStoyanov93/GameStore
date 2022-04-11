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

        public async Task<IActionResult> Details(string id)
        {
            var game = await gameService.Details(id);

            var userId = User.GetUserId();
            game.Sorting = await gameService.GetOwnership(id, userId);

            return View(game);
        }

        public async Task<IActionResult> Buy(string id)
		{
            var userId = User.GetUserId();

            try
            {
                await this.storeService.Buy(id, userId);
            }
            catch (ArgumentException ae)
            {
                return BadRequest(ae.Message);
            }

           return RedirectToAction("All", "Collection");
		}

        public async Task<IActionResult> AddToWishlist(string id)
		{
            var userId = User.GetUserId();

            try
            {
                await this.storeService.WishlistAdd(id, userId);
            }
            catch (ArgumentException ae)
            {
                return BadRequest(ae.Message);
            }

            return RedirectToAction("Wishlist", "Store");
        }

        public async Task<IActionResult> RemoveFromWishlist(string id)
		{
            var userId = User.GetUserId();
            
            try
            {
                await this.storeService.WishlistRemove(id, userId);
            }
            catch (ArgumentException ae)
            {
                return BadRequest(ae.Message);
            }

            return RedirectToAction("Browse", "Store");
        }

        public IActionResult Download()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "TheGame.txt");
            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            string fileName = "TheGame.txt";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
    }
}
