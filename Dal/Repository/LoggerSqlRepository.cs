using NLog;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Dal.Repository
{
    class LoggerSqlRepository<T> : SqlRepository<T> where T : class
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public LoggerSqlRepository(DbSet<T> entities, DbContext ctx) : base(entities, ctx)
        {
        }

        public override IQueryable<T> GetAll()
        {
            _logger.Trace("GetAll");
            try
            {
                return base.GetAll();
            }
            catch (Exception e)
            {
                _logger.Error(e, "Error on GetAll");
                throw;
            }
        }
        public override T GetById(int id)
        {
            _logger.Trace(nameof(GetById), id);
            try
            {
                return base.GetById(id);
            }
            catch (Exception e)
            {
                _logger.Error(e, "Error on getting by id: " + id);
                throw;
            }
        }

        public override T Save(T entity)
        {
            _logger.Trace("Save", entity);
            try
            {
                return base.Save(entity);
            }
            catch (Exception e)
            {
                _logger.Error(e, "Error on Save", entity);
                throw;
            }
        }
        public override T[] Save(IEnumerable<T> entity)
        {
            _logger.Trace("Save[]", entity);
            try
            {
                return base.Save(entity);
            }
            catch (Exception e)
            {
                _logger.Error(e, "Error on Save", entity);
                throw;
            }
        }

        public override void Update(T entity)
        {
            _logger.Trace("Update", entity);
            try
            {
                base.Update(entity);
            }
            catch (Exception e)
            {
                _logger.Error(e, "Error on updating", entity);
                throw;
            }
        }

        public override void Delete(T entity)
        {
            _logger.Trace("Delete", entity);
            try
            {
                base.Delete(entity);
            }
            catch (Exception e)
            {
                _logger.Error(e, "Delete", entity);
                throw;
            }
        }
        public override void DeleteById(int id)
        {
            _logger.Trace("DeleteById", id);
            try
            {
                base.DeleteById(id);
            }
            catch (Exception e)
            {
                _logger.Error(e, "DeleteById", id);
                throw;
            }
        }
    }
}

