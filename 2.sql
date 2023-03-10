USE [ChequePrintSCB]
GO
SET IDENTITY_INSERT [dbo].[MenuMaster] ON 

INSERT [dbo].[MenuMaster] ([MenuId], [MenuName], [MenuUrl], [MenuParentID], [MenuOrderingNo], [MenuClass], [MenuIcon], [IsActive], [SubMenuOrderNo]) VALUES (2, N'Dashboard', N'Home/Index', 0, 1, NULL, NULL, 1, NULL)
INSERT [dbo].[MenuMaster] ([MenuId], [MenuName], [MenuUrl], [MenuParentID], [MenuOrderingNo], [MenuClass], [MenuIcon], [IsActive], [SubMenuOrderNo]) VALUES (3, N'Administration', N'#', 0, 2, NULL, NULL, 1, NULL)
INSERT [dbo].[MenuMaster] ([MenuId], [MenuName], [MenuUrl], [MenuParentID], [MenuOrderingNo], [MenuClass], [MenuIcon], [IsActive], [SubMenuOrderNo]) VALUES (4, N'User Management', N'User/Index', 3, 0, NULL, NULL, 1, 1)
INSERT [dbo].[MenuMaster] ([MenuId], [MenuName], [MenuUrl], [MenuParentID], [MenuOrderingNo], [MenuClass], [MenuIcon], [IsActive], [SubMenuOrderNo]) VALUES (5, N'Access Management', N'MenuAccess/Index', 3, 0, NULL, NULL, 1, 2)
INSERT [dbo].[MenuMaster] ([MenuId], [MenuName], [MenuUrl], [MenuParentID], [MenuOrderingNo], [MenuClass], [MenuIcon], [IsActive], [SubMenuOrderNo]) VALUES (6, N'Audit Log Report', N'AuditLog/Index', 3, 0, NULL, NULL, 1, 3)
INSERT [dbo].[MenuMaster] ([MenuId], [MenuName], [MenuUrl], [MenuParentID], [MenuOrderingNo], [MenuClass], [MenuIcon], [IsActive], [SubMenuOrderNo]) VALUES (7, N'Security Matrix', N'SecurityMatrix/Index', 3, 0, NULL, NULL, 1, 4)
INSERT [dbo].[MenuMaster] ([MenuId], [MenuName], [MenuUrl], [MenuParentID], [MenuOrderingNo], [MenuClass], [MenuIcon], [IsActive], [SubMenuOrderNo]) VALUES (9, N'Report', N'#', 0, 5, NULL, NULL, 1, NULL)
INSERT [dbo].[MenuMaster] ([MenuId], [MenuName], [MenuUrl], [MenuParentID], [MenuOrderingNo], [MenuClass], [MenuIcon], [IsActive], [SubMenuOrderNo]) VALUES (10, N'Cheque Printing', N'#', 0, 4, NULL, NULL, 1, NULL)
INSERT [dbo].[MenuMaster] ([MenuId], [MenuName], [MenuUrl], [MenuParentID], [MenuOrderingNo], [MenuClass], [MenuIcon], [IsActive], [SubMenuOrderNo]) VALUES (11, N'Download Cheque Book Logs', N'ChequeBookLogs/Index', 10, 0, NULL, NULL, 1, 1)
INSERT [dbo].[MenuMaster] ([MenuId], [MenuName], [MenuUrl], [MenuParentID], [MenuOrderingNo], [MenuClass], [MenuIcon], [IsActive], [SubMenuOrderNo]) VALUES (12, N'Cheque Book Print', N'ChequeBookPrint/Index', 10, 0, NULL, NULL, 1, 2)
INSERT [dbo].[MenuMaster] ([MenuId], [MenuName], [MenuUrl], [MenuParentID], [MenuOrderingNo], [MenuClass], [MenuIcon], [IsActive], [SubMenuOrderNo]) VALUES (13, N'Requisition Form Print', N'RequisitionForm/Index', 10, 0, NULL, NULL, 1, 3)
INSERT [dbo].[MenuMaster] ([MenuId], [MenuName], [MenuUrl], [MenuParentID], [MenuOrderingNo], [MenuClass], [MenuIcon], [IsActive], [SubMenuOrderNo]) VALUES (14, N'Process Maker Approval Data', N'ProcessMakerApproval/Index', 7, 0, NULL, NULL, 1, 5)
INSERT [dbo].[MenuMaster] ([MenuId], [MenuName], [MenuUrl], [MenuParentID], [MenuOrderingNo], [MenuClass], [MenuIcon], [IsActive], [SubMenuOrderNo]) VALUES (15, N'Approval Request', N'Approval/Index', 7, 0, NULL, NULL, 1, 6)
INSERT [dbo].[MenuMaster] ([MenuId], [MenuName], [MenuUrl], [MenuParentID], [MenuOrderingNo], [MenuClass], [MenuIcon], [IsActive], [SubMenuOrderNo]) VALUES (16, N'Return Approval Request', N'ApprovalReturnsProcessing/Index', 7, 0, NULL, NULL, 1, 7)
INSERT [dbo].[MenuMaster] ([MenuId], [MenuName], [MenuUrl], [MenuParentID], [MenuOrderingNo], [MenuClass], [MenuIcon], [IsActive], [SubMenuOrderNo]) VALUES (17, N'Approved Returns', N'ApprovedReturns/Index', 7, 0, NULL, NULL, 1, 8)
INSERT [dbo].[MenuMaster] ([MenuId], [MenuName], [MenuUrl], [MenuParentID], [MenuOrderingNo], [MenuClass], [MenuIcon], [IsActive], [SubMenuOrderNo]) VALUES (21, N'Daily Audit Log Report', NULL, 4, 1, NULL, NULL, 1, 1)
INSERT [dbo].[MenuMaster] ([MenuId], [MenuName], [MenuUrl], [MenuParentID], [MenuOrderingNo], [MenuClass], [MenuIcon], [IsActive], [SubMenuOrderNo]) VALUES (22, N'Daily Cheque Book Print Report', N'DailyChequeBookPrintRpt/Index', 9, 1, NULL, NULL, 1, NULL)
INSERT [dbo].[MenuMaster] ([MenuId], [MenuName], [MenuUrl], [MenuParentID], [MenuOrderingNo], [MenuClass], [MenuIcon], [IsActive], [SubMenuOrderNo]) VALUES (23, N'Cheque Book Review', N'ChequeBookReview/Index', 10, 0, NULL, NULL, 1, 4)
SET IDENTITY_INSERT [dbo].[MenuMaster] OFF
GO
SET IDENTITY_INSERT [dbo].[RoleMenuMapping] ON 

