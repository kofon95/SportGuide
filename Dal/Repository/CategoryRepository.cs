using System.Linq;

namespace Dal.Repository
{
    class CategoryRepository : IRepository<Category, int>
    {
        SportGuideEntities _ctx;

        public CategoryRepository(SportGuideEntities _ctx)
        {
            this._ctx = _ctx;
        }

        public IQueryable<Category> GetAll()
        {
            return _ctx.Category;
        }

        public Category GetById(int id)
        {
            return _ctx.Category.Single(t => t.id == id);
        }

        public Category Save(Category entity)
        {
            var added = _ctx.Category.Add(entity);
            _ctx.SaveChanges();
            return added;
        }

        public void Update(Category entity)
        {
            var updating = _ctx.Category.Single(t => t.id == entity.id);
            updating.category_name = entity.category_name;

            _ctx.SaveChanges();
        }

        public void Delete(Category entity)
        {
            _ctx.Category.Remove(entity);
        }

        public void DeleteById(int id)
        {
            var entity = _ctx.Category.Single(t => t.id == id);
            Delete(entity);
        }
    }
}
