using System.Linq;

namespace Dal.Repository
{
    class HallRepository : IRepository<Hall, int>
    {
        SportGuideEntities _ctx;

        public HallRepository(SportGuideEntities _ctx)
        {
            this._ctx = _ctx;
        }

        public IQueryable<Hall> GetAll()
        {
            return _ctx.Hall;
        }

        public Hall GetById(int id)
        {
            return _ctx.Hall.Single(t => t.id == id);
        }

        public Hall Save(Hall entity)
        {
            var added = _ctx.Hall.Add(entity);
            _ctx.SaveChanges();
            return added;
        }

        public void Update(Hall entity)
        {
            var updating = _ctx.Hall.Single(t => t.id == entity.id);
            updating.address = entity.address;
            updating.description = entity.description;
            updating.hall_name = entity.hall_name;

            _ctx.SaveChanges();
        }

        public void Delete(Hall entity)
        {
            _ctx.Hall.Remove(entity);
        }

        public void DeleteById(int id)
        {
            var entity = _ctx.Hall.Single(t => t.id == id);
            Delete(entity);
        }
    }
}
