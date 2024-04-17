using CinemaPark.Core.Repositories;
using CinemaPark.Data.Contexts;
using CinemaPark.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaPark.Data;

public static class ServiceRegistration
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IGenreRepository, GenreRepository>();
        services.AddScoped<IMovieRepository, MovieRepository>();
    }
}
