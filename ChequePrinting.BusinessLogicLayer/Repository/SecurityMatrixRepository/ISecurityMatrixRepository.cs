using ChequePrinting.BusinessLogicLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChequePrinting.BusinessLogicLayer.Repository.SecurityMatrixRepository
{
    public interface ISecurityMatrixRepository
    {
        IEnumerable<SecurityMetricsViewModel> GetUserSecurityMetricsReport(SecuritySearchVM searchVM);
    }
}
