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
        List<T> Get();
        Task<List<T>> GetAsync();

        Task<T> GetAsync(int id);
        T Get(int id);

        AsyncTableQuery<T> AsQueryableAsync();
        TableQuery<T> AsQueryable();
        Task<int> CountAsync(Expression<Func<T, bool>> predicate = null);
        int Count(Expression<Func<T, bool>> predicate = null);
        Task<int> DeleteAsync(T entity);
        int Delete(T entity);
        Task<ObservableCollection<T>> GetAsync<TValue>(Expression<Func<T, bool>> predicate = null, Expression<Func<T, TValue>> orderBy = null);
        ObservableCollection<T> Get<TValue>(Expression<Func<T, bool>> predicate = null, Expression<Func<T, TValue>> orderBy = null);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate);
        T Get(Expression<Func<T, bool>> predicate);
        Task<int> InsertAsync(T entity);
        int Insert(T entity);

        Task<int> UpdateAsync(T entity);
        int Update(T entity);

    }
}