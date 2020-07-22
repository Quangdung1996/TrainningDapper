using CQRSDapper.Common.Interfaces;
using CQRSDapper.DAL.Interfaces;
using CQRSDapper.Domain.Models;
using CQRSDapper.Domain.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CQRSDapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET api/values
        [HttpGet("{customerCode}")]
        public async Task<IActionResult> Get(string customerCode)
        {
            var customer = await _customerService.GetCustomerByCustomerCodeAsync(customerCode);
            return Ok(customer);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _customerService.GetAllAsync();
            return Ok(result);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerMetaModel customerMeta)
        {
            var result = await _customerService.InsertAsync(customerMeta);
            return Ok(result);
        }

        // PUT api/values/5
        [HttpPut("{customerCode}")]
        public async Task<IActionResult> UpdateCustomer([FromBody] CustomerMeta customerMeta, string customerCode)
        {
            var result = await _customerService.UpdateAsync(customerMeta, customerCode);
            return Ok(result);
        }

        // DELETE api/values/5
        [HttpDelete("{customerCode}")]
        public async Task<IActionResult> Delete(string customerCode)
        {
            var result = await _customerService.DeleteAsync(customerCode);
            if (result > 0)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}