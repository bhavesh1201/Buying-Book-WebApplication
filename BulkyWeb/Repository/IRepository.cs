using System.Linq.Expressions;

namespace BulkyWeb.Repository
{
    public interface IRepository <T> where T : class
    {
        IEnumerable<T> GetAll (string? prop = null);
        T Get(Expression<Func<T,bool>> Filter , string? prop = null);

        void Add(T entity);  
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);


    }
}
