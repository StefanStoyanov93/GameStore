using GameStore.Core.Models;
using GameStore.Core.Models.Manager;
using GameStore.Core.Serveces;
using GameStore.Core.Serveces.Contracts;
using GameStore.Data.Data;
using GameStore.Data.Models;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Tests
{
    public class StoreServiceTest
    {
        private ServiceProvider serviceProvider;
        private InMemoryDbContext dbContext;

        [SetUp]
        public async Task Setup()
        {
            dbContext = new InMemoryDbContext();
            var serviceCollection = new ServiceCollection();

            serviceProvider = serviceCollection
                .AddSingleton(sp => dbContext.CreateContext())
                .AddSingleton<IStoreServices, StoreServices>()
                .BuildServiceProvider();

            var repo = serviceProvider.GetService<GameStoreDbContext>();
            await SeedDbAsync(repo);
        }

        [Test]
        public void GameDoesNotExistWhenYouTryToBuyException()
        {
            var game = new Game()
            {
                Id = Guid.Parse("55cf1f0a-a6d7-4d6e-9c1d-4bed0ec274b6")
            };

            var user = new User
            {
                Id = "55cf1f0a - a6d7 - 4d6e-9c1d - 4bed0ec274b6"
            };

            var service = serviceProvider.GetService<IStoreServices>();


            Assert.CatchAsync<ArgumentException>(async () => await service.Buy(game.Id.ToString(), user.Id), "Game doesn't exist.");
        }

        [Test]
        public async Task TestThatGameWithThisNameAlreadyExist()
        {
            var gamename = "ElderRing";

            var service = serviceProvider.GetService<IStoreServices>();

            Assert.IsTrue(await service.NameExists(gamename));
        }


        [Test]
        public async Task TestThatGameWithThisNameAlreadyExistReturnFalse()
        {
            var gamename = "derere";

            var service = serviceProvider.GetService<IStoreServices>();

            Assert.IsFalse(await service.NameExists(gamename));
        }

        [Test]
        public async Task TestThatGetIndexGamesReturnTreeGames()
        {
            var service = serviceProvider.GetService<IStoreServices>();


            var games = await service.GetIndexGames();
            var count = games.ToList().Count;


            Assert.AreEqual(3, count);
        }

        [Test]
        public void TestThatGetGenresReturnRightCount()
        {
            var service = serviceProvider.GetService<IStoreServices>();


            var games = service.GetGenres();
            var count = games.ToList().Count;


            Assert.AreEqual(20, count);
        }

        [Test]
        public void TestAreSame()
        {
            var service = serviceProvider.GetService<IStoreServices>();
            var repo = serviceProvider.GetService<GameStoreDbContext>();

            var games = repo.Games
                .Skip((1 - 1) * 12)
                .Take(12)
                .Select(x => new BaseGameModel
                {
                    Id = x.Id.ToString(),
                    Name = x.Name,
                    Image = x.ImageUrl,
                })
                .ToList();
            var count = games.Count;
            var model = new GameServiceManagerModel()
            {
                Games = games,
                TotalGames = count
            };


            Assert.AreEqual(model, service.All(null, 1, 12));
        }


        [Test]
        public void GameDoesNotExistWhenYouTryAddToWishlistException()
        {
            var gameid = "adsvdfsgsfd";
            var userId = "55cf1f0a - a6d7 - 4d6e-9c1d - 4bed0ec274b6";

            var service = serviceProvider.GetService<IStoreServices>();


            Assert.CatchAsync<ArgumentException>(async () => await service.WishlistAdd(gameid, userId), "Game doesn't exist.");
        }

        [Test]
        public void GameDoesNotExistWhenYouTryToRemoveFromWishlistException()
        {
            var gameid = "adsvdfsgsfd";
            var userId = "55cf1f0a - a6d7 - 4d6e-9c1d - 4bed0ec274b6";

            var service = serviceProvider.GetService<IStoreServices>();


            Assert.CatchAsync<ArgumentException>(async () => await service.WishlistRemove(gameid, userId), "Game doesn't exist.");
        }


        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }

        private async Task SeedDbAsync(GameStoreDbContext repo)
        {
            //var genre = new Genre
            //{
            //    Id = 1,
            //    GenreName = "Action"
            //};

            var game = new Game()
            {
                Id = new Guid("9940548e-dadc-405f-83f9-57431685cf5d"),
                Name = "ElderRing",
                Publisher = "Vasko",
                Developer = "Pesho",
                Description = "asddssa",
                ImageUrl = "dfdsafa",
                Price = 12.5m,
                ReleaseDate = DateTime.Now
            };

            var gameTwo = new Game()
            {
                Id = new Guid("7DC67E2F-06ED-4234-9B7C-4332ECC14F1E"),
                Name = "GodOfWar",
                Publisher = "meme",
                Developer = "meme",
                Description = "dwdfethytjkiyluo;",
                ImageUrl = "edrgtjukuk",
                Price = 12.5m,
                ReleaseDate = DateTime.Now
            };

            var gameThree = new Game()
            {
                Id = new Guid("31E4F1F2-0117-437A-AB80-D6E3AE702F1B"),
                Name = "Halo",
                Publisher = "meme",
                Developer = "meme",
                Description = "sfefhtujkluo;;",
                ImageUrl = "edrdsfgfhghgtjukuk",
                Price = 18.5m,
                ReleaseDate = DateTime.Now
            };

            //var genregame = new GameGenre
            //{
            //    GameId = game.Id,
            //    GenreId = genre.Id
            //};

            var user = new User()
            {
                Id = "55cf1f0a - a6d7 - 4d6e-9c1d - 4bed0ec274b6"
            };

            //await repo.AddAsync(genre);
            await repo.AddAsync(game);
            await repo.AddAsync(gameTwo);
            await repo.AddAsync(gameThree);
            //await repo.AddAsync(genregame);
            await repo.SaveChangesAsync();
        }
    }
}
