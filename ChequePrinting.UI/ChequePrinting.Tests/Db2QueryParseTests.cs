using ChequePrinting.BusinessLogicLayer.SqlQuery;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChequePrinting.Tests
{
    [TestClass]
    public class Db2QueryParseTests
    {
        //[TestMethod]
        //public void GetChequeBookLogs_Query_Parse()
        //{
        //    //Act
        //    string query = DB2Sql.sql_GetChequeBookLogs;
        //}
        [TestMethod]
        public void GetChequeBookLogs_OldQuery_Parse()
        {
            DateTime LastTimeStamp = new DateTime();

            string oldQuery = "SELECT DISTINCT I.CURRENCYCODE,I.ACCOUNTNO,A.BRANCHCODE,B.SHORTNAME AS FULLNAME,R.RELATIONSHIPNO,R.RELATIONSHIPTYPE,I.CHQSTARTNO, " +
                             "I.ISSUEDATE,I.REQID,I.NOOFLEAVES,I.BOOKSTATUS,I.MAKERID,I.MAKERDATE, " +
                             "CHAR(I.MAKERTIME) as MAKERTIME,TIMESTAMP(I.MAKERDATE,I.MAKERTIME) AS MAKERDATETIME, " +
                             "I.MAKERIPADDRESS,I.CHECKERID,I.CHECKERDATE,CHAR(I.CHECKERTIME) as CHECKERTIME, " +
                             "TIMESTAMP(I.CHECKERDATE,I.CHECKERTIME) AS CHECKERDATETIME, " +
                             "I.CHECKERIPADDRESS, " +
                             "I.STATUSFLAG, I.NOOFTIMESPRINTED, PRINTEDDATE, " +
                             "RA.FLATNO, RA.BLDGNAME, RA.NRLANDMRK, RA.STREET, SA.TRS, SB.MOB,RI.INFOCODE " +
                             "FROM DB2INST1.CHQISUE I " +
                             "INNER JOIN DB2INST1.ACCOUNT B ON I.ACCOUNTNO=B.ACCOUNTNO " +
                             "INNER JOIN DB2INST1.MASTREL M ON M.MASTERNO=SUBSTR(I.ACCOUNTNO,3,7) AND M.PRIMARYFLAG='Y' " +
                             "INNER JOIN DB2INST1.REL R ON R.RELATIONSHIPNO=M.RELATIONSHIPNO " +
                             "LEFT JOIN DB2INST1.RELINFO RI ON RI.RELATIONSHIPNO = R.RELATIONSHIPNO and RI.INFOCODE = 'IBK' " +
                             "INNER JOIN DB2INST1.MAST A ON A.MASTERNO=M.MASTERNO " +
                             "LEFT OUTER JOIN (SELECT FLATNO,BLDGNAME,NRLANDMRK,STREET,RELATIONSHIPNO FROM DB2INST1.RELADDR WHERE ADDTYPECODE = 'MAI')  RA " +
                             "ON M.RELATIONSHIPNO = RA.RELATIONSHIPNO " +
                             "LEFT OUTER JOIN " +
                             "(SELECT  CONTACT AS TRS,RELATIONSHIPNO FROM DB2INST1.RELCONT WHERE CONTACTTYPECODE = 'TRS') SA " +
                             "ON R.RELATIONSHIPNO  = SA.RELATIONSHIPNO " +
                             "LEFT OUTER JOIN " +
                             "(SELECT  CONTACT AS MOB,RELATIONSHIPNO FROM DB2INST1.RELCONT WHERE CONTACTTYPECODE = 'MOB') SB " +
                             "ON R.RELATIONSHIPNO   = SB.RELATIONSHIPNO " +
                             "WHERE TIMESTAMPDIFF(2,CHAR(TIMESTAMP(I.CHECKERDATE,I.CHECKERTIME)-TIMESTAMP('" + LastTimeStamp.ToString("MM/dd/yyyy") + "','" + LastTimeStamp.ToString("hh:mm:ss") + "'))) > 0  AND I.CHECKERDATE >'2018-01-01'";

            string newQuery = @"SELECT DISTINCT I.CURRENCYCODE,I.ACCOUNTNO,A.BRANCHCODE,B.SHORTNAME AS FULLNAME,R.RELATIONSHIPNO,R.RELATIONSHIPTYPE,I.CHQSTARTNO,  
                            I.ISSUEDATE,I.REQID,I.NOOFLEAVES,I.BOOKSTATUS,I.MAKERID,I.MAKERDATE, 
                            CHAR(I.MAKERTIME) as MAKERTIME,TIMESTAMP(I.MAKERDATE,I.MAKERTIME) AS MAKERDATETIME, 
                            I.MAKERIPADDRESS,I.CHECKERID,I.CHECKERDATE,CHAR(I.CHECKERTIME) as CHECKERTIME, 
                            TIMESTAMP(I.CHECKERDATE,I.CHECKERTIME) AS CHECKERDATETIME, 
                            I.CHECKERIPADDRESS, 
                            I.STATUSFLAG, I.NOOFTIMESPRINTED, PRINTEDDATE, 
                            RA.FLATNO, RA.BLDGNAME, RA.NRLANDMRK, RA.STREET, SA.TRS, SB.MOB,RI.INFOCODE 
                            FROM DB2INST1.CHQISUE I  
                            INNER JOIN DB2INST1.ACCOUNT B ON I.ACCOUNTNO=B.ACCOUNTNO 
                            INNER JOIN DB2INST1.MASTREL M ON M.MASTERNO=SUBSTR(I.ACCOUNTNO,3,7) AND M.PRIMARYFLAG='Y'  
                            INNER JOIN DB2INST1.REL R ON R.RELATIONSHIPNO=M.RELATIONSHIPNO 
                            LEFT JOIN DB2INST1.RELINFO RI ON RI.RELATIONSHIPNO = R.RELATIONSHIPNO and RI.INFOCODE = 'IBK' 
                            INNER JOIN DB2INST1.MAST A ON A.MASTERNO=M.MASTERNO 
                            LEFT OUTER JOIN (SELECT FLATNO,BLDGNAME,NRLANDMRK,STREET,RELATIONSHIPNO FROM DB2INST1.RELADDR WHERE ADDTYPECODE = 'MAI')  RA 
                            ON M.RELATIONSHIPNO = RA.RELATIONSHIPNO  
                            LEFT OUTER JOIN 
                            (SELECT  CONTACT AS TRS,RELATIONSHIPNO FROM DB2INST1.RELCONT WHERE CONTACTTYPECODE = 'TRS') SA  
                            ON R.RELATIONSHIPNO  = SA.RELATIONSHIPNO 
                            LEFT OUTER JOIN  
                            (SELECT  CONTACT AS MOB,RELATIONSHIPNO FROM DB2INST1.RELCONT WHERE CONTACTTYPECODE = 'MOB') SB 
                            ON R.RELATIONSHIPNO   = SB.RELATIONSHIPNO  
                            WHERE TIMESTAMPDIFF(2,CHAR(TIMESTAMP(I.CHECKERDATE,I.CHECKERTIME)
                -TIMESTAMP('"+ LastTimeStamp.ToString("yyyy-MM-dd")+","+ LastTimeStamp.ToString("hh:mm:ss")+"))) > 0  AND I.CHECKERDATE >'2018-01-01'";

            Assert.AreEqual(oldQuery, newQuery);
        }
    }
}
