using System.Linq;

namespace Dal.Repository
{
    internal class WorkoutRepository : IRepository<Workout, int>
    {
        readonly SportGuideEntities _ctx;
        public WorkoutRepository(SportGuideEntities ctx)
        {
            _ctx = ctx;
        }

        public IQueryable<Workout> GetAll()
        {
            return _ctx.Workout;
        }

        public Workout GetById(int id)
        {
            return _ctx.Workout.Single(t => t.id== id);
        }

        public Workout Save(Workout entity)
        {
            var added = _ctx.Workout.Add(entity);
            _ctx.SaveChanges();
            return added;
        }

        public void Update(Workout entity)
        {
            var updating = _ctx.Workout.Single(t => t.id == entity.id);
            updating.hall_id = entity.hall_id;
            updating.info = entity.info;
            updating.kind_of_sport_id = entity.kind_of_sport_id;
            updating.min_age = entity.min_age;
            updating.max_age = entity.max_age;
            updating.paiment_for_month = entity.paiment_for_month;
            updating.time = entity.time;
            updating.trainer_id = entity.trainer_id;

            updating.mon = entity.mon;
            updating.tue = entity.tue;
            updating.wed = entity.wed;
            updating.thu = entity.thu;
            updating.fri = entity.fri;
            updating.sat = entity.sat;
            updating.sun = entity.sun;

            _ctx.SaveChanges();
        }

        public void Delete(Workout entity)
        {
            _ctx.Workout.Remove(entity);
        }

        public void DeleteById(int id)
        {
            var entity = _ctx.Workout.Single(t => t.id == id);
            Delete(entity);
        }
    }
}
