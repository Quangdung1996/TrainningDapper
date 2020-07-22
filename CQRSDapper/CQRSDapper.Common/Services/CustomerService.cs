using AutoMapper;
using CQRSDapper.Common.Interfaces;
using CQRSDapper.DAL.Interfaces;
using CQRSDapper.Domain.Models;
using CQRSDapper.Domain.Models.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CQRSDapper.Common.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<ReturnResponse<Customer>> InsertAsync(CustomerMetaModel customerMeta)
        {
            var customer = _mapper.Map<Customer>(customerMeta);
            return await _customerRepository.InsertAsync(customer);
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _customerRepository.GetAllAsync();
        }

        public async Task<Customer> GetCustomerByCustomerCodeAsync(string CustomerCode)
        {
            return await _customerRepository.GetByCustomerCodeAsync(CustomerCode);
        }

        public async Task<ReturnResponse<CustomerMeta>> UpdateAsync(CustomerMeta customerMeta, string customerCode)
        {
            return await _customerRepository.UpdateAsync(customerMeta, customerCode);
        }

        public async Task<int> DeleteAsync(string customerCode)
        {
            return await _customerRepository.DeleteAsync(customerCode);
        }
    }
}