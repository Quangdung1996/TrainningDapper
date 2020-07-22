using CQRSDapper.DAL.Interfaces;
using CQRSDapper.DAL.Repositories;
using CQRSDapper.Domain.Models;
using CQRSDapper.Domain.Models.Dto;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace CQRSDapper.DAL.Repository
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<int> DeleteAsync(string customerCode)
        {
            var query = "DELETE FROM Customers WHERE CustomerCode=@CustomerCode";
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@CustomerCode", customerCode, DbType.String);
            return await this.ExecuteAsync(query, dynamicParameters);
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            var query = "Select * FROM Customers";
            return await this.QueryAsync(query);
        }

        public async Task<Customer> GetByCustomerCodeAsync(string customerCode)
        {
            var query = "Select * FROM Customers WHERE CustomerCode=@CustomerCode";
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@CustomerCode", customerCode, DbType.String);
            return await this.QueryFirstOrDefaultAsync(query, dynamicParameters);
        }

        public async Task<ReturnResponse<Customer>> InsertAsync(Customer customer)
        {
            var query = "INSERT INTO Customers (IndustryId, CustomerCode, CustomerName, Address, ContactInfo,Note ,CreatedBy,ModifiedBy,Created,Modified,Deleted)" +
                " VALUES (@IndustryId, @CustomerCode, @CustomerName, @Address, @ContactInfo,@Note ,@CreatedBy,@ModifiedBy,@Created,@Modified,@Deleted)";
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@IndustryId", customer.IndustryId, DbType.Int32);
            dynamicParameters.Add("@CustomerCode", customer.CustomerCode, DbType.String);
            dynamicParameters.Add("@CustomerName", customer.CustomerName, DbType.String);
            dynamicParameters.Add("@Address", customer.Address, DbType.String);
            dynamicParameters.Add("@ContactInfo", customer.ContactInfo, DbType.String);
            dynamicParameters.Add("@Note", customer.Note, DbType.String);
            dynamicParameters.Add("@CreatedBy", customer.CreatedBy, DbType.String);
            dynamicParameters.Add("@ModifiedBy", customer.ModifiedBy, DbType.String);
            dynamicParameters.Add("@Created", customer.Created, DbType.DateTime);
            dynamicParameters.Add("@Modified", customer.Modified, DbType.DateTime);
            dynamicParameters.Add("@Deleted", customer.Deleted, DbType.Int32);
            var returnResponse = await this.InsertAsync(query, dynamicParameters);
            if (returnResponse.Successful)
            {
                returnResponse.Item = customer;
            }

            return returnResponse;
        }

        public async Task<ReturnResponse<CustomerMeta>> UpdateAsync(CustomerMeta customerMeta, string customerCode)
        {
            var customerFilter = await GetByCustomerCodeAsync(customerCode);
            if (customerFilter is null)
            {
                return new ReturnResponse<CustomerMeta>
                {
                    Successful = false,
                    Error = $"Could find customer with CustomerCode={customerCode}"
                };
            }
           
            var query = "Update Customers SET IndustryId=@IndustryId, CustomerName=@CustomerName, Address=@Address, ContactInfo=@ContactInfo,Note=@Note WHERE CustomerCode=@CustomerCode";
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@IndustryId", customerMeta.IndustryId, DbType.Int32);
            dynamicParameters.Add("@CustomerCode", customerCode, DbType.String);
            dynamicParameters.Add("@CustomerName", customerMeta.CustomerName, DbType.String);
            dynamicParameters.Add("@Address", customerMeta.Address, DbType.String);
            dynamicParameters.Add("@ContactInfo", customerMeta.ContactInfo, DbType.String);
            dynamicParameters.Add("@Note", customerMeta.Note, DbType.String);
            var result = await this.UpdateAsync(query, dynamicParameters);
            var returnResponse = new ReturnResponse<CustomerMeta>();
            if (result > 0)
            {
                returnResponse.Item = customerMeta;
                returnResponse.Successful = true;
            }
            return returnResponse;
        }
    }
}