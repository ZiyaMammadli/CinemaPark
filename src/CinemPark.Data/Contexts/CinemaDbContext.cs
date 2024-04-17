using CinemaPark.Core.Entities;
using CinemaPark.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CinemaPark.Data.Contexts;

public class CinemaDbContext:DbContext
{
    public CinemaDbContext(DbContextOptions<CinemaDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GenreConfiguration).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Genre> genres { get; set; }
    public DbSet<Movie> movies { get; set; }
}
