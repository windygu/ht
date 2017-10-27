﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using BWP.B3Butchery.BO;
using BWP.B3Butchery.Utils;
using BWP.B3Frameworks;
using BWP.B3Frameworks.BO;
using BWP.B3Frameworks.BO.NamedValueTemplate;
using BWP.B3Frameworks.Utils;
using BWP.B3UnitedInfos.BO;
using BWP.Web.Layout;
using BWP.Web.Utils;
using BWP.Web.WebControls;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.EnterpriseServices.DomainObjects2.DQuery;
using Forks.EnterpriseServices.SqlDoms;
using Forks.Utils;
using TSingSoft.WebControls2;
using DataKind = BWP.B3Frameworks.B3FrameworksConsts.DataSources;


namespace BWP.Web.Pages.B3Butchery.Reports.QuickFreezeInStoreDiffReport_
{
    public class QuickFreezeInStoreDiffReport : DFBrowseGridReportPage
    {
        DFInfo mainInfo = DFInfo.Get(typeof(FrozenInStore));
        protected override string AccessRoleName
        {
            get { return "B3Butchery.报表.速冻与入库差异表"; }
        }

        protected override string Caption
        {
            get { return "速冻与入库差异表"; }
        }

        CheckBoxListWithReverseSelect checkbox;
        protected override void InitQueryPanel(QueryPanel queryPanel)
        {
            base.InitQueryPanel(queryPanel);
            var panel = queryPanel.CreateTab("显示字段");
            checkbox = new CheckBoxListWithReverseSelect() { RepeatColumns = 6, RepeatDirection = RepeatDirection.Horizontal };
            //显示字段包括：{会计单位}、{计划号}、{存货名称}、{存货编码}、{速冻入库数量}（速冻入库单主数量）、{入库数量}（入库单存货主数量）、{差异数量}（速冻入库数量-入库数量）
            checkbox.Items.Add(new ListItem("会计单位", "AccountingUnit_Name"));
            checkbox.Items.Add(new ListItem("计划号", "ProductionPlan_Name"));
            checkbox.Items.Add(new ListItem("存货名称", "Name"));
            checkbox.Items.Add(new ListItem("存货编码", "Code"));
            checkbox.Items.Add(new ListItem("速冻入库数量", "QuickFreezeNumber"));
            checkbox.Items.Add(new ListItem("入库数量", "InStoreNumber"));
            checkbox.Items.Add(new ListItem("差异数量", "DiffNumber"));

            panel.EAdd(checkbox);
            queryPanel.ConditonPanel.EAdd(CreateDataRangePanel());
        }

        DateInput sd, ed;
        Control CreateDataRangePanel()
        {
            var hPanel = new HLayoutPanel();
            hPanel.Add(new SimpleLabel("时间"));
            sd = hPanel.Add(new DateInput());
            hPanel.Add(new LiteralControl("→"));
            ed = hPanel.Add(new DateInput());
            return hPanel;
        }

        protected override void AddQueryControls(VLayoutPanel vPanel)
        {
            var customPanel = new LayoutManager("Main", mainInfo, mQueryContainer);
            //查询条件包括：：{会计单位}、{计划号}、{存货名称}、{存货编码}

            customPanel.Add("AccountingUnit_ID", QueryCreator.DFChoiceBoxEnableMultiSelection(mainInfo.Fields["AccountingUnit_ID"], mQueryContainer, "AccountingUnit_ID", DataKind.授权会计单位全部));
            customPanel["AccountingUnit_ID"].NotAutoAddToContainer = true;

            customPanel.Add("ProductionPlan_ID", QueryCreator.DFChoiceBoxEnableMultiSelection(mainInfo.Fields["ProductionPlan_ID"], mQueryContainer, "ProductionPlan_ID", B3ButcheryDataSource.计划号));
            customPanel["ProductionPlan_ID"].NotAutoAddToContainer = true;

            customPanel.Add("Goods_Name", new SimpleLabel("存货名称"), QueryCreator.DFTextBox(mainInfo.Fields["AccountingUnit_Name"]));
            customPanel.Add("Goods_Code", new SimpleLabel("存货编号"), QueryCreator.DFTextBox(mainInfo.Fields["Department_Name"]));
            customPanel.CreateDefaultConfig(2).Expand = false;
            vPanel.Add(customPanel.CreateLayout());
        }

