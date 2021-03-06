﻿using System.Linq;

namespace Dal.Repository
{
    internal class KindOfSportRepository :IRepository<KindOfSport, int>
    {
        readonly SportGuideEntities _ctx;

        public KindOfSportRepository(SportGuideEntities ctx)
        {
            _ctx = ctx;
        }

        public IQueryable<KindOfSport> GetAll()
        {
            return _ctx.KindOfSport;
        }

        public KindOfSport GetById(int id)
        {
            return _ctx.KindOfSport.Single(t => t.id == id);
        }

        public KindOfSport Save(KindOfSport entity)
        {
            var added = _ctx.KindOfSport.Add(entity);
            _ctx.SaveChanges();
            return added;
        }

        public void Update(KindOfSport entity)
        {
            var updating = _ctx.KindOfSport.Single(t => t.id == entity.id);
            updating.category_id = entity.category_id;
            updating.min_age = entity.min_age;
            updating.sport_name= entity.sport_name;

            _ctx.SaveChanges();
        }

        public void Delete(KindOfSport entity)
        {
            _ctx.KindOfSport.Remove(entity);
        }

        public void DeleteById(int id)
        {
            var entity = _ctx.KindOfSport.Single(t => t.id == id);
            Delete(entity);
        }
    }
}
