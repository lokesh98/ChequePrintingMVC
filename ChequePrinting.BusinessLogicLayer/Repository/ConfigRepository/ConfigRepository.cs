using ChequePrinting.BusinessLogicLayer.SqlQuery;
using ChequePrinting.BusinessLogicLayer.ViewModel;
using ChequePrinting.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChequePrinting.BusinessLogicLayer.Repository.ConfigRepository
{
    public class ConfigRepository : IConfigRepository
    {
        public IEnumerable<BranchViewModel> GetBranches()
        {
            var list = new List<BranchViewModel>();
            DataSet ds = GetBranchDS();
            if (Helper.HasRowsInDataSetTable(ds))
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    list.Add(new BranchViewModel()
                    {
                        BranchCode = Helper.GetDBNUllString(row["BRANCHCODE"]),
                        BranchName = Helper.GetDBNUllString(row["BRANCHNAME"]),
                    });
                }
            }
            return list;
        }

        public IEnumerable<UserRolesViewModel> GetUserRoles()
        {
            var list = new List<UserRolesViewModel>();
            DataSet ds = GetUserRoleDS();
            if (Helper.HasRowsInDataSetTable(ds))
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    list.Add(new UserRolesViewModel()
                    {
                        RoleId = Helper.GetInteger(row["RoleId"]),
                        RoleName = Helper.GetDBNUllString(row["RoleDesc"]),
                    });
                }
            }
            return list;
        }

        public IEnumerable<IslockedViewModel> GetIsLocked()
        {
            var list = new List<IslockedViewModel>();
            DataSet ds = GetIsLockedDS();
            if (Helper.HasRowsInDataSetTable(ds))
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    list.Add(new IslockedViewModel()
                    {
                        IsLockedId = Helper.GetDBNUllString(row["OPTION_ID"]),
                        IslockedName = Helper.GetDBNUllString(row["OPTION_VALUE"])
                }  );
                }
            }
            return list;
        }

        private DataSet GetIsLockedDS()
        {
            DataSet ds = DAO.GetDataSet(DbSql.sql_GetIsLocked, CommandType.Text, null);
            return ds;
        }
        private DataSet GetUserRoleDS()
        {
            DataSet ds = DAO.GetDataSet(DbSql.sql_GetUserRoles, CommandType.Text, null);
            return ds;
        }
        private DataSet GetBranchDS()
        {
            DataSet ds = DAO.GetDataSet(DbSql.sql_GetBranches, CommandType.Text, null);
            return ds;
        }


     
        public IEnumerable<UserStatusViewModel> GetUserStatus()
        {
            List<UserStatusViewModel> list = new List<UserStatusViewModel>();
            var data = DAO.Getlist(DbSql.sql_GetUserStatus, CommandType.Text, null);
            foreach (DataRow dr in data)
            {
                list.Add(new UserStatusViewModel()
                {
                    StatusId = Helper.GetInteger(dr["OPTION_ID"]),
                    StatusName = Helper.GetDBNUllString(dr["OPTION_VALUE"])
                });
            }
            return list;
        }
    }
}
