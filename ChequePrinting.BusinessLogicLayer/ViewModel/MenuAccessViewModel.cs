using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChequePrinting.BusinessLogicLayer.ViewModel
{
    public class MenuAccessViewModel
    {
        public int MappingId { get; set; }
        public string RoleDesc { get; set; }
        public int MenuId { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedDate { get; set; }
    }
}
