using Data.Context;
using Data.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Data.Repositories
{
    public sealed class EFGenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class
    {
        public EFGenericRepository()
        {
            _context = new ApplicationContext();
            _dbSet = _context.Set<TEntity>();
        }

        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public IEnumerable<TEntity> Get() =>
            _dbSet.AsNoTracking();

        public IEnumerable<TEntity> Get(Func<TEntity, bool> predicate) =>
            _dbSet.AsNoTracking()
            .Where(predicate)
            .ToList();

        public TEntity FindById(int id) => _dbSet.Find(id);

        public async void Create(TEntity item)
        {
            _dbSet.Add(item);

            await _context.SaveChangesAsync();
        }

        public async void Update(TEntity item)
        {
            _context.Entry(item).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public async void Remove(TEntity item)
        {
            _context.Entry(item).State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }
    }
}
