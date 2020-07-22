using CQRSDapper.DAL.Interfaces;
using CQRSDapper.Domain.Models;
using Dapper;
using Polly;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSDapper.DAL.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly string _connectionString;

        public readonly AsyncRetryPolicy _retryPolicy;
        private readonly int[] _sqlExceptions = new[] { 53, -2 };
        private const int RetryCount = 3;
        private const int WaitBetweenRetriesInSeconds = 15;

        public BaseRepository(string connectionString)
        {
            _retryPolicy = Policy.Handle<SqlException>(exception => _sqlExceptions.Contains(exception.Number))
                                 .WaitAndRetryAsync(retryCount: RetryCount,
                                                    sleepDurationProvider: attempt => TimeSpan.FromSeconds(WaitBetweenRetriesInSeconds));

            _connectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async virtual Task<int> ExecuteAsync(string query, DynamicParameters parameters = null, CommandType commandType = CommandType.Text)
        {
            try
            {
                return await _retryPolicy.ExecuteAsync(async () =>
                {
                    using (var conn = CreateConnection())
                    {
                        return await conn.ExecuteAsync(query, parameters, commandType: commandType);
                    }
                });
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async virtual Task<ReturnResponse<T>> InsertAsync(string query, DynamicParameters parameters = null, CommandType commandType = CommandType.Text)
        {
            var returnResponse = new ReturnResponse<T>();
            try
            {
                return await _retryPolicy.ExecuteAsync(async () =>
                  {
                      using (var conn = CreateConnection())
                      {
                          returnResponse.Successful = await conn.ExecuteAsync(query, parameters, commandType: commandType) > 0;
                          return returnResponse;
                      }
                  });
            }
            catch (Exception e)
            {
                returnResponse.Successful = false;
                returnResponse.Error = e.Message;
                return returnResponse;
            }
        }

        public async virtual Task<IEnumerable<T>> QueryAsync(string query)
        {
            try
            {
                return await _retryPolicy.ExecuteAsync(async () =>
                {
                    using (var conn = CreateConnection())
                    {
                        return await conn.QueryAsync<T>(query, default);
                    }
                });
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async virtual Task<IEnumerable<T>> QueryAsync(string query, DynamicParameters parameters = null)
        {
            try
            {
                return await _retryPolicy.ExecuteAsync(async () =>
                {
                    using (var conn = CreateConnection())
                    {
                        return await conn.QueryAsync<T>(query, parameters);
                    }
                });
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async virtual Task<T> QueryFirstOrDefaultAsync(string query, DynamicParameters parameters = null)
        {
            try
            {
                return await _retryPolicy.ExecuteAsync(async () =>
                 {
                     using (var conn = CreateConnection())
                     {
                         return await conn.QueryFirstOrDefaultAsync<T>(query, parameters);
                     }
                 });
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async virtual Task<int> UpdateAsync(string query, DynamicParameters parameters = null, CommandType commandType = CommandType.Text)
        {
            try
            {
                return await _retryPolicy.ExecuteAsync(async () =>
                {
                    using (var conn = CreateConnection())
                    {
                        return await conn.ExecuteAsync(query, parameters, commandType: commandType);
                    }
                });
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}