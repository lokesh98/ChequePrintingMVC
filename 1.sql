USE [master]
GO
/****** Object:  Database [ChequePrintSCB]    Script Date: 2/1/2023 11:17:51 AM ******/
CREATE DATABASE [ChequePrintSCB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ChequePrintDb_Data', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\ChequePrintSCB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 10%)
 LOG ON 
( NAME = N'ChequePrintDb_Log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\ChequePrintSCB_1.ldf' , SIZE = 76736KB , MAXSIZE = UNLIMITED, FILEGROWTH = 10%)
GO
ALTER DATABASE [ChequePrintSCB] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ChequePrintSCB].[dbo].[sp_fulltext_database] @action = 'disable'
end
GO
ALTER DATABASE [ChequePrintSCB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ChequePrintSCB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ChequePrintSCB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ChequePrintSCB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ChequePrintSCB] SET ARITHABORT OFF 
GO
ALTER DATABASE [ChequePrintSCB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ChequePrintSCB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ChequePrintSCB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ChequePrintSCB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ChequePrintSCB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ChequePrintSCB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ChequePrintSCB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ChequePrintSCB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ChequePrintSCB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ChequePrintSCB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ChequePrintSCB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ChequePrintSCB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ChequePrintSCB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ChequePrintSCB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ChequePrintSCB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ChequePrintSCB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ChequePrintSCB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ChequePrintSCB] SET RECOVERY FULL 
GO
ALTER DATABASE [ChequePrintSCB] SET  MULTI_USER 
GO
ALTER DATABASE [ChequePrintSCB] SET PAGE_VERIFY TORN_PAGE_DETECTION  
GO
ALTER DATABASE [ChequePrintSCB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ChequePrintSCB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ChequePrintSCB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [ChequePrintSCB] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'ChequePrintSCB', N'ON'
GO
ALTER DATABASE [ChequePrintSCB] SET QUERY_STORE = OFF
GO
USE [ChequePrintSCB]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [ChequePrintSCB]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_CleanString]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[fn_CleanString]
(
	@Message NVARCHAR(1000)
)
RETURNS NVARCHAR(1000)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultString AS NVARCHAR(1000)

	SET @ResultString = REPLACE(@Message,'''','');

	-- Return the result of the function
	RETURN @ResultString

END
GO
/****** Object:  Table [dbo].[BRANCHTABLE]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BRANCHTABLE](
	[BRANCHCODE] [char](3) NOT NULL,
	[BRANCHNAME] [char](35) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Chequeissuemasterlist]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Chequeissuemasterlist](
	[ISSUEID] [bigint] IDENTITY(1,1) NOT NULL,
	[CURRENCYCODE] [char](3) NULL,
	[ACCOUNTNO] [char](13) NULL,
	[BRANCHCODE] [char](3) NULL,
	[FULLNAME] [varchar](250) NULL,
	[RELATIONSHIPNO] [char](9) NULL,
	[RELATIONSHIPTYPE] [char](1) NULL,
	[CHQSTARTNO] [bigint] NULL,
	[C_CHQSTARTNOChange] [varchar](50) NULL,
	[C_NumberofLeaves] [varchar](50) NULL,
	[ISSUEDATE] [datetime] NULL,
	[REQID] [char](13) NULL,
	[NOOFLEAVES] [int] NULL,
	[BOOKSTATUS] [char](2) NULL,
	[MAKERID] [char](10) NULL,
	[MAKERDATE] [datetime] NULL,
	[MAKERTIME] [char](8) NULL,
	[MAKERDATETIME] [datetime] NULL,
	[MAKERIPADDRESS] [char](15) NULL,
	[CHECKERID] [char](10) NULL,
	[CHECKERDATE] [datetime] NULL,
	[CHECKERTIME] [char](8) NULL,
	[CHECKERDATETIME] [datetime] NULL,
	[CHECKERIPADDRESS] [nvarchar](255) NULL,
	[STATUSFLAG] [char](2) NULL,
	[NOOFTIMESPRINTED] [int] NULL,
	[PRINTEDDATE] [datetime] NULL,
	[L_STATUS] [char](1) NULL,
	[L_PRINTEDBY] [char](7) NULL,
	[L_PRINTEDDATE] [datetime] NULL,
	[L_NOOFPRINT] [smallint] NULL,
	[L_REVIEWEDBY] [char](7) NULL,
	[L_REVIEWEDDATE] [datetime] NULL,
	[C_STATUS] [char](1) NULL,
	[C_PRINTEDBY] [char](7) NULL,
	[C_PRINTEDDATE] [datetime] NULL,
	[C_NOOFPRINT] [smallint] NULL,
	[FLATNO] [varchar](250) NULL,
	[BLDGNAME] [varchar](250) NULL,
	[NRLANDMRK] [varchar](250) NULL,
	[STREET] [varchar](250) NULL,
	[TRS] [varchar](100) NULL,
	[MOB] [varchar](100) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChqIssueBuffer]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChqIssueBuffer](
	[CURRENCYCODE] [char](3) NULL,
	[ACCOUNTNO] [char](15) NULL,
	[BRANCHCODE] [char](10) NULL,
	[FULLNAME] [char](100) NULL,
	[RELATIONSHIPNO] [char](9) NULL,
	[RELATIONSHIPTYPE] [char](1) NULL,
	[CHQSTARTNO] [bigint] NULL,
	[ISSUEDATE] [varchar](50) NULL,
	[REQID] [char](13) NULL,
	[NOOFLEAVES] [int] NULL,
	[BOOKSTATUS] [char](2) NULL,
	[MAKERID] [char](10) NULL,
	[MAKERDATE] [varchar](50) NULL,
	[MAKERTIME] [char](8) NULL,
	[MAKERDATETIME] [varchar](50) NULL,
	[MAKERIPADDRESS] [char](15) NULL,
	[CHECKERID] [char](10) NULL,
	[CHECKERDATE] [varchar](50) NULL,
	[CHECKERTIME] [char](8) NULL,
	[CHECKERDATETIME] [varchar](50) NULL,
	[CHECKERIPADDRESS] [nvarchar](255) NULL,
	[STATUSFLAG] [char](2) NULL,
	[NOOFTIMESPRINTED] [int] NULL,
	[PRINTEDDATE] [varchar](50) NULL,
	[FLATNO] [varchar](250) NULL,
	[BLDGNAME] [varchar](250) NULL,
	[NRLANDMRK] [varchar](250) NULL,
	[STREET] [varchar](250) NULL,
	[TRS] [varchar](25) NULL,
	[MOB] [varchar](25) NULL,
	[INFOCODE] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CHQISSUEDETAILS]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CHQISSUEDETAILS](
	[ISSUEID] [bigint] NULL,
	[CHQNO] [bigint] NULL,
	[CHQNO_1] [char](10) NULL,
	[CHQNO_2] [char](10) NULL,
	[CHQNO_3] [char](10) NULL,
	[CHQNO_4] [char](10) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChqIssueMaster]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChqIssueMaster](
	[ISSUEID] [bigint] IDENTITY(1,1) NOT NULL,
	[CURRENCYCODE] [char](3) NULL,
	[ACCOUNTNO] [char](15) NULL,
	[BRANCHCODE] [char](10) NULL,
	[FULLNAME] [char](100) NULL,
	[RELATIONSHIPNO] [char](9) NULL,
	[RELATIONSHIPTYPE] [char](1) NULL,
	[CHQSTARTNO] [bigint] NULL,
	[ISSUEDATE] [datetime] NULL,
	[REQID] [char](13) NULL,
	[NOOFLEAVES] [int] NULL,
	[BOOKSTATUS] [char](2) NULL,
	[MAKERID] [char](10) NULL,
	[MAKERDATE] [datetime] NULL,
	[MAKERTIME] [char](8) NULL,
	[MAKERDATETIME] [datetime] NULL,
	[MAKERIPADDRESS] [char](15) NULL,
	[CHECKERID] [char](10) NULL,
	[CHECKERDATE] [datetime] NULL,
	[CHECKERTIME] [char](8) NULL,
	[CHECKERDATETIME] [datetime] NULL,
	[CHECKERIPADDRESS] [nvarchar](255) NULL,
	[STATUSFLAG] [char](2) NULL,
	[NOOFTIMESPRINTED] [int] NULL,
	[PRINTEDDATE] [datetime] NULL,
	[L_STATUS] [char](1) NULL,
	[L_PRINTEDBY] [char](7) NULL,
	[L_PRINTEDDATE] [datetime] NULL,
	[L_NOOFPRINT] [smallint] NULL,
	[L_REVIEWEDBY] [char](7) NULL,
	[L_REVIEWEDDATE] [datetime] NULL,
	[C_STATUS] [char](1) NULL,
	[C_PRINTEDBY] [char](7) NULL,
	[C_PRINTEDDATE] [datetime] NULL,
	[C_NOOFPRINT] [smallint] NULL,
	[FLATNO] [varchar](250) NULL,
	[BLDGNAME] [varchar](250) NULL,
	[NRLANDMRK] [varchar](250) NULL,
	[STREET] [varchar](250) NULL,
	[TRS] [varchar](25) NULL,
	[MOB] [varchar](25) NULL,
	[INFOCODE] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChqIssueMasterDB2]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChqIssueMasterDB2](
	[ISSUEID] [bigint] IDENTITY(1,1) NOT NULL,
	[CURRENCYCODE] [char](3) NULL,
	[ACCOUNTNO] [char](15) NULL,
	[BRANCHCODE] [char](10) NULL,
	[FULLNAME] [char](100) NULL,
	[RELATIONSHIPNO] [char](9) NULL,
	[RELATIONSHIPTYPE] [char](1) NULL,
	[CHQSTARTNO] [bigint] NULL,
	[ISSUEDATE] [datetime] NULL,
	[REQID] [char](13) NULL,
	[NOOFLEAVES] [int] NULL,
	[BOOKSTATUS] [char](2) NULL,
	[MAKERID] [char](10) NULL,
	[MAKERDATE] [datetime] NULL,
	[MAKERTIME] [char](8) NULL,
	[MAKERDATETIME] [datetime] NULL,
	[MAKERIPADDRESS] [char](15) NULL,
	[CHECKERID] [char](10) NULL,
	[CHECKERDATE] [datetime] NULL,
	[CHECKERTIME] [char](8) NULL,
	[CHECKERDATETIME] [datetime] NULL,
	[CHECKERIPADDRESS] [nvarchar](255) NULL,
	[STATUSFLAG] [char](2) NULL,
	[NOOFTIMESPRINTED] [int] NULL,
	[PRINTEDDATE] [datetime] NULL,
	[L_STATUS] [char](1) NULL,
	[L_PRINTEDBY] [char](7) NULL,
	[FLATNO] [varchar](250) NULL,
	[BLDGNAME] [varchar](250) NULL,
	[NRLANDMRK] [varchar](250) NULL,
	[STREET] [varchar](250) NULL,
	[TRS] [varchar](25) NULL,
	[MOB] [varchar](25) NULL,
	[INFOCODE] [varchar](50) NULL,
 CONSTRAINT [PK_ChqIssueMasterDB2] PRIMARY KEY CLUSTERED 
(
	[ISSUEID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ControlTable]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ControlTable](
	[LastTimeStamp] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DB2data]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DB2data](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Data] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IssueLog]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IssueLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Message] [varchar](max) NULL,
	[InnerException] [varchar](max) NULL,
	[Source] [varchar](max) NULL,
	[Date] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MenuMaster]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MenuMaster](
	[MenuId] [int] IDENTITY(1,1) NOT NULL,
	[MenuName] [varchar](100) NULL,
	[MenuUrl] [varchar](100) NULL,
	[MenuParentID] [int] NULL,
	[MenuOrderingNo] [int] NULL,
	[MenuClass] [varchar](100) NULL,
	[MenuIcon] [varchar](100) NULL,
	[IsActive] [bit] NULL,
	[SubMenuOrderNo] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[MenuId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleMaster]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleMaster](
	[RoleId] [int] NOT NULL,
	[RoleDesc] [varchar](50) NULL,
	[IsActive] [bit] NULL,
	[LandingPageURL] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleMenuMapping]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleMenuMapping](
	[MappingId] [int] IDENTITY(1,1) NOT NULL,
	[RoleDesc] [varchar](50) NULL,
	[MenuId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[MappingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SecurityMatrix]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SecurityMatrix](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Role] [varchar](100) NULL,
	[Menu] [varchar](100) NULL,
	[SubMenu] [varchar](100) NULL,
 CONSTRAINT [PK_SecurityMatrix] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SMS_EXCEL_UPLOAD]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SMS_EXCEL_UPLOAD](
	[SMS_BODY] [nvarchar](max) NULL,
	[SCHEDULE_ON] [datetime] NULL,
	[CONTACT] [varchar](250) NULL,
	[ADDED_BY] [varchar](50) NULL,
	[ADDED_ON] [datetime] NULL,
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FLAG] [nvarchar](50) NULL,
	[CHECKED_BY] [varchar](50) NULL,
	[CHECKED_ON] [datetime] NULL,
	[PROCESSED_ON] [datetime] NULL,
	[REMARKS] [varchar](50) NULL,
	[STATUS] [int] NULL,
	[DeliveryStatus] [bit] NULL,
	[SENTSTATUS] [int] NULL,
	[DeliveryDate] [datetime] NULL,
	[UPLOAD_CODE] [nvarchar](50) NULL,
	[IS_SUPPORT] [int] NULL,
	[count] [int] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SQLUserProcess]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SQLUserProcess](
	[SPID] [int] NULL,
	[UserID] [char](20) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Survey]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Survey](
	[PSID] [char](7) NOT NULL,
	[Q1] [char](1) NULL,
	[Q2] [char](1) NULL,
	[Q3] [char](1) NULL,
	[Q4] [char](1) NULL,
	[Q5] [char](1) NULL,
	[Q6] [char](1) NULL,
	[Q7] [char](1) NULL,
	[Q8] [char](1) NULL,
	[Q9] [char](1) NULL,
	[Q10] [char](1) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBL_ERRORLOG]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBL_ERRORLOG](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ExceptionMsg] [varchar](1000) NULL,
	[ExceptionType] [varchar](1000) NULL,
	[ExceptionSource] [nvarchar](max) NULL,
	[ExceptionURL] [varchar](100) NULL,
	[Logdate] [datetime] NULL,
 CONSTRAINT [PK_ErrorLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBL_MENUACCESS]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBL_MENUACCESS](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserGroupId] [int] NOT NULL,
	[MenuId] [int] NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
 CONSTRAINT [PK_TBL_MENUACCESS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBL_OPTIONLIST]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBL_OPTIONLIST](
	[OPTION_ID] [int] NOT NULL,
	[OPTION_NO] [int] NOT NULL,
	[OPTION_VALUE] [varchar](50) NOT NULL,
	[OPTION_DESC] [varchar](50) NULL,
 CONSTRAINT [PK_TBL_OPTIONLIST] PRIMARY KEY CLUSTERED 
(
	[OPTION_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBL_OPTIONLIST_ISLOCKED]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBL_OPTIONLIST_ISLOCKED](
	[OPTION_ID] [varchar](50) NOT NULL,
	[OPTION_NO] [varchar](50) NOT NULL,
	[OPTION_VALUE] [varchar](50) NOT NULL,
	[OPTION_DESC] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBL_PRINTSETTINGS]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBL_PRINTSETTINGS](
	[CHQTYPE] [char](1) NOT NULL,
	[PRINTER_SELECTED] [varchar](250) NOT NULL,
	[PAPER_SOURCE] [varchar](50) NOT NULL,
	[PAGE_HEIGHT] [float] NOT NULL,
	[ALLOW_OFFSET] [char](1) NULL,
	[PAGE_HEIGHT_OFFSET] [float] NULL,
	[OFFSET_FREQ] [int] NULL,
	[ACCNO_TOP] [float] NOT NULL,
	[ACCNO_LEFT] [float] NOT NULL,
	[ACCNAME_TOP] [float] NOT NULL,
	[ACCNAME_LEFT] [float] NOT NULL,
	[BRANCH_TOP] [float] NOT NULL,
	[BRANCH_LEFT] [float] NOT NULL,
	[REMARKS_TOP] [float] NULL,
	[REMARKS_LEFT] [float] NULL,
	[CREATED_BY] [char](10) NULL,
	[CREATED_DATE] [datetime] NULL,
	[MODIFIED_BY] [char](10) NULL,
	[MODIFIED_DATE] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBL_USER]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBL_USER](
	[USER_ID] [varchar](15) NOT NULL,
	[USER_NAME] [varchar](255) NOT NULL,
	[PWD] [varchar](255) NULL,
	[USER_TYPE] [int] NOT NULL,
	[EMAIL] [varchar](50) NOT NULL,
	[STATUS] [int] NOT NULL,
	[USER_GROUP_ID] [int] NULL,
	[CAN_UNDO_PRINT] [varchar](1) NOT NULL,
	[EBBS_USER_ID] [varchar](15) NULL,
	[CAN_DOWNLOAD] [varchar](1) NOT NULL,
	[USER_MODE] [varchar](1) NOT NULL,
	[CREATED_BY] [varchar](50) NOT NULL,
	[CREATED_DATE] [datetime] NOT NULL,
	[MODIFIED_BY] [char](7) NULL,
	[MODIFIED_DATE] [datetime] NULL,
	[IsLogin] [int] NULL,
	[LastLogin] [datetime] NULL,
	[LoginAttempt] [int] NULL,
	[LoginFailedCount] [int] NULL,
	[IsLocked] [int] NULL,
 CONSTRAINT [PK_TBL_USER] PRIMARY KEY CLUSTERED 
(
	[USER_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBL_USER_new]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBL_USER_new](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[USER_ID] [char](8) NULL,
	[USER_NAME] [varchar](255) NOT NULL,
	[PWD] [varchar](255) NULL,
	[USER_TYPE] [int] NOT NULL,
	[EMAIL] [varchar](50) NOT NULL,
	[STATUS] [int] NOT NULL,
	[USER_GROUP_ID] [int] NULL,
	[CREATED_BY] [char](8) NULL,
	[CREATED_DATE] [datetime] NOT NULL,
	[MODIFIED_BY] [char](8) NULL,
	[MODIFIED_DATE] [datetime] NULL,
	[ISLOGIN] [int] NULL,
	[LASTLOGIN] [datetime] NULL,
	[LOGIN_FAILED_COUNT] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserActivityLog]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserActivityLog](
	[LogID] [bigint] IDENTITY(1,1) NOT NULL,
	[UserID] [char](7) NOT NULL,
	[SourceModule] [char](25) NOT NULL,
	[ActivityDesc] [varchar](500) NOT NULL,
	[LogTimeStamp] [datetime] NOT NULL,
	[RefID] [char](10) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserActivityLog_new]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserActivityLog_new](
	[LogID] [bigint] IDENTITY(1,1) NOT NULL,
	[UserID] [char](8) NULL,
	[ActivityDesc] [char](50) NOT NULL,
	[LogTimeStamp] [datetime] NOT NULL,
	[narrative_type] [char](10) NULL,
	[result] [bigint] NULL,
	[filename] [varchar](50) NULL,
	[header] [varchar](60) NULL,
	[type] [char](15) NULL,
	[valdate] [varchar](15) NULL,
	[valuedate] [varchar](15) NULL,
	[risk_analysis] [int] NULL,
	[old_valuedate] [varchar](15) NULL,
	[valid_time] [int] NULL,
	[reason] [text] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserAuditLog]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserAuditLog](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[UserID] [varchar](50) NULL,
	[UserLoginId] [varchar](50) NULL,
	[RequestType] [varchar](100) NULL,
	[OldValue] [varchar](150) NULL,
	[NewValue] [varchar](150) NULL,
	[Audit_Desc] [varchar](max) NULL,
	[IP] [varchar](100) NULL,
	[MachineName] [nvarchar](500) NULL,
	[LogTimeStamp] [datetime] NULL,
	[Url] [varchar](100) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserInfoAuditLog]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserInfoAuditLog](
	[USER_AUDIT_ID] [bigint] IDENTITY(1,1) NOT NULL,
	[USER_CODE] [varchar](50) NULL,
	[USER_LOGIN_ID] [varchar](50) NULL,
	[REQUEST_TYPE] [varchar](100) NULL,
	[OLD_VALUE] [varchar](150) NULL,
	[NEW_VALUE] [varchar](150) NULL,
	[USER_NAME] [varchar](50) NULL,
	[AUDIT_DATE] [datetime] NULL,
	[AUDIT_DESC] [varchar](max) NULL,
	[IP] [varchar](100) NULL,
	[MACHINE_NAME] [varchar](500) NULL,
	[ROLE_ID] [varchar](50) NULL,
	[url] [varchar](150) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[ChqIssueMaster] ADD  CONSTRAINT [DF_ChqIssueMaster_C_STATUS]  DEFAULT ((0)) FOR [C_STATUS]
GO
ALTER TABLE [dbo].[ChqIssueMaster] ADD  CONSTRAINT [DF_ChqIssueMaster_C_NOOFPRINT]  DEFAULT ((0)) FOR [C_NOOFPRINT]
GO
ALTER TABLE [dbo].[SMS_EXCEL_UPLOAD] ADD  CONSTRAINT [DF_SMS_EXCEL_UPLOAD_STATUS_1]  DEFAULT ((0)) FOR [STATUS]
GO
ALTER TABLE [dbo].[SMS_EXCEL_UPLOAD] ADD  CONSTRAINT [DF_SMS_EXCEL_UPLOAD_count]  DEFAULT ((0)) FOR [count]
GO
ALTER TABLE [dbo].[RoleMenuMapping]  WITH CHECK ADD  CONSTRAINT [FK_RoleMenuMapping_MenuMaster] FOREIGN KEY([MenuId])
REFERENCES [dbo].[MenuMaster] ([MenuId])
GO
ALTER TABLE [dbo].[RoleMenuMapping] CHECK CONSTRAINT [FK_RoleMenuMapping_MenuMaster]
GO
/****** Object:  StoredProcedure [dbo].[p_AddUserActivityLog]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[p_AddUserActivityLog]
@UserID as Char(7),
@SourceModule as Char(25),
@ActivityDesc as Varchar(50),
@RefID as Char(10)=''
As
INSERT INTO UserActivityLog (UserID,SourceModule,ActivityDesc,LogTimeStamp,RefID) VALUES(@UserID,@SourceModule,@ActivityDesc,GetDate(),@RefID)

GO
/****** Object:  StoredProcedure [dbo].[p_ChqPrintReviewed]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[p_ChqPrintReviewed]  
@ISSUEID as varchar(max),  
@UserID as Char(7)  
AS  
  
  
BEGIN  
UPDATE CHQISSUEMASTER SET L_STATUS='R',L_REVIEWEDBY=@UserID,L_REVIEWEDDATE=GetDate()  
 WHERE ISSUEID=@ISSUEID  

  
declare @ActivityDesc varchar(100)  
select @ActivityDesc =  'ChequeBook No: ' + Cast(@ISSUEID as varchar(2000)) + ' Reviewed'  
  
EXEC  p_AddUserActivityLog   
@UserID,  
'Cheque Book Print Review',  
@ActivityDesc,  
null  
END
GO
/****** Object:  StoredProcedure [dbo].[p_ChqPrintUndo]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[p_ChqPrintUndo]
@ISSUEID as BigInt,
@UserID as Char(7)
As
UPDATE CHQISSUEMASTER SET L_PRINTEDBY='',L_STATUS='W',L_PRINTEDDATE=NULL,L_NOOFPRINT=0
	WHERE ISSUEID=@ISSUEID

declare @ActivityDesc varchar(100)
select @ActivityDesc =  'ChequeBook No: ' + Cast(@ISSUEID as varchar(20)) + ' Print status reset'

EXEC  p_AddUserActivityLog 
@UserID,
'Cheque Book Print Undo',
@ActivityDesc,
null
GO
/****** Object:  StoredProcedure [dbo].[p_ClearTranBuffer]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[p_ClearTranBuffer]
@UserID char(7)
AS

DELETE FROM ChqIssueBuffer

declare @ActivityDesc varchar(100)
select @ActivityDesc =  'Buffer data cleared'

--EXEC  p_AddUserActivityLog 
--@UserID,
--'Clear Download',
--@ActivityDesc,
--null
GO
/****** Object:  StoredProcedure [dbo].[p_GetChqBookItemToPrint]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[p_GetChqBookItemToPrint]  
@ChqBookID as nVarchar(10)  
AS  
SET NOCOUNT ON  
DECLARE @CHQSTARTNO as BigInt,  
 @CHQLEAVES as Int,  
 @ISSUEID as BigInt,  
 @BUF_NO as BIGINT,  
 @CHQNO_1 as CHAR(10)  
  
SELECT @CHQSTARTNO=CHQSTARTNO,@CHQLEAVES=NOOFLEAVES FROM CHQISSUEMASTER WHERE ISSUEID=@ChqBookID  
PRINT @CHQSTARTNO  
PRINT @CHQLEAVES  
SET @BUF_NO=@CHQSTARTNO  
DELETE FROM CHQISSUEDETAILS WHERE ISSUEID=@CHQBOOKID  
WHILE @BUF_NO<=@CHQSTARTNO+@CHQLEAVES-1  
 BEGIN  
 SET @CHQNO_1=RIGHT(CONVERT(nChar(11),10000000000+@BUF_NO),10)  
 INSERT INTO CHQISSUEDETAILS (ISSUEID,CHQNO,CHQNO_1)VALUES(Convert(bigint,@ChqBookID),@BUF_NO,@CHQNO_1)  
 SET @BUF_NO=@BUF_NO+1  
 END  
  
SELECT  I.FULLNAME as CUSTOMERNAME,  
(RTRIM(I.CURRENCYCODE) + ' ' + LEFT(I.ACCOUNTNO,2) + '-' + SUBSTRING(I.ACCOUNTNO,3,7) + '-' + RIGHT(RTRIM(I.ACCOUNTNO),2)) as ACCOUNTNO,  
RIGHT('00000000000000000000' + LEFT(I.ACCOUNTNO,11),20) AS ACCOUNTNO1,  
'0601' AS BANKCODE,  
RIGHT('0000' + B.BRANCHCODE,4) AS BRANCHCODE,  
B.BRANCHNAME,I.RELATIONSHIPTYPE,I.NOOFLEAVES,I.CHECKERID,I.ISSUEID,  
 I.FLATNO,I.BLDGNAME,I.NRLANDMRK,I.STREET,I.TRS,I.MOB,I.CHQSTARTNO,  
 D.ChqNo_1,  
 ChqNo_2=CASE WHEN I.CURRENCYCODE='USD' AND RIGHT(RTRIM(I.ACCOUNTNO),2) IN ('54','55') THEN 'NEGOTIABLE ONLY IN NEPAL' ELSE '' END,  
 ChqNo_3=''  
 FROM CHQISSUEMASTER I ,BRANCHTABLE B,CHQISSUEDETAILS D  
WHere  cast(I.BRANCHCODE as int)= cast(B.BRANCHCODE as int)  
AND I.IssueID=@ChqBookID 
AND I.ISSUEID=D.ISSUEID  
ORDER BY D.CHQNO ASC
GO
/****** Object:  StoredProcedure [dbo].[p_GetChqBookItemToPrintNew]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[p_GetChqBookItemToPrintNew]
@ChqBookID as nVarchar(10)
AS
SET NOCOUNT ON
DECLARE @CHQSTARTNO as BigInt,
	@CHQLEAVES as Int,
	@ISSUEID as BigInt,
	@BUF_NO as BIGINT,
	@CHQNO_1 as CHAR(10)
SELECT @CHQSTARTNO=CHQSTARTNO,@CHQLEAVES=NOOFLEAVES FROM CHQISSUEMASTER WHERE ISSUEID=@ChqBookID
PRINT @CHQSTARTNO
PRINT @CHQLEAVES
SET @BUF_NO=@CHQSTARTNO
DELETE FROM CHQISSUEDETAILS WHERE ISSUEID=@CHQBOOKID
WHILE @BUF_NO<=@CHQSTARTNO+@CHQLEAVES-1
	BEGIN
	SET @CHQNO_1=RIGHT(CONVERT(nChar(11),10000000000+@BUF_NO),10)
	INSERT INTO CHQISSUEDETAILS (ISSUEID,CHQNO,CHQNO_1)VALUES(Convert(bigint,@ChqBookID),@BUF_NO,@CHQNO_1)
	SET @BUF_NO=@BUF_NO+1
	END

SELECT I.FULLNAME as CUSTOMERNAME,
(RTRIM(I.CURRENCYCODE) + ' ' + LEFT(I.ACCOUNTNO,2) + '-' + SUBSTRING(I.ACCOUNTNO,3,7) + '-' + RIGHT(RTRIM(I.ACCOUNTNO),2)) as ACCOUNTNO,
RIGHT('00000000000000000000' + LEFT(I.ACCOUNTNO,11),20) AS ACCOUNTNO1,'0601' AS BANKCODE,
RIGHT('0000' + B.BRANCHCODE,4) AS BRANCHCODE,B.BRANCHNAME,I.RELATIONSHIPTYPE,I.NOOFLEAVES,I.ISSUEID,
	D.ChqNo_1,
	ChqNo_2=CASE WHEN I.CURRENCYCODE='USD' AND RIGHT(RTRIM(I.ACCOUNTNO),2) IN ('54','55') THEN 'NEGOTIABLE ONLY IN NEPAL' ELSE '' END,
	ChqNo_3=''
	FROM CHQISSUEMASTER I ,BRANCHTABLE B,CHQISSUEDETAILS D
WHere 	cast(I.BRANCHCODE as int)= cast(B.BRANCHCODE as int)
AND	I.IssueID=@ChqBookID
AND	I.ISSUEID=D.ISSUEID
ORDER BY D.CHQNO ASC





GO
/****** Object:  StoredProcedure [dbo].[p_GetChqBookPrintItem]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[p_GetChqBookPrintItem]
@DoFilter as tinyInt=0,
@eBBSUserID as Char(7)=''
AS
IF @DoFilter=1 
	SELECT M.*,B.BRANCHNAME FROM CHQISSUEMASTER M,BRANCHTABLE B  WHERE 
	cast(M.BRANCHCODE as int) = cast(B.BRANCHCODE as int) AND  M.L_STATUS='W'	
	AND M.MAKERID =@eBBSUserID
ELSE
	SELECT M.*,B.BRANCHNAME FROM CHQISSUEMASTER M JOIN BRANCHTABLE B ON cast(M.BRANCHCODE as int) =cast( B.BRANCHCODE as int) WHERE
	M. L_STATUS='W'
GO
/****** Object:  StoredProcedure [dbo].[p_GetChqBookReview]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[p_GetChqBookReview]
AS
SELECT * FROM CHQISSUEMASTER WHERE L_STATUS='P' AND C_STATUS = '1'
GO
/****** Object:  StoredProcedure [dbo].[p_GetChqBookReviewList]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[p_GetChqBookReviewList]
AS
SELECT * FROM Chequeissuemasterlist WHERE L_STATUS='P' AND C_STATUS = '1'
GO
/****** Object:  StoredProcedure [dbo].[p_GetChqIssueBuffer]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[p_GetChqIssueBuffer]
AS
SELECT * FROM CHQISSUEBUFFER

GO
/****** Object:  StoredProcedure [dbo].[p_GetLogReport]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[p_GetLogReport]
@FromDate as DATETIME,
@EndDate as DATETIME

As
SELECT [LogID]
      ,[UserID]
      ,[SourceModule]
      ,[ActivityDesc]
      ,[LogTimeStamp] FROM USERACTIVITYLOG WHERE LOGTIMESTAMP BETWEEN @FromDate AND @EndDate
GO
/****** Object:  StoredProcedure [dbo].[p_GetPeriodicPrintReport]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[p_GetPeriodicPrintReport]
@FromDate as varchar(10),
@EndDate as varchar(10),
@BRANCHCODE as varchar(20)
As


  SELECT  DISTINCT  '' AS CHQSTARTNO, M.CURRENCYCODE, M.ACCOUNTNO,m.L_STATUS, m.BRANCHCODE, m.FULLNAME, m.RELATIONSHIPTYPE, m.NOOFLEAVES,m.STATUSFLAG,
   m.INFOCODE,m.MOB, m.L_REVIEWEDBY, '' as L_REVIEWEDDATE, GETDATE() AS L_PRINTEDDATE,
   A.USER_NAME as MakerName,B.USER_NAME as ReviewerName
 FROM CHQISSUEMASTER M,TBL_USER A,TBL_USER B where
-- DATEDIFF(DAY,L_PRINTEDDATE,@FromDate)<=0
--AND DATEDIFF(DAY,L_PRINTEDDATE,@EndDate)>=0
--  AND
  M.L_PRINTEDBY=A.User_ID
 AND M.L_REVIEWEDBY=B.User_ID
 AND L_Status = 'R' and L_STATUS NOT IN ('D')
  --order by FULLNAME DESC
GO
/****** Object:  StoredProcedure [dbo].[p_GetPeriodicPrintReportAccount]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[p_GetPeriodicPrintReportAccount]
@FromDate as varchar(10),
@EndDate as varchar(10)
,
@BRANCHCODE as varchar(20)
As
--if (@FromDate ='' or @FromDate = NULL or @FromDate ='  /  /') or (@EndDate ='' or @EndDate = NULL or @EndDate ='  /  /' )
--SELECT '' as ACCOUNTNO
--else 
SELECT *
	FROM CHQISSUEMASTER M,TBL_USER A,TBL_USER B
	WHERE 	DATEDIFF(DAY,L_PRINTEDDATE,@FromDate)<=0
	AND	DATEDIFF(DAY,L_PRINTEDDATE,@EndDate)>=0
	--AND	M.L_PRINTEDBY=A.User_ID
	--AND	M.L_REVIEWEDBY=B.User_ID
	AND L_STATUS = 'R'
	--AND M.BRANCHCODE like '%' + @BRANCHCODE + '%'

GO
/****** Object:  StoredProcedure [dbo].[p_GetReqItemToPrint]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[p_GetReqItemToPrint]    
@ChqBookID as nVarchar(10)    
AS    
SET NOCOUNT ON    
DECLARE @CHQSTARTNO as BigInt,    
 @CHQLEAVES as Int,    
 @ISSUEID as BigInt,    
 @BUF_NO as BIGINT,    
 @CHQNO_1 as CHAR(10)    
    
SELECT @CHQSTARTNO=CHQSTARTNO,@CHQLEAVES=NOOFLEAVES FROM CHQISSUEMASTER WHERE ISSUEID=@ChqBookID    
PRINT @CHQSTARTNO    
PRINT @CHQLEAVES    
SET @BUF_NO=@CHQSTARTNO    
DELETE FROM CHQISSUEDETAILS WHERE ISSUEID=@CHQBOOKID    
WHILE @BUF_NO<=@CHQSTARTNO+@CHQLEAVES-1    
 BEGIN    
 SET @CHQNO_1=RIGHT(CONVERT(nChar(11),10000000000+@BUF_NO),10)    
 INSERT INTO CHQISSUEDETAILS (ISSUEID,CHQNO,CHQNO_1)VALUES(Convert(bigint,@ChqBookID),@BUF_NO,@CHQNO_1)    
 SET @BUF_NO=@BUF_NO+1    
 END    
    
SELECT top 1 I.FULLNAME as CUSTOMERNAME,    
(RTRIM(I.CURRENCYCODE) + ' ' + LEFT(I.ACCOUNTNO,2) + '-' + SUBSTRING(I.ACCOUNTNO,3,7) + '-' + RIGHT(RTRIM(I.ACCOUNTNO),2)) as ACCOUNTNO,    
RIGHT('00000000000000000000' + LEFT(I.ACCOUNTNO,11),20) AS ACCOUNTNO1,    
'0601' AS BANKCODE,    
RIGHT('0000' + B.BRANCHCODE,4) AS BRANCHCODE, I.InfoCode as ONLINEBANKING,   
B.BRANCHNAME,I.RELATIONSHIPTYPE,I.NOOFLEAVES,I.CHECKERID,I.ISSUEID,    
 I.FLATNO,I.BLDGNAME,I.NRLANDMRK,I.STREET,I.TRS,I.MOB,I.CHQSTARTNO,    
 D.ChqNo_1,    
 ChqNo_2=CASE WHEN I.CURRENCYCODE='USD' AND RIGHT(RTRIM(I.ACCOUNTNO),2) IN ('54','55') THEN 'NEGOTIABLE ONLY IN NEPAL' ELSE '' END,    
 ChqNo_3=''    
 FROM CHQISSUEMASTER I ,BRANCHTABLE B,CHQISSUEDETAILS D    
WHere  cast(I.BRANCHCODE as int)= cast(B.BRANCHCODE as int)    
AND I.IssueID=@ChqBookID   
AND I.ISSUEID=D.ISSUEID    
ORDER BY D.CHQNO ASC
GO
/****** Object:  StoredProcedure [dbo].[p_GetRequisitionFormPrintItem]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[p_GetRequisitionFormPrintItem]

@eBBSUserID as Char(7)='',
@RelationShipType varchar(30),  
 @FilterByMakerID BIT 
AS
IF @FilterByMakerID=1
    SELECT M.ISSUEID,m.CURRENCYCODE,m.ACCOUNTNO, m.INFOCODE,case when m.INFOCODE = 'IBK' then 'Yes' else 'No' end as ONLINEBANKING ,
    M.BRANCHCODE,M.FULLNAME ,M.RELATIONSHIPNO,
    M.RELATIONSHIPTYPE ,M.CHQSTARTNO ,M.ISSUEDATE ,M.REQID ,M.NOOFLEAVES ,M.BOOKSTATUS ,
    M.MAKERID ,M.MAKERDATE ,M.MAKERTIME ,M.MAKERDATETIME ,M.MAKERIPADDRESS ,M.CHECKERID ,
    M.CHECKERDATE ,M.CHECKERTIME ,M.CHECKERDATETIME ,M.CHECKERIPADDRESS ,M.STATUSFLAG ,
    M.NOOFTIMESPRINTED ,M.PRINTEDDATE ,M.L_STATUS ,M.L_PRINTEDBY ,M.L_PRINTEDDATE ,
    M.L_NOOFPRINT ,M.L_REVIEWEDBY ,M.L_REVIEWEDDATE ,M.C_STATUS ,M.C_PRINTEDBY ,
    M.C_PRINTEDDATE ,M.C_NOOFPRINT ,M.FLATNO ,M.BLDGNAME ,M.NRLANDMRK ,M.STREET ,
    M.TRS ,M.MOB,B.BRANCHNAME FROM CHQISSUEMASTER M,BRANCHTABLE B WHERE
    M.BRANCHCODE = B.BRANCHCODE AND M.C_STATUS='0'  AND M.RELATIONSHIPTYPE =CAST(ISNULL(@RelationShipType,RELATIONSHIPTYPE) AS CHAR(1))  
	AND M.MAKERID =@eBBSUserID


ELSE
    SELECT M.ISSUEID,m.CURRENCYCODE,m.ACCOUNTNO, m.INFOCODE,case when m.INFOCODE = 'IBK' then 'Yes' else 'No' end as ONLINEBANKING,
    M.BRANCHCODE,M.FULLNAME ,M.RELATIONSHIPNO ,
    M.RELATIONSHIPTYPE ,M.CHQSTARTNO ,M.ISSUEDATE ,M.REQID ,M.NOOFLEAVES ,M.BOOKSTATUS ,
    M.MAKERID ,M.MAKERDATE ,M.MAKERTIME ,M.MAKERDATETIME ,M.MAKERIPADDRESS ,M.CHECKERID ,
    M.CHECKERDATE ,M.CHECKERTIME ,M.CHECKERDATETIME ,M.CHECKERIPADDRESS ,M.STATUSFLAG ,
    M.NOOFTIMESPRINTED ,M.PRINTEDDATE ,M.L_STATUS ,M.L_PRINTEDBY ,M.L_PRINTEDDATE ,
    M.L_NOOFPRINT ,M.L_REVIEWEDBY ,M.L_REVIEWEDDATE ,M.C_STATUS ,M.C_PRINTEDBY ,
    M.C_PRINTEDDATE ,M.C_NOOFPRINT ,M.FLATNO ,M.BLDGNAME ,M.NRLANDMRK ,M.STREET ,
    M.TRS ,M.MOB ,B.BRANCHNAME FROM CHQISSUEMASTER M JOIN BRANCHTABLE B ON M.BRANCHCODE = B.BRANCHCODE
    WHERE M.C_STATUS='0'  AND M.RELATIONSHIPTYPE =CAST(ISNULL(@RelationShipType,RELATIONSHIPTYPE) AS CHAR(1))  


--	IF @DoFilter=1   
-- SELECT M.*,B.BRANCHNAME FROM CHQISSUEMASTER M,BRANCHTABLE B  WHERE   
-- cast(M.BRANCHCODE as int) = cast(B.BRANCHCODE as int) AND  M.C_STATUS='0' AND M.MAKERID =@eBBSUserID  
--ELSE  
-- SELECT M.*,B.BRANCHNAME FROM CHQISSUEMASTER M JOIN BRANCHTABLE B ON cast(M.BRANCHCODE  as int)=cast( B.BRANCHCODE as int)  WHERE  
-- M. C_STATUS='0'
GO
/****** Object:  StoredProcedure [dbo].[p_InitBufferTable]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[p_InitBufferTable]
AS
DELETE FROM ChqIssueBuffer


GO
/****** Object:  StoredProcedure [dbo].[p_InitSQLSPID]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[p_InitSQLSPID]
@UserID as nVarchar(20)
As
IF EXISTS(Select * from SQLUserProcess Where SPID=@@SPID)
		UPDATE SQLUserProcess SET UserID=@UserID Where SPID=@@SPID
	ELSE
		INSERT INTO SQLUserProcess (SPID,UserID) VALUES(@@SPID,@UserID)


GO
/****** Object:  StoredProcedure [dbo].[p_PostBufferChqLogs]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[p_PostBufferChqLogs]
As
SET NOCOUNT ON
INSERT INTO CHQISSUEMASTER (CURRENCYCODE,ACCOUNTNO,BRANCHCODE,FULLNAME,RELATIONSHIPNO,RELATIONSHIPTYPE,
	CHQSTARTNO,ISSUEDATE,REQID,NOOFLEAVES,BOOKSTATUS,MAKERID,MAKERDATE,MAKERTIME,MAKERDATETIME,
	MAKERIPADDRESS,CHECKERID,CHECKERDATE,CHECKERTIME,CHECKERDATETIME,CHECKERIPADDRESS,STATUSFLAG,
	NOOFTIMESPRINTED,PRINTEDDATE,L_STATUS,L_PRINTEDBY,L_PRINTEDDATE,L_NOOFPRINT,
	FLATNO,BLDGNAME,NRLANDMRK,STREET,TRS,MOB,INFOCODE)

SELECT CURRENCYCODE,ACCOUNTNO,BRANCHCODE,FULLNAME,RELATIONSHIPNO,RELATIONSHIPTYPE,
	CHQSTARTNO,ISSUEDATE,REQID,NOOFLEAVES,BOOKSTATUS,MAKERID,MAKERDATE,MAKERTIME,MAKERDATETIME,
	MAKERIPADDRESS,CHECKERID,CHECKERDATE,CHECKERTIME,CHECKERDATETIME,CHECKERIPADDRESS,STATUSFLAG,
	NOOFTIMESPRINTED,PRINTEDDATE,'W','',NULL,0,FLATNO,BLDGNAME,NRLANDMRK,STREET,TRS,MOB,INFOCODE FROM ChqIssueBuffer 

DELETE FROM CHQISSUEBUFFER
UPDATE ControlTable SET LastTimeStamp=(SELECT MAX(CHECKERDATETIME) FROM CHQISSUEMASTER)
GO
/****** Object:  StoredProcedure [dbo].[p_PostBufferChqLogsUpload]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[p_PostBufferChqLogsUpload]
As
SET NOCOUNT ON
INSERT INTO CHQISSUEMASTER (CURRENCYCODE,ACCOUNTNO,BRANCHCODE,FULLNAME,RELATIONSHIPNO,RELATIONSHIPTYPE,
	CHQSTARTNO,ISSUEDATE,REQID,NOOFLEAVES,BOOKSTATUS,MAKERID,MAKERDATE,MAKERTIME,MAKERDATETIME,
	MAKERIPADDRESS,CHECKERID,CHECKERDATE,CHECKERTIME,CHECKERDATETIME,CHECKERIPADDRESS,STATUSFLAG,
	NOOFTIMESPRINTED,PRINTEDDATE,L_STATUS,L_PRINTEDBY,L_PRINTEDDATE,L_NOOFPRINT,
	FLATNO,BLDGNAME,NRLANDMRK,STREET,TRS,MOB,INFOCODE)

SELECT CURRENCYCODE,ACCOUNTNO,BRANCHCODE,FULLNAME,RELATIONSHIPNO,RELATIONSHIPTYPE,
	CHQSTARTNO,ISSUEDATE,REQID,NOOFLEAVES,BOOKSTATUS,MAKERID,MAKERDATE,MAKERTIME,MAKERDATETIME,
	MAKERIPADDRESS,CHECKERID,CHECKERDATE,CHECKERTIME,CHECKERDATETIME,CHECKERIPADDRESS,STATUSFLAG,
	NOOFTIMESPRINTED,PRINTEDDATE,'W','',NULL,0,FLATNO,BLDGNAME,NRLANDMRK,STREET,TRS,MOB,INFOCODE FROM ChqIssueMasterDB2  

DELETE FROM ChqIssueMasterDB2
UPDATE ControlTable SET LastTimeStamp=(SELECT MAX(CHECKERDATETIME) FROM CHQISSUEMASTER)
GO
/****** Object:  StoredProcedure [dbo].[p_RequisitionPrintUndo]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[p_RequisitionPrintUndo]  
@ISSUEID as BigInt,  
@UserID as Char(7)  
As  
UPDATE CHQISSUEMASTER SET C_PRINTEDBY='',C_STATUS='0',C_PRINTEDDATE=NULL,C_NOOFPRINT=0  
 WHERE ISSUEID=@ISSUEID  
  
  
declare @ActivityDesc varchar(100)  
select @ActivityDesc =  'ChequeBook No: ' + Cast(@ISSUEID as varchar(20)) + 'requisition status reset'  
  
EXEC  p_AddUserActivityLog   
@UserID,  
'Requisition form print undo',  
@ActivityDesc,  
null
GO
/****** Object:  StoredProcedure [dbo].[p_SavePrintSettings]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[p_SavePrintSettings]
@CHQTYPE char(1),
@PRINTER_SELECTED varchar(250),
@PAPER_SOURCE varchar(50),
@PAGE_HEIGHT float,
@ALLOW_OFFSET char(1),
@PAGE_HEIGHT_OFFSET float,
@OFFSET_FREQ int,
@ACCNO_TOP float,
@ACCNO_LEFT float, 
@ACCNAME_TOP float,
@ACCNAME_LEFT float,
@BRANCH_TOP float,
@BRANCH_LEFT float,
@REMARKS_TOP float,
@REMARKS_LEFT float,
@USERID char(10)

AS

BEGIN
IF EXISTS (SELECT * FROM TBL_PRINTSETTINGS WHERE CHQTYPE = @CHQTYPE)

UPDATE TBL_PRINTSETTINGS SET
PRINTER_SELECTED = @PRINTER_SELECTED, 
PAPER_SOURCE = @PAPER_SOURCE,
PAGE_HEIGHT = @PAGE_HEIGHT, 
ALLOW_OFFSET = @ALLOW_OFFSET,
PAGE_HEIGHT_OFFSET = @PAGE_HEIGHT_OFFSET,
OFFSET_FREQ = @OFFSET_FREQ,
ACCNO_TOP= @ACCNO_TOP,
ACCNO_LEFT = @ACCNO_LEFT,
ACCNAME_TOP = @ACCNAME_TOP ,
ACCNAME_LEFT= @ACCNAME_LEFT ,
BRANCH_TOP = @BRANCH_TOP,
BRANCH_LEFT=@BRANCH_LEFT,
REMARKS_TOP=@REMARKS_TOP,
REMARKS_LEFT=@REMARKS_LEFT,
MODIFIED_BY = @USERID, 
MODIFIED_DATE = GETDATE()
WHERE CHQTYPE = @CHQTYPE

ELSE 

INSERT INTO TBL_PRINTSETTINGS (CHQTYPE,PRINTER_SELECTED,PAPER_SOURCE,
PAGE_HEIGHT,ALLOW_OFFSET,PAGE_HEIGHT_OFFSET,OFFSET_FREQ,ACCNO_TOP,
ACCNO_LEFT,ACCNAME_TOP,ACCNAME_LEFT,BRANCH_TOP,BRANCH_LEFT,REMARKS_TOP,REMARKS_LEFT,
CREATED_BY,CREATED_DATE)

VALUES

(@CHQTYPE,@PRINTER_SELECTED,@PAPER_SOURCE,@PAGE_HEIGHT,@ALLOW_OFFSET,@PAGE_HEIGHT_OFFSET,
@OFFSET_FREQ,@ACCNO_TOP,
@ACCNO_LEFT,@ACCNAME_TOP,@ACCNAME_LEFT,@BRANCH_TOP,@BRANCH_LEFT,@REMARKS_TOP,
@REMARKS_LEFT,@USERID,GETDATE())

END

GO
/****** Object:  StoredProcedure [dbo].[p_SplitIntoColumn]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Dinesh
-- Create date: 2022-7-21
-- Description:	SP Split comma seperated value into column
-- =============================================
CREATE PROCEDURE [dbo].[p_SplitIntoColumn]
	
AS
BEGIN



 --SELECT Row_Number() OVER(ORDER BY id) as sn,Data into #temp1  from
	--		(
	--	 SELECT Row_Number() OVER(ORDER BY id) AS RowNumber,id,data FROM DB2data  where  len(data) >85
	--	 ) t
	--	 WHERE t.RowNumber % 2 = 1

	--	 SELECT Row_Number() OVER(ORDER BY id) as sn,data into #temp2  from
	--		(
	--	 SELECT Row_Number() OVER(ORDER BY id) AS RowNumber,id,data FROM DB2data where  len(data) >85
		 
	--	 ) t
	--	 WHERE t.RowNumber % 2 = 0

 --select a.sn, REplace(a.data + b.Data,'||',',') as filecontent into #temp3  from #temp1 as a inner join #temp2 as b on a.sn=b.sn
--space , new line remove 
 -- select 
 --REPLACE(LTRIM(RTRIM(REPLACE(REPLACE(REPLACE(REPLACE(data, CHAR(10), CHAR(32)),CHAR(13), CHAR(32)),CHAR(160), CHAR(32)),CHAR(9),CHAR(32)))),'"','')
 --as filecontent  from   DB2data

 --select  REplace(Data,'||',',') as filecontent into #temp  from DB2data

 select  REplace(RTRIM(LTRIM(REplace(Data,'"',''))) ,'||',',') as filecontent into #temp   from DB2data

 Insert into ChqIssueMasterDB2
 (
       [CURRENCYCODE]
      ,[ACCOUNTNO]
      ,[BRANCHCODE]
      ,[FULLNAME]
      ,[RELATIONSHIPNO]
      ,[RELATIONSHIPTYPE]
      ,[CHQSTARTNO]
      ,[ISSUEDATE]
      ,[REQID]
      ,[NOOFLEAVES]
      ,[BOOKSTATUS]
      ,[MAKERID]
      ,[MAKERDATE]
      ,[MAKERTIME]
      ,[MAKERDATETIME]
      ,[MAKERIPADDRESS]
      ,[CHECKERID]
      ,[CHECKERDATE]
      ,[CHECKERTIME]
      ,[CHECKERDATETIME]
      ,[CHECKERIPADDRESS]
      ,[STATUSFLAG]
      ,[NOOFTIMESPRINTED]
      ,[PRINTEDDATE]

      ,[FLATNO]
      ,[BLDGNAME]
      ,[NRLANDMRK]
      ,[STREET]
      ,[TRS]
      ,[MOB]
      ,[INFOCODE]
 )
select ParsedData.* 
from #temp mt
cross apply ( select str = mt.filecontent + ',,' ) f1
cross apply ( select p1  = charindex( ',', str ) ) ap1
cross apply ( select p2  = charindex( ',', str, p1 + 1 ) ) ap2
cross apply ( select p3  = charindex( ',', str, p2 + 1 ) ) ap3
cross apply ( select p4  = charindex( ',', str, p3 + 1 ) ) ap4
cross apply ( select p5  = charindex( ',', str, p4 + 1 ) ) ap5
cross apply ( select p6  = charindex( ',', str, p5 + 1 ) ) ap6
cross apply ( select p7  = charindex( ',', str, p6 + 1 ) ) ap7
cross apply ( select p8  = charindex( ',', str, p7 + 1 ) ) ap8
cross apply ( select p9  = charindex( ',', str, p8 + 1 ) ) ap9
cross apply ( select p10 = charindex( ',', str, p9 + 1 ) ) ap10
cross apply ( select p11 = charindex( ',', str, p10 + 1 ) ) ap11
cross apply ( select p12 = charindex( ',', str, p11 + 1 ) ) ap12
cross apply ( select p13 = charindex( ',', str, p12 + 1 ) ) ap13
cross apply ( select p14 = charindex( ',', str, p13 + 1 ) ) ap14
cross apply ( select p15 = charindex( ',', str, p14 + 1 ) ) ap15
cross apply ( select p16 = charindex( ',', str, p15 + 1 ) ) ap16
cross apply ( select p17 = charindex( ',', str, p16 + 1 ) ) ap17
cross apply ( select p18 = charindex( ',', str, p17 + 1 ) ) ap18
cross apply ( select p19 = charindex( ',', str, p18 + 1 ) ) ap19
cross apply ( select p20 = charindex( ',', str, p19 + 1 ) ) ap20

cross apply ( select p21 = charindex( ',', str, p20 + 1 ) ) ap21
cross apply ( select p22 = charindex( ',', str, p21 + 1 ) ) ap22
cross apply ( select p23 = charindex( ',', str, p22 + 1 ) ) ap23
cross apply ( select p24 = charindex( ',', str, p23 + 1 ) ) ap24
cross apply ( select p25 = charindex( ',', str, p24 + 1 ) ) ap25
cross apply ( select p26 = charindex( ',', str, p25 + 1 ) ) ap26

cross apply ( select p27 = charindex( ',', str, p26 + 1 ) ) ap27
cross apply ( select p28 = charindex( ',', str, p27 + 1 ) ) ap28
cross apply ( select p29 = charindex( ',', str, p28 + 1 ) ) ap29
cross apply ( select p30 = charindex( ',', str, p29 + 1 ) ) ap30
cross apply ( select p31 = charindex( ',', str, p30 + 1 ) ) ap31



cross apply ( select CURRENCYCODE = substring( str, 1, p1-1 )                   
                    ,ACCOUNTNO = substring( str, p1+1, p2-p1-1 )
					,BRANCHCODE = substring( str, p2+1, p3-p2-1 )
					,FULLNAME = substring( str, p3+1, p4-p3-1 )
					,RELATIONSHIPNO = substring( str, p4+1, p5-p4-1 )
					,RELATIONSHIPTYPE = substring( str, p5+1, p6-p5-1 )
					,CHQSTARTNO   = substring( str, p6+1, p7-p6-1 )
					,ISSUEDATE = substring( str, p7+1, p8-p7-1 )
					,REQID = substring( str, p8+1, p9-p8-1 )
					,NOOFLEAVES = substring( str, p9+1, p10-p9-1 )
					,BOOKSTATUS = substring( str, p10+1, p11-p10-1 )
					,MAKERID= substring( str, p11+1, p12-p11-1 )
					,MAKERDATE = substring( str, p12+1, p13-p12-1 )
					,MAKERTIME = substring( str, p13+1, p14-p13-1 )
					,MAKERDATETIME  = substring( str, p14+1, p15-p14-1 )
					,MAKERIPADDRESS = substring( str, p15+1, p16-p15-1 ) 
					,CHECKERID = substring( str, p16+1, p17-p16-1 )
					,CHECKERDATE = substring( str, p17+1, p18-p17-1 )
					,CHECKERTIME     = substring( str, p18+1, p19-p18-1 )
					,CHECKERDATETIME = substring( str, p19+1, p20-p19-1 )


					,CHECKERIPADDRESS = substring( str, p20+1, p21-p20-1 )
					,STATUSFLAG = substring( str, p21+1, p22-p21-1 )
					,NOOFTIMESPRINTED = substring( str, p22+1, p23-p22-1 )
					,PRINTEDDATE = substring( str, p23+1, p24-p23-1 )
					,FLATNO = substring( str, p24+1, p25-p24-1 )
					,BLDGNAME = substring( str, p25+1, p26-p25-1 )

					
					,NRLANDMRK = substring( str, p26+1, p27-p26-1 )
					,STREET = substring( str, p27+1, p28-p27-1 )
					,TRS = substring( str, p28+1, p29-p28-1 )
					,MOB = substring( str, p29+1, p30-p29-1 )
					,INFOCODE = substring( str, p30+1, p31-p30-1 )
          ) ParsedData
		  where 
  CHECKERDATETIME > (select * from [dbo].[ControlTable]) 
		 
		  drop table #temp

		   --update ChqIssueMasterDB2 set [L_STATUS] = 'W'
		   --UPDATE ControlTable SET LastTimeStamp=(SELECT MAX(CHECKERDATETIME) FROM ChqIssueMasterDB2)
END
GO
/****** Object:  StoredProcedure [dbo].[p_updateChecknNoandLeave]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[p_updateChecknNoandLeave]    
@checkStartno AS varchar(50),    
@Noofleave AS varchar(50),    
@IssueID  AS int,    
@UpdatedBy as varchar(50)    
AS    
   declare @oldLeaf varchar(100)  
declare @oldChequeStartNo varchar(100)   
     
select @oldChequeStartNo = CHQSTARTNO, @oldLeaf = NOOFLEAVES  from CHQISSUEMASTER where  ISSUEID = @IssueID   
      
update CHQISSUEMASTER  set CHQSTARTNO = @checkStartno,  NOOFLEAVES = @Noofleave,   L_REVIEWEDBY = @UpdatedBy   where ISSUEID = @IssueID    
     
     
--update Chequeissuemasterlist    
--set C_CHQSTARTNOChange = @checkStartno,    
--C_NumberofLeaves = @Noofleave,    
--L_REVIEWEDBY = @UpdatedBy    
-- where ISSUEID = @IssueID


declare @ActivityDesc varchar(100)  
select @ActivityDesc =  'ChequeBook No: ' + Cast(@ISSUEID as varchar(20)) + ' Cheque startno and Leaf,  reset'  + 'ChequeLeaf :' + @oldLeaf + 'to ' + @Noofleave +', ChqStartNo :' + @oldChequeStartNo + 'to ' + @checkStartno
  
EXEC  p_AddUserActivityLog   
@UpdatedBy,  
'Cheque Book Print Undo',  
@ActivityDesc,  
null
GO
/****** Object:  StoredProcedure [dbo].[p_updateChecknNoandLeavs]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[p_updateChecknNoandLeavs]
@checkStartno AS INT,
@Noofleave AS INT,
@userId as Char(7)
AS

 
  
  
update CHQISSUEMASTER
set CHQSTARTNO = @checkStartno,
NOOFLEAVES = @Noofleave
 where ISSUEID = @userId
 
 
update Chequeissuemasterlist
set C_CHQSTARTNOChange = @checkStartno,
C_NumberofLeaves = @Noofleave
 where ISSUEID = @userId
GO
/****** Object:  StoredProcedure [dbo].[p_UpdateChqBookPrintFlag]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[p_UpdateChqBookPrintFlag]
@ISSUEID as BigInt,
@UserID as Char(7)
As
UPDATE CHQISSUEMASTER SET L_PRINTEDBY=@UserID,L_STATUS='P',L_PRINTEDDATE=GETDATE(),L_NOOFPRINT=1
	WHERE ISSUEID=@ISSUEID

declare @ActivityDesc varchar(100)
select @ActivityDesc =  'ChequeBook No: ' + Cast(@ISSUEID as varchar(20)) + ' Printed'

EXEC  p_AddUserActivityLog 
@UserID,
'Cheque Book Print',
@ActivityDesc,
null
GO
/****** Object:  StoredProcedure [dbo].[p_UpdateRequisitionFormPrintFlag]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[p_UpdateRequisitionFormPrintFlag]
(
	@ISSUEID AS BIGINT,
	@UserID AS CHAR(7)
)
AS
BEGIN

	UPDATE CHQISSUEMASTER
		SET C_PRINTEDBY=@UserID,C_STATUS='1',C_PRINTEDDATE=GETDATE(),C_NOOFPRINT=1
		WHERE ISSUEID=@ISSUEID;


DECLARE @ActivityDesc VARCHAR(100);
SELECT @ActivityDesc =  'ChequeBook No: ' + Cast(@ISSUEID as varchar(20)) + ' Printed'

EXEC  p_AddUserActivityLog @UserID,'Requisition form printed',@ActivityDesc,null

END
GO
/****** Object:  StoredProcedure [dbo].[P_UserInfoAuditLog]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[P_UserInfoAuditLog]
(
  @UserCode VARCHAR(50),
  @UserName VARCHAR(100),
  @RequestType VARCHAR(100) NULL,
  @OldValue nvarchar(100) NULL,
  @NewValue nvarchar(100) NULL,
  @AuditDescription VARCHAR(200),
  @Ip VARCHAR(100),
  @MachineName VARCHAR(100),
  @RoleId varchar(50),
  @Url VARCHAR(150)

)
AS
BEGIN
--INSERT INTO UserInfoAuditLog(USER_CODE,USER_NAME,REQUEST_TYPE,AUDIT_DATE,AUDIT_DESC,IP,MACHINE_NAME,ROLE_ID,url)
--VALUES(@UserCode,@UserName,@RequestType,GETDATE(),@AuditDescription,@Ip,@MachineName,@RoleId,@Url)

INSERT INTO UserInfoAuditLog(USER_CODE,USER_NAME,REQUEST_TYPE,OLD_VALUE,NEW_VALUE,AUDIT_DATE,AUDIT_DESC,IP,MACHINE_NAME,ROLE_ID,url)
VALUES(@UserCode,@UserName,@RequestType,@OldValue,@NewValue,GETDATE(),@AuditDescription,@Ip,@MachineName,@RoleId,@Url)
END
GO
/****** Object:  StoredProcedure [dbo].[SP_CheckLogInLogOutStatus]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_CheckLogInLogOutStatus]
(
 @PSID VARCHAR(50)
)
AS 
BEGIN


DECLARE @LastSystemUsedTime DATETIME,
        @TimeDifference VARCHAR(50)

SET @LastSystemUsedTime=(SELECT MAX(AUDIT_DATE)  FROM UserInfoauditLog
                       WHERE USER_CODE=@PSID)
SET @TimeDifference=DATEDIFF(MINUTE,CAST(@LastSystemUsedTime AS datetime),GETDATE());

IF(CAST(@TimeDifference AS INT) >15)
BEGIN
   UPDATE TBL_USER
   SET
   IsLogin=9
   WHERE [USER_ID]=@PSID
    
END
  
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_CheckUserExists]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_CheckUserExists]
(
	@UserId VARCHAR(255)
)
AS
BEGIN   
	  DECLARE @Counter INT;
	  SET @Counter = (SELECT COUNT ([USER_ID]) FROM TBL_USER WHERE LOWER([USER_ID])=LOWER(@UserId))    
      
      IF @Counter>0
      BEGIN  
           SELECT  1  
      END  
      ELSE  
      BEGIN  
            SELECT -1 
      END  
	
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetAuditLogReport]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
CREATE PROC [dbo].[sp_GetAuditLogReport]
(
	@FromDate	DATE,
	@ToDate		DATE,
	@UserLoginId VARCHAR(100),
	@Start		INT,
	@Last		INT
)
AS

BEGIN
		IF(@FromDate<>'' AND @ToDate<>'' )
			BEGIN
					SELECT *FROM (SELECT ROW_NUMBER() OVER(ORDER BY Id DESC) AS RowNo, Id,LogTimeStamp as [TimeStamp],IP,
							MachineName,UserLoginId AS UserInfo,RequestType AS Event 
							,Url,Audit_Desc AS EventDesc,OldValue, NewValue, UserID AS ReflectedUserID, 
							CAST(FORMAT(LogTimeStamp,'hh:mm tt')AS VARCHAR) AS  TimeSpan
							FROM UserAuditLog
							WHERE (CAST(LogTimeStamp AS DATE)>=@FromDate 
							AND CAST([LogTimeStamp] AS DATE) <= @ToDate) 
							AND UserLoginId = isnull(@UserLoginId,UserLoginId)
							)a
							WHERE RowNo BETWEEN @Start AND @Last
							ORDER BY [TimeStamp] DESC;

					SELECT COUNT( Id) AS TotalRow FROM UserAuditLog
							WHERE (CAST(LogTimeStamp AS DATE)>=@FromDate 
							AND CAST(LogTimeStamp AS DATE) <= @ToDate) 
							AND UserLoginId = isnull(@UserLoginId,UserLoginId);
					END
		ELSE
			BEGIN
				SELECT *FROM (SELECT ROW_NUMBER() OVER(ORDER BY Id DESC) AS RowNo, Id, LogTimeStamp AS [TimeStamp],IP,MachineName,Url,
					SUBSTRING(UserLoginId,14,10) AS UserInfo, SUBSTRING(RequestType,14,2000) As Event,Audit_Desc AS EventDesc,OldValue, NewValue,
					UserID AS ReflectedUserID
					,CAST(FORMAT(LogTimeStamp,'hh:mm tt')AS VARCHAR) AS TimeSpan 
					FROM UserAuditLog
					)a
					WHERE RowNo BETWEEN @Start AND @Last
					ORDER BY [TimeStamp] DESC;

				SELECT COUNT( Id) AS TotalRow FROM UserAuditLog;
			END
 END

 
GO
/****** Object:  StoredProcedure [dbo].[sp_GetChqBookReviewList_New]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_GetChqBookReviewList_New]
(
	@UserID VARCHAR(255)=NULL,  
	@RelationShipType varchar(30),
	@FilterByMakerID BIT,    
	@Start	INT,      
	@Last   INT
)
AS
--SELECT * FROM Chequeissuemasterlist WHERE L_STATUS='P' AND C_STATUS = '1'  
	
		BEGIN
			SELECT *FROM (SELECT ROW_NUMBER() OVER(ORDER BY ISSUEID DESC) AS RowNo, M.*,B.BRANCHNAME FROM CHQISSUEMASTER M
			 JOIN BRANCHTABLE B  ON cast(M.BRANCHCODE AS INT) =cast( B.BRANCHCODE AS INT)
				WHERE M. L_STATUS='P'
				AND M.C_STATUS = '1'
				) c WHERE RowNo BETWEEN @Start AND @Last;
				   
			SELECT COUNT(*)  as TotalRows  FROM CHQISSUEMASTER M JOIN BRANCHTABLE B  ON 
			  CAST(M.BRANCHCODE AS INT) = CAST(B.BRANCHCODE AS INT) 
			 	AND M. L_STATUS='P'
				AND M.C_STATUS = '1'	

		END

GO
/****** Object:  StoredProcedure [dbo].[sp_GetChqIssueBuffer_New]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetChqIssueBuffer_New]
(
	@Start		INT,
	@Last		INT
)
AS
BEGIN
	SELECT *FROM (SELECT ROW_NUMBER() OVER(ORDER BY [ACCOUNTNO] DESC) AS RowNo, CURRENCYCODE ,[ACCOUNTNO]
		,chq.[BRANCHCODE],brn.BRANCHNAME,[FULLNAME],[RELATIONSHIPNO]
		,[RELATIONSHIPTYPE],[CHQSTARTNO]
		,[ISSUEDATE],[REQID],[NOOFLEAVES],[BOOKSTATUS]
		,[MAKERID],[MAKERDATE],[MAKERTIME],[MAKERDATETIME],[MAKERIPADDRESS],[CHECKERID],[CHECKERDATE],[CHECKERTIME],[CHECKERDATETIME]
		,[CHECKERIPADDRESS],[STATUSFLAG],[NOOFTIMESPRINTED],[PRINTEDDATE]
		,[FLATNO],[BLDGNAME],[NRLANDMRK],[STREET],[TRS],[MOB]
		FROM CHQISSUEBUFFER as chq
		Inner join BRANCHTABLE as brn On chq.BRANCHCODE = brn.BRANCHCODE 
		) A
		WHERE RowNo BETWEEN @Start AND @Last

		SELECT COUNT( [ACCOUNTNO]) AS TotalRow FROM CHQISSUEBUFFER;
							
END


GO
/****** Object:  StoredProcedure [dbo].[sp_GetChqIssueMasterDB2_fetch]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetChqIssueMasterDB2_fetch]  
(
 @Start  INT,  
 @Last  INT  
)  
AS  
BEGIN  
 SELECT *FROM (SELECT ROW_NUMBER() OVER(ORDER BY [ACCOUNTNO] DESC) AS RowNo, CURRENCYCODE ,[ACCOUNTNO]  
  ,chq.[BRANCHCODE],brn.BRANCHNAME,[FULLNAME],[RELATIONSHIPNO]  
  ,[RELATIONSHIPTYPE],[CHQSTARTNO]  
  ,[ISSUEDATE],[REQID],[NOOFLEAVES],[BOOKSTATUS]  
  ,[MAKERID],[MAKERDATE],[MAKERTIME],[MAKERDATETIME],[MAKERIPADDRESS],[CHECKERID],[CHECKERDATE],[CHECKERTIME],[CHECKERDATETIME]  
  ,[CHECKERIPADDRESS],[STATUSFLAG],[NOOFTIMESPRINTED],[PRINTEDDATE]  
  ,[FLATNO],[BLDGNAME],[NRLANDMRK],[STREET],[TRS],[MOB]  
  FROM ChqIssueMasterDB2 as chq  
  Inner join BRANCHTABLE as brn On chq.BRANCHCODE = brn.BRANCHCODE   
  ) A  
  WHERE RowNo BETWEEN @Start AND @Last  
  
  SELECT COUNT( [ACCOUNTNO]) AS TotalRow FROM ChqIssueMasterDB2;  
         
END  
  
  
GO
/****** Object:  StoredProcedure [dbo].[sp_GetChqueBookPrintItem_NEW]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_GetChqueBookPrintItem_NEW]
(
	@UserID VARCHAR(255)=NULL,  
	@RelationShipType varchar(30),
	@FilterByMakerID BIT,    
	@Start	INT,      
	@Last   INT
)
AS
BEGIN
	
	IF(@FilterByMakerID=1)
		BEGIN
			 SELECT *FROM (SELECT ROW_NUMBER() OVER(ORDER BY ISSUEID DESC) AS RowNo,M.*,B.BRANCHNAME FROM CHQISSUEMASTER M JOIN
			BRANCHTABLE B  ON 
			CAST(M.BRANCHCODE AS INT) = CAST(B.BRANCHCODE AS INT) 
			AND  M.L_STATUS='W'	
			AND M.MAKERID =@UserID
			AND M.RELATIONSHIPTYPE =CAST(ISNULL(@RelationShipType,RELATIONSHIPTYPE) AS CHAR(1))
			) c WHERE RowNo BETWEEN @Start AND @Last   

			SELECT COUNT(*)  as TotalRows  FROM CHQISSUEMASTER M JOIN BRANCHTABLE B  ON 
			  CAST(M.BRANCHCODE AS INT) = CAST(B.BRANCHCODE AS INT) 
			  AND  M.L_STATUS='W'	
			  AND M.RELATIONSHIPTYPE =CAST(ISNULL(@RelationShipType,RELATIONSHIPTYPE) AS CHAR(1))
			  AND M.MAKERID =@UserID
		END
	ELSE
		BEGIN
			SELECT *FROM (SELECT ROW_NUMBER() OVER(ORDER BY ISSUEID DESC) AS RowNo, M.*,B.BRANCHNAME FROM CHQISSUEMASTER M
			 JOIN BRANCHTABLE B  ON cast(M.BRANCHCODE AS INT) =cast( B.BRANCHCODE AS INT)
				WHERE M. L_STATUS='W'
				AND M.RELATIONSHIPTYPE =CAST(ISNULL(@RelationShipType,RELATIONSHIPTYPE) AS CHAR(1))
				) c WHERE RowNo BETWEEN @Start AND @Last;
				   
			SELECT COUNT(*)  as TotalRows  FROM CHQISSUEMASTER M JOIN BRANCHTABLE B  ON 
			  CAST(M.BRANCHCODE AS INT) = CAST(B.BRANCHCODE AS INT) 
			  AND M.RELATIONSHIPTYPE =CAST(ISNULL(@RelationShipType,RELATIONSHIPTYPE) AS CHAR(1))
			  AND  M.L_STATUS='W'	

		END

END
GO
/****** Object:  StoredProcedure [dbo].[SP_GETLOGLIST]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GETLOGLIST]
(
@fromdate VARCHAR(50)=NULL,
@todate VARCHAR(50)=NULL,
@Start INT,
@Last INT
)
	
AS
BEGIN
	SET NOCOUNT ON;

IF(@FromDate<>'' AND @ToDate<>'' )
BEGIN
SELECT ut.*FROM
(SELECT ROW_NUMBER() OVER(ORDER BY Id DESC) AS RowNo
      ,a.ID
      ,a.IP
      ,a.MachineName
      ,a.UserID
      ,a.UserLoginId
	  ,a.RequestType
	  ,a.OldValue
	  ,a.NewValue
      ,a.Audit_Desc
      ,a.LogTimeStamp
 from UserAuditLog as a  
WHERE  cast ( a.LogTimeStamp as date) >= @fromdate AND cast ( a.LogTimeStamp as date) <= @todate
)ut
WHERE ut.RowNo BETWEEN @Start AND @Last
ORDER BY ut.Id DESC

SELECT COUNT(Id) AS TotalRow FROM UserAuditLog 	
	 WHERE  cast ( LogTimeStamp as date) >= @fromdate AND cast ( LogTimeStamp as date) <= @todate
	 END

ELSE
	 BEGIN
	 SELECT ut.*FROM
(SELECT ROW_NUMBER() OVER(ORDER BY Id DESC) AS RowNo
      ,a.ID
      ,a.IP
      ,a.MachineName
      ,a.UserID
      ,a.UserLoginId
	  ,a.RequestType
	  ,a.OldValue
	  ,a.NewValue
      ,a.Audit_Desc
      ,a.LogTimeStamp
 from UserAuditLog as a 
)ut
WHERE ut.RowNo BETWEEN @Start AND @Last
ORDER BY ut.Id DESC

SELECT COUNT(Id) AS TotalRow FROM UserAuditLog 	
	
	 END
END

GO
/****** Object:  StoredProcedure [dbo].[sp_GetMenuList]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:  <Lokendra Chand>  
-- Create date: <2022-01-20>  
-- Description: <Gets Menu List>  
-- =============================================  
--EXEC sp_GetMenuList 'Admin'  
CREATE PROCEDURE [dbo].[sp_GetMenuList]  
(  
 @RoleDesc varchar(50)='ADMIN'  
)  
AS  
BEGIN  
   SELECT m.MenuId,m.MenuName, m.MenuUrl,m.MenuParentID, m.MenuOrderingNo,m.MenuClass, m.MenuIcon, m.IsActive, m.SubMenuOrderNo     
   FROM MenuMaster m INNER JOIN RoleMenuMapping r  ON m.MenuId = r.MenuId  
   INNER JOIN RoleMaster rm ON rm.RoleDesc = r.RoleDesc  
   WHERE (rm.RoleDesc =@RoleDesc)  
   AND m.IsActive=1      
   
END  
  
  
GO
/****** Object:  StoredProcedure [dbo].[sp_GetPeriodicPrintReport_New]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_GetPeriodicPrintReport_New]
(
    @FromDate VARCHAR(10),
    @ToDate VARCHAR(10),
    @BranchCode VARCHAR(10),
    @Start INT,
    @Last INT
)
AS
BEGIN
    
  SELECT DISTINCT  top 10  1 AS CHQSTARTNO, M.CURRENCYCODE,1 as ISSUEID, M.ACCOUNTNO,m.L_STATUS, m.BRANCHCODE, m.FULLNAME, m.RELATIONSHIPTYPE, m.NOOFLEAVES,m.STATUSFLAG,
   m.INFOCODE,m.MOB, m.L_REVIEWEDBY, '2022-01-01' as L_REVIEWEDDATE, GETDATE() AS L_PRINTEDDATE,
   A.USER_NAME as MakerName,B.USER_NAME as ReviewerName
 FROM CHQISSUEMASTER M,TBL_USER A,TBL_USER B where
-- DATEDIFF(DAY,L_PRINTEDDATE,@FromDate)<=0
--AND DATEDIFF(DAY,L_PRINTEDDATE,@ToDate)>=0
  --AND
 -- M.L_PRINTEDBY=A.User_ID
 --AND M.L_REVIEWEDBY=B.User_ID
 L_Status = 'R' and L_STATUS NOT IN ('D')


     select count (*) as TotalRow from (SELECT  DISTINCT   1 AS CHQSTARTNO, M.CURRENCYCODE,1 as ISSUEID, M.ACCOUNTNO,m.L_STATUS, m.BRANCHCODE, m.FULLNAME, m.RELATIONSHIPTYPE, m.NOOFLEAVES,m.STATUSFLAG,
   m.INFOCODE,m.MOB, m.L_REVIEWEDBY, '2022-01-01' as L_REVIEWEDDATE, GETDATE() AS L_PRINTEDDATE,
   A.USER_NAME as MakerName,B.USER_NAME as ReviewerName
 FROM CHQISSUEMASTER M,TBL_USER A,TBL_USER B where
 DATEDIFF(DAY,L_PRINTEDDATE,@FromDate)<=0
AND DATEDIFF(DAY,L_PRINTEDDATE,@ToDate)>=0
  AND
  M.L_PRINTEDBY=A.User_ID
 AND M.L_REVIEWEDBY=B.User_ID
 AND L_Status = 'R' and L_STATUS NOT IN ('D')) a




 

    --SELECT *FROM (SELECT DISTINCT ROW_NUMBER() OVER(ORDER BY ISSUEID DESC) AS RowNo, 1 as chqstartno,M.ISSUEID,
    -- M.CURRENCYCODE,M.ACCOUNTNO,M.L_STATUS, M.BRANCHCODE,M.FULLNAME,
    --                M.RELATIONSHIPTYPE,M.NOOFLEAVES,M.L_PRINTEDDATE,M.INFOCODE, M.MOB, M.L_REVIEWEDBY,M.L_REVIEWEDDATE ,
    --                A.USER_NAME as MakerName,B.USER_NAME as ReviewerName
    --                FROM CHQISSUEMASTER M,TBL_USER A,TBL_USER B
    --                WHERE BRANCHCODE = ISNULL(@BranchCode, BRANCHCODE)
    --                AND DATEDIFF(DAY,L_PRINTEDDATE,@FromDate)<=0
    --                AND DATEDIFF(DAY,L_PRINTEDDATE,@ToDate)>=0
    --                AND L_STATUS not in ('D') AND L_Status = 'R'
    --                ) C
    --    WHERE RowNo BETWEEN @Start AND @Last



     --SELECT COUNT(ISSUEID) as TotalRow
     --       FROM CHQISSUEMASTER M,TBL_USER A,TBL_USER B
     --       WHERE DATEDIFF(DAY,L_PRINTEDDATE,@FromDate)<=0
     --       AND DATEDIFF(DAY,L_PRINTEDDATE,@ToDate)>=0
     --       AND L_STATUS not in ('D') AND L_Status = 'R'

END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetRequisitionFormPrintItem_New]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetRequisitionFormPrintItem_New]
(
	@UserID VARCHAR(255)=NULL,  
	@RelationShipType varchar(30),
	@FilterByMakerID BIT,    
	@Start	INT,      
	@Last   INT
)
AS
BEGIN
	IF(@FilterByMakerID=1)
		BEGIN
			 SELECT *FROM (SELECT ROW_NUMBER() OVER(ORDER BY ISSUEID DESC) AS RowNo,M.*,B.BRANCHNAME FROM CHQISSUEMASTER M JOIN
				BRANCHTABLE B  ON 
				CAST(M.BRANCHCODE AS INT) = CAST(B.BRANCHCODE AS INT) 
				AND M.C_STATUS='0'
				AND M.MAKERID =@UserID
				AND M.RELATIONSHIPTYPE =CAST(ISNULL(@RelationShipType,RELATIONSHIPTYPE) AS CHAR(1))
				) c WHERE RowNo BETWEEN @Start AND @Last   
	
			SELECT COUNT(*)  as TotalRows  FROM CHQISSUEMASTER M JOIN BRANCHTABLE B  ON 
				  CAST(M.BRANCHCODE AS INT) = CAST(B.BRANCHCODE AS INT) 
				  AND M.C_STATUS='0'
				  AND M.RELATIONSHIPTYPE =CAST(ISNULL(@RelationShipType,RELATIONSHIPTYPE) AS CHAR(1))
				  AND M.MAKERID =@UserID
	END
	ELSE
		BEGIN
			SELECT *FROM (SELECT ROW_NUMBER() OVER(ORDER BY ISSUEID DESC) AS RowNo, M.*,B.BRANCHNAME FROM CHQISSUEMASTER M
			 JOIN BRANCHTABLE B  ON cast(M.BRANCHCODE AS INT) =cast( B.BRANCHCODE AS INT)
				WHERE M.C_STATUS='0'
				AND M.RELATIONSHIPTYPE =CAST(ISNULL(@RelationShipType,RELATIONSHIPTYPE) AS CHAR(1))
				) c WHERE RowNo BETWEEN @Start AND @Last;
				   
			SELECT COUNT(*)  as TotalRows  FROM CHQISSUEMASTER M JOIN BRANCHTABLE B  ON 
			  CAST(M.BRANCHCODE AS INT) = CAST(B.BRANCHCODE AS INT) 
			  AND M.RELATIONSHIPTYPE =CAST(ISNULL(@RelationShipType,RELATIONSHIPTYPE) AS CHAR(1))
			  AND  M.C_STATUS='0'
		END
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetSecurityMatrix]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetSecurityMatrix]
(
	@Start	INT,      
	@Last   INT
)
AS
BEGIN
SELECT *FROM (SELECT ROW_NUMBER() OVER (ORDER BY MM.MenuId ) AS RecNo,mm.MenuName AS MainMenu,SM.MenuName AS SubMenu,rm.RoleDesc
		FROM MenuMaster AS MM
		   LEFT JOIN MenuMaster AS SM ON MM.MenuId=SM.MenuParentID
		   JOIN RoleMenuMapping AS MA ON MA.MenuId=SM.MenuId
		   JOIN RoleMaster AS rm ON rm.RoleDesc=MA.RoleDesc
			WHERE MM.MenuParentID=0
			AND SM.MenuParentID<>0
		)a
		WHERE RecNo BETWEEN @Start AND @Last  

SELECT COUNT(MM.MenuId)  as TotalRows  
		FROM MenuMaster AS MM
		   LEFT JOIN MenuMaster AS SM ON MM.MenuId=SM.MenuParentID
		   JOIN RoleMenuMapping AS MA ON MA.MenuId=SM.MenuId
		   JOIN RoleMaster AS rm ON rm.RoleDesc=MA.RoleDesc
			WHERE MM.MenuParentID=0
			AND SM.MenuParentID<>0
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetUserByUserID]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetUserByUserID]  
(  
 @UserID VARCHAR(30)  
)  
AS  
BEGIN  
 SELECT [USER_ID],[USER_NAME],r.RoleId, r.RoleDesc,EMAIL,CAN_DOWNLOAD,CAN_UNDO_PRINT, IsLogin,LoginAttempt,LoginFailedCount,  
   c.OPTION_VALUE AS STATUS_NAME,  IsLOCKED,
   CASE WHEN LoginFailedCount<3 THEN 0 ELSE 1 END AS IsLockedOut,u.STATUS AS STATUS_ID    
   FROM TBL_USER u JOIN RoleMaster r  
   ON r.RoleId = u.USER_TYPE  
   INNER JOIN TBL_OPTIONLIST as c ON u.STATUS = c.OPTION_ID   
   WHERE [USER_ID]=@UserID  
END  
  
  
GO
/****** Object:  StoredProcedure [dbo].[sp_GetUserList]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetUserList]
(
	@SearchText VARCHAR(50)=NULL,
	@Start INT,
	@Last INT
)
	
AS
BEGIN

	SELECT ut.*FROM (SELECT ROW_NUMBER() OVER(ORDER BY [USER_ID]) AS RowNo,
	       a.USER_ID ,a.USER_NAME ,rm.RoleId, rm.RoleDesc
	      ,a.EMAIL ,a.STATUS ,c.OPTION_VALUE AS STATUS_NAME,a.STATUS AS STATUS_ID
	      ,a.CREATED_BY ,a.CREATED_DATE ,[LastLogin],a.MODIFIED_BY
	      ,a.MODIFIED_DATE ,IsLogin,IsLocked AS IsLocked,CAN_UNDO_PRINT,CAN_DOWNLOAD  
	  FROM TBL_USER as a
	  INNER JOIN RoleMaster as rm ON  a.USER_TYPE = rm.RoleId
	  INNER JOIN TBL_OPTIONLIST as c ON a.STATUS = c.OPTION_ID 
	 WHERE a.[USER_NAME] LIKE '%'+ISNULL(@SearchText,a.[USER_NAME])+'%' 
	)ut
	WHERE ut.RowNo BETWEEN @Start AND @Last
	
	SELECT COUNT([USER_ID]) AS TotalRow FROM TBL_USER u	
		 INNER JOIN RoleMaster as rm ON  u.USER_TYPE = rm.RoleId
	  INNER JOIN TBL_OPTIONLIST as c ON u.STATUS = c.OPTION_ID 
	 WHERE u.[USER_NAME] LIKE '%'+ISNULL(@SearchText,u.[USER_NAME])+'%' 
	
END

GO
/****** Object:  StoredProcedure [dbo].[sp_GetUserLoginProfile]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetUserLoginProfile] 
(
	@PSID VARCHAR(50),
	@l_auth VARCHAR(50)
) 

AS

--EXEC SP_CheckLogInLogOutStatus @PSID
 
 -----------------------------------------------------------------------------------------------------------------
  
DECLARE @CUSTOMPSID VARCHAR(50)
DECLARE @LoginTime INT=(SELECT ISNULL(LoginAttempt,0) FROM TBL_USER WHERE [USER_ID]=LTRIM(RTRIM(@PSID)) AND STATUS = 4)

DECLARE @FailedCount INT =(SELECT ISNULL(LoginFailedCount,0) FROM TBL_USER WHERE [USER_ID]=LTRIM(RTRIM(@PSID)) AND STATUS = 4) ;
 
 IF(@l_auth = 'true' AND @FailedCount < 3)
		BEGIN 
		UPDATE TBL_USER SET 
					LastLogin=GETDATE()
					,islogin =1
					WHERE [USER_ID]=@PSID;

		UPDATE TBL_USER SET LoginFailedCount = 0 WHERE	[USER_ID]=LTRIM(RTRIM(@PSID))

		SELECT u.[USER_ID],u.[USER_NAME],u.USER_TYPE,ur.OPTION_DESC,IsLogin,LoginAttempt,LoginFailedCount,'UNLOCKED' AS LOCKED
                                FROM TBL_USER u
                                inner join TBL_OPTIONLIST ur on u.USER_TYPE=ur.OPTION_ID
                                where u.[USER_ID]=@PSID and u.STATUS=4
		END
ELSE
		BEGIN						
		   Update TBL_USER set  LoginFailedCount = @FailedCount+1 WHERE  [USER_ID]=LTRIM(RTRIM(@PSID)) AND STATUS = 4;  
		 END
	    
	IF @FailedCount >=3
				 BEGIN
			      Update TBL_USER set IsLocked = 8  WHERE  [USER_ID]=@PSID AND STATUS = 4;
				  SELECT u.[USER_ID],u.[USER_NAME],u.USER_TYPE,ur.OPTION_DESC,IsLogin,LoginAttempt,LoginFailedCount,'LOCKED' AS LOCKED
                                FROM TBL_USER u
                                inner join TBL_OPTIONLIST ur on u.USER_TYPE=ur.OPTION_ID
                                where u.[USER_ID]=@PSID and u.STATUS=4

				 END

	IF(@LoginTime < 3 )
		BEGIN
		UPDATE TBL_USER
		SET LoginAttempt=0
		WHERE	[USER_ID]=LTRIM(RTRIM(@PSID))

			SELECT u.[USER_ID],u.[USER_NAME],u.USER_TYPE,ur.OPTION_DESC,IsLogin,LoginAttempt,LoginFailedCount,'UNLOCKED' AS LOCKED
                                FROM TBL_USER u
                                inner join TBL_OPTIONLIST ur on u.USER_TYPE=ur.OPTION_ID
                                where u.[USER_ID]=@PSID and u.STATUS=4

		END

GO
/****** Object:  StoredProcedure [dbo].[sp_GetUserLoginProfile_New]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_GetUserLoginProfile_New] 
(
	@UserID VARCHAR(50)
) 
AS
BEGIN

  update TBL_USER set islogin = 0

	DECLARE @LastLoginDate DATETIME
    DECLARE @TimeDifference VARCHAR(50)
		
	SET @LastLoginDate = (SELECT MAX(LastLogin) FROM TBL_USER WHERE [USER_ID]=@UserID);
	SET @TimeDifference=DATEDIFF(MINUTE,CAST(@LastLoginDate AS datetime),GETDATE());
	IF(CAST(@TimeDifference AS INT) >15 )
		BEGIN
		   UPDATE TBL_USER
		   SET IsLogin = 0
		   WHERE [USER_ID]=LTRIM(RTRIM(@UserID))

		END

		SELECT [USER_ID],[USER_NAME],r.RoleId, r.RoleDesc, IsLogin,LoginAttempt,LoginFailedCount,
		isnull(IsLocked,0) AS IsLockedOut,LandingPageURL		
		FROM TBL_USER u JOIN RoleMaster r
		ON r.RoleId = u.USER_TYPE
		WHERE [USER_ID]=@UserID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_INSERT_LOG]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_INSERT_LOG]
	(
	@userid nvarchar(50),
	@activitydesc nvarchar(150),
	@logtime nvarchar(100),
	@narrative nvarchar(100) = null,
	@result int = null,
	@filename nvarchar(150) = null,
	@header nvarchar(200) = null,
	@type nvarchar(50) = null,
	@valdate nvarchar(100) = null,
	@valuedate nvarchar(100) = null,
	@risk int = null,
	@oldvalue nvarchar(100) = null,
	@validtime nvarchar(100) = null,
	@reason nvarchar(max) = null
	)
AS
BEGIN
	
	SET NOCOUNT ON;

   INSERT INTO UserActivityLog
   VALUES (@userid,@activitydesc,@logtime,@narrative,@result,@filename,@header,@type,@valdate,@valuedate,@risk,@oldvalue,@validtime,@reason)
END
GO
/****** Object:  StoredProcedure [dbo].[SP_INSERT_USER]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_INSERT_USER]
(
	@USER_CODE varchar(50),
    @USER_NAME varchar(250),
    @EMAIL varchar(50),
    @ROLE_ID int,
    @STATUS_ID int,
	@IS_LOGIN int,
	@CanDownload BIT,
	@CanUndoPrint BIT,
	@IsLocked BIT,
	@CREATED_BY varchar(50)
)
AS
BEGIN
	  IF EXISTS(SELECT * FROM TBL_USER Where USER_ID=RTRIM(@USER_CODE))  
		RAISERROR('User Code already exists.',16,1)  
	  ELSE 
		BEGIN
			INSERT INTO TBL_USER (USER_ID,EBBS_USER_ID, USER_NAME,STATUS,EMAIL,USER_TYPE,IsLogin,LoginFailedCount,CREATED_BY,CREATED_DATE, CAN_UNDO_PRINT, CAN_DOWNLOAD,USER_MODE,IsLocked)
			VALUES (@USER_CODE,@USER_CODE,@USER_NAME,@STATUS_ID, @EMAIL, @ROLE_ID,@IS_LOGIN,0,@CREATED_BY,GETDATE(),@CanUndoPrint,@CanDownload,'N' ,@IsLocked)
		END
END




GO
/****** Object:  StoredProcedure [dbo].[SP_INSERT_USERAUDIT_LOG]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_INSERT_USERAUDIT_LOG]
	(
	@UserCode nvarchar(100),
	@AuditDescription nvarchar(400),
	@UserLoginId nvarchar(100),
	@RequestType nvarchar(100),
	@OldValue nvarchar(100),
	@NewValue nvarchar(100),
	@Ip nvarchar(100),
	@MachineName nvarchar(100)
	)
AS
BEGIN
	
	SET NOCOUNT ON;

   INSERT INTO UserAuditLog
   VALUES (@UserCode,@UserLoginId,@RequestType,@OldValue,@NewValue,@AuditDescription,@Ip,@MachineName,GETDATE())
END
GO
/****** Object:  StoredProcedure [dbo].[sp_InsertUserActivityLog_New]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[sp_InsertUserActivityLog_New]
(
	@IP VARCHAR(50),
	@MachineName VARCHAR(50),
	@UserLoginID VARCHAR(50),
	@Event VARCHAR(200)=NULL,
	@EventDesc VARCHAR(500),
	@Url VARCHAR(200),
	@OldValue VARCHAR(100),
	@NewValue VARCHAR(100),
	@ReflectedUserID VARCHAR(50)=NULL
)
AS
BEGIN

	INSERT INTO UserAuditLog(LogTimeStamp,[IP],MachineName,UserLoginId,RequestType,Url,Audit_Desc, OldValue, NewValue,UserID)
	VALUES (CURRENT_TIMESTAMP,@IP,@MachineName,@UserLoginID,@Event,@Url,@EventDesc, @OldValue, @NewValue,@ReflectedUserID)
END
GO
/****** Object:  StoredProcedure [dbo].[sp_ReviewAllCheques]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_ReviewAllCheques]
@ISSUEID as varchar(max),  
@UserID as Char(7)  
AS  
  
  
BEGIN  

select  @ISSUEID =  ISSUEID from [dbo].[ChqIssueMaster] where L_STATUS = 'P'
UPDATE CHQISSUEMASTER SET L_STATUS='R',L_REVIEWEDBY=@UserID,L_REVIEWEDDATE=GetDate()  
 WHERE L_STATUS = 'P'

--select (ISSUEID) as a from [dbo].[ChqIssueMaster] where L_STATUS = 'P'
  
declare @ActivityDesc varchar(100)  
--select @ActivityDesc =  'ChequeBook No: ' + @ISSUEID + ' Reviewed' 
select @ActivityDesc =  'All cheques Reviewed' 


  
EXEC  p_AddUserActivityLog   
@UserID,  
'Cheque Book Print Review',  
@ActivityDesc,  
null  
END
GO
/****** Object:  StoredProcedure [dbo].[SP_SAVEACCESS]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_SAVEACCESS]
(   
	@menu_id int,
	@usergroup_id int,
	@created_by INT
)AS 
 

BEGIN

	  INSERT INTO TBL_MENUACCESS (UserGroupId,MenuId,CreatedBy,CreatedDate)
	  VALUES (@usergroup_id,@menu_id,@created_by,GETDATE());

	  SELECT SCOPE_IDENTITY();
END
	





 




GO
/****** Object:  StoredProcedure [dbo].[sp_SaveRoleMenuMapping]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_SaveRoleMenuMapping]  
(  
	@RoleDesc VARCHAR(50),  
	@MenuId INT  
)  
AS  
BEGIN  
  
   INSERT INTO RoleMenuMapping (RoleDesc,MenuId)  
   VALUES (@RoleDesc, @MenuId);  
  
   SELECT SCOPE_IDENTITY();  
END
GO
/****** Object:  StoredProcedure [dbo].[SP_UPDATE_USER]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_UPDATE_USER]
	@USER_CODE varchar(50),
    @USER_NAME varchar(250),
    @EMAIL varchar(50),
    @ROLE_ID int,
    @STATUS_ID int,
	@IS_LOGIN int,
	@CanDownload BIT,
	@CanUndoPrint BIT,
	@IsLocked BIT,
	@ModifiedBy varchar(15)
	
AS
BEGIN
	
    UPDATE TBL_USER
           SET USER_NAME = @USER_NAME
              ,EMAIL = @EMAIL
              ,USER_TYPE = @ROLE_ID
              ,STATUS = @STATUS_ID 
	          ,ISLOGIN =@IS_LOGIN
              ,LoginFailedCount = 0 
			  ,CAN_UNDO_PRINT = @CanUndoPrint
			  ,CAN_DOWNLOAD = @CanDownload
			  ,IsLocked=@IsLocked
			  ,MODIFIED_BY = @ModifiedBy
			  ,MODIFIED_DATE =GETDATE()
	  WHERE USER_ID=@USER_CODE

END
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateUserLoginStatus]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_UpdateUserLoginStatus]
(
	@Flag VARCHAR(20),
	@UserId VARCHAR(50),
	@LoginFailedCount INT
)
AS
BEGIN

  update TBL_USER set islogin = 0 WHERE [USER_ID] = @UserId;

	IF(@Flag = 'LOGGED_IN')
		BEGIN
			UPDATE TBL_USER
			SET IsLogin =	1,
				LastLogin=	GETDATE()				
			WHERE [USER_ID] = @UserId;
			
		END
	ELSE IF(@Flag = 'INVALID_CREDENTIAL')
		BEGIN
			DECLARE @IsLockedOut INT = 0;
			SET @LoginFailedCount = (@LoginFailedCount+1);
			IF(@LoginFailedCount>=3)
				BEGIN
					SET @IsLockedOut = 1;
				END

			UPDATE TBL_USER
			SET LoginFailedCount=	(@LoginFailedCount+1)				
			WHERE [USER_ID] = @UserId;			
		END
	ELSE IF(@Flag = 'LOG_OFF')
		BEGIN
			UPDATE TBL_USER
			SET Islogin =	0				
			WHERE [USER_ID] = @UserId;
			
		END
END
GO
/****** Object:  StoredProcedure [dbo].[SP_USER_LOGIN]    Script Date: 2/1/2023 11:17:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_USER_LOGIN]  
(
	 @UserID VARCHAR(50),  
	 @l_auth VARCHAR(50)  
 )
AS  
  
DECLARE @Failedcount AS INT = 1 ;  
DECLARE @v_Usercode As nvarchar(50);  
DECLARE @Username As nvarchar(50);  
DECLARE @Roleid AS INT;  
DECLARE @Status As INT;  
  
BEGIN  
   
 SET NOCOUNT ON;  
  update TBL_USER set islogin = 0
 -----------------------------------------------------------------------------------------------------------------  
  
 SELECT @v_Usercode = [USER_ID],@Username = [USER_NAME],@Roleid = USER_TYPE,@Status = [STATUS],
		@Failedcount = ISNULL(LoginFailedCount,0)   
 FROM TBL_USER WHERE [USER_ID] = @UserID  
  
  IF NOT EXISTS(SELECT * FROM TBL_USER WHERE [USER_ID]=LTRIM(RTRIM(@UserID)))  
	  BEGIN    
		 SELECT 'NOT EXIXT' AS [USER_ID],0 AS [STATUS],'UNLOCKED' AS LOCKED  
	  END  
  
 IF @l_auth = LTRIM(RTRIM('true')) AND @v_Usercode =@UserID AND @Failedcount < 3  
    BEGIN  
		 UPDATE TBL_USER SET   
		 LASTLOGIN=GETDATE()  
		 WHERE [USER_ID]=@UserID;  
  
		SELECT * FROM TBL_USER WHERE [USER_ID] = @UserID AND [STATUS] = 4  
  
		UPDATE TBL_USER 
				SET LoginFailedCount = 0 
				WHERE [USER_ID]=LTRIM(RTRIM(@UserID));   
    END  
ELSE  
     BEGIN 
		UPDATE TBL_USER 
		SET LoginFailedCount = @Failedcount+1 
		WHERE [USER_ID]=LTRIM(RTRIM(@UserID));    
  
     END  
  
  IF @Failedcount >=3 
     BEGIN  
		 SELECT 'LOCKED' AS LOCKED,0 AS [STATUS],@UserID AS USER_CODE;  
			 UPDATE TBL_USER set [STATUS] = 8  WHERE [USER_ID]=LTRIM(RTRIM(@UserID));    
     END  
END 
GO
USE [master]
GO
ALTER DATABASE [ChequePrintSCB] SET  READ_WRITE 
GO
