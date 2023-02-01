using ChequePrinting.BusinessLogicLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChequePrinting.BusinessLogicLayer.Repository.ConfigRepository
{
    public interface IConfigRepository
    {
        IEnumerable<UserRolesViewModel> GetUserRoles();
        IEnumerable<BranchViewModel> GetBranches();
        IEnumerable<UserStatusViewModel> GetUserStatus();
        IEnumerable<IslockedViewModel> GetIsLocked();
    }
}
