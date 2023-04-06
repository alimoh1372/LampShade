using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using _0_Framework.Domain;
using Microsoft.EntityFrameworkCore;

namespace _0_Framework.Infrastructure
{
    public class BaseRepository<TKey,T>:IBaseRepository<TKey,T> where T:class
    {
        private readonly DbContext _context;

        public BaseRepository(DbContext context)
        {
            _context = context;
        }

        public void Create(T entity)
        {
            _context.Add(entity);
        }

        public T Get(TKey id)
        {
            return _context.Set<T>().Find(id);
        }

        public List<T> Get()
        {
            return _context.Set<T>().ToList();
        }

        public bool IsExists(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Any(expression);
        }

        public void SaveChanges()
        {
            int result = 0;
          result=  _context.SaveChanges();
          Console.WriteLine(result);
        }
    }
}