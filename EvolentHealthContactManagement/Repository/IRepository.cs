using System.Collections.Generic;

namespace EvolentHealthContactManagement.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();

        bool Add(TEntity entity,out int id);
        bool AddRange(List<TEntity> entities);

        bool Remove(TEntity entity);
        
        bool Update(TEntity entity);
    }
}