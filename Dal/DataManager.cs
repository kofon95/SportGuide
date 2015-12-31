using Dal.Repository;
using System.Data.Entity;

namespace Dal
{
    public class DataManager
    {
        SportGuideEntities _ctx;

        public DataManager()
        {
            _ctx = new SportGuideEntities();
        }

        #region Lazy load

        private IRepository<Login, int> _login;
        public IRepository<Login, int> Login
        {
            get
            {
                if (_login == null)
                    _login = new LoginRepository(_ctx);
                return _login;
            }
        }

        private IRepository<User, int> _user;
        public IRepository<User, int> User
        {
            get
            {
                if (_user == null)
                    _user = new UserRepository(_ctx);
                return _user;
            }
        }

        private IRepository<LoginSocialNetwork, int> _loginSocialNetwork;
        public IRepository<LoginSocialNetwork, int> LoginSocialNetwork
        {
            get
            {
                if (_loginSocialNetwork == null)
                    _loginSocialNetwork = new LoginSocialNetworkRepository(_ctx);
                return _loginSocialNetwork;
            }
        }

        private IRepository<City, int> _city;
        public IRepository<City, int> City
        {
            get
            {
                if (_city == null)
                    _city = new CityRepository(_ctx);
                return _city;
            }
        }

        private IRepository<Republic, int> _republic;
        public IRepository<Republic, int> Republic
        {
            get
            {
                if (_republic == null)
                    _republic = new RepublicRepository(_ctx);
                return _republic;
            }
        }

        private IRepository<Workout, int> _workout;
        public IRepository<Workout, int> Workout
        {
            get
            {
                if (_workout == null)
                    _workout = new WorkoutRepository(_ctx);
                return _workout;
            }
        }

        private IRepository<Hall, int> _hall;
        public IRepository<Hall, int> Hall
        {
            get
            {
                if (_hall == null)
                    _hall = new HallRepository(_ctx);
                return _hall;
            }
        }

        private IRepository<KindOfSport, int> _kindOfSport;
        public IRepository<KindOfSport, int> KindOfSport
        {
            get
            {
                if (_kindOfSport == null)
                    _kindOfSport = new KindOfSportRepository(_ctx);
                return _kindOfSport;
            }
        }

        private IRepository<Category, int> _category;
        public IRepository<Category, int> Category
        {
            get
            {
                if (_category == null)
                    _category = new CategoryRepository(_ctx);
                return _category;
            }
        }

        private IRepository<PhoneOfHall, int> _phoneOfHall;
        public IRepository<PhoneOfHall, int> PhoneOfHall
        {
            get
            {
                if (_phoneOfHall == null)
                    _phoneOfHall = new PhoneOfHallRepository(_ctx);
                return _phoneOfHall;
            }
        }

        private IRepository<Trainer, int> _trainer;
        public IRepository<Trainer, int> Trainer
        {
            get
            {
                if (_trainer == null)
                    _trainer = new TrainerRepository(_ctx);
                return _trainer;
            }
        }

        private IRepository<HallYandexMapLocation, int> _hallYandexMapLocation;
        public IRepository<HallYandexMapLocation, int> HallYandexMapLocation
        {
            get
            {
                if (_hallYandexMapLocation == null)
                    _hallYandexMapLocation = new HallYandexMapLocationRepository(_ctx);
                return _hallYandexMapLocation;
            }
        }

        #endregion
    }
}
