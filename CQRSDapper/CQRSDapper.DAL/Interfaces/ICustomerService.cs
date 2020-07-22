using CQRSDapper.Domain.Models;
using CQRSDapper.Domain.Models.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CQRSDapper.DAL.Interfaces
{
    public interface ICustomerService
    {
        Task<ReturnResponse<Customer>> InsertAsync(CustomerMetaModel customerMeta);

        Task<Customer> GetCustomerByCustomerCodeAsync(string customerCode);

        Task<IEnumerable<Customer>> GetAllAsync();
        Task<ReturnResponse<CustomerMeta>> UpdateAsync(CustomerMeta customerMeta, string customerCode);
        Task<int> DeleteAsync(string customerCode);
    }
}