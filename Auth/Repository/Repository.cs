using Microsoft.EntityFrameworkCore;
using Auth.Repository.IRepository;
using System.Linq.Expressions;
using Auth.Data;

namespace Auth.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _db;
        public DbSet<T> Dbset;
        public Repository(AppDbContext db)
        {
            _db = db;
            this.Dbset = _db.Set<T>();
        }
        public void Add(T entity)
        {
            Dbset.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = Dbset;
            query = query.Where(filter);
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = Dbset;
            return query.ToList();
        }

        public void Remove(T entity)
        {
            Dbset.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            Dbset.RemoveRange(entity);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(T entity)
        {
            Dbset.Update(entity);
        }
    }
}
