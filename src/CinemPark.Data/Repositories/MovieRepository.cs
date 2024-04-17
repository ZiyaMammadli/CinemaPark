using CinemaPark.Core.Entities;
using CinemaPark.Core.Repositories;
using CinemaPark.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaPark.Data.Repositories
{
    public class MovieRepository : GenericRepository<Movie>, IMovieRepository
    {
        public MovieRepository(CinemaDbContext context) : base(context)
        {
        }
    }
}
