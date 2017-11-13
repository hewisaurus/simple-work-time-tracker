using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace Database.Dapper
{
    public abstract class RepositoryBase
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly string _connectionString;

        protected RepositoryBase(IConnectionFactory connectionFactory, string connectionString)
        {
            _connectionFactory = connectionFactory;
            _connectionString = connectionString;
        }

        protected async Task<DbConnection> OpenConnectionAsync()
        {
            var connection = _connectionFactory.Create();
            await connection.OpenAsync();
            return connection;
        }

        protected async Task<T> QueryAsync<T>(Func<IDbConnection, Task<T>> func)
        {
            if (func == null)
            {
                throw new ArgumentNullException("func");
            }
            try
            {
                using (var connection = await OpenConnectionAsync())
                {
                    return await func(connection);
                }
            }
            catch (TimeoutException ex)
            {
                throw new Exception(string.Format("{0}.QueryAsync() timed out", GetType().FullName));
            }
            catch (Exception ex)
            {
                throw new Exception("Exception in QueryAsync()", ex);
            }
        }

        protected async Task<int> ExecuteAsync(Func<IDbConnection, Task<int>> func)
        {
            if (func == null)
            {
                throw new ArgumentNullException("func");
            }
            try
            {
                using (var connection = await OpenConnectionAsync())
                {
                    return await func(connection);
                }
            }
            catch (TimeoutException ex)
            {
                throw new Exception(string.Format("{0}.ExecuteAsync() timed out", GetType().FullName));
            }
            catch (Exception ex)
            {
                throw new Exception("Exception in ExecuteAsync()", ex);
            }
        }
    }
}
