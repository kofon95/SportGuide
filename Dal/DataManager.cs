using Dal.Repository;

namespace Dal
{
    public class DataManager
    {
        public readonly SportGuideEntities _ctx;
        public bool ProxyCreationEnabled
        {
            get { return _ctx.Configuration.ProxyCreationEnabled; }
            set { _ctx.Configuration.ProxyCreationEnabled = value; }
        }

        public DataManager()
        {
            _ctx = new SportGuideEntities();
        }


        #region Lazy load

        private IRepository<Login, int> _login;
        public IRepository<Login, int> Login => _login ?? (_login = new LoggerSqlRepository<Login>(_ctx.Login, _ctx));

        private IRepository<User, int> _user;
        public IRepository<User, int> User => _user ?? (_user = new LoggerSqlRepository<User>(_ctx.User, _ctx));

        private IRepository<LoginSocialNetwork, int> _loginSocialNetwork;
        public IRepository<LoginSocialNetwork, int> LoginSocialNetwork => _loginSocialNetwork ?? (_loginSocialNetwork = new LoggerSqlRepository<LoginSocialNetwork>(_ctx.LoginSocialNetwork, _ctx));

        private IRepository<City, int> _city;
        public IRepository<City, int> City => _city ?? (_city = new LoggerSqlRepository<City>(_ctx.City, _ctx));

        private IRepository<Republic, int> _republic;
        public IRepository<Republic, int> Republic => _republic ?? (_republic = new LoggerSqlRepository<Republic>(_ctx.Republic, _ctx));

        private IRepository<Workout, int> _workout;
        public IRepository<Workout, int> Workout => _workout ?? (_workout = new LoggerSqlRepository<Workout>(_ctx.Workout, _ctx));

        private IRepository<Hall, int> _hall;
        public IRepository<Hall, int> Hall => _hall ?? (_hall = new LoggerSqlRepository<Hall>(_ctx.Hall, _ctx));

        private IRepository<KindOfSport, int> _kindOfSport;
        public IRepository<KindOfSport, int> KindOfSport => _kindOfSport ?? (_kindOfSport = new LoggerSqlRepository<KindOfSport>(_ctx.KindOfSport, _ctx));

        private IRepository<Category, int> _category;
        public IRepository<Category, int> Category => _category ?? (_category = new LoggerSqlRepository<Category>(_ctx.Category, _ctx));

        private IRepository<PhoneOfHall, int> _phoneOfHall;
        public IRepository<PhoneOfHall, int> PhoneOfHall => _phoneOfHall ?? (_phoneOfHall = new LoggerSqlRepository<PhoneOfHall>(_ctx.PhoneOfHall, _ctx));

        private IRepository<Trainer, int> _trainer;
        public IRepository<Trainer, int> Trainer => _trainer ?? (_trainer = new LoggerSqlRepository<Trainer>(_ctx.Trainer, _ctx));

        private IRepository<HallYandexMapLocation, int> _hallYandexMapLocation;
        public IRepository<HallYandexMapLocation, int> HallYandexMapLocation => _hallYandexMapLocation ?? (_hallYandexMapLocation = new LoggerSqlRepository<HallYandexMapLocation>(_ctx.HallYandexMapLocation, _ctx));

        private IRepository<HallImages, int> _hallImages;
        public IRepository<HallImages, int> HallImages => _hallImages ?? (_hallImages = new LoggerSqlRepository<HallImages>(_ctx.HallImages, _ctx));

        #endregion
    }
}
