using BulkyWeb.Models;
using System.Linq.Expressions;

namespace BulkyWeb.Repository
{
    public class CategoryRepository : Repository<Category> , ICategoryRepository
    {

        private readonly AppDbContext _db;
        public CategoryRepository(AppDbContext db) : base(db)
        {
            _db = db;
            
        }
        

        public void Update(Category category)
        {
            _db.categories.Update(category);
        }
    }
}
