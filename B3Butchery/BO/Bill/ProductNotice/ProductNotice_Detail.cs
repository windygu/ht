﻿using System;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;

namespace BWP.B3Butchery.BO {
  [Serializable, DFClass, LogicName("生产计划投入明细")] 
  public class ProductNotice_Detail : GoodsDetail {
    public long ProductNotice_ID { get; set; }
 
    [LogicName("加工要求")]
    public string ProduceRequest { get; set; }

    [LogicName("生产日期")]
    public DateTime? ProduceDate { get; set; }

    [LogicName("交货日期")]
    public DateTime? DeliveryDate { get; set; }

    [LogicName("源单据号")]
    public long? DmoID { get; set; }

  }

  [Serializable]
  public class ProductNotice_DetailCollection : DmoCollection<ProductNotice_Detail> {

  }
}
