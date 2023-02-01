using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChequePrinting.BusinessLogicLayer.Repository.LoggerRepository
{
    public interface ILogger
    {
        void LogError(Exception exception);
    }
}
