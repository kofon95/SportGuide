using Dal.Repository;
using System.Linq;

namespace Dal.Repository
{
    internal class LoginRepository : IRepository<Login, int>
    {
        private SportGuideEntities _ctx;
        private const string _defaultRole = "User";

        public LoginRepository(SportGuideEntities _ctx)
        {
            this._ctx = _ctx;
        }

        public IQueryable<Login> GetAll()
        {
            return _ctx.Login;
        }

        public Login GetById(int id)
        {
            return _ctx.Login.Single(t => t.id == id);
        }

        public Login Save(Login entity)
        {
            if (entity.role == null) entity.role = _defaultRole;
            var added = _ctx.Login.Add(entity);
            _ctx.SaveChanges();
            return added;
        }

        public void Update(Login entity)
        {
            var updating = _ctx.Login.Single(t => t.id == entity.id);
            updating.email = entity.email;
            updating.password = entity.password;
            updating.role = entity.role;

            _ctx.SaveChanges();
        }

        public void Delete(Login entity)
        {
            _ctx.Login.Remove(entity);
        }

        public void DeleteById(int id)
        {
            var entity = _ctx.Login.Single(t => t.id == id);
            Delete(entity);
        }
    }
}