﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Forks.EnterpriseServices.BusinessInterfaces;
using BWP.B3Frameworks.BL;
using BWP.B3Butchery.BO;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.EnterpriseServices.DomainObjects2.DQuery;
using Forks.EnterpriseServices.SqlDoms;
using Forks.Utils;
using BWP.B3Frameworks.BO.NamedValueTemplate;
using Forks.EnterpriseServices;

namespace BWP.B3Butchery.BL
{

  [BusinessInterface(typeof(ProduceOutputBL))]
	[LogicName("产出单")]
  public interface IProduceOutputBL : IDepartmentWorkFlowBillBL<ProduceOutput>
  {
    //载入明细
    void GetGoodsDetailList(ProduceOutput Dmo);
  }

  public class ProduceOutputBL : DepartmentWorkFlowBillBL<ProduceOutput>, IProduceOutputBL
  {
    /// <summary>
    /// 载入明细
    /// </summary>
    /// <param name="dmo"></param>
    public void GetGoodsDetailList(ProduceOutput dmo)
    {
      var goodsList = GetGoodsList(dmo);
      foreach (var goods in goodsList)
      {
        var detail = new ProduceOutput_Detail();
        detail.Goods_ID = goods.Goods_ID;
        detail.Goods_Name = goods.Goods_Name;
        detail.Goods_Code = goods.Goods_Code;
        detail.Goods_Spec = goods.Goods_Spec;
        detail.Goods_MainUnit = goods.Goods_MainUnit;
        detail.Goods_SecondUnit = goods.Goods_SecondUnit;
        detail.Number = (Money<decimal>?)goods.Number;
        detail.SecondNumber = (Money<decimal>?)goods.SecondNumber;

        dmo.Details.Add(detail);
      };
    }

    //载入存货列表
    public List<ProductPlanOrLinkOutput> GetGoodsList(ProduceOutput dmo)
    {
      var planBill = new JoinAlias(typeof(ProductPlan));
      var outputPlanDetail = new JoinAlias(typeof(ProductPlan_OutputDetail));
      var linkDetail = new JoinAlias(typeof(ProductLinks_OutputDetail));
      DQueryDom query = new DQueryDom(outputPlanDetail);
      query.From.AddJoin(JoinType.Inner, new DQDmoSource(planBill), DQCondition.EQ(planBill, "ID", outputPlanDetail, "ProductPlan_ID"));
      query.From.AddJoin(JoinType.Inner, new DQDmoSource(linkDetail), DQCondition.EQ(linkDetail, "Goods_ID", outputPlanDetail, "Goods_ID"));

      query.Where.Conditions.Add(DQCondition.And(DQCondition.EQ(planBill, "BillState", 单据状态.已审核),
                 DQCondition.EQ(planBill, "PlanNumber", dmo.PlanNumber_Name),
                 DQCondition.EQ(linkDetail, "ProductLinks_ID", dmo.ProductLinks_ID)));

      query.Columns.Add(DQSelectColumn.Field("Goods_ID"));
      query.Columns.Add(DQSelectColumn.Field("Goods_Name"));
      query.Columns.Add(DQSelectColumn.Field("Goods_Code"));
      query.Columns.Add(DQSelectColumn.Field("Goods_Spec"));
      query.Columns.Add(DQSelectColumn.Field("Goods_MainUnit"));
      query.Columns.Add(DQSelectColumn.Field("Goods_SecondUnit"));
      query.Columns.Add(DQSelectColumn.Sum("PlanNumber"));
      query.Columns.Add(DQSelectColumn.Sum("PlanSecondNumber"));

      query.GroupBy.Expressions.Add(DQExpression.Field("Goods_ID"));
      query.GroupBy.Expressions.Add(DQExpression.Field("Goods_Name"));
      query.GroupBy.Expressions.Add(DQExpression.Field("Goods_Code"));
      query.GroupBy.Expressions.Add(DQExpression.Field("Goods_Spec"));
      query.GroupBy.Expressions.Add(DQExpression.Field("Goods_MainUnit"));
      query.GroupBy.Expressions.Add(DQExpression.Field("Goods_SecondUnit"));

      var list = new List<ProductPlanOrLinkOutput>();
      using (var context = new TransactionContext())
      {
        using (var reader = context.Session.ExecuteReader(query))
        {
          while (reader.Read())
          {
            var goodsList = new ProductPlanOrLinkOutput();
            goodsList.Goods_ID = (long)reader[0];
            goodsList.Goods_Name = (string)reader[1];
            goodsList.Goods_Code = (string)reader[2];
            goodsList.Goods_Spec = (string)reader[3];
            goodsList.Goods_MainUnit = (string)reader[4];
            goodsList.Goods_SecondUnit = (string)reader[5];
            goodsList.Number = (Money<decimal>?)reader[6];
            goodsList.SecondNumber = (Money<decimal>?)reader[7];
            list.Add(goodsList);
          }
        }
      }

      return list;
    }
    public class ProductPlanOrLinkOutput
    {
      public long Goods_ID { get; set; }

      public string Goods_Name { get; set; }

      public string Goods_Code { get; set; }

      public string Goods_Spec { get; set; }

      public string Goods_MainUnit { get; set; }

      public string Goods_SecondUnit { get; set; }

      public Money<decimal>? Number { get; set; }

      public Money<decimal>? SecondNumber { get; set; }
    }

  }
}
