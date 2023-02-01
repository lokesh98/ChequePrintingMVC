using ChequePrinting.BusinessLogicLayer.ViewModel;
using ChequePrinting.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChequePrinting.BusinessLogicLayer.Repository.ChequeBookPrintRepository
{
    public class ChequeBookPrintRepository : IChequeBookPrintRepository
    {
        public IEnumerable<ChequeIssueMasterViewModel> GetChequeBookPrintItem(ChequeBookPrintSearchVM searchVM)
        {
            var list = new List<ChequeIssueMasterViewModel>();
            DataSet ds = GetChequeBookPrintDS(searchVM);
            MapChequeData(list, ds);
            searchVM.Total = Helper.GetInteger(ds.Tables[1].Rows[0]["TotalRows"]);
            return list;
        }

        public IEnumerable<ChequeIssueMasterViewModel> GetChequeBookReview(ChequeBookPrintSearchVM searchVM)
        {
            var list = new List<ChequeIssueMasterViewModel>();
            DataSet ds = GetChequeBookReviewDS(searchVM);
            MapChequeReview(list, ds);
            searchVM.Total = Helper.GetInteger(ds.Tables[1].Rows[0]["TotalRows"]);
            return list;
        }
        private void MapReqData(List<ChequeIssueMasterViewModel> list, DataSet ds)
        {
            if (Helper.HasRowsInDataSetTable(ds))
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var model = new ChequeIssueMasterViewModel()
                    {
                        IssueID = Helper.GetInteger(row["ISSUEID"]),
                        CurrencyCode = Helper.GetDBNUllString(row["CURRENCYCODE"]),
                        AccountNo = Helper.GetDBNUllString(row["ACCOUNTNO"]),
                        OnlineBanking = Helper.GetDBNUllString(row["ONLINEBANKING"]),
                        Branch = new BranchViewModel() { BranchCode = Helper.GetDBNUllString(row["BRANCHCODE"]), BranchName = Helper.GetDBNUllString(row["BRANCHNAME"]) },
                        FullName = Helper.GetDBNUllString(row["FULLNAME"]),
                        RelationShipNo = Helper.GetDBNUllString(row["RELATIONSHIPNO"]),
                        RelationShipType = Helper.GetDBNUllString(row["RELATIONSHIPTYPE"]),
                        ChequeStartNo = Helper.GetInteger(row["CHQSTARTNO"]),
                        IssueDate = Helper.GetDBNUllDateString(row["ISSUEDATE"], Settings.ShortDateFormat),
                        ReqID = Helper.GetDBNUllString(row["REQID"]),
                        NoOfLeaves = Helper.GetInteger(row["NOOFLEAVES"]),
                        BookStatus = Helper.GetDBNUllString(row["BOOKSTATUS"]),
                        MakerID = Helper.GetDBNUllString(row["MAKERID"]),
                        MakerDate = Helper.GetDBNUllString(row["MAKERDATE"]),
                        MakerTime = Helper.GetDBNUllString(row["MAKERTIME"]),
                        MakerDateTime = Helper.GetDBNUllString(row["MAKERDATETIME"]),
                        MakerIPAddress = Helper.GetDBNUllString(row["MAKERIPADDRESS"]),
                        CheckerID = Helper.GetDBNUllString(row["CHECKERID"]),
                        CheckerDate = Helper.GetDBNUllString(row["CHECKERDATE"]),
                        CheckerTime = Helper.GetDBNUllString(row["CHECKERTIME"]),
                        CheckerDateTime = Helper.GetDBNUllString(row["CHECKERDATETIME"]),
                        CheckerIPAddress = Helper.GetDBNUllString(row["CHECKERIPADDRESS"]),
                        StatusFlag = Helper.GetDBNUllString(row["STATUSFLAG"]),
                        NoOfTimesPrinted = Helper.GetInteger(row["NOOFTIMESPRINTED"]),
                        PrintedDate = Helper.GetDBNUllDateString(row["PRINTEDDATE"], Settings.ShortDateFormat),
                        LStatus = Helper.GetDBNUllString(row["L_STATUS"]),
                        LPrintedBy = Helper.GetDBNUllString(row["L_PRINTEDBY"]),
                        LPrintedDate = Helper.GetDBNUllString(row["L_PRINTEDDATE"]),
                        LNoOfPrint = Helper.GetInteger(row["L_NOOFPRINT"]),
                        LReviewedBy = Helper.GetDBNUllString(row["L_REVIEWEDBY"]),
                        LReviewedDate = Helper.GetDBNUllString(row["L_REVIEWEDDATE"]),
                        CStatus = Helper.GetDBNUllString(row["C_STATUS"]),
                        CPrintedBy = Helper.GetDBNUllString(row["C_PRINTEDBY"]),
                        CPrintedDate = Helper.GetDBNUllDateString(row["C_PRINTEDDATE"], Settings.ShortDateFormat),
                        CNoOfPrint = Helper.GetInteger(row["C_NOOFPRINT"]),
                        FlatNo = Helper.GetDBNUllString(row["FLATNO"]),
                        BldgName = Helper.GetDBNUllString(row["BLDGNAME"]),
                        NrLandMark = Helper.GetDBNUllString(row["NRLANDMRK"]),
                        Street = Helper.GetDBNUllString(row["STREET"]),
                        TRS = Helper.GetDBNUllString(row["TRS"]),
                        Mobile = Helper.GetDBNUllString(row["MOB"]),
                        InfoCode = Helper.GetDBNUllString(row["INFOCODE"]),

                    };
                    list.Add(model);

                }
            }
        }
        private void MapChequeData(List<ChequeIssueMasterViewModel> list, DataSet ds)
        {
            if (Helper.HasRowsInDataSetTable(ds))
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var model = new ChequeIssueMasterViewModel()
                    {
                        IssueID = Helper.GetInteger(row["ISSUEID"]),
                        CurrencyCode = Helper.GetDBNUllString(row["CURRENCYCODE"]),
                        AccountNo = Helper.GetDBNUllString(row["ACCOUNTNO"]),
                        Branch = new BranchViewModel() { BranchCode = Helper.GetDBNUllString(row["BRANCHCODE"]), BranchName = Helper.GetDBNUllString(row["BRANCHNAME"]) },
                        FullName = Helper.GetDBNUllString(row["FULLNAME"]),
                        RelationShipNo = Helper.GetDBNUllString(row["RELATIONSHIPNO"]),
                        RelationShipType = Helper.GetDBNUllString(row["RELATIONSHIPTYPE"]),
                        ChequeStartNo = Helper.GetInteger(row["CHQSTARTNO"]),
                        IssueDate = Helper.GetDBNUllDateString(row["ISSUEDATE"], Settings.ShortDateFormat),
                        ReqID = Helper.GetDBNUllString(row["REQID"]),
                        NoOfLeaves = Helper.GetInteger(row["NOOFLEAVES"]),
                        BookStatus = Helper.GetDBNUllString(row["BOOKSTATUS"]),
                        MakerID = Helper.GetDBNUllString(row["MAKERID"]),
                        MakerDate = Helper.GetDBNUllString(row["MAKERDATE"]),
                        MakerTime = Helper.GetDBNUllString(row["MAKERTIME"]),
                        MakerDateTime = Helper.GetDBNUllString(row["MAKERDATETIME"]),
                        MakerIPAddress = Helper.GetDBNUllString(row["MAKERIPADDRESS"]),
                        CheckerID = Helper.GetDBNUllString(row["CHECKERID"]),
                        CheckerDate = Helper.GetDBNUllString(row["CHECKERDATE"]),
                        CheckerTime = Helper.GetDBNUllString(row["CHECKERTIME"]),
                        CheckerDateTime = Helper.GetDBNUllString(row["CHECKERDATETIME"]),
                        CheckerIPAddress = Helper.GetDBNUllString(row["CHECKERIPADDRESS"]),
                        StatusFlag = Helper.GetDBNUllString(row["STATUSFLAG"]),
                        NoOfTimesPrinted = Helper.GetInteger(row["NOOFTIMESPRINTED"]),
                        PrintedDate = Helper.GetDBNUllDateString(row["PRINTEDDATE"], Settings.ShortDateFormat),
                        LStatus = Helper.GetDBNUllString(row["L_STATUS"]),
                        LPrintedBy = Helper.GetDBNUllString(row["L_PRINTEDBY"]),
                        LPrintedDate = Helper.GetDBNUllString(row["L_PRINTEDDATE"]),
                        LNoOfPrint = Helper.GetInteger(row["L_NOOFPRINT"]),
                        LReviewedBy = Helper.GetDBNUllString(row["L_REVIEWEDBY"]),
                        LReviewedDate = Helper.GetDBNUllString(row["L_REVIEWEDDATE"]),
                        CStatus = Helper.GetDBNUllString(row["C_STATUS"]),
                        CPrintedBy = Helper.GetDBNUllString(row["C_PRINTEDBY"]),
                        CPrintedDate = Helper.GetDBNUllDateString(row["C_PRINTEDDATE"], Settings.ShortDateFormat),
                        CNoOfPrint = Helper.GetInteger(row["C_NOOFPRINT"]),
                        FlatNo = Helper.GetDBNUllString(row["FLATNO"]),
                        BldgName = Helper.GetDBNUllString(row["BLDGNAME"]),
                        NrLandMark = Helper.GetDBNUllString(row["NRLANDMRK"]),
                        Street = Helper.GetDBNUllString(row["STREET"]),
                        TRS = Helper.GetDBNUllString(row["TRS"]),
                        Mobile = Helper.GetDBNUllString(row["MOB"]),
                        InfoCode = Helper.GetDBNUllString(row["INFOCODE"]),

                    };
                    list.Add(model);

                }
            }
        }

        private void MapChequeReview(List<ChequeIssueMasterViewModel> list, DataSet ds)
        {
            if (Helper.HasRowsInDataSetTable(ds))
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var model = new ChequeIssueMasterViewModel()
                    {
                        IssueID = Helper.GetInteger(row["ISSUEID"]),
                        CurrencyCode = Helper.GetDBNUllString(row["CURRENCYCODE"]),
                        AccountNo = Helper.GetDBNUllString(row["ACCOUNTNO"]),
                        Branch = new BranchViewModel() { BranchCode = Helper.GetDBNUllString(row["BRANCHCODE"]), BranchName = Helper.GetDBNUllString(row["BRANCHNAME"]) },
                        FullName = Helper.GetDBNUllString(row["FULLNAME"]),
                        RelationShipNo = Helper.GetDBNUllString(row["RELATIONSHIPNO"]),
                        RelationShipType = Helper.GetDBNUllString(row["RELATIONSHIPTYPE"]),
                        ChequeStartNo = Helper.GetInteger(row["CHQSTARTNO"]),
                        IssueDate = Helper.GetDBNUllDateString(row["ISSUEDATE"], Settings.ShortDateFormat),
                        ReqID = Helper.GetDBNUllString(row["REQID"]),
                        NoOfLeaves = Helper.GetInteger(row["NOOFLEAVES"]),
                        BookStatus = Helper.GetDBNUllString(row["BOOKSTATUS"]),
                        MakerID = Helper.GetDBNUllString(row["MAKERID"]),
                        MakerDate = Helper.GetDBNUllString(row["MAKERDATE"]),
                        MakerTime = Helper.GetDBNUllString(row["MAKERTIME"]),
                        MakerDateTime = Helper.GetDBNUllString(row["MAKERDATETIME"]),
                        MakerIPAddress = Helper.GetDBNUllString(row["MAKERIPADDRESS"]),
                        CheckerID = Helper.GetDBNUllString(row["CHECKERID"]),
                        CheckerDate = Helper.GetDBNUllString(row["CHECKERDATE"]),
                        CheckerTime = Helper.GetDBNUllString(row["CHECKERTIME"]),
                        CheckerDateTime = Helper.GetDBNUllString(row["CHECKERDATETIME"]),
                        CheckerIPAddress = Helper.GetDBNUllString(row["CHECKERIPADDRESS"]),
                        StatusFlag = Helper.GetDBNUllString(row["STATUSFLAG"]),
                        NoOfTimesPrinted = Helper.GetInteger(row["NOOFTIMESPRINTED"]),
                        PrintedDate = Helper.GetDBNUllDateString(row["PRINTEDDATE"], Settings.ShortDateFormat),
                        LStatus = Helper.GetDBNUllString(row["L_STATUS"]),
                        LPrintedBy = Helper.GetDBNUllString(row["L_PRINTEDBY"]),
                        LPrintedDate = Helper.GetDBNUllString(row["L_PRINTEDDATE"]),
                        LNoOfPrint = Helper.GetInteger(row["L_NOOFPRINT"]),
                        LReviewedBy = Helper.GetDBNUllString(row["L_REVIEWEDBY"]),
                        LReviewedDate = Helper.GetDBNUllString(row["L_REVIEWEDDATE"]),
                        CStatus = Helper.GetDBNUllString(row["C_STATUS"]),
                        CPrintedBy = Helper.GetDBNUllString(row["C_PRINTEDBY"]),
                        CPrintedDate = Helper.GetDBNUllDateString(row["C_PRINTEDDATE"], Settings.ShortDateFormat),
                        CNoOfPrint = Helper.GetInteger(row["C_NOOFPRINT"]),
                        FlatNo = Helper.GetDBNUllString(row["FLATNO"]),
                        BldgName = Helper.GetDBNUllString(row["BLDGNAME"]),
                        NrLandMark = Helper.GetDBNUllString(row["NRLANDMRK"]),
                        Street = Helper.GetDBNUllString(row["STREET"]),
                        TRS = Helper.GetDBNUllString(row["TRS"]),
                        Mobile = Helper.GetDBNUllString(row["MOB"]),
                        InfoCode = Helper.GetDBNUllString(row["INFOCODE"]),

                    };
                    list.Add(model);

                }
            }
        }
        public IEnumerable<ChequeBookViewModel> GetChequeBooks(string issueID)
        {
            var list = new List<ChequeBookViewModel>();
           
            DataSet ds = GetChequeBookDS(issueID);
            if (Helper.HasRowsInDataSetTable(ds))
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var model = new ChequeBookViewModel()
                    {
                        IssueID = Helper.GetInteger(row["ISSUEID"]),
                        AccountNo = Helper.GetDBNUllString(row["ACCOUNTNO"]),
                        Branch = new BranchViewModel() { BranchCode = Helper.GetDBNUllString(row["BRANCHCODE"]), BranchName = Helper.GetDBNUllString(row["BRANCHNAME"]) },
                        AccountNo1 = Helper.GetDBNUllString(row["ACCOUNTNO1"]),
                        CustomerName = Helper.GetDBNUllString(row["CUSTOMERNAME"]),
                        BankCode = Helper.GetDBNUllString(row["BANKCODE"]),
                        RelationShipType = Helper.GetDBNUllString(row["RELATIONSHIPTYPE"]),
                        NoOfLeaves = Helper.GetDBNUllString(row["NOOFLEAVES"]),
                        CheckerID = Helper.GetDBNUllString(row["CHECKERID"]),                      
                        FlatNo = Helper.GetDBNUllString(row["FLATNO"]),
                        BldgName = Helper.GetDBNUllString(row["BLDGNAME"]),
                        NRLandMark = Helper.GetDBNUllString(row["NRLANDMRK"]),
                        Street = Helper.GetDBNUllString(row["STREET"]),
                        TRS = Helper.GetDBNUllString(row["TRS"]),
                        Mobile = Helper.GetDBNUllString(row["MOB"]),
                        ChequeStartNo = Helper.GetDBNUllString(row["CHQSTARTNO"]),
                        ChequeNo1 = Helper.GetDBNUllString(row["ChqNo_1"]),
                        ChequeNo2 = Helper.GetDBNUllString(row["ChqNo_2"]),
                        ChequeNo3 = Helper.GetDBNUllString(row["ChqNo_3"]),

                    };
                    list.Add(model);

                }
            }
            return list;
        }
        public IEnumerable<ChequeBookViewModel> GetChequeReview(string issueID)
        {
            var list = new List<ChequeBookViewModel>();

            DataSet ds = GetChequeBookReviewDS(issueID);
            if (Helper.HasRowsInDataSetTable(ds))
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var model = new ChequeBookViewModel()
                    {
                        IssueID = Helper.GetInteger(row["ISSUEID"]),
                        AccountNo = Helper.GetDBNUllString(row["ACCOUNTNO"]),
                        Branch = new BranchViewModel() { BranchCode = Helper.GetDBNUllString(row["BRANCHCODE"]), BranchName = Helper.GetDBNUllString(row["BRANCHNAME"]) },
                        AccountNo1 = Helper.GetDBNUllString(row["ACCOUNTNO1"]),
                        CustomerName = Helper.GetDBNUllString(row["CUSTOMERNAME"]),
                        BankCode = Helper.GetDBNUllString(row["BANKCODE"]),
                        RelationShipType = Helper.GetDBNUllString(row["RELATIONSHIPTYPE"]),
                        NoOfLeaves = Helper.GetDBNUllString(row["NOOFLEAVES"]),
                        CheckerID = Helper.GetDBNUllString(row["CHECKERID"]),
                        FlatNo = Helper.GetDBNUllString(row["FLATNO"]),
                        BldgName = Helper.GetDBNUllString(row["BLDGNAME"]),
                        NRLandMark = Helper.GetDBNUllString(row["NRLANDMRK"]),
                        Street = Helper.GetDBNUllString(row["STREET"]),
                        TRS = Helper.GetDBNUllString(row["TRS"]),
                        Mobile = Helper.GetDBNUllString(row["MOB"]),
                        ChequeStartNo = Helper.GetDBNUllString(row["CHQSTARTNO"]),
                        ChequeNo1 = Helper.GetDBNUllString(row["ChqNo_1"]),
                        ChequeNo2 = Helper.GetDBNUllString(row["ChqNo_2"]),
                        ChequeNo3 = Helper.GetDBNUllString(row["ChqNo_3"]),

                    };
                    list.Add(model);

                }
            }
            return list;
        }
        public IEnumerable<ChequeIssueMasterViewModel> GetRequisitionPrintItem(ChequeBookPrintSearchVM searchVM)
        {
            var list = new List<ChequeIssueMasterViewModel>();
            DataSet ds = GetRequisitionDS(searchVM);
            MapReqData(list, ds);
            //searchVM.Total = Helper.GetInteger(ds.Tables[1].Rows[0]["TotalRows"]);
            return list;
        }

        #region PrivateDataset
        private DataSet GetChequeBookDS(string issueID)
        {
            SqlParameter[] param = new SqlParameter[]
             {
                new SqlParameter("@ChqBookID",issueID)
             };
            DataSet ds = DAO.GetDataSet("p_GetChqBookItemToPrint", CommandType.StoredProcedure, param);
            return ds;
        }

        private DataSet GetChequeBookReviewDS(string issueID)
        {
            SqlParameter[] param = new SqlParameter[]
             {
                new SqlParameter("@ChqBookID",issueID)
             };
            DataSet ds = DAO.GetDataSet("p_GetChqBookItemToPrint", CommandType.StoredProcedure, param);
            return ds;
        }

        private DataSet GetChequeBookPrintDS(ChequeBookPrintSearchVM searchVM)
        {
            int startRow = (searchVM.CurrentPage - 1) * searchVM.PerPage + 1;
            int endRow = startRow + searchVM.PerPage - 1;
            if (searchVM.ChequeType == "Both")
            {
                searchVM.ChequeType = "";
            }
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@UserID",searchVM.UserID),
                new SqlParameter("@FilterByMakerID",searchVM.FilterByMakerID),
                new SqlParameter("@RelationShipType", string.IsNullOrEmpty(searchVM.ChequeType) ? (object)DBNull.Value : searchVM.ChequeType),
                new SqlParameter("@Start",startRow),
                new SqlParameter("@Last",endRow)
            };
            DataSet ds = DAO.GetDataSet("sp_GetChqueBookPrintItem_NEW", CommandType.StoredProcedure, param);
            return ds;
        }

        private DataSet GetChequeBookReviewDS(ChequeBookPrintSearchVM searchVM)
        {
            int startRow = (searchVM.CurrentPage - 1) * searchVM.PerPage + 1;
            int endRow = startRow + searchVM.PerPage - 1;
            if (searchVM.ChequeType == "Both")
            {
                searchVM.ChequeType = "";
            }
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@UserID",searchVM.UserID),
                new SqlParameter("@FilterByMakerID",searchVM.FilterByMakerID),
                new SqlParameter("@RelationShipType", string.IsNullOrEmpty(searchVM.ChequeType) ? (object)DBNull.Value : searchVM.ChequeType),
                new SqlParameter("@Start",startRow),
                new SqlParameter("@Last",endRow)
            };
            DataSet ds = DAO.GetDataSet("sp_GetChqBookReviewList_New", CommandType.StoredProcedure, param);
            return ds;
        }
        private DataSet GetRequisitionDS(ChequeBookPrintSearchVM searchVM)
        {
            int startRow = (searchVM.CurrentPage - 1) * searchVM.PerPage + 1;
            int endRow = startRow + searchVM.PerPage - 1;
            if (searchVM.ChequeType == "Both")
            {
                searchVM.ChequeType = "";
            }
            SqlParameter[] param = new SqlParameter[]
            {
                //new SqlParameter("@UserID",searchVM.UserID),
                //new SqlParameter("@FilterByMakerID",searchVM.FilterByMakerID),
                //new SqlParameter("@RelationShipType", string.IsNullOrEmpty(searchVM.ChequeType) ? (object)DBNull.Value : searchVM.ChequeType),
                //new SqlParameter("@Start",startRow),
                //new SqlParameter("@Last",endRow)
                        new SqlParameter("@eBBSUserID",searchVM.UserID),
                new SqlParameter("@FilterByMakerID",searchVM.FilterByMakerID),
                 new SqlParameter("@RelationShipType", string.IsNullOrEmpty(searchVM.ChequeType) ? (object)DBNull.Value : searchVM.ChequeType)
              
                
            };
            //DataSet ds = DAO.GetDataSet("sp_GetRequisitionFormPrintItem_New", CommandType.StoredProcedure, param);
            DataSet ds = DAO.GetDataSet("p_GetRequisitionFormPrintItem", CommandType.StoredProcedure, param);

            return ds;
        }

        public ResponseViewModel UpdateChequeBookPrintFlag(string issueID, string userID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[]
                {
                      new SqlParameter("@ISSUEID", issueID),
                      new SqlParameter("@UserID", userID),
                };
                int result = DAO.IDU("p_UpdateChqBookPrintFlag", CommandType.StoredProcedure, param);
                return new ResponseViewModel() { Message = MsgBox.update_msg, Status = MsgBox.success_status, LastId = result };
            }
            catch (Exception ex)
            {
                return new ResponseViewModel() { Exception = ex, Message = ex.Message, Status = MsgBox.failed_status };
            }
        }

        public ResponseViewModel UpdateChequeReviewPrintFlag(string issueID, string userID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[]
                {
                      new SqlParameter("@ISSUEID", issueID),
                      new SqlParameter("@UserID", userID),
                };
                int result = DAO.IDU("p_ChqPrintReviewed", CommandType.StoredProcedure, param);
                return new ResponseViewModel() { Message = MsgBox.update_msg, Status = MsgBox.success_status, LastId = result };
            }
            catch (Exception ex)
            {
                return new ResponseViewModel() { Exception = ex, Message = ex.Message, Status = MsgBox.failed_status };
            }
        }

        public ResponseViewModel UpdateCheque(ChequeBookViewModel model)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[]
                {
                     new SqlParameter("@IssueID", model.IssueID),
                      new SqlParameter("@checkStartno", model.ChequeStartNo),
                      new SqlParameter("@Noofleave", model.NoOfLeaves),
                      new SqlParameter("@UpdatedBy", model.UpdatedBy),
                };
                int result = DAO.IDU("p_updateChecknNoandLeave", CommandType.StoredProcedure, param);
                return new ResponseViewModel() { Message = MsgBox.update_msg, Status = MsgBox.success_status, LastId = result };
            }
            catch (Exception ex)
            {
                return new ResponseViewModel() { Exception = ex, Message = ex.Message, Status = MsgBox.failed_status };
            }
        }

        public ResponseViewModel ReviewCheque(ChequeBookViewModel model)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[]
                {
                     new SqlParameter("@IssueID", model.IssueID),
                      //new SqlParameter("@checkStartno", model.ChequeStartNo),
                      //new SqlParameter("@Noofleave", model.NoOfLeaves),
                      new SqlParameter("@UserID", model.UpdatedBy),
                };
                int result = DAO.IDU("p_ChqPrintReviewed", CommandType.StoredProcedure, param);
                return new ResponseViewModel() { Message = MsgBox.update_msg, Status = MsgBox.success_status, LastId = result };
            }
            catch (Exception ex)
            {
                return new ResponseViewModel() { Exception = ex, Message = ex.Message, Status = MsgBox.failed_status };
            }
        }

        public ResponseViewModel UpdateRequisition(ChequeBookViewModel model)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[]
                {
                     new SqlParameter("@IssueID", model.IssueID),
                      new SqlParameter("@checkStartno", model.ChequeStartNo),
                      new SqlParameter("@Noofleave", model.NoOfLeaves),
                      new SqlParameter("@UpdatedBy", model.UpdatedBy),
                };
                int result = DAO.IDU("p_updateChecknNoandLeave", CommandType.StoredProcedure, param);
                return new ResponseViewModel() { Message = MsgBox.update_msg, Status = MsgBox.success_status, LastId = result };
            }
            catch (Exception ex)
            {
                return new ResponseViewModel() { Exception = ex, Message = ex.Message, Status = MsgBox.failed_status };
            }
        }

        public ResponseViewModel ReviewAll(ChequeBookViewModel model)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[]
                {
                     new SqlParameter("@IssueID", model.IssueID),
                     new SqlParameter("@UserID", model.UpdatedBy),
                };
                int result = DAO.IDU("sp_ReviewAllCheques", CommandType.StoredProcedure, param);
                return new ResponseViewModel() { Message = MsgBox.update_msg, Status = MsgBox.success_status, LastId = result };
            }
            catch (Exception ex)
            {
                return new ResponseViewModel() { Exception = ex, Message = ex.Message, Status = MsgBox.failed_status };
            }
        }

        public ResponseViewModel UndoCheque(ChequeBookViewModel model)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[]
                {
                     new SqlParameter("@IssueID", model.IssueID),
                     new SqlParameter("@UserID", model.UpdatedBy),
                };
                int result = DAO.IDU("p_ChqPrintUndo", CommandType.StoredProcedure, param);
                return new ResponseViewModel() { Message = MsgBox.update_msg, Status = MsgBox.success_status, LastId = result };
            }
            catch (Exception ex)
            {
                return new ResponseViewModel() { Exception = ex, Message = ex.Message, Status = MsgBox.failed_status };
            }
        }

        public ResponseViewModel UndoRequisition(ChequeBookViewModel model)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[]
                {
                     new SqlParameter("@IssueID", model.IssueID),
                     new SqlParameter("@UserID", model.UpdatedBy),
                };
                int result = DAO.IDU("p_RequisitionPrintUndo", CommandType.StoredProcedure, param);
                return new ResponseViewModel() { Message = MsgBox.update_msg, Status = MsgBox.success_status, LastId = result };
            }
            catch (Exception ex)
            {
                return new ResponseViewModel() { Exception = ex, Message = ex.Message, Status = MsgBox.failed_status };
            }
        }

        //public ResponseViewModel UpdateCheckNoAndLeave(string issueID, string NoOfLeaves, string ChequeStartNo, string userID)
        //{
        //    try
        //    {
        //        SqlParameter[] param = new SqlParameter[]
        //        {
        //             new SqlParameter("@IssueID", issueID),
        //              new SqlParameter("@checkStartno", ChequeStartNo),
        //              new SqlParameter("@Noofleave", NoOfLeaves),
        //              new SqlParameter("@userId", userID),

        //        };
        //        int result = DAO.IDU("p_updateChecknNoandLeavs", CommandType.StoredProcedure, param);
        //        return new ResponseViewModel() { Message = MsgBox.update_msg, Status = MsgBox.success_status, LastId = result };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ResponseViewModel() { Exception = ex, Message = ex.Message, Status = MsgBox.failed_status };
        //    }
        //}
        #endregion

    }
}
