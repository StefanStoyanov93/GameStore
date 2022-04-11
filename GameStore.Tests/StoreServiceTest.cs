using GameStore.Core.Serveces;
using GameStore.Core.Serveces.Contracts;
using GameStore.Data.Data;
using GameStore.Data.Models;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
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
        public void GameDoesExistWhenYouTryToBuyException()
        {
            var gameid = "9940548e-dadc-405f-83f9-57431685cf5d";

            var user = new User
            {
                Id = "55cf1f0a-a6d7-4d6e-9c1d-4bed0ec274b6"
            };

            var service = serviceProvider.GetService<IStoreServices>();


            Assert.DoesNotThrowAsync(async () => await service.Buy(gameid, user.Id));
        }

        [Test]
        public async Task TestThatGameWithThisNameAlreadyExist()
        {
            var gamename = "ElderRing";

            var service = serviceProvider.GetService<IStoreServices>();

            Assert.IsTrue(await service.NameExists(gamename));
        }

        [Test]
        public void GameDoesNotExistWhenYouTryAddToWishlistException()
        {
            var gameid = "adsvdfsgsfd";
            var user = new User
            {
                Id = "55cf1f0a - a6d7 - 4d6e-9c1d - 4bed0ec274b6"
            };

            var service = serviceProvider.GetService<IStoreServices>();


            Assert.CatchAsync<ArgumentException>(async () => await service.Buy(gameid, user.Id), "Game doesn't exist.");
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
            Guid newGuid = Guid.Parse("9940548e-dadc-405f-83f9-57431685cf5d");

            var game = new Game()
            {
                Id = newGuid,
                Name = "ElderRing",
                Publisher = "Vasko",
                Developer = "Pesho",
                Description = "asddssa",
                ImageUrl = "dfdsafa",
                Price = 12.5m,
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
            //await repo.AddAsync(genregame);
            await repo.SaveChangesAsync();
        }
    }
}
