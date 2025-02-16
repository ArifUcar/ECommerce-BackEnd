using AU_Framework.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AU_Framework.Domain.Entities
{
    public sealed class ErrorLog : BaseEntity
    {
        public string ErrorMessage { get; set; }
        public string StackTrace {get;set;}

        public string RequestPath { get; set; }

        public string RequestMethod { get; set; }

        public DateTime Timestamp { get; set; }

    }
}
