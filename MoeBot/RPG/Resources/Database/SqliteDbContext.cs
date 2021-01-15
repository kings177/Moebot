using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using Microsoft.EntityFrameworkCore;


namespace RPG.Resources.Database
{
    class SqliteDbContext : DbContext
    {
        public DbSet<UserData> Data { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder Options)
        {
            Options.UseSqlite($"Data source=RPG.dllDatabase.sqlite");
        }
    }
}
