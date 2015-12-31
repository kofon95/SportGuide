using System.Linq;

namespace Dal.Repository
{
    internal class LoginSocialNetworkRepository : IRepository<LoginSocialNetwork, int>
    {
        SportGuideEntities _ctx;

        public LoginSocialNetworkRepository(SportGuideEntities _ctx)
        {
            this._ctx = _ctx;
        }

        public IQueryable<LoginSocialNetwork> GetAll()
        {
            return _ctx.LoginSocialNetwork;
        }

        public LoginSocialNetwork GetById(int id)
        {
            return _ctx.LoginSocialNetwork.Single(t => t.id == id);
        }

        public LoginSocialNetwork Save(LoginSocialNetwork entity)
        {
            var added = _ctx.LoginSocialNetwork.Add(entity);
            _ctx.SaveChanges();
            return added;
        }

        public void Update(LoginSocialNetwork entity)
        {
            var updating = _ctx.LoginSocialNetwork.Single(t => t.id == entity.id);
            updating.uid_sn = entity.uid_sn;
            updating.network_name = entity.network_name;

            _ctx.SaveChanges();
        }

        public void Delete(LoginSocialNetwork entity)
        {
            _ctx.LoginSocialNetwork.Remove(entity);
        }

        public void DeleteById(int id)
        {
            var entity = _ctx.LoginSocialNetwork.Single(t => t.id == id);
            Delete(entity);
        }
    }
}
