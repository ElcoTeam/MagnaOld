namespace MGNMEServer
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.timer_fs1 = new System.Windows.Forms.Timer(this.components);
            this.timer_fs2 = new System.Windows.Forms.Timer(this.components);
            this.timer_fsb = new System.Windows.Forms.Timer(this.components);
            this.timer_fsc = new System.Windows.Forms.Timer(this.components);
            this.timer_rsb40 = new System.Windows.Forms.Timer(this.components);
            this.timer_rsb60 = new System.Windows.Forms.Timer(this.components);
            this.timer_rsc = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(64, 35);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(157, 49);
            this.button1.TabIndex = 0;
            this.button1.Text = "下发订单";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(64, 137);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(157, 55);
            this.button2.TabIndex = 1;
            this.button2.Text = "暂停下发";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // timer_fs1
            // 
            this.timer_fs1.Interval = 1000;
            this.timer_fs1.Tick += new System.EventHandler(this.timer_fs1_Tick);
            // 
            // timer_fs2
            // 
            this.timer_fs2.Interval = 1000;
           // this.timer_fs2.Tick += new System.EventHandler(this.timer_fs2_Tick);
            // 
            // timer_fsb
            // 
            this.timer_fsb.Interval = 1000;
            this.timer_fsb.Tick += new System.EventHandler(this.timer_fsb_Tick);
            // 
            // timer_fsc
            // 
            this.timer_fsc.Interval = 1000;
            this.timer_fsc.Tick += new System.EventHandler(this.timer_fsc_Tick);
            // 
            // timer_rsb40
            // 
            this.timer_rsb40.Interval = 1000;
            this.timer_rsb40.Tick += new System.EventHandler(this.timer_rsb40_Tick);
            // 
            // timer_rsb60
            // 
            this.timer_rsb60.Interval = 1000;
            this.timer_rsb60.Tick += new System.EventHandler(this.timer_rsb60_Tick);
            // 
            // timer_rsc
            // 
            this.timer_rsc.Interval = 1000;
            this.timer_rsc.Tick += new System.EventHandler(this.timer_rsc_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "l";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Timer timer_fs1;
        private System.Windows.Forms.Timer timer_fs2;
        private System.Windows.Forms.Timer timer_fsb;
        private System.Windows.Forms.Timer timer_fsc;
        private System.Windows.Forms.Timer timer_rsb40;
        private System.Windows.Forms.Timer timer_rsb60;
        private System.Windows.Forms.Timer timer_rsc;
    }
}

