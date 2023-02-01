using ChequePrinting.BusinessLogicLayer.SqlQuery;
using ChequePrinting.BusinessLogicLayer.ViewModel;
using ChequePrinting.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChequePrinting.BusinessLogicLayer.Repository.MenuRepository
{
    public class MenuRepository : IMenuRepository
    {
        public IList<MenuViewModel> GetMenuList(string roleDesc)
        {
            var list = new List<MenuViewModel>();
            DataSet ds = GetMenuMaster(roleDesc);
            if (Helper.HasRowsInDataSetTable(ds))
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var model = new MenuViewModel()
                    {
                        MenuId = Helper.GetInteger(row["MenuId"]),
                        MenuName = Helper.GetDBNUllString(row["MenuName"]),
                        MenuUrl = Helper.GetDBNUllString(row["MenuUrl"]),
                        MenuParentId = Helper.GetInteger(row["MenuParentID"]),
                        MenuOrderingNo = Helper.GetInteger(row["MenuOrderingNo"]),
                        MenuClass = Helper.GetDBNUllString(row["MenuClass"]),
                        MenuIcon = Helper.GetDBNUllString(row["MenuIcon"]),
                        IsActive = Helper.GetDBNUllBoolean(row["IsActive"].ToString()),
                        SubMenuOrderNo = Helper.GetInteger(row["SubMenuOrderNo"]),
                    };
                    list.Add(model);
                }
            }
            return list;
        }
        private DataSet GetMenuMaster(string roleDesc)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@RoleDesc",roleDesc),
            };
            DataSet ds = DAO.GetDataSet("sp_GetMenuList", CommandType.StoredProcedure, param);
            return ds;
        }

        public IEnumerable<MenuAccessViewModel> GetMenuMapping(string userGroups)
        {
            var list = new List<MenuAccessViewModel>();
            DataSet ds = GetMenuMappingDataset(userGroups);
            if (Helper.HasRowsInDataSetTable(ds))
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var model = new MenuAccessViewModel()
                    {
                        MappingId = Helper.GetInteger(row["MappingId"]),
                        RoleDesc = Helper.GetDBNUllString(row["RoleDesc"]),
                        MenuId = Helper.GetInteger(row["MenuId"]),
                    };
                    list.Add(model);
                }
            }
            return list;
        }
        private DataSet GetMenuMappingDataset(string userGroups)
        {
            SqlParameter[] param = new SqlParameter[]
               {
                    new SqlParameter("@UserGroup",userGroups)
               };
            DataSet ds = DAO.GetDataSet(DbSql.sql_GetRoleMenuMapping, CommandType.Text, param);
            return ds;
        }

        public IList<MenuViewModel> GetMenuForAccessMgmt()
        {
            var list = new List<MenuViewModel>();
            DataSet ds = GetDsForAccessMgmt();
            if (Helper.HasRowsInDataSetTable(ds))
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var model = new MenuViewModel()
                    {
                        MenuId = Helper.GetInteger(row["MenuId"]),
                        MenuName = Helper.GetDBNUllString(row["MenuName"]),
                        MenuUrl = Helper.GetDBNUllString(row["MenuUrl"]),
                        MenuParentId = Helper.GetInteger(row["MenuParentID"]),
                        MenuOrderingNo = Helper.GetInteger(row["MenuOrderingNo"]),
                        MenuClass = Helper.GetDBNUllString(row["MenuClass"]),
                        MenuIcon = Helper.GetDBNUllString(row["MenuIcon"]),
                        IsActive = Helper.GetDBNUllBoolean(row["IsActive"].ToString()),
                        SubMenuOrderNo = Helper.GetInteger(row["SubMenuOrderNo"]),
                    };
                    list.Add(model);
                }
            }
            return list;
        }

        private DataSet GetDsForAccessMgmt()
        {
            DataSet ds = DAO.GetDataSet(DbSql.sql_GetAllMenu, CommandType.Text, null);
            return ds;
        }

        public ResponseViewModel SaveRoleMenuMapping(MenuAccessViewModel model)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[]
                {
                     new SqlParameter("@MenuId",model.MenuId),
                     new SqlParameter("@RoleDesc",model.RoleDesc),
                };
                int result = DAO.IDU("sp_SaveRoleMenuMapping", CommandType.StoredProcedure, param);
                return new ResponseViewModel() { Message = MsgBox.save_msg, Status = MsgBox.success_status };
            }
            catch (Exception ex)
            {
                return new ResponseViewModel() { Exception = ex, Message = ex.Message, Status = MsgBox.failed_status };
            }
        }
        public ResponseViewModel DeleteMenuAccess(string userGroup)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@RoleDesc",userGroup),
                };
                int result = DAO.IDU(DbSql.sql_DeleteRoleMenuMapping, CommandType.Text, param);
                return new ResponseViewModel() { Message = MsgBox.delete_msg, Status = MsgBox.success_status };
            }
            catch (Exception ex)
            {
                return new ResponseViewModel() { Exception = ex, Message = ex.Message, Status = MsgBox.failed_status };
            }

        }

    }
}
