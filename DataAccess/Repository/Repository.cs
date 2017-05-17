using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class, IEntity 
    {
        private readonly ApplicationDbContext _context;
        private DbSet<T> _entities;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<T> GetAll()
        {
            return Query.ToList();
        }

        public async Task<T> GetAsync(int id)
        {
            return await Query.SingleOrDefaultAsync(x => x.Id == id);
        }

        public T GetById(int id)
        {
            return Query.FirstOrDefault(x => x.Id == id);
        }

        public void Create(T item)
        {
            Entities.Add(item);
        }

        public void Update(T item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(T item)
        {
            Entities.Remove(item);
        }

        protected IDbSet<T> Entities
        {
            get { return _entities ?? (_entities = _context.Set<T>()); }
        }

        public IQueryable<T> Query
        {
            get { return Entities; }
        }

    }
}