INSERT [dbo].[RoleMenuMapping] ([MappingId], [RoleDesc], [MenuId]) VALUES (65, N'MAKER', 2)
INSERT [dbo].[RoleMenuMapping] ([MappingId], [RoleDesc], [MenuId]) VALUES (66, N'MAKER', 10)
INSERT [dbo].[RoleMenuMapping] ([MappingId], [RoleDesc], [MenuId]) VALUES (67, N'MAKER', 11)
INSERT [dbo].[RoleMenuMapping] ([MappingId], [RoleDesc], [MenuId]) VALUES (68, N'MAKER', 12)
INSERT [dbo].[RoleMenuMapping] ([MappingId], [RoleDesc], [MenuId]) VALUES (69, N'MAKER', 13)
INSERT [dbo].[RoleMenuMapping] ([MappingId], [RoleDesc], [MenuId]) VALUES (75, N'CHECKER', 9)
INSERT [dbo].[RoleMenuMapping] ([MappingId], [RoleDesc], [MenuId]) VALUES (76, N'CHECKER', 10)
INSERT [dbo].[RoleMenuMapping] ([MappingId], [RoleDesc], [MenuId]) VALUES (77, N'CHECKER', 12)
INSERT [dbo].[RoleMenuMapping] ([MappingId], [RoleDesc], [MenuId]) VALUES (78, N'CHECKER', 13)
INSERT [dbo].[RoleMenuMapping] ([MappingId], [RoleDesc], [MenuId]) VALUES (79, N'CHECKER', 22)
INSERT [dbo].[RoleMenuMapping] ([MappingId], [RoleDesc], [MenuId]) VALUES (80, N'CHECKER', 23)
INSERT [dbo].[RoleMenuMapping] ([MappingId], [RoleDesc], [MenuId]) VALUES (94, N'ADMIN', 3)
INSERT [dbo].[RoleMenuMapping] ([MappingId], [RoleDesc], [MenuId]) VALUES (95, N'ADMIN', 4)
INSERT [dbo].[RoleMenuMapping] ([MappingId], [RoleDesc], [MenuId]) VALUES (96, N'ADMIN', 5)
INSERT [dbo].[RoleMenuMapping] ([MappingId], [RoleDesc], [MenuId]) VALUES (97, N'ADMIN', 6)
INSERT [dbo].[RoleMenuMapping] ([MappingId], [RoleDesc], [MenuId]) VALUES (98, N'ADMIN', 7)
INSERT [dbo].[RoleMenuMapping] ([MappingId], [RoleDesc], [MenuId]) VALUES (99, N'ADMIN', 9)
INSERT [dbo].[RoleMenuMapping] ([MappingId], [RoleDesc], [MenuId]) VALUES (100, N'ADMIN', 10)
INSERT [dbo].[RoleMenuMapping] ([MappingId], [RoleDesc], [MenuId]) VALUES (101, N'ADMIN', 11)
INSERT [dbo].[RoleMenuMapping] ([MappingId], [RoleDesc], [MenuId]) VALUES (102, N'ADMIN', 12)
INSERT [dbo].[RoleMenuMapping] ([MappingId], [RoleDesc], [MenuId]) VALUES (103, N'ADMIN', 13)
INSERT [dbo].[RoleMenuMapping] ([MappingId], [RoleDesc], [MenuId]) VALUES (104, N'ADMIN', 22)
INSERT [dbo].[RoleMenuMapping] ([MappingId], [RoleDesc], [MenuId]) VALUES (105, N'ADMIN', 23)
SET IDENTITY_INSERT [dbo].[RoleMenuMapping] OFF
GO
INSERT [dbo].[RoleMaster] ([RoleId], [RoleDesc], [IsActive], [LandingPageURL]) VALUES (1, N'ADMIN', 1, N'User/Index')
INSERT [dbo].[RoleMaster] ([RoleId], [RoleDesc], [IsActive], [LandingPageURL]) VALUES (2, N'CHECKER', 1, N'DailyChequeBookPrintRpt/Index')
INSERT [dbo].[RoleMaster] ([RoleId], [RoleDesc], [IsActive], [LandingPageURL]) VALUES (3, N'MAKER', 1, N'ChequeBookPrint/Index')
GO
SET IDENTITY_INSERT [dbo].[SecurityMatrix] ON 

