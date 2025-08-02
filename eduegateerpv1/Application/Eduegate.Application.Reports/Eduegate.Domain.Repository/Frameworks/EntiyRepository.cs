using Eduegate.Domain.Entity.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Repository.Frameworks
{
    public class EntiyRepository<T,TContext> where T : class
                                             where TContext : DbContext
    {
        private readonly TContext context;
        private DbSet<T> entities;

        public EntiyRepository(TContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }

        public long? GetMaxID(Func<T, long?> expression) 
        {
            return this.entities.Max(expression);
        }

        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return this.entities.Where(predicate).AsEnumerable();
        }

        public T GetById(object id)
        {
            return this.entities.Find(id);
        }

        public T Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            entities.Add(entity);
            context.SaveChanges();
            return entity;
        }

        public T Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            entities.Add(entity);
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
            return entity;
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            entities.Remove(entity);
            context.SaveChanges();
        }

        public bool Delete(Expression<Func<T, bool>> predicate)
        {
            entities.RemoveRange(this.entities.Where(predicate));
            context.SaveChanges();
            return true;
        }

    }
}