        protected override DQueryDom GetQueryDom()
        {
            var alias = new JoinAlias(typeof(tempClass));
            var query = new DQueryDom(alias);
            query.RegisterQueryTable(typeof(tempClass), new string[] { "Department_ID", "AccountingUnit_ID", "ProductionPlan_ID", "AccountingUnit_Name", "ProductionPlan_Name", "Name", "Code", "QuickFreezeNumber", "InStoreNumber" }, tempClass.GetQueryDom(mQueryContainer, sd, ed));
            foreach (ListItem field in checkbox.Items)
            {
                if (field.Selected)
                {
                    if (field.Value == "QuickFreezeNumber" || field.Value == "InStoreNumber")
                    {
                        query.Columns.Add(DQSelectColumn.Sum(field.Value));
                        SumColumnIndexs.Add(query.Columns.Count - 1);
                    }
                    else if (field.Value == "DiffNumber")
                    {
                        query.Columns.Add(DQSelectColumn.Create(DQExpression.Sum(DQExpression.Subtract(DQExpression.IfNull(DQExpression.Field("QuickFreezeNumber"), DQExpression.Value(0)), DQExpression.IfNull(DQExpression.Field("InStoreNumber"), DQExpression.Value(0)))), field.Text));
                        SumColumnIndexs.Add(query.Columns.Count - 1);
                    }
                    else
                    {
                        query.Columns.Add(DQSelectColumn.Field(field.Value));
                        query.GroupBy.Expressions.Add(DQExpression.Field(field.Value));
                    }
                }
            }
            var accids = mQueryContainer.GetControl<DFChoiceBox>("AccountingUnit_ID").Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

            var pnumberids = mQueryContainer.GetControl<DFChoiceBox>("ProductionPlan_ID").Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            if (accids.Length > 0)
                query.Where.Conditions.Add(DQCondition.InList(DQExpression.Field("AccountingUnit_ID"), accids.Select(x => DQExpression.Value(long.Parse(x))).ToArray()));

            if (pnumberids.Length > 0)
                query.Where.Conditions.Add(DQCondition.InList(DQExpression.Field("ProductionPlan_ID"), pnumberids.Select(x => DQExpression.Value(long.Parse(x))).ToArray()));

            OrganizationUtil.AddOrganizationLimit<Department>(query, "Department_ID");

            if (query.Columns.Count == 0)
                throw new Exception("至少选择一条显示列");
            return query;
        }

        [DFClass]
        class tempClass
        {
            public long? Department_ID { get; set; }
            public long? AccountingUnit_ID { get; set; }
            public long? ProductionPlan_ID { get; set; }
            [LogicName("会计单位")]
            public string AccountingUnit_Name { get; set; }
            [LogicName("计划号")]
            public string ProductionPlan_Name { get; set; }
            [LogicName("存货名称")]
            public string Name { get; set; }
            [LogicName("存货编码")]
            public string Code { get; set; }
            [LogicName("速冻入库数量")]
            public Money<decimal>? QuickFreezeNumber { get; set; }
            [LogicName("入库数量")]
            public Money<decimal>? InStoreNumber { get; set; }

            public static DQueryDom GetQueryDom(QueryContainer mQueryContainer, DateInput sd, DateInput ed)
            {
                var domainID = DomainContext.Current.ID;
                var outputQuery = GetQuickFreezeQuery(domainID, mQueryContainer, sd, ed);
                var instoreQuery = GetInStoreQuery(domainID, mQueryContainer, sd, ed);
                outputQuery.UnionNext.Select = instoreQuery;
                outputQuery.UnionNext.Type = UnionType.All;
                return outputQuery;
            }
        }

