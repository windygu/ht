﻿namespace B3ButcheryCE.ProductInStoreConfirm_
{
    partial class ProductInStoreConfirmOK
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.listView1 = new System.Windows.Forms.ListView();
            this.存货名称 = new System.Windows.Forms.ColumnHeader();
            this.辅数量 = new System.Windows.Forms.ColumnHeader();
            this.主数量 = new System.Windows.Forms.ColumnHeader();
            this.btnOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.Add(this.存货名称);
            this.listView1.Columns.Add(this.辅数量);
            this.listView1.Columns.Add(this.主数量);
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(238, 230);
            this.listView1.TabIndex = 0;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.ItemActivate += new System.EventHandler(this.listView1_ItemActivate);
            // 
            // 存货名称
            // 
            this.存货名称.Text = "存货名称";
            this.存货名称.Width = 162;
            // 
            // 辅数量
            // 
            this.辅数量.Text = "辅数量";
            this.辅数量.Width = 60;
            // 
            // 主数量
            // 
            this.主数量.Text = "主数量";
            this.主数量.Width = 60;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(64, 236);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(107, 26);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "确认";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // ProductInStoreConfirmOK
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.listView1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProductInStoreConfirmOK";
            this.Text = "确认库存";
            this.Load += new System.EventHandler(this.ProductInStoreConfirmOK_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ProductInStoreConfirmOK_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader 存货名称;
        private System.Windows.Forms.ColumnHeader 主数量;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.ColumnHeader 辅数量;
    }
}