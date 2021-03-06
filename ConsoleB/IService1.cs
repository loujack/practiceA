﻿using System.Linq;
using System.ServiceModel;

namespace GetLogWCF
{
    // 注意: 您可以使用 [重構] 功能表上的 [重新命名] 命令同時變更程式碼和組態檔中的介面名稱 "IService1"。
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        IQueryable<GetLogDLL.GetLog.RecordLog> GetLog();
    }
}
