using ASAPSystems.Task.Infrastructure.EntityService.Context;
using ASAPSystems.Task.Infrastructure.IEntityService.BaseEntityServices;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static ASAPSystems.Task.Common.Enums.Enums;

namespace ASAPSystems.Task.Infrastructure.EntityService.BaseEntityServices
{
    public class BaseRepository<T> : IBaseRepository<T>, IDisposable where T : class
    {
        #region Props
        private ILogger _Logger { get; }
        public APPDbContext _AppDbContext { get; }

        #endregion
        #region CTOR
        public BaseRepository(APPDbContext appDbContext, ILogger Logger)
        {
            _Logger = Logger;
            _AppDbContext = appDbContext;

        }
        #endregion
        #region Main Methods
        public bool Delete(T entity)
        {
            bool Res = default(bool);
            try
            {
                _AppDbContext.Set<T>().Remove(entity);
                Res = true;
            }
            catch (Exception)
            {

                throw;
            }
            return Res;
        }

        public bool Delete(List<T> entityList)
        {
            bool Res = default(bool);
            try
            {
                _AppDbContext.Set<T>().RemoveRange(entityList);
                Res = true;
            }
            catch (Exception)
            {

                throw;
            }
            return Res;
        }
        public T Get(Expression<Func<T, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<T> query = _AppDbContext.Set<T>();

            if (filter != null)
            {
                query = query.AsNoTracking().Where(filter);
            }

            query = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return query.FirstOrDefault();
        }

        public List<T> GetAll(Expression<Func<T, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<T> query = _AppDbContext.Set<T>();

            if (filter != null)
            {
                query = query.AsNoTracking().Where(filter);
            }

            if (includeProperties.Length > 0)
            {
                query = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                            .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            }

            return query.ToList();
        }

        public bool GetAny(Expression<Func<T, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<T> query = _AppDbContext.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return query.Any();
        }

        public int GetCount(Expression<Func<T, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<T> query = _AppDbContext.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return query.Count();
        }

        public int GetCount(Expression<Func<T, bool>> filter)
        {
            return _AppDbContext.Set<T>().Where(filter).Count();
        }
        public IQueryable<T> GetWhere(Expression<Func<T, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<T> query = _AppDbContext.Set<T>();
            if (filter != null)
                query = query.AsNoTracking().Where(filter);
            query = includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return query.AsQueryable<T>();
        }
        public List<T> GetPageWhere<TKey>(int skipCount, int takeCount, Expression<Func<T, TKey>> sortingExpression, Expression<Func<T, bool>> filter, SortDirection sortDir, string includeString)
        {
            List<T> queryResult = null;

            switch (sortDir)
            {
                case SortDirection.Ascending:
                    if (skipCount == 0)
                        queryResult = GetWhere(filter, includeString).OrderBy<T, TKey>(sortingExpression).Take(takeCount).ToList();
                    else
                        queryResult = GetWhere(filter, includeString).OrderBy<T, TKey>(sortingExpression).Skip(skipCount).Take(takeCount).ToList();
                    break;
                case SortDirection.Descending:
                    if (skipCount == 0)
                        queryResult = GetWhere(filter, includeString).OrderByDescending<T, TKey>(sortingExpression).Take(takeCount).ToList();
                    else
                        queryResult = GetWhere(filter, includeString).OrderByDescending<T, TKey>(sortingExpression).Skip(skipCount).Take(takeCount).ToList();
                    break;
                default:
                    break;
            }
            return queryResult;
        }
        public List<T> GetFromProc(string ProcName, params string[] Paramaters)
        {
            string sql = $"Exec {ProcName} ";
            if (Paramaters != null || Paramaters.Count() > 0)
            {
                string Params = string.Empty;
                for (int i = 0; i < Paramaters.Count(); i++)
                {
                    if (i != 0)
                    {
                        if ((Paramaters.Count() - i) != 0)
                            Params += ',';
                    }
                    if (Paramaters[i].Contains(',') || Paramaters[i].Contains('-'))
                        Paramaters[i] = $"'{Paramaters[i]}'";
                    Params += Paramaters[i];
                }
                sql += Params;
            }
            return _AppDbContext.Set<T>().FromSqlRaw(sql).ToList();
        }
        public bool Insert(T entity)
        {

            bool Result = false;
            try
            {
                _AppDbContext.Set<T>().Add(entity);
                Result = true;
            }
            catch (Exception ex)
            {
                throw;
            }
            return Result;
        }
        public T InsertAndReturnEntity(T entity)
        {

            bool Result = false;
            try
            {
                _AppDbContext.Set<T>().Add(entity);
                Result = true;
            }
            catch (Exception ex)
            {
                throw;
            }
            return entity;
        }
        public bool Insert(List<T> entityList)
        {
            bool Result = false;
            try
            {
                _AppDbContext.Set<T>().AddRange(entityList);
                Result = true;
            }
            catch (Exception ex)
            {
                throw;
            }
            return Result;
        }
        public bool Update(T entity)
        {
            bool res = default(bool);
            try
            {
                _AppDbContext.Update(entity);
                res = true;
            }
            catch (Exception)
            {

                throw;
            }
            return res;
        }

        public bool Update(List<T> entityList)
        {
            bool res = default(bool);
            try
            {
                _AppDbContext.Set<T>().UpdateRange(entityList);
                res = true;
            }
            catch (Exception)
            {

                throw;
            }
            return res;
        }
        public void Dispose()
        {
            _AppDbContext.Dispose();
        }
        #region ADO Methods
        public int ExecuteNonQuery(string commandText, string connectionString)
        {
            int result = default(int);
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = conn.CreateCommand();
                    command.CommandText = commandText;
                    command.CommandType = CommandType.StoredProcedure;

                    result = command.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exception)
            {
                // Logger.LogError(exception, "");
            }
            return result;
        }
        #endregion
        #endregion
    }
}
