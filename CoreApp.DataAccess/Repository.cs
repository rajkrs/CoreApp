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

    public class Repository<T> : IRepository<T> where T : class
    {
        protected DbContext _dbContext { get; set; }

        public Repository(DbContext repositoryContext)
        {
            _dbContext = repositoryContext;
        }

        public virtual IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }

        }

        public virtual IQueryable<T> GetAll<TKey>(Expression<Func<T, TKey>> SortCriteria = null)
        {

            return SortCriteria == null ? _dbContext.Set<T>().AsQueryable<T>() : _dbContext.Set<T>().OrderByDescending(SortCriteria).AsQueryable<T>();

        }


        public virtual IQueryable<T> GetAll()
        {

            return _dbContext.Set<T>();

        }

        public virtual Task<List<T>> GetAllAsync()
        {
            return _dbContext.Set<T>().ToListAsync();
        }



        public virtual T GetById(object id)
        {

            return _dbContext.Set<T>().Find(id);


        }

        public virtual Task<T> GetByIdAsync(object id)
        {

            return _dbContext.Set<T>().FindAsync(id);


        }



        public void Add(T entity)
        {

            _dbContext.Set<T>().Add(entity);

        }

        public void AddRange(IEnumerable<T> entities)
        {

            _dbContext.Set<T>().AddRange(entities);

        }



        public virtual void Update(T entity)
        {
            _dbContext.Set<T>().Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;

        }

        public void UpdateRange(IEnumerable<T> entities)
        {

            _dbContext.Set<T>().UpdateRange(entities);

        }



        public virtual void Delete(T entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
            {
                _dbContext.Set<T>().Attach(entity);
            }
            _dbContext.Set<T>().Remove(entity);
        }

        public virtual void DeleteById(object id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                Delete(entity);
            }
        }


        public void DeleteRange(IEnumerable<T> entity)
        {
            _dbContext.Set<T>().RemoveRange(entity);
        }

        public async Task<bool> SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
            bool returnValue = true;
            using (var dbContextTransaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    _dbContext.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    returnValue = false;
                    dbContextTransaction.Rollback();
                }
            }
            return returnValue;

        }






        public async Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var rowsAffected = 0;
            using (var db = _dbContext.Database.GetDbConnection())
            {
                rowsAffected = await db.ExecuteAsync(sql, param, transaction, commandTimeout, commandType);
            }
            return rowsAffected;
        }

        public async Task<IEnumerable<T>> QueryAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            IEnumerable<T> result;
            using (var db = _dbContext.Database.GetDbConnection())
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
