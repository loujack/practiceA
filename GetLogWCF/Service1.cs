﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace GetLogWCF
{
    // 注意: 您可以使用 [重構] 功能表上的 [重新命名] 命令同時變更程式碼和組態檔中的類別名稱 "Service1"。
    public class Service1 : IService1
    {
        public IQueryable<RecordLog> GetLog()
        {
            XLogDataContext XLogDB = new XLogDataContext();
            IQueryable<RecordLog> result = XLogDB.RecordLog.Select(o => o).Take(36);
            return result;
        }
    }

}
