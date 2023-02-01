using ChequePrinting.BusinessLogicLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChequePrinting.BusinessLogicLayer.Repository.RequisitionRepository
{
    public interface IRequisitionFormRepository
    {
        ResponseViewModel UpdateRequisitionFormPrintFlag(string issueID, string userID);
        IEnumerable<ChequeBookViewModel> GetReq(string issueID);
    }
}
