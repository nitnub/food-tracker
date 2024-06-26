﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace FoodTracker.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {


        T Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false);
        T Get(Expression<Func<T, bool>> filter, params string[]? includeProperties);
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, params string[]? includeProperties);
        void Add(T entity);

        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);

    }
}