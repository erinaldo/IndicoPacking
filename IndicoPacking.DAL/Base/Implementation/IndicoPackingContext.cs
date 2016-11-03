using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using Dapper;
using IndicoPacking.DAL.Base.Core;
using IndicoPacking.DAL.Common;
using IndicoPacking.DAL.Objects.Core;
using IndicoPackingCodeBase.Tools;
using System.Configuration;
using System.Collections.Generic;
using IndicoPacking.DAL.Objects.Views;
using System.Text;

namespace IndicoPacking.DAL.Base.Implementation
{
    public class IndicoPackingContext : IDbContext
    {
		private readonly HashSet<IEntity> _dirtyEntities;
        private readonly HashSet<IEntity> _entities; 
        private readonly IDbConnection _connection;

		public IUnitOfWork Unit { get; set; }

        public  IndicoPackingContext()
        {
            _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["IndicoPacking"].ConnectionString);
            _connection.Open();
            _dirtyEntities = new HashSet<IEntity>();
			_entities=new HashSet<IEntity>();
        }


        public T Get<T>(int id, string tableName) where T : class, IEntity
        {
            T entity;
            var found = _entities.OfType<T>().Where(t => t.ID == id).ToList();
            if (found.Any())
                return found.First();
            try
            {
                entity = _connection.Query<T>(string.Format("SELECT TOP 1 * FROM [dbo].[{0}] WHERE ID = '{1}'",tableName, id)).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new Exception("cannot retrieve data from the database", e);
            }
            if (entity == null)
                return null;
            _entities.Add(entity);
            entity.PropertyChanged += EntityPropertyChanged;
            entity._context = this;

            return entity;
        }

       public IEnumerable<T> Get<T>(string tableName) where T : class, IEntity
       {
            List<T> entities;
            var localentities = _entities.OfType<T>().ToList();
            if (localentities.Count == Count(tableName))
                return localentities;
            try
            {
                entities = _connection.Query<T>(string.Format("SELECT * FROM [dbo].[{0}]",tableName)).ToList();
            }
            catch (Exception e)
            {
                throw new Exception("cannot retrieve data from the database", e);
            }
            if (entities.Count <= 0)
                return entities;
            foreach (var entity in entities)
            {
                _entities.Add(entity);
                entity.PropertyChanged += EntityPropertyChanged;
                entity._context = this;
            }
            return entities;
        }

		public int? Add(IEntity entity,string tableName,Dictionary<string,object> columns)
        {
			var query = "INSERT INTO [dbo].[{0}] ({1}) VALUES({2}); SELECT SCOPE_IDENTITY()";
			var cols = new StringBuilder();
			var values = new StringBuilder();
			foreach(var item in columns)
			{
				cols.Append(item.Key+",");
				values.AppendFormat("'{0}',", item.Value.ToString().Replace("'","''"));
			}

			query = string.Format(query, tableName, cols.ToString().TrimEnd(','), values.ToString().TrimEnd(','));
			
            var res = entity == null ? 0 : (int)_connection.ExecuteScalar(query);

		    if (res <= 0 || entity == null)
                return res;
		    entity._context = this;
		    entity.PropertyChanged += EntityPropertyChanged;
		    _entities.Add(entity);

		    return res;
        }

        private void EntityPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            _dirtyEntities.Add((IEntity)sender);
        }


        public void Dispose()
        {
            _connection.Close();
            _dirtyEntities.Clear();
        }

        public void SaveChanges()
        {
            if (_dirtyEntities.Count > 0)
            {
				foreach (var entity in _dirtyEntities)
                {
                    var type = entity.GetType();
                    var typeName = type.Name;
                    typeName = typeName.Substring(0, typeName.Length - 2);
                    var id = type.GetProperty("ID").GetValue(entity);
                    var values = entity.GetColumns();
                    var query = "UPDATE [dbo].[" + typeName + "] SET {0} WHERE ID = " + id;
                    var setQuery = new StringBuilder();
                    foreach (var item in values)
                    {
                        setQuery.AppendFormat("{0} = '{1}',", item.Key, item.Value);
                    }
                    _connection.Execute(string.Format(query, setQuery));
                }
            }
            _dirtyEntities.Clear();
        }

        public void Delete<T>(IEntity entity,string tableName) where T: class,IEntity
        {
			if (entity != null && entity.ID > 0) { }
            {
				Delete<T>(entity.ID,tableName);
            }
        }


        public void Delete<T>(int id,string tableName) where T: class,IEntity
        {
            if (id > 0)
            {
				var query = string.Format("DELETE [dbo].[{0}] WHERE ID = '{1}'",tableName,id);
                _connection.Execute(query);
                var ent = _entities.OfType<T>().FirstOrDefault(t => t.ID == id);
                _entities.Remove(ent);
            }
        }


        public void DeleteRange<T>(IEnumerable<IEntity> entities,string tableName) where T: class,IEntity
        {
            var list = entities.ToList();
            if (entities == null || !list.Any())
                return;
            foreach (var entity in list)
                Delete<T>(entity,tableName);
        }

        public IEnumerable<T> Find<T>(Func<T, bool> predicate, string tableName) where T : class, IEntity
        {
            var localentities = _entities.OfType<T>().ToList();
            if (localentities.Count == Count(tableName))
                return localentities.Where(predicate);

            var entities= Get<T>(tableName).Where(predicate).ToList();
            foreach (var entity in entities)
            {
                entity._context = this;
                entity.PropertyChanged += EntityPropertyChanged;
                _entities.Add(entity);
            }
            return entities;
        }

        public IEnumerable<T> Where<T>(object values,string tableName) where T : class, IEntity
        {
            const string query = "SELECT * FROM [dbo].[{0}] WHERE {1}";

            var whereBulder = new StringBuilder();
            var objectType = values.GetType();
            var first = true;
            foreach (var property in objectType.GetProperties())
            {
                whereBulder.AppendFormat("{2} {0} = '{1}'", property.Name, property.GetValue(values).ToString().Replace("'","''"),first?"":"AND");
                first = false;
            }

            var result = _connection.Query<T>(string.Format(query,tableName,whereBulder)).ToList();
            if (result.Count <= 0)
                return result;
            foreach (var item in result)
            {
                item.PropertyChanged += EntityPropertyChanged;
                item._context = this;
            }
            return result;
        }

        public int Count(string tableName)
        {
            const string query = "SELECT COUNT(*) FROM [dbo].[{0}]";
            return (int)_connection.ExecuteScalar(string.Format(query,tableName));
        }

		public List<T> QueryView<T>(string viewname,object where=null ) where T : class
        {
            var queryBuilder = new StringBuilder("SELECT * FROM [dbo][" + viewname + "]");
            if (where != null)
                queryBuilder.Append(CreateWhere(where));
            return _connection.Query<T>(queryBuilder.ToString()).ToList();
        }

		public List<T> Query<T>(string sql) where T : class
        {
            return _connection.Query<T>(sql).ToList();
        }

        private static string CreateWhere(object where)
        {
            var type = where.GetType();
            var builder = new StringBuilder(" WHERE ");
            var condisions = type.GetProperties().Select(property => string.Format("[{0}] = '{1}'", property.Name, property.GetValue(@where).ToString().Replace("'","''"))).ToList();
            return builder.Append(condisions.Aggregate((c, n) => c + " AND " + n)).ToString();
        }
    }
}
