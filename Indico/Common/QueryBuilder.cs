﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndicoPacking.Extensions;

namespace IndicoPacking.Common
{
    /// <summary>
    /// this class will help to build SQL queries easily
    /// </summary>
    /// <author>shanaka rusith</author>
    public class QueryBuilder
    {
        /// <summary>
        /// generate query for delete row from a table
        /// </summary>
        /// <param name="tableName">name of the table</param>
        /// <param name="id">id of the item</param>
        /// <returns>generated query</returns>
        public static string DeleteFromTable(string tableName, int id)
        {
            return string.Format("DELETE FROM [dbo].[{0}] WHERE ID = {1}", tableName, id);
        }

        /// <summary>
        /// delete range of items from a table
        /// </summary>
        /// <param name="tableName">name of the table</param>
        /// <param name="ids">list of ids to delete</param>
        /// <returns>generated query</returns>
        public static string DeleteFromTable(string tableName, IEnumerable<int> ids)
        {
            var stringBuilder = new StringBuilder();
            foreach (var id in ids)
            {
                stringBuilder.Append(DeleteFromTable(tableName, id) + ";");
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// generates the query for select all items from a table
        /// </summary>
        /// <param name="tableName">name of the table</param>
        /// <returns>generated query</returns>
        public static string SelectAll(string tableName)
        {
            return string.Format("SELECT * FROM [dbo].[{0}]", tableName);
        }

        /// <summary>
        /// generate the query for select an specific item from a table  or a view
        /// </summary>
        /// <param name="tableName">name of the table</param>
        /// <param name="id">id to select</param>
        /// <returns>generated query</returns>
        public static string Select(string tableName, int id)
        {
            return string.Format("SELECT * FROM [dbo].[{0}] WHERE ID = {1}", tableName, id);
        }

        /// <summary>
        /// creates an query for update item in the database
        /// </summary>
        /// <param name="tableName">name of the table</param>
        /// <param name="values">dictionary that contains column name and value</param>
        /// <param name="id">id of the item to update</param>
        /// <returns>generated query</returns>
        public static string Update(string tableName, Dictionary<string, object> values, int id)
        {
            if (values == null || values.Count < 1)
                return "";
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(string.Format("UPDATE [dbo].[{0}] SET ", tableName));
            var first = true;
            foreach (var key in values.Keys)
            {
                var value = values[key];
                if (value == null)
                {
                    stringBuilder.Append(string.Format("{1}[{0}] = NULL", key, !first ? "," : ""));
                    continue;
                }
                var wrapper = value.IsNumeric() ? "" : "'";
                stringBuilder.Append(string.Format("{0}{1} = {2}{3}{4}", !first ? ", " : "", string.Format("[{0}]", key), wrapper, (value as bool?)?.ToOneZero() ?? value, wrapper));
                first = false;
            }
            stringBuilder.AppendLine(string.Format(" WHERE ID = {0};", id));
            return stringBuilder.ToString();
        }

        /// <summary>
        ///generates query for insert into a table 
        /// </summary>
        /// <param name="tableName">name of the table</param>
        /// <param name="values">dictionary that contains column name and value</param>
        /// <returns>generated query</returns>
        public static string Insert(string tableName, Dictionary<string, object> values)
        {
            if (values == null || values.Count < 1)
                return "";
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(string.Format("INSERT INTO [dbo].[{0}] ", tableName));
            var columnNames = new List<string>();
            var valuestrings = new List<string>();
            foreach (var key in values.Keys)
            {
                var value = values[key];
                columnNames.Add(key);
                var wrapper = value.IsNumeric() ? "" : "'";
                if (value == null)
                {
                    valuestrings.Add("NULL");
                    continue;
                }
                valuestrings.Add(string.Format("{0}{1}{2}", wrapper, (value as bool?)?.ToOneZero() ?? value, wrapper));
            }

            stringBuilder.Append(string.Format("({0}) VALUES({1});", GenerateColumnNameList(columnNames), valuestrings.Aggregate((c, n) => c + "," + n)));
            return stringBuilder.ToString();
        }

        /// <summary>
        /// generates query for execute stored procedure with parameters
        /// </summary>
        /// <param name="spName">name of the stored procedure</param>
        /// <param name="parameters">parameters for the procedure</param>
        /// <returns>generated query</returns>
        public static string ExecuteStoredProcedure(string spName, params object[] parameters)
        {
            if (string.IsNullOrWhiteSpace(spName))
                return "";
            var builder = new StringBuilder();
            var valuestrings = (from item in parameters let wrapper = item.IsNumeric() ? "" : "'" select string.Format("{0}{1}{2}", wrapper, (item as bool?)?.ToOneZero() ?? item, wrapper)).ToList();
            builder.Append(string.Format("EXEC [dbo].[SPC_{0}] {1}", spName, valuestrings.Aggregate((c, n) => c + "," + n)));
            return builder.ToString();
        }

        /// <summary>
        /// Create query for get objects from a table using where conditions
        /// </summary>
        /// <param name="tableName">name of the target table or view</param>
        /// <param name="values">values for where clause</param>
        /// <returns></returns>
        public static string Where(string tableName, IDictionary<string, object> values)
        {
            if (string.IsNullOrWhiteSpace(tableName))
                return null;
            var stringBuilder = new StringBuilder(string.Format("SELECT * FROM [dbo].[{0}]", tableName));
            if (values == null || values.Count < 1)
                return stringBuilder.ToString();
            var wherestrings = new List<string>();
            foreach (var item in values)
            {
                var value = item.Value;
                var wrapper = value.IsNumeric() ? "" : "'";
                if (string.IsNullOrWhiteSpace(item.Key))
                    continue;
                if (value == null)
                    wherestrings.Add(string.Format("[{0}] IS NULL", item.Key));
                wherestrings.Add(string.Format("[{3}] = {0}{1}{2}", wrapper, (value as bool?)?.ToOneZero() ?? value, wrapper, item.Key));
            }
            return string.Format(stringBuilder + " WHERE " + wherestrings.Aggregate((c, n) => n + " AND " + c));
        }

        /// <summary>
        /// Create query for get total count of a table
        /// </summary>
        /// <param name="tableName">name of the table</param>
        /// <returns></returns>
        public static string Count(string tableName)
        {
            return string.IsNullOrWhiteSpace(tableName) ? null : string.Format("SELECT COUNT(*) FROM [dbo].[{0}]",tableName);
        }

        private static string GenerateColumnNameList(IEnumerable<string> columnNames)
        {
            var stringBuilder = new StringBuilder();
            foreach (var column in columnNames)
            {
                stringBuilder.AppendFormat("[{0}] ,", column);
            }
            return stringBuilder.ToString().TrimEnd(',');
        }
    }
}
