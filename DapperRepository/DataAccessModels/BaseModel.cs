using System;
using System.Collections.Generic;
using System.ComponentModel;
using Dapper;

namespace DataAccessModels
{
    public class BaseModel
    {
        public string TableName { get; set; }

        public string GetInsertSql(Dictionary<string, object> propDictionary)
        {
            var columns = "";
            var values = "";

            foreach (var item in propDictionary)
            {
                columns += item.Key + ", ";
                values += "@" + item.Key + ", ";
            }
            columns = columns.Remove(columns.Length - 2, 2);
            values = values.Remove(values.Length - 2, 2);

            string sql = "INSERT INTO "+TableName+" (" + columns + ") VALUES(" + values + ")";
            return sql;
        }

        public string GetWhereClause(KeyValuePair<string, object> searchField)
        {
            return " WHERE " + searchField.Key + " = @" + searchField.Key;
        }

        public string GetSelectSql()
        {
            return "SELECT * FROM " + TableName;
        }

        public string GetUpdateSql(Dictionary<string, object> propDictionary )
        {
            
            var setSql = "";
            var count = propDictionary.Count;

            foreach (var item in propDictionary)
            {
                setSql += item.Key + " = @" + item.Key + ", ";
            }

            setSql = setSql.Remove(setSql.Length - 2, 2);
            setSql += " ";
            string sql = "UPDATE " + TableName + " SET " + setSql;
            return sql;
        }

        public string GetDeleteSql()
        {
            return "DELETE FROM " + TableName + " WHERE id = @id";
        }

        public Dictionary<string, object> GetPropertyDictionary()
        {
            var propDictionary = new Dictionary<string, object>();

            var passedType = this.GetType();

            foreach (var propertyInfo in passedType.GetProperties())
            {
                var isDef = Attribute.IsDefined(propertyInfo, typeof(DisplayNameAttribute));

                if (isDef)
                {
                    var value = propertyInfo.GetValue(this, null);

                    if (value != null)
                    {
                        var displayNameAttribute =
                            (DisplayNameAttribute)
                                Attribute.GetCustomAttribute(propertyInfo, typeof(DisplayNameAttribute));
                        var displayName = displayNameAttribute.DisplayName;
                        propDictionary.Add(displayName, value);
                    }
                }
            }

            return propDictionary;
        }

        public DynamicParameters BuildParameters(Dictionary<string, object> propDictionary)
        {
            var parameters = new DynamicParameters();

            foreach (var item in propDictionary)
            {
                parameters.Add(item.Key, item.Value);
            }

            return parameters;
        }

        public KeyValuePair<string, object> GetSearchField()
        {
            var searchField = new KeyValuePair<string, object>();

            var passedType = this.GetType();

            foreach (var propertyInfo in passedType.GetProperties())
            {
                var isDef = Attribute.IsDefined(propertyInfo, typeof(SearchField));

                if (isDef)
                {
                    var value = propertyInfo.GetValue(this, null);

                    if (value != null)
                    {
                        var searchFeildAttribute =
                            (SearchField)
                                Attribute.GetCustomAttribute(propertyInfo, typeof(SearchField));
                        var displayName = searchFeildAttribute.Field;
                        searchField = new KeyValuePair<string, object>(displayName, value);
                    }
                }
            }

            return searchField;
        }
    }
}
