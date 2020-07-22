using CQRSDapper.Domain.Models;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace CQRSDapper.DAL.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<ReturnResponse<T>> InsertAsync(string query, DynamicParameters parameters = null, CommandType commandType = CommandType.Text);

        Task<int> UpdateAsync(string query, DynamicParameters parameters = null, CommandType commandType = CommandType.Text);

        Task<IEnumerable<T>> QueryAsync(string query);

        Task<IEnumerable<T>> QueryAsync(string query, DynamicParameters parameters = null);

        Task<int> ExecuteAsync(string query, DynamicParameters parameters = null, CommandType commandType = CommandType.Text);

        Task<T> QueryFirstOrDefaultAsync(string query, DynamicParameters parameters = null);
    }
}