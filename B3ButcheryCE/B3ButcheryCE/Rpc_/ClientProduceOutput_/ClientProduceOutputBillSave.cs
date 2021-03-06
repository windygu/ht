﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using B3ButcheryCE.Rpc_;

namespace B3ButcheryCE.Rpc_.ClientProduceOutput_
{
    public class ClientProduceOutputBillSave : ClientBase
    {
        public long ID { get; set; }

        public long AccountingUnit_ID { get; set; }

        public long Department_ID { get; set; }

        public long Domain_ID { get; set; }

        public long User_ID { get; set; }

        public DateTime CreateTime { get; set; }

        public string CollectType { get; set; }

        public long? ProductLinks_ID { get; set; }

        public List<ClientGoods> Details = new List<ClientGoods>();
    }
}
