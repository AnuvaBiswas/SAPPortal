using System.Linq.Expressions;

namespace LabManagementSystem1.Components.Services.Interfaces
{
    public interface ICommonRepository
    {
        public interface ICommonRepository<T> where T : class
        {
            void Insert(T entity);
            T Insert1(T entity);
            void BulkInsert(List<T> entities);

            void Update(T entity);
            T Update1(T entity);
            void BulkUpdate(List<T> entities);
            void Update(int entityId, Dictionary<string, object> propertyUpdates);
            void BulkUpdate(List<T> entities, Dictionary<string, object> commonPropertyUpdates);

            void Delete(T entity);
            int Delete1(T entity);
            void DeleteByCode(string code, string value);
            void DeleteById(int id);
            void DeleteByIdLong(long id);
            void BulkDelete(List<T> entities);
            void BulkDelete(Expression<Func<T, bool>> condition);
            void BulkDelete(List<T> entities, Expression<Func<T, bool>> condition);
            void Update(Expression<Func<T, bool>> condition, Action<T> updateAction);
            List<string> GetAllSelectProperty(String PropertyName);
            List<Dictionary<string, string>> GetAllDynamicProperties(params string[] propertyNames);

            List<T> GetAll();
            List<T> GetAll(Expression<Func<T, bool>> condition);
            List<T> GetAllDateString(Expression<Func<T, bool>> condition);
            T GetByIdLong(long id);
            T GetById(int id);
            //List<string> GetAllDistinctPeriods(Expression<Func<T, string>> periodSelector);

            IList<string> GetAllDistinctPeriods<U>(Expression<Func<U, string>> periodSelector) where U : class;

            T GetByCode(string code, string value);
            public T Upsert(T entity, string propertyName, object propertyValue);
            void BulkInsertOrUpdate(IEnumerable<T> entities, Func<T, string> compositeKeySelector);
            string GetAllCalCulate(string id);

            void BulkUpdatebyCompsitkey(IEnumerable<T> entities, Func<T, string> compositeKeySelector);


            //  List<String> GetAllDistinct(String  ColumnName );

            IList<string> GetAllDistinctColumn<U>(Expression<Func<U, string>> periodSelector) where U : class;

            string GetPassBookData(string empcode, string finyear);
        }
    }
}
