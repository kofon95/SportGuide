using Dal.Repository;
using System.Linq;

namespace Dal.Repository
{
    internal class UserRepository : IRepository<User, int>
    {
        private readonly SportGuideEntities _ctx;
        private const string _defaultRole = "User";

        public UserRepository(SportGuideEntities ctx)
        {
            _ctx = ctx;
        }

        public IQueryable<User> GetAll()
        {
            return _ctx.User;
        }

        public User GetById(int id)
        {
            return _ctx.User.Single(t => t.id == id);
        }

        public User Save(User entity)
        {
            if (entity.role == null) entity.role = _defaultRole;
            var added = _ctx.User.Add(entity);
            _ctx.SaveChanges();
            return added;
        }

        public void Update(User entity)
        {
            var updating = _ctx.User.Single(t => t.id == entity.id);
            updating.name = entity.name;
            updating.birthday = entity.birthday;
            updating.city_id = entity.city_id;
            updating.gender = entity.gender;
            updating.role = entity.role;
            updating.first_ip = entity.first_ip;

            _ctx.SaveChanges();
        }

        public void Delete(User entity)
        {
            _ctx.User.Remove(entity);
        }

        public void DeleteById(int id)
        {
            var entity = _ctx.User.Single(t => t.id == id);
            Delete(entity);
        }
    }
}