using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using Dapper;
using DataAccessModels;

namespace DataAccessLayer.repository
{
    public class Repository
    {
         private readonly SqlConnectionDev _sqlConn;

        public Repository(SqlConnectionDev sqlConn)
        {
            _sqlConn = sqlConn;
        }

        public bool Create(BaseModel requestObject)
        {
            Dictionary<string, object> propDictionary = requestObject.GetPropertyDictionary();
            DynamicParameters parameters = new DynamicParameters();
            parameters = requestObject.BuildParameters(propDictionary);
            string sql = requestObject.GetInsertSql(propDictionary);


            using (var conn = _sqlConn.GetSqlConnection())
            {
                conn.Execute(sql, parameters);
            }

            return true;
        }

        public IEnumerable<T> Read<T>(BaseModel requestObject)
        {
            
            DynamicParameters parameters = new DynamicParameters();
            KeyValuePair<string, object> searchField = requestObject.GetSearchField();
            string sql = requestObject.GetSelectSql();
            sql += requestObject.GetWhereClause(searchField);
            parameters.Add(searchField.Key, searchField.Value);
            IEnumerable<T> queryResult;

            using (var conn = _sqlConn.GetSqlConnection())
            {
                queryResult = conn.Query<T>(sql, parameters);
            }

            return queryResult;
        }

        public IEnumerable<T> Read<T>(BaseModel requestObject, string whereClause)
        {

            DynamicParameters parameters = new DynamicParameters();
            KeyValuePair<string, object> searchField = requestObject.GetSearchField();
            string sql = requestObject.GetSelectSql();
            sql += whereClause;
            parameters.Add(searchField.Key, searchField.Value);
            IEnumerable<T> queryResult;

            using (var conn = _sqlConn.GetSqlConnection())
            {
                queryResult = conn.Query<T>(sql, parameters);
            }

            return queryResult;
        }


        public IEnumerable<T> ReadAll<T>(BaseModel requestObject)
        {
            string sql = requestObject.GetSelectSql();
            IEnumerable<T> queryResult;
            using (var conn = _sqlConn.GetSqlConnection())
            {
                queryResult = conn.Query<T>(sql);
            }

            return queryResult;
        }

        public bool Update(BaseModel requestObject)
        {
            Dictionary<string, object> propDictionary = requestObject.GetPropertyDictionary();
            string sql = requestObject.GetUpdateSql(propDictionary);
            KeyValuePair<string, object> searchField = requestObject.GetSearchField();
            propDictionary.Add(searchField.Key, searchField.Value);
            string whereSql = requestObject.GetWhereClause(searchField);
            sql += whereSql;
            DynamicParameters parameters = requestObject.BuildParameters(propDictionary);

            using (var conn = _sqlConn.GetSqlConnection())
            {
                conn.Execute(sql, parameters);
            }

            return true;
        }

        public bool Delete(BaseModel requestObject)
        {
            string sql = requestObject.GetDeleteSql();
            DynamicParameters parameters = new DynamicParameters();
            KeyValuePair<string, object> searchField = requestObject.GetSearchField();
            parameters.Add(searchField.Key, searchField.Value);


            using (var conn = _sqlConn.GetSqlConnection())
            {
                conn.Execute(sql, parameters);
            }

            return true;
        }
    }
}
