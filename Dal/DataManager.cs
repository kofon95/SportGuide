using Dal.Repository;

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
