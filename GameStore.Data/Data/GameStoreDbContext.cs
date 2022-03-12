﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Data.Data
{
    public class GameStoreDbContext : IdentityDbContext
    {
        public GameStoreDbContext(DbContextOptions<GameStoreDbContext> options)
            : base(options)
        {
        }
    }
}