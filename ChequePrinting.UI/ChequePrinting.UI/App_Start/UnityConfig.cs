using ChequePrinting.BusinessLogicLayer.Repository.MenuRepository;
using ChequePrinting.BusinessLogicLayer.Repository.SecurityMatrixRepository;
using ChequePrinting.BusinessLogicLayer.Repository.UserRepository;
using ChequePrinting.BusinessLogicLayer.Repository.LoggerRepository;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using ChequePrinting.BusinessLogicLayer.Repository.AccountRepository;
using ChequePrinting.BusinessLogicLayer.Repository.AuditLogRepository;
using ChequePrinting.BusinessLogicLayer.Repository.ChequeBookPrintRepository;
using ChequePrinting.BusinessLogicLayer.Repository.RequisitionRepository;
using ChequePrinting.BusinessLogicLayer.Repository.ChequeBookLogRepository;
using ChequePrinting.BusinessLogicLayer.Repository.Db2DataFetchingRepository;
using ChequePrinting.BusinessLogicLayer.Repository.DailyChequeBookPrintRptRepository;
using ChequePrinting.BusinessLogicLayer.Repository.ConfigRepository;

namespace ChequePrinting.UI
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<ILogger, Logger>();
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<ISecurityMatrixRepository, SecurityMatrixRepository>();
            container.RegisterType<IMenuRepository, MenuRepository>();
            container.RegisterType<IAccountRepository, AccountRepository>();
            container.RegisterType<IAuditLogRepository, AuditLogRepository>();
            container.RegisterType<IChequeBookPrintRepository, ChequeBookPrintRepository>();
            container.RegisterType<IRequisitionFormRepository, RequisitionRepository>();
            container.RegisterType<IChequeBookLogRepository, ChequeBookLogRepository>();
            container.RegisterType<IDb2DataFetchingRepository, Db2DataFetchingRepository>();
            container.RegisterType<IDailyChequeBookPrintRptRepository, DailyChequeBookPrintRptRepository>();
            container.RegisterType<IConfigRepository, ConfigRepository>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}