INSERT [dbo].[SecurityMatrix] ([Id], [Role], [Menu], [SubMenu]) VALUES (1, N'ADMIN', N'SYSTEM', N'User Management')
INSERT [dbo].[SecurityMatrix] ([Id], [Role], [Menu], [SubMenu]) VALUES (2, N'', N'', N'Print Parameter Setting')
INSERT [dbo].[SecurityMatrix] ([Id], [Role], [Menu], [SubMenu]) VALUES (3, N'', N'', N'DB2Connection Setting')
INSERT [dbo].[SecurityMatrix] ([Id], [Role], [Menu], [SubMenu]) VALUES (4, N'', N'', N'Log Out')
INSERT [dbo].[SecurityMatrix] ([Id], [Role], [Menu], [SubMenu]) VALUES (5, N'', N'', N'Exit')
INSERT [dbo].[SecurityMatrix] ([Id], [Role], [Menu], [SubMenu]) VALUES (6, N'', N'REPORT', N'Daily Audit Log Report')
INSERT [dbo].[SecurityMatrix] ([Id], [Role], [Menu], [SubMenu]) VALUES (7, N'', N'', N'Activity Log')
INSERT [dbo].[SecurityMatrix] ([Id], [Role], [Menu], [SubMenu]) VALUES (8, N'', N'', N'Security Matrix')
INSERT [dbo].[SecurityMatrix] ([Id], [Role], [Menu], [SubMenu]) VALUES (9, N'MAKER', N'SYSTEM', N'Log Out')
INSERT [dbo].[SecurityMatrix] ([Id], [Role], [Menu], [SubMenu]) VALUES (10, N'', N'', N'Exit')
INSERT [dbo].[SecurityMatrix] ([Id], [Role], [Menu], [SubMenu]) VALUES (11, N'', N'CHEQUE PRINTING', N'Download ChequeBook Log')
INSERT [dbo].[SecurityMatrix] ([Id], [Role], [Menu], [SubMenu]) VALUES (12, N'', N'', N'Cheque Book Print')
INSERT [dbo].[SecurityMatrix] ([Id], [Role], [Menu], [SubMenu]) VALUES (13, N'', N'', N'Requisition Form Print')
INSERT [dbo].[SecurityMatrix] ([Id], [Role], [Menu], [SubMenu]) VALUES (14, N'', N'REPORT ', N'Daily Cheque Book Print Report')
INSERT [dbo].[SecurityMatrix] ([Id], [Role], [Menu], [SubMenu]) VALUES (15, N'CHECKER', N'SYSTEM', N'Log Out')
INSERT [dbo].[SecurityMatrix] ([Id], [Role], [Menu], [SubMenu]) VALUES (16, N'', N'', N'Exit')
INSERT [dbo].[SecurityMatrix] ([Id], [Role], [Menu], [SubMenu]) VALUES (17, N'', N'CHEQUE PRINTING', N'Cheque Book Printing Review')
INSERT [dbo].[SecurityMatrix] ([Id], [Role], [Menu], [SubMenu]) VALUES (18, N'', N'', N'Cheque Book Review List')
INSERT [dbo].[SecurityMatrix] ([Id], [Role], [Menu], [SubMenu]) VALUES (19, N'', N'REPORT', N'Daily Cheque Book Print Report')
SET IDENTITY_INSERT [dbo].[SecurityMatrix] OFF
GO
INSERT [dbo].[TBL_USER] ([USER_ID], [USER_NAME], [PWD], [USER_TYPE], [EMAIL], [STATUS], [USER_GROUP_ID], [CAN_UNDO_PRINT], [EBBS_USER_ID], [CAN_DOWNLOAD], [USER_MODE], [CREATED_BY], [CREATED_DATE], [MODIFIED_BY], [MODIFIED_DATE], [IsLogin], [LastLogin], [LoginAttempt], [LoginFailedCount], [IsLocked]) VALUES (N'1111   ', N'Reporter', NULL, 1, N'abc@gmaill.com', 5, NULL, N'0', N'1111   ', N'0', N'N', N'1111   ', CAST(N'2021-01-11T16:05:02.000' AS DateTime), N'admin  ', CAST(N'2022-08-24T14:24:38.583' AS DateTime), 0, CAST(N'2022-07-12T09:41:44.257' AS DateTime), 0, 0, 1)
INSERT [dbo].[TBL_USER] ([USER_ID], [USER_NAME], [PWD], [USER_TYPE], [EMAIL], [STATUS], [USER_GROUP_ID], [CAN_UNDO_PRINT], [EBBS_USER_ID], [CAN_DOWNLOAD], [USER_MODE], [CREATED_BY], [CREATED_DATE], [MODIFIED_BY], [MODIFIED_DATE], [IsLogin], [LastLogin], [LoginAttempt], [LoginFailedCount], [IsLocked]) VALUES (N'admin  ', N'admin', NULL, 1, N'hi@nepotech.com', 4, NULL, N'0', N'admin  ', N'0', N'N', N'nepo   ', CAST(N'2011-07-14T14:03:59.000' AS DateTime), N'admin  ', CAST(N'2022-09-11T14:47:41.540' AS DateTime), 0, CAST(N'2023-01-03T11:10:55.690' AS DateTime), 0, 0, 0)
INSERT [dbo].[TBL_USER] ([USER_ID], [USER_NAME], [PWD], [USER_TYPE], [EMAIL], [STATUS], [USER_GROUP_ID], [CAN_UNDO_PRINT], [EBBS_USER_ID], [CAN_DOWNLOAD], [USER_MODE], [CREATED_BY], [CREATED_DATE], [MODIFIED_BY], [MODIFIED_DATE], [IsLogin], [LastLogin], [LoginAttempt], [LoginFailedCount], [IsLocked]) VALUES (N'apple  ', N'Reporter', NULL, 1, N'r@gmail.com', 5, NULL, N'1', N'apple  ', N'0', N'N', N'a      ', CAST(N'2021-01-11T15:59:54.000' AS DateTime), N'admin  ', CAST(N'2022-04-15T11:48:32.800' AS DateTime), 0, NULL, 0, 0, 0)
INSERT [dbo].[TBL_USER] ([USER_ID], [USER_NAME], [PWD], [USER_TYPE], [EMAIL], [STATUS], [USER_GROUP_ID], [CAN_UNDO_PRINT], [EBBS_USER_ID], [CAN_DOWNLOAD], [USER_MODE], [CREATED_BY], [CREATED_DATE], [MODIFIED_BY], [MODIFIED_DATE], [IsLogin], [LastLogin], [LoginAttempt], [LoginFailedCount], [IsLocked]) VALUES (N'Checker', N'checker', N'test', 2, N'checker@checker.com', 4, NULL, N'0', N'checker', N'0', N'N', N'Checker', CAST(N'2011-12-08T12:47:36.000' AS DateTime), N'admin  ', CAST(N'2022-07-28T10:33:17.267' AS DateTime), 0, CAST(N'2023-01-04T11:58:38.917' AS DateTime), 0, 0, 0)
INSERT [dbo].[TBL_USER] ([USER_ID], [USER_NAME], [PWD], [USER_TYPE], [EMAIL], [STATUS], [USER_GROUP_ID], [CAN_UNDO_PRINT], [EBBS_USER_ID], [CAN_DOWNLOAD], [USER_MODE], [CREATED_BY], [CREATED_DATE], [MODIFIED_BY], [MODIFIED_DATE], [IsLogin], [LastLogin], [LoginAttempt], [LoginFailedCount], [IsLocked]) VALUES (N'Checkerr', N'checkerr', N'test', 2, N'checker@checker.com', 4, NULL, N'0', N'checker', N'0', N'N', N'Checker', CAST(N'2011-12-08T12:47:36.000' AS DateTime), N'admin  ', CAST(N'2022-07-28T10:33:17.267' AS DateTime), 0, CAST(N'2022-08-15T10:49:46.570' AS DateTime), 0, 0, 0)
INSERT [dbo].[TBL_USER] ([USER_ID], [USER_NAME], [PWD], [USER_TYPE], [EMAIL], [STATUS], [USER_GROUP_ID], [CAN_UNDO_PRINT], [EBBS_USER_ID], [CAN_DOWNLOAD], [USER_MODE], [CREATED_BY], [CREATED_DATE], [MODIFIED_BY], [MODIFIED_DATE], [IsLogin], [LastLogin], [LoginAttempt], [LoginFailedCount], [IsLocked]) VALUES (N'maker  ', N'maker', NULL, 3, N'maker@maker.com', 4, NULL, N'1', N'maker  ', N'1', N'N', N'maker  ', CAST(N'2011-07-14T14:04:27.000' AS DateTime), N'admin  ', CAST(N'2022-05-25T15:48:17.447' AS DateTime), 1, CAST(N'2023-01-17T11:33:38.340' AS DateTime), 0, 0, 0)
INSERT [dbo].[TBL_USER] ([USER_ID], [USER_NAME], [PWD], [USER_TYPE], [EMAIL], [STATUS], [USER_GROUP_ID], [CAN_UNDO_PRINT], [EBBS_USER_ID], [CAN_DOWNLOAD], [USER_MODE], [CREATED_BY], [CREATED_DATE], [MODIFIED_BY], [MODIFIED_DATE], [IsLogin], [LastLogin], [LoginAttempt], [LoginFailedCount], [IsLocked]) VALUES (N'simran', N'simran', NULL, 3, N'maker@maker.com', 4, NULL, N'1', N'maker  ', N'1', N'N', N'maker  ', CAST(N'2011-07-14T14:04:27.000' AS DateTime), N'admin  ', CAST(N'2022-05-25T15:48:17.447' AS DateTime), 0, CAST(N'2022-08-26T14:39:32.683' AS DateTime), 0, 0, 0)
INSERT [dbo].[TBL_USER] ([USER_ID], [USER_NAME], [PWD], [USER_TYPE], [EMAIL], [STATUS], [USER_GROUP_ID], [CAN_UNDO_PRINT], [EBBS_USER_ID], [CAN_DOWNLOAD], [USER_MODE], [CREATED_BY], [CREATED_DATE], [MODIFIED_BY], [MODIFIED_DATE], [IsLogin], [LastLogin], [LoginAttempt], [LoginFailedCount], [IsLocked]) VALUES (N'test   ', N'test', NULL, 1, N'test@gmail.com', 4, NULL, N'1', N'test   ', N'0', N'N', N'test   ', CAST(N'2021-03-04T18:05:34.000' AS DateTime), N'test   ', CAST(N'2021-07-09T15:06:07.000' AS DateTime), 0, CAST(N'2022-07-04T11:02:20.197' AS DateTime), 0, 0, 0)
GO
SET IDENTITY_INSERT [dbo].[TBL_USER_new] ON 

