using System.Linq;

namespace Dal.Repository
{
    internal class HallImagesRepository : IRepository<HallImages, int>
    {
        private readonly SportGuideEntities _ctx;

        public HallImagesRepository(SportGuideEntities ctx)
        {
            _ctx = ctx;
        }

        public IQueryable<HallImages> GetAll()
        {
            return _ctx.HallImages;
        }

        public HallImages GetById(int id)
        {
            return _ctx.HallImages.Single(t => t.id == id);
        }

        public HallImages Save(HallImages entity)
        {
            var added = _ctx.HallImages.Add(entity);
            _ctx.SaveChanges();
            return added;
        }

        public void Update(HallImages entity)
        {
            var updating = _ctx.HallImages.Single(t => t.id == entity.id);
            updating.hall_id = entity.hall_id;
            updating.src = entity.src;

            _ctx.SaveChanges();
        }

        public void Delete(HallImages entity)
        {
            _ctx.HallImages.Remove(entity);
        }

        public void DeleteById(int id)
        {
            var entity = _ctx.HallImages.Single(t => t.id == id);
            Delete(entity);
        }
    }
}