using Dal;
using NLog;
using System.Collections.Generic;
using System.Linq;

namespace SportGuideASP
{
    public static class StaticData
    {
        public static Logger Log { get; set; } = LogManager.GetCurrentClassLogger();

        public static void Reset()
        {
            Log.Trace("Reset categories data");
            _categories = null;
        }

        private static IEnumerable<Category> _categories;
        public static IEnumerable<Category> Categories
        {
            get
            {
                if (_categories == null)
                    _categories = new DataManager().Category.GetAll().ToArray();
                return _categories;
            }
        }
    }
}