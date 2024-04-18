using CinemaPark.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CinemaPark.Core.Repositories;

public interface IGenreRepository : IGenericRepository<Genre>
{
    Task<Genre> GetByIdAsync(int id);
    Task<bool>IsExist(Expression<Func<Genre,bool>>expression);
}
