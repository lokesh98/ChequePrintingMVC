using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChequePrinting.BusinessLogicLayer.ViewModel
{
    public class MenuViewModel
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string MenuUrl { get; set; }
        public int MenuParentId { get; set; }
        public int MenuOrderingNo { get; set; }
        public string MenuClass { get; set; }
        public string MenuIcon { get; set; }
        public bool IsActive { get; set; }
        public int SubMenuOrderNo { get; set; }
    }
}
