using System.Linq;

namespace Dal.Repository
{
    internal class RepublicRepository : IRepository<Republic, int>
    {
        private readonly SportGuideEntities _ctx;

        public RepublicRepository(SportGuideEntities ctx)
        {
            _ctx = ctx;
        }

        public IQueryable<Republic> GetAll()
        {
            return _ctx.Republic;
        }

        public Republic GetById(int id)
        {
            return _ctx.Republic.Single(t => t.id == id);
        }

        public Republic Save(Republic entity)
        {
            var added = _ctx.Republic.Add(entity);
            _ctx.SaveChanges();
            return added;
        }

        public void Update(Republic entity)
        {
            var updating = _ctx.Republic.Single(t => t.id == entity.id);
            updating.name = entity.name;
            updating.country_id = entity.country_id;

            _ctx.SaveChanges();
        }

        public void Delete(Republic entity)
        {
            _ctx.Republic.Remove(entity);
        }

        public void DeleteById(int id)
        {
            var entity = _ctx.Republic.Single(t => t.id == id);
            Delete(entity);
        }
    }
}