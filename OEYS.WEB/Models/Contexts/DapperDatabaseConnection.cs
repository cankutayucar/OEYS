using Dapper;
using System.Data;
using System.Data.SqlClient;
using Dapper.Contrib;
using Dapper.Contrib.Extensions;
using OEYS.WEB.Models.Entities.Common;

namespace OEYS.WEB.Models.Contexts
{
    public static class DapperDatabaseConnection
    {
        static IDbConnection _connection;
        static bool _dataReaderState = true;

        public static void DapperDatabaseConnectionConfigure(IConfiguration configuration) => _connection = new SqlConnection(configuration.GetConnectionString("Mssql"));

        public static IDbConnection Connection
        {
            get
            {
                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();
                return _connection;
            }
        }

        public static async Task<IEnumerable<T>> QueryAsync<T>(string sql, object parameters = null)
             => parameters != null ? await _connection.QueryAsync<T>(sql, param: parameters) : await _connection.QueryAsync<T>(sql);

        public static async Task<(IEnumerable<T>, int)> QueryPagingAsync<T>(string sql, int pageNumber, int pageSize)
        {
            var count = (await _connection.QueryAsync<T>(sql)).Count();
            var pagedList = (await _connection.QueryAsync<T>(sql)).Skip(pageNumber).Take(pageSize).ToList();
            return (pagedList, count);
        }

        public static async Task<int> ExecuteAsync(string sql, object parameters = null)
            => parameters != null ? await _connection.ExecuteAsync(sql, parameters) : await _connection.ExecuteAsync(sql);

        public static async Task InsertAsync<T>(T model) where T : class => await _connection.InsertAsync<T>(model);

        public static async Task UpdateAsync<T>(T model) where T : class => await _connection.UpdateAsync<T>(model);

        public static async Task<T> GetData<T>(int id) where T : class => await _connection.GetAsync<T>(id);


        public static void DataReaderReady()
            => _dataReaderState = true;

        public static void DataReaderBusy()
            => _dataReaderState = false;

        public static bool DataReaderState => _dataReaderState;
    }
}
