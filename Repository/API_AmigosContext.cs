using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Amigo;
using Domain.Estado;
using Domain.Pais;
using Microsoft.EntityFrameworkCore.Design;

namespace Respository
{
    public class API_AmigosContext : DbContext
    {
        public API_AmigosContext (DbContextOptions<API_AmigosContext> options)
            : base(options)
        {
        }

        public DbSet<Amigo> Amigos { get; set; }
        public DbSet<Pais> Pais { get; set; }
        public DbSet<Estado> Estado { get; set; }
        public DbSet<Amizade> Amizades { get; set; }
    }

    public class BloggingContextFactory : IDesignTimeDbContextFactory<API_AmigosContext>
    {
        public API_AmigosContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<API_AmigosContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=API_AmigosContext;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new API_AmigosContext(optionsBuilder.Options);
        }
    }
}
