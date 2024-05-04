using FoodTracker.DataAccess.Data;
using FoodTracker.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace FoodTracker.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        //public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false)
        //{
        //    IQueryable<T> query;
        //    if (tracked)
        //    {
        //        query = dbSet;
        //    }
        //    else
        //    {
        //        query = dbSet.AsNoTracking();
        //    }

        //    query = query.Where(filter);
        //    if (!string.IsNullOrEmpty(includeProperties))
        //    {
        //        foreach (var prop in includeProperties.Split(",", StringSplitOptions.RemoveEmptyEntries))
        //        {
        //            query = query.Include(prop).;
        //        }
        //    }
        //    return query.FirstOrDefault();

        //}
        public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false)
        {
            //IQueryable<T> query;
            //if (tracked)
            //{
            //    query = dbSet;
            //}
            //else
            //{
            //    query = dbSet.AsNoTracking();
            //}

            //query = query.Where(filter);
            //if (!string.IsNullOrEmpty(includeProperties))
            //{
            //    foreach (var prop in includeProperties.Split(",", StringSplitOptions.RemoveEmptyEntries))
            //    {
            //        query = query.Include(prop).;
            //    }
            //}
            //return query.FirstOrDefault();
            
            if (includeProperties != null)
            {
                return Get(filter, [includeProperties]);
            }
            return Get(filter, []);
            //return Get(filter, [includeProperties]);

        }
        public T Get(Expression<Func<T, bool>> filter, params string[] includeProperties)
        {
            IQueryable<T> query;

                query = dbSet.AsNoTracking();
      

            query = query.Where(filter);
            if (!includeProperties.IsNullOrEmpty())
            {
                foreach (var prop in includeProperties)
                {
                    query = query.Include(prop);
                }
            }
            return query.FirstOrDefault();

        }

        //public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        //{
        //    IQueryable<T> query = dbSet;
        //    if (filter != null)
        //    {
        //        query = query.Where(filter);
        //    }
        //    if (!string.IsNullOrEmpty(includeProperties))
        //    {
        //        foreach (var prop in includeProperties.Split(",", StringSplitOptions.RemoveEmptyEntries))
        //        {
        //            query = query.Include(prop);
        //        }
        //    }
        //    return query.ToList();
        //}

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            //IQueryable<T> query = dbSet;
            //if (filter != null)
            //{
            //    query = query.Where(filter);
            //}
            //if (!string.IsNullOrEmpty(includeProperties))
            //{
            //    foreach (var prop in includeProperties.Split(",", StringSplitOptions.RemoveEmptyEntries))
            //    {
            //        query = query.Include(prop);
            //    }
            //}
            //return query.ToList();
            if (includeProperties != null)
            {
                return GetAll(filter, [includeProperties]);
            }
            var a = new string[0];
            return GetAll(filter, Array.Empty<string>());
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, params string[] includeProperties)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (!includeProperties.IsNullOrEmpty())
            {
                foreach (var prop in includeProperties)
                {
                    query = query.Include(prop);
                }
            }
            return query.ToList();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }
    }
}
