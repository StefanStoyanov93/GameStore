using GameStore.Core.Serveces.Contracts;
using GameStore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GameStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStoreServices storeService;

        public HomeController(ILogger<HomeController> logger, IStoreServices _service)
        {
            _logger = logger;
            storeService = _service;
        }

        public async Task<IActionResult> Index()
        {
            var indexGames = await storeService.GetIndexGames();

            return View(indexGames);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
            => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}