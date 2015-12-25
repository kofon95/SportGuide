using System.Linq;

namespace Dal.Repository
{
    class PhoneOfHallRepository : IRepository<PhoneOfHall, int>
    {
        SportGuideEntities _ctx;

        public PhoneOfHallRepository(SportGuideEntities _ctx)
        {
            this._ctx = _ctx;
        }

        public IQueryable<PhoneOfHall> GetAll()
        {
            return _ctx.PhoneOfHall;
        }

        public PhoneOfHall GetById(int id)
        {
            return _ctx.PhoneOfHall.Single(t => t.id == id);
        }

        public PhoneOfHall Save(PhoneOfHall entity)
        {
            var added = _ctx.PhoneOfHall.Add(entity);
            _ctx.SaveChanges();
            return added;
        }

        public void Update(PhoneOfHall entity)
        {
            var updating = _ctx.PhoneOfHall.Single(t => t.id == entity.id);
            updating.hall_id = entity.hall_id;
            updating.phone_number = entity.phone_number;

            _ctx.SaveChanges();
        }

        public void Delete(PhoneOfHall entity)
        {
            _ctx.PhoneOfHall.Remove(entity);
        }

        public void DeleteById(int id)
        {
            var entity = _ctx.PhoneOfHall.Single(t => t.id == id);
            Delete(entity);
        }
    }
}
