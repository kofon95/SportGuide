using System.Linq;

namespace Dal.Repository
{
    internal class CityRepository : IRepository<City, int>
    {
        private SportGuideEntities _ctx;

        public CityRepository(SportGuideEntities ctx)
        {
            _ctx = ctx;
        }

        public IQueryable<City> GetAll()
        {
            return _ctx.City;
        }

        public City GetById(int id)
        {
            return _ctx.City.Single(t => t.id == id);
        }

        public City Save(City entity)
        {
            var added = _ctx.City.Add(entity);
            _ctx.SaveChanges();
            return added;
        }

        public void Update(City entity)
        {
            var updating = _ctx.City.Single(t => t.id == entity.id);
            updating.name = entity.name;
            updating.republic_id = entity.republic_id;

            _ctx.SaveChanges();
        }

        public void Delete(City entity)
        {
            _ctx.City.Remove(entity);
        }

        public void DeleteById(int id)
        {
            var entity = _ctx.City.Single(t => t.id == id);
            Delete(entity);
        }
    }
}