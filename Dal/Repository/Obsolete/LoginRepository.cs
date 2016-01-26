using Dal.Repository;
using System.Linq;

namespace Dal.Repository
{
    internal class LoginRepository : IRepository<Login, int>
    {
        private readonly SportGuideEntities _ctx;

        public LoginRepository(SportGuideEntities ctx)
        {
            _ctx = ctx;
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
            var added = _ctx.Login.Add(entity);
            _ctx.SaveChanges();
            return added;
        }

        public void Update(Login entity)
        {
            var updating = _ctx.Login.Single(t => t.id == entity.id);
            updating.email = entity.email;
            updating.password = entity.password;

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