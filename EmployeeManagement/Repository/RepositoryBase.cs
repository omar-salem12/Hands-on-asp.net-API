using Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EmployeeManagement.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly RespositoryContext _repositoryContext;

        public RepositoryBase(RespositoryContext repositoryContext )

        {
            _repositoryContext = repositoryContext;
        }
        public void Create(T entity)
        {
            _repositoryContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _repositoryContext.Set<T>().Remove(entity);
        }

        public IQueryable<T> FindAll(bool trackChanges)
        {
          return   !trackChanges ?  _repositoryContext.Set<T>().AsNoTracking() : _repositoryContext.Set<T>();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> condition, bool trackChanges)
        {
            return !trackChanges ? _repositoryContext.Set<T>().Where(condition).AsNoTracking() :
                             _repositoryContext.Set<T>().Where(condition);
        }

        public void Update(T entity)
        {
            _repositoryContext.Set<T>().Update(entity);
        }
    }
}
