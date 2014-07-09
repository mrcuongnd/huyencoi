namespace OPS5000Retrofit
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cboxSystemType = new System.Windows.Forms.ComboBox();
            this.cboxDataType = new System.Windows.Forms.ComboBox();
            this.rbtnMoveAll = new System.Windows.Forms.RadioButton();
            this.rbtnMoveByTime = new System.Windows.Forms.RadioButton();
            this.numStartYear = new System.Windows.Forms.NumericUpDown();
            this.numStartMonth = new System.Windows.Forms.NumericUpDown();
            this.numStartDay = new System.Windows.Forms.NumericUpDown();
            this.numStartHour = new System.Windows.Forms.NumericUpDown();
            this.numEndYear = new System.Windows.Forms.NumericUpDown();
            this.numEndMonth = new System.Windows.Forms.NumericUpDown();
            this.numEndDay = new System.Windows.Forms.NumericUpDown();
            this.numEndHour = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.btnBrowser = new System.Windows.Forms.Button();
            this.btnConvert = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.numStartYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStartMonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStartDay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStartHour)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEndYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEndMonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEndDay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEndHour)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "システム種別";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "データ種別";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "移行種別";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 139);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "開始時刻";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 175);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "終了時刻";
            // 
            // cboxSystemType
            // 
            this.cboxSystemType.FormattingEnabled = true;
            this.cboxSystemType.Items.AddRange(new object[] {
            "OPS6000",
            "DSVS6000",
            "OPS9000",
            "DSVS9000"});
            this.cboxSystemType.Location = new System.Drawing.Point(120, 21);
            this.cboxSystemType.Name = "cboxSystemType";
            this.cboxSystemType.Size = new System.Drawing.Size(121, 21);
            this.cboxSystemType.TabIndex = 5;
            this.cboxSystemType.Text = "OPS6000";
            // 
            // cboxDataType
            // 
            this.cboxDataType.FormattingEnabled = true;
            this.cboxDataType.Items.AddRange(new object[] {
            "時データ",
            "日データ",
            "月データ",
            "年データ",
            "コメントデータ"});
            this.cboxDataType.Location = new System.Drawing.Point(120, 57);
            this.cboxDataType.Name = "cboxDataType";
            this.cboxDataType.Size = new System.Drawing.Size(121, 21);
            this.cboxDataType.TabIndex = 6;
            this.cboxDataType.Text = "時データ";
            // 
            // rbtnMoveAll
            // 
            this.rbtnMoveAll.AutoSize = true;
            this.rbtnMoveAll.Checked = true;
            this.rbtnMoveAll.Location = new System.Drawing.Point(120, 96);
            this.rbtnMoveAll.Name = "rbtnMoveAll";
            this.rbtnMoveAll.Size = new System.Drawing.Size(73, 17);
            this.rbtnMoveAll.TabIndex = 7;
            this.rbtnMoveAll.TabStop = true;
            this.rbtnMoveAll.Text = "一括移行";
            this.rbtnMoveAll.UseVisualStyleBackColor = true;
            this.rbtnMoveAll.CheckedChanged += new System.EventHandler(this.rbtnMoveAll_CheckedChanged);
            // 
            // rbtnMoveByTime
            // 
            this.rbtnMoveByTime.AutoSize = true;
            this.rbtnMoveByTime.Location = new System.Drawing.Point(211, 96);
            this.rbtnMoveByTime.Name = "rbtnMoveByTime";
            this.rbtnMoveByTime.Size = new System.Drawing.Size(97, 17);
            this.rbtnMoveByTime.TabIndex = 8;
            this.rbtnMoveByTime.TabStop = true;
            this.rbtnMoveByTime.Text = "期間指定移行";
            this.rbtnMoveByTime.UseVisualStyleBackColor = true;
            this.rbtnMoveByTime.CheckedChanged += new System.EventHandler(this.rbtnMoveBySelect_CheckedChanged);
            // 
            // numStartYear
            // 
            this.numStartYear.Location = new System.Drawing.Point(120, 131);
            this.numStartYear.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numStartYear.Name = "numStartYear";
            this.numStartYear.Size = new System.Drawing.Size(59, 20);
            this.numStartYear.TabIndex = 9;
            this.numStartYear.Value = new decimal(new int[] {
            1900,
            0,
            0,
            0});
            // 
            // numStartMonth
            // 
            this.numStartMonth.Location = new System.Drawing.Point(230, 131);
            this.numStartMonth.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.numStartMonth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numStartMonth.Name = "numStartMonth";
            this.numStartMonth.Size = new System.Drawing.Size(35, 20);
            this.numStartMonth.TabIndex = 10;
            this.numStartMonth.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
            // 
            // numStartDay
            // 
            this.numStartDay.Location = new System.Drawing.Point(298, 131);
            this.numStartDay.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.numStartDay.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numStartDay.Name = "numStartDay";
            this.numStartDay.Size = new System.Drawing.Size(35, 20);
            this.numStartDay.TabIndex = 11;
            this.numStartDay.Value = new decimal(new int[] {
            31,
            0,
            0,
            0});
            // 
            // numStartHour
            // 
            this.numStartHour.Location = new System.Drawing.Point(364, 131);
            this.numStartHour.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.numStartHour.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numStartHour.Name = "numStartHour";
            this.numStartHour.Size = new System.Drawing.Size(35, 20);
            this.numStartHour.TabIndex = 12;
            this.numStartHour.Value = new decimal(new int[] {
            24,
            0,
            0,
            0});
            // 
            // numEndYear
            // 
            this.numEndYear.Location = new System.Drawing.Point(120, 168);
            this.numEndYear.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numEndYear.Name = "numEndYear";
            this.numEndYear.Size = new System.Drawing.Size(59, 20);
            this.numEndYear.TabIndex = 13;
            this.numEndYear.Value = new decimal(new int[] {
            1900,
            0,
            0,
            0});
            // 
            // numEndMonth
            // 
            this.numEndMonth.Location = new System.Drawing.Point(230, 168);
            this.numEndMonth.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.numEndMonth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numEndMonth.Name = "numEndMonth";
            this.numEndMonth.Size = new System.Drawing.Size(35, 20);
            this.numEndMonth.TabIndex = 14;
            this.numEndMonth.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
            // 
            // numEndDay
            // 
            this.numEndDay.Location = new System.Drawing.Point(298, 168);
            this.numEndDay.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.numEndDay.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numEndDay.Name = "numEndDay";
            this.numEndDay.Size = new System.Drawing.Size(35, 20);
            this.numEndDay.TabIndex = 15;
            this.numEndDay.Value = new decimal(new int[] {
            31,
            0,
            0,
            0});
            // 
            // numEndHour
            // 
            this.numEndHour.Location = new System.Drawing.Point(364, 168);
            this.numEndHour.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.numEndHour.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numEndHour.Name = "numEndHour";
            this.numEndHour.Size = new System.Drawing.Size(35, 20);
            this.numEndHour.TabIndex = 16;
            this.numEndHour.Value = new decimal(new int[] {
            24,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(185, 133);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(19, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "年";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(271, 133);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(19, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "月";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(339, 133);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(19, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "日";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(405, 133);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(19, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "時";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(185, 170);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(19, 13);
            this.label10.TabIndex = 21;
            this.label10.Text = "年";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(271, 170);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(19, 13);
            this.label11.TabIndex = 22;
            this.label11.Text = "月";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(339, 170);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(19, 13);
            this.label12.TabIndex = 23;
            this.label12.Text = "日";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(405, 170);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(19, 13);
            this.label13.TabIndex = 24;
            this.label13.Text = "時";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(21, 226);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(78, 13);
            this.label14.TabIndex = 25;
            this.label14.Text = "移行元フォルダ";
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(120, 224);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(238, 20);
            this.txtPath.TabIndex = 26;
            // 
            // btnBrowser
            // 
            this.btnBrowser.Location = new System.Drawing.Point(364, 222);
            this.btnBrowser.Name = "btnBrowser";
            this.btnBrowser.Size = new System.Drawing.Size(75, 23);
            this.btnBrowser.TabIndex = 27;
            this.btnBrowser.Text = "参照";
            this.btnBrowser.UseVisualStyleBackColor = true;
            this.btnBrowser.Click += new System.EventHandler(this.btnBrowser_Click);
            // 
            // btnConvert
            // 
            this.btnConvert.Location = new System.Drawing.Point(246, 289);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(75, 23);
            this.btnConvert.TabIndex = 28;
            this.btnConvert.Text = "設定";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(364, 289);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 29;
            this.btnCancel.Text = "消去";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(462, 330);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.btnBrowser);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.numEndHour);
            this.Controls.Add(this.numEndDay);
            this.Controls.Add(this.numEndMonth);
            this.Controls.Add(this.numEndYear);
            this.Controls.Add(this.numStartHour);
            this.Controls.Add(this.numStartDay);
            this.Controls.Add(this.numStartMonth);
            this.Controls.Add(this.numStartYear);
            this.Controls.Add(this.rbtnMoveByTime);
            this.Controls.Add(this.rbtnMoveAll);
            this.Controls.Add(this.cboxDataType);
            this.Controls.Add(this.cboxSystemType);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "帳票バンクデータ移行ツール";
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numStartYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStartMonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStartDay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStartHour)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEndYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEndMonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEndDay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEndHour)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboxSystemType;
        private System.Windows.Forms.ComboBox cboxDataType;
        private System.Windows.Forms.RadioButton rbtnMoveAll;
        private System.Windows.Forms.RadioButton rbtnMoveByTime;
        private System.Windows.Forms.NumericUpDown numStartYear;
        private System.Windows.Forms.NumericUpDown numStartMonth;
        private System.Windows.Forms.NumericUpDown numStartDay;
        private System.Windows.Forms.NumericUpDown numStartHour;
        private System.Windows.Forms.NumericUpDown numEndYear;
        private System.Windows.Forms.NumericUpDown numEndMonth;
        private System.Windows.Forms.NumericUpDown numEndDay;
        private System.Windows.Forms.NumericUpDown numEndHour;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button btnBrowser;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
    }
}

