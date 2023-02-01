using ChequePrinting.BusinessLogicLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChequePrinting.BusinessLogicLayer.Repository.UserRepository
{
    public interface IUserRepository
    {
        IEnumerable<UserViewModel> GetUserList(UserSearchVM searchVM);
        ResponseViewModel AddNewUser(UserViewModel model);
        ResponseViewModel UpdateUser(UserViewModel model);
        ResponseViewModel DeleteUser(string userId);
        UserViewModel GetUserByUserID(string userId);
    }
}
