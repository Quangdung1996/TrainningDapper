using System;
using System.Collections.Generic;
using System.Text;

namespace CQRSDapper.Domain.Models.Dto
{
    public class CustomerMeta
    {
        public int IndustryId { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string ContactInfo { get; set; }
        public string Note { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime Modified { get; set; }
        public int Deleted { get; set; }
    }
}
