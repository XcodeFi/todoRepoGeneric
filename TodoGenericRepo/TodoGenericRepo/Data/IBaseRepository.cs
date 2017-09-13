using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TodoGenericRepo.Models;

namespace TodoGenericRepo.Data
{
    public interface IBaseRepository<T> where T : Entity, new()
    {
        #region Async Method
        Task<List<T>> GetAsync();
        Task<T> GetAsync(int id); AsyncTableQuery<T> AsQueryableAsync();
        Task<int> CountAsync(Expression<Func<T, bool>> predicate = null);
        Task<int> DeleteAsync(T entity);
        Task<ObservableCollection<T>> GetAsync<TValue>(Expression<Func<T, bool>> predicate = null, Expression<Func<T, TValue>> orderBy = null);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate);
        Task<int> UpdateAsync(T entity);
        Task<int> InsertAsync(T entity);
        #endregion
        List<T> Get();
        T Get(int id);
        TableQuery<T> AsQueryable();
        int Count(Expression<Func<T, bool>> predicate = null);
        int Delete(T entity);
        ObservableCollection<T> Get<TValue>(Expression<Func<T, bool>> predicate = null, Expression<Func<T, TValue>> orderBy = null);
        T Get(Expression<Func<T, bool>> predicate);
        int Insert(T entity);
        int Update(T entity);
    }
}