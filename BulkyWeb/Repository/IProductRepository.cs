using BulkyWeb.Models;
using System.Linq.Expressions;

namespace BulkyWeb.Repository
{
    public interface IProductRepository :IRepository<Product>
    {
        void Update(Product product);

        
        
    }
}
