using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChequePrinting.BusinessLogicLayer.ViewModel
{
    public class SecurityMetricsViewModel
    {
        public int Id { get; set; }
        public string Role { get; set; }
        public string MenuName { get; set; }
        public string SubMenu { get; set; }
    }
    public class SecuritySearchVM
    {
        public int CurrentPage { get; set; }
        public int PerPage { get; set; }
        public int Total { get; set; }
    }
}
