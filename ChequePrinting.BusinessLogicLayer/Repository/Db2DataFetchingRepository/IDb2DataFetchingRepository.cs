using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChequePrinting.BusinessLogicLayer.Repository.Db2DataFetchingRepository
{
    public interface IDb2DataFetchingRepository
    {
        DataSet GetDataFromDB2(DateTime lastTimeStamp);
    }
}
