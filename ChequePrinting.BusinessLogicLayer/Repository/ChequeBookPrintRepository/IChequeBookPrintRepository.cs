using ChequePrinting.BusinessLogicLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChequePrinting.BusinessLogicLayer.Repository.ChequeBookPrintRepository
{
    public interface IChequeBookPrintRepository
    {
        IEnumerable<ChequeIssueMasterViewModel> GetChequeBookPrintItem(ChequeBookPrintSearchVM searchVM);
        IEnumerable<ChequeBookViewModel> GetChequeBooks(string issueID);
        IEnumerable<ChequeBookViewModel> GetChequeReview(string issueID);
        IEnumerable<ChequeIssueMasterViewModel> GetRequisitionPrintItem(ChequeBookPrintSearchVM searchVM);
        ResponseViewModel UpdateChequeBookPrintFlag(string issueID, string userID);
        ResponseViewModel UpdateChequeReviewPrintFlag(string issueID, string userID);

        //ResponseViewModel UpdateCheckNoAndLeave(string issueID, string NoOfLeaves, string ChequeStartNo, string userID);

        ResponseViewModel UpdateCheque(ChequeBookViewModel model);
        ResponseViewModel ReviewCheque(ChequeBookViewModel model);
        ResponseViewModel ReviewAll(ChequeBookViewModel model);
        
        ResponseViewModel UpdateRequisition(ChequeBookViewModel model);

        ResponseViewModel UndoRequisition(ChequeBookViewModel model);
        ResponseViewModel UndoCheque(ChequeBookViewModel model);
        IEnumerable<ChequeIssueMasterViewModel> GetChequeBookReview(ChequeBookPrintSearchVM searchVM);

    }
}
