using System.Linq;

namespace Dal.Repository
{
    class TrainerRepository : IRepository<Trainer, int>
    {
        SportGuideEntities _ctx;
        
        public TrainerRepository(SportGuideEntities _ctx)
        {
            this._ctx = _ctx;
        }

        public IQueryable<Trainer> GetAll()
        {
            return _ctx.Trainer;
        }

        public Trainer GetById(int id)
        {
            return _ctx.Trainer.Single(t => t.id == id);
        }

        public Trainer Save(Trainer entity)
        {
            var added = _ctx.Trainer.Add(entity);
            _ctx.SaveChanges();
            return added;
        }

        public void Update(Trainer entity)
        {
            var updating = _ctx.Trainer.Single(t => t.id == entity.id);
            updating.birthday = entity.birthday;
            updating.name = entity.name;
            updating.phone_number = entity.phone_number;
            updating.photo_src = entity.photo_src;

            _ctx.SaveChanges();
        }

        public void Delete(Trainer entity)
        {
            _ctx.Trainer.Remove(entity);
        }

        public void DeleteById(int id)
        {
            var entity = _ctx.Trainer.Single(t => t.id == id);
            Delete(entity);
        }
    }
}
