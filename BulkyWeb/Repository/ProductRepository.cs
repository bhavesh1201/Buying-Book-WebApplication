using BulkyWeb.Models;
using System.Linq.Expressions;

namespace BulkyWeb.Repository
{
    public class ProductRepository : Repository<Product> , IProductRepository 
    {

        private readonly AppDbContext _db;

        public ProductRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }
      
     

        public void Update(Product product)
        {
            _db.products.Update(product);
        }
    }
}
