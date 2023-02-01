using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChequePrinting.BusinessLogicLayer.SqlQuery
{
    public static class DbSql
    {
        public const string sp_LogError = "INSERT INTO TBL_ERRORLOG(ExceptionMsg, ExceptionType, ExceptionSource, ExceptionURL, Logdate) VALUES(@ExceptionMsg, @ExceptionType ,@ExceptionSource, @ExceptionURL, GETDATE())";
        public const string sql_GetRoleMenuMapping = "SELECT *FROM RoleMenuMapping WHERE RoleDesc = @UserGroup";
        public const string sql_DeleteRoleMenuMapping = "DELETE FROM RoleMenuMapping WHERE RoleDesc=@RoleDesc";
        public const string sql_GetAllMenu = "SELECT * FROM MenuMaster WHERE IsActive=1 ";
        public const string sql_DeleteUserByID = "DELETE FROM TBL_USER WHERE USER_ID=@UserID ";
        public const string sql_GetLastTimeStamp = "SELECT LASTTIMESTAMP FROM CONTROLTABLE";
        public const string sql_GetChequeIssueBuffer= "SELECT * FROM ChqIssueBuffer";
        public const string sql_GetChequeBufferCount = "SELECT COUNT(CURRENCYCODE) FROM ChqIssueBuffer";
        public const string sql_GetUserRoles = "SELECT RoleId, RoleDesc FROM RoleMaster";
        public const string sql_GetIsLocked = "  select option_ID, OPTION_VALUE from [TBL_OPTIONLIST_ISLOCKED] where OPTION_DESC = 'LOCK'";
        public const string sql_GetBranches = " SELECT BRANCHCODE,BRANCHNAME FROM BRANCHTABLE";
        public const string sql_GetUserStatus = "SELECT OPTION_ID, OPTION_VALUE  FROM TBL_OPTIONLIST WHERE OPTION_DESC = 'USER_STATUS' ";
        public const string sql_DestroyAccount= "UPDATE CHQISSUEMASTER  SET L_Status = 'D' WHERE ACCOUNTNO =@AccountNo AND DATEDIFF(DAY,L_PRINTEDDATE,@StartDate)<=0 AND DATEDIFF(DAY,L_PRINTEDDATE,@EndDate)>=0";
        //public const string sql_SendSMS = "insert into eNotify.dbo.SMS_EXCEL_UPLOAD([SMS_BODY],[SCHEDULE_ON],[CONTACT],[ADDED_BY],[ADDED_ON],[FLAG],[CHECKED_BY],[CHECKED_ON],[PROCESSED_ON],[STATUS],[DeliveryStatus],[SENTSTATUS],[DeliveryDate],[UPLOAD_CODE],[IS_SUPPORT], [REMARKS]) values('Cheque Book for your account is ready for delivery. If the request was not initiated by you, please call our 24X7 Client Care Centre at 977 14791800. StanChart',GETDATE(),'" + MOB + "','" + MAKERNAME + "','" + L_PRINTEDDATE + "',1,'" + L_REVIEWEDBY + "','" + L_REVIEWEDDATE + "',getdate(),'0','0','0',getdate(),'1','1','" + ACCOUNTNO + "')")";
        public const string sql_SendSMS = "insert into eNotify.dbo.SMS_EXCEL_UPLOAD([SMS_BODY],[SCHEDULE_ON],[CONTACT],[ADDED_BY],[ADDED_ON],[FLAG],[CHECKED_BY],[CHECKED_ON],[PROCESSED_ON],[STATUS],[DeliveryStatus],[SENTSTATUS],[DeliveryDate],[UPLOAD_CODE],[IS_SUPPORT], [REMARKS]) values('Cheque Book for your account is ready for delivery. If the request was not initiated by you, please call our 24X7 Client Care Centre at 977 14791800. StanChart',GETDATE(),@MOB,@MAKERNAME,@L_PRINTEDDATE,1,@L_REVIEWEDBY,@L_REVIEWEDDATE,getdate(),'0','0','0',getdate(),'1','1',@ACCOUNTNO )";
      //  public const string sql_SendSMS = " insert into dbo.SMS_EXCEL_UPLOAD([SMS_BODY],[SCHEDULE_ON],[CONTACT],[ADDED_BY],[ADDED_ON],[FLAG],[CHECKED_BY],[CHECKED_ON],[PROCESSED_ON],[STATUS],[DeliveryStatus],[SENTSTATUS],[DeliveryDate],[UPLOAD_CODE],[IS_SUPPORT], [REMARKS]) values('Cheque Book for your account is ready for delivery. If the request was not initiated by you, please call our 24X7 Client Care Centre at 977 14791800. StanChart',GETDATE(),@MOB,@MAKERNAME,@L_PRINTEDDATE,1,@L_REVIEWEDBY,@L_REVIEWEDDATE,getdate(),'0','0','0',getdate(),'1','1',@ACCOUNTNO )";

    }
}
