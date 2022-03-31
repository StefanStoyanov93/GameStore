using GameStore.Core.Models;
using GameStore.Core.Serveces.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Areas.Manager.Controllers
{
    public class StoreController : ManagerController
    {
        private readonly IStoreServices service;

        public StoreController(IStoreServices _service)
        {
            this.service = _service;
        }


        [HttpPost]
        public IActionResult AddGame(AddGameFormModel game)
        {
            if (!ModelState.IsValid)
            {
                game.Genres = service.GetGenres();

                return View(game);
            }


            service.Create(game.Name, 
                game.Developer, 
                game.Publisher, 
                game.Description, 
                game.Price,
                game.ImageUrl,
                game.ReleaseDate,
                game.GenreIds.ToList());


            return RedirectToAction("Index");
        }

        public IActionResult AddGame()
        {
            return View(new AddGameFormModel
            {
                Genres = service.GetGenres()
            });
        }
    }
}
