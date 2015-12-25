using System.Linq;

namespace Dal.Repository
{
    class HallYandexMapLocationRepository : IRepository<HallYandexMapLocation, int>
    {
        SportGuideEntities _ctx;

        public HallYandexMapLocationRepository(SportGuideEntities _ctx)
        {
            this._ctx = _ctx;
        }

        public IQueryable<HallYandexMapLocation> GetAll()
        {
            return _ctx.HallYandexMapLocation;
        }

        public HallYandexMapLocation GetById(int id)
        {
            return _ctx.HallYandexMapLocation.Single(t => t.id == id);
        }

        public HallYandexMapLocation Save(HallYandexMapLocation entity)
        {
            var added = _ctx.HallYandexMapLocation.Add(entity);
            _ctx.SaveChanges();
            return added;
        }

        public void Update(HallYandexMapLocation entity)
        {
            var updating = _ctx.HallYandexMapLocation.Single(t => t.id == entity.id);
            updating.latitude = entity.latitude;
            updating.longitude = entity.longitude;

            _ctx.SaveChanges();
        }

        public void Delete(HallYandexMapLocation entity)
        {
            _ctx.HallYandexMapLocation.Remove(entity);
        }

        public void DeleteById(int id)
        {
            var entity = _ctx.HallYandexMapLocation.Single(t => t.id == id);
            Delete(entity);
        }
    }
}
