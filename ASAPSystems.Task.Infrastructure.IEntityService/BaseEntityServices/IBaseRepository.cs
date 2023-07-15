using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static ASAPSystems.Task.Common.Enums.Enums;

namespace ASAPSystems.Task.Infrastructure.IEntityService.BaseEntityServices
{
    public interface IBaseRepository<T>
    {
        #region Methods :
        IQueryable<T> GetWhere(Expression<Func<T, bool>> filter = null, string includeProperties = "");
        int GetCount(Expression<Func<T, bool>> filter = null, string includeProperties = "");
        bool GetAny(Expression<Func<T, bool>> filter = null, string includeProperties = "");
        T Get(Expression<Func<T, bool>> filter = null, string includeProperties = "");
        List<T> GetAll(Expression<Func<T, bool>> filter = null, string includeProperties = "");
        bool Insert(T entity);
        bool Insert(List<T> entityList);
        T InsertAndReturnEntity(T entity);

        bool Update(T entity);
        int GetCount(Expression<Func<T, bool>> filter);
        List<T> GetFromProc(string ProcName, params string[] Paramaters);
        List<T> GetPageWhere<TKey>(int skipCount, int takeCount, Expression<Func<T, TKey>> sortingExpression, Expression<Func<T, bool>> filter, SortDirection sortDir, string includeString);
        int ExecuteNonQuery(string commandText, string connectionString);
        #endregion
    }
}
