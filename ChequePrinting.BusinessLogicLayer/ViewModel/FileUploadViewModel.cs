using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChequePrinting.BusinessLogicLayer.ViewModel
{
    public class FileUploadViewModel
    {
        public string Document { get; set; }
        public string DocumentName { get; set; }
        public string DocumentType { get; set; }
        public int DocumentSize { get; set; }
    }
}
