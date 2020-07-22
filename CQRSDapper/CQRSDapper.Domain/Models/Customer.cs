using System;

namespace CQRSDapper.Domain.Models
{
    public class Customer
    {
        public int RowId { get; set; }
        public int IndustryId { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string ContactInfo { get; set; }
        public string Note { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public int Deleted { get; set; }

        public Customer()
        {
            Created = DateTime.Now;
        }
    }
}