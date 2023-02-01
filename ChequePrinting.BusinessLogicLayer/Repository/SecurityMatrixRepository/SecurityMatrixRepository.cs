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

namespace ChequePrinting.BusinessLogicLayer.Repository.SecurityMatrixRepository
{
    public class SecurityMatrixRepository : ISecurityMatrixRepository
    {
        public IEnumerable<SecurityMetricsViewModel> GetUserSecurityMetricsReport(SecuritySearchVM searchVM)
        {
            var list = new List<SecurityMetricsViewModel>();
            DataSet ds = GetSecurityMatrixDataSet(searchVM);
            if (Helper.HasRowsInDataSetTable(ds))
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var mode = new SecurityMetricsViewModel()
                    {
                        Id = Helper.GetInteger(row["RecNo"]),
                        Role = Helper.GetDBNUllString(row["RoleDesc"]),
                        MenuName = Helper.GetDBNUllString(row["MainMenu"]),
                        SubMenu = Helper.GetDBNUllString(row["SubMenu"]),
                    };
                    list.Add(mode);
                }
            }
            searchVM.Total = Helper.GetInteger(ds.Tables[1].Rows[0]["TotalRows"]);
            return list;
        }
        private DataSet GetSecurityMatrixDataSet(SecuritySearchVM searchVM)
        {
            int startRow = (searchVM.CurrentPage - 1) * searchVM.PerPage + 1;
            int endRow = startRow + searchVM.PerPage - 1;
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Start",startRow),
                new SqlParameter("@Last",endRow)
            };
            DataSet ds = DAO.GetDataSet("sp_GetSecurityMatrix", CommandType.StoredProcedure, param);
            return ds;
        }
    }
}
