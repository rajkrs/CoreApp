using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp.DataAccess
{

    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected DbContext RepositoryContext { get; set; }

        public RepositoryBase(DbContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

        public async Task<IEnumerable<T>> FindAllAsync()
        {
            return await RepositoryContext.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return await RepositoryContext.Set<T>().Where(expression).ToListAsync();
        }

        public void Create(T entity)
        {
            RepositoryContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            RepositoryContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            RepositoryContext.Set<T>().Remove(entity);
        }

        public async Task SaveAsync()
        {
            await RepositoryContext.SaveChangesAsync();
        }






        public async Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var rowsAffected = 0;
            using (var db = RepositoryContext.Database.GetDbConnection())
            {
                rowsAffected = await db.ExecuteAsync(sql, param, transaction, commandTimeout, commandType);
            }
            return rowsAffected;
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            IEnumerable<T> result;
            using (var db = RepositoryContext.Database.GetDbConnection())
            {
                result = await db.QueryAsync<T>(sql, param, transaction, commandTimeout, commandType);
            }
            return result;
        }


        //QueryAsync
        //QueryFirstAsync
        //QueryFirstOrDefaultAsync
        //QuerySingleAsync
        //QuerySingleOrDefaultAsync
        //QueryMultipleAsync



    }
}
