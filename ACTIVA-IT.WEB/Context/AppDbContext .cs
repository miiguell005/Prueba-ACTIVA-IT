using ACTIVA_IT.WEB.Context.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACTIVA_IT.WEB.Context
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options)
                : base(options)
        {

        }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Cancion> Cancion { get; set; }
        public DbSet<Album> Album { get; set; }
    }
}
