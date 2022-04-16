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
        public void MethodBrowseDoesntReturnNullWHenIsUsed()
        {
            var service = serviceProvider.GetService<IStoreServices>();


            Assert.IsNotNull(service.Collection(null, 0, GameSorting.ReleaseDate, 1, 12, null, true));
        }

        [Test]
        public void MethodCollectionDoesntReturnNullWHenIsUsed()
        {
            var service = serviceProvider.GetService<IStoreServices>();


            Assert.IsNotNull(service.Browse(null,0,GameSorting.ReleaseDate, 1, 12));
        }

        [Test]
        public void MethodAllDoesntReturnNullWHenIsUsed()
        {
            var service = serviceProvider.GetService<IStoreServices>();


            Assert.IsNotNull(service.All(null, 1, 12));
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
                Id =  Guid.NewGuid(),
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
                Id = new Guid("b215fe9b-9112-4b8b-8a38-75bc6dbc6280"),
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

            await repo.AddAsync(game);
            await repo.AddAsync(gameTwo);
            await repo.AddAsync(gameThree);
            //await repo.AddAsync(genregame);
            await repo.SaveChangesAsync();
        }
    }
}