        static DQueryDom GetQuickFreezeQuery(long domainID, QueryContainer mQueryContainer, DateInput sd, DateInput ed)
        {
            var main = new JoinAlias(typeof(FrozenInStore));
            var detail = new JoinAlias(typeof(FrozenInStore_Detail));
            var goodsAlias = new JoinAlias("g1", typeof(Goods));
            var query = new DQueryDom(detail);
            query.From.AddJoin(JoinType.Left, new DQDmoSource(main), DQCondition.EQ(main, "ID", detail, "FrozenInStore_ID"));
            query.From.AddJoin(JoinType.Left, new DQDmoSource(goodsAlias), DQCondition.EQ(detail, "Goods_ID", goodsAlias, "ID"));
            query.Columns.Add(DQSelectColumn.Field("Department_ID", main));
            query.Columns.Add(DQSelectColumn.Field("AccountingUnit_ID", main));
            query.Columns.Add(DQSelectColumn.Field("ProductionPlan_ID", main));
            query.Columns.Add(DQSelectColumn.Field("AccountingUnit_Name", main));
            query.Columns.Add(DQSelectColumn.Field("ProductionPlan_PlanNumber", main));
            query.Columns.Add(DQSelectColumn.Field("Name", goodsAlias));
            query.Columns.Add(DQSelectColumn.Field("Code", goodsAlias));
            query.Columns.Add(DQSelectColumn.Create(DQExpression.Value<Money<decimal>>(0m), "QuickFreezeNumberInStoreNumber"));
            query.Columns.Add(DQSelectColumn.Create(DQExpression.Field(detail, "Number"), "QuickFreezeNumber"));
            query.Where.Conditions.Add(DQCondition.And(DQCondition.EQ(main, "BillState", 单据状态.已审核), DQCondition.EQ(main, "Domain_ID", domainID)));
            AddCondition(main, goodsAlias, query, "Date", mQueryContainer, sd, ed);
            return query;
        }

        static DQueryDom GetInStoreQuery(long domainID, QueryContainer mQueryContainer, DateInput sd, DateInput ed)
        {
            var main = new JoinAlias(typeof(ProductInStore));
            var detail = new JoinAlias(typeof(ProductInStore_Detail));
            var banCPAlias = new JoinAlias(typeof(ChengPinToBanChengPinConfig));
            var goodsAlias = new JoinAlias("g2", typeof(Goods));
            var query = new DQueryDom(detail);
            query.From.AddJoin(JoinType.Left, new DQDmoSource(main), DQCondition.EQ(main, "ID", detail, "ProductInStore_ID"));
            query.From.AddJoin(JoinType.Left, new DQDmoSource(banCPAlias), DQCondition.EQ(detail, "Goods_ID", banCPAlias, "Goods_ID"));
            query.From.AddJoin(JoinType.Left, new DQDmoSource(goodsAlias), DQCondition.EQ(banCPAlias, "Goods2_ID", goodsAlias, "ID"));
            query.Columns.Add(DQSelectColumn.Field("Department_ID", main));
            query.Columns.Add(DQSelectColumn.Field("AccountingUnit_ID", main));
            query.Columns.Add(DQSelectColumn.Create(DQExpression.Field(detail, "ProductPlan_ID"), "ProductionPlan_ID"));
            query.Columns.Add(DQSelectColumn.Field("AccountingUnit_Name", main));
            query.Columns.Add(DQSelectColumn.Create(DQExpression.Field(detail, "ProductPlan_Name"), "ProductionPlan_Name"));
            query.Columns.Add(DQSelectColumn.Field("Name", goodsAlias));
            query.Columns.Add(DQSelectColumn.Field("Code", goodsAlias));
            query.Columns.Add(DQSelectColumn.Create(DQExpression.Value<Money<decimal>>(0m), "InStoreNumberQuickFreezeNumber"));
            query.Columns.Add(DQSelectColumn.Create(DQExpression.Field(detail, "Number"), "InStoreNumber"));
            query.Where.Conditions.Add(DQCondition.And(DQCondition.EQ(main, "BillState", 单据状态.已审核), DQCondition.EQ(main, "Domain_ID", domainID)));
            AddCondition(main, goodsAlias, query, "InStoreDate", mQueryContainer, sd, ed);
            return query;
        }

        static void AddCondition(JoinAlias main, JoinAlias goodsAlias, DQueryDom query, string dtFile, QueryContainer mQueryContainer, DateInput sd, DateInput ed)
        {
            var goodsName = mQueryContainer.GetControl<DFTextBox>("Goods_Name").Text;
            var goodsCode = mQueryContainer.GetControl<DFTextBox>("Goods_Code").Text;
            if (!string.IsNullOrEmpty(goodsName))
                query.Where.Conditions.Add(DQCondition.Or(DQCondition.Like(goodsAlias, "Name", goodsName), DQCondition.Like(goodsAlias, "Spell", goodsName)));
            if (!string.IsNullOrEmpty(goodsCode))
                query.Where.Conditions.Add(DQCondition.Like(goodsAlias, "Code", goodsCode));
            if (sd.Value.HasValue)
                query.Where.Conditions.Add(DQCondition.GreaterThanOrEqual(main, dtFile, sd.Value.Value));
            if (ed.Value.HasValue)
                query.Where.Conditions.Add(DQCondition.LessThanOrEqual(main, dtFile, ed.Value.Value));
        }
    }
}
