using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CQRSDapper.Domain.Models
{
    public class ReturnResponse<T>
    {
        public T Item { get; set; }
        public bool Successful { get; set; }
        public string Error {get;set;}
    }
}
