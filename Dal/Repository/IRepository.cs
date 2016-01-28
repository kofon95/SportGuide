using System.Collections.Generic;
using System.Linq;

namespace Dal.Repository
{
    public interface IRepository<TEntity, TId>
    {
        #region SELECT

        IQueryable<TEntity> GetAll();
        TEntity GetById(TId id);

        #endregion

        #region UPDATE
        void Update(TEntity entity);
        #endregion

        #region INSERT
        TEntity Save(TEntity entity);
        TEntity[] Save(IEnumerable<TEntity> entity);
        #endregion

        #region DELETE
        void Delete(TEntity entity);
        void Delete(IEnumerable<TEntity> entities);
        void DeleteById(TId id);
        #endregion
    }
}
