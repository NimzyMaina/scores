using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Scores.Models;

namespace Scores.Persistence.Abstract
{
    public interface IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        Task <IEnumerable<T>> AllIncludingAsync(params Expression<Func<T, object>>[] includeProperties);
        Task <IEnumerable<T>> GetAllAsync();
        Task <int> CountAsync();
        Task <T> GetSingleAsync(int id);
        Task <T> GetSingleAsync(Expression<Func<T, bool>> predicate);
        Task <T> GetSingleAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void DeleteWhere(Expression<Func<T, bool>> predicate);
    }
}