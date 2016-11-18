/*This code is a generated one , Change the source code of the generator if you want some change in this code
You can find the source code of the code generator from here -> https://github.com/rusith/MyCodeGenerator*/

using System;
using System.Collections.Generic;
using IndicoPacking.DAL.Objects.Implementation;

namespace IndicoPacking.DAL.Base.Core
{
    public interface IDbContext : IDisposable
    {
		#region Properties

		IUnitOfWork Unit { get; set;}

		#endregion

		#region Methods

        void SaveChanges();
        T Get<T>(object key,string tableName, string primarykeyName) where T :  Entity;
        List<T> Get<T>(string tableName) where T :  Entity;
        Entity Add(Entity entity,string tableName,Dictionary<string,object> columns);
        void DeleteRange<T>(IEnumerable<Entity> entities, string tableName, string primaryKeyName) where T: Entity;
        List<T> Find<T>(Func<T, bool> predicate, string tableName) where T :  Entity;
		List<T> Where<T>(object values,string tableName) where T :  Entity;
        int Count(string tableName);
		void Delete<T>(Entity entity,string tableName, string primaryKeyName) where T: Entity;
        void Delete<T>(object key,string tableName, string primaryKeyName) where T: Entity;
		List<T> QueryView<T>(string viewname, object where = null) where T : class;
		List<T> Query<T>(string sql) where T : class;
		List<T> SelectPage<T>(string tableName,int pageSize,int page,string primaryKeyName) where T : Entity;
		void AddEntityReference(Entity parent,Entity child,string column);

		#endregion
    }
}
