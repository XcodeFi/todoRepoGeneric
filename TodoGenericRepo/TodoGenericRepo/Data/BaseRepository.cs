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
    public class BaseRepository<T> : IBaseRepository<T> where T : Entity, new()
    {
        private SQLiteAsyncConnection _dbAsync;
        private SQLiteConnection _db;
        protected static object locker = new object();
        //private bool IsExistsTable = false;

        public BaseRepository(SQLiteAsyncConnection dbAsync, SQLiteConnection db)
        {
            this._db = db;
            this._dbAsync = dbAsync;
            if (!TableExists())
            {
                _dbAsync.CreateTableAsync<T>();
            }
        }

        #region Async method
        public AsyncTableQuery<T> AsQueryableAsync()
        {
            return _dbAsync.Table<T>();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null)
        {
            var query = _dbAsync.Table<T>();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return await query.CountAsync();
        }

        public async Task<int> DeleteAsync(T entity)
        {
            return await _dbAsync.DeleteAsync(entity);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbAsync.FindAsync<T>(predicate);
        }

        public async Task<List<T>> GetAsync()
        {
            return await _dbAsync.Table<T>().ToListAsync();
        }

        public async Task<T> GetAsync(int id)
        {
            return await _dbAsync.FindAsync<T>(id);
        }

        public async Task<ObservableCollection<T>> GetAsync<TValue>(Expression<Func<T, bool>> predicate = null, Expression<Func<T, TValue>> orderBy = null)
        {
            var query = _dbAsync.Table<T>();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (orderBy != null)
            {
                query = query.OrderBy<TValue>(orderBy);
            }

            var collection = new ObservableCollection<T>();
            var items = await query.ToListAsync();
            foreach (var item in items)
            {
                collection.Add(item);
            }

            return collection;
        }

        public async Task<int> UpdateAsync(T entity)
        {
            return await _dbAsync.UpdateAsync(entity);
        }

        public async Task<int> InsertAsync(T entity)
        {
            return await _dbAsync.InsertAsync(entity);
        }
        #endregion

        #region normal method
        public TableQuery<T> AsQueryable()
        {
            lock (locker) { return _db.Table<T>(); }
        }


        public int Count(Expression<Func<T, bool>> predicate = null)
        {
            lock (locker)
            {
                var query = _db.Table<T>();

                if (predicate != null)
                {
                    query = query.Where(predicate);
                }

                return query.Count();
            }
        }

        public int Delete(T entity)
        {
            lock (locker)
            {
                return _db.Delete(entity);
            }
        }

        public List<T> Get()
        {
            lock (locker) { return _db.Table<T>().ToList(); }

        }

        public T Get(Expression<Func<T, bool>> predicate)
        {
            lock (locker) { return _db.Find<T>(predicate); }

        }

        public T Get(int id)
        {
            lock (locker) { return _db.Find<T>(id); }

        }


        public ObservableCollection<T> Get<TValue>(Expression<Func<T, bool>> predicate = null, Expression<Func<T, TValue>> orderBy = null)
        {
            lock (locker)
            {
                var query = _db.Table<T>();

                if (predicate != null)
                {
                    query = query.Where(predicate);
                }
                if (orderBy != null)
                {
                    query = query.OrderBy<TValue>(orderBy);
                }

                var collection = new ObservableCollection<T>();
                var items = query.ToList();
                foreach (var item in items)
                {
                    collection.Add(item);
                }

                return collection;
            }
        }

        public int Insert(T entity)
        {
            lock (locker) { return _db.Insert(entity); }
        }

        public int Update(T entity)
        {
            lock (locker) { return _db.Update(entity); }
        }
        #endregion

        #region private method
        bool TableExists()
        {
            lock (locker)
            {
                const string cmdText = "SELECT name FROM sqlite_master WHERE type='table' AND name=?";
                var cmd = _db.CreateCommand(cmdText, typeof(T).Name);
                return cmd.ExecuteScalar<string>() != null;
            }
        }
        #endregion

    }
}