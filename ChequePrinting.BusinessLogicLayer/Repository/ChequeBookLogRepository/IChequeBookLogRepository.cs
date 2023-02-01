using ChequePrinting.BusinessLogicLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChequePrinting.BusinessLogicLayer.Repository.ChequeBookLogRepository
{
    public interface IChequeBookLogRepository
    {
        IEnumerable<ChequeIssueBufferViewModel> GetBufferLogs(ChequeBookPrintSearchVM searchVM);
        bool DownloadToBufferLogs(DataTable dt);
        bool UploadDB2(DataTable dt);
        string GetLastTimeStamp();
        ResponseViewModel ClearBuffer(string userID);
        ResponseViewModel PostBufferChequeLogs();
    }
}
