using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Dal.Repository
{
    internal class SqlRepository<T> : IRepository<T, int> where T : class
    {
        DbSet<T> _entities;
        DbContext _ctx;

        public SqlRepository(DbSet<T> entities, DbContext ctx)
        {
            _ctx = ctx;
            _entities = entities;
        }

        public virtual void Delete(T entity)
        {
            _entities.Remove(entity);
            _ctx.SaveChanges();
        }
        public virtual void Delete(IEnumerable<T> entities)
        {
            // Cannot delete many here!
            bool oldValidateOnSaveEnabled = _ctx.Configuration.ValidateOnSaveEnabled;
            try
            {
                _ctx.Configuration.ValidateOnSaveEnabled = false;

                var entityList = entities.ToList();
                for (int i = 0; i < entityList.Count; ++i)
                {
                    _ctx.Entry(entityList[i]).State = EntityState.Deleted;
                    //_entities.Remove(entity);
                }
                //_entities.RemoveRange(entities);
                _ctx.SaveChanges();
            }
            finally
            {
                _ctx.Configuration.ValidateOnSaveEnabled = true;
            }
        }

        public virtual void DeleteById(int id)
        {
            _entities.Remove(GetById(id));
            _ctx.SaveChanges();
        }

        public virtual IQueryable<T> GetAll()
        {
            return _entities;
        }
        public virtual IOrderedQueryable<T> GetOrdered()
        {
            return _entities;
        }

        public virtual T GetById(int id)
        {
            return _entities.Find(id);
        }

        public virtual T Save(T entity)
        {
            var added = _entities.Add(entity);
            _ctx.SaveChanges();
            return added;
        }
        public virtual T[] Save(IEnumerable<T> entity)
        {
            var added = new List<T>();
            foreach (var item in entity)
                added.Add(_entities.Add(item));
            _ctx.SaveChanges();
            return added.ToArray();
        }

        public virtual void Update(T entity)
        {
            if (entity == null)
                new ArgumentNullException(nameof(entity), "Argument cannot be null");

            _ctx.SaveChanges();
        }
    }
}
