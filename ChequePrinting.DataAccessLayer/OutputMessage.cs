using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChequePrinting.DataAccessLayer
{
    public class OutputMessage
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }
    }
}
