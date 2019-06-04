using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CoreApp.DataAccess
{
    public interface IRepositoryBase<T>
    {

        IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
        IQueryable<T> GetAll<TKey>(Expression<Func<T, TKey>> SortCriteria = null);
        IQueryable<T> GetAll();
        Task<List<T>> GetAllAsync();

        T GetById(object id);
        Task<T> GetByIdAsync(object id);
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);

        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);


        void Delete(T entity);
        void DeleteById(object id);
        void DeleteRange(IEnumerable<T> entities);

        Task<bool> SaveAsync();



        //Extended Execution
        Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        //ExecuteAsync
        //QueryAsync
        //QueryFirstAsync
        //QueryFirstOrDefaultAsync
        //QuerySingleAsync
        //QuerySingleOrDefaultAsync
        //QueryMultipleAsync



    }
}
