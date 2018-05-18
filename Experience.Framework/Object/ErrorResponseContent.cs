using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Experience.Framework
{
    public class ErrorResponseContent
    {
        public string Message { get; set; }
        public string Solution { get; set; }
        public int EventCode { get; set; }
        public string BackUrl { get; set; }
    }
}
