using ChequePrinting.BusinessLogicLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChequePrinting.BusinessLogicLayer.Repository.AccountRepository
{
    public interface IAccountRepository
    {
        LoginViewModel GetUserByUserID(string userId);
        bool ValidateUser(string userId);       
        ResponseViewModel UpdateLoginInfo(string userID, string flag, int failedAttept);
    }
}
