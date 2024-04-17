using CinemaPark.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CinemaPark.Core.Repositories;

public interface IGenericRepository<TEntity> where TEntity : BaseEntity, new()
{
    DbSet<TEntity> Table {  get; }
    Task InsertAsync(TEntity entity);

    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression = null, params string[] includes);
    Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> expression = null, params string[] includes);
    Task<TEntity> GetByIdAsync(int id);
    void Delete(TEntity entity);
    Task<int> CommitAsync();
}
