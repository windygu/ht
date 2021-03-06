﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Frameworks.BO;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;
using BWP.B3Frameworks.Attributes;
using Forks.Utils;
using BWP.B3Frameworks.BO.MoneyTemplate;

namespace BWP.B3Butchery.BO
{
  [DFClass, Serializable, LogicName("成品入库_明细")]
  public class ProductInStore_Detail : GoodsDetail
  {
    public long ProductInStore_ID { get; set; }

    [LogicName("生产日期")]
    public new DateTime? ProductionDate { get; set; }

    [LogicName("生产计划号")]
    public long? ProductPlan_ID { get; set; }

    [ReferenceTo(typeof(ProductPlan), "PlanNumber")]
    [Join("ProductPlan_ID", "ID")]
    [LogicName("生产计划号")]
    public string ProductPlan_Name { get; set; }

    [LogicName("货位ID")]
    [DFPrompt("货位")]
    public long? CargoSpace_ID { get; set; }

    [ReferenceTo(typeof(CargoSpace), "Name")]
    [Join("CargoSpace_ID", "ID")]
    [DFPrompt("货位")]
    public string CargoSpace_Name { get; set; }

    #region 仙坛个性字段

    [LogicName("参考单价")]
    public Money<decimal>? AvgPrice { get; set; }

     [LogicName("参考金额")]
    public Money<金额>? AvgMoeny { get; set; }

    #endregion

    #region 耘垦个性字段

    [LogicName("生产完工明细ID")]
    public long? ProduceFinish_Detail_ID { get; set; }

    #endregion

    [NonDmoProperty]
    public short BillType {
      get { return DmoTypeIDAttribute.GetID(typeof(ProductInStore)); }
    }
  }

  [Serializable]
	public class ProductInStore_DetailCollection : DmoCollection<ProductInStore_Detail>
	{ }
}