INSERT [dbo].[TBL_USER_new] ([ID], [USER_ID], [USER_NAME], [PWD], [USER_TYPE], [EMAIL], [STATUS], [USER_GROUP_ID], [CREATED_BY], [CREATED_DATE], [MODIFIED_BY], [MODIFIED_DATE], [ISLOGIN], [LASTLOGIN], [LOGIN_FAILED_COUNT]) VALUES (2, N'admin   ', N'admin', NULL, 1, N'admin@12.com', 4, NULL, NULL, CAST(N'2020-01-01T00:00:00.000' AS DateTime), N'admin   ', CAST(N'2022-03-03T16:34:36.520' AS DateTime), 0, CAST(N'2022-03-14T16:17:47.860' AS DateTime), 0)
INSERT [dbo].[TBL_USER_new] ([ID], [USER_ID], [USER_NAME], [PWD], [USER_TYPE], [EMAIL], [STATUS], [USER_GROUP_ID], [CREATED_BY], [CREATED_DATE], [MODIFIED_BY], [MODIFIED_DATE], [ISLOGIN], [LASTLOGIN], [LOGIN_FAILED_COUNT]) VALUES (3, N'checker ', N'checker', NULL, 2, N'checker@gmail.com', 4, NULL, N'admin   ', CAST(N'2022-03-03T16:35:13.407' AS DateTime), NULL, NULL, 0, CAST(N'2022-03-03T16:38:21.057' AS DateTime), 0)
INSERT [dbo].[TBL_USER_new] ([ID], [USER_ID], [USER_NAME], [PWD], [USER_TYPE], [EMAIL], [STATUS], [USER_GROUP_ID], [CREATED_BY], [CREATED_DATE], [MODIFIED_BY], [MODIFIED_DATE], [ISLOGIN], [LASTLOGIN], [LOGIN_FAILED_COUNT]) VALUES (5, N'admin1  ', N'admin1', NULL, 2, N'admin@fmil.com', 4, NULL, NULL, CAST(N'2022-09-09T00:00:00.000' AS DateTime), N'admin   ', CAST(N'2022-03-11T13:00:40.300' AS DateTime), 0, CAST(N'2022-03-03T17:35:12.397' AS DateTime), 0)
INSERT [dbo].[TBL_USER_new] ([ID], [USER_ID], [USER_NAME], [PWD], [USER_TYPE], [EMAIL], [STATUS], [USER_GROUP_ID], [CREATED_BY], [CREATED_DATE], [MODIFIED_BY], [MODIFIED_DATE], [ISLOGIN], [LASTLOGIN], [LOGIN_FAILED_COUNT]) VALUES (6, N'maker   ', N'maker', NULL, 3, N'maker@gmail.com', 4, NULL, N'admin   ', CAST(N'2022-03-03T18:21:00.003' AS DateTime), N'admin   ', CAST(N'2022-03-11T13:00:00.757' AS DateTime), 0, NULL, 0)
SET IDENTITY_INSERT [dbo].[TBL_USER_new] OFF
GO
