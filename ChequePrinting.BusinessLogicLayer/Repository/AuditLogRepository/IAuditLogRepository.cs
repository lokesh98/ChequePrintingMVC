using ChequePrinting.BusinessLogicLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChequePrinting.BusinessLogicLayer.Repository.AuditLogRepository
{
    public interface IAuditLogRepository
    {
        ResponseViewModel AddAuditLog(AuditLogViewModel model);
        IEnumerable<AuditLogViewModel> GetAuditLogs(AuditSearchVM searchVM);
    }
}
