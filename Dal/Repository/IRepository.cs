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
        #endregion

        #region Delete
        void Delete(TEntity entity);
        void DeleteById(TId id);
        #endregion
    }
}
