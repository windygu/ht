﻿namespace B3ButcheryCE.FrozenInStoreConfirm_
{
    partial class FrozenInStoreConfirmScan
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtFrozenInStoreID = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(55, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.Text = "速冻库号:";
            // 
            // txtFrozenInStoreID
            // 
            this.txtFrozenInStoreID.Location = new System.Drawing.Point(55, 98);
            this.txtFrozenInStoreID.Name = "txtFrozenInStoreID";
            this.txtFrozenInStoreID.Size = new System.Drawing.Size(139, 23);
            this.txtFrozenInStoreID.TabIndex = 1;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(83, 144);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(72, 29);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "确  定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // FrozenInStoreConfirmScan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtFrozenInStoreID);
            this.Controls.Add(this.label1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrozenInStoreConfirmScan";
            this.Text = "扫描速冻库号";
            this.Deactivate += new System.EventHandler(this.FrozenInStoreConfirmScan_Deactivate);
            this.Activated += new System.EventHandler(this.FrozenInStoreConfirmScan_Activated);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrozenInStoreConfirmScan_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFrozenInStoreID;
        private System.Windows.Forms.Button btnOK;
    }
}