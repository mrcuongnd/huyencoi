namespace ExternalDataConvert
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cBoxSystems = new System.Windows.Forms.ComboBox();
            this.cBoxTypeData = new System.Windows.Forms.ComboBox();
            this.txtBrowserSource = new System.Windows.Forms.TextBox();
            this.btnBrowserSource = new System.Windows.Forms.Button();
            this.txtBrowserDes = new System.Windows.Forms.TextBox();
            this.btnBrowserDes = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSetting = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // cBoxSystems
            // 
            this.cBoxSystems.FormattingEnabled = true;
            this.cBoxSystems.Items.AddRange(new object[] {
            "OPS6000",
            "OPS9000"});
            this.cBoxSystems.Location = new System.Drawing.Point(107, 19);
            this.cBoxSystems.Name = "cBoxSystems";
            this.cBoxSystems.Size = new System.Drawing.Size(148, 21);
            this.cBoxSystems.TabIndex = 0;
            this.cBoxSystems.Text = "OPS6000";
            // 
            // cBoxTypeData
            // 
            this.cBoxTypeData.FormattingEnabled = true;
            this.cBoxTypeData.Items.AddRange(new object[] {
            "Trend",
            "Message"});
            this.cBoxTypeData.Location = new System.Drawing.Point(107, 65);
            this.cBoxTypeData.Name = "cBoxTypeData";
            this.cBoxTypeData.Size = new System.Drawing.Size(148, 21);
            this.cBoxTypeData.TabIndex = 1;
            this.cBoxTypeData.Text = "Trend";
            // 
            // txtBrowserSource
            // 
            this.txtBrowserSource.Location = new System.Drawing.Point(107, 120);
            this.txtBrowserSource.Name = "txtBrowserSource";
            this.txtBrowserSource.Size = new System.Drawing.Size(193, 20);
            this.txtBrowserSource.TabIndex = 2;
            // 
            // btnBrowserSource
            // 
            this.btnBrowserSource.Location = new System.Drawing.Point(306, 118);
            this.btnBrowserSource.Name = "btnBrowserSource";
            this.btnBrowserSource.Size = new System.Drawing.Size(75, 23);
            this.btnBrowserSource.TabIndex = 3;
            this.btnBrowserSource.Text = "参照";
            this.btnBrowserSource.UseVisualStyleBackColor = true;
            this.btnBrowserSource.Click += new System.EventHandler(this.btnBrowserSource_Click);
            // 
            // txtBrowserDes
            // 
            this.txtBrowserDes.Location = new System.Drawing.Point(107, 157);
            this.txtBrowserDes.Name = "txtBrowserDes";
            this.txtBrowserDes.Size = new System.Drawing.Size(193, 20);
            this.txtBrowserDes.TabIndex = 4;
            // 
            // btnBrowserDes
            // 
            this.btnBrowserDes.Location = new System.Drawing.Point(306, 155);
            this.btnBrowserDes.Name = "btnBrowserDes";
            this.btnBrowserDes.Size = new System.Drawing.Size(75, 23);
            this.btnBrowserDes.TabIndex = 5;
            this.btnBrowserDes.Text = "参照";
            this.btnBrowserDes.UseVisualStyleBackColor = true;
            this.btnBrowserDes.Click += new System.EventHandler(this.btnBrowserDes_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "システム種別";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "データ種別";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "移行元フォルダ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 160);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "移行先フォルダ";
            // 
            // btnSetting
            // 
            this.btnSetting.Location = new System.Drawing.Point(207, 220);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(75, 23);
            this.btnSetting.TabIndex = 10;
            this.btnSetting.Text = "設定";
            this.btnSetting.UseVisualStyleBackColor = true;
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(306, 220);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "消去";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(407, 260);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSetting);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnBrowserDes);
            this.Controls.Add(this.txtBrowserDes);
            this.Controls.Add(this.btnBrowserSource);
            this.Controls.Add(this.txtBrowserSource);
            this.Controls.Add(this.cBoxTypeData);
            this.Controls.Add(this.cBoxSystems);
            this.Name = "frmMain";
            this.Text = "外部保存データ移行ツール";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cBoxSystems;
        private System.Windows.Forms.ComboBox cBoxTypeData;
        private System.Windows.Forms.TextBox txtBrowserSource;
        private System.Windows.Forms.Button btnBrowserSource;
        private System.Windows.Forms.TextBox txtBrowserDes;
        private System.Windows.Forms.Button btnBrowserDes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSetting;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
    }
}

