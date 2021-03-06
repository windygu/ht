﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Frameworks.BO.NamedValueTemplate;
using Forks.EnterpriseServices.JsonRpc;
using Forks.Utils;

namespace BWP.B3Butchery.Hippo.QueryObjs
{
    [RpcObject]
    public class ProductNoticeQueryObj
    {
        public long? ID { get; set; }

        public NamedValue<单据状态>? BillState { get; set; }

        public DateTime? MaxTime { get; set; }

        public DateTime? MinTime { get; set; }

        public long? AccountingUnit_ID { get; set; }

        public string AccountingUnit_Name { get; set; }

        public long? Department_ID { get; set; }

        public string Department_Name { get; set; }

        public long? Employee_ID { get; set; }

        public string Employee_Name { get; set; }

        public long? ProductionUnit_ID { get; set; }

        public string ProductionUnit_Name { get; set; }
    }
}
