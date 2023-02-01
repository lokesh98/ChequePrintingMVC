using ChequePrinting.BusinessLogicLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChequePrinting.BusinessLogicLayer.Repository.DailyChequeBookPrintRptRepository
{
    public interface IDailyChequeBookPrintRptRepository
    {
        IEnumerable<DailyChequeBookReportViewModel> GetDailyChequePrintReport(DailyChequeBookRptSearchVM searchVM);

        ResponseViewModel DestroyAccount(DailyChequeBookRptSearchVM searchVM);
        IEnumerable<DailyChequeBookReportViewModel> SendSMS(DailyChequeBookRptSearchVM searchVM);

    }
}
