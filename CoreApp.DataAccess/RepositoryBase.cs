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
        protected DbContext _dbContext { get; set; }
        private DbSet<T> _dbSet = null;


        public RepositoryBase(DbContext repositoryContext)
        {
            _dbContext = repositoryContext;
            _dbSet = _dbContext.Set<T>();
        }

        public virtual IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            IQueryable<T> query = _dbSet;

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

            return SortCriteria == null ? _dbSet.AsQueryable<T>() : _dbSet.OrderByDescending(SortCriteria).AsQueryable<T>();

        }


        public virtual IQueryable<T> GetAll()
        {

            return _dbSet;

        }

        public virtual Task<List<T>> GetAllAsync()
        {
            return _dbSet.ToListAsync();
        }



        public virtual T GetById(object id)
        {

            return _dbSet.Find(id);


        }

        public virtual Task<T> GetByIdAsync(object id)
        {

            return _dbSet.FindAsync(id);


        }



        public void Add(T entity)
        {

            _dbSet.Add(entity);

        }

        public void AddRange(IEnumerable<T> entities)
        {

            _dbSet.AddRange(entities);

        }



        public virtual void Update(T entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;

        }

        public void UpdateRange(IEnumerable<T> entities)
        {

            _dbSet.UpdateRange(entities);

        }



        public virtual void Delete(T entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
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
            _dbSet.RemoveRange(entity);
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

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
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